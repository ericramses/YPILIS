﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	public class LSEGYNResult2 : LSEResult
	{
        public LSEGYNResult2()
		{
			this.m_MLH1Result = LSEResultEnum.Intact;
			this.m_MSH2Result = LSEResultEnum.Intact;
			this.m_MSH6Result = LSEResultEnum.Intact;
			this.m_PMS2Result = LSEResultEnum.Intact;
			this.m_BrafResult = LSEResultEnum.NotApplicable;
			this.m_MethResult = LSEResultEnum.NotApplicable;
            this.m_BRAFIsIndicated = false;

            this.m_Interpretation = "Intact nuclear expression of MLH1, MSH2, MSH6, and PMS2 mismatch repair proteins.";
            this.m_Comment = "The results are compatible with a sporadic tumor and indicate a low risk for Lynch Syndrome.  If clinical suspicion for Lynch Syndrome is high, microsatellite instability (MSI) testing by PCR is recommended.";
            this.m_Method = IHCMethod;
            this.m_References = LSEGYNReferences;
		}
	}
}
