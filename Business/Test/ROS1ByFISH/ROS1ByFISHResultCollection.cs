using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.ROS1ByFISH
{
    public class ROS1ByFISHResultCollection : ObservableCollection<ROS1ByFISHResult>
    {
        public ROS1ByFISHResultCollection()
        {
            this.Add(new ROS1ByFISHNegativeResult());
            this.Add(new ROS1ByFISHAbnormalResult());            
        }
    }
}
