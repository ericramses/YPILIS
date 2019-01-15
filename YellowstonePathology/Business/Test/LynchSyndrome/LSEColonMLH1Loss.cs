using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
    class LSEColonMLH1Loss : LSERule
    {
        public LSEColonMLH1Loss()
        {
            this.m_Indication = LSEType.COLON;
            this.m_MLH1Result = LSEResultEnum.Loss;
            this.m_MSH2Result = LSEResultEnum.Intact;
            this.m_MSH6Result = LSEResultEnum.Intact;
            this.m_PMS2Result = LSEResultEnum.Intact;

            this.m_BRAFResult = TestResult.Detected;
            this.m_MethResult = TestResult.NotApplicable;
            this.m_BRAFRequired = true;
            this.m_MethRequired = false;
            this.m_Interpretation = "The results are compatible with a sporadic tumor and further genetic evaluation is not indicated.";
            this.m_Method = IHCMethod;
            this.m_References = LSEColonReferences;
        }
    }
}
