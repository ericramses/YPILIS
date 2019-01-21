using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
    public class LSEGYNBRAFMeth : LSERule
    {
        public LSEGYNBRAFMeth()
        {
            this.m_ResultName = "Reflex to BRAF/Meth";
            this.m_Indication = LSEType.GYN;

            this.m_Interpretation = "The results are compatible with a sporadic tumor and further genetic evaluation is not indicated.";
            this.m_References = LSEGYNReferences;
        }

        public override bool IncludeInIHCCollection(YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeIHC panelSetOrderLynchSyndromeIHC)
        {
            bool result = false;
            LSEIHCResultIntactExpression intactExpression = new LSEIHCResultIntactExpression();
            LSEIHCResultLossOfExpression lossExpression = new LSEIHCResultLossOfExpression();
            if (panelSetOrderLynchSyndromeIHC.MLH1Result == lossExpression.Description &&
                panelSetOrderLynchSyndromeIHC.MSH2Result == intactExpression.Description &&
                panelSetOrderLynchSyndromeIHC.MSH6Result == intactExpression.Description &&
                panelSetOrderLynchSyndromeIHC.PMS2Result == lossExpression.Description)
            {
                result = true;
            }
            return result;
        }

        public override void SetResults(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation panelSetOrderLynchSyndromeEvaluation)
        {
            this.BuildMethod(accessionOrder, panelSetOrderLynchSyndromeEvaluation);
            base.SetResults(accessionOrder, panelSetOrderLynchSyndromeEvaluation);
            string result = this.BuildLossResult(accessionOrder, panelSetOrderLynchSyndromeEvaluation);
            result += this.BuildBRAFResult(accessionOrder, panelSetOrderLynchSyndromeEvaluation);
            result += this.BuildMethResult(accessionOrder, panelSetOrderLynchSyndromeEvaluation);
            panelSetOrderLynchSyndromeEvaluation.Result = result;
        }
    }
}
