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
            this.m_InterpretationFirstLine = "-THERE ARE MUTATIONS IN [DETECTEDMUTATIONLIST] BUT NOT [NOTDETECTEDMUTATIONLIST] GENES.";
            this.m_InterpretationSecondLine = "THE PRESENCE OF [DETECTEDMUTATIONLIST] MUTATION IN COLORECTAL CANCER IS ASSOCIATED WITH RESISTANCE TO ANTI-EGFR THERAPY, BUT POSSIBLE SENSITIVITY " +
                "TO MEK INHIBITORS. [DETECTEDMUTATIONLIST] MUTATION IS ALSO REPORTED TO BE ASSOCIATED WITH SHORTER SURVIVAL.";
        }       
    }
}
