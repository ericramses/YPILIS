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
            this.m_InterpretationFirstLine = "NO EVIDENCE OF MUTATION IN [NOTDETECTEDMUTATIONLIST] GENES";
            this.m_InterpretationSecondLine = "-THE LACK OF MUTATIONS IN THESE GENES IN COLORECTAL TUMORS SUGGESTS SENSITIVITY TO ANTI-EGFR ANTIBODIES.";
        }       
    }
}
