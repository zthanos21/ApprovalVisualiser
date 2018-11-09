using System.Collections.Generic;
using System.Text;

namespace ApprovalVisualiser.Models
{
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
}




