using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
    class LSEColonMLH1Loss1 : LSERule
    {
        public LSEColonMLH1Loss1()
        {
            this.m_Indication = LSEType.COLON;
            this.m_MLH1Result = LSEResultEnum.Loss;
            this.m_MSH2Result = LSEResultEnum.Intact;
            this.m_MSH6Result = LSEResultEnum.Intact;
            this.m_PMS2Result = LSEResultEnum.Intact;

            this.m_BRAFResult = TestResult.NotDetected;
            this.m_MethResult = TestResult.Detected;
            this.m_BRAFRequired = true;
            this.m_MethRequired = true;
            this.m_Result = "Loss of nuclear expression of MLH1 mismatch repair proteins." + Environment.NewLine +
                "BRAF mutation V600E NOT DETECTED." + Environment.NewLine + "MLH1 promoter methylation DETECTED.";
            this.m_Interpretation = "The results are compatible with a sporadic tumor and further genetic evaluation is not indicated unless " +
                "there is high clinical suspicion for Lynch Syndrome.";
            this.m_Method = IHCMethod;
            this.m_References = LSEColonReferences;
        }
    }
}
