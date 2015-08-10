using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.API2MALT1
{
	public class API2MALT1PositiveResult : YellowstonePathology.Business.Test.TestResult
	{
		public API2MALT1PositiveResult()
		{
			this.m_Result = "Positive";
			this.m_ResultCode = "P2MLT1PSTV";
		}
	}
}
