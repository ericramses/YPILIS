using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.NGCT
{
	public class NGCTNGPositiveResult : NGCTResult
	{
		public NGCTNGPositiveResult()
		{
			this.m_ResultCode = "NGPSTV";
			this.m_Result = "Positive";
		}
	}
}
