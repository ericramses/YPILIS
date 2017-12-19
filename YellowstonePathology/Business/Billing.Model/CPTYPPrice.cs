using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
    public class CPTCodeYPPrice
    {
        List<CPTCodePrice> m_CPTCodePriceList;

        public CPTCodeYPPrice()
        {
            this.m_CPTCodePriceList = new List<CPTCodePrice>();            
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("81210", null), "YPI", "YP", 72.51));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("81220", null), "YPI", "YP", 883.90));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("81240", null), "YPI", "YP", 52.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("81241", null), "YPI", "YP", 52.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("81261", null), "YPI", "YP", 101.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("81270", null), "YPI", "YP", 38.44));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("81275", null), "YPI", "YP", 469.98));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("83890", null), "YPI", "YP", 5.35));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("83891", null), "YPI", "YP", 5.35));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("83892", null), "YPI", "YP", 5.35));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("83896", null), "YPI", "YP", 5.35));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("83898", null), "YPI", "YP", 22.39));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("83900", null), "YPI", "YP", 44.77));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("83901", null), "YPI", "YP", 22.39));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("83903", null), "YPI", "YP", 21.98));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("83907", null), "YPI", "YP", 17.84));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("83909", null), "YPI", "YP", 22.39));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("83914", null), "YPI", "YP", 22.39));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("85055", null), "YPI", "YP", 35.76));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("86023", null), "YPI", "YP", 16.63));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("86356", null), "YPI", "YP", 35.76));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("86367", null), "YPI", "YP", 50.37));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("87491", null), "YPI", "YP", 46.87));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("87491", null), "YPI", "Client", 95.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("87591", null), "YPI", "YP", 46.87));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("87591", null), "YPI", "Client", 95.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("87621", null), "YPI", "YP", 46.87));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("87621", null), "YPI", "Client", 66.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("87798", null), "YPI", "YP", 36.85));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("88104", null), "YPI", "YP", 29.45));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("88112", null), "YPI", "YP", 37.71));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("88112", null), "YPI", "Client", 242.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("88141", null), "YPI", "Client", 38.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("88142", null), "YPI", "YP", 40.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("88160", null), "YPI", "YP", 23.10));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("88161", null), "YPI", "YP", 24.69));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("88164", null), "YPI", "YP", 14.11));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("88172", null), "YPI", "YP", 19.61));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("88173", null), "YPI", "YP", 55.18));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("88175", null), "YPI", "YP", 35.37));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("88175", null), "YPI", "Client", 40.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("88182", null), "YPI", "YP", 56.05));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("88184", null), "YPI", "YP", 67.89));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("88185", null), "YPI", "YP", 40.58));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("88300", null), "YPI", "YP", 16.74));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("88302", null), "YPI", "YP", 36.44));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("88304", null), "YPI", "YP", 43.75));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("88304", null), "YPI", "Client", 79.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("88305", null), "YPI", "YP", 57.09));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("88305", null), "YPI", "Client", 110.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("88307", null), "YPI", "YP", 113.95));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("88309", null), "YPI", "YP", 157.06));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("88311", null), "YPI", "YP", 5.31));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("88312", null), "YPI", "YP", 63.13));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("88312", null), "YPI", "Client", 165.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("88313", null), "YPI", "YP", 52.64));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("88313", null), "YPI", "Client", 124));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("88323", null), "YPI", "YP", 48.19));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("88331", null), "YPI", "YP", 24.01));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("88332", null), "YPI", "YP", 8.81));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("88333", null), "YPI", "YP", 27.23));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("88334", null), "YPI", "YP", 16.74));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("88342", null), "YPI", "YP", 50.10));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("88342", null), "YPI", "Client", 105.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("88360", null), "YPI", "YP", 57.09));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("88360", null), "YPI", "Client", 125.00));
            this.m_CPTCodePriceList.Add(new CPTCodePrice(Billing.Model.CptCodeCollection.GetCPTCode("88368", null), "YPI", "YP", 125.07));
        }
    }
}
