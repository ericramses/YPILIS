using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class CPTCodeBigHornPrice
    {
        List<CPTCodePrice> m_CPTCodePriceList;

        public CPTCodeBigHornPrice()
        {
            this.m_CPTCodePriceList = new List<CPTCodePrice>();            
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("cpt:87491("), "YPI", "Client", 97.66));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("cpt:87591"), "YPI", "Client", 97.66));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("cpt:88112"), "YPI", "Client", 47.63));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("cpt:88175"), "YPI", "Client", 38.57));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("cpt:88304"), "YPI", "Client", 52.52));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("cpt:88305"), "YPI", "Client", 71.76));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("cpt:88307"), "YPI", "Client", 159.95));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("cpt:88312"), "YPI", "Client", 69.67));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("cpt:88313"), "YPI", "Client", 55.67));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("cpt:88323"), "YPI", "Client", 60.19));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("cpt:88342"), "YPI", "Client", 66.52));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("cpt:88368"), "YPI", "Client", 230.27));
        }
    }
}
