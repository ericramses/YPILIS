using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	public class LSEGeneralAllIntact : LSERule
    {

		public LSEGeneralAllIntact()
		{
            this.m_ResultName = "All Intact";
            this.m_Indication = LSEType.GENERAL;

            this.m_Result = "Intact nuclear expression of MLH1, MSH2, MSH6, and PMS2 mismatch repair proteins.";
            this.m_Interpretation = "Mismatch repair protein expression is intact, indicating that the tumor is unlikely to respond to PD-1 blockade therapy.";
            this.m_Method = IHCMethod;
            this.m_References = LSEGENReferences;
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
