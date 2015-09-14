﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.HPVTWI
{
	public class HPVTWIEpicObxView : YellowstonePathology.Business.HL7View.EPIC.EpicObxView
    {
		public HPVTWIEpicObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{
			
		}

        public override void ToXml(XElement document)
        {            
            PanelSetOrderHPVTWI panelSetOrder = (PanelSetOrderHPVTWI)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            this.AddHeader(document, panelSetOrder, "High Risk HPV Report");
            this.AddNextObxElement("", document, "F");            

            string resultText = "Result: " + panelSetOrder.Result;
            this.AddNextObxElement(resultText, document, "F");

            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("Reference: Negative", document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("Specimen: ThinPrep fluid", document, "F");
            this.AddNextObxElement("", document, "F");

            bool hpvHasBeenOrdered = this.m_AccessionOrder.PanelSetOrderCollection.Exists(62);

            string additionalTestingComment = string.Empty;
            if (hpvHasBeenOrdered == true)
            {
				additionalTestingComment = YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapWordDocument.HPV1618HasBeenOrderedComment;                
            }
            else
            {
				additionalTestingComment = YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapWordDocument.NoAdditionalTestingOrderedComment;
            }

            this.AddNextObxElement("Additional Testing:", document, "F");
            this.AddNextObxElement(additionalTestingComment, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            this.AddNextObxElement("Test Information: ", document, "F");            
            this.AddNextObxElement(panelSetOrder.TestInformation, document, "F");            
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("References:", document, "F");            
            this.AddNextObxElement(panelSetOrder.References, document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement(panelSetOrder.ASRComment, document, "F");

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.AddNextObxElement(locationPerformed, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
        }
    }
}
