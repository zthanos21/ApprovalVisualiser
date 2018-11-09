using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ApprovalVisualiser.Models
{
    public class EntityAnalyzer<T> where T : new()
    {
        Type typeToProcess;


        public EntityAnalyzer()
        {
            typeToProcess = new T().GetType();
            DerivedProperties = new HashSet<PropertyDefinition>();
            ObjectProperties = new HashSet<PropertyDefinition>();
        }


        private void ExtractDerivedProperties()
        {
            var propertyInfos = typeToProcess.BaseType.GetProperties();

            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                DerivedProperties.Add(GetPropertyDefinition(propertyInfo));
            }
        }

        public void Analyze()
        {
            ExtractDerivedProperties();
            var propertyInfos = typeToProcess.GetProperties();
            Name = typeToProcess.Name;
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                var props = GetPropertyDefinition(propertyInfo);
                if (!DerivedProperties.Any(a => a.Name == props.Name))
                    ObjectProperties.Add(props);
            }

        }

        private static PropertyDefinition GetPropertyDefinition(PropertyInfo propertyInfo)
        {
            PropertyDefinition value = new PropertyDefinition();
            Type type = propertyInfo.PropertyType;
            value.IsCollection = propertyInfo.PropertyType.Namespace.Contains("Collection");
            value.IsEntity = propertyInfo.PropertyType.Namespace.Contains("Entities.Models");
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                value.Type = (Nullable.GetUnderlyingType(type) + "?").Split('.').LastOrDefault();
                value.Name = propertyInfo.Name;
                value.IsNullable = true;
            }
            else
            {
                if (value.IsCollection)
                {
                    value.IsEntity = propertyInfo.PropertyType.FullName.Contains("Entities.Models");
                    value.Type = propertyInfo.PropertyType.FullName
                                                    .Replace("[[", "[").Split('[').LastOrDefault()
                                                    .Split(',').FirstOrDefault()
                                                    .Split('.').LastOrDefault();
                    //type.GetTypeInfo().ToString().Split('.').LastOrDefault();
                }
                else
                    value.Type = type.GetTypeInfo().ToString().Split('.').LastOrDefault();
                value.Name = propertyInfo.Name;
                value.IsNullable = false;
            }

            return value;
        }


        public string Name { get; set; }
        public bool HasProperties { get { return ObjectProperties.Count() > 0; } }
        public ICollection<PropertyDefinition> Properties { get { return ObjectProperties.Where(w => w.IsEntity == false && w.IsNullable == false).ToList(); } }
        public ICollection<PropertyDefinition> NullableProperties { get { return ObjectProperties.Where(w => w.IsEntity == false && w.IsNullable == true).ToList(); } }
        public ICollection<PropertyDefinition> EntityProperties { get { return ObjectProperties.Where(w => w.IsEntity == true).ToList(); } }
        public ICollection<PropertyDefinition> DerivedProperties { get; set; }
        private ICollection<PropertyDefinition> ObjectProperties { get; set; }
    }
}




