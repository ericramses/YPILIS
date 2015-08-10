using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.HPVTWI
{
	public class HPVTWIA567PositiveResult : HPVTWIResult
	{
        public HPVTWIA567PositiveResult()
		{
            this.m_PreliminaryResultCode = "HPVTWIPPN";
            this.m_A5A6Result = HPVTWIResult.PositiveResult;
            this.m_A7Result = HPVTWIResult.PositiveResult;
            this.m_A9Result = HPVTWIResult.NegativeResult;            

            this.m_OveralResultCode = "HPVTWPSTV";
            this.m_OveralResult = HPVTWIResult.PositiveResult;            
		}
	}
}
