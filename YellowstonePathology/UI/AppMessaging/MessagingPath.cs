using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StackExchange.Redis;
using Newtonsoft.Json;

namespace YellowstonePathology.UI.AppMessaging
{
	public class MessagingPath
	{
        private static volatile MessagingPath instance;
        private static object syncRoot = new Object();

        public delegate void NextEventHandler(object sender, UI.CustomEventArgs.AccessionOrderReturnEventArgs e);
        public event NextEventHandler Next;

        private Navigation.PageNavigator m_PageNavigator;
        private bool m_PageNavigatorWasPassedIn;

        static MessagingPath()
        {
            
        }        

        private MessagingPath()
        {                        
            this.m_PageNavigatorWasPassedIn = false;
        }

        public void HandleMessageRecieved(string channel, AccessionLockMessage accessionLockMessage)
        {            
            switch(accessionLockMessage.MessageId)
            {
                case AccessionLockMessageIdEnum.ASK:
                    this.HandleASKRecieved(accessionLockMessage);
                    break;
                case AccessionLockMessageIdEnum.GIVE:
                    this.ShowResponseReceivedPage(accessionLockMessage);
                    break;
                case AccessionLockMessageIdEnum.HOLD:
                    break;
            }
        }

        public void HandleASKRecieved(AccessionLockMessage message)
        {            
            MessagingDialog messagingDialog = new MessagingDialog();
            this.m_PageNavigator = messagingDialog.PageNavigator;
            messagingDialog.Closed += MessagingDialog_Closed;

            AppMessaging.LockRequestReceivedPage lockRequestReceivedPage = new AppMessaging.LockRequestReceivedPage(message);                
            messagingDialog.PageNavigator.Navigate(lockRequestReceivedPage);
            messagingDialog.Show();            
        }

        public void StartSendRequest(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, Navigation.PageNavigator pageNavigator)
        {
            this.m_PageNavigator = pageNavigator;
            this.m_PageNavigatorWasPassedIn = true;

            UI.AppMessaging.AccessionLockMessage message = new AccessionLockMessage(accessionOrder.MasterAccessionNo, System.Environment.MachineName, Business.User.SystemIdentity.Instance.User.UserName, AccessionLockMessageIdEnum.ASK);
            ISubscriber subscriber = Business.RedisConnection.Instance.GetSubscriber();
            subscriber.Publish(message.MasterAccessionNo, JsonConvert.SerializeObject(message));

            this.ShowLockRequestSentPage(message);
        }

        public void Start(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {                        
            MessagingDialog messagingDialog = new MessagingDialog();
            this.m_PageNavigator = messagingDialog.PageNavigator;
            messagingDialog.Closed += MessagingDialog_Closed;

            UI.AppMessaging.AccessionLockMessage message = new AccessionLockMessage(accessionOrder.MasterAccessionNo, System.Environment.MachineName, Business.User.SystemIdentity.Instance.User.UserName, AccessionLockMessageIdEnum.ASK);
            AppMessaging.LockRequestPage lockRequestPage = new AppMessaging.LockRequestPage(message);                
            lockRequestPage.RequestLock += LockRequestPage_RequestLock;

            messagingDialog.PageNavigator.Navigate(lockRequestPage);
            messagingDialog.Show();            
        }

        private void MessagingDialog_Closed(object sender, EventArgs e)
        {
            
        }

        private void LockRequestPage_RequestLock(object sender, CustomEventArgs.AccessionLockMessageReturnEventArgs e)
        {
            UI.AppMessaging.AccessionLockMessage message = new AccessionLockMessage(e.Message.MasterAccessionNo, System.Environment.MachineName, Business.User.SystemIdentity.Instance.User.UserName, AccessionLockMessageIdEnum.ASK);
            ISubscriber subscriber = Business.RedisConnection.Instance.GetSubscriber();
            subscriber.Publish(message.MasterAccessionNo, JsonConvert.SerializeObject(message));
            this.ShowLockRequestSentPage(e.Message);
        }

        private void ShowLockRequestSentPage(UI.AppMessaging.AccessionLockMessage message)
        {
            LockRequestSentPage lockRequestSentPage = null;
            if(this.m_PageNavigatorWasPassedIn == true)
            {
                lockRequestSentPage = new LockRequestSentPage(message, System.Windows.Visibility.Collapsed, System.Windows.Visibility.Visible);
            }
            else
            {
                lockRequestSentPage = new LockRequestSentPage(message, System.Windows.Visibility.Visible, System.Windows.Visibility.Collapsed);
            }

            //lockRequestSentPage.Next += LockRequestSentPage_Next;
            this.m_PageNavigator.Navigate(lockRequestSentPage);
        }

        private void LockRequestSentPage_Next(object sender, CustomEventArgs.AccessionOrderReturnEventArgs e)
        {
            if (this.Next != null) this.Next(this, e);
        }        

        private void ShowResponseReceivedPage(AccessionLockMessage message)
        {
            LockRequestResponseReceivedPage lockRequestResponseReceivedPage = null;
            if(this.m_PageNavigatorWasPassedIn == false)
            {
                lockRequestResponseReceivedPage = new LockRequestResponseReceivedPage(message, System.Windows.Visibility.Visible, System.Windows.Visibility.Collapsed);
            }
            else
            {
                lockRequestResponseReceivedPage = new LockRequestResponseReceivedPage(message, System.Windows.Visibility.Collapsed, System.Windows.Visibility.Visible);
            }

            this.m_PageNavigator.Navigate(lockRequestResponseReceivedPage);            
        }        

        public static MessagingPath Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new MessagingPath();
                    }
                }

                return instance;
            }
        }
    }
}
