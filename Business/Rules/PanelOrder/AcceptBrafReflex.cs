using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Rules.PanelOrder
{
	public class AcceptBrafReflex : AcceptBraf
	{
		public AcceptBrafReflex()
        {
			this.m_Rule.ActionList.Clear();

			this.m_Rule.ActionList.Add(DoesUserHavePermission);
			this.m_Rule.ActionList.Add(HaltIfPanelOrderIsAccepted);
			this.m_Rule.ActionList.Add(HaltIfPanelSetOrderIsFinal);
			this.m_Rule.ActionList.Add(HaltIfAnyResultsAreEmpty);
			this.m_Rule.ActionList.Add(PatientIsLinked);
			this.m_Rule.ActionList.Add(IndicatorIsSet);
			this.m_Rule.ActionList.Add(SetPositiveResult);
			this.m_Rule.ActionList.Add(SetNegativeResult);			
			this.m_Rule.ActionList.Add(SetResultCommentPapillaryThyroid);			
			this.m_Rule.ActionList.Add(SetReportIndicationCRC);
			this.m_Rule.ActionList.Add(SetReportIndicationPapillaryThyroid);
			this.m_Rule.ActionList.Add(SetReportIndicationMetastaticMelanoma);
			this.m_Rule.ActionList.Add(SetReportInterpretationCRC);
			this.m_Rule.ActionList.Add(SetReportInterpretationPapillaryThyroid);
			this.m_Rule.ActionList.Add(SetReportInterpretationMetastaticMelanoma);
			this.m_Rule.ActionList.Add(SetReportMethod);
			this.m_Rule.ActionList.Add(AcceptPanel);
			this.m_Rule.ActionList.Add(Save);
		}		

		protected override void SetReportInterpretationCRC()
		{
			StringBuilder interpretation = new StringBuilder();
			interpretation.Append("The use of monoclonal antibodies such as cetuximab has expanded the treatment options for patients with metastatic colorectal cancer (CRC). These agents selectively inhibit the ");
			interpretation.Append("epidermal growth factor receptor (EGFR) and thus prevent activation of the RAS-RAF-MAPK pathway that drives tumor growth.  Recent studies have demonstrated that as many as 35 to ");
			interpretation.Append("45% of metastatic colorectal cancers harbor oncogenic point mutations in codons 12 or 13 of the KRAS gene, resulting in constitutive activation of the RAS-RAF-MAPK pathway.  Both ");
			interpretation.Append("KRAS and BRAF mutations result in constitutive activation of the RAS-RAF-MAPK pathway, leading to uncontrolled proliferation of tumor cells.  Tumors with either KRAS or BRAF ");
			interpretation.Append("mutations exhibit resistance to anti-EGFR therapies.  Therefore, testing for both KRAS and BRAF mutations provides useful prognostic information and allows for individualized ");
			interpretation.Append("treatment of patients with metastatic CRC.  A product indicative of a KRAS mutation was not detected; however, a 107-base product indicative of a BRAFV600E mutation was detected.  ");
			interpretation.Append("This confirms that the patient has a metastatic CRC that is unlikely to respond to anti-EGFR therapy.");

			if (this.ResultIsPositive())
			{
				this.m_PanelSetOrder.PanelSetOrderCommentCollection.GetPanelSetOrderComment("report_interpretation").Comment = interpretation.ToString();  
			}
			else if (this.ResultIsNegative())
			{
				this.m_PanelSetOrder.PanelSetOrderCommentCollection.GetPanelSetOrderComment("report_interpretation").Comment = m_CommentList.GetCommentListItemByCommentId(44).Comment;
			}
		}

		private void SetReportMethod()
		{
			StringBuilder method = new StringBuilder();
			method.Append("DNA was first extracted from the patient’s paraffin-embedded specimen using an automated DNA extraction system.  KRAS allele-specific PCR was then performed on the patient’s sample.  ");
			method.Append("The products generated from this reaction were then subjected to a second PCR step employing fluorescently-labeled nucleotides and primers designed to detect both mutant and wild-type forms " );
			method.Append("of the KRAS gene.  Utilizing the SHIFTED TERMINATION ASSAY (STA), the presence of any of the 12 KRAS point mutations causes termination of complementary DNA chain synthesis during ");
			method.Append("amplification. Thus, complementary DNA strands are formed with lengths that are specific to the particular KRAS point mutation present. The products of the STA reaction were analyzed using ");
			method.Append("capillary electrophoresis to detect the presence of DNA fragments indicative of a KRAS mutation.  BRAF allele-specific PCR using fluorescently-labeled primers was then performed, which results ");
			method.Append("in the amplification of a 107-base fragment of DNA if the BRAF V600E mutation is present.  The products of the allele-specific PCR were then analyzed by capillary electrophoresis for the ");
			method.Append("presence of the 107-base DNA fragment indicative of a BRAF V600E mutation.");

			if (this.ResultIsPositive())
			{
				this.m_PanelSetOrder.PanelSetOrderCommentCollection.GetPanelSetOrderComment("report_method").Comment = method.ToString();
			}
			else if (this.ResultIsNegative())
			{
				this.m_PanelSetOrder.PanelSetOrderCommentCollection.GetPanelSetOrderComment("report_method").Comment = method.ToString();
			}
		}
	}
}
