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
            this.m_ResultName = "Send out for further testing";
            this.m_Indication = LSEType.GENERAL;

            this.m_Interpretation = "Results indicate mismatch repair deficiency, which may render the tumor responsive to PD-1 blockade therapy.  " +
                "As a subset of patients with MMR deficient prostate cancers have Lynch Syndrome, genetic counseling is recommended.";
            this.m_Method = IHCMethod;
            this.m_References = LSEGENReferences;
        }

        public override bool IncludeInIHCCollection(YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeIHC panelSetOrderLynchSyndromeIHC)
        {
            bool result = false;
            LSEIHCResultLossOfExpression lossExpression = new LSEIHCResultLossOfExpression();
            if (panelSetOrderLynchSyndromeIHC.MLH1Result == lossExpression.Description ||
                panelSetOrderLynchSyndromeIHC.MSH2Result == lossExpression.Description ||
                panelSetOrderLynchSyndromeIHC.MSH6Result == lossExpression.Description ||
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
