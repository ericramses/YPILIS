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

            this.m_Interpretation = "Mismatch repair protein expression is intact, indicating that the tumor is unlikely to respond to PD-1 blockade therapy.";
            this.m_Method = IHCMethod;
            this.m_References = LSEGENReferences;
		}

        public override bool IncludeInIHCCollection(YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeIHC panelSetOrderLynchSyndromeIHC)
        {
            bool result = false;
            LSEIHCResultIntactExpression intactExpression = new LSEIHCResultIntactExpression();
            if (panelSetOrderLynchSyndromeIHC.MLH1Result == intactExpression.Description &&
                panelSetOrderLynchSyndromeIHC.MSH2Result == intactExpression.Description &&
                panelSetOrderLynchSyndromeIHC.MSH6Result == intactExpression.Description &&
                panelSetOrderLynchSyndromeIHC.PMS2Result == intactExpression.Description)
            {
                result = true;
            }
            return result;
        }

        public override void SetResults(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation panelSetOrderLynchSyndromeEvaluation)
        {
            base.SetResults(accessionOrder, panelSetOrderLynchSyndromeEvaluation);
            panelSetOrderLynchSyndromeEvaluation.Result = LSERule.IHCAllIntactResult;
        }
    }
}
