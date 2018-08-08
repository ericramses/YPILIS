using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	public class LSEColorectalResult12 : LSEResult
	{
		public LSEColorectalResult12()
		{
            this.m_Indication = LSEType.COLON;
            this.m_MLH1Result = LSEResultEnum.Loss;
			this.m_MSH2Result = LSEResultEnum.Intact;
			this.m_MSH6Result = LSEResultEnum.Intact;
			this.m_PMS2Result = LSEResultEnum.Loss;
            this.m_BrafResult = LSEResultEnum.Detected;
            this.m_MethResult = LSEResultEnum.Detected;
            this.m_BRAFIsIndicated = false;
            
            this.m_Comment = this.m_Comment = "The results are compatible with a sporadic tumor and further genetic evaluation is not indicated.";

            this.m_Method = IHCMethod;
            this.m_References = LSEColonReferences;
		}
	}
}
