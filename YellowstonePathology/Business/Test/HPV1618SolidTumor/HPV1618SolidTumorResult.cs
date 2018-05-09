using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.HPV1618SolidTumor
{
	public class HPV1618SolidTumorResult
	{
        public static string PositiveResult = "Positive";
        public static string NegativeResult = "Negative";
        public static string IndeterminateResult = "Indeterminate";		

        protected string m_ResultCode;
        protected string m_HPV16Result;
        protected string m_HPV18Result;        
        protected string m_SquamousCellCarcinomaInterpretation;

        protected string m_Method = "DNA was extracted from the patient's specimen using an automated method.  The Aptima HPV 16 18/45 genotype assay is an in vitro nucleic acid amplification test for the qualitative detection of E6/E7 viral messenger RNA(mRNA) of human papillomavirus(HPV) types 16, 18, and 45 in cervical specimens from women with Aptima HPV assay positive results.  This assay has further been validated as a laboratory developed test for the qualitative detection of HPV 16 18/45 in specimens from patients diagnosed with squamous cell carcinoma of head and neck.  The Aptima HPV 16 18/45 genotype assay can differentiate HPV 16 from HPV 18 and/or HPV 45, but does not differentiate between HPV 18 and HPV 45.";
        protected string m_References = "Highly Effective Detection of Human Papillomavirus 16 and 18 DNA by a Testing Algorithm Combining Broad-Spectrum and Type-Specific PCR J Clin Microbiol. 2006 September; 44(9): 3292-3298.";        

		public HPV1618SolidTumorResult()
		{
            
		}

		public string ResultCode
		{
			get { return this.m_ResultCode; }
		}

		public void SetResult(YellowstonePathology.Business.Test.HPV1618SolidTumor.HPV1618SolidTumorTestOrder panelSetOrder)
		{            			
            panelSetOrder.ResultCode = this.m_ResultCode;
            panelSetOrder.HPV16Result = this.m_HPV16Result;
            panelSetOrder.HPV18Result = this.m_HPV18Result;
            panelSetOrder.Method = this.m_Method;
            panelSetOrder.ReportReferences = this.m_References;            
            panelSetOrder.Interpretation = this.m_SquamousCellCarcinomaInterpretation;            
		}

		public virtual void AcceptResults(YellowstonePathology.Business.Test.HPV1618.PanelSetOrderHPV1618 panelSetOrder,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity)
		{
			YellowstonePathology.Business.Test.PanelOrder panelOrder = panelSetOrder.PanelOrderCollection.GetUnacceptedPanelOrder();
			panelOrder.AcceptResults();
			panelSetOrder.Accept();
		}		

		public void Clear(HPV1618SolidTumorTestOrder hpv1618SolidTumorTestOrder)
        {
            hpv1618SolidTumorTestOrder.Method = null;
            hpv1618SolidTumorTestOrder.ReportReferences = null;
            hpv1618SolidTumorTestOrder.Interpretation = null;
            hpv1618SolidTumorTestOrder.HPV16Result = null;
            hpv1618SolidTumorTestOrder.HPV18Result = null;
        }

		public virtual void FinalizeResults(YellowstonePathology.Business.Test.HPV1618SolidTumor.HPV1618SolidTumorTestOrder panelSetOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity, Business.Test.AccessionOrder accessionOrder)
		{
			panelSetOrder.Finish(accessionOrder);
		}

		public virtual void UnFinalizeResults(YellowstonePathology.Business.Test.HPV1618SolidTumor.HPV1618SolidTumorTestOrder panelSetOrder)
		{
			panelSetOrder.Unfinalize();
		}

		public static YellowstonePathology.Business.Rules.MethodResult IsOkToAccept(YellowstonePathology.Business.Test.HPV1618SolidTumor.HPV1618SolidTumorTestOrder panelSetOrder)
		{
			YellowstonePathology.Business.Rules.MethodResult result = panelSetOrder.IsOkToAccept();

			if (result.Success == true)
            {
				if (string.IsNullOrEmpty(panelSetOrder.ResultCode) == true)
				{
					result.Success = false;
					result.Message = "The results cannot be accepted because there is no result.";
				}
            }
			return result;
		}

		public static YellowstonePathology.Business.Rules.MethodResult IsOkToSetResult(YellowstonePathology.Business.Test.HPV1618SolidTumor.HPV1618SolidTumorTestOrder panelSetOrder)
		{
			YellowstonePathology.Business.Rules.MethodResult result = new Rules.MethodResult();

			if (string.IsNullOrEmpty(panelSetOrder.Indication) == true)
			{
				result.Success = false;
				result.Message = "You must choose an indication first.";
			}
			else if (panelSetOrder.Accepted == true)
			{
				result.Success = false;
				result.Message = "The results cannot be set because they have already been accepted.";
			}
			return result;
		}
	}
}
