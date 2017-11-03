using System;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Task.Model
{
    [PersistentClass("tblTaskOrderDetailFax", "tblTaskOrderDetail", "YPIDATA")]
    public class TaskOrderDetailFax : TaskOrderDetail
    {
        private string m_FaxNumber;
        private string m_SendToName;

        public TaskOrderDetailFax()
        {
        }

        public TaskOrderDetailFax(string taskOrderDetailId, string taskOrderId, string objectId, Task task, int clientId) 
            : base(taskOrderDetailId, taskOrderId, objectId, task, clientId)
        {
            TaskFax taskFax = (Model.TaskFax)task;
            this.TaskId = taskFax.TaskId;
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string FaxNumber
        {
            get { return this.m_FaxNumber; }
            set
            {
                if (this.m_FaxNumber != value)
                {
                    this.m_FaxNumber = value;
                    this.NotifyPropertyChanged("FaxNumber");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string SendToName
        {
            get { return this.m_SendToName; }
            set
            {
                if (this.m_SendToName != value)
                {
                    this.m_SendToName = value;
                    this.NotifyPropertyChanged("SendToName");
                }
            }
        }

        public bool PropertiesAreEnabled
        {
            get
            {
                bool result = false;
                if (string.IsNullOrEmpty(this.m_FaxNumber) == true ||
                    string.IsNullOrEmpty(this.m_SendToName) == true) result = true;
                return result;
            }
        }
    }
}
