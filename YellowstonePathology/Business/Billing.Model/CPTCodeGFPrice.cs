using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class CPTCodeGFPrice
    {
        List<CPTCodePrice> m_CPTCodePriceList;

        public CPTCodeGFPrice()
        {
            this.m_CPTCodePriceList = new List<CPTCodePrice>();
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCodeById("cpt:85055"), "YPI", "Client", 42.88));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCodeById("cpt:86023"), "YPI", "Client", 19.94));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCodeById("cpt:86360"), "YPI", "Client", 75.21));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCodeById("cpt:87491"), "YPI", "Client", 97.66));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCodeById("cpt:87591"), "YPI", "Client", 97.66));
        }
    }
}
