using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class CPTCodeCMMCPrice
    {
        List<CPTCodePrice> m_CPTCodePriceList;

        public CPTCodeCMMCPrice()
        {
            this.m_CPTCodePriceList = new List<CPTCodePrice>();            
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCodeById("cpt:81210"), "YPI", "Client", 289.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCodeById("cpt:81275"), "YPI", "Client", 600.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCodeById("cpt:87621"), "YPI", "Client", 92.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCodeById("cpt:88104"), "YPI", "Client", 29.15));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCodeById("cpt:88112"), "YPI", "Client", 22.63));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCodeById("cpt:88160"), "YPI", "Client", 24.72));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCodeById("cpt:88161"), "YPI", "Client", 33.39));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCodeById("cpt:88173"), "YPI", "Client", 48.70));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCodeById("cpt:88175"), "YPI", "Client", 23.49));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCodeById("cpt:88184"), "YPI", "Client", 179.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCodeById("cpt:88185"), "YPI", "Client", 107.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCodeById("cpt:88300"), "YPI", "Client", 6.56));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCodeById("cpt:88302"), "YPI", "Client", 14.94));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCodeById("cpt:88304"), "YPI", "Client", 20.74));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCodeById("cpt:88305"), "YPI", "Client", 21.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCodeById("cpt:88307"), "YPI", "Client", 132.76));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCodeById("cpt:88309"), "YPI", "Client", 188.49));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCodeById("cpt:88311"), "YPI", "Client", 5.16));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCodeById("cpt:88312"), "YPI", "Client", 43.58));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCodeById("cpt:88313"), "YPI", "Client", 34.96));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCodeById("cpt:88323"), "YPI", "Client", 38.69));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCodeById("cpt:88331"), "YPI", "Client", 23.56));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCodeById("cpt:88332"), "YPI", "Client", 8.19));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCodeById("cpt:88333"), "YPI", "Client", 26.59));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCodeById("cpt:88334"), "YPI", "Client", 16.80));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCodeById("cpt:88342"), "YPI", "Client", 35.00));
                //above code is for non-medicare. Medicare code G0461/G0462 is 37.52
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCodeById("cpt:88360"), "YPI", "Client", 48.94));
        }
    }
}
