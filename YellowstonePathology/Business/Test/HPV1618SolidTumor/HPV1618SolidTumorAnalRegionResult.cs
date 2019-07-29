using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.HPV1618SolidTumor
{
	public class HPV1618SolidTumorAnalRegionResult : HPV1618SolidTumorResult
	{
        public static string SquamousCellCarcinomaInterpretation = "Human papillomavirus (HPV) infection strongly correlates with the development " +
            "of anal intraepithelial neoplasias and carcinomas.  A recent study showed high-risk HPV virus types were detected in progressively " +
            "greater number of anal intraepithelial lesions from 56% in low grade to 88% in high grade. Type 16 was the prevalent subtype and was " +
            "noted in 28% of low grade and 68% of high-grade lesions.  A subset of these that were associated with type 16 or 18 showed progression, " +
            "whereas those associated with non-16/18 subtypes regressed, thereby raising the possibility of conservative management for these " +
            "lesions.";

        public HPV1618SolidTumorAnalRegionResult()
        {
            this.m_ResultCode = "HPV1618ANLRGN";
        }

        public override void SetResult(YellowstonePathology.Business.Test.HPV1618SolidTumor.HPV1618SolidTumorTestOrder panelSetOrder)
        {
            panelSetOrder.ResultCode = this.m_ResultCode;
            if(panelSetOrder.HPVDNAResult == HPV1618SolidTumorResult.DetectedResult)
            {
                this.m_SquamousCellCarcinomaInterpretation = SquamousCellCarcinomaInterpretation;

                if (this.m_HPV16Result == HPV1618SolidTumorResult.DetectedResult)
                {
                    this.m_SquamousCellCarcinomaInterpretation += " HPV 16 was detected in the sample by real time PCR.";
                }

                if (this.m_HPV18Result == HPV1618SolidTumorResult.DetectedResult)
                {
                    this.m_SquamousCellCarcinomaInterpretation += " HPV 18/45 was detected.";
                }
            }
            else
            {
                this.m_SquamousCellCarcinomaInterpretation = SquamousCellCarcinomaInterpretation;
                this.m_SquamousCellCarcinomaInterpretation += " HPV 16 was not detected in the sample by real time PCR.";
                this.m_SquamousCellCarcinomaInterpretation += " HPV 18/45 was not detected.";
            }

            panelSetOrder.Method = this.m_Method;
            panelSetOrder.ReportReferences = this.m_References;
            panelSetOrder.Interpretation = this.m_SquamousCellCarcinomaInterpretation;
        }

        public void SetNotDetectedResult(YellowstonePathology.Business.Test.HPV1618SolidTumor.HPV1618SolidTumorTestOrder panelSetOrder)
        {
            panelSetOrder.HPV6Result = HPV1618SolidTumorResult.NotDetectedResult;
            panelSetOrder.HPV16Result = HPV1618SolidTumorResult.NotDetectedResult;
            panelSetOrder.HPV18Result = HPV1618SolidTumorResult.NotDetectedResult;
            panelSetOrder.HPV31Result = HPV1618SolidTumorResult.NotDetectedResult;
            panelSetOrder.HPV33Result = HPV1618SolidTumorResult.NotDetectedResult;
            panelSetOrder.HPV45Result = HPV1618SolidTumorResult.NotDetectedResult;
            panelSetOrder.HPV58Result = HPV1618SolidTumorResult.NotDetectedResult;
            panelSetOrder.HPVDNAResult = HPV1618SolidTumorResult.NotDetectedResult;

            this.SetResult(panelSetOrder);
        }
	}
}
