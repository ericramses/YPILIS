using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace YellowstonePathology.Business.Test.Model
{
    public class JsonTestFactory
    {
        public JsonTestFactory() { }

        public static Test FromJson(JObject jObject)
        {
            Test result = null;
            string testBase = jObject["testBase"].ToString();
            string jsonString = jObject.ToString();
            switch (testBase)
            {
                case Test.TestBase:
                    {
                        result = JsonConvert.DeserializeObject<Business.Test.Model.Test>(jsonString, new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.All,
                            ObjectCreationHandling = ObjectCreationHandling.Replace,
                        });
                        break;
                    }
                case Test.GradedBase:
                    {
                        result = JsonConvert.DeserializeObject<Business.Test.Model.GradedTest>(jsonString, new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.All,
                            ObjectCreationHandling = ObjectCreationHandling.Replace,
                        });
                        break;
                    }
                case Test.ImmunoHistochemistryBase:
                    {
                        result = JsonConvert.DeserializeObject<Business.Test.Model.ImmunoHistochemistryTest>(jsonString, new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.All,
                            ObjectCreationHandling = ObjectCreationHandling.Replace,
                        });
                        break;
                    }
                case Test.CytochemicalBase:
                    {
                        result = JsonConvert.DeserializeObject<Business.Test.Model.CytochemicalTest>(jsonString, new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.All,
                            ObjectCreationHandling = ObjectCreationHandling.Replace,
                        });
                        break;
                    }
                case Test.CytochemicalForMicroorganismsBase:
                    {
                        result = JsonConvert.DeserializeObject<Business.Test.Model.CytochemicalForMicroorganisms>(jsonString, new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.All,
                            ObjectCreationHandling = ObjectCreationHandling.Replace,
                        });
                        break;
                    }
                case Test.NoCptCodeBase:
                    {
                        result = JsonConvert.DeserializeObject<Business.Test.Model.NoCptCodeTest>(jsonString, new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.All,
                            ObjectCreationHandling = ObjectCreationHandling.Replace,
                        });
                        break;
                    }
                case Test.DualStainBase:
                    {
                        result = JsonConvert.DeserializeObject<Business.Test.Model.DualStain>(jsonString, new JsonSerializerSettings
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
