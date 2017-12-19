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
        private static CptCodeCollection instance = null;
        private static readonly object padlock = new object();

        public CptCodeCollection()
        {
        }

        public static CptCodeCollection Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = GetAll();
                    }
                    return instance;
                }
            }
        }

        public bool IsMedicareCode(string cptCode)
        {
            bool result = false;
            return result;
        }

        public CptCode Get(string code, string modifier)
        {
            CptCode result = null;
            foreach (CptCode cptCode in Instance)
            {
                if (string.IsNullOrEmpty(modifier) == true)
                {
                    if (cptCode.Code == code && cptCode.Modifier == null)
                    {
                        result = cptCode;
                        break;
                    }
                }
                else if(cptCode.Code == code && cptCode.Modifier != null && cptCode.Modifier.Modifier == modifier)
                {
                    result = cptCode;
                    break;
                }
            }
            return result;
        }       

        public CptCodeCollection GetCptCodeCollection(FeeScheduleEnum feeSchedule)
        {
            CptCodeCollection result = new CptCodeCollection();
            foreach (CptCode cptCode in Instance)
            {
                if (cptCode.FeeSchedule == feeSchedule)
                {
                    result.Add(cptCode);                    
                }
            }
            return result;
        }

        public CptCodeCollection GetSorted(CptCodeCollection cptCodeCollection)
        {
            CptCodeCollection result = new CptCodeCollection();
            IOrderedEnumerable<CptCode> orderedResult = cptCodeCollection.OrderBy(i => i.Code);
            foreach (CptCode cptCode in orderedResult)
            {
                result.Add(cptCode);
            }
            return result;
        }

        private static CptCodeCollection GetAll()
        {
            YellowstonePathology.Business.Billing.Model.CptCodeCollection result = new Model.CptCodeCollection();                        
            LuaScript prepared = YellowstonePathology.Store.RedisDB.LuaScriptJsonGet("*");

            foreach(string jString in (string[])YellowstonePathology.Store.AppDataStore.Instance.RedisStore.GetDB(Store.AppDBNameEnum.CPTCode).ScriptEvaluate(prepared))
            {
                JObject jObject = JsonConvert.DeserializeObject<JObject>(jString);
                string c = jObject["code"].ToString();
                if(jObject["codeType"].ToString() == "PQRS")
                {
                    PQRSCode pqrsCode = CptCodeFactory.PQRSFromJson(jObject, null);
                    result.Add(pqrsCode);
                    ExpandPQRSModifiers(jObject, result);
                }
                else
                {
                    CptCode cptCode = CptCodeFactory.CptFromJson(jObject, null);
                    result.Add(cptCode);
                    ExpandCptModifiers(jObject, result);
                }

            }

            return result;
        }

        private static void ExpandCptModifiers(JObject jObject, CptCodeCollection cptCodeCollection)
        {
            foreach (JObject codeModifier in jObject["modifiers"])
            {
                string modifierString = codeModifier["modifier"].ToString();
                CptCode code = CptCodeFactory.CptFromJson(jObject, modifierString);
                cptCodeCollection.Add(code);
            }
        }

        private static void ExpandPQRSModifiers(JObject jObject, CptCodeCollection cptCodeCollection)
        {
            foreach (JObject codeModifier in jObject["modifiers"])
            {
                string modifierString = codeModifier["modifier"].ToString();
                PQRSCode code = CptCodeFactory.PQRSFromJson(jObject, modifierString);
                cptCodeCollection.Add(code);
            }
        }
    }
}
