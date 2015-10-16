using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.NGCT
{
    public class NGCTBothPositiveResult : NGCTResult
    {
        public NGCTBothPositiveResult()
        {
            this.m_NGResultCode = NGCTResult.NGPositiveResultCode;
            this.m_NeisseriaGonorrhoeaeResult = NGCTResult.PositiveResult;
            this.m_CTResultCode = NGCTResult.CTPositiveResultCode;
            this.m_ChlamydiaTrachomatisResult = NGCTResult.PositiveResult;
        }
    }
}
