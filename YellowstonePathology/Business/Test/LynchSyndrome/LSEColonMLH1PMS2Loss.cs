using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
    public class LSEColonMLH1PMS2Loss : LSERule
    {
        public LSEColonMLH1PMS2Loss()
        {
            this.m_Indication = LSEType.COLON;
            this.m_MLH1Result = LSEResultEnum.Loss;
            this.m_MSH2Result = LSEResultEnum.Intact;
            this.m_MSH6Result = LSEResultEnum.Intact;
            this.m_PMS2Result = LSEResultEnum.Loss;
            this.m_BRAFResult = TestResult.Detected;
            this.m_MethResult = TestResult.NotApplicable;
            this.m_BRAFRequired = true;
            this.m_MethRequired = false;

            this.m_Result = "Loss of nuclear expression of MLH1 and PMS2 mismatch repair proteins." + Environment.NewLine + "BRAF mutation V600E detected.";
            this.m_Interpretation = "BRAF mutation is present in the majority of MSI-high sporadic colorectal cancers. These cancers are MSI " +
                "high due to MLH1 methylation.  MSI-high colorectal tumors have been found to respond to immunotherapy.  As BRAF mutation is " +
                "rarely found in Lynch-related colorectal cancers, further genetic evaluation is not indicated.";
            this.m_Method = IHCBRAFMethod;
            this.m_References = LSEColonReferences;
        }
    }
}
