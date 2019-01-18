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

            this.m_Result = "Loss of nuclear expression of PMS2 mismatch repair proteins.";
            this.m_Interpretation = "This staining pattern is highly suggestive of Lynch Syndrome and is associated with germline MSH2, EPCAM, " +
                "or MSH6 mutations.  Recommend genetic counseling and further evaluation.";

        }

        public override bool IsIHCMatch(IHCResult ihcResult)
        {
            bool result = false;
            if ((ihcResult.MSH2Result.LSEResult == LSEResultEnum.Loss && ihcResult.MSH6Result.LSEResult == LSEResultEnum.Loss) ||
                (ihcResult.MLH1Result.LSEResult == LSEResultEnum.Intact && ihcResult.PMS2Result.LSEResult == LSEResultEnum.Loss))
            {
                result = true;
            }
            return result;
        }
    }
}
