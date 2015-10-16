using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.NGCT
{
    public class NGCTBothNegativeResult : NGCTResult
    {
        public NGCTBothNegativeResult()
        {
            this.m_NGResultCode = NGCTResult.NGNegativeResultCode;
            this.m_NeisseriaGonorrhoeaeResult = NGCTResult.NegativeResult;
            this.m_CTResultCode = NGCTResult.CTNegativeResultCode;
            this.m_ChlamydiaTrachomatisResult = NGCTResult.NegativeResult;
        }
    }
}
