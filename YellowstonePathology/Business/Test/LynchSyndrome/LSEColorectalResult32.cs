using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	public class LSEColorectalResult32 : LSEResult
	{
		public LSEColorectalResult32()
		{
            this.m_Indication = LSEType.COLON;
            this.m_MLH1Result = LSEResultEnum.Loss;
			this.m_MSH2Result = LSEResultEnum.Intact;
			this.m_MSH6Result = LSEResultEnum.Intact;
			this.m_PMS2Result = LSEResultEnum.Intact;
			this.m_BrafResult = LSEResultEnum.Negative;
			this.m_MethResult = LSEResultEnum.Positive;
            this.m_BRAFIsIndicated = true;

			this.m_Interpretation = "Loss of nuclear expression of MLH1 mismatch repair proteins.\r\nBRAF mutation V600E NOT DETECTED.\r\nMLH1 promoter methylation DETECTED.";
			this.m_Comment = "The results are compatible with a sporadic tumor and further genetic evaluation is not indicated unless there is high clinical suspicion for Lynch Syndrome.";
            this.m_Method = IHCBRAFMLHMethod;
            this.m_References = LSEColonReferences;
		}
	}
}
