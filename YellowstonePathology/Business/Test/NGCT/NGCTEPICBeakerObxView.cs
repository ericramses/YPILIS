using System;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.NGCT
{
    public class NGCTEPICBeakerObxView : YellowstonePathology.Business.HL7View.EPIC.EPICObxView
    {
        public NGCTEPICBeakerObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{

        }

        public override void ToXml(XElement document)
        {
            NGCTTestOrder testOrder = (NGCTTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

            this.AddHeader(document, testOrder, "Chlamydia Gonorrhea Screening");

            this.AddNextObxElementBeaker("CHLAMYDIATRACHOMATISRESULT", "Chlamydia trachomatis result: " + testOrder.ChlamydiaTrachomatisResult, document, "F");
            this.AddNextObxElementBeaker("CHLAMYDIATRACHOMATISRESULTREFERENCE", "Reference: Negative", document, "F");

            this.AddNextObxElementBeaker("NEISSERIAGONORROEAERESULT", "Neisseria gonorrhoeae result: " + testOrder.NeisseriaGonorrhoeaeResult, document, "F");
            this.AddNextObxElementBeaker("NEISSERIAGONORROEARESULTREFERENCE", "Reference: Negative", document, "F");

            this.AddAmendments(document);

            this.AddNextObxElementBeaker("SPECIMENDESCRIPTION", "Specimen: Thin Prep Fluid", document, "F");

            this.AddNextObxElementBeaker("METHOD", "Method: " + testOrder.Method, document, "F");

            this.AddNextObxElementBeaker("REFERENCES", "References: " + testOrder.ReportReferences, document, "F");

            this.AddNextObxElementBeaker("TESTINFORMATION", "Test Information: " + testOrder.TestInformation, document, "F");

            string locationPerformed = testOrder.GetLocationPerformedComment();
            this.AddNextObxElementBeaker("LOCATIONPERFORMED", "Location Performed: " + locationPerformed, document, "F");
        }
    }
}
