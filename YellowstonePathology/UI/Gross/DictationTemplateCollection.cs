using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;

namespace YellowstonePathology.UI.Gross
{
    public class DictationTemplateCollection : ObservableCollection<DictationTemplate>
    {
        private static DictationTemplateCollection instance;

        public DictationTemplateCollection() { }

        public static DictationTemplateCollection Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = BuildFromRedis();
                }
                return instance;
            }
        }

        public DictationTemplate GetClone(string specimenId)
        {
            DictationTemplate notFound = DictationTemplateCollection.Instance.FirstOrDefault(t => t.TemplateName == "Template Not Found.");
            YellowstonePathology.Business.Persistence.ObjectCloner objectCloner = new YellowstonePathology.Business.Persistence.ObjectCloner();
            DictationTemplate result = (DictationTemplate)objectCloner.Clone(notFound);

            foreach (DictationTemplate dictationTemplate in this)
            {
                if(dictationTemplate.SpecimenCollection.Exists(specimenId) == true)
                {
                    result = (DictationTemplate)objectCloner.Clone(dictationTemplate);
                    break;
                }
            }
            return result;
        }

        public string ToJSON()
        {
            var camelCaseFormatter = new JsonSerializerSettings();
            camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();
            string result = JsonConvert.SerializeObject(this, Formatting.Indented, camelCaseFormatter);
            return result;
        }

        public static DictationTemplateCollection BuildFromRedis()
        {
            DictationTemplateCollection result = new DictationTemplateCollection();
            Store.RedisDB dictationTemplateDb = Store.AppDataStore.Instance.RedisStore.GetDB(Store.AppDBNameEnum.DictationTemplate);
            foreach (string jString in dictationTemplateDb.GetAllJSONKeys())
            {
                DictationTemplate dictationTemplate = JsonConvert.DeserializeObject<DictationTemplate>(jString, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All,
                    ObjectCreationHandling = ObjectCreationHandling.Replace,
                });

                JObject jObject = JsonConvert.DeserializeObject<JObject>(jString);
                foreach(string id in jObject["specimenIds"])
                {
                    Business.Specimen.Model.Specimen specimen = Business.Specimen.Model.SpecimenCollection.Instance.GetSpecimen(id);
                    dictationTemplate.SpecimenCollection.Add(specimen);
                }

                result.Add(dictationTemplate);
            }
            return result;
        }
    }
}
