using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	public class LSEGeneralResult2 : LSERule
    {
		public LSEGeneralResult2()
		{
            this.m_Indication = LSEType.GENERAL;
            this.m_MLH1Result = LSEResultEnum.AnyLoss;
            this.m_MSH2Result = LSEResultEnum.AnyLoss;
            this.m_MSH6Result = LSEResultEnum.AnyLoss;
            this.m_PMS2Result = LSEResultEnum.AnyLoss;            

            this.m_Result = "Results indicate mismatch repair deficiency, which may render the tumor responsive to PD-1 blockade therapy.  As a subset of patients with MMR deficient prostate cancers have Lynch Syndrome, genetic counseling is recommended.";
            this.m_Method = IHCMethod;
            this.m_References = LSEGENReferences;        
		}

        public override void SetResultsV2(PanelSetOrderLynchSyndromeEvaluation psoLSE)        
        {
            this.m_Result = this.BuildLossResult();
        }

        public override void SetResults(AccessionOrder accessionOrder, PanelSetOrderLynchSyndromeEvaluation panelSetOrderLynchSyndromEvaluation)
        {
            throw new Exception("needs workd");
            /*
            panelSetOrderLynchSyndromEvaluation.Interpretation = this.BuildLossInterpretation();
            panelSetOrderLynchSyndromEvaluation.Result = this.m_Result;
            panelSetOrderLynchSyndromEvaluation.ReflexToBRAFMeth = this.m_ReflexToBRAFMeth;
            panelSetOrderLynchSyndromEvaluation.Method = this.m_Method;
            panelSetOrderLynchSyndromEvaluation.ReportReferences = this.m_References;
            */
        }
    }
}
