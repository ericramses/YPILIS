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
        protected string m_UserName;
        protected string m_ComputerName;
        protected DateTime m_TimeLockAquired;

        public MessageBody()
        {

        }

        public MessageBody(string masterAccessionNo, string requestingUserName, string requestingComputerName, DateTime timeLockAquired)
        {
            this.m_MasterAccessionNo = masterAccessionNo;
            this.m_UserName = requestingUserName;
            this.m_ComputerName = requestingComputerName;
            this.m_TimeLockAquired = timeLockAquired;
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

        public string UserName
        {
            get { return this.m_UserName; }
            set { this.m_UserName = value; }
        }

        public string ComputerName
        {
            get { return this.m_ComputerName; }
            set { this.m_ComputerName = value; }
        }

        public DateTime TimeLockAquired
        {
            get { return this.m_TimeLockAquired; }
            set { this.m_TimeLockAquired = value; }
        }
    }
}
