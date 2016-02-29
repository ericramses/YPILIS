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
        private static volatile MessageQueues instance;
        private static object syncRoot = new Object();

        public delegate void PropertyChangedNotificationHandler(String info);
        public event PropertyChangedEventHandler PropertyChanged;

        public const string LockReleaseRequestQueueName = "lockreleaserequests";
        public const string LockReleaseResponseQueueName = "lockreleaseresponses";

        private System.Messaging.MessageQueue m_LockReleaseRequestQueue;
        private System.Messaging.MessageQueue m_LockReleaseResponseQueue;

        private MessageQueueCollection<MessageQueueMessage> m_MessageCollection;
        private MessagingDialog m_MessagingDialog;

        static MessageQueues()
        {

        }

        private MessageQueues()
        {
            this.m_MessageCollection = new MessageQueueCollection<MessageQueueMessage>();

            this.m_MessagingDialog = new MessagingDialog();
            this.m_MessagingDialog.Visibility = System.Windows.Visibility.Collapsed;            

            this.m_LockReleaseRequestQueue = new System.Messaging.MessageQueue(Environment.MachineName + "\\" + LockReleaseRequestQueueName);
            this.m_LockReleaseRequestQueue.Formatter = new System.Messaging.XmlMessageFormatter(new Type[] { typeof(LockReleaseRequestMessageBody) });
            this.m_LockReleaseRequestQueue.ReceiveCompleted += LockReleaseRequestMessageQueue_ReceiveCompleted;
            this.m_LockReleaseRequestQueue.BeginReceive();

            this.m_LockReleaseResponseQueue = new System.Messaging.MessageQueue(Environment.MachineName + "\\" + LockReleaseResponseQueueName);
            this.m_LockReleaseResponseQueue.Formatter = new System.Messaging.XmlMessageFormatter(new Type[] { typeof(LockReleaseResponseMessageBody) });
            this.m_LockReleaseResponseQueue.ReceiveCompleted += LockReleaseResponseMessageQueue_ReceiveCompleted;
            this.m_LockReleaseResponseQueue.BeginReceive();
        }

        public void StartSendLockReleaseRequest(Business.Test.AccessionOrder accessionOrder)
        {            
            UI.AppMessaging.MessagingPage page = new UI.AppMessaging.MessagingPage();
            page.StartSendLockReleaseRequest(accessionOrder);
            this.m_MessagingDialog.PageNavigator.Navigate(page);
            this.m_MessagingDialog.Visibility = System.Windows.Visibility.Visible;
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
            System.Messaging.MessageQueue queue = new System.Messaging.MessageQueue(messageBody.ComputerName + "\\" + LockReleaseRequestQueueName);                       
            queue.Send(message);

            MessageQueueMessage messageQueueMessage = new MessageQueueMessage(message, MessageDirectionEnum.Sent);
            this.m_MessageCollection.Insert(0, messageQueueMessage);

            if (this.m_MessagingDialog.Visibility == System.Windows.Visibility.Collapsed) this.m_MessagingDialog.Visibility = System.Windows.Visibility.Visible;
        }

        public void SendLockReleaseResponse(System.Messaging.Message requestMessage)
        {            
            /*
            System.Messaging.Message message = new System.Messaging.Message(messageBody);

            System.Messaging.Message responseMessage = new System.Messaging.Message();
            requestMessage.ResponseQueue.Send(responseMessage);

            MessageQueueMessage responseMessageQueueMessage = new MessageQueueMessage(responseMessage, MessageDirectionEnum.Sent);
            this.m_MessageCollection.Add(responseMessageQueueMessage);            

            if (this.m_MessagingDialog.Visibility == System.Windows.Visibility.Collapsed) this.m_MessagingDialog.Visibility = System.Windows.Visibility.Visible;            
            */
        }

        private void LockReleaseRequestMessageQueue_ReceiveCompleted(object sender, System.Messaging.ReceiveCompletedEventArgs e)
        {
            System.Messaging.Message receivedMessage = this.m_LockReleaseRequestQueue.EndReceive(e.AsyncResult);            

            MessageQueueMessage receviedMessageQueueMessage = new MessageQueueMessage(receivedMessage, MessageDirectionEnum.Received);
            this.m_MessageCollection.Add(receviedMessageQueueMessage);            

            this.m_LockReleaseRequestQueue.BeginReceive();
            if (this.m_MessagingDialog.Visibility == System.Windows.Visibility.Collapsed) this.m_MessagingDialog.Visibility = System.Windows.Visibility.Visible;
        }

        private void LockReleaseResponseMessageQueue_ReceiveCompleted(object sender, System.Messaging.ReceiveCompletedEventArgs e)
        {
            System.Messaging.Message message = this.m_LockReleaseResponseQueue.EndReceive(e.AsyncResult);            

            MessageQueueMessage messageQueueMessage = new MessageQueueMessage(message, MessageDirectionEnum.Received);
            this.m_MessageCollection.Add(messageQueueMessage);

            this.m_LockReleaseResponseQueue.BeginReceive();

            MessagingPage messagingPage = new MessagingPage();
            messagingPage.StartReceiveLockReleaseResponse(message);

            this.m_MessagingDialog.PageNavigator.Navigate(messagingPage);

            if (this.m_MessagingDialog.Visibility == System.Windows.Visibility.Collapsed) this.m_MessagingDialog.Visibility = System.Windows.Visibility.Visible;            
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
