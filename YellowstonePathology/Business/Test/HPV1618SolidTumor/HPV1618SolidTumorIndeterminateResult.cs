using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.HPV1618SolidTumor
{
	public class HPV1618IndeterminateResult : HPV1618SolidTumorResult
	{
        public HPV1618IndeterminateResult()
        {
            this.m_ResultCode = "HPV1618INDMT";
            this.m_HPV16Result = HPV1618SolidTumorResult.IndeterminateResult;
            this.m_HPV18Result = HPV1618SolidTumorResult.IndeterminateResult;
        }
	}
}
