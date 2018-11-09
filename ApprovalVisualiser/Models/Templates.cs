using System.Collections.Generic;

namespace ApprovalVisualiser.Models
{
    public class Templates
    {
        const string FILE_PATH = @"C:\Users\sales\Documents\LINQPad Queries\templateFiles\";

        public Templates()
        {
            TemplateList = new List<TemplateClass>();
            InitializeTemplates();
        }
        private void InitializeTemplates()
        {
            this.TemplateList.Add(new TemplateClass
            {
                TemplateType = TemplateType.DTOClass,
                Template = System.IO.File.ReadAllText(FILE_PATH + "DTOClass.tpl")
            });

            this.TemplateList.Add(new TemplateClass
            {
                TemplateType = TemplateType.IRepoClass,
                Template = System.IO.File.ReadAllText(FILE_PATH + "IRepoClass.tpl")
            });


            this.TemplateList.Add(new TemplateClass
            {
                TemplateType = TemplateType.IServiceClass,
                Template = System.IO.File.ReadAllText(FILE_PATH + "IServiceClass.tpl")
            });

            this.TemplateList.Add(new TemplateClass
            {
                TemplateType = TemplateType.ServiceClass,
                Template = System.IO.File.ReadAllText(FILE_PATH + "ServiceClass.tpl")
            });

            this.TemplateList.Add(new TemplateClass
            {
                TemplateType = TemplateType.RepositoryClass,
                Template = System.IO.File.ReadAllText(FILE_PATH + "RepositoryClass.tpl")
            });





            this.TemplateList.Add(new TemplateClass
            {
                TemplateType = TemplateType.Prop,
                Template = "\tpublic %typeValue% %propertyNameValue% { get; set; }"
            });


            this.TemplateList.Add(new TemplateClass
            {
                TemplateType = TemplateType.NestedDTORef,
                Template = "\tpublic %typeValue%DTO %propertyNameValue% { get; set; }"
            });

            this.TemplateList.Add(new TemplateClass
            {
                TemplateType = TemplateType.NestedDTOCollectionRef,
                Template = "\tpublic  ICollection<%typeValue%DTO> %propertyNameValue% { get; set; }"
            });

            this.TemplateList.Add(new TemplateClass
            {
                TemplateType = TemplateType.PropertyToModel,
                Template = "\t%propertyNameValue% = this.%propertyNameValue%,"
            });

            this.TemplateList.Add(new TemplateClass
            {
                TemplateType = TemplateType.UpdateModel,
                Template = "\tmodel.%propertyNameValue% = %propertyNameValue%;"
            });

            this.TemplateList.Add(new TemplateClass
            {
                TemplateType = TemplateType.InitializeCollection,
                Template = "\t%propertyNameValue% = new HashSet<%typeValue%DTO>();"
            });

            this.TemplateList.Add(new TemplateClass
            {
                TemplateType = TemplateType.InitializeEntities,
                Template = "\t%propertyNameValue% = new %typeValue%DTO();"
            });

            this.TemplateList.Add(new TemplateClass
            {
                TemplateType = TemplateType.GetAllBasicField,
                Template = "\t%propertyNameValue% = q.%propertyNameValue%,"
            });

            this.TemplateList.Add(new TemplateClass
            {
                TemplateType = TemplateType.AddNestedEntity,
                Template = System.IO.File.ReadAllText(FILE_PATH + "repositoryAddNested.tpl")
            });
            this.TemplateList.Add(new TemplateClass
            {
                TemplateType = TemplateType.AddNestedEntityCollection,
                Template = System.IO.File.ReadAllText(FILE_PATH + "repositoryAddNestedCollection.tpl")
            });


        }

        private ICollection<TemplateClass> TemplateList { get; set; }


        private string PrepareKey(string key)
        {
            return string.Format("%{0}%", key);
        }

        public string ApplyTemplate(TemplateType templateType, ICollection<KeyValues> keyValues)
        {
            var result = TemplateList.FirstOrDefault(w => w.TemplateType == templateType).Template;
            if (!string.IsNullOrEmpty(result))
            {
                foreach (var item in keyValues)
                {
                    result = result.Replace(PrepareKey(item.Key), item.Value);
                }

            }
            return result;
        }
    }
}
