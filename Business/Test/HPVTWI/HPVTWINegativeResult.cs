using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.HPVTWI
{
	public class HPVTWINegativeResult : HPVTWIResult
	{
		public HPVTWINegativeResult()
		{
            this.m_PreliminaryResultCode = "HPVTWINNN";
            this.m_A5A6Result = HPVTWIResult.NegativeResult;
            this.m_A7Result = HPVTWIResult.NegativeResult;
            this.m_A9Result = HPVTWIResult.NegativeResult;

            this.m_OveralResultCode = "HPVTWINGTV";
            this.m_OveralResult = HPVTWIResult.NegativeResult;            
		}
	}
}
