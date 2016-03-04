using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace YellowstonePathology.UI.AppMessaging
{	
	public partial class MessagingPage : UserControl, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
        
        private System.Messaging.Message m_Message;
        private Business.Test.AccessionOrder m_AccessionOrder;

        private System.Windows.Threading.DispatcherTimer m_DispatchTimer;
        private string m_CountDownMessage;
        private int m_CurrentCountDown;

        private string m_MasterAccessionNo;
        private string m_LockAquiredByUserName;
        private string m_LockAquiredByHostName;
        private Nullable<DateTime> m_TimeLockAquired;        

        public MessagingPage(Business.Test.AccessionOrder accessionOrder)
		{
            this.m_AccessionOrder = accessionOrder;
            this.m_MasterAccessionNo = accessionOrder.MasterAccessionNo;
            this.m_LockAquiredByUserName = accessionOrder.LockAquiredByUserName;
            this.m_LockAquiredByHostName = accessionOrder.LockAquiredByHostName;
            this.m_TimeLockAquired = accessionOrder.TimeLockAquired;

            InitializeComponent();
            DataContext = this;

            if (this.m_AccessionOrder.IsLockAquiredByMe == false)
            {
                this.ButtonRequest.Visibility = Visibility.Visible;
            }
            else
            {
                this.ButtonTakeIt.Visibility = Visibility.Visible;
                this.ButtonHoldYourHorses.Visibility = Visibility.Visible;
            }
                        
            AppMessaging.MessageQueues.Instance.ResponseReceived += Instance_ResponseReceived;
		}

        public MessagingPage(System.Messaging.Message message)
        {
            this.m_Message = message;

            MessageBody messageBody = (MessageBody)message.Body;
            this.m_LockAquiredByUserName = messageBody.LockAquiredByUserName;
            this.m_LockAquiredByHostName = messageBody.LockAquiredByHostName;
            this.m_MasterAccessionNo = messageBody.MasterAccessionNo;
            this.m_TimeLockAquired = messageBody.TimeLockAquired;

            InitializeComponent();
            DataContext = this;
            AppMessaging.MessageQueues.Instance.ResponseReceived += Instance_ResponseReceived;

            this.ButtonTakeIt.Visibility = Visibility.Visible;
            this.ButtonHoldYourHorses.Visibility = Visibility.Visible;
            this.StartCountDownTimer();
        }

        private void Instance_ResponseReceived(object sender, EventArgs e)
        {
            //MessageBox.Show("Hello");
        }                

        public string MasterAccessionNo
        {
            get { return this.m_MasterAccessionNo; }
        }

        public string LockAquiredByUserName
        {
            get { return this.m_LockAquiredByUserName; }
        }

        public string LockAquiredByHostName
        {
            get { return this.m_LockAquiredByHostName; }
        }

        public Nullable<DateTime> TimeLockAquired
        {
            get { return this.m_TimeLockAquired; }
        }

        private void StartCountDownTimer()
        {
            this.m_CurrentCountDown = 15;

            this.m_DispatchTimer = new System.Windows.Threading.DispatcherTimer();
            this.m_DispatchTimer.Interval = new TimeSpan(0, 0, 1);
            this.m_DispatchTimer.Tick += DispatchTimer_Tick;
            this.m_DispatchTimer.Start();
        }

        public string CountDownMessage
        {
            get { return this.m_CountDownMessage; }            
        }

        private void DispatchTimer_Tick(object sender, EventArgs e)
        {
            this.m_CurrentCountDown -= 1;
            this.m_CountDownMessage = "You have " + this.m_CurrentCountDown + " seconds to respond.";

            if(this.m_CurrentCountDown == 0)
            {
                this.m_CountDownMessage = string.Empty;
                this.m_DispatchTimer.Stop();                
                MessageQueues.Instance.SendLockReleaseResponse(this.m_Message, true); 
                Window window = Window.GetWindow(this);
                window.Close();
            }

            this.NotifyPropertyChanged("CountDownMessage");
        }

        public Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
        }

        public MessageQueues MessageQueues
        {
            get { return MessageQueues.Instance; }
        }
               

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}        

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
        
        }

        private void ButtonAskToTakeCase_Click(object sender, RoutedEventArgs e)
        {            
            MessageQueues.Instance.SendLockReleaseRequest(this.m_AccessionOrder);
            //this.StartCountDownTimer();
        }

        private void ButtonRespondTakeCase_Click(object sender, RoutedEventArgs e)
        {            
            this.m_DispatchTimer.Stop();
            MessageQueues.Instance.SendLockReleaseResponse(this.m_Message, true);            
            Window window = Window.GetWindow(this);
            window.Close();
        }

        private void ButtonRespondHoldYourHorses_Click(object sender, RoutedEventArgs e)
        {
            this.m_DispatchTimer.Stop();
            MessageQueues.Instance.SendLockReleaseResponse(this.m_Message, false);
            Window window = Window.GetWindow(this);
            window.Close();
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }
    }
}
