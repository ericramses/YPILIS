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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace YellowstonePathology.YpiConnect.Client
{
    /// <summary>
    /// Interaction logic for ReportBrowser.xaml
    /// </summary>
	public partial class ShippingPage : Page, INotifyPropertyChanged, YellowstonePathology.Business.Interface.IPersistPageChanges
    {
		public event PropertyChangedEventHandler PropertyChanged;
		
		private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;
        private YellowstonePathology.Business.ClientOrder.Model.ShipmentListItemCollection m_ShipmentListItemCollection;        

		public ShippingPage()
        {            
			InitializeComponent();
			this.DataContext = this;
			Loaded += new RoutedEventHandler(ShippingPage_Loaded);
        }

		private void ShippingPage_Loaded(object sender, RoutedEventArgs e)
		{
			UserInteractionMonitor.Instance.Register(this);
			this.GetShipments();
		}

		protected void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		private void HyperlinkShippingDetails_Click(object sender, RoutedEventArgs e)
        {
			if (this.ListViewShipments.SelectedItem != null)
			{
                YellowstonePathology.Business.ClientOrder.Model.ShipmentListItem shipmentListItem = (YellowstonePathology.Business.ClientOrder.Model.ShipmentListItem)this.ListViewShipments.SelectedItem;
                this.ShowShippingDetailsPage(shipmentListItem.ShipmentId);
			}
		}

        private void ShowShippingDetailsPage(string shipmentId)
        {            
            YellowstonePathology.YpiConnect.Proxy.ClientOrderServiceProxy proxy = new Proxy.ClientOrderServiceProxy();
            YellowstonePathology.Business.ClientOrder.Model.ShipmentReturnResult shipmentReturnResult = proxy.GetShipment(shipmentId);

			this.m_ObjectTracker = new Business.Persistence.ObjectTracker();
            this.m_ObjectTracker.RegisterObject(shipmentReturnResult.Shipment);
            ShipmentDetailsPage shipmentDetailsPage = new ShipmentDetailsPage(shipmentReturnResult.Shipment, shipmentReturnResult.ClientOrderDetailCollection, this.m_ObjectTracker);
            ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(shipmentDetailsPage);
        }

		private void HyperlinkPackingSlip_Click(object sender, RoutedEventArgs e)
        {
			if (this.ListViewShipments.SelectedItem != null)
			{
				this.Save();
				YellowstonePathology.Business.ClientOrder.Model.ShipmentListItem shipmentListItem = (YellowstonePathology.Business.ClientOrder.Model.ShipmentListItem)this.ListViewShipments.SelectedItem;
				YellowstonePathology.YpiConnect.Proxy.ClientOrderServiceProxy proxy = new Proxy.ClientOrderServiceProxy();
				System.Xml.Linq.XElement packingSlipElement = proxy.PackingSlipReport(shipmentListItem.ShipmentId);
				PackingSlipData packingSlipData = new PackingSlipData(packingSlipElement);
				PackingSlip packingSlip = new PackingSlip(packingSlipData);

				XpsDocumentViewer xpsDocumentViewer = new XpsDocumentViewer();
				xpsDocumentViewer.LoadDocument(packingSlip.FixedDocument);
				xpsDocumentViewer.ShowDialog();
			}
		}

        private void HyperlinkHome_Click(object sender, RoutedEventArgs e)
        {
			HomePage honmePage = new HomePage();
			ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(honmePage);
        }

		private void HyperlinkOrders_Click(object sender, RoutedEventArgs e)
		{
			OrderEntry.OrderBrowserPage orderBrowserPage = new OrderEntry.OrderBrowserPage();
			ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(orderBrowserPage);
		}

		private void GetShipments()
		{            
			YellowstonePathology.YpiConnect.Proxy.ClientOrderServiceProxy proxy = new Proxy.ClientOrderServiceProxy();
			this.m_ShipmentListItemCollection = proxy.GetShipmentListItemCollection(YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount.PrimaryClientId);
			NotifyPropertyChanged("ShipmentListItemCollection");         
		}

		public YellowstonePathology.Business.ClientOrder.Model.ShipmentListItemCollection ShipmentListItemCollection
		{
			get { return this.m_ShipmentListItemCollection; }
		}

		private void ListViewShipment_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (this.ListViewShipments.SelectedItem != null)
			{
				YellowstonePathology.Business.ClientOrder.Model.ShipmentListItem shipmentListItem = (YellowstonePathology.Business.ClientOrder.Model.ShipmentListItem)this.ListViewShipments.SelectedItem;
                this.ShowShippingDetailsPage(shipmentListItem.ShipmentId);
			}
		}

		private void ButtonNew_Click(object sender, RoutedEventArgs e)
		{
			int clientId = YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount.PrimaryClientId;
			string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            YellowstonePathology.Business.ClientOrder.Model.Shipment shipment = new Business.ClientOrder.Model.Shipment(objectId, clientId, YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount.DisplayName,
			    YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount.Client.ClientName);

			this.m_ObjectTracker = new Business.Persistence.ObjectTracker();
			this.m_ObjectTracker.RegisterRootInsert(shipment);

            YellowstonePathology.YpiConnect.Proxy.SubmitterServiceProxy proxy = new Proxy.SubmitterServiceProxy();
			YellowstonePathology.Business.Persistence.RemoteObjectTransferAgent remoteObjectTransferAgent = new Business.Persistence.RemoteObjectTransferAgent();
            this.m_ObjectTracker.PrepareRemoteTransferAgent(shipment, remoteObjectTransferAgent);
            proxy.Submit(remoteObjectTransferAgent);

            YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetailCollection clientOrderDetailCollection = new Business.ClientOrder.Model.ClientOrderDetailCollection();
			ShipmentDetailsPage shipmentDetailsPage = new ShipmentDetailsPage(shipment, clientOrderDetailCollection, this.m_ObjectTracker);
			ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(shipmentDetailsPage);
		}

		private void ButtonRemove_Click(object sender, RoutedEventArgs e)
		{            
			Control button = (Control)sender;
			YellowstonePathology.Business.ClientOrder.Model.ShipmentListItem shipmentListItem = (YellowstonePathology.Business.ClientOrder.Model.ShipmentListItem)button.Tag;
			this.SelectItemOnButtonClick(shipmentListItem);

			MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this shipment?", "Delete shipment", MessageBoxButton.YesNo, MessageBoxImage.Question);
			if (result == MessageBoxResult.Yes)
			{
				this.m_ObjectTracker = new Business.Persistence.ObjectTracker();

				YellowstonePathology.YpiConnect.Proxy.ClientOrderServiceProxy proxy = new Proxy.ClientOrderServiceProxy();
				YellowstonePathology.Business.ClientOrder.Model.ShipmentReturnResult shipmentReturnResult = proxy.GetShipment(shipmentListItem.ShipmentId);
                YellowstonePathology.YpiConnect.Proxy.SubmitterServiceProxy submitterProxy = new Proxy.SubmitterServiceProxy();

                foreach (YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail in shipmentReturnResult.ClientOrderDetailCollection)
                {
                    this.m_ObjectTracker.RegisterObject(clientOrderDetail);
                    clientOrderDetail.ShipDate = null;
                    clientOrderDetail.ShipmentId = null;
                    clientOrderDetail.Shipped = false;

					YellowstonePathology.Business.Persistence.RemoteObjectTransferAgent remoteObjectTransferAgent = new Business.Persistence.RemoteObjectTransferAgent();
                    this.m_ObjectTracker.PrepareRemoteTransferAgent(clientOrderDetail, remoteObjectTransferAgent);
                    submitterProxy.Submit(remoteObjectTransferAgent);
                }
				
				this.m_ObjectTracker.RegisterRootDelete(shipmentReturnResult.Shipment);
				YellowstonePathology.Business.Persistence.RemoteObjectTransferAgent remoteObjectTransferAgent2 = new Business.Persistence.RemoteObjectTransferAgent();
                this.m_ObjectTracker.PrepareRemoteTransferAgent(shipmentReturnResult.Shipment, remoteObjectTransferAgent2);
                submitterProxy.Submit(remoteObjectTransferAgent2);

				this.m_ShipmentListItemCollection.Remove(shipmentListItem);
			}         
		}

		private void SelectItemOnButtonClick(YellowstonePathology.Business.ClientOrder.Model.ShipmentListItem shipment)
		{
			for (int idx = 0; idx < this.ListViewShipments.Items.Count; idx++)
			{
				YellowstonePathology.Business.ClientOrder.Model.ShipmentListItem listShipment = (YellowstonePathology.Business.ClientOrder.Model.ShipmentListItem)this.ListViewShipments.Items[idx];
				if (listShipment.ShipmentId == shipment.ShipmentId)
				{
					this.ListViewShipments.SelectedIndex = idx;
					break;
				}
			}
		}

		public void Save()
        {
            
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
