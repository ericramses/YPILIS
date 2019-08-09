using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.ThinPrepPap
{
    public class ThinPrepPapEPICBeakerObxView : YellowstonePathology.Business.HL7View.EPIC.EPICObxView
    {
        public ThinPrepPapEPICBeakerObxView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, int obxCount) 
            : base(accessionOrder, reportNo, obxCount)
		{

        }

        public override void ToXml(XElement document)
        {
            YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology panelSetOrderCytology = (YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

            this.AddHeader(document, panelSetOrderCytology, "Thin Prep Pap Report");
            this.AddNextObxElement("", document, "F");

            //this.AddNextObxElement("##############################################", document, "F");
            this.AddNextObxElementBeaker("EPITHELIALCELL", "Epithelial Cell Description:", document, "F");
            this.AddNextObxElementBeaker("SCREENINGIMPRESSION", panelSetOrderCytology.ScreeningImpression.ToUpper(), document, "F");

            //this.AddNextObxElement(string.Empty, document, "F");

            string otherConditions = panelSetOrderCytology.OtherConditions;
            if (string.IsNullOrEmpty(otherConditions) == true)
            {
                otherConditions = "None.";
            }
            this.AddNextObxElementBeaker("OTHERCONDITIONS", "Other Conditions: "  + otherConditions, document, "F");
            //else
            //{
            //    this.HandleLongString(otherConditions, document, "F");
            //}

            //this.AddNextObxElement("##############################################", document, "F");
            //this.AddNextObxElement(string.Empty, document, "F");

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrderCytology.OrderedOn, panelSetOrderCytology.OrderedOnId);
            this.AddNextObxElementBeaker("SPECIMEN", "Description: " + specimenOrder.Description, document, "F");
            //this.AddNextObxElement(specimenOrder.Description, document, "F");
            //this.AddNextObxElement(string.Empty, document, "F");

            this.AddNextObxElementBeaker("SPECIMEN", "Specimen Adequacy: " + panelSetOrderCytology.SpecimenAdequacy, document, "F");
            //this.AddNextObxElement(panelSetOrderCytology.SpecimenAdequacy, document, "F");
            //this.AddNextObxElement(string.Empty, document, "F");

            if (string.IsNullOrEmpty(panelSetOrderCytology.ReportComment) == false)
            {
                this.AddNextObxElementBeaker("COMMENT", "Comment: " + panelSetOrderCytology.ReportComment, document, "F");
                //string reportComment = panelSetOrderCytology.ReportComment;
                //this.HandleLongString(reportComment, document, "F");
                //this.AddNextObxElement(string.Empty, document, "F");
            }

            //this.AddNextObxElement(string.Empty, document, "F");

            this.AddNextObxElementBeaker("SIGNATURE", "Finaled By: " + panelSetOrderCytology.Signature, document, "F");
            if (panelSetOrderCytology.FinalTime.HasValue == true)
            {
                this.AddNextObxElementBeaker("FINALDATE", "E-signed " + panelSetOrderCytology.FinalTime.Value.ToString("MM/dd/yyyy HH:mm"), document, "F");
            }
            //this.AddNextObxElement(string.Empty, document, "F");
            this.AddAmendments(document);

            this.AddNextObxElementBeaker("SCREENINGMETHOD", "Screening Method: " + panelSetOrderCytology.Method, document, "F");
            //this.AddNextObxElementBeaker(panelSetOrderCytology.Method, document, "F");
            //this.AddNextObxElement(string.Empty, document, "F");

            this.AddNextObxElementBeaker("REFERENCES", "References: " + panelSetOrderCytology.ReportReferences, document, "F");
            //this.AddNextObxElement(panelSetOrderCytology.ReportReferences, document, "F");
            //this.AddNextObxElement(string.Empty, document, "F");

            string disclaimer = "This Pap test is only a screening test. A negative result does not definitively rule out the presence of disease. Women should, therefore, in consultation with their physician, have this test performed at mutually agreed intervals.";
            this.AddNextObxElementBeaker("ASR", "ASR: " + disclaimer, document, "F");
            //this.AddNextObxElement(string.Empty, document, "F");

            string locationPerformed = panelSetOrderCytology.GetLocationPerformedComment();
            this.AddNextObxElementBeaker("LOCATIONPERFORMED", "Location Performed: " + locationPerformed, document, "F");
            //this.AddNextObxElement(string.Empty, document, "F");
        }
    }
}
