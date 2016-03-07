using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.CustomEventArgs
{
    public class MessageReturnEventArgs : System.EventArgs
    {
        private System.Messaging.Message m_Message;

        public MessageReturnEventArgs(System.Messaging.Message message)
        {
            this.m_Message = message;
        }

        public System.Messaging.Message Message
        {
            get { return this.m_Message; }
        }
    }
}
