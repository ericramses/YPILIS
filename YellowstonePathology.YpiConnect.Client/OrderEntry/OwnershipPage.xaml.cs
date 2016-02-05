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
using System.Xml;

namespace YellowstonePathology.YpiConnect.Client.OrderEntry
{
    /// <summary>
    /// Interaction logic for OrderEntryWindow.xaml
    /// </summary>
	public partial class OwnershipPage : Page, YellowstonePathology.Business.Interface.IPersistPageChanges, INotifyPropertyChanged
    {
        public delegate void PropertyChangedNotificationHandler(String info);
        public event PropertyChangedEventHandler PropertyChanged;

		public delegate void ReturnEventHandler(object sender, YellowstonePathology.YpiConnect.Client.PageNavigationReturnEventArgs e);
		public event ReturnEventHandler Return;

		private const string IsNotOwnerInstructions = "To Take ownership click \"Take Ownership\" and then click \"Finish\"; or click \"Back\" to cancel.";
		private const string IsOwnerInstructions = "You are the current owner of this case, click \"Back\" to go back.";

		private YellowstonePathology.Business.ClientOrder.Model.ClientOrder m_ClientOrder;
		private bool m_CurrentUserIsOwner;
		private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;

		public OwnershipPage(YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder, YellowstonePathology.Business.Persistence.ObjectTracker objectTracker)
        {
			this.m_ClientOrder = clientOrder;
			this.m_ObjectTracker = objectTracker;
			if (YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount.DisplayName == this.m_ClientOrder.OrderedBy)
			{
				this.m_CurrentUserIsOwner = true;
			}

            InitializeComponent();
            
            this.DataContext = this;
			this.Loaded += new RoutedEventHandler(OwnershipPage_Loaded);            
        }

		private void OwnershipPage_Loaded(object sender, RoutedEventArgs e)
        {
			UserInteractionMonitor.Instance.Register(this);
		}

		public string Instructions
		{
			get
			{
				string result = IsNotOwnerInstructions;
				if (this.m_CurrentUserIsOwner == true)
				{
					result = IsOwnerInstructions;
				}
				return result;
			}
		}

		public bool CurrentUserIsOwner
		{
			get { return this.m_CurrentUserIsOwner; }
		}

		public Visibility FinishButtonVisibility
		{
			get
			{
				Visibility result = System.Windows.Visibility.Visible;
				if (this.m_CurrentUserIsOwner == true)
				{
					result = System.Windows.Visibility.Hidden;
				}
				return result;
			}
		}

		public YellowstonePathology.Business.ClientOrder.Model.ClientOrder ClientOrder
		{
			get { return this.m_ClientOrder; }
		}

		public YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount CurrentUser
		{
			get { return YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount; }
		}

		private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
			YellowstonePathology.YpiConnect.Client.PageNavigationReturnEventArgs args = new YellowstonePathology.YpiConnect.Client.PageNavigationReturnEventArgs(YellowstonePathology.YpiConnect.Client.PageNavigationDirectionEnum.Back, null);
			Return(this, args);
        }

		private void ButtonFinish_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.YpiConnect.Client.PageNavigationReturnEventArgs args = new YellowstonePathology.YpiConnect.Client.PageNavigationReturnEventArgs(YellowstonePathology.YpiConnect.Client.PageNavigationDirectionEnum.Next, null);
			Return(this, args);
		}

		private void ButtonTakeOwnerShip_Click(object sender, RoutedEventArgs e)
        {
			this.m_ClientOrder.OrderedBy = YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount.DisplayName;
			this.m_CurrentUserIsOwner = true;
            this.NotifyPropertyChanged("ClientOrder");
		}

		public bool OkToSaveOnNavigation(Type pageNavigatingTo)
		{
			bool result = true;
			return result;
		}

		public bool OkToSaveOnClose()
		{
			return true;
		}

		public void Save(bool releaseLock)
		{			
            YellowstonePathology.YpiConnect.Proxy.SubmitterServiceProxy proxy = new Proxy.SubmitterServiceProxy();
			YellowstonePathology.Business.Persistence.RemoteObjectTransferAgent remoteObjectTransferAgent = new Business.Persistence.RemoteObjectTransferAgent();
            this.m_ObjectTracker.PrepareRemoteTransferAgent(this.m_ClientOrder, remoteObjectTransferAgent);
            proxy.Submit(remoteObjectTransferAgent);
		}

		public void UpdateBindingSources()
		{

		}

        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
	}
}
