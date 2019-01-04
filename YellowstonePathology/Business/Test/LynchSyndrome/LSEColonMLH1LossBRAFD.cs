using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
    class LSEColonMLH1LossBRAFD : LSERule
    {
        public LSEColonMLH1LossBRAFD()
        {
            this.m_Indication = LSEType.COLON;
            this.m_MLH1Result = LSEResultEnum.Loss;
            this.m_MSH2Result = LSEResultEnum.Intact;
            this.m_MSH6Result = LSEResultEnum.Intact;
            this.m_PMS2Result = LSEResultEnum.Intact;

            this.m_BRAFResult = LSEResultEnum.Detected;
            this.m_AdditionalTesting = LSERule.AdditionalTestingReflexBRAFMeth;
            this.m_Interpretation = "The results are compatible with a sporadic tumor and further genetic evaluation is not indicated.";
            this.m_Method = IHCMethod;
            this.m_References = LSEColonReferences;
        }
    }
}
