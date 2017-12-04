using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Surgical
{
	public class PQRSRadicalProstatectomyMeasure : PQRSMeasure
	{
		public PQRSRadicalProstatectomyMeasure()
        {
			this.m_PQRIKeyWordCollection.Add("Prostate");
			this.m_Header = "Radical Prostatectomy Pathology Reporting";

            this.m_CptCodeCollection.Add(Billing.Model.CptCodeCollection.GetCPTCodeById("cpt:88309"));

			this.m_PQRSCodeCollection.Add((Billing.Model.PQRSCode)Billing.Model.CptCodeCollection.GetCPTCodeById("pqrs:3267f"));
			this.m_PQRSCodeCollection.Add((Billing.Model.PQRSCode)Billing.Model.CptCodeCollection.GetCPTCodeById("pqrs:3267f1p"));
			this.m_PQRSCodeCollection.Add((Billing.Model.PQRSCode)Billing.Model.CptCodeCollection.GetCPTCodeById("pqrs:3267f8p"));
			this.m_PQRSCodeCollection.Add((Billing.Model.PQRSCode)Billing.Model.CptCodeCollection.GetCPTCodeById("pqrs:g8798"));
		}        
	}
}
