using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace YellowstonePathology.YpiConnect.Client
{
    /// <summary>
    /// Interaction logic for LoginDialog.xaml
    /// </summary>
	public partial class MessagePage : Page, INotifyPropertyChanged, YellowstonePathology.Business.Interface.IPersistPageChanges
    {
		public delegate void PropertyChangedNotificationHandler(String info);
        public event PropertyChangedEventHandler PropertyChanged;

		private YellowstonePathology.YpiConnect.Contract.Message m_Message;

		public MessagePage(YellowstonePathology.YpiConnect.Contract.Message message)
		{
			this.Message = message;
			InitializeComponent();

			this.DataContext = this.Message;

			Loaded += new RoutedEventHandler(MessagePage_Loaded);
		}

		void MessagePage_Loaded(object sender, RoutedEventArgs e)
		{
			UserInteractionMonitor.Instance.Register(this);
		}

		private void HyperlinkBack_Click(object sender, RoutedEventArgs e)
		{
			ApplicationNavigator.ApplicationContentFrame.NavigationService.GoBack();
		}

		public YellowstonePathology.YpiConnect.Contract.Message Message
		{
			get { return this.m_Message; }
			set
			{
				this.m_Message = value;
				NotifyPropertyChanged("Message");
			}
		}

        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

		private void ButtonSendMessage_Click(object sender, RoutedEventArgs e)
		{			
			this.SendMessage();
		}		

		private void SendMessage()
		{		
			Mouse.OverrideCursor = Cursors.Wait;
			YellowstonePathology.YpiConnect.Proxy.MessageServiceProxy messageServiceProxy = new Proxy.MessageServiceProxy();
			messageServiceProxy.Send(this.m_Message);
			Mouse.OverrideCursor = null;
			MessageSentPage messageSentPage = new MessageSentPage();
			ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(messageSentPage);		
		}       		

		public void Save()
		{
            //Do nothing.
		}

		public bool OkToSaveOnNavigation(Type pageNavigatingTo)
		{
			return false;
		}

		public bool OkToSaveOnClose()
		{
			return false;
		}

		public void UpdateBindingSources()
		{
		}
	}
}
