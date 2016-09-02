using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Task.Model
{
    public class TaskOrderDetailFactory
    {
        public static TaskOrderDetail GetTaskOrderDetail(string taskId)
        {
            TaskOrderDetail result = null;
            switch(taskId)
            {
                case "FDXSHPMNT":
                    result = new TaskOrderDetailFedexShipment();
                    break;
                default:
                    result = new TaskOrderDetail();
                    break;
            }
            return result;
        }
    }
}
