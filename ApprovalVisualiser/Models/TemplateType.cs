﻿using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApprovalVisualiser.Models
{

    public enum TemplateType
        {
            DTOClass,
            IRepoClass,
            RepositoryClass,
            IServiceClass,
            ServiceClass,
            NestedDTORef,
            NestedDTOCollectionRef,
            PropertyToModel,
            UpdateModel,
            InitializeCollection,
            InitializeEntities,
            Namespace,
            Class,
            Constructor,
            Prop,
            PropFull,
            Function,
            GetAllBasicField,
            AddNestedEntity,
            AddNestedEntityCollection,


    }


        public abstract class TemplatedCodeGenBase<TEntity> where TEntity : new()
        {
            private List<KeyValues> teplateKeyValues = new List<KeyValues>();

            protected EntityAnalyzer<TEntity> entityAnal = new EntityAnalyzer<TEntity>();
            protected Templates tpls = new Templates();
            public TemplatedCodeGenBase()
            {
                entityAnal.Analyze();
            }
            public abstract string Generate();

            protected string ParsePropertiesTemplates(ICollection<PropertyDefinition> list, TemplateType templateType)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var item in list)
                {
                    List<KeyValues> nsValues = new List<KeyValues>
                {
                    new KeyValues { Key = "typeValue", Value = item.Type },
                    new KeyValues { Key = "propertyNameValue", Value = item.Name }
                };
                    sb.AppendLine(tpls.ApplyTemplate(templateType, nsValues));
                }
                return sb.ToString();
            }

            public void AddKeyValues(string key, string value)
            {
                teplateKeyValues.Add(new KeyValues { Key = key, Value = value });
            }

            public string ParseTemplate(TemplateType tt)
            {
                return tpls.ApplyTemplate(tt, teplateKeyValues);

            }

            public string ParseListKey(List<ParseListType> pair)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var item in pair)
                {
                    sb.AppendLine(ParsePropertiesTemplates(item.List, item.TemplateType));
                }

                return sb.ToString();
            }
        }

        public class ParseListType
        {
            public ICollection<PropertyDefinition> List { get; set; }
            public TemplateType TemplateType { get; set; }
        }


        public class TemplatedCodeGen<T> where T : new()
        {
            EntityAnalyzer<T> entityAnal = new EntityAnalyzer<T>();
            Templates tpls = new Templates();

            public TemplatedCodeGen()
            {
                entityAnal.Analyze();
            }


            private string ParsePropertiesTemplates(ICollection<PropertyDefinition> list, TemplateType templateType)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var item in list)
                {
                    List<KeyValues> nsValues = new List<KeyValues>
                {
                    new KeyValues { Key = "typeValue", Value = item.Type },
                    new KeyValues { Key = "propertyNameValue", Value = item.Name }
                };
                    sb.AppendLine(tpls.ApplyTemplate(templateType, nsValues));
                }
                return sb.ToString();
            }
            /// <summary>
            /// 
            /// </summary>
            /// <returns></returns>
            public string Generate()
            {
                string result = "";

                StringBuilder sb = new StringBuilder();
                sb.AppendLine(ParsePropertiesTemplates(entityAnal.Properties, TemplateType.Prop));
                sb.AppendLine(ParsePropertiesTemplates(entityAnal.NullableProperties, TemplateType.Prop));
                sb.AppendLine(ParsePropertiesTemplates(entityAnal.EntityProperties.Where(w => w.IsCollection == false).ToList(), TemplateType.NestedDTORef));
                sb.AppendLine(ParsePropertiesTemplates(entityAnal.EntityProperties.Where(w => w.IsCollection == true).ToList(), TemplateType.NestedDTOCollectionRef));

                StringBuilder propertiesToModelList = new StringBuilder();
                //propertiesToModelList.AppendLine(ParsePropertiesTemplates(entityAnal.DerivedProperties.Where(w => w.IsEntity == false).ToList(), TemplateType.PropertyToModel));
                propertiesToModelList.AppendLine(ParsePropertiesTemplates(entityAnal.Properties, TemplateType.PropertyToModel));
                propertiesToModelList.AppendLine(ParsePropertiesTemplates(entityAnal.NullableProperties, TemplateType.PropertyToModel));
                //            propertiesToModelList.AppendLine(ParsePropertiesTemplates(entityAnal.EntityProperties.Where(w => w.IsCollection == false).ToList(), TemplateType.PropertyToModel));
                //            propertiesToModelList.AppendLine(ParsePropertiesTemplates(entityAnal.EntityProperties.Where(w => w.IsCollection == true).ToList(), TemplateType.PropertyToModel));

                StringBuilder updateModelList = new StringBuilder();
                updateModelList.AppendLine(ParsePropertiesTemplates(entityAnal.DerivedProperties.Where(w => w.IsEntity == false).ToList(), TemplateType.UpdateModel));
                updateModelList.AppendLine(ParsePropertiesTemplates(entityAnal.Properties, TemplateType.UpdateModel));
                updateModelList.AppendLine(ParsePropertiesTemplates(entityAnal.NullableProperties, TemplateType.UpdateModel));

                StringBuilder initializeCollectionList = new StringBuilder();
                initializeCollectionList.AppendLine(ParsePropertiesTemplates(entityAnal.EntityProperties.Where(w => w.IsCollection == true).ToList(), TemplateType.InitializeCollection));

                List<KeyValues> nsValues = new List<KeyValues>
            {
                new KeyValues{ Key = "namespaceValue", Value = "ProManager.Data.Models" },
                new KeyValues{ Key = "entityNameValue", Value = entityAnal.Name },
                new KeyValues{ Key = "propertyTemplateList", Value = sb.ToString()},
                new KeyValues{ Key = "propertyToModelList", Value = propertiesToModelList.ToString()},
                new KeyValues{ Key = "updateModelList", Value = updateModelList.ToString()},
                new KeyValues{ Key = "initializeCollectionList", Value = initializeCollectionList.ToString()},
                new KeyValues{ Key = "initializeEntitiesList", Value = ParsePropertiesTemplates(entityAnal.EntityProperties.Where(w => w.IsCollection == false).ToList(), TemplateType.InitializeEntities)}
            };

                result = tpls.ApplyTemplate(TemplateType.DTOClass, nsValues);
                return result;
            }
        }
}




