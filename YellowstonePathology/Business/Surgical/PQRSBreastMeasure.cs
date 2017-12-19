using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Surgical
{
    public class PQRSBreastMeasure : PQRSMeasure
    {
        public PQRSBreastMeasure()
        {
            this.m_PQRIKeyWordCollection.Add("Breast");
			this.m_Header = "Breast Cancer Resection Pathology Reporting";
            this.m_CptCodeCollection.Add(Billing.Model.CptCodeCollection.GetCPTCode("88307", null));
            this.m_CptCodeCollection.Add(Billing.Model.CptCodeCollection.GetCPTCode("88309", null));

            this.m_PQRSCodeCollection.Add(Billing.Model.PQRSCodeCollection.GetPQRSCode("3260F", null));
			this.m_PQRSCodeCollection.Add(Billing.Model.PQRSCodeCollection.GetPQRSCode("3260F", "1P"));
			this.m_PQRSCodeCollection.Add(Billing.Model.PQRSCodeCollection.GetPQRSCode("3260F", "8P"));
        }        
    }
}
