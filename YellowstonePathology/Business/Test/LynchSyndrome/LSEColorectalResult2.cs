using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	public class LSEColorectalResult2 : LSEResult
	{
		public LSEColorectalResult2()
		{
            this.m_Indication = LSEType.COLON;
            this.m_MLH1Result = LSEResultEnum.Loss;
			this.m_MSH2Result = LSEResultEnum.Intact;
			this.m_MSH6Result = LSEResultEnum.Intact;
			this.m_PMS2Result = LSEResultEnum.Loss;
			this.m_BrafResult = LSEResultEnum.Positive;
			this.m_MethResult = LSEResultEnum.NotApplicable;
            this.m_BRAFIsIndicated = true;

			this.m_Interpretation = "Loss of nuclear expression of MLH1 and PMS2 mismatch repair proteins.\r\nBRAF mutation V600E detected.";
			this.m_Comment = "BRAF mutation is present in the majority of MSI-high sporadic colorectal cancers. These cancers are MSI high due to MLH1 methylation.  MSI-high colorectal tumors have been found to respond to immunotherapy.  As BRAF mutation is rarely found in Lynch-related colorectal cancers, further genetic evaluation is not indicated.";
            this.m_Method = IHCBRAFMethod;
            this.m_References = LSEColonReferences;
		}
	}
}
