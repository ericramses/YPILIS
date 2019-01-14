using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	public class LSEGYNResult3 : LSERule
    {
        public LSEGYNResult3()
		{
            this.m_Indication = "LSEGYN";
            this.m_MLH1Result = LSEResultEnum.Intact;
			this.m_MSH2Result = LSEResultEnum.Loss;
			this.m_MSH6Result = LSEResultEnum.Loss;
			this.m_PMS2Result = LSEResultEnum.Intact;

            this.m_BRAFResult = TestResult.NotApplicable;
            this.m_MethResult = TestResult.NotApplicable;
            this.m_Result = "This staining pattern is highly suggestive of Lynch Syndrome and is associated with germline MSH2, EPCAM, or MSH6 mutations.  Recommend genetic counseling and further evaluation.";
        }

		public override void SetResults(AccessionOrder accessionOrder, YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation panelSetOrderLynchSyndromEvaluation)
        {
            throw new Exception("needs workd");
            /*
            panelSetOrderLynchSyndromEvaluation.Interpretation = this.m_Interpretation;
            panelSetOrderLynchSyndromEvaluation.Result = this.m_Result;
            panelSetOrderLynchSyndromEvaluation.ReflexToBRAFMeth = this.m_ReflexToBRAFMeth;
            panelSetOrderLynchSyndromEvaluation.Method = "IHC: " + IHCMethod;
            panelSetOrderLynchSyndromEvaluation.ReportReferences = "IHC: " + LSEGYNReferences;
            */
        }
	}
}
