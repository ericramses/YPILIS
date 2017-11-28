using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace YellowstonePathology.Business.Billing.Model
{
    public class CptCodeCollection : List<CptCode>
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
                            instance = FromJSON();                            
                    }
                }

                return instance;
            }
        }        

        public CptCodeCollection()
        {

        }

        public void WriteToRedis()
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

            for(int i=0; i<items.Length; i++)
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
            CptCodeCollection allCodes = GetAll();
            foreach (CptCode cptCode in allCodes)
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

        public static CptCodeCollection GetAll()
        {
            return Instance;            
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

        public string ToJSON()
        {
            string result = JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });

            return result;
        }

        public static CptCodeCollection FromJSON()
        {
            string jsonString = string.Empty;
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (StreamReader sr = new StreamReader(assembly.GetManifestResourceStream("YellowstonePathology.Business.Billing.Model.CPTCodeDefinition.json")))
            {
                jsonString = sr.ReadToEnd();
            }

            YellowstonePathology.Business.Billing.Model.CptCodeCollection result = JsonConvert.DeserializeObject<Business.Billing.Model.CptCodeCollection>(jsonString, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                ObjectCreationHandling = ObjectCreationHandling.Replace
            });

            return result;
        }
    }
}
