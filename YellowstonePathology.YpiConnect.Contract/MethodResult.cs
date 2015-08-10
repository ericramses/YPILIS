using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace YellowstonePathology.YpiConnect.Contract
{
    [DataContract]
    public class MethodResult
    {
        private string m_Message;
        private bool m_Success;

        public MethodResult()
        {
            this.m_Success = true;
        }

        [DataMember]
        public string Message
        {
            get { return this.m_Message; }
            set { this.m_Message = value; }
        }

        [DataMember]
        public bool Success
        {
            get { return this.m_Success; }
            set { this.m_Success = value; }
        }        
    }
}
