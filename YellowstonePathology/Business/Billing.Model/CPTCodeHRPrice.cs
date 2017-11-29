using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class CPTCodeHRPrice
    {
        List<CPTCodePrice> m_CPTCodePriceList;

        public CPTCodeHRPrice()
        {
            this.m_CPTCodePriceList = new List<CPTCodePrice>();            
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("cpt:88104"), "YPI", "Client", 19.8));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("cpt:88112"), "YPI", "Client", 43.33));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("cpt:88160"), "YPI", "Client", 20.11));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("cpt:88161"), "YPI", "Client", 23.55));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("cpt:88172"), "YPI", "Client", 15.41));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("cpt:88173"), "YPI", "Client", 49.25));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("cpt:88300"), "YPI", "Client", 13.50));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("cpt:88302"), "YPI", "Client", 31.08));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("cpt:88304"), "YPI", "Client", 39.23));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("cpt:88305"), "YPI", "Client", 50.87));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("cpt:88307"), "YPI", "Client", 79.75));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("cpt:88309"), "YPI", "Client", 216.72));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("cpt:8311"), "YPI", "Client", 4.41));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("cpt:88312"), "YPI", "Client", 40.77));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("cpt:88313"), "YPI", "Client", 36.39));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("cpt:88331"), "YPI", "Client", 19.84));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("cpt:88332"), "YPI", "Client", 7.25));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("cpt:88342"), "YPI", "Client", 39.12));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("cpt:88360"), "YPI", "Client", 42.00));
        }
    }
}
