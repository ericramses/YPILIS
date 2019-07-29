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
        protected string m_HPVDNAResult;
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

		public virtual void SetResult(YellowstonePathology.Business.Test.HPV1618SolidTumor.HPV1618SolidTumorTestOrder panelSetOrder)
		{
		}

        private static void SetResultCode(YellowstonePathology.Business.Test.HPV1618SolidTumor.HPV1618SolidTumorTestOrder panelSetOrder)
        {
            if (panelSetOrder.ResultCode == null)
            {
                if (panelSetOrder.Indication == Business.Test.HPV1618SolidTumor.HPV1618SolidTumorIndication.SquamousCellCarcinomaAnalRegion)
                {
                    panelSetOrder.ResultCode = "HPV1618ANLRGN";
                }
                else
                {
                    panelSetOrder.ResultCode = "HPV1618D";
                }
            }
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
            hpv1618SolidTumorTestOrder.ResultCode = null;
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
            hpv1618SolidTumorTestOrder.HPVDNAResult = null;
        }

        public void FinalizeResults(YellowstonePathology.Business.Test.HPV1618SolidTumor.HPV1618SolidTumorTestOrder panelSetOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity, Business.Test.AccessionOrder accessionOrder)
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
                    SetResultCode(panelSetOrder);
				}
            }

            if (result.Success == true)
            {
                string message = string.Empty;
                if (string.IsNullOrEmpty(panelSetOrder.HPVDNAResult) == true)
                {
                    result.Success = false;
                    message = "HPV DNA, ";
                }
                if (string.IsNullOrEmpty(panelSetOrder.HPV6Result) == true)
                {
                    result.Success = false;
                    message += "HPV-6, ";
                }
                if (string.IsNullOrEmpty(panelSetOrder.HPV16Result) == true)
                {
                    result.Success = false;
                    message += "HPV-16, ";
                }
                if (string.IsNullOrEmpty(panelSetOrder.HPV18Result) == true)
                {
                    result.Success = false;
                    message += "HPV-18, ";
                }
                if (string.IsNullOrEmpty(panelSetOrder.HPV31Result) == true)
                {
                    result.Success = false;
                    message += "HPV-31, ";
                }
                if (string.IsNullOrEmpty(panelSetOrder.HPV33Result) == true)
                {
                    result.Success = false;
                    message += "HPV-33, ";
                }
                if (string.IsNullOrEmpty(panelSetOrder.HPV45Result) == true)
                {
                    result.Success = false;
                    message += "HPV-45, ";
                }
                if (string.IsNullOrEmpty(panelSetOrder.HPV58Result) == true)
                {
                    result.Success = false;
                    message += "HPV-58, ";
                }

                if(message.Length > 0)
                {
                    message = message.Substring(0, message.Length - 2);
                    result.Message = "The results cannot be accepted because the " + message + " result/s need a value." + Environment.NewLine +
                        "Not Performed, Detected or Not Detected are acceptable values.";
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
