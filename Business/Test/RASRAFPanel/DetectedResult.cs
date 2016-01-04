using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.RASRAFPanel
{
    public class DetectedResult : RASRAFPanelResult
    {
        public DetectedResult()
        {            
            this.m_InterpretationFirstLine = "-THERE ARE MUTATIONS IN THE [DETECTEDMUTATIONLIST] BUT NOT [NOTDETECTEDMUTATIONLIST] GENES.";
            this.m_InterpretationSecondLine = "THE PRESENCE OF [DETECTEDMUTATIONLIST] MUTATION IN COLORECTAL CANCER IS ASSOCIATED WITH RESISTANCE TO ANTI-EGFR THERAPY, BUT POSSIBLE SENSITIVITY " +
                "TO MEK INHIBITORS. [DETECTEDMUTATIONLIST] MUTATION IS ALSO REPORTED TO BE ASSOCIATED WITH SHORTER SURVIVAL.";
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
