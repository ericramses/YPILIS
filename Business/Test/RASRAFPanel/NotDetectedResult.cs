using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.RASRAFPanel
{
    public class NotDetectedResult : RASRAFPanelResult
    {
        public NotDetectedResult()
        {            
            this.m_InterpretationFirstLine = "NO EVIDENCE OF MUTATION IN THE [NOTDETECTEDMUTATIONLIST] GENES";
            this.m_InterpretationSecondLine = "-THE LACK OF MUTATIONS IN THESE GENES IN A COLORECTAL TUMORS SUGGESTS SENSITIVITY TO ANTI-EGFR ANTIBODIES.";
        }

        public override void SetResults(RASRAFPanelTestOrder testOrder, ResultCollection resultCollection)
        {
            StringBuilder interpretation = new StringBuilder();
            interpretation.AppendLine(this.m_InterpretationFirstLine);
            interpretation.AppendLine();
            interpretation.AppendLine(this.m_InterpretationSecondLine);
            interpretation.AppendLine(this.m_InterpretationBody);

            this.m_InterpretationFirstLine = this.m_InterpretationFirstLine.Replace("[DETECTEDMUTATIONLIST]", resultCollection.GetNotDetectedListString());
            this.m_InterpretationSecondLine = this.m_InterpretationSecondLine.Replace("[DETECTEDMUTATIONLIST]", resultCollection.GetDetectedListString());

            testOrder.Interpretation = interpretation.ToString();
            base.SetResults(testOrder, resultCollection);
        }
    }
}
