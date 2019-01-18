using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
    public class LSEColonSendOut : LSERule
    {
        public LSEColonSendOut()
        {
            this.m_ResultName = "Further testing";
            this.m_Indication = LSEType.COLON;

            this.m_Result = "Not sure"; // "Intact nuclear expression of MLH1, MSH2, MSH6, and PMS2 mismatch repair proteins.";
            this.m_Interpretation = "Not sure"; //"The results are compatible with a sporadic tumor and indicate a low risk for Lynch Syndrome.  " +
                //"If clinical suspicion for Lynch Syndrome is high, microsatellite instability (MSI) testing by PCR is recommended. " +
                //"If MSI testing is desired, please contact Yellowstone Pathology with the request.";
            this.m_Method = IHCMethod;
            this.m_References = LSEColonReferences;
        }

        public override bool IncludeInIHCCollection(YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeIHC panelSetOrderLynchSyndromeIHC)
        {
            bool result = false;
            /*
            if (ihcResult.MSH2Result.LSEResult == LSEResultEnum.Loss ||
                (ihcResult.MSH2Result.LSEResult == LSEResultEnum.Loss && ihcResult.MSH6Result.LSEResult == LSEResultEnum.Loss) ||
                ihcResult.MSH6Result.LSEResult == LSEResultEnum.Loss ||
                (ihcResult.MLH1Result.LSEResult == LSEResultEnum.Intact && ihcResult.PMS2Result.LSEResult == LSEResultEnum.Loss))
            {
                result = true;
            }
            */
            return result;
        }
    }
}
