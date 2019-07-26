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
        public static string DetectedResult = "Detected";
        public static string NotDetectedResult = "Not Detected";
        public static string IndeterminateResult = "Indeterminate";		

        protected string m_ResultCode;
        protected string m_HPV6Result;
        protected string m_HPV16Result;
        protected string m_HPV18Result;
        protected string m_HPV31Result;
        protected string m_HPV33Result;
        protected string m_HPV45Result;
        protected string m_HPV58Result;
        protected string m_SquamousCellCarcinomaInterpretation;

        protected string m_Method = "HPV DNA Tissue testing utilizes type-specific primers for early protein genes (E5-E7). Six high-risk (HR) " +
            "types, 16, 18, 31, 33, 45, 58, and one low risk (LR) type, 6/11, are detected by fragment analysis, which covers 95% of " +
            "cancer-related strains.  This test has a limit of detection of 5-10% for detecting HPV subtypes out of total DNA.";
        protected string m_References = "1. A. Molijn et al. Molecular diagnosis of human papillomavirus (HPV) infections. Journal of Clinical " +
            "Virology 32S (2005) S43–S51" + Environment.NewLine + 
            "2. P. Boscolo-Rizzo et al. New insights into human papillomavirus-associated head and neck squamous cell carcinoma head and neck " +
            "squamous cell carcinoma. Acta Otorhinolaryngol Ital 2013;33:77-87";


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
            panelSetOrder.HPV6Result = this.m_HPV6Result;
            panelSetOrder.HPV16Result = this.m_HPV16Result;
            panelSetOrder.HPV18Result = this.m_HPV18Result;
            panelSetOrder.HPV31Result = this.m_HPV31Result;
            panelSetOrder.HPV33Result = this.m_HPV33Result;
            panelSetOrder.HPV45Result = this.m_HPV45Result;
            panelSetOrder.HPV58Result = this.m_HPV58Result;
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
            hpv1618SolidTumorTestOrder.HPV6Result = null;
            hpv1618SolidTumorTestOrder.HPV16Result = null;
            hpv1618SolidTumorTestOrder.HPV18Result = null;
            hpv1618SolidTumorTestOrder.HPV31Result = null;
            hpv1618SolidTumorTestOrder.HPV33Result = null;
            hpv1618SolidTumorTestOrder.HPV45Result = null;
            hpv1618SolidTumorTestOrder.HPV58Result = null;
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
