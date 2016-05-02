using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Task.Model
{
	public class TaskOrderDetailCollection : ObservableCollection<TaskOrderDetail>
	{
		public const string PREFIXID = "TSKOD";

		public TaskOrderDetailCollection()
		{

		}

        public bool HasUnacknowledgeItems()
        {
            bool result = false;
            foreach (TaskOrderDetail taskOrderDetail in this)
            {
                if (taskOrderDetail.Acknowledged == false)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public bool HasAcknowledgeItems()
        {
            bool result = false;
            foreach (TaskOrderDetail taskOrderDetail in this)
            {
                if (taskOrderDetail.Acknowledged == true)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

		public bool Exists(string taskOrderDetailId)
		{
			bool result = false;
			foreach (TaskOrderDetail taskOrderDetail in this)
			{
				if (taskOrderDetail.TaskOrderDetailId == taskOrderDetailId)
				{
					result = true;
					break;
				}
			}
			return result;
		}

		public TaskOrderDetail Get(string taskOrderDetailId)
		{
			TaskOrderDetail result = null;
			foreach (TaskOrderDetail taskOrderDetail in this)
			{
				if (taskOrderDetail.TaskOrderDetailId == taskOrderDetailId)
				{
					result = taskOrderDetail;
					break;
				}
			}
			return result;
		}

	}
}
