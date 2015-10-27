using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.HPVTWI
{
    public class HPVTWINoResult : HPVTWIResult
    {
        public HPVTWINoResult()
        {
            this.m_ResultCode = null;
            this.m_Result = null;
        }
    }
}
