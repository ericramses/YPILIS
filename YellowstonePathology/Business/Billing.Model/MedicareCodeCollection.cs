using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class MedicareCodeCollection : ObservableCollection<CptCode>
    {
        public bool IsMedicareCode(string cptCode)
        {
            bool result = false;
            foreach (CptCode item in this)
            {
                if (item.Code == cptCode)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public static MedicareCodeCollection GetAll()
        {
            MedicareCodeCollection result = new MedicareCodeCollection();
            result.Add(CptCodeCollection.GetCPTCodeById("cpt:g0123"));
            result.Add(CptCodeCollection.GetCPTCodeById("cpt:g0124"));
            result.Add(CptCodeCollection.GetCPTCodeById("cpt:g0145"));
            result.Add(CptCodeCollection.GetCPTCodeById("cpt:g0461"));
            result.Add(CptCodeCollection.GetCPTCodeById("cpt:g0462"));

            return result;
        }
    }
}
