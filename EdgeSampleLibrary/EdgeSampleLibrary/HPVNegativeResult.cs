using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EdgeSampleLibrary
{
    public class HPVNegativeResult : HPVResult
    {
        public HPVNegativeResult()
        {            
            this.m_ResultCode = "HPVTWINGTV";
            this.m_Result = HPVResult.NegativeResult;
        }
    }
}
