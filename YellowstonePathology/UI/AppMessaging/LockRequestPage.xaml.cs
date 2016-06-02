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
    public partial class LockRequestPage : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;        

        public delegate void RequestLockEventHandler(object sender, UI.CustomEventArgs.AccessionLockMessageReturnEventArgs e);
        public event RequestLockEventHandler RequestLock;

        private Business.Test.AccessionOrder m_AccessionOrder;
        private AccessionLockMessage m_Message;        

        public LockRequestPage(AccessionLockMessage message)
        {
            this.m_Message = message;
            InitializeComponent();
            DataContext = this;
        }

        public AccessionLockMessage Message
        {
            get { return this.m_Message; }
        }           

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}                

        private void ButtonAskToTakeCase_Click(object sender, RoutedEventArgs e)
        {
            if (this.RequestLock != null) this.RequestLock(this, new CustomEventArgs.AccessionLockMessageReturnEventArgs(this.m_Message));              
        }        

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
        	Window.GetWindow(this).Close();
        }
    }
}
