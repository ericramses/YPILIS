using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Surgical
{
	public class PQRSColorectalMeasure : PQRSMeasure
	{
		public PQRSColorectalMeasure()
        {            
            this.m_PQRIKeyWordCollection.Add("Cecum");
			this.m_PQRIKeyWordCollection.Add("Colon");
            this.m_PQRIKeyWordCollection.Add("Sigmoid");
            this.m_PQRIKeyWordCollection.Add("Rectum");
            this.m_PQRIKeyWordCollection.Add("Apendix");
            this.m_PQRIKeyWordCollection.Add("Appendix");

            this.m_Header = "Colorectal Cancer Pathology Reporting";
            this.m_CptCodeCollection.Add(Billing.Model.CptCodeCollection.Get("88309", null));

			this.m_PQRSCodeCollection.Add(Billing.Model.PQRSCodeCollection.Get("G8721", null));
			this.m_PQRSCodeCollection.Add(Billing.Model.PQRSCodeCollection.Get("G8722", null));
			this.m_PQRSCodeCollection.Add(Billing.Model.PQRSCodeCollection.Get("G8723", null));
		}        
	}
}
