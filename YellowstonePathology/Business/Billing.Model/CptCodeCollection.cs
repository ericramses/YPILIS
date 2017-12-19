using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StackExchange.Redis;

namespace YellowstonePathology.Business.Billing.Model
{
    public class CptCodeCollection : ObservableCollection<CptCode>
    {
        public CptCodeCollection()
        {
        }        

        public bool IsMedicareCode(string cptCode)
        {
            bool result = false;
            return result;
        }

        /*public static CptCode Get(string code, string modifier)
        {
            CptCode result = null;
            RedisResult redisResult = Business.RedisAppDataConnection.Instance.CptCodeDb.Execute("json.get", new object[] { code, "." });
            JObject jObject = JsonConvert.DeserializeObject<JObject>((string)redisResult);
            result = CptCodeFactory.FromJson(jObject);
            result.Modifier = modifier;
            return result;
        }*/

        public static CptCode GetCPTCode(string code, string modifier)
        {
            CptCode result = null;
            RedisResult redisResult = Business.RedisAppDataConnection.Instance.GetDB(RedisAppDataConnection.CPTCODEDBNUM).Execute("json.get", new object[] { code, "." });
            JObject jObject = JsonConvert.DeserializeObject<JObject>((string)redisResult);
            result = CptCodeFactory.FromJson(jObject, modifier);
            return result;
        }       

        public static CptCodeCollection GetCptCodeCollection(FeeScheduleEnum feeSchedule)
        {
            CptCodeCollection result = new CptCodeCollection();
            CptCodeCollection allCodes = CptCodeCollection.GetAll(true, true);
            foreach (CptCode cptCode in allCodes)
            {
                if (cptCode.FeeSchedule == feeSchedule)
                {
                    result.Add(cptCode);                    
                }
            }
            return result;
        }

        public static CptCodeCollection GetSorted(CptCodeCollection cptCodeCollection)
        {
            CptCodeCollection result = new CptCodeCollection();
            IOrderedEnumerable<CptCode> orderedResult = cptCodeCollection.OrderBy(i => i.Code);
            foreach (CptCode cptCode in orderedResult)
            {
                result.Add(cptCode);
            }
            return result;
        }

        public static CptCodeCollection GetAll(bool includePqrs, bool expandModifiers)
        {
            YellowstonePathology.Business.Billing.Model.CptCodeCollection result = new Model.CptCodeCollection();                        
            IServer server = Business.RedisAppDataConnection.Instance.Server;

            RedisKey[] keyResult = server.Keys(Business.RedisAppDataConnection.CPTCODEDBNUM, "*").ToArray<RedisKey>();
            foreach (RedisKey key in keyResult)
            {
                RedisResult redisResult = Business.RedisAppDataConnection.Instance.GetDB(RedisAppDataConnection.CPTCODEDBNUM).Execute("json.get", new object[] { key.ToString(), "." });
                JObject jObject = JsonConvert.DeserializeObject<JObject>((string)redisResult);
                CptCode code = CptCodeFactory.FromJson(jObject, null);
                result.Add(code);

                if (expandModifiers == true)
                {
                    ExpandModifiers(jObject, result);
                }
            }

            if (includePqrs == true)
            {
                PQRSCodeCollection pqrsCodeCollection = PQRSCodeCollection.GetAll(expandModifiers);
                foreach (PQRSCode pqrsCode in pqrsCodeCollection)
                {
                    result.Add(pqrsCode);
                }
            }
            
            return result;
        }

        private static void ExpandModifiers(JObject jObject, CptCodeCollection cptCodeCollection)
        {
            foreach (JObject codeModifier in jObject["modifiers"])
            {
                string modifierString = codeModifier["modifier"].ToString();
                CptCode code = CptCodeFactory.FromJson(jObject, modifierString);
                cptCodeCollection.Add(code);
            }
        }        
    }
}
