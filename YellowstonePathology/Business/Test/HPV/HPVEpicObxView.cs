﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.HPV
{
	public class HPVEPICObxView : YellowstonePathology.Business.HL7View.EPIC.EPICObxView
    {
		public HPVEPICObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{
			
		}

        public override void ToXml(XElement document)
        {            
            HPVTestOrder panelSetOrder = (HPVTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            this.AddHeader(document, panelSetOrder, "HPV Report");
            this.AddNextObxElement("", document, "F");            

            string resultText = "Result: " + panelSetOrder.Result;
            this.AddNextObxElement(resultText, document, "F");

            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("Reference: Negative", document, "F");
            this.AddNextObxElement("", document, "F");
            this.AddAmendments(document);

            this.AddNextObxElement("Specimen: ThinPrep fluid", document, "F");
            this.AddNextObxElement("", document, "F");            

            this.AddNextObxElement("Test Information: ", document, "F");            
            this.HandleLongString(panelSetOrder.TestInformation, document, "F");            
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("References:", document, "F");            
            this.HandleLongString(panelSetOrder.ReportReferences, document, "F");
            this.AddNextObxElement("", document, "F");

            this.HandleLongString(panelSetOrder.ASRComment, document, "F");

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.AddNextObxElement(locationPerformed, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
        }
    }
}
