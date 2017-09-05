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

        public void SetResult(string hpv16Result, string hpv18Result)
        {            
            this.m_HPV16Result = hpv16Result;
            this.m_HPV18Result = hpv18Result;

            this.m_SquamousCellCarcinomaInterpretation = SquamousCellCarcinomaInterpretation;
            if (this.m_HPV16Result == "Positive")
            {
                this.m_SquamousCellCarcinomaInterpretation += " HPV 16 was detected in the sample by real time PCR.";
            }
            else
            {
                this.m_SquamousCellCarcinomaInterpretation += " HPV 16 was not detected in the sample by real time PCR.";
            }

            if (this.m_HPV18Result == "Positive")
            {
                this.m_SquamousCellCarcinomaInterpretation += " HPV 18/45 was detected.";
            }
            else
            {
                this.m_SquamousCellCarcinomaInterpretation += " HPV 18/45 was not detected.";
            }            
        }
	}
}
