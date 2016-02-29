using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.AppMessaging
{
    [Serializable]
    public class MessageBody
    {
        protected string m_Message;
        protected string m_MasterAccessionNo;
        protected string m_LockAquiredByUserName;
        protected string m_LockAquiredByHostName;
        protected DateTime m_TimeLockAquired;
        protected string m_RequestingUserName;
        protected string m_RequestingHostName;

        public MessageBody()
        {

        }

        public MessageBody(string masterAccessionNo, string lockAquiredByUserName, string lockAquiredByHostName, DateTime timeLockAquired)
        {
            this.m_MasterAccessionNo = masterAccessionNo;
            this.m_LockAquiredByUserName = lockAquiredByUserName;
            this.m_LockAquiredByHostName = lockAquiredByHostName;
            this.m_TimeLockAquired = timeLockAquired;

            this.m_RequestingHostName = Environment.MachineName;
            this.m_RequestingUserName = Business.User.SystemIdentity.Instance.User.UserName;
        }

        public string Message
        {
            get { return this.m_Message; }
            set { this.m_Message = value; }
        }

        public string MasterAccessionNo
        {
            get { return this.m_MasterAccessionNo; }
            set { this.m_MasterAccessionNo = value; }
        }

        public string LockAquiredByUserName
        {
            get { return this.m_LockAquiredByUserName; }
            set { this.m_LockAquiredByUserName = value; }
        }

        public string LockAquiredByHostName
        {
            get { return this.m_LockAquiredByHostName; }
            set { this.m_LockAquiredByHostName = value; }
        }

        public DateTime TimeLockAquired
        {
            get { return this.m_TimeLockAquired; }
            set { this.m_TimeLockAquired = value; }
        }
    }
}
