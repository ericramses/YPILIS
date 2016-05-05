using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.AppMessaging
{
    public class MessageQueueMessage
    {
        private System.Messaging.Message m_Message;

        private string m_Label;
        private MessageBody m_MessageBody;
        private MessageDirectionEnum m_Direction;
        private DateTime m_MessageDate;

        public MessageQueueMessage(System.Messaging.Message message, MessageDirectionEnum direction)
        {
            this.m_Message = message;

            this.m_MessageBody = (MessageBody)message.Body;
            
            this.m_Label = message.Label;
            this.m_Direction = direction;
            this.m_MessageDate = DateTime.Now;
        }

        public MessageBody MessageBody
        {
            get { return this.m_MessageBody; }
        }

        public string Label
        {
            get { return this.m_Label; }
        }

        public MessageDirectionEnum Direction
        {
            get { return this.m_Direction; }
        }

        public DateTime MessageDate
        {
            get { return this.m_MessageDate; }
        }
    }
}
