using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.NGCT
{
    public class NGCTIndeterminateResult : NGCTResult
    {
        public NGCTIndeterminateResult()
        {
            this.m_ResultCode = NGCTResult.IndeterminateResultCode;
            this.m_Result = NGCTResult.IndeterminateResult;
        }
    }
}
