using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.PNH
{
	public class PNHNegativeWithPreviousPositiveResult : PNHResult
	{
		public static string ResultCode = "PNHNGTVPRVSPSTV";

		public PNHNegativeWithPreviousPositiveResult()
		{

			this.m_Result = "No PNH clone identified";
			this.m_ResultCode = PNHNegativeWithPreviousPositiveResult.ResultCode;
			this.m_Comment = "Flow cytometric analysis does not identify any evidence of a PNH clone, based on analysis of several different " +
				"GPI-linked antibodies on 3 separate cell populations (red blood cells, monocytes and granulocytes).";
		}
	}
}
