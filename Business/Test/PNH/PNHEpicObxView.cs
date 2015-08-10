using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.PNH
{
	public class PNHEpicObxView : YellowstonePathology.Business.HL7View.EPIC.EpicObxView
    {
		public PNHEpicObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{
			
		}

        public override void ToXml(XElement document)
        {
			PNHTestOrder testOrder = (PNHTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
			this.AddHeader(document, testOrder, "PNH, Highly Sensitive(FLAER)");
            this.AddNextObxElement("", document, "F");

			this.AddNextObxElement("Result: " + testOrder.Result, document, "F");
            this.AddNextObxElement("", document, "F");
            this.AddNextObxElement("Reference: Negative", document, "F");
            this.AddNextObxElement("", document, "F");

			if (string.IsNullOrEmpty(testOrder.Comment) == false)
            {
                this.AddNextObxElement("Comment: ", document, "F");
				this.AddNextObxElement(testOrder.Comment, document, "F");
                this.AddNextObxElement("", document, "F");
            }

			this.AddNextObxElement("Pathologist: " + testOrder.Signature, document, "F");
			if (testOrder.FinalTime.HasValue == true)
            {
				this.AddNextObxElement("E-signed " + testOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
            }
            this.AddNextObxElement("", document, "F");

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(testOrder.OrderedOn, testOrder.OrderedOnId);
            this.AddNextObxElement("Specimen Description: " + specimenOrder.Description, document, "F");
            this.AddNextObxElement("", document, "F");

            this.AddNextObxElement("Method: ", document, "F");
            this.HandleLongString(testOrder.Method, document, "F");
            this.AddNextObxElement("", document, "F");            

            this.AddNextObxElement("References: ", document, "F");
            this.HandleLongString(testOrder.References, document, "F");
            
            this.AddNextObxElement("", document, "F");
            this.HandleLongString(testOrder.ASRComment, document, "F");

			string locationPerformed = testOrder.GetLocationPerformedComment();
            this.HandleLongString(locationPerformed, document, "F");
            this.AddNextObxElement(string.Empty, document, "F");
        }
    }
}
