using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ROS1ByFISH
{
    public class ROS1ByFISHResultFactory
    {
        public static ROS1ByFISHResult GetResult(string resultCode)
        {
            ROS1ByFISHResult result = null;
            switch (resultCode)
            {
                case "ROS1FISHNGTV":
                    result = new ROS1ByFISHNegativeResult();
                    break;                
            }
            return result;
        }
    }
}
