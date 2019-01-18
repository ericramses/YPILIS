using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	public class LSEGYNAllIntact : LSERule
    {
        public LSEGYNAllIntact()
		{
            this.m_ResultName = "All Intact";
            this.m_Indication = LSEType.GYN;

            this.m_BRAFResult = TestResult.NotApplicable;
            this.m_MethResult = TestResult.NotApplicable;
            this.m_BRAFRequired = false;
            this.m_MethRequired = false;
            this.m_Result = "Intact nuclear expression of MLH1, MSH2, MSH6, and PMS2 mismatch repair proteins.";
            this.m_Interpretation = "The results are compatible with a sporadic tumor and indicate a low risk for Lynch Syndrome.  " +
                "If clinical suspicion for Lynch Syndrome is high, microsatellite instability (MSI) testing by PCR is recommended.";
            this.m_Method = IHCMethod;
            this.m_References = LSEGYNReferences;
		}

        protected override bool CanMatchIHC(IHCResult ihcResult)
        {
            bool result = false;
            if (ihcResult.MLH1Result.LSEResult == LSEResultEnum.Intact &&
                ihcResult.MSH2Result.LSEResult == LSEResultEnum.Intact &&
                ihcResult.MSH6Result.LSEResult == LSEResultEnum.Intact &&
                ihcResult.PMS2Result.LSEResult == LSEResultEnum.Intact)
            {
                result = true;
            }
            return result;
        }

        protected override bool CanMatchBRAF(string brafResult)
        {
            return brafResult == TestResult.NotApplicable;
        }

        protected override bool CanMatchMeth(string methResult)
        {
            return methResult == TestResult.NotApplicable;
        }
    }
}
