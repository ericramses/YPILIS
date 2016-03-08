using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.AppMessaging
{
	public class MessagingPath
	{
        private static volatile MessagingPath instance;
        private static object syncRoot = new Object();

        private bool m_DialogIsActive;
        private Navigation.PageNavigator m_PageNavigator;

        static MessagingPath()
        {
            
        }        

        private MessagingPath()
        {            
            this.m_DialogIsActive = false;
        }       

        public void StartRequestReceived(System.Messaging.Message message)
        {
            if (this.m_DialogIsActive == false)
            {
                MessagingDialog messagingDialog = new MessagingDialog();
                this.m_PageNavigator = messagingDialog.PageNavigator;
                messagingDialog.Closed += MessagingDialog_Closed;

                AppMessaging.LockRequestReceivedPage lockRequestReceivedPage = new AppMessaging.LockRequestReceivedPage(message);                
                messagingDialog.PageNavigator.Navigate(lockRequestReceivedPage);
                messagingDialog.Show();
                this.m_DialogIsActive = true;
            }
        }

        public void StartSendRequest(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, Navigation.PageNavigator pageNavigator)
        {
            this.m_PageNavigator = pageNavigator;
            MessageQueues.Instance.SendLockReleaseRequest(accessionOrder);
            this.ShowLockRequestSentPage(accessionOrder);
        }

        public void Start(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {            
            if(this.m_DialogIsActive == false)
            {
                MessagingDialog messagingDialog = new MessagingDialog();
                this.m_PageNavigator = messagingDialog.PageNavigator;
                messagingDialog.Closed += MessagingDialog_Closed;

                AppMessaging.LockRequestPage lockRequestPage = new AppMessaging.LockRequestPage(accessionOrder);                
                lockRequestPage.RequestLock += LockRequestPage_RequestLock;

                messagingDialog.PageNavigator.Navigate(lockRequestPage);
                messagingDialog.Show();
                this.m_DialogIsActive = true;
            }
        }

        private void MessagingDialog_Closed(object sender, EventArgs e)
        {
            this.m_DialogIsActive = false;
        }

        private void LockRequestPage_RequestLock(object sender, CustomEventArgs.AccessionOrderReturnEventArgs e)
        {
            MessageQueues.Instance.SendLockReleaseRequest(e.AccessionOrder);
            this.ShowLockRequestSentPage(e.AccessionOrder);
        }

        private void ShowLockRequestSentPage(Business.Test.AccessionOrder accessionOrder)
        {
            LockRequestSentPage lockRequestSentPage = new LockRequestSentPage(accessionOrder);
            lockRequestSentPage.ShowResponseReceivedPage += LockRequestSentPage_ShowResponseReceivedPage;
            this.m_PageNavigator.Navigate(lockRequestSentPage);
        }

        private void LockRequestSentPage_ShowResponseReceivedPage(object sender, CustomEventArgs.MessageReturnEventArgs e)
        {                        
            LockRequestResponseReceivedPage lockRequestResponseReceivedPage = new LockRequestResponseReceivedPage(e.Message);
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
