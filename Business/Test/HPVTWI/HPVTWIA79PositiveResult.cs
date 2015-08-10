using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.HPVTWI
{
	public class HPVTWIA79PositiveResult : HPVTWIResult
	{
        public HPVTWIA79PositiveResult()
		{
            this.m_PreliminaryResultCode = "HPVTWINPP";
            this.m_A5A6Result = HPVTWIResult.NegativeResult;
            this.m_A7Result = HPVTWIResult.PositiveResult;
            this.m_A9Result = HPVTWIResult.PositiveResult;

            this.m_OveralResultCode = "HPVTWPSTV";
            this.m_OveralResult = HPVTWIResult.PositiveResult; 
		}
	}
}
