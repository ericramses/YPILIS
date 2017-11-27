using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace YellowstonePathology.Business.Billing.Model
{
    public class CptCodeFactory
    {
        public CptCodeFactory() { }

        public static CptCode FromJson(JObject jObject)
        {
            CptCode result = new Model.CptCode();
            string cptType = jObject["cptType"].ToString();
            string jsonString = jObject.ToString();
            switch(cptType)
            {
                case CptCode.CptTypeNormal:
                    {
                         result = JsonConvert.DeserializeObject<Business.Billing.Model.CptCode>(jsonString, new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.All,
                            ObjectCreationHandling = ObjectCreationHandling.Replace,
                        });
                        break;
                    }
                case CptCode.CptTypePQRS:
                    {
                        result = JsonConvert.DeserializeObject<Business.Billing.Model.PQRSCode>(jsonString, new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.All,
                            ObjectCreationHandling = ObjectCreationHandling.Replace
                        });
                        break;
                    }
                case CptCode.CptTypeGCode:
                    {
                        result = JsonConvert.DeserializeObject<Business.Billing.Model.CptCode>(jsonString, new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.All,
                            ObjectCreationHandling = ObjectCreationHandling.Replace
                        });
                        break;
                    }
            }

            return result;
        }
    }
}
