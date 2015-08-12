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
using System.Collections.ObjectModel;

namespace YellowstonePathology.YpiConnect.Client
{
    /// <summary>
	/// Interaction logic for ShipmentDetailsPage.xaml
    /// </summary>
	public partial class ShipmentDetailsPage : Page, INotifyPropertyChanged, YellowstonePathology.Business.Interface.IPersistPageChanges
    {
		public delegate void PropertyChangedNotificationHandler(String info);
		public event PropertyChangedEventHandler PropertyChanged;		
		
		private string m_ContainerId;

		private YellowstonePathology.Business.ClientOrder.Model.Shipment m_Shipment;
        private YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetailCollection m_ClientOrderDetailCollection;
		private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;

		public ShipmentDetailsPage(YellowstonePathology.Business.ClientOrder.Model.Shipment shipment, YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetailCollection clientOrderDetailCollection, YellowstonePathology.Business.Persistence.ObjectTracker objectTracker)
        {
			this.m_Shipment = shipment;
            this.m_ClientOrderDetailCollection = clientOrderDetailCollection;
            this.m_ObjectTracker = objectTracker;                      

			InitializeComponent();

			DataContext = this;

			Loaded += new RoutedEventHandler(ShipmentDetailsPage_Loaded);
            this.TextBoxContainerId.KeyUp +=new KeyEventHandler(TextBoxContainerId_KeyUp);
		}

		private void ShipmentDetailsPage_Loaded(object sender, RoutedEventArgs e)
		{
			UserInteractionMonitor.Instance.Register(this);
			this.m_ContainerId = string.Empty;
			this.TextBoxContainerId.Text = string.Empty;
			this.TextBoxContainerId.Focus();
		}

        public YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetailCollection ClientOrderDetailCollection
        {
            get { return this.m_ClientOrderDetailCollection; }
        }

		private void HyperlinkShipments_Click(object sender, RoutedEventArgs e)
		{
			ApplicationNavigator.ApplicationContentFrame.NavigationService.GoBack();
		}

		private void HyperlinkPackingSlip_Click(object sender, RoutedEventArgs e)
		{
			this.Save();

			YellowstonePathology.YpiConnect.Proxy.ClientOrderServiceProxy proxy = new Proxy.ClientOrderServiceProxy();
			System.Xml.Linq.XElement packingSlipElement = proxy.PackingSlipReport(this.m_Shipment.ShipmentId);
			PackingSlipData packingSlipData = new PackingSlipData(packingSlipElement);
			PackingSlip packingSlip = new PackingSlip(packingSlipData);

			XpsDocumentViewer xpsDocumentViewer = new XpsDocumentViewer();
			xpsDocumentViewer.LoadDocument(packingSlip.FixedDocument);
			xpsDocumentViewer.ShowDialog();
		}

		private void HyperlinkHome_Click(object sender, RoutedEventArgs e)
		{
			HomePage homePage = new HomePage();
			ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(homePage);            
		}

		private void HyperlinkOrders_Click(object sender, RoutedEventArgs e)
		{
			OrderEntry.OrderBrowserPage orderBrowserPage = new OrderEntry.OrderBrowserPage();
			ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(orderBrowserPage);
		}

		public YellowstonePathology.Business.ClientOrder.Model.Shipment Shipment
		{
			get { return this.m_Shipment; }
		}

		public string ContainerId
		{
			get { return this.m_ContainerId; }
			set
			{
				if (this.m_ContainerId != value)
				{
					this.m_ContainerId = value;
					this.NotifyPropertyChanged("ContainerId");
				}
			}
		}        

		private void TextBoxContainerId_KeyUp(object sender, KeyEventArgs e)
		{
            this.TextBoxContainerId.KeyUp -= TextBoxContainerId_KeyUp;
			if (this.m_ContainerId.Length == 40)
			{
                this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate()
                {
                    this.LocateContainerId();
                    this.ContainerId = string.Empty;
                }
                ));				            
			}
            this.TextBoxContainerId.KeyUp += new KeyEventHandler(TextBoxContainerId_KeyUp);
		}

		private void LocateContainerId()
		{            
			if (this.m_ClientOrderDetailCollection.ExistsByContainerId(this.m_ContainerId) == true)
			{
                MessageBox.Show("The container scanned is already in the shipment.", "Shipment", MessageBoxButton.OK);                    
			}
			else
			{
				YellowstonePathology.YpiConnect.Proxy.ClientOrderServiceProxy proxy = new Proxy.ClientOrderServiceProxy();
				YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder = proxy.GetClientOrderByContainerId(this.m_ContainerId);

				if(clientOrder != null)
                {
                    YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail = clientOrder.ClientOrderDetailCollection.GetByContainerId(this.m_ContainerId);
					SpecimenMatchingPage specimenMatchingPage = new SpecimenMatchingPage(clientOrder, clientOrderDetail, this.m_Shipment, this.m_ClientOrderDetailCollection);
					ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(specimenMatchingPage);
				}
				else
				{
					MessageBox.Show("The container id scanned was not found.");
				}
			}                              
		}

		public void SaveChangesCommandHandler(object target, ExecutedRoutedEventArgs args)
		{
			this.Save();
		}

		public bool OkToSaveOnNavigation(Type pageNavigatingTo)
		{
			return true;
		}

		public bool OkToSaveOnClose()
		{
			return true;
		}

		public void Save()
		{            
            YellowstonePathology.YpiConnect.Proxy.SubmitterServiceProxy proxy = new Proxy.SubmitterServiceProxy();
			YellowstonePathology.Business.Persistence.RemoteObjectTransferAgent remoteObjectTransferAgent = new YellowstonePathology.Business.Persistence.RemoteObjectTransferAgent();
            this.m_ObjectTracker.PrepareRemoteTransferAgent(this.m_Shipment, remoteObjectTransferAgent);
            proxy.Submit(remoteObjectTransferAgent);            
		}

		private void ButtonShip_Click(object sender, RoutedEventArgs e)
		{            
			this.m_Shipment.Shipped = true;
			this.m_Shipment.ShipDate = DateTime.Now;
			foreach (YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail in this.m_ClientOrderDetailCollection)
			{
				clientOrderDetail.Shipped = true;
				clientOrderDetail.ShipDate = this.m_Shipment.ShipDate;
			}
			this.Save();
			MessageBox.Show("The shipment has been marked as shipped.");         
		}

		protected void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		public void UpdateBindingSources()
		{

		}
	}
}
