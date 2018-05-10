using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	public class LSEProstateResult2 : LSEResult
	{
		public LSEProstateResult2()
		{            
            this.m_BRAFIsIndicated = false;
            this.m_Comment = "Results indicate mismatch repair deficiency, which may render the tumor responsive to pembrolizumab therapy.  As a subset of patients with MMR deficient prostate cancers have Lynch Syndrome, genetic counseling is recommended.";
            this.m_Method = IHCMethod;
            this.m_References = LSEPROSReferences;        
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
