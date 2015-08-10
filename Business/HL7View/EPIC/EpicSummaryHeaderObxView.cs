using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.HL7View.EPIC
{
	public class EpicSummaryHeaderObxView : EpicObxView
	{
        public EpicSummaryHeaderObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{
			
		}
	
		public override void ToXml(XElement document)
		{			
            this.AddNextObxElement(string.Empty, document, "F");
            this.AddNextObxElement("Testing performed by: Yellowstone Pathology Institute, Inc", document, "F");            
            this.AddNextObxElement(string.Empty, document, "F");
            this.AddNextObxElement("Summary Report For: " + this.m_AccessionOrder.PatientDisplayName, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");            
		}        		
	}
}
