using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace YellowstonePathology.Business.Test.Model
{
    public class JsonStainFactory
    {
        public JsonStainFactory() { }

        public static Stain FromJson(JObject jObject)
        {
            Stain result = null;
            string stainType = jObject["stainType"].ToString();
            string jsonString = jObject.ToString();
            switch (stainType)
            {
                case Stain.SpecialStainBase:
                    {
                        result = JsonConvert.DeserializeObject<Stain>(jsonString, new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.All,
                            ObjectCreationHandling = ObjectCreationHandling.Replace,
                        });
                        break;
                    }
                case Stain.GradedBase:
                    {
                        result = JsonConvert.DeserializeObject<Stain>(jsonString, new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.All,
                            ObjectCreationHandling = ObjectCreationHandling.Replace,
                        });
                        break;
                    }
                case Stain.ImmunoHistochemistryBase:
                    {
                        result = JsonConvert.DeserializeObject<Stain>(jsonString, new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.All,
                            ObjectCreationHandling = ObjectCreationHandling.Replace,
                        });
                        break;
                    }
                case Stain.CytochemicalBase:
                    {
                        result = JsonConvert.DeserializeObject<Stain>(jsonString, new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.All,
                            ObjectCreationHandling = ObjectCreationHandling.Replace,
                        });
                        break;
                    }
                case Stain.CytochemicalForMicroorganismsBase:
                    {
                        result = JsonConvert.DeserializeObject<Stain>(jsonString, new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.All,
                            ObjectCreationHandling = ObjectCreationHandling.Replace,
                        });
                        break;
                    }
                case Stain.UnknownBase:
                    {
                        result = JsonConvert.DeserializeObject<Stain>(jsonString, new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.All,
                            ObjectCreationHandling = ObjectCreationHandling.Replace,
                        });
                        break;
                    }
                case Stain.DualStainBase:
                    {
                        result = JsonConvert.DeserializeObject<Stain>(jsonString, new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.All,
                            ObjectCreationHandling = ObjectCreationHandling.Replace,
                        });
                        break;
                    }
            }
            return result;
        }
    }
}
