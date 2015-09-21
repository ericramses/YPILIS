using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ROS1ByFISH
{
    public class ROS1ByFISHNegativeResult : ROS1ByFISHResult
	{
        public ROS1ByFISHNegativeResult()
		{            
            this.m_ResultCode = "ROS1FISHNGTV";
			this.m_Result = "Negative";
            this.m_ResultDisplayText = "Negative";
            this.m_ResultAbbreviation = "Negative";

            this.m_Interpretation = null;
            this.m_ProbeSetDetail = "ROS1: A normal FISH signal pattern of two fusions (2F) was observed.";
		}        
	}
}
