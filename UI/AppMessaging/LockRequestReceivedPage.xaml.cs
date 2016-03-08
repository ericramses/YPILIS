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
	public partial class LockRequestReceivedPage : UserControl, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
        
        private System.Messaging.Message m_Message;
        private MessageBody m_MessageBody;

        private System.Windows.Threading.DispatcherTimer m_DispatchTimer;
        private string m_CountDownMessage;
        private int m_CurrentCountDown;        

        public LockRequestReceivedPage(System.Messaging.Message message)
        {
            this.m_Message = message;
            this.m_MessageBody = (MessageBody)message.Body;

            InitializeComponent();
            DataContext = this;            
            
            this.StartCountDownTimer();
        }    
        
        public MessageBody MessageBody
        {
            get { return this.m_MessageBody; }
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
            }

            this.NotifyPropertyChanged("CountDownMessage");
        }                       		                       

        private void ButtonRespondTakeIt_Click(object sender, RoutedEventArgs e)
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

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
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
