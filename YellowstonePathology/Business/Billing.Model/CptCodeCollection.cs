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

        public bool Exists(string code)
        {
            bool result = false;
            foreach(CptCode cptCode in this)
            {
                if(cptCode.Code == code)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public bool HasSVHCDM(string code)
        {
            bool result = false;
            foreach (CptCode cptCode in this)
            {
                if (cptCode.Code == code)
                {
                    if(string.IsNullOrEmpty(cptCode.SVHCDMCode) == false)
                    {
                        result = true;
                        break;
                    }                    
                }
            }
            return result;
        }

        public bool IsMedicareCode(string cptCode)
        {
            bool result = false;
            return result;
        }

        public void AddCloneWithModifier(string code, string modifier)
        {
            foreach (CptCode cptCode in this)
            {
                if (cptCode.Code == code)
                {
                    CptCode result = cptCode.Clone(cptCode);
                    result.SetModifier(modifier);
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
                    result = cptCode.Clone(cptCode);
                    result.SetModifier(modifier);
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
                result.Add(cptCode.Clone(cptCode));
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

        public void Load()
        {
            this.ClearItems();              
            Store.RedisDB cptDb = Store.AppDataStore.Instance.RedisStore.GetDB(Store.AppDBNameEnum.CPTCode);
            foreach (string jString in (string[])cptDb.GetAllJSONKeys())
            {                
                CptCode cptCode = CptCodeFactory.FromJson(jString);
                this.Add(cptCode);                
            }            
        }
    }
}
