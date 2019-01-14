using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	public class LSEGeneralResult1 : LSERule
    {

		public LSEGeneralResult1()
		{
            this.m_Indication = LSEType.GENERAL;
            this.m_MLH1Result = LSEResultEnum.Intact;
            this.m_MSH2Result = LSEResultEnum.Intact;
            this.m_MSH6Result = LSEResultEnum.Intact;
            this.m_PMS2Result = LSEResultEnum.Intact;

            this.m_BRAFResult = TestResult.NotApplicable;
            this.m_MethResult = TestResult.NotApplicable;
            this.m_Result = "Mismatch repair protein expression is intact, indicating that the tumor is unlikely to respond to PD-1 blockade therapy.";
            this.m_Method = IHCMethod;
            this.m_References = LSEGENReferences;
		}

        public override void SetResults(AccessionOrder accessionOrder, PanelSetOrderLynchSyndromeEvaluation panelSetOrderLynchSyndromEvaluation)
        {
            throw new Exception("needs work");
            /*
            panelSetOrderLynchSyndromEvaluation.Interpretation = this.m_Interpretation;
            panelSetOrderLynchSyndromEvaluation.Result = this.m_Result;
            panelSetOrderLynchSyndromEvaluation.ReflexToBRAFMeth = this.m_ReflexToBRAFMeth;
            panelSetOrderLynchSyndromEvaluation.Method = this.m_Method;
            panelSetOrderLynchSyndromEvaluation.ReportReferences = this.m_References;
            */
        }

        public override bool IsIHCMatch(IHCResult ihcResult)
        {
            bool result = false;
            if (ihcResult.MLH1Result.LSEResult == LSEResultEnum.Intact &&
                ihcResult.MSH2Result.LSEResult == LSEResultEnum.Intact &&
                ihcResult.MSH6Result.LSEResult == LSEResultEnum.Intact &&
                ihcResult.PMS2Result.LSEResult == LSEResultEnum.Intact)
            {
                result = true;
            }

            return result;
        }
    }
}
