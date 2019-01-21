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
            this.m_ResultName = "Send out for further testing";

            this.m_Interpretation = "This staining pattern is highly suggestive of Lynch Syndrome   Recommend genetic counseling and further evaluation.";
            this.m_Method = IHCMethod;
            this.m_References = LSEGYNReferences;
        }

        public override bool IncludeInIHCCollection(YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeIHC panelSetOrderLynchSyndromeIHC)
        {
            bool result = false;
            LSEIHCResultIntactExpression intactExpression = new LSEIHCResultIntactExpression();
            LSEIHCResultLossOfExpression lossExpression = new LSEIHCResultLossOfExpression();
            if (panelSetOrderLynchSyndromeIHC.MLH1Result == intactExpression.Description &&
                panelSetOrderLynchSyndromeIHC.MSH2Result == lossExpression.Description &&
                panelSetOrderLynchSyndromeIHC.MSH6Result == lossExpression.Description &&
                panelSetOrderLynchSyndromeIHC.PMS2Result == intactExpression.Description)
            {
                result = true;
            }
            else if (panelSetOrderLynchSyndromeIHC.MLH1Result == intactExpression.Description &&
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
            base.SetResults(accessionOrder, panelSetOrderLynchSyndromeEvaluation);
            panelSetOrderLynchSyndromeEvaluation.Result = this.BuildLossResult(accessionOrder, panelSetOrderLynchSyndromeEvaluation);
        }
    }
}
