using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.PDL1
{
    public class PDL1NegativeResult : PDL1Result
    {
        public PDL1NegativeResult()
        {
            this.m_Result = "Negative";
            this.m_ResultCode = "PDL1NGTV";
            this.m_ResultAbbreviation = "Negative";
        }
    }
}
