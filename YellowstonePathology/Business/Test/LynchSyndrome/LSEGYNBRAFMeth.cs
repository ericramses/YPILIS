using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
    public class LSEGYNBRAFMeth : LSERule
    {
        public LSEGYNBRAFMeth()
        {
            this.m_ResultName = "Reflex to BRAF/Meth";
            this.m_Indication = LSEType.GYN;
            this.m_BRAFResult = TestResult.NotApplicable;
            this.m_MethResult = TestResult.Detected;
            this.m_BRAFRequired = false;
            this.m_MethRequired = true;
            this.m_Result = "Loss of nuclear expression of MLH1 and PMS2 mismatch repair proteins." + Environment.NewLine + "MLH1 methylation detected.";
            this.m_Interpretation = "The results are compatible with a sporadic tumor and further genetic evaluation is not indicated.";

        }

        protected override bool CanMatchIHC(IHCResult ihcResult)
        {
            bool result = false;
            if (ihcResult.MLH1Result.LSEResult == LSEResultEnum.Loss && ihcResult.PMS2Result.LSEResult == LSEResultEnum.Loss)
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
            return methResult == TestResult.Detected;
        }
    }
}
