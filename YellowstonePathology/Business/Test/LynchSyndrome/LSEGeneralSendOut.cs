using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
    public class LSEGeneralSendOut : LSERule
    {
        public LSEGeneralSendOut()
        {
            this.m_ResultName = "Further testing";
            this.m_Indication = LSEType.GENERAL;
            this.m_BRAFResult = TestResult.NotApplicable;
            this.m_MethResult = TestResult.NotApplicable;
            this.m_BRAFRequired = false;
            this.m_MethRequired = false;
            this.m_Result = "Loss of nuclear expression of MLH1, MSH2, MSH6, and/or PMS2 mismatch repair proteins.";
            this.m_Interpretation = "Results indicate mismatch repair deficiency, which may render the tumor responsive to PD-1 blockade therapy.  " +
                "As a subset of patients with MMR deficient prostate cancers have Lynch Syndrome, genetic counseling is recommended.";
            this.m_Method = IHCMethod;
            this.m_References = LSEGENReferences;
        }

        protected override bool CanMatchIHC(IHCResult ihcResult)
        {
            bool result = false;
            if (ihcResult.MLH1Result.LSEResult == LSEResultEnum.Loss ||
                ihcResult.MSH2Result.LSEResult == LSEResultEnum.Loss ||
                ihcResult.MSH6Result.LSEResult == LSEResultEnum.Loss ||
                ihcResult.PMS2Result.LSEResult == LSEResultEnum.Loss)
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
