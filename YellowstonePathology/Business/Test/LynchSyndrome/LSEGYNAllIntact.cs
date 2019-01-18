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

            this.m_Result = "Intact nuclear expression of MLH1, MSH2, MSH6, and PMS2 mismatch repair proteins.";
            this.m_Interpretation = "The results are compatible with a sporadic tumor and indicate a low risk for Lynch Syndrome.  " +
                "If clinical suspicion for Lynch Syndrome is high, microsatellite instability (MSI) testing by PCR is recommended.";
            this.m_Method = IHCMethod;
            this.m_References = LSEGYNReferences;
		}

        public override bool IncludeInIHCCollection(YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeIHC panelSetOrderLynchSyndromeIHC)
        {
            bool result = false;
            if (panelSetOrderLynchSyndromeIHC.MLH1Result == LSEResultEnum.Intact.ToString() &&
                panelSetOrderLynchSyndromeIHC.MSH2Result == LSEResultEnum.Intact.ToString() &&
                panelSetOrderLynchSyndromeIHC.MSH6Result == LSEResultEnum.Intact.ToString() &&
                panelSetOrderLynchSyndromeIHC.PMS2Result == LSEResultEnum.Intact.ToString())
            {
                result = true;
            }
            return result;
        }
    }
}
