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
            if (panelSetOrderLynchSyndromeIHC.MLH1Result == LSEResultEnum.Intact.ToString() &&
                panelSetOrderLynchSyndromeIHC.MSH2Result == LSEResultEnum.Loss.ToString() &&
                panelSetOrderLynchSyndromeIHC.MSH6Result == LSEResultEnum.Intact.ToString() &&
                panelSetOrderLynchSyndromeIHC.PMS2Result == LSEResultEnum.Intact.ToString())
            {
                result = true;
            }
            else if (panelSetOrderLynchSyndromeIHC.MLH1Result == LSEResultEnum.Intact.ToString() &&
                panelSetOrderLynchSyndromeIHC.MSH2Result == LSEResultEnum.Loss.ToString() &&
                panelSetOrderLynchSyndromeIHC.MSH6Result == LSEResultEnum.Loss.ToString() &&
                panelSetOrderLynchSyndromeIHC.PMS2Result == LSEResultEnum.Intact.ToString())
            {
                result = true;
            }
            else if (panelSetOrderLynchSyndromeIHC.MLH1Result == LSEResultEnum.Intact.ToString() &&
                panelSetOrderLynchSyndromeIHC.MSH2Result == LSEResultEnum.Intact.ToString() &&
                panelSetOrderLynchSyndromeIHC.MSH6Result == LSEResultEnum.Loss.ToString() &&
                panelSetOrderLynchSyndromeIHC.PMS2Result == LSEResultEnum.Intact.ToString())
            {
                result = true;
            }
            else if (panelSetOrderLynchSyndromeIHC.MLH1Result == LSEResultEnum.Intact.ToString() &&
                panelSetOrderLynchSyndromeIHC.MSH2Result == LSEResultEnum.Intact.ToString() &&
                panelSetOrderLynchSyndromeIHC.MSH6Result == LSEResultEnum.Intact.ToString() &&
                panelSetOrderLynchSyndromeIHC.PMS2Result == LSEResultEnum.Loss.ToString())
            {
                result = true;
            }
            return result;
        }
    }
}
