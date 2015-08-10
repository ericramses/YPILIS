using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Trichomonas
{
    public class TrichomonasResult : YellowstonePathology.Business.Test.TestResult
    {
        public static string Method = "DNA was extracted from the patient’s specimen using an automated method.  Real time PCR amplification was performed for organism detection and identification.";        

		public TrichomonasResult()
        {
        }

		public static YellowstonePathology.Business.Rules.MethodResult IsOkToSetResult(TrichomonasTestOrder trichomonasTestOrder)
		{
			YellowstonePathology.Business.Rules.MethodResult result = new YellowstonePathology.Business.Rules.MethodResult();
			if (trichomonasTestOrder.Accepted == true)
			{
				result.Success = false;
				result.Message = "The result cannot be set because it is already accepted.";
			}
			else if (trichomonasTestOrder.ResultCode == null)
			{
				result.Success = false;
				result.Message = "The result cannot be set to no result.";
			}
			return result;
		}
	}
}
