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

            this.m_CptCodeCollection.Add(Business.Billing.Model.CptCodeCollection.Instance.GetClone("88309", null));

			this.m_PQRSCodeCollection.Add(Billing.Model.PQRSCodeCollection.Get("3267F", null));
			this.m_PQRSCodeCollection.Add(Billing.Model.PQRSCodeCollection.Get("3267F", "1P"));
			this.m_PQRSCodeCollection.Add(Billing.Model.PQRSCodeCollection.Get("3267F", "8P"));
			this.m_PQRSCodeCollection.Add(Billing.Model.PQRSCodeCollection.Get("G8798", null));
		}        
	}
}
