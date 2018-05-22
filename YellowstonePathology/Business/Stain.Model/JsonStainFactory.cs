using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace YellowstonePathology.Business.Stain.Model
{
    public class JsonStainFactory
    {
        public const string DualStainBase = "DualStain";
        public const string UnknownBase = "Unknown";
        public const string CytochemicalForMicroorganismsBase = "CytochemicalForMicroorganisms";
        public const string CytochemicalBase = "Cytochemical";
        public const string ImmunoHistochemistryBase = "IHC";
        public const string GradedBase = "GradedStain";
        public const string SpecialStainBase = "SpecialStain";

        public JsonStainFactory() { }

        public static Stain FromJson(JObject jObject)
        {
            Stain result = null;
            string stainType = jObject["stainType"].ToString();
            string jsonString = jObject.ToString();
            switch (stainType)
            {
                case SpecialStainBase:
                    {
                        result = JsonConvert.DeserializeObject<Stain>(jsonString, new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.All,
                            ObjectCreationHandling = ObjectCreationHandling.Replace,
                        });
                        break;
                    }
                case GradedBase:
                    {
                        result = JsonConvert.DeserializeObject<Stain>(jsonString, new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.All,
                            ObjectCreationHandling = ObjectCreationHandling.Replace,
                        });
                        break;
                    }
                case ImmunoHistochemistryBase:
                    {
                        result = JsonConvert.DeserializeObject<Stain>(jsonString, new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.All,
                            ObjectCreationHandling = ObjectCreationHandling.Replace,
                        });
                        break;
                    }
                case CytochemicalBase:
                    {
                        result = JsonConvert.DeserializeObject<Stain>(jsonString, new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.All,
                            ObjectCreationHandling = ObjectCreationHandling.Replace,
                        });
                        break;
                    }
                case CytochemicalForMicroorganismsBase:
                    {
                        result = JsonConvert.DeserializeObject<Stain>(jsonString, new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.All,
                            ObjectCreationHandling = ObjectCreationHandling.Replace,
                        });
                        break;
                    }
                case UnknownBase:
                    {
                        result = JsonConvert.DeserializeObject<Stain>(jsonString, new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.All,
                            ObjectCreationHandling = ObjectCreationHandling.Replace,
                        });
                        break;
                    }
                case DualStainBase:
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
