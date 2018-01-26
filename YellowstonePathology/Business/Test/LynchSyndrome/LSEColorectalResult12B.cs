﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	public class LSEColorectalResult12B : LSEResult
	{
		public LSEColorectalResult12B()
		{
			this.m_MLH1Result = LSEResultEnum.Loss;
			this.m_MSH2Result = LSEResultEnum.Intact;
			this.m_MSH6Result = LSEResultEnum.Intact;
			this.m_PMS2Result = LSEResultEnum.Loss;
            this.m_BrafResult = LSEResultEnum.NotApplicable;
            this.m_MethResult = LSEResultEnum.Positive;
            this.m_BRAFIsIndicated = false;

            this.m_Interpretation = "Loss of nuclear expression of MLH1 and PMS2 mismatch repair proteins. MLH1 promoter methylation DETECTED.";
            this.m_Comment = this.m_Comment = "The results are compatible with a sporadic tumor and further genetic evaluation is not indicated.";

            this.m_Method = IHCMethod;
            this.m_References = LSEColonReferences;
		}
	}
}
