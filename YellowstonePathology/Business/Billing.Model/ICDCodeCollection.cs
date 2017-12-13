using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StackExchange.Redis;

namespace YellowstonePathology.Business.Billing.Model
{
    public class  ICDCodeCollection : ObservableCollection<ICDCode>
    {
        private static volatile ICDCodeCollection instance;
        private static object syncRoot = new Object();

        public static ICDCodeCollection Instance
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

        public ICDCodeCollection()
        {

        }

        public ICDCode GetICDCode(string code)
        {
            ICDCode result = null;
            foreach (ICDCode icdCode in ICDCodeCollection.Instance)
            {
                if (icdCode.Code.ToUpper() == code.ToUpper())
                {
                    result = icdCode;
                    break;
                }
            }
            return result;
        }

        public ICDCode GetICDCodeById(string icdCodeId)
        {
            ICDCode result = null;
            foreach (ICDCode icdCode in ICDCodeCollection.Instance)
            {
                if (icdCode.ICDCodeId.ToUpper() == icdCodeId.ToUpper())
                {
                    result = icdCode;
                    break;
                }
            }
            return result;
        }

        public ICDCode GetClone(string icdCodeId)
        {
            ICDCode result = ICDCode.Clone(this.GetICDCodeById(icdCodeId));
            return result;
        }

        public static ICDCodeCollection GetSortedByCode(ICDCodeCollection icdCodeList)
        {
            ICDCodeCollection result = new ICDCodeCollection();
            IOrderedEnumerable<ICDCode> orderedResult = icdCodeList.OrderBy(i => i.Code);
            foreach (ICDCode icdCode in orderedResult)
            {
                result.Add(icdCode);
            }
            return result;
        }

        public static ICDCodeCollection GetSortedByDescription(ICDCodeCollection icdCodeList)
        {
            ICDCodeCollection result = new ICDCodeCollection();
            IOrderedEnumerable<ICDCode> orderedResult = icdCodeList.OrderBy(i => i.Description);
            foreach (ICDCode icdCode in orderedResult)
            {
                result.Add(icdCode);
            }
            return result;
        }

        public static ICDCodeCollection GetByCategory(string category)
        {
            ICDCodeCollection result = new ICDCodeCollection();
            foreach (ICDCode icdCode in ICDCodeCollection.Instance)
            {
                if (icdCode.Category == category)
                {
                    result.Add(icdCode);
                }
            }
            return result;
        }

        public static ICDCodeCollection GetBillingCodeList()
        {
            ICDCodeCollection collection = ICDCodeCollection.GetByCategory("Cytology");
            List<ICDCode> list = collection.ToList<ICDCode>();
            collection = ICDCodeCollection.GetByCategory("NGCT");
            list.AddRange(collection);
            collection = ICDCodeCollection.GetByCategory("Routine HPV");
            list.AddRange(collection);
            collection = ICDCodeCollection.GetByCategory("Trichomonas/Cervx");
            list.AddRange(collection);
            collection = ICDCodeCollection.GetByCategory("Trichomonas");
            list.AddRange(collection);
            collection = ICDCodeCollection.GetByCategory(string.Empty);
            list.AddRange(collection);

            ICDCodeCollection result = new Model.ICDCodeCollection();
            foreach(ICDCode code in list)
            {
                result.Add(code);
            }

            result = ICDCodeCollection.GetSortedByCode(result);

            return result;
        }

        public static ICDCodeCollection GetFlowCodeList()
        {
            ICDCodeCollection result = ICDCodeCollection.GetSortedByDescription(ICDCodeCollection.GetByCategory("Flow"));
            return result;
        }

        private static ICDCodeCollection FromRedis()
        {
            ICDCodeCollection result = new ICDCodeCollection();                        
            IServer server = RedisAppDataConnection.Instance.Server;

            RedisKey[] keyResult = server.Keys(Business.RedisAppDataConnection.ICDCODEDBNUM, "*").ToArray<RedisKey>();
            foreach (RedisKey key in keyResult)
            {
                RedisResult redisResult = RedisAppDataConnection.Instance.GetDB(RedisAppDataConnection.ICDCODEDBNUM).Execute("json.get", new object[] { key.ToString(), "." });
                JObject jObject = JsonConvert.DeserializeObject<JObject>((string)redisResult);
                ICDCode code = ICDCodeFactory.FromJson(jObject);
                result.Add(code);
            }

            return result;
        }
    }
}
