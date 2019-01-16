using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	public class LSEGeneralAnyLoss : LSERule
    {
		public LSEGeneralAnyLoss()
		{
            this.m_Indication = LSEType.GENERAL;
            this.m_MLH1Result = LSEResultEnum.AnyLoss;
            this.m_MSH2Result = LSEResultEnum.AnyLoss;
            this.m_MSH6Result = LSEResultEnum.AnyLoss;
            this.m_PMS2Result = LSEResultEnum.AnyLoss;

            this.m_BRAFResult = TestResult.NotApplicable;
            this.m_MethResult = TestResult.NotApplicable;
            this.m_BRAFRequired = false;
            this.m_MethRequired = false;
            this.m_Result = "Loss of nuclear expression of MLH1, MSH2, MSH6, and/or PMS2 mismatch repair proteins.";
            this.m_Interpretation = "Results indicate mismatch repair deficiency, which may render the tumor responsive to PD-1 blockade therapy.  " +
                "As a subset of patients with MMR deficient prostate cancers have Lynch Syndrome, genetic counseling is recommended.";
            this.m_Method = IHCMethod;
            this.m_References = LSEGENReferences;        
		}

        public override void SetResults(AccessionOrder accessionOrder, PanelSetOrderLynchSyndromeEvaluation panelSetOrderLynchSyndromEvaluation)
        {
            panelSetOrderLynchSyndromEvaluation.Interpretation = this.m_Interpretation;
            panelSetOrderLynchSyndromEvaluation.Result = this.BuildLossResult();
            panelSetOrderLynchSyndromEvaluation.ReflexToBRAFMeth = this.m_BRAFRequired;
            panelSetOrderLynchSyndromEvaluation.Method = this.m_Method;
            panelSetOrderLynchSyndromEvaluation.ReportReferences = this.m_References;
        }

        public override bool IsIHCMatch(IHCResult ihcResult)
        {
            bool result = false;
            if (ihcResult.MLH1Result.LSEResult == LSEResultEnum.Loss ||
                ihcResult.MSH2Result.LSEResult == LSEResultEnum.Loss ||
                ihcResult.MSH6Result.LSEResult == LSEResultEnum.Loss ||
                ihcResult.PMS2Result.LSEResult == LSEResultEnum.Loss)
            {
                result = true;
            }

            return result;
        }
    }
}
