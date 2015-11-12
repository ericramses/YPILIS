using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.ReticulatedPlateletAnalysis
{
	public class RPAEpicObxView : YellowstonePathology.Business.HL7View.EPIC.EpicObxView
    {
		public RPAEpicObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{
			
		}

        public override void ToXml(XElement document)
        {
			YellowstonePathology.Business.Test.LLP.PanelSetOrderLeukemiaLymphoma panelSetOrder = (YellowstonePathology.Business.Test.LLP.PanelSetOrderLeukemiaLymphoma)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            System.Diagnostics.Trace.Assert(panelSetOrder.FlowMarkerCollection.Count == 1, "There can only be one marker for this test type.");

            this.AddHeader(document, panelSetOrder, "Reticulated Platelet");
            this.AddNextObxElement(string.Empty, document, "F");

            string result = panelSetOrder.FlowMarkerCollection[0].Result;
            this.AddNextObxElement("Result: " + result, document, "F");
            this.AddNextObxElement("Reference: 0-.55%", document, "F");
            this.AddNextObxElement("Antibodies Used: CD41, Thiozole Orange", document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            this.AddAmendments(document);

            this.AddNextObxElement("Method: Quantitative Flow Cytometry", document, "F");
            this.AddNextObxElement(string.Empty, document, "F");

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.AddNextObxElement(locationPerformed, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
        }
    }
}
