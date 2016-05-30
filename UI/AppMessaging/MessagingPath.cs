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

        public delegate void LockWasReleasedEventHandler(object sender, EventArgs e);
        public event LockWasReleasedEventHandler LockWasReleased;

        public delegate void HoldYourHorsesEventHandler(object sender, EventArgs e);
        public event HoldYourHorsesEventHandler HoldYourHorses;

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

        public void StartRequestReceived(string message)
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
            //MessageQueues.Instance.SendLockReleaseRequest(accessionOrder);
            this.ShowLockRequestSentPage(accessionOrder);
        }

        public void Start(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {                        
            MessagingDialog messagingDialog = new MessagingDialog();
            this.m_PageNavigator = messagingDialog.PageNavigator;
            messagingDialog.Closed += MessagingDialog_Closed;

            AppMessaging.LockRequestPage lockRequestPage = new AppMessaging.LockRequestPage(accessionOrder);                
            lockRequestPage.RequestLock += LockRequestPage_RequestLock;

            messagingDialog.PageNavigator.Navigate(lockRequestPage);
            messagingDialog.Show();            
        }

        private void MessagingDialog_Closed(object sender, EventArgs e)
        {
            
        }

        private void LockRequestPage_RequestLock(object sender, CustomEventArgs.AccessionOrderReturnEventArgs e)
        {
            //MessageQueues.Instance.SendLockReleaseRequest(e.AccessionOrder);
            //this.ShowLockRequestSentPage(e.AccessionOrder);
        }

        private void ShowLockRequestSentPage(Business.Test.AccessionOrder accessionOrder)
        {
            LockRequestSentPage lockRequestSentPage = null;
            if(this.m_PageNavigatorWasPassedIn == true)
            {
                lockRequestSentPage = new LockRequestSentPage(accessionOrder, System.Windows.Visibility.Collapsed, System.Windows.Visibility.Visible);
            }
            else
            {
                lockRequestSentPage = new LockRequestSentPage(accessionOrder, System.Windows.Visibility.Visible, System.Windows.Visibility.Collapsed);
            }

            //lockRequestSentPage.ShowResponseReceivedPage += LockRequestSentPage_ShowResponseReceivedPage;
            //lockRequestSentPage.Next += LockRequestSentPage_Next;
            //this.m_PageNavigator.Navigate(lockRequestSentPage);
        }

        private void LockRequestSentPage_Next(object sender, UI.CustomEventArgs.AccessionOrderReturnEventArgs e)
        {
            if (this.Next != null) this.Next(this, e);
        }

        private void LockRequestSentPage_ShowResponseReceivedPage(object sender, EventArgs e)
        {
            LockRequestResponseReceivedPage lockRequestResponseReceivedPage = null;
            if(this.m_PageNavigatorWasPassedIn == false)
            {
                //lockRequestResponseReceivedPage = new LockRequestResponseReceivedPage(e.Message, System.Windows.Visibility.Visible, System.Windows.Visibility.Collapsed);
            }
            else
            {
                //lockRequestResponseReceivedPage = new LockRequestResponseReceivedPage(e.Message, System.Windows.Visibility.Collapsed, System.Windows.Visibility.Visible);
            }

            lockRequestResponseReceivedPage.LockWasReleased += LockRequestResponseReceivedPage_LockWasReleased;
            lockRequestResponseReceivedPage.HoldYourHorses += LockRequestResponseReceivedPage_HoldYourHorses;
            this.m_PageNavigator.Navigate(lockRequestResponseReceivedPage);            
        }

        private void LockRequestResponseReceivedPage_HoldYourHorses(object sender, EventArgs e)
        {
            if (HoldYourHorses != null) this.HoldYourHorses(this, new EventArgs());
        }

        private void LockRequestResponseReceivedPage_LockWasReleased(object sender, EventArgs e)
        {
            if (this.LockWasReleased != null) this.LockWasReleased(this, new EventArgs());
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
