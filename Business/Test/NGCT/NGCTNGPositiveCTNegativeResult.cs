using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.NGCT
{
    public class NGCTNGPositiveCTNegativeResult : NGCTResult
    {
        public NGCTNGPositiveCTNegativeResult()
        {
            this.m_NGResultCode = NGCTResult.NGPositiveResultCode;
            this.m_NeisseriaGonorrhoeaeResult = NGCTResult.PositiveResult;
            this.m_CTResultCode = NGCTResult.CTNegativeResultCode;
            this.m_ChlamydiaTrachomatisResult = NGCTResult.NegativeResult;
        }
    }
}
