using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.HPVTWI
{
	public class HPVTWIA5679PositiveResult : HPVTWIResult
	{
		public HPVTWIA5679PositiveResult()
		{
            this.m_PreliminaryResultCode = "HPVTWIPPP";
            this.m_A5A6Result = HPVTWIResult.PositiveResult;
            this.m_A7Result = HPVTWIResult.PositiveResult;
            this.m_A9Result = HPVTWIResult.PositiveResult;

            this.m_OveralResultCode = "HPVTWPSTV";
            this.m_OveralResult = HPVTWIResult.PositiveResult;            
		}
	}
}
