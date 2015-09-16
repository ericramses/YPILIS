using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.HPVTWI
{
    public class HPVTWIPositiveResult : HPVTWIResult
	{
		public HPVTWIPositiveResult()
		{            
            this.m_OveralResultCode = "HPVTWPSTV";
            this.m_OveralResult = HPVTWIResult.PositiveResult; 
		}
	}
}
