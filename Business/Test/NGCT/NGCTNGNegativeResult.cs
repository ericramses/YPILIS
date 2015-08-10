using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.NGCT
{
	public class NGCTNGNegativeResult : NGCTResult
	{
		public NGCTNGNegativeResult()
		{
			this.m_ResultCode = "NGNGTV";
			this.m_Result = "Negative";
		}
	}
}
