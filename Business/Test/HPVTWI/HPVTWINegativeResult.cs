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
            this.m_ResultCode = "HPVTWINGTV";
            this.m_Result = HPVTWIResult.NegativeResult;            
		}
	}
}
