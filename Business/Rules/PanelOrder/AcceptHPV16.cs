using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Rules.PanelOrder
{
	public class AcceptHPV16 : Accept
	{
		/*const string PositiveInterpretation = "About 30% of squamous cell carcinomas arising in the head and neck are driven by infection by " +
			"human papillomavirus (HPV) type 16.  These tumors are biologically and clinically distinct from non-HPV related tumors, typically " +
			"occurring in younger patients and without an association with alcohol or tobacco.   Multiple studies have shown that HPV-positive " +
			"tumors have a better prognosis; there is slower disease progression and improved survival with as much as a 60%-80% reduction in risk " +
			"of death due to the disease.  Further, there may be differences in the management of patients with an HPV-related malignancy.  This " +
			"patient was diagnosed with a squamous cell carcinoma of the head and neck and was therefore referred for testing for HPV type 16." +
			"\n\nDuring the PCR " +
			"reaction on this patient's sample there was increasing fluorescence detected in the pattern of a classic PCR curve.  The fluorescent " +
			"signal exceeded the threshold determined during test validation to be significant for the presence of HPV-16.  Therefore this patient's " +
			"tissue does contain HPV-16 and the tumor would be expected to have a better prognosis.  This finding may also have implications for " +
			"management of the disease.";*/

		const string PositiveInterpretation = "Approximately 30% of squamous cell carcinomas originating in the head and neck are driven by infection by " +
			"human papillomavirus type 16 (HPV16).  These tumors are biologically and clinically distinct from non-HPV related tumors, as they are typically " +
			"found in younger patients and are not associated with alcohol or tobacco use.  Multiple studies have demonstrated that HPV16-positive neoplasms " +
			"are associated with slower progression and thus improved overall survival, with as much as a 60 to 80% reduction in the risk of death due to disease.  " +
			"Furthermore, there may be differences in the clinical management of patients harboring an HPV16-related malignancy.\n\n" +
			"This patient was diagnosed with a squamous cell carcinoma of the head or neck and was therefore referred for HPV16 DNA testing.  During PCR analysis " +
			"of the patient’s sample, a fluorescence signal with a pattern and threshold indicative of the presence of HPV16 DNA was detected.  All controls, " +
			"including the sample’s internal control, reacted appropriately.  Therefore, HPV16 DNA is detected in the patient’s tissue and the tumor would be " +
			"expected to have a better prognosis.  This finding may also have implications for management of the disease.";


		const string NegativeInterpretation = "Approximately 30% of squamous cell carcinomas originating in the head and neck are driven by infection by human papillomavirus type 16 (HPV16).  These tumors are biologically and clinically distinct from non-HPV related tumors, as they are typically found in younger patients and are not associated with alcohol or tobacco use.  Multiple studies have demonstrated that HPV16-positive neoplasms are associated with slower progression and thus improved overall survival, with as much as a 60 to 80% reduction in the risk of death due to disease.  Furthermore, there may be differences in the clinical management of patients harboring an HPV16-related malignancy. " +
            "This patient was diagnosed with a squamous cell carcinoma of the head or neck and was therefore referred for HPV16 DNA testing.  During PCR analysis of the patient’s sample, a fluorescence signal with a pattern and threshold indicative of the presence of HPV16 DNA was not detected.  All controls, including the sample’s internal control, reacted appropriately.  Therefore, HPV16 DNA is not detected in the patient’s tissue.  Because this analysis only tests for the presence of HPV16 DNA, the presence of other high-risk HPV types cannot be excluded.";

		public AcceptHPV16()
        {                                                
            this.m_Rule.ActionList.Add(SetPositiveResult);
            this.m_Rule.ActionList.Add(SetNegativeResult);
            this.m_Rule.ActionList.Add(SetIndeterminateResult);
            this.m_Rule.ActionList.Add(base.AcceptPanel);
			this.m_Rule.ActionList.Add(SetPositiveInterpretation);
			this.m_Rule.ActionList.Add(SetNegativeInterpretation);
			this.m_Rule.ActionList.Add(SetIndeterminateInterpretation);
			this.m_Rule.ActionList.Add(SetComment);
			this.m_Rule.ActionList.Add(base.Save);
        }
        
        private void SetPositiveResult()
        {
            //If the test is Positive then Positive.
			var query = from items in this.m_PanelOrderBeingAccepted.TestOrderCollection
						where items.Result == "Positive"
						select items;
			if (query.Count<YellowstonePathology.Domain.Test.Model.TestOrder>() > 0)
			{
				//this.m_PanelSetOrder.PanelSetResultOrderCollection[0].Result = "POSITIVE for HPV-16 by PCR";
				this.m_PanelSetOrder.PanelSetResultOrderCollection[0].Result = "POSITIVE";
			}
        }

        private void SetNegativeResult()
        {            
            //If the test is Negative then the result is Negative
			var query = from items in this.m_PanelOrderBeingAccepted.TestOrderCollection
						where items.Result == "Negative"
						select items;
			if (query.Count<YellowstonePathology.Domain.Test.Model.TestOrder>() > 0)
			{
				//this.m_PanelSetOrder.PanelSetResultOrderCollection[0].Result = "NEGATIVE for HPV-16 by PCR";
				this.m_PanelSetOrder.PanelSetResultOrderCollection[0].Result = "NEGATIVE";
			}
        }

		private void SetIndeterminateResult()
        {
			//If the test is Indeterminate or QNS or No Result then the result is Indeterminate
			var query = from items in this.m_PanelOrderBeingAccepted.TestOrderCollection
						where items.Result == "QNS" || items.Result == "Indeterminate" || items.Result == "No Result"
						select items;
			if (query.Count<YellowstonePathology.Domain.Test.Model.TestOrder>() > 0)
			{
				this.m_PanelSetOrder.PanelSetResultOrderCollection[0].Result = "Indeterminate";
			}
        }

		private void SetPositiveInterpretation()
		{
			if (this.m_PanelSetOrder.PanelSetResultOrderCollection[0].Result == "POSITIVE")
				//this.m_PanelSetOrder.PanelSetResultOrderCollection[0].Result == "POSITIVE for HPV-16 by PCR")
			{
				this.SetInterpretation(PositiveInterpretation);
			}
		}

		private void SetNegativeInterpretation()
		{
			if (this.m_PanelSetOrder.PanelSetResultOrderCollection[0].Result == "NEGATIVE")
				//this.m_PanelSetOrder.PanelSetResultOrderCollection[0].Result == "NEGATIVE for HPV-16 by PCR")
			{
				this.SetInterpretation(NegativeInterpretation);
			}
		}

		private void SetIndeterminateInterpretation()
		{
			if (this.m_PanelSetOrder.PanelSetResultOrderCollection[0].Result == "Indeterminate")
			{
				this.SetInterpretation(string.Empty);
			}
		}

		private void SetComment()
		{
			YellowstonePathology.Business.Test.PanelSetOrderComment panelSetOrderComment = null;
			var query = from psoc in this.m_PanelSetOrder.PanelSetOrderCommentCollection where psoc.CommentName == "Report Comment" select psoc;
			if (query.Count<YellowstonePathology.Business.Test.PanelSetOrderComment>() == 0)
			{
				panelSetOrderComment = this.m_PanelSetOrder.PanelSetOrderCommentCollection.Add(this.m_PanelSetOrder.ReportNo);
				panelSetOrderComment.ReportNo = this.m_PanelSetOrder.ReportNo;
				panelSetOrderComment.CommentName = "Report Comment";
				panelSetOrderComment.Comment = string.Empty;
			}
		}

		private void SetInterpretation(string interpretation)
		{
			YellowstonePathology.Business.Test.PanelSetOrderComment panelSetOrderComment = null;
			var query = from psoc in this.m_PanelSetOrder.PanelSetOrderCommentCollection where psoc.CommentName == "Report Interpretation" select psoc;
			if (query.Count<YellowstonePathology.Business.Test.PanelSetOrderComment>() > 0)
			{
				panelSetOrderComment = query.Single<YellowstonePathology.Business.Test.PanelSetOrderComment>();
			}
			else
			{
				panelSetOrderComment = this.m_PanelSetOrder.PanelSetOrderCommentCollection.Add(this.m_PanelSetOrder.ReportNo);
				panelSetOrderComment.ReportNo = this.m_PanelSetOrder.ReportNo;
			}
			panelSetOrderComment.CommentName = "Report Interpretation";
			panelSetOrderComment.Comment = interpretation;
		}
	}
}
