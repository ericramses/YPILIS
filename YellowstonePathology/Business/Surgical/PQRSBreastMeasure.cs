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
            this.m_CptCodeCollection.Add(Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("cpt:88307"));
            this.m_CptCodeCollection.Add(Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("cpt:88309"));

            this.m_PQRSCodeCollection.Add((Billing.Model.PQRSCode)Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("pqrs:3260f"));
			this.m_PQRSCodeCollection.Add((Billing.Model.PQRSCode)Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("pqrs:3260f1p"));
			this.m_PQRSCodeCollection.Add((Billing.Model.PQRSCode)Billing.Model.CptCodeCollection.Instance.GetCPTCodeById("pqrs:3260f8p"));
        }        
    }
}
