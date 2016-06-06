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

        public delegate void LockWasReleasedEventHandler(object sender, EventArgs e);
        public event LockWasReleasedEventHandler LockWasReleased;

        public delegate void HoldYourHorsesEventHandler(object sender, EventArgs e);
        public event HoldYourHorsesEventHandler HoldYourHorses;

        private Navigation.PageNavigator m_PageNavigator;
        private bool m_PageNavigatorWasPassedIn;

        private List<Action> m_LockAquiredActionList;
        private List<Action> m_LockReleasedActionList;

        private MessagingDialog m_MessagingDialog;

        static MessagingPath()
        {
            
        }        

        private MessagingPath()
        {                        
            this.m_PageNavigatorWasPassedIn = false;
            this.m_LockAquiredActionList = new List<Action>();
            this.m_LockReleasedActionList = new List<Action>();
        }

        public List<Action> LockAquiredActionList
        {
            get { return this.m_LockAquiredActionList; }
        }

        public List<Action> LockReleasedActionList
        {
            get { return this.m_LockReleasedActionList; }
        }

        public void HandleMessageRecieved(string channel, AccessionLockMessage message)
        {            
            if(message.DidISendThis() == false)
            {
                switch (message.MessageId)
                {
                    case AccessionLockMessageIdEnum.ASK:
                        this.HandleASKRecieved(message);
                        break;
                    case AccessionLockMessageIdEnum.HOLD:
                        this.ShowResponseReceivedPage(message);
                        break;
                    case AccessionLockMessageIdEnum.GIVE:
                        YellowstonePathology.Business.Persistence.DocumentGateway.Instance.RefreshAccessionOrder(message.MasterAccessionNo);
                        this.ShowResponseReceivedPage(message);
                        this.RunLockAquiredActionList();
                        break;
                }
            }            
        }     
        
        private void RunLockReleasedActionList()
        {
            foreach(Action action in this.m_LockReleasedActionList)
            {
                action.Invoke();
            }
        }

        private void RunLockAquiredActionList()
        {
            foreach (Action action in this.m_LockAquiredActionList)
            {
                action.Invoke();
            }
        }

        public void HandleASKRecieved(AccessionLockMessage message)
        {
            this.m_MessagingDialog = new MessagingDialog();
            this.m_PageNavigator = this.m_MessagingDialog.PageNavigator;
            this.m_MessagingDialog.Closed += MessagingDialog_Closed;
            

            AppMessaging.LockRequestReceivedPage lockRequestReceivedPage = new AppMessaging.LockRequestReceivedPage(message);
            lockRequestReceivedPage.Take += LockRequestReceivedPage_Take;
            lockRequestReceivedPage.Hold += LockRequestReceivedPage_Hold;
            this.m_MessagingDialog.PageNavigator.Navigate(lockRequestReceivedPage);
            this.m_MessagingDialog.Show();            
        }

        private void LockRequestReceivedPage_Hold(object sender, CustomEventArgs.AccessionLockMessageReturnEventArgs e)
        {
            UI.AppMessaging.AccessionLockMessage message = new AccessionLockMessage(e.Message.MasterAccessionNo, System.Environment.MachineName, Business.User.SystemIdentity.Instance.User.UserName, AccessionLockMessageIdEnum.HOLD);
            ISubscriber subscriber = Business.RedisConnection.Instance.GetSubscriber();
            subscriber.Publish(message.MasterAccessionNo, JsonConvert.SerializeObject(message));
            this.m_MessagingDialog.Close();
        }

        private void LockRequestReceivedPage_Take(object sender, CustomEventArgs.AccessionLockMessageReturnEventArgs e)
        {            
            this.RunLockReleasedActionList();
            this.m_MessagingDialog.Close();

            UI.AppMessaging.AccessionLockMessage message = new AccessionLockMessage(e.Message.MasterAccessionNo, System.Environment.MachineName, Business.User.SystemIdentity.Instance.User.UserName, AccessionLockMessageIdEnum.GIVE);
            ISubscriber subscriber = Business.RedisConnection.Instance.GetSubscriber();
            subscriber.Publish(message.MasterAccessionNo, JsonConvert.SerializeObject(message));
        }

        public void StartSendRequest(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, Navigation.PageNavigator pageNavigator)
        {
            this.m_PageNavigator = pageNavigator;
            this.m_PageNavigatorWasPassedIn = true;

            UI.AppMessaging.AccessionLockMessage message = new AccessionLockMessage(accessionOrder.MasterAccessionNo, System.Environment.MachineName, Business.User.SystemIdentity.Instance.User.UserName, AccessionLockMessageIdEnum.ASK);
            ISubscriber subscriber = Business.RedisConnection.Instance.GetSubscriber();
            subscriber.Publish(message.MasterAccessionNo, JsonConvert.SerializeObject(message));

            this.ShowLockRequestSentPage(accessionOrder);
        }

        public void Start(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {                        
            MessagingDialog messagingDialog = new MessagingDialog();
            this.m_PageNavigator = messagingDialog.PageNavigator;
            messagingDialog.Closed += MessagingDialog_Closed;

            UI.AppMessaging.AccessionLockMessage message = new AccessionLockMessage(accessionOrder.MasterAccessionNo, accessionOrder.LockAquiredByHostName, accessionOrder.LockAquiredByUserName, AccessionLockMessageIdEnum.ASK);
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
            UI.AppMessaging.AccessionLockMessage message = new AccessionLockMessage(e.AccessionOrder.MasterAccessionNo, System.Environment.MachineName, Business.User.SystemIdentity.Instance.User.UserName, AccessionLockMessageIdEnum.ASK);
            ISubscriber subscriber = Business.RedisConnection.Instance.GetSubscriber();
            subscriber.Publish(message.MasterAccessionNo, JsonConvert.SerializeObject(message));
            this.ShowLockRequestSentPage(e.AccessionOrder);
        }

        private void ShowLockRequestSentPage(Business.Test.AccessionOrder accessionOrder)
        {
            LockRequestSentPage lockRequestSentPage = null;
            if(this.m_PageNavigatorWasPassedIn == true)
            {
                lockRequestSentPage = new LockRequestSentPage(accessionOrder.LockAquiredByUserName, accessionOrder.LockAquiredByHostName, accessionOrder.MasterAccessionNo, System.Windows.Visibility.Collapsed, System.Windows.Visibility.Visible);
            }
            else
            {
                lockRequestSentPage = new LockRequestSentPage(accessionOrder.LockAquiredByUserName, accessionOrder.LockAquiredByHostName, accessionOrder.MasterAccessionNo, System.Windows.Visibility.Visible, System.Windows.Visibility.Collapsed);
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

            lockRequestResponseReceivedPage.LockWasReleased += LockRequestResponseReceivedPage_LockWasReleased;
            lockRequestResponseReceivedPage.HoldYourHorses += LockRequestResponseReceivedPage_HoldYourHorses;
            this.m_PageNavigator.Navigate(lockRequestResponseReceivedPage);            
        }

        private void LockRequestResponseReceivedPage_HoldYourHorses(object sender, EventArgs e)
        {
            this.HoldYourHorses(this, new EventArgs());
        }

        private void LockRequestResponseReceivedPage_LockWasReleased(object sender, EventArgs e)
        {
            this.LockWasReleased(this, new EventArgs());
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
