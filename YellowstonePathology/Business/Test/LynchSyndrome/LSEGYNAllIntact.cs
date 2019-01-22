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

            this.m_Interpretation = "The results are compatible with a sporadic tumor and indicate a low risk for Lynch Syndrome.  " +
                "If clinical suspicion for Lynch Syndrome is high, microsatellite instability (MSI) testing by PCR is recommended.";
            this.m_Method = IHCMethod;
            this.m_References = LSEGYNReferences;
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
