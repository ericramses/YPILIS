using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.HPV1618
{
    public class HPV1618EPICBeakerObxView : YellowstonePathology.Business.HL7View.EPIC.EPICObxView
    {
		public HPV1618EPICBeakerObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{

        }

        public override void ToXml(XElement document)
        {
            PanelSetOrderHPV1618 panelSetOrder = (PanelSetOrderHPV1618)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            this.AddHeader(document, panelSetOrder, "HPV-16/18 Genotyping");

            this.AddNextObxElementBeaker("HPV16RESULT", "HPV-16 Result: " + panelSetOrder.HPV16Result, document, "F");
            this.AddNextObxElementBeaker("HPV16RESULTREFERENCE", "HPV-16 Reference: Negative", document, "F");

            this.AddNextObxElementBeaker("HPV1845RESULT", "HPV-18/45 Result: " + panelSetOrder.HPV18Result, document, "F");
            this.AddNextObxElementBeaker("HPV1845RESULTREFERENCE", "HPV-18/45 Reference: Negative", document, "F");

            if (string.IsNullOrEmpty(panelSetOrder.Comment) == false)
            {
                this.AddNextObxElementBeaker("COMMENT", "Comment: " + panelSetOrder.Comment, document, "F");
            }

            this.AddAmendments(document);

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderByOrderTarget(panelSetOrder.OrderedOnId);
            this.AddNextObxElementBeaker("SPECIMENDESCRIPTION", "Specimen: " + specimenOrder.GetDescription(), document, "F");

            this.AddNextObxElementBeaker("METHOD", "Method: " + panelSetOrder.Method, document, "F");

            this.AddNextObxElementBeaker("REFERENCES", "References: " + panelSetOrder.ReportReferences, document, "F");

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.AddNextObxElementBeaker("LOCATIONPERFORMED", "Location Performed: " + locationPerformed, document, "F");
        }
    }
}
