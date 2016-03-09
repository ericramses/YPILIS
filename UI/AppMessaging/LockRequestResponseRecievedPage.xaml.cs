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

        public delegate void LockAquiredEventHandler(object sender, EventArgs e);
        public event LockAquiredEventHandler LockAquired;

        private MessageBody m_MessageBody;        

        public LockRequestResponseReceivedPage(System.Messaging.Message message, System.Windows.Visibility closeButtonVisibility, System.Windows.Visibility nextButtonVisibility)
        {            
            this.m_MessageBody = (MessageBody)message.Body;                        
            InitializeComponent();
            DataContext = this;

            this.ButtonClose.Visibility = closeButtonVisibility;
            this.ButtonNext.Visibility = nextButtonVisibility;
        }  
        
        public MessageBody MessageBody
        {
            get { return this.m_MessageBody; }
        }                                                                		                   

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            if (this.LockAquired != null) this.LockAquired(this, new EventArgs());
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
