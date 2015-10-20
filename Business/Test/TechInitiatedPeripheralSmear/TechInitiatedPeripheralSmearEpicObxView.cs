using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.TechInitiatedPeripheralSmear
{
    public class TechInitiatedPeripheralSmearEpicObxView : YellowstonePathology.Business.HL7View.EPIC.EpicObxView
    {
        public TechInitiatedPeripheralSmearEpicObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{

        }

        public override void ToXml(XElement document)
        {
            TechInitiatedPeripheralSmearTestOrder testOrder = (TechInitiatedPeripheralSmearTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

            this.AddHeader(document, testOrder, "Tech Initiated Peripheral Smear");
            this.AddNextObxElement(string.Empty, document, "F");

            this.HandleLongString("Tech Comment: " + testOrder.TechComment, document, "F");
            this.AddNextObxElement("", document, "F");

            this.HandleLongString("Pathologist Comment: " + testOrder.PathologistComment, document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("Specimen: Thin Prep Fluid", document, "F");
            this.AddNextObxElement("", document, "F");

            string locationPerformed = testOrder.GetLocationPerformedComment();
            this.AddNextObxElement(locationPerformed, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
        }
    }
}
