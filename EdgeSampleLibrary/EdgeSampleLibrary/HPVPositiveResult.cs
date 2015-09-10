using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EdgeSampleLibrary
{
    public class HPVPositiveResult : HPVResult
    {
        public HPVPositiveResult()
        {
            this.m_ResultCode = "HPVTWPSTV";
            this.m_Result = HPVResult.PositiveResult;
        }
    }
}
