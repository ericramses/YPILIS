using System;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Task.Model
{
    [PersistentClass("tblTaskOrderDetailFax", "tblTaskOrderDetail", "YPIDATA")]
    public class TaskOrderDetailFax : TaskOrderDetail
    {
        private string m_FaxNumber;
        private string m_SendToName;
        private string m_DocumentName;

        public TaskOrderDetailFax()
        {
        }

        public TaskOrderDetailFax(string taskOrderDetailId, string taskOrderId, string objectId, TaskFax task, int clientId) 
            : base(taskOrderDetailId, taskOrderId, objectId, task, clientId)
        {
            TaskFax taskFax = (Model.TaskFax)task;
            this.TaskId = taskFax.TaskId;
            this.m_DocumentName = task.DocumentName;
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

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string DocumentName
        {
            get { return this.m_DocumentName; }
            set
            {
                if (this.m_DocumentName != value)
                {
                    this.m_DocumentName = value;
                    this.NotifyPropertyChanged("DocumentName");
                }
            }
        }

        public string FaxNumberProxy
        {
            get { return this.CorrectFaxNumber(this.m_FaxNumber); }
            set
            {
                if (this.m_FaxNumber != value)
                {
                    this.m_FaxNumber = this.CorrectFaxNumber(value);
                    this.NotifyPropertyChanged("FaxNumberProxy");
                }
            }
        }

        private string CorrectFaxNumber(string numberIn)
        {
            string result = numberIn;

            if (string.IsNullOrEmpty(result) == true) return numberIn;

            result = result.Replace("(", "");
            result = result.Replace(")", "");
            result = result.Replace("-", "");
            result = result.Replace(" ", "");

            if (result.Length == 10 || result.Length == 7) return result;
            if (result.Length == 11 && result[0] == '1') return result.Remove(0, 1);
            if (result.Length == 11 && result[0] == '9') return result.Remove(0, 1);
            if (result.Length == 12 && result[0] == '1' && result[1] == '9') return result.Remove(0, 2);

            return result;
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
