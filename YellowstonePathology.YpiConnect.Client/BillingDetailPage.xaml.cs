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
using System.Windows.Xps;
using System.Windows.Xps.Packaging;

namespace YellowstonePathology.YpiConnect.Client
{
    /// <summary>
	/// Interaction logic for SearchResultPage.xaml
    /// </summary>
	public partial class BillingDetailPage : Page, YellowstonePathology.Shared.Interface.IPersistPageChanges
    {
		private YellowstonePathology.YpiConnect.Contract.Billing.BillingAccession m_BillingAccession;
		private YellowstonePathology.YpiConnect.Contract.Billing.BillingDetail m_BillingDetail;
		private YellowstonePathology.YpiConnect.Contract.Billing.PanelSetOrderCPTCodeBillCollection m_NonClientPanelSetOrderCPTCodeBillCollection;
		private YellowstonePathology.YpiConnect.Contract.Billing.PanelSetOrderCPTCodeBillCollection m_MedicarePanelSetOrderCPTCodeBillCollection;

		public BillingDetailPage(YellowstonePathology.YpiConnect.Contract.Billing.BillingAccession billingAccession, YellowstonePathology.YpiConnect.Contract.Billing.BillingDetail billingDetail)
        {
            this.m_BillingAccession = billingAccession;
			this.m_BillingDetail = billingDetail;
			this.m_NonClientPanelSetOrderCPTCodeBillCollection = this.m_BillingDetail.PanelSetOrderCPTCodeBillCollection.GetNonClientCollection("Client");
			this.m_MedicarePanelSetOrderCPTCodeBillCollection = this.m_BillingDetail.PanelSetOrderCPTCodeBillCollection.GetMedicareCollection();

            InitializeComponent();

			this.DataContext = this;
			this.Loaded += new RoutedEventHandler(BillingDetailPage_Loaded);
        }

        public YellowstonePathology.YpiConnect.Contract.Billing.BillingAccession BillingAccession
        {
            get { return this.m_BillingAccession; }
        }

        public YellowstonePathology.YpiConnect.Contract.Billing.BillingDetail BillingDetail
        {
            get { return this.m_BillingDetail;}
        }

		public YellowstonePathology.YpiConnect.Contract.Billing.PanelSetOrderCPTCodeBillCollection NonClientPanelSetOrderCPTCodeBillCollection
		{
			get { return this.m_NonClientPanelSetOrderCPTCodeBillCollection; }
		}

		public YellowstonePathology.YpiConnect.Contract.Billing.PanelSetOrderCPTCodeBillCollection MedicarePanelSetOrderCPTCodeBillCollection
		{
			get { return this.m_MedicarePanelSetOrderCPTCodeBillCollection; }
		}

		private void BillingDetailPage_Loaded(object sender, RoutedEventArgs e)
        {
			UserInteractionMonitor.Instance.Register(this);
		}

        private void HyperlinkViewDocument_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink hyperLink = (Hyperlink)sender;
            YellowstonePathology.YpiConnect.Contract.RemoteFile remoteFile = (YellowstonePathology.YpiConnect.Contract.RemoteFile)hyperLink.Tag;

            if (remoteFile.IsDownloaded == false)
            {                
                YellowstonePathology.YpiConnect.Proxy.FileTransferServiceProxy fileTransferServiceProxy = new Proxy.FileTransferServiceProxy();
                fileTransferServiceProxy.Download(ref remoteFile, YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount);                
            }

            if (remoteFile.Extension == ".XPS")
            {
                XpsDocument xpsDocument = XpsDocumentHelper.FromMemoryStream(remoteFile.MemoryStream);
                XpsDocumentViewer xpsDocumentViewer = new XpsDocumentViewer();
                xpsDocumentViewer.LoadDocument(xpsDocument);
				xpsDocumentViewer.ShowDialog();
            }

            if (remoteFile.Extension == ".TIF")
            {
                TifDocumentViewer tifDocumentViewer = new TifDocumentViewer();
                tifDocumentViewer.Load(remoteFile.MemoryStream);
				tifDocumentViewer.ShowDialog();
            }            
        }

		private void HyperlinkBack_Click(object sender, RoutedEventArgs e)
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
