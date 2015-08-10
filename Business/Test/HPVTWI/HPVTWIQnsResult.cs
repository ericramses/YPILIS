using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.HPVTWI
{
	public class HPVTWIQNSResult : HPVTWIResult
	{
        public HPVTWIQNSResult()
		{
            this.m_PreliminaryResultCode = "HPVTWIQNS";
            this.m_A5A6Result = HPVTWIResult.QnsResult;
            this.m_A7Result = HPVTWIResult.QnsResult;
            this.m_A9Result = HPVTWIResult.QnsResult;

            this.m_OveralResultCode = "HPVTWIQNS";
            this.m_OveralResult = HPVTWIResult.InsuficientDNA;            
		}
	}
}
