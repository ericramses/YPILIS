using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	public class LSEGeneralResult1 : LSEResult
	{

		public LSEGeneralResult1()
		{
            this.m_Indication = LSEType.GENERAL;
            this.m_MLH1Result = LSEResultEnum.Intact;
            this.m_MSH2Result = LSEResultEnum.Intact;
            this.m_MSH6Result = LSEResultEnum.Intact;
            this.m_PMS2Result = LSEResultEnum.Intact;
            this.m_BRAFIsIndicated = false;
			this.m_Interpretation = "Intact nuclear expression of MLH1, MSH2, MSH6, and PMS2 mismatch repair proteins.";
            this.m_Comment = "Mismatch repair protein expression is intact, indicating that the tumor is unlikely to respond to PD-1 blockade therapy.";
            this.m_Method = IHCMethod;
            this.m_References = LSEGENReferences;
		}

        public override void SetResults(AccessionOrder accessionOrder, PanelSetOrderLynchSyndromeEvaluation panelSetOrderLynchSyndromEvaluation)
        {
            panelSetOrderLynchSyndromEvaluation.Interpretation = this.m_Interpretation;
            panelSetOrderLynchSyndromEvaluation.Comment = this.m_Comment;
            panelSetOrderLynchSyndromEvaluation.BRAFIsIndicated = this.m_BRAFIsIndicated;
            panelSetOrderLynchSyndromEvaluation.Method = this.m_Method;
            panelSetOrderLynchSyndromEvaluation.ReportReferences = this.m_References;
        }
    }
}
