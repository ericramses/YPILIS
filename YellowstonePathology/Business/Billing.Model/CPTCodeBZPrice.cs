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
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Get("85055", null), "YPI", "Client", 41.71));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Get("86023", null), "YPI", "Client", 19.40));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Get("88184", null), "YPI", "Client", 91.03));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Get("88185", null), "YPI", "Client", 55.08));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Get("88312", null), "YPI", "Client", 74.55));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Get("88313", null), "YPI", "Client", 59.57));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Get("88323", null), "YPI", "Client", 61.45));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Get("88342", null), "YPI", "Client", 71.18));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Get("88360", null), "YPI", "Client", 76.42));
        }
    }
}
