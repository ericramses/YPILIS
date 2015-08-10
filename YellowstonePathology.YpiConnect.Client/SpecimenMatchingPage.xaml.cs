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
using System.Windows.Forms;

namespace YellowstonePathology.YpiConnect.Client
{
    /// <summary>
	/// Interaction logic for ReportBrowserNotEnabledPage.xaml
    /// </summary>
	public partial class SpecimenMatchingPage : Page, YellowstonePathology.Shared.Interface.IPersistPageChanges
	{				
		private YellowstonePathology.Business.ClientOrder.Model.Shipment m_Shipment;
        private YellowstonePathology.Business.ClientOrder.Model.ClientOrder m_ClientOrder;
        private YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail m_ClientOrderDetail;
        private YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetailCollection m_ClientOrderDetailCollection;        

        public SpecimenMatchingPage(YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder, YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail, 
            YellowstonePathology.Business.ClientOrder.Model.Shipment shipment, 
            YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetailCollection clientOrderDetailCollection)
        {
			this.m_ClientOrder = clientOrder;
            this.m_ClientOrderDetail = clientOrderDetail;
			this.m_Shipment = shipment;
            this.m_ClientOrderDetailCollection = clientOrderDetailCollection;            

            InitializeComponent();
			this.DataContext = this;

			Loaded += new RoutedEventHandler(SpecimenMatchingPage_Loaded);
        }

		private void SpecimenMatchingPage_Loaded(object sender, RoutedEventArgs e)
		{
			UserInteractionMonitor.Instance.Register(this);
		}

		private void HyperlinkAddToShipment_Click(object sender, RoutedEventArgs e)
		{
            this.AddToShipment();
			this.GoBack();         
		}

		private void HyperlinkBack_Click(object sender, RoutedEventArgs e)
		{
			this.GoBack();
		}				

        public YellowstonePathology.Business.ClientOrder.Model.ClientOrder ClientOrder
        {
            get { return this.m_ClientOrder; }
        }

		public YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail ClientOrderDetail
		{
            get { return this.m_ClientOrderDetail; }
		}

		private void ButtonBack_Click(object sender, RoutedEventArgs e)
		{
			this.GoBack();
		}

		private void ButtonCorrect_Click(object sender, RoutedEventArgs e)
		{
            this.AddToShipment();
		    this.GoBack();         
		}

        private void AddToShipment()
        {
			YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new Business.Persistence.ObjectTracker();
            objectTracker.RegisterObject(this.m_ClientOrderDetail);

            this.m_ClientOrderDetail.Shipped = this.m_Shipment.Shipped;
            this.m_ClientOrderDetail.ShipDate = this.m_Shipment.ShipDate;
            this.m_ClientOrderDetail.ShipmentId = this.m_Shipment.ShipmentId;
            this.m_ClientOrderDetailCollection.Add(this.m_ClientOrderDetail);

            YellowstonePathology.YpiConnect.Proxy.SubmitterServiceProxy proxy = new Proxy.SubmitterServiceProxy();
			YellowstonePathology.Business.Persistence.RemoteObjectTransferAgent remoteObjectTransferAgent = new Business.Persistence.RemoteObjectTransferAgent();
            objectTracker.PrepareRemoteTransferAgent(this.m_ClientOrderDetail, remoteObjectTransferAgent);
            proxy.Submit(remoteObjectTransferAgent);
        }

		private void GoBack()
		{
			ApplicationNavigator.ApplicationContentFrame.NavigationService.GoBack();
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
