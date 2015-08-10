using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ALKForNSCLCByFISH
{
    public class ALKForNSCLCByFISHResultFactory
    {
        public static ALKForNSCLCByFISHResult GetResult(string resultCode)
        {
            ALKForNSCLCByFISHResult result = null;
            switch (resultCode)
            {
                case "ALKNSCLCFSHNGTV":                    
                    result = new ALKForNSCLCByFISHNegativeResult();
                    break;
                case "ALKNSCLCFSHNGTVWGAMP":                    
                    result = new ALKForNSCLCByFISHNegativeWithGeneAmplificationResult();                    
                    break;
                case "ALKNSCLCFSHPSTV":
                    result = new ALKForNSCLCByFISHPositiveResult();
                    break;
            }
            return result;
        }
    }
}
