using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.HER2AmplificationByISH
{
	public class HER2AmplificationByISHCMMCNteView : YellowstonePathology.Business.HL7View.CMMC.CMMCNteView
	{
        protected YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        protected string m_DateFormat = "yyyyMMddHHmm";
        protected string m_ReportNo;

		public HER2AmplificationByISHCMMCNteView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo)
		{
            this.m_AccessionOrder = accessionOrder;
            this.m_ReportNo = reportNo;            
		}		

		public override void ToXml(XElement document, object writer)
		{
			HER2AmplificationByISHTestOrder panelSetOrder = (HER2AmplificationByISHTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            this.AddCompanyHeader(document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("HER2 Gene Amplification", document);
            this.AddNextNteElement("Master Accession #: " + panelSetOrder.MasterAccessionNo, document);
            this.AddNextNteElement("Report #: " + panelSetOrder.ReportNo, document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("HER2: " + panelSetOrder.Result, document);
            this.AddNextNteElement("Ratio: " + panelSetOrder.Her2Chr17Ratio, document);

            if (panelSetOrder.ResultComment != string.Empty)
            {
                this.AddNextNteElement("Comment: " + panelSetOrder.ResultComment, document);
                this.AddBlankNteElement(document);
            }

            this.AddNextNteElement("Number of invasive tumor cells counted: " + panelSetOrder.CellsCounted.ToString(), document);
            this.AddNextNteElement("Number of observers: " + panelSetOrder.NumberOfObservers.ToString(), document);
            this.AddNextNteElement("HER2 average copy number per nucleus: " + panelSetOrder.AverageHer2NeuSignal, document);
            this.AddNextNteElement("Chr17 average copy number per nucleus: " + panelSetOrder.AverageChr17Signal, document);
            this.AddNextNteElement("Ratio of average HER2/Chr17 signals: " + panelSetOrder.Her2Chr17Ratio, document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("Interpretation: ", document);
            this.HandleLongString(panelSetOrder.InterpretiveComment, document);
            this.AddBlankNteElement(document);

            this.AddNextNteElement("Method: ", document);
            string method = System.Security.SecurityElement.Escape("This test was performed using a molecular method, In Situ Hybridization (ISH) with the US FDA approved Inform HER2 DNA probe kit, modified to report results according to ASCO/CAP guidelines. The test was performed on paraffin embedded tissue in compliance with ASCO/CAP guidelines.  Probes used include a locus specific probe for HER2 and an internal hybridization control probe for the centromeric region of chromosome 17 (Chr17).");
            this.AddNextNteElement(method, document);
            this.AddBlankNteElement(document);            

            this.AddNextNteElement("References: ", document);
            this.AddNextNteElement(panelSetOrder.ReportReference, document);
            this.AddBlankNteElement(document);

            foreach (YellowstonePathology.Business.Amendment.Model.Amendment amendment in panelSetOrder.AmendmentCollection)
            {
                this.AddNextNteElement(amendment.AmendmentType + ": " + amendment.AmendmentDate.Value.ToString("MM/dd/yyyy"), document);
                this.HandleLongString(amendment.Text, document);
                if (amendment.RequirePathologistSignature == true)
                {
                    this.AddNextNteElement("Signature: " + amendment.PathologistSignature, document);
                }
                this.AddNextNteElement("", document);
            }            
		}        
	}
}
