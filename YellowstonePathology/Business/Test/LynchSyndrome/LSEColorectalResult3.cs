using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	public class LSEColorectalResult3 : LSEResult
	{
		public LSEColorectalResult3()
		{
			this.m_MLH1Result = LSEResultEnum.Loss;
			this.m_MSH2Result = LSEResultEnum.Intact;
			this.m_MSH6Result = LSEResultEnum.Intact;
			this.m_PMS2Result = LSEResultEnum.Loss;
			this.m_BrafResult = LSEResultEnum.NotDetected;
			this.m_MethResult = LSEResultEnum.Positive;
            this.m_BRAFIsIndicated = true;

			this.m_Interpretation = "Loss of nuclear expression of MLH1 and PMS2 mismatch repair proteins.\r\nBRAF mutation NOT DETECTED.\r\nMLH1 promoter methylation DETECTED.";
			this.m_Comment = "The results are compatible with a sporadic tumor and further genetic evaluation is not indicated unless there is high clinical suspicion for Lynch Syndrome.";
            this.m_Method = IHCBRAFMLHMethod;
            this.m_References = LSEColonReferences;
		}
	}
}
