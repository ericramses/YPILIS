using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	public class LSEGYNMLH1PMS2Loss : LSERule
    {
        public LSEGYNMLH1PMS2Loss()
		{
            this.m_Indication = LSEType.GYN;
			this.m_MLH1Result = LSEResultEnum.Loss;
			this.m_MSH2Result = LSEResultEnum.Intact;
			this.m_MSH6Result = LSEResultEnum.Intact;
			this.m_PMS2Result = LSEResultEnum.Loss;

            this.m_BRAFResult = TestResult.NotApplicable;
            this.m_MethResult = TestResult.Detected;
            this.m_BRAFRequired = false;
            this.m_MethRequired = true;
            this.m_Result = "Loss of nuclear expression of MLH1 and PMS2 mismatch repair proteins." + Environment.NewLine + "MLH1 methylation detected.";
            this.m_Interpretation = "The results are compatible with a sporadic tumor and further genetic evaluation is not indicated.";
        }

        public override void SetResults(AccessionOrder accessionOrder, YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation panelSetOrderLynchSyndromEvaluation)
        {
			YellowstonePathology.Business.Test.LynchSyndrome.MLH1MethylationAnalysisTest panelSetMLH1 = new YellowstonePathology.Business.Test.LynchSyndrome.MLH1MethylationAnalysisTest();
			YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderMLH1MethylationAnalysis panelSetOrderMLH1MethylationAnalysis = (YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderMLH1MethylationAnalysis)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetMLH1.PanelSetId, panelSetOrderLynchSyndromEvaluation.OrderedOnId, true);

            panelSetOrderLynchSyndromEvaluation.Interpretation = this.m_Interpretation;
            panelSetOrderLynchSyndromEvaluation.Result = this.m_Result;
            panelSetOrderLynchSyndromEvaluation.ReflexToBRAFMeth = this.m_MethRequired;
            panelSetOrderLynchSyndromEvaluation.Method = "IHC: " + IHCMethod + Environment.NewLine + Environment.NewLine + "MLH1: " + panelSetOrderMLH1MethylationAnalysis.Method;
            panelSetOrderLynchSyndromEvaluation.ReportReferences = "IHC: " + LSEGYNReferences + Environment.NewLine + Environment.NewLine + "MLH1: " + panelSetOrderMLH1MethylationAnalysis.ReportReferences;
        }
	}
}
