using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class CPTCodeBZPrice
    {
        List<CPTCodePrice> m_CPTCodePriceList;

        public CPTCodeBZPrice()
        {
            this.m_CPTCodePriceList = new List<CPTCodePrice>();            
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("cpt:85055"), "YPI", "Client", 41.71));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("cpt:86023"), "YPI", "Client", 19.40));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("cpt:88184"), "YPI", "Client", 91.03));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("cpt:88185"), "YPI", "Client", 55.08));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("cpt:88312"), "YPI", "Client", 74.55));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("cpt:88313"), "YPI", "Client", 59.57));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("cpt:88323"), "YPI", "Client", 61.45));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("cpt:88342"), "YPI", "Client", 71.18));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("cpt:88360"), "YPI", "Client", 76.42));
        }
    }
}
