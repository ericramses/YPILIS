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
			this.m_Result = "Negative for ROS1 gene rearrangement";
            this.m_ResultDisplayText = "Negative";
            this.m_ResultAbbreviation = "Negative";
			
			this.m_Interpretation = "An ALK gene rearrangement was observed in 0% of the nuclei scored and is below the cutoff of <10% for this assay. This represents " +
                "a NORMAL result and suggests that ALK inhibitors are not indicated.";

            this.m_ProbeSetDetail = "ROS1: A normal FISH signal pattern of two fusions (2F) was observed.";
		}        
	}
}
