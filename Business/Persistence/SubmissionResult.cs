using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace YellowstonePathology.Business.Persistence
{
    [DataContract]
    public class SubmissionResult
    {
        private bool m_Success;
        private string m_Message;

        public SubmissionResult()
        {
            this.m_Success = true;
        }

        [DataMember]
        public bool Success
        {
            get { return this.m_Success; }
            set { this.m_Success = value; }
        }

        [DataMember]
        public string Message
        {
            get { return this.m_Message; }
            set { this.m_Message = value; }
        }

    }
}
