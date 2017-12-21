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

        public static CptCode Get(string code, string modifier)
        {
            CptCode result = null;
            Store.RedisDB cptDb = Store.AppDataStore.Instance.RedisStore.GetDB(Store.AppDBNameEnum.CPTCode);
            RedisResult redisResult = cptDb.DataBase.Execute("json.get", new object[] { code, "." });
            JObject jObject = JsonConvert.DeserializeObject<JObject>((string)redisResult);
            if (jObject["codeType"].ToString() == "PQRS")
            {
                result = CptCodeFactory.PQRSFromJson(jObject, modifier);
            }
            else
            {
                result = CptCodeFactory.CptFromJson(jObject, modifier);
            }
            return result;
        }

        public static CptCodeCollection GetCptCodeCollection(FeeScheduleEnum feeSchedule)
        {
            CptCodeCollection result = new CptCodeCollection();
            foreach (CptCode cptCode in GetAll(true, true))
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

        public static CptCodeCollection GetCollection(Collection<CPTCodeWithModifier> codesAndModifiers)
        {
            CptCodeCollection result = new Model.CptCodeCollection();
            LuaScript prepared = YellowstonePathology.Store.RedisDB.LuaScriptJsonGetKeys(codesAndModifiers);
            Store.RedisDB cptDb = Store.AppDataStore.Instance.RedisStore.GetDB(Store.AppDBNameEnum.CPTCode);
            string[] redisResults = (string[])cptDb.DataBase.ScriptEvaluate(prepared);
            for (int idx = 0; idx < redisResults.Length; idx ++)
            {
                CPTCodeWithModifier cptCodeWithModifier = codesAndModifiers[idx];
                JObject jObject = JsonConvert.DeserializeObject<JObject>(redisResults[idx]);
                if (jObject["codeType"].ToString() == "PQRS")
                {
                    PQRSCode pqrsCode = CptCodeFactory.PQRSFromJson(jObject, cptCodeWithModifier.Modifier);
                    result.Add(pqrsCode);
                }
                else
                {
                    CptCode cptCode = CptCodeFactory.CptFromJson(jObject, cptCodeWithModifier.Modifier);
                    result.Add(cptCode);
                }
            }
            return result;
        }

        public static CptCodeCollection GetAll(bool includePqrs, bool expandModifiers)
        {
            YellowstonePathology.Business.Billing.Model.CptCodeCollection result = new Model.CptCodeCollection();                        
            
            Store.RedisDB cptDb = Store.AppDataStore.Instance.RedisStore.GetDB(Store.AppDBNameEnum.CPTCode);
            foreach (RedisResult redisResult in (RedisResult[])cptDb.GetAllJSONKeys())
            {
                /*
                JObject jObject = JsonConvert.DeserializeObject<JObject>(redisResult);
                if (jObject["codeType"].ToString() == "PQRS")
                {
                    if (includePqrs == true)
                    {
                        PQRSCode pqrsCode = CptCodeFactory.PQRSFromJson(jObject, null);
                        result.Add(pqrsCode);
                        if (expandModifiers == true) ExpandPQRSModifiers(jObject, result);
                    }
                }
                else
                {
                    CptCode cptCode = CptCodeFactory.CptFromJson(jObject, null);
                    result.Add(cptCode);
                    if (expandModifiers == true) ExpandCptModifiers(jObject, result);
                }
                */
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
