using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.NGCT
{
    public class NGCTNGNegativeCTPositiveResult : NGCTResult
    {
        public NGCTNGNegativeCTPositiveResult()
        {
            this.m_NGResultCode = NGCTResult.NGNegativeResultCode;
            this.m_NeisseriaGonorrhoeaeResult = NGCTResult.NegativeResult;
            this.m_CTResultCode = NGCTResult.CTPositiveResultCode;
            this.m_ChlamydiaTrachomatisResult = NGCTResult.PositiveResult;
        }
    }
}
