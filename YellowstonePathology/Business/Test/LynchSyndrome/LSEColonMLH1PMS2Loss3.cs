using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
    public class LSEColonMLH1PMS2Loss3 : LSERule
    {
        public LSEColonMLH1PMS2Loss3()
        {
            this.m_Indication = LSEType.COLON;
            this.m_MLH1Result = LSEResultEnum.Loss;
            this.m_MSH2Result = LSEResultEnum.Intact;
            this.m_MSH6Result = LSEResultEnum.Intact;
            this.m_PMS2Result = LSEResultEnum.Loss;
            this.m_BRAFResult = TestResult.Detected;
            this.m_MethResult = TestResult.Detected;
            this.m_BRAFRequired = false;
            this.m_MethRequired = false;

            this.m_Result = "Loss of nuclear expression of MLH1 and PMS2 mismatch repair proteins." + Environment.NewLine +
                "BRAF mutation V600E DETECTED." + Environment.NewLine + "MLH1 promoter methylation DETECTED.";
            this.m_Interpretation = "The results are compatible with a sporadic tumor and further genetic evaluation is not indicated.";

            this.m_Method = IHCMethod;
            this.m_References = LSEColonReferences;
        }
    }
}
