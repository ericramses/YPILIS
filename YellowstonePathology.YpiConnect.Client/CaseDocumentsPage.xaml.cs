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
	/// Interaction logic for ReportBrowserListPage.xaml
    /// </summary>
	public partial class CaseDocumentsPage : PageFunction<Type>, YellowstonePathology.Shared.Interface.IPersistPageChanges
    {
        YellowstonePathology.YpiConnect.Contract.RemoteFileList m_CaseDocumentList;

        public CaseDocumentsPage(YellowstonePathology.YpiConnect.Contract.RemoteFileList caseDocumentList)
        {
            this.m_CaseDocumentList = caseDocumentList;
            InitializeComponent();
            this.DataContext = this.m_CaseDocumentList;

			Loaded += new RoutedEventHandler(CaseDocumentsPage_Loaded);
        }

		void CaseDocumentsPage_Loaded(object sender, RoutedEventArgs e)
		{
			UserInteractionMonitor.Instance.Register(this);
		}

		private void HyperlinkGeneral_Click(object sender, RoutedEventArgs e)
		{
			OnReturn(new ReturnEventArgs<Type>(typeof(LeukemiaLymphomaSignoutPage)));
		}

		private void HyperlinkDocuments_Click(object sender, RoutedEventArgs e)
		{
		}

		private void HyperlinkGating_Click(object sender, RoutedEventArgs e)
		{
			OnReturn(new ReturnEventArgs<Type>(typeof(LeukemiaLymphomaGatingPage)));
		}

		private void HyperlinkMarkers_Click(object sender, RoutedEventArgs e)
		{
			OnReturn(new ReturnEventArgs<Type>(typeof(LeukemiaLymphomaMarkersPage)));
		}

		private void HyperlinkResults_Click(object sender, RoutedEventArgs e)
		{
			OnReturn(new ReturnEventArgs<Type>(typeof(LeukemiaLymphomaResultPage)));
		}

		private void HyperlinkCoding_Click(object sender, RoutedEventArgs e)
		{

		}

		private void HyperlinkAmendments_Click(object sender, RoutedEventArgs e)
		{

		}

		private void HyperlinkViewReport_Click(object sender, RoutedEventArgs e)
		{
			OnReturn(new ReturnEventArgs<Type>(typeof(LeukemiaLymphomaReportPage)));
		}

		private void HyperlinkFinal_Click(object sender, RoutedEventArgs e)
		{
			OnReturn(new ReturnEventArgs<Type>(typeof(CaseFinalPage)));
		}

		private void HyperlinkCaseList_Click(object sender, RoutedEventArgs e)
		{
			OnReturn(new ReturnEventArgs<Type>(typeof(PathologistSignoutPage)));
		}

		public void Save()
		{
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

            if (remoteFile.Extension == ".PDF")
            {
                remoteFile.SaveTempPdf();                
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo processStartInfo = new System.Diagnostics.ProcessStartInfo(remoteFile.TempFileName);
                process.StartInfo = processStartInfo;
                process.Start();
            }
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
