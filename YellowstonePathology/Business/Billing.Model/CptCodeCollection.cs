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

        public static CptCode GetCptCode(string code)
        {
            string key = "cpt:" + code.ToLower();
            if (Business.RedisAppDataConnection.Instance.CptCodeDb.KeyExists(key) == false)
            {
                key = "pqrs:" + code.ToLower();
            }

            CptCode result = CptCodeCollection.GetCPTCodeById(key);
            return result;
        }

        public static CptCode GetCPTCodeById(string cptCodeId)
        {
            CptCode result = null;
            RedisResult redisResult = Business.RedisAppDataConnection.Instance.CptCodeDb.Execute("json.get", new object[] { cptCodeId, "." });
            JObject jObject = JsonConvert.DeserializeObject<JObject>((string)redisResult);
            result = CptCodeFactory.FromJson(jObject);

            return result;
        }

        public static CptCodeCollection GetCptCodes(FeeScheduleEnum feeSchedule)
        {
            CptCodeCollection result = CptCodeCollection.GetCptCodeCollection(feeSchedule);
            return result;
        }        

        public static CptCodeCollection GetCptCodeCollection(FeeScheduleEnum feeSchedule)
        {
            CptCodeCollection result = new CptCodeCollection();
            CptCodeCollection allCodes = CptCodeCollection.GetAll();
            foreach (CptCode cptCode in allCodes)
            {
                if (cptCode.FeeSchedule == feeSchedule)
                {
                    result.Add(cptCode);                    
                }
            }
            return result;
        }

        public static CptCode GetNewInstance(string cptCode, string modifier)
        {
            CptCode result = CptCode.Clone(CptCodeCollection.GetCptCode(cptCode));
            result.Modifier = modifier;
            return result;         
        }

        public static CptCode GetClone(string cptCodeId, string modifier)
        {
            CptCode result = CptCode.Clone(CptCodeCollection.GetCPTCodeById(cptCodeId));
            result.Modifier = modifier;
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

        public static CptCodeCollection GetAll()
        {
            YellowstonePathology.Business.Billing.Model.CptCodeCollection result = new Model.CptCodeCollection();                        
            IServer server = Business.RedisAppDataConnection.Instance.Server;

            RedisKey[] keyResult = server.Keys((int)Business.RedisDatabaseEnum.CptCodes, "*").ToArray<RedisKey>();
            foreach (RedisKey key in keyResult)
            {
                RedisResult redisResult = Business.RedisAppDataConnection.Instance.CptCodeDb.Execute("json.get", new object[] { key.ToString(), "." });
                JObject jObject = JsonConvert.DeserializeObject<JObject>((string)redisResult);
                CptCode code = CptCodeFactory.FromJson(jObject);
                result.Add(code);
            }
            
            return result;
        }
    }
}
