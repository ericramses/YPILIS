using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	public class LSEGeneralResult2 : LSEResult
	{
		public LSEGeneralResult2()
		{
            this.m_Indication = LSEType.GENERAL;
            this.m_MLH1Result = LSEResultEnum.AnyLoss;
            this.m_MSH2Result = LSEResultEnum.AnyLoss;
            this.m_MSH6Result = LSEResultEnum.AnyLoss;
            this.m_PMS2Result = LSEResultEnum.AnyLoss;
            this.m_BRAFIsIndicated = false;
            this.m_Comment = "Results indicate mismatch repair deficiency, which may render the tumor responsive to PD-1 blockade therapy.  As a subset of patients with MMR deficient prostate cancers have Lynch Syndrome, genetic counseling is recommended.";
            this.m_Method = IHCMethod;
            this.m_References = LSEGENReferences;        
		}

        public override void SetResultsV2()
        {
            this.m_Interpretation = this.BuildLossInterpretation();
        }

        public override void SetResults(AccessionOrder accessionOrder, PanelSetOrderLynchSyndromeEvaluation panelSetOrderLynchSyndromEvaluation)
        {
            panelSetOrderLynchSyndromEvaluation.Interpretation = this.BuildLossInterpretation();
            panelSetOrderLynchSyndromEvaluation.Comment = this.m_Comment;
            panelSetOrderLynchSyndromEvaluation.BRAFIsIndicated = this.m_BRAFIsIndicated;
            panelSetOrderLynchSyndromEvaluation.Method = this.m_Method;
            panelSetOrderLynchSyndromEvaluation.ReportReferences = this.m_References;
        }
    }
}
