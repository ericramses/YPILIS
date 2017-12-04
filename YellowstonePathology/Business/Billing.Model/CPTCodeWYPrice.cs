using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class CPTCodeWYPrice
    {
        List<CPTCodePrice> m_CPTCodePriceList;

        public CPTCodeWYPrice()
        {
            this.m_CPTCodePriceList = new List<CPTCodePrice>();            
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCodeById("cpt:88112"), "YPI", "Client", 46.37));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCodeById("cpt:88184"), "YPI", "Client", 82.79));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCodeById("cpt:88302"), "YPI", "Client", 49.33));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCodeById("cpt:88304"), "YPI", "Client", 51.14));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCodeById("cpt:88305"), "YPI", "Client", 69.86));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCodeById("cpt:88307"), "YPI", "Client", 155.63));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCodeById("cpt:88312"), "YPI", "Client", 67.81));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCodeById("cpt:88313"), "YPI", "Client", 54.20));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCodeById("cpt:88342"), "YPI", "Client", 64.75));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCodeById("cpt:88360"), "YPI", "Client", 69.52));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCodeById("cpt:88368"), "YPI", "Client", 160.06));
        }
    }
}
