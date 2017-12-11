using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace YellowstonePathology.Business.Billing.Model
{
    public class CptCodeFactory
    {
        public CptCodeFactory() { }

        private static CptCodeModifier GetModifier(JObject jObject, string modifier)
        {
            CptCodeModifier result = null;
            foreach(JObject codeModifier in jObject["modifiers"])
            {
                if(codeModifier["modifier"].ToString() == modifier)
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

        public static CptCode FromJson(JObject jObject, string modifier)
        {
            CptCode result = FromJson(jObject);
            CptCodeModifier cptCodeModifier = null;
            if (string.IsNullOrEmpty(modifier) == false)
            {
                cptCodeModifier = GetModifier(jObject, modifier);
                if(cptCodeModifier == null)
                {
                    throw new Exception("trying to get Cpt Code " + jObject["code"].ToString() + " with modifier " + modifier + " not available for the code.");
                }
            }

            string jsonString = jObject.ToString();
            result = JsonConvert.DeserializeObject<Business.Billing.Model.CptCode>(jsonString, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All,
                    ObjectCreationHandling = ObjectCreationHandling.Replace,
                });

            result.Modifier = cptCodeModifier;

            return result;
        }

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
