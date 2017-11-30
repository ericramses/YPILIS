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

        }

        /*public void WriteToRedis()
        {
            Business.RedisLocksConnection redis = new RedisLocksConnection();
            IDatabase db = redis.GetDatabase();
            db.KeyDelete("cptcodes");

            foreach (CptCode cptCode in this)
            {
                db.KeyDelete("cptcode:" + cptCode.Code);

                string result = JsonConvert.SerializeObject(cptCode, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                });

                db.ListRightPush("cptcodes", "cptcode:" + cptCode.Code);
                db.StringSet("cptcode:" + cptCode.Code, result);
            }
        }

        public static CptCodeCollection BuildFromRedis()
        {
            CptCodeCollection result = new CptCodeCollection();
            Business.RedisLocksConnection redis = new RedisLocksConnection();
            IDatabase db = redis.GetDatabase();
            RedisValue[] items = db.ListRange("cptcodes", 0, -1);

            for (int i = 0; i < items.Length; i++)
            {
                RedisValue json = db.StringGet(items[i].ToString());
                YellowstonePathology.Business.Billing.Model.CptCode cptCode = JsonConvert.DeserializeObject<Business.Billing.Model.CptCode>(json, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All,
                    ObjectCreationHandling = ObjectCreationHandling.Replace
                });

                result.Add(cptCode);
            }

            return result;
        }*/

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
            Business.RedisAppDataConnection redis = new RedisAppDataConnection("default");            
            IServer server = redis.Server;

            RedisKey[] keyResult = server.Keys(0,"cpt:*").ToArray<RedisKey>();
            foreach (RedisKey key in keyResult)
            {
                RedisResult redisResult = redis.Db.Execute("json.get", new object[] { key.ToString(), "." });
                JObject jObject = JsonConvert.DeserializeObject<JObject>((string)redisResult);
                CptCode code = CptCodeFactory.FromJson(jObject);
                result.Add(code);
            }

            keyResult = server.Keys(0, "pqrs:*").ToArray<RedisKey>();
            foreach (RedisKey key in keyResult)
            {
                RedisResult redisResult = redis.Db.Execute("json.get", new object[] { key.ToString(), "." });
                JObject jObject = JsonConvert.DeserializeObject<JObject>((string)redisResult);
                CptCode code = CptCodeFactory.FromJson(jObject);
                result.Add(code);
            }

            return result;
        }
    }
}
