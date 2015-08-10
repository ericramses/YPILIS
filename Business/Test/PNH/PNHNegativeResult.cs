using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.PNH
{
	public class PNHNegativeResult : PNHResult
	{
		public static string ResultCode = "PNHNGTV";
		public PNHNegativeResult()
		{
			this.m_Result = "Negative (No evidence of paroxysmal nocturnal hemoglobinuria)";
			this.m_ResultCode = PNHNegativeResult.ResultCode;
		}
	}
}
