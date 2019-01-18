using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
    public class LSEGYNSendOut : LSERule
    {
        public LSEGYNSendOut()
        {
            this.m_Indication = LSEType.GYN;
            this.m_ResultName = "Further testing";
            this.m_BRAFResult = TestResult.NotApplicable;
            this.m_MethResult = TestResult.NotApplicable;
            this.m_BRAFRequired = false;
            this.m_MethRequired = false;
            this.m_Result = "Loss of nuclear expression of PMS2 mismatch repair proteins.";
            this.m_Interpretation = "This staining pattern is highly suggestive of Lynch Syndrome and is associated with germline MSH2, EPCAM, " +
                "or MSH6 mutations.  Recommend genetic counseling and further evaluation.";

        }

        protected override bool CanMatchIHC(IHCResult ihcResult)
        {
            bool result = false;
            if ((ihcResult.MSH2Result.LSEResult == LSEResultEnum.Loss && ihcResult.MSH6Result.LSEResult == LSEResultEnum.Loss) ||
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
