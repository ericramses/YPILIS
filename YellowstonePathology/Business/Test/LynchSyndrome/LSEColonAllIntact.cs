using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	public class LSEColonAllIntact : LSERule
	{

		public LSEColonAllIntact()
		{
            this.m_ResultName = "All Intact";
            this.m_Indication = LSEType.COLON;

            this.m_Interpretation = "The results are compatible with a sporadic tumor and indicate a low risk for Lynch Syndrome.  " +
                "If clinical suspicion for Lynch Syndrome is high, microsatellite instability (MSI) testing by PCR is recommended. " +
                "If MSI testing is desired, please contact Yellowstone Pathology with the request.";
            this.m_Method = IHCMethod;
            this.m_References = LSEColonReferences;
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
