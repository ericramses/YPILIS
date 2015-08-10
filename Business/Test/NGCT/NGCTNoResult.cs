using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.NGCT
{
	public class NGCTNoResult : NGCTResult
	{
		public NGCTNoResult()
		{
			this.m_ResultCode = null;
			this.m_Result = null;
			this.m_Method = null;
			this.m_References = null;
			this.m_TestDevelopment = null;
		}
	}
}
