using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace YellowstonePathology.Business.Billing.Model
{
    public class ICDCodeFactory
    {
        public ICDCodeFactory() { }

        public static ICDCode FromJson(JObject jObject)
        {
            string jsonString = jObject.ToString();
            ICDCode result = JsonConvert.DeserializeObject<ICDCode>(jsonString, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                ObjectCreationHandling = ObjectCreationHandling.Replace,
            });

            return result;
        }
    }
}
