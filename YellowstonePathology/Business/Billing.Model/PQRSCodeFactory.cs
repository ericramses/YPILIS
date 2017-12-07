using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace YellowstonePathology.Business.Billing.Model
{
    public class PQRSCodeFactory
    {
        public static PQRSCode FromJson(JObject jObject)
        {
            string jsonString = jObject.ToString();
            PQRSCode result = JsonConvert.DeserializeObject<Business.Billing.Model.PQRSCode>(jsonString, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                ObjectCreationHandling = ObjectCreationHandling.Replace
            });

            return result;
        }
    }
}
