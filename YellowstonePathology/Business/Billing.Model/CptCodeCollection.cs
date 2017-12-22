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
                        instance = GetAllCodes();
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

        public void AddCloneWithModifier(string code, string modifier)
        {            
            foreach(CptCode cptCode in this)
            {
                if(cptCode.Code == code)
                {
                    CptCode result = CptCode.Clone(cptCode);
                    if (string.IsNullOrEmpty(modifier) == false)
                    {
                        CptCodeModifier cptCodeModifier = new CptCodeModifier();
                        cptCodeModifier.Modifier = modifier;
                        result.Modifier = cptCodeModifier;
                    }
                    this.Add(result);
                    break;
                }
            }                        
        }        

        public CptCode GetClone(string code, string modifier)
        {
            CptCode result = null;
            foreach(CptCode cptCode in this)
            {
                if(cptCode.Code == code)
                {
                    result = CptCode.Clone(cptCode);
                    if (string.IsNullOrEmpty(modifier) == false)
                    {
                        CptCodeModifier cptCodeModifier = new CptCodeModifier();
                        cptCodeModifier.Modifier = modifier;
                        result.Modifier = cptCodeModifier;
                    }
                    break;
                }
            }
            return result;
        }

        public CptCode Get(string code)
        {
            CptCode result = null;
            foreach (CptCode cptCode in this)
            {
                if (cptCode.Code == code)
                {
                    result = cptCode;                    
                    break;
                }
            }
            return result;
        }

        public CptCodeCollection Clone()
        {
            CptCodeCollection result = new CptCodeCollection();
            foreach(CptCode cptCode in this)
            {
                result.Add(CptCode.Clone(cptCode));
            }
            return result;
        }

        public CptCodeCollection GetCptCodeCollection(FeeScheduleEnum feeSchedule)
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

        public static CptCodeCollection GetAllCodes()
        {
            YellowstonePathology.Business.Billing.Model.CptCodeCollection result = new Model.CptCodeCollection();                        
            
            Store.RedisDB cptDb = Store.AppDataStore.Instance.RedisStore.GetDB(Store.AppDBNameEnum.CPTCode);
            foreach (string jString in (string[])cptDb.GetAllJSONKeys())
            {                
                JObject jObject = JsonConvert.DeserializeObject<JObject>(jString);
                if (jObject["codeType"].ToString() == "PQRS")
                {                    
                    PQRSCode pqrsCode = CptCodeFactory.PQRSFromJson(jObject, null);
                    result.Add(pqrsCode);                    
                }
                else
                {
                    CptCode cptCode = CptCodeFactory.CptFromJson(jObject, null);
                    result.Add(cptCode);                
                }             
            }

            return result;
        }

        private void ExpandCptModifiers(JObject jObject, CptCodeCollection cptCodeCollection)
        {
            foreach (JObject codeModifier in jObject["modifiers"])
            {
                string modifierString = codeModifier["modifier"].ToString();
                CptCode code = CptCodeFactory.CptFromJson(jObject, modifierString);
                cptCodeCollection.Add(code);
            }
        }

        private void ExpandPQRSModifiers(JObject jObject, CptCodeCollection cptCodeCollection)
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
