using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class CPTCodeLibPrice
    {
        List<CPTCodePrice> m_CPTCodePriceList;

        public CPTCodeLibPrice()
        {
            this.m_CPTCodePriceList = new List<CPTCodePrice>();            
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCodeById("cpt:87621"), "YPI", "Client", 51.10));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCodeById("cpt:88112"), "YPI", "Client", 47.63));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCodeById("cpt:88175"), "YPI", "Client", 38.57));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCodeById("cpt:88304"), "YPI", "Client", 52.52));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCodeById("cpt:88305"), "YPI", "Client", 71.76));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCodeById("cpt:88312"), "YPI", "Client", 69.67));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCodeById("cpt:88313"), "YPI", "Client", 55.67));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCodeById("cpt:88342"), "YPI", "Client", 66.52));
        }
    }
}
