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

        public override bool IncludeInIHCCollection(YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeIHC panelSetOrderLynchSyndromeIHC)
        {
            bool result = false;
            if (panelSetOrderLynchSyndromeIHC.MLH1Result == LSEResultEnum.Intact.ToString() &&
                panelSetOrderLynchSyndromeIHC.MSH2Result == LSEResultEnum.Loss.ToString() &&
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
