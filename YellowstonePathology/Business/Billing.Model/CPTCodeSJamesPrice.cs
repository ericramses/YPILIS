using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class CPTCodeSJamesPrice
    {
        List<CPTCodePrice> m_CPTCodePriceList;

        public CPTCodeSJamesPrice()
        {
            this.m_CPTCodePriceList = new List<CPTCodePrice>();            
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("cpt:85055"), "YPI", "Client", 42.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("cpt:86023"), "YPI", "Client", 40.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("cpt:88312"), "YPI", "Client", 72.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("cpt:88313"), "YPI", "Client", 56.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("cpt:88323"), "YPI", "Client", 59.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("cpt:88342"), "YPI", "Client", 65.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("cpt:88360"), "YPI", "Client", 66.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("cpt:88368"), "YPI", "Client", 172.50));
        }
    }
}
