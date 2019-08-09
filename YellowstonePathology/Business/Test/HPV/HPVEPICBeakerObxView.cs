using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.HPV
{
    public class HPVEPICBeakerObxView : YellowstonePathology.Business.HL7View.EPIC.EPICObxView
    {
        public HPVEPICBeakerObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{

        }

        public override void ToXml(XElement document)
        {
            HPVTestOrder panelSetOrder = (HPVTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            this.AddHeader(document, panelSetOrder, "HPV Report");

            this.AddNextObxElementBeaker("RESULT", "Result: " + panelSetOrder.Result, document, "F");

            this.AddNextObxElementBeaker("RESULTREFERENCE", "Reference: Negative", document, "F");

            this.AddAmendments(document);

            this.AddNextObxElementBeaker("SPECIMENDESCRIPTION", "Specimen: ThinPrep fluid", document, "F");

            this.AddNextObxElementBeaker("TESTINFORMATION", "Test Information: " + panelSetOrder.TestInformation, document, "F");

            this.AddNextObxElementBeaker("REFERENCES", "References: " + panelSetOrder.ReportReferences, document, "F");

            this.AddNextObxElementBeaker("ASR", "ASR: " + panelSetOrder.ASRComment, document, "F");

            string locationPerformed = panelSetOrder.GetLocationPerformedComment();
            this.AddNextObxElementBeaker("LOCATIONPERFORMED", "Location Performed: " + locationPerformed, document, "F");
        }
    }
}
