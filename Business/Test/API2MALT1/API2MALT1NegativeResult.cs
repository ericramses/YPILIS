using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.API2MALT1
{
	public class API2MALT1NegativeResult : YellowstonePathology.Business.Test.TestResult
	{
		public API2MALT1NegativeResult()
		{
			this.m_Result = "Negative";
			this.m_ResultCode = "P2MLT1NGTV";
		}
	}
}
