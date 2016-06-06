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
	public partial class LockRequestSentPage : UserControl, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

        public delegate void NextEventHandler(object sender, UI.CustomEventArgs.AccessionLockMessageReturnEventArgs e);
        public event NextEventHandler Next;        

        private System.Windows.Threading.DispatcherTimer m_DispatchTimer;
        private string m_CountDownMessage;
        private int m_CurrentCountDown;        
        private string m_DisplayMessage;

        public LockRequestSentPage(string username, string computerName, string masterAccessionNo, System.Windows.Visibility closeButtonVisibility, System.Windows.Visibility nextButtonVisibility)
		{
            this.m_DisplayMessage = "A resquest was sent to " + computerName + "\\" + username + " for the lock on " + masterAccessionNo + ".";

            InitializeComponent();

            DataContext = this;
            this.ButtonClose.Visibility = closeButtonVisibility;
            this.ButtonNext.Visibility = nextButtonVisibility;
            this.StartCountDownTimer();        
		}

        public string DisplayMessage
        {
            get { return this.m_DisplayMessage; }
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
            this.m_CountDownMessage = "Please wait " + this.m_CurrentCountDown ;

            if(this.m_CurrentCountDown == 0)
            {
                this.m_CountDownMessage = string.Empty;
                this.m_DispatchTimer.Stop();                                
            }

            this.NotifyPropertyChanged("CountDownMessage");
        }                               		                           

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
        	Window.GetWindow(this).Close();
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            //if (this.Next != null) this.Next(this, new CustomEventArgs.AccessionLockMessageReturnEventArgs()
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
