using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.HPV1618
{
	public class HPV1618Result : YellowstonePathology.Business.Test.TestResult
	{
        public static string PositiveResult = "Positive";
        public static string NegativeResult = "Negative";
        public static string InvalidResult = "Invalid";
        public static string InvalidResultCode = "HPV1618NVLD";

        public static string Method = "DNA was extracted from the patient’s specimen using an automated method.  Real-time PCR amplification was " +
			"performed using primers and hydrolysis probes specific for HPV types 16 and 18. The beta-actin gene was used as an internal control. " +
			"The real-time PCR curves were analyzed to determine the presence of HPV types 16 and 18 in the specimen.";

        public static string References = "Highly Effective Detection of Human Papillomavirus 16 and 18 DNA by a Testing Algorithm Combining Broad-Spectrum " +
            "and Type-Specific PCR J Clin Microbiol. 2006 September; 44(9): 3292–3298.";

        protected string m_SquamousCellCarcinomaInterpretation;
        protected string m_HPV16Result;
        protected string m_HPV18Result;
        protected string m_m_HPV16ResultCode;
        protected string m_m_HPV18ResultCode;
        protected string m_Method;
        protected string m_References;

        public HPV1618Result()
		{
            this.m_Method = HPV1618Result.Method;
            this.m_References = HPV1618Result.References;
        }
	}
}
