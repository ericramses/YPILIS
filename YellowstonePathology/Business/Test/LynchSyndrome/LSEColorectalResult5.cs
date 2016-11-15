﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	public class LSEColorectalResult5 : LSEResult
	{
		public LSEColorectalResult5()
		{
			this.m_MLH1Result = LSEResultEnum.Intact;
			this.m_MSH2Result = LSEResultEnum.Loss;
            this.m_MSH6Result = LSEResultEnum.Loss;
			this.m_PMS2Result = LSEResultEnum.Intact;
			this.m_BrafResult = LSEResultEnum.NotApplicable;
			this.m_MethResult = LSEResultEnum.NotApplicable;
            this.m_BRAFIsIndicated = false;

			this.m_Interpretation = "Loss of nuclear expression of MSH2 and MSH6 mismatch repair proteins.";
			this.m_Comment = "This staining pattern is highly suggestive of Lynch Syndrome and is associated with germline MSH2, EPCAM, or MSH6 mutations.  Recommend genetic counseling and further evaluation.";
            this.m_Method = IHCMethod;
            this.m_References = LSEColonReferences;
		}
	}
}
