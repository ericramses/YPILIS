using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
                            instance = FromRedis();
                    }
                }

                return instance;
            }
        }        

        public CptCodeCollection()
        {

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
            IDatabase db = Business.RedisConnection2.Instance.GetDatabase();

            RedisResult redisResult = db.Execute("json.get", new object[] { "cptcodes" });
            /*if (redisResult.IsNull == true)
            {
                string jsonString = string.Empty;
                Assembly assembly = Assembly.GetExecutingAssembly();
                using (StreamReader sr = new StreamReader(assembly.GetManifestResourceStream("YellowstonePathology.Business.Billing.Model.CPTCodeDefinition.json")))
                {
                    jsonString = sr.ReadToEnd();
                }
                db.Execute("json.set", new object[] { "cptcodes", ".", jsonString });
                redisResult = db.Execute("json.get", new object[] { "cptcodes" });
            }*/

            JArray jsonCptCodes = JArray.Parse((string)redisResult);
            foreach (JObject jObject in jsonCptCodes.Children<JObject>())
            {
                CptCode code = CptCodeFactory.FromJson(jObject);
                result.Add(code);
            }

            return result;
        }
    }
}
