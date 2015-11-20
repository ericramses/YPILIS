using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.HPVTWI
{
    public class HPVTWIInvalidResult : HPVTWIResult
	{
		public HPVTWIInvalidResult()
		{            
            this.m_ResultCode = "HPVTWNVLD";
            this.m_Result = HPVTWIResult.InvalidResult; 
		}
	}
}
