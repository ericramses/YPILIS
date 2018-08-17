using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using StackExchange.Redis;

namespace YellowstonePathology.Business.Specimen.Model
{
    public class SpecimenCollection : ObservableCollection<Specimen>
    {
        private static SpecimenCollection instance;

        public SpecimenCollection() { }

        public static SpecimenCollection Instance
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

        public bool Exists(string specimenId)
        {
            bool result = false;
            foreach (Specimen specimen in this)
            {
                if (specimen.SpecimenId == specimenId)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public Specimen GetSpecimen(string specimenId)
        {
            Specimen result = null;
            foreach (Specimen specimen in this)
            {
                if (specimen.SpecimenId == specimenId)
                {
                    result = specimen;
                    break;
                }
            }
            return result;
        }

        private static SpecimenCollection Sort(SpecimenCollection specimenCollection)
        {
            SpecimenCollection result = new SpecimenCollection();
            IOrderedEnumerable<Specimen> orderedResult = specimenCollection.OrderBy(i => i.SpecimenName);
            foreach (Specimen specimen in orderedResult)
            {
                result.Add(specimen);
            }
            return result;
        }

        public static SpecimenCollection GetSkins()
        {
            SpecimenCollection result = new SpecimenCollection();
            result.Add(SpecimenCollection.Instance.GetSpecimen("SKEXOSPCMN")); // new SpecimenDefinition.SkinExcisionOrientedBiopsy());
            result.Add(SpecimenCollection.Instance.GetSpecimen("SKEXUOSPCMN")); // new SpecimenDefinition.SkinExcisionUnorientedBiopsy());
            result.Add(SpecimenCollection.Instance.GetSpecimen("SKEXOCSPCMN")); // new SpecimenDefinition.SkinExcisionOrientedwithCurettingsBiopsy());
            result.Add(SpecimenCollection.Instance.GetSpecimen("SKEXUOCSPCMN")); // new SpecimenDefinition.SkinExcisionUnorientedwithCurettingsBiopsy());
            result.Add(SpecimenCollection.Instance.GetSpecimen("SKSHPHMSSPCMN")); // new SpecimenDefinition.SkinShavePunchMiscBiopsy());
            result.Add(SpecimenCollection.Instance.GetSpecimen("SKSHCSPCMN")); // new SpecimenDefinition.SkinShavewithCurettingsBiopsy());            
            return Sort(result);
        }

        public string ToJSON()
        {
            var camelCaseFormatter = new JsonSerializerSettings();
            camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();
            string result = JsonConvert.SerializeObject(this, Formatting.Indented, camelCaseFormatter);
            return result;
        }

        public static SpecimenCollection BuildFromRedis()
        {
            SpecimenCollection result = new SpecimenCollection();
            Store.RedisDB specimenDb = Store.AppDataStore.Instance.RedisStore.GetDB(Store.AppDBNameEnum.Specimen);
            foreach (string jString in specimenDb.GetAllJSONKeys())
            {
                Specimen specimen = JsonConvert.DeserializeObject<Specimen>(jString, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All,
                    ObjectCreationHandling = ObjectCreationHandling.Replace,
                });

                JObject jObject = JsonConvert.DeserializeObject<JObject>(jString);
                string code = jObject["cptCodeId"].ToString();
                if (string.IsNullOrEmpty(code) == false)
                {
                    specimen.CPTCode = Store.AppDataStore.Instance.CPTCodeCollection.GetClone(code, null);
                }                

                /*string id = jObject["specimenId"].ToString();
                if(id == "NLLSPCMN")
                {
                    specimen.SpecimenId = null;
                }*/

                result.Add(specimen);
            }
            return Sort(result);
        }
    }
}
