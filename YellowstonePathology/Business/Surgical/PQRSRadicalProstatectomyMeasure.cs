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

            this.m_CptCodeCollection.Add(Billing.Model.CptCodeCollection.GetCPTCode("88309", null));

			this.m_PQRSCodeCollection.Add((Billing.Model.PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("3267F", null));
			this.m_PQRSCodeCollection.Add((Billing.Model.PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("3267F", "1P"));
			this.m_PQRSCodeCollection.Add((Billing.Model.PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("3267F", "8P"));
			this.m_PQRSCodeCollection.Add((Billing.Model.PQRSCode)Billing.Model.CptCodeCollection.GetCPTCode("G8798", null));
		}        
	}
}
