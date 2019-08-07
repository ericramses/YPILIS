using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Task.Model
{
    public class TaskOrderView
    {
        private TaskOrder m_TaskOrder;

        public TaskOrderView(TaskOrder taskOrder)
        {
            this.m_TaskOrder = taskOrder;
        }

        public TaskOrder TaskOrder
        {
            get { return this.m_TaskOrder; }
        }

        public Business.User.SystemUser OrderedByUser
        {
            get { return YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetSystemUserById(this.m_TaskOrder.OrderedById); }
        }
    }
}
