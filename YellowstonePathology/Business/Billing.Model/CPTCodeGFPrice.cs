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
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Instance.Get("85055", null), "YPI", "Client", 42.88));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Instance.Get("86023", null), "YPI", "Client", 19.94));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Instance.Get("86360", null), "YPI", "Client", 75.21));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Instance.Get("87491", null), "YPI", "Client", 97.66));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.Instance.Get("87591", null), "YPI", "Client", 97.66));
        }
    }
}
