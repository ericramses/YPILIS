using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.PDL1
{
    public class PDL1PositiveResult : PDL1Result
    {
        public PDL1PositiveResult()
        {
            this.m_Result = "Positive";
            this.m_ResultCode = "PDL1PSTV";
            this.m_ResultAbbreviation = "Positive";
        }
    }
}
