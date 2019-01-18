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

            this.m_Result = "Loss of nuclear expression of MLH1 and PMS2 mismatch repair proteins." + Environment.NewLine + "MLH1 methylation detected.";
            this.m_Interpretation = "The results are compatible with a sporadic tumor and further genetic evaluation is not indicated.";

        }

        public override bool IsIHCMatch(IHCResult ihcResult)
        {
            bool result = false;
            if (ihcResult.MLH1Result.LSEResult == LSEResultEnum.Loss && ihcResult.PMS2Result.LSEResult == LSEResultEnum.Loss)
            {
                result = true;
            }
            return result;
        }
    }
}
