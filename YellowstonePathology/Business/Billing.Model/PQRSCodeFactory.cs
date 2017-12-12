using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace YellowstonePathology.Business.Billing.Model
{
    public class PQRSCodeFactory
    {
        private static PQRSCode FromJson(JObject jObject)
        {
            string jsonString = jObject.ToString();
            PQRSCode result = JsonConvert.DeserializeObject<Business.Billing.Model.PQRSCode>(jsonString, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                ObjectCreationHandling = ObjectCreationHandling.Replace
            });

            return result;
        }


        private static CptCodeModifier GetModifier(JObject jObject, string modifier)
        {
            CptCodeModifier result = null;
            foreach (JObject codeModifier in jObject["modifiers"])
            {
                if (codeModifier["modifier"].ToString() == modifier)
                {
                    string modifierString = codeModifier.ToString();
                    result = JsonConvert.DeserializeObject<Business.Billing.Model.CptCodeModifier>(modifierString, new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All,
                        ObjectCreationHandling = ObjectCreationHandling.Replace,
                    });
                    break;
                }
            }
            return result;
        }

        public static PQRSCode FromJson(JObject jObject, string modifier)
        {
            PQRSCode result = FromJson(jObject);
            CptCodeModifier cptCodeModifier = null;
            if (string.IsNullOrEmpty(modifier) == false)
            {
                cptCodeModifier = GetModifier(jObject, modifier);
                if (cptCodeModifier == null)
                {
                    throw new Exception("trying to get PQRS Code " + jObject["code"].ToString() + " with modifier " + modifier + " not available for the code.");
                }
            }

            result.ReportingDefinition = result.Description;

            if (cptCodeModifier != null)
            {
                result.Modifier = cptCodeModifier;
            }

            return result;
        }
    }
}
