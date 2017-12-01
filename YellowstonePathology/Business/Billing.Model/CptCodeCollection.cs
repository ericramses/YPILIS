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
        private static volatile CptCodeCollection instance;
        private static object syncRoot = new Object();

        public static CptCodeCollection Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = FromRedis();
                    }
                }

                return instance;
            }
        }        

        public CptCodeCollection()
        {
            Business.RedisSingleton redis = Business.RedisSingleton.Instance;
        }        

        public bool IsMedicareCode(string cptCode)
        {
            bool result = false;
            return result;
        }

        public CptCode GetCptCode(string code)
        {
            CptCode result = null;
            foreach (CptCode cptCode in this)
            {
                if (cptCode.Code.ToUpper() == code.ToUpper())
                {
                    result = cptCode;
                }
            }
            return result;
        }

        public CptCode GetCPTCodeById(string cptCodeId)
        {
            CptCode result = null;
            foreach (CptCode cptCode in this)
            {
                if (cptCode.CPTCodeId.ToUpper() == cptCodeId.ToUpper())
                {
                    result = cptCode;
                }
            }
            return result;
        }

        public CptCodeCollection GetCptCodes(FeeScheduleEnum feeSchedule)
        {
            CptCodeCollection result = new CptCodeCollection();
            foreach (CptCode cptCode in this)
            {
                if (cptCode.FeeSchedule == feeSchedule)
                {
                    result.Add(cptCode);
                }
            }
            return result;
        }        

        public static CptCodeCollection GetCptCodeCollection(FeeScheduleEnum feeSchedule)
        {
            CptCodeCollection result = new CptCodeCollection();
            foreach (CptCode cptCode in CptCodeCollection.Instance)
            {
                if (cptCode.FeeSchedule == feeSchedule)
                {
                    result.Add(cptCode);                    
                }
            }
            return result;
        }

        public CptCode GetNewInstance(string cptCode, string modifier)
        {
            CptCode result = CptCode.Clone(this.GetCptCode(cptCode));
            result.Modifier = modifier;
            return result;         
        }

        public CptCode GetClone(string cptCodeId, string modifier)
        {
            CptCode result = CptCode.Clone(this.GetCPTCodeById(cptCodeId));
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

        public static CptCodeCollection FromRedis()
        {
            YellowstonePathology.Business.Billing.Model.CptCodeCollection result = new Model.CptCodeCollection();                        
            IServer server = Business.RedisAppDataConnection.Instance.Server;

            RedisKey[] keyResult = server.Keys((int)Business.RedisDatabaseEnum.Default,"cpt:*").ToArray<RedisKey>();
            foreach (RedisKey key in keyResult)
            {
                RedisResult redisResult = Business.RedisAppDataConnection.Instance.Db.Execute("json.get", new object[] { key.ToString(), "." });
                JObject jObject = JsonConvert.DeserializeObject<JObject>((string)redisResult);
                CptCode code = CptCodeFactory.FromJson(jObject);
                result.Add(code);
            }

            keyResult = server.Keys(0, "pqrs:*").ToArray<RedisKey>();
            foreach (RedisKey key in keyResult)
            {
                RedisResult redisResult = RedisAppDataConnection.Instance.Db.Execute("json.get", new object[] { key.ToString(), "." });
                JObject jObject = JsonConvert.DeserializeObject<JObject>((string)redisResult);
                CptCode code = CptCodeFactory.FromJson(jObject);
                result.Add(code);
            }
            
            return result;
        }
    }
}
