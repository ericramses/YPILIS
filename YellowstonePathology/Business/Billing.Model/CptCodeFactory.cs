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
            string jsonString = jObject.ToString();
            CptCode result = JsonConvert.DeserializeObject<Business.Billing.Model.CptCode>(jsonString, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All,
                    ObjectCreationHandling = ObjectCreationHandling.Replace,
                });

            return result;
        }
    }
}
