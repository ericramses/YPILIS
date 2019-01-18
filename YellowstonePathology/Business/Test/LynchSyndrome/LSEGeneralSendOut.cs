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

            this.m_Result = "Loss of nuclear expression of MLH1, MSH2, MSH6, and/or PMS2 mismatch repair proteins.";
            this.m_Interpretation = "Results indicate mismatch repair deficiency, which may render the tumor responsive to PD-1 blockade therapy.  " +
                "As a subset of patients with MMR deficient prostate cancers have Lynch Syndrome, genetic counseling is recommended.";
            this.m_Method = IHCMethod;
            this.m_References = LSEGENReferences;
        }

        public override bool IncludeInIHCCollection(YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeIHC panelSetOrderLynchSyndromeIHC)
        {
            bool result = false;
            if (panelSetOrderLynchSyndromeIHC.MLH1Result == LSEResultEnum.Loss.ToString() ||
                panelSetOrderLynchSyndromeIHC.MSH2Result == LSEResultEnum.Loss.ToString() ||
                panelSetOrderLynchSyndromeIHC.MSH6Result == LSEResultEnum.Loss.ToString() ||
                panelSetOrderLynchSyndromeIHC.PMS2Result == LSEResultEnum.Loss.ToString())
            {
                result = true;
            }
            return result;
        }
    }
}
