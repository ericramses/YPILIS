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
	public partial class LockRequestResponseReceivedPage : UserControl, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

        private AccessionLockMessage m_Message;        

        public LockRequestResponseReceivedPage(AccessionLockMessage message, System.Windows.Visibility closeButtonVisibility, System.Windows.Visibility nextButtonVisibility)
        {
            this.m_Message = message;
            InitializeComponent();
            DataContext = this;

            this.ButtonClose.Visibility = closeButtonVisibility;
            this.ButtonNext.Visibility = nextButtonVisibility;
        }  
        
        public AccessionLockMessage Message
        {
            get { return this.m_Message; }
        }                                                                		                   

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            /*            
            if(MessageBody.LockWasReleased == true)
            {
                if (this.LockWasReleased != null) this.LockWasReleased(this, new EventArgs());
            }
            else
            {
                if (this.HoldYourHorses != null) this.HoldYourHorses(this, new EventArgs());
            } 
            */           
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
