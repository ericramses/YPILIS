using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StackExchange.Redis;

namespace YellowstonePathology.Business.Billing.Model
{
    public class PQRSCodeCollection : ObservableCollection<PQRSCode>
    {
        public PQRSCodeCollection() { }

        public static PQRSCodeCollection GetAll(bool expandModifiers)
        {
            PQRSCodeCollection result = new PQRSCodeCollection();
            IServer server = Business.RedisAppDataConnection.Instance.Server;

            RedisKey[] keyResult = server.Keys(Business.RedisAppDataConnection.CPTCODEDBNUM, "pqrs:*").ToArray<RedisKey>();
            foreach (RedisKey key in keyResult)
            {
                RedisResult redisResult = Business.RedisAppDataConnection.Instance.CptCodeDb.Execute("json.get", new object[] { key.ToString(), "." });
                JObject jObject = JsonConvert.DeserializeObject<JObject>((string)redisResult);
                PQRSCode pqrsCode = PQRSCodeFactory.FromJson(jObject, null);
                result.Add(pqrsCode);
                if (expandModifiers == true)
                {
                    ExpandModifiers(jObject, result);
                }
            }
            
            /*result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("3125F", null));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("3125F", "1P"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("3125F", "8P"));

            result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("3126F", null));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("3126F", "1P"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("3126F", "8P"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("G8797", null));

            result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("3250F", null));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("3260", null));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("3260F", null));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("3260F", "1P"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("3260F", "8P"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("3267F", null));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("3267F", "1P"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("3267F", "8P"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("G8721", null));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("G8722", null));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("G8723", null));            
			result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("G8798", null));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("3394F", null));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("3394F", "8P"));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("3395F", null));

            result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("G9418", null));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("G9419", null));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("G9420", null));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("G9421", null));
            result.Add((PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("G9428", null));*/

            return result;
        }

        private static void ExpandModifiers(JObject jObject, PQRSCodeCollection pqrsCodeCollection)
        {
            foreach (JObject codeModifier in jObject["modifiers"])
            {
                string modifierString = codeModifier["modifier"].ToString();
                PQRSCode code = PQRSCodeFactory.FromJson(jObject, modifierString);
                pqrsCodeCollection.Add(code);
            }
        }

        public static PQRSCode GetPQRSCode(string code, string modifier)
        {
            PQRSCode result = null;
            string key = "pqrs:" + code;
            RedisResult redisResult = Business.RedisAppDataConnection.Instance.CptCodeDb.Execute("json.get", new object[] { key, "." });
            JObject jObject = JsonConvert.DeserializeObject<JObject>((string)redisResult);
            result = PQRSCodeFactory.FromJson(jObject, modifier);
            return result;
        }
    }
}
