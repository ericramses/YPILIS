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
	public partial class OrderDetailTypePage : Page, INotifyPropertyChanged, YellowstonePathology.Business.Interface.IPersistPageChanges
    {
        public event PropertyChangedEventHandler PropertyChanged;
		public delegate void ReturnEventHandler(object sender, YellowstonePathology.YpiConnect.Client.PageNavigationReturnEventArgs e);
		public event ReturnEventHandler Return;

		private YellowstonePathology.Business.ClientOrder.Model.ClientOrder m_ClientOrder;
        private YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetailTypeCollection m_ClientOrderDetailTypCollection;
		private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;

		public OrderDetailTypePage(YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder, YellowstonePathology.Business.Persistence.ObjectTracker objectTracker)
        {
			this.m_ClientOrder = clientOrder;
            this.m_ClientOrderDetailTypCollection = new Business.ClientOrder.Model.ClientOrderDetailTypeCollection();
			this.m_ObjectTracker = objectTracker;          

			InitializeComponent();
            
            this.DataContext = this;
			this.Loaded += new RoutedEventHandler(OrderDetailTypePage_Loaded);            
        }

		private void OrderDetailTypePage_Loaded(object sender, RoutedEventArgs e)
        {
			UserInteractionMonitor.Instance.Register(this);
			this.ListBoxClientOrderDetailType.Focus();
		}

		protected void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		public YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetailTypeCollection ClientOrderDetailTypeCollection
		{
			get { return this.m_ClientOrderDetailTypCollection; }
		}

		public YellowstonePathology.Business.ClientOrder.Model.ClientOrder ClientOrder
		{
			get { return this.m_ClientOrder; }
		}

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
			if (this.ValidateAndUpdateBindingSources(true) == true)
			{                
				YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetailType clientOrderDetailType = (YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetailType)this.ListBoxClientOrderDetailType.SelectedItem;
				YellowstonePathology.YpiConnect.Client.PageNavigationReturnEventArgs args = new YellowstonePathology.YpiConnect.Client.PageNavigationReturnEventArgs(YellowstonePathology.YpiConnect.Client.PageNavigationDirectionEnum.Next, clientOrderDetailType);
				Return(this, args);
			}
		}        

		private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
			this.ValidateAndUpdateBindingSources(false);
			YellowstonePathology.YpiConnect.Client.PageNavigationReturnEventArgs args = new YellowstonePathology.YpiConnect.Client.PageNavigationReturnEventArgs(YellowstonePathology.YpiConnect.Client.PageNavigationDirectionEnum.Back, null);
			Return(this, args);
		}

		private bool ValidateAndUpdateBindingSources(bool showMessages)
		{
			bool result = true;
			if (this.ValidateOrderType(showMessages) == false)
			{
				result = false;
				if (showMessages == true)
				{
					return result;
				}
			}
			return result;
		}

		private bool ValidateOrderType(bool showMessages)
		{
			bool result = true;
			YellowstonePathology.Business.Rules.MethodResult methodResult = this.OrderDetailTypeIsSelected();
			if (methodResult.Success == false)
			{
				result = false;
				if (showMessages == true)
				{
					MessageBox.Show(methodResult.Message);
				}
			}
			return result;
		}

		private YellowstonePathology.Business.Rules.MethodResult OrderDetailTypeIsSelected()
		{
			YellowstonePathology.Business.Rules.MethodResult result = new Business.Rules.MethodResult();
			result.Success = true;
			if (this.ListBoxClientOrderDetailType.SelectedItem == null)
			{
				result.Success = false;
				result.Message = "Please select a specimen type.";
			}
			return result;
		}

		public bool OkToSaveOnNavigation(Type pageNavigatingTo)
		{

            bool result = false;
            if (this.m_ClientOrder.PanelSetId.HasValue == true)
            {
                result = true;
            }
            return result;
		}

		public bool OkToSaveOnClose()
		{
            bool result = false;
            if (this.m_ClientOrder.PanelSetId.HasValue == true)
            {
                result = true;
            }
            return result;
		}

		public void Save()
		{            
			YellowstonePathology.YpiConnect.Proxy.SubmitterServiceProxy proxy = new Proxy.SubmitterServiceProxy();
            YellowstonePathology.Business.Persistence.RemoteObjectTransferAgent remoteObjectTransferAgent = new YellowstonePathology.Business.Persistence.RemoteObjectTransferAgent();
            this.m_ObjectTracker.PrepareRemoteTransferAgent(this.m_ClientOrder, remoteObjectTransferAgent);
            proxy.Submit(remoteObjectTransferAgent);
		}

		public void UpdateBindingSources()
		{
			this.ValidateAndUpdateBindingSources(false);
		}
	}
}
