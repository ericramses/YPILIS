using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.HPV1618SolidTumor
{
	public class HPV1618SolidTumorResultCollection : List<HPV1618SolidTumorResult>
	{
		public HPV1618SolidTumorResultCollection()
        {

        }

        public HPV1618SolidTumorResult GetResult(string resultCode)
        {
            HPV1618SolidTumorResult result = null;
			foreach (HPV1618SolidTumorResult hpv1618Result in this)
            {
				if (hpv1618Result.ResultCode == resultCode)
                {
					result = hpv1618Result;
                    break;
                }
            }
            return result;
        }

		public static HPV1618SolidTumorResultCollection GetAllResults()
        {
            HPV1618SolidTumorResultCollection result = new HPV1618SolidTumorResultCollection();
            result.Add(new HPV1618NoResult());
			result.Add(new HPV1618BothNegativeResult());
			result.Add(new HPV1618BothPositiveResult());
			result.Add(new HPV1618IndeterminateResult());
			result.Add(new HPV16NegativeHPV18PositiveResult());
			result.Add(new HPV16PositiveHPV18NegativeResult());
            result.Add(new HPV1618SolidTumorAnalRegionResult());
            return result;
		}
	}
}
