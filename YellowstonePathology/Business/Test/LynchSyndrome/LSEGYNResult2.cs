using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	public class LSEGYNResult2 : LSERule
    {
        public LSEGYNResult2()
		{
            this.m_Indication = "LSEGYN";
            this.m_MLH1Result = LSEResultEnum.Intact;
			this.m_MSH2Result = LSEResultEnum.Intact;
			this.m_MSH6Result = LSEResultEnum.Intact;
			this.m_PMS2Result = LSEResultEnum.Intact;

            this.m_BRAFResult = TestResult.NotApplicable;
            this.m_MethResult = TestResult.NotApplicable;
            this.m_BRAFRequired = false;
            this.m_MethRequired = false;
            this.m_Interpretation = "The results are compatible with a sporadic tumor and indicate a low risk for Lynch Syndrome.  If clinical suspicion for Lynch Syndrome is high, microsatellite instability (MSI) testing by PCR is recommended.";
            this.m_Method = IHCMethod;
            this.m_References = LSEGYNReferences;
		}
	}
}
