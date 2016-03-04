using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace YellowstonePathology.UI.AppMessaging
{
    public class MessageQueues : INotifyPropertyChanged
    {
        public delegate void ReleaseLockEventHandler(object sender, EventArgs e);
        public event ReleaseLockEventHandler ReleaseLock;

        public delegate void AquireLockEventHandler(object sender, EventArgs e);
        public event AquireLockEventHandler AquireLock;

        public delegate void RequestReceivedEventHandler(object sender, UI.CustomEventArgs.MessageReturnEventArgs e);
        public event RequestReceivedEventHandler RequestReceived;

        public delegate void ResponseReceivedEventHandler(object sender, UI.CustomEventArgs.MessageReturnEventArgs e);
        public event ResponseReceivedEventHandler ResponseReceived;

        private static volatile MessageQueues instance;
        private static object syncRoot = new Object();

        public delegate void PropertyChangedNotificationHandler(String info);
        public event PropertyChangedEventHandler PropertyChanged;

        public const string LockReleaseRequestQueueName = "lockreleaserequests";
        public const string LockReleaseResponseQueueName = "lockreleaseresponses";

        private System.Messaging.MessageQueue m_LockReleaseRequestQueue;
        private System.Messaging.MessageQueue m_LockReleaseResponseQueue;

        private MessageQueueCollection<MessageQueueMessage> m_MessageCollection;        

        static MessageQueues()
        {

        }

        private MessageQueues()
        {
            this.m_MessageCollection = new MessageQueueCollection<MessageQueueMessage>();            

            this.m_LockReleaseRequestQueue = new System.Messaging.MessageQueue(Environment.MachineName + "\\" + LockReleaseRequestQueueName);
            this.m_LockReleaseRequestQueue.Formatter = new System.Messaging.XmlMessageFormatter(new Type[] { typeof(LockReleaseRequestMessageBody) });
            this.m_LockReleaseRequestQueue.ReceiveCompleted += LockReleaseRequestMessageQueue_ReceiveCompleted;
            this.m_LockReleaseRequestQueue.BeginReceive();

            this.m_LockReleaseResponseQueue = new System.Messaging.MessageQueue(Environment.MachineName + "\\" + LockReleaseResponseQueueName);
            this.m_LockReleaseResponseQueue.Formatter = new System.Messaging.XmlMessageFormatter(new Type[] { typeof(LockReleaseResponseMessageBody) });
            this.m_LockReleaseResponseQueue.ReceiveCompleted += LockReleaseResponseMessageQueue_ReceiveCompleted;
            this.m_LockReleaseResponseQueue.BeginReceive();
        }        

        public MessageQueueCollection<MessageQueueMessage> MessageCollection
        {
            get { return this.m_MessageCollection; }
        }

        public void SendLockReleaseRequest(Business.Test.AccessionOrder accessionOrder)
        {            
            LockReleaseRequestMessageBody messageBody = new LockReleaseRequestMessageBody(accessionOrder.MasterAccessionNo, accessionOrder.LockAquiredByUserName, accessionOrder.LockAquiredByHostName, accessionOrder.TimeLockAquired.Value);
            System.Messaging.Message message = new System.Messaging.Message(messageBody);            

            message.ResponseQueue = new System.Messaging.MessageQueue(Environment.MachineName + "\\" + LockReleaseResponseQueueName);
            System.Messaging.MessageQueue queue = new System.Messaging.MessageQueue(messageBody.LockAquiredByHostName + "\\" + LockReleaseRequestQueueName);                       
            queue.Send(message);

            MessageQueueMessage messageQueueMessage = new MessageQueueMessage(message, MessageDirectionEnum.Sent);
            this.m_MessageCollection.Insert(0, messageQueueMessage);            
        }

        public void SendLockReleaseResponse(System.Messaging.Message requestMessage, bool releaseLock)
        {
            MessageBody receivedMessageBody = (MessageBody)requestMessage.Body;
            LockReleaseResponseMessageBody responseMessageBody = new LockReleaseResponseMessageBody(receivedMessageBody, releaseLock);            

            if(releaseLock == true) if (this.ReleaseLock != null) this.ReleaseLock(receivedMessageBody.MasterAccessionNo, new EventArgs());            
            
            System.Messaging.Message responseMessage = new System.Messaging.Message(responseMessageBody);            
            requestMessage.ResponseQueue.Send(responseMessage);

            MessageQueueMessage responseMessageQueueMessage = new MessageQueueMessage(responseMessage, MessageDirectionEnum.Sent);
            this.m_MessageCollection.Add(responseMessageQueueMessage);                       
        }

        private void LockReleaseRequestMessageQueue_ReceiveCompleted(object sender, System.Messaging.ReceiveCompletedEventArgs e)
        {            
            System.Messaging.Message receivedMessage = this.m_LockReleaseRequestQueue.EndReceive(e.AsyncResult);            

            MessageQueueMessage receviedMessageQueueMessage = new MessageQueueMessage(receivedMessage, MessageDirectionEnum.Received);
            this.m_MessageCollection.Add(receviedMessageQueueMessage);            

            this.m_LockReleaseRequestQueue.BeginReceive();
            if (RequestReceived != null) this.RequestReceived(this, new UI.CustomEventArgs.MessageReturnEventArgs(receivedMessage));
        }        

        private void LockReleaseResponseMessageQueue_ReceiveCompleted(object sender, System.Messaging.ReceiveCompletedEventArgs e)
        {
            System.Messaging.Message message = this.m_LockReleaseResponseQueue.EndReceive(e.AsyncResult);
            MessageBody messageBody = (MessageBody)message.Body;

            MessageQueueMessage messageQueueMessage = new MessageQueueMessage(message, MessageDirectionEnum.Received);
            this.m_MessageCollection.Add(messageQueueMessage);

            this.m_LockReleaseResponseQueue.BeginReceive();                        
            if (this.AquireLock != null) this.AquireLock(messageBody.MasterAccessionNo, new EventArgs());
            if (ResponseReceived != null) this.ResponseReceived(this, new UI.CustomEventArgs.MessageReturnEventArgs(message));
        }        

        public void CreateMessageQueuesIfNotExist()
        {            
            if (System.Messaging.MessageQueue.Exists(Environment.MachineName + "\\" + LockReleaseRequestQueueName) == false)
            {
                System.Messaging.MessageQueue.Create(Environment.MachineName + "\\" + LockReleaseRequestQueueName);
            }

            if (System.Messaging.MessageQueue.Exists(Environment.MachineName + "\\" + LockReleaseResponseQueueName) == false)
            {
                System.Messaging.MessageQueue.Create(Environment.MachineName + "\\" + LockReleaseResponseQueueName);
            }
        }

        public static MessageQueues Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new MessageQueues();
                    }
                }

                return instance;
            }
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

    }
}
