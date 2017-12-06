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
            bool hasHeader = jObject["header"] != null;
            string jsonString = jObject.ToString();
            if (hasHeader == true)
            {
                result = JsonConvert.DeserializeObject<Business.Billing.Model.PQRSCode>(jsonString, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All,
                    ObjectCreationHandling = ObjectCreationHandling.Replace
                });
            }
            else
            {
                    result = JsonConvert.DeserializeObject<Business.Billing.Model.CptCode>(jsonString, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All,
                    ObjectCreationHandling = ObjectCreationHandling.Replace,
                });
            }

            return result;
        }
    }
}
