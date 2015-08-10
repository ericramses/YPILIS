using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.HL7View.CMMC
{
	public class CCMCBrafNteView : CMMCNteView
	{
        protected YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        protected string m_DateFormat = "yyyyMMddHHmm";
        protected string m_ReportNo;

		public CCMCBrafNteView(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo)
		{
            this.m_AccessionOrder = accessionOrder;
            this.m_ReportNo = reportNo;            
		}

		public override void ToXml(XElement document)
		{
			YellowstonePathology.Domain.Test.BRAFV600EK.BRAFV600EKTestOrder testOrder = (YellowstonePathology.Domain.Test.BRAFV600EK.BRAFV600EKTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);

			this.AddCompanyHeader(document);
			this.AddBlankNteElement(document);

			this.AddNextNteElement("BRAF V600E/K Mutation Analysis", document);
			this.AddNextNteElement("Master Accession #: " + testOrder.MasterAccessionNo, document);
			this.AddNextNteElement("Report #: " + testOrder.ReportNo, document);
			this.AddBlankNteElement(document);

			this.AddNextNteElement("Result: " + testOrder.Result, document);
			this.AddBlankNteElement(document);

			this.AddNextNteElement("Finaled By: ", document);
			this.AddNextNteElement(testOrder.Signature, document);

			if (testOrder.FinalDate.HasValue == true)
			{
				this.AddNextNteElement("*** E-Signed " + testOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm") + " ***", document);
			}
			this.AddBlankNteElement(document);

			this.AddNextNteElement("Indication: " + testOrder.Indication, document);
			this.AddBlankNteElement(document);

			this.AddNextNteElement("Interpretation: ", document);
			this.HandleLongString(testOrder.Interpretation, document);
			this.AddBlankNteElement(document);

			if (string.IsNullOrEmpty(testOrder.TumorNucleiPercent) == false)
			{
				this.AddNextNteElement("Tumor Nuclei Percent: ", document);
				this.HandleLongString(testOrder.TumorNucleiPercent, document);
				this.AddBlankNteElement(document);
			}

			YellowstonePathology.Business.Test.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(testOrder.OrderedOn, testOrder.OrderedOnId);
			this.AddNextNteElement("Specimen: " + specimenOrder.SpecimenNumber.ToString(), document);
			this.HandleLongString(specimenOrder.Description, document);
			this.AddBlankNteElement(document);

			string method = "Following lysis of paraffin embedded tissue; highly purified DNA was extracted from the specimen using an automated method.  PCR amplification using fluorescently labeled primers was then performed.  The products of the PCR reaction were then separated by high resolution capillary electrophoresis to look for the presence of the 107 nucleotide fragment indicative of a BRAF V600E mutation.";
			this.AddNextNteElement("Method:", document);
			this.AddNextNteElement(method, document);
			this.AddBlankNteElement(document);

			this.AddNextNteElement("References: ", document);
			this.HandleLongString(testOrder.Reference, document);
			this.AddBlankNteElement(document);

			string asr = "This test was developed and its performance characteristics determined by Yellowstone Pathology Institute, Inc.  It has not been cleared or approved by the U.S. Food and Drug Administration. The FDA has determined that such clearance or approval is not necessary.  This test is used for clinical purposes.  It should not be regarded as investigational or for research.  This laboratory is certified under the Clinical Laboratory Improvement Amendments of 1988 (CLIA-88) as qualified to perform high complexity clinical laboratory testing.";
			this.AddNextNteElement(asr, document);

			string locationPerformed = testOrder.GetLocationPerformedComment();
			this.AddNextNteElement(locationPerformed, document);
			this.AddBlankNteElement(document);
		}

        /*public override void ToXml(XElement document)
        {
			YellowstonePathology.Business.Test.PanelSetOrderBraf panelSetOrder = (YellowstonePathology.Business.Test.PanelSetOrderBraf)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
			YellowstonePathology.Business.Test.PanelSetResultOrder brafResult = panelSetOrder.PanelSetResultOrderCollection.GetPanelSetResultOrder(193);            

            this.AddCompanyHeader(document);
            this.AddBlankNteElement(document);

			this.AddNextNteElement("BRAF V600E/K Mutation Analysis", document);
            this.AddNextNteElement("Master Accession #: " + panelSetOrder.MasterAccessionNo, document);
            this.AddNextNteElement("Report #: " + panelSetOrder.ReportNo, document);
            this.AddBlankNteElement(document);

			this.AddNextNteElement("Result: " + brafResult.Result, document);
            this.AddBlankNteElement(document);

			this.AddNextNteElement("Finaled By: ", document);
			this.AddNextNteElement(panelSetOrder.Signature, document);

			if (panelSetOrder.FinalDate.HasValue == true)
			{
				this.AddNextNteElement("*** E-Signed " + panelSetOrder.FinalTime.Value.ToString("MM/dd/yyyy HH:mm") + " ***", document);
			}
			this.AddBlankNteElement(document);

			YellowstonePathology.Business.Test.PanelSetOrderComment indication = panelSetOrder.PanelSetOrderCommentCollection.GetByDocumentCommentId(8);
			this.AddNextNteElement("Indication: " + indication.Comment, document);
			this.AddBlankNteElement(document);

			YellowstonePathology.Business.Test.PanelSetOrderComment interpretation = panelSetOrder.PanelSetOrderCommentCollection.GetByDocumentCommentId(1);
			this.AddNextNteElement("Interpretation: ", document);
			this.HandleLongString(interpretation.Comment, document);
			this.AddBlankNteElement(document);

			YellowstonePathology.Business.Test.PanelSetOrderComment tumorNucleiPercent = panelSetOrder.PanelSetOrderCommentCollection.GetByDocumentCommentId(11);
			if (tumorNucleiPercent != null)
			{
				this.AddNextNteElement("Tumor Nuclei Percent: ", document);
				this.HandleLongString(tumorNucleiPercent.Comment, document);
				this.AddBlankNteElement(document);
			}

			YellowstonePathology.Business.Test.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(panelSetOrder.OrderedOn, panelSetOrder.OrderedOnId);
			this.AddNextNteElement("Specimen: " + specimenOrder.SpecimenNumber.ToString(), document);
			this.HandleLongString(specimenOrder.Description, document);
            this.AddBlankNteElement(document);

			string method = "Following lysis of paraffin embedded tissue; highly purified DNA was extracted from the specimen using an automated method.  PCR amplification using fluorescently labeled primers was then performed.  The products of the PCR reaction were then separated by high resolution capillary electrophoresis to look for the presence of the 107 nucleotide fragment indicative of a BRAF V600E mutation.";
			this.AddNextNteElement("Method:", document);
            this.AddNextNteElement(method, document);
            this.AddBlankNteElement(document);

			YellowstonePathology.Business.Test.PanelSetOrderComment references = panelSetOrder.PanelSetOrderCommentCollection.GetByDocumentCommentId(6);
			this.AddNextNteElement("References: ", document);
			this.HandleLongString(references.Comment, document);
			this.AddBlankNteElement(document);

			string asr = "This test was developed and its performance characteristics determined by Yellowstone Pathology Institute, Inc.  It has not been cleared or approved by the U.S. Food and Drug Administration. The FDA has determined that such clearance or approval is not necessary.  This test is used for clinical purposes.  It should not be regarded as investigational or for research.  This laboratory is certified under the Clinical Laboratory Improvement Amendments of 1988 (CLIA-88) as qualified to perform high complexity clinical laboratory testing.";
			this.AddNextNteElement(asr, document);

			string locationPerformed = panelSetOrder.GetLocationPerformedComment();
			this.AddNextNteElement(locationPerformed, document);
			this.AddBlankNteElement(document);
		}*/
	}
}
