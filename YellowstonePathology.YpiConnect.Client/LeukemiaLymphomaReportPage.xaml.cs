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
using System.Xml.Linq;

namespace YellowstonePathology.YpiConnect.Client
{
    /// <summary>
	/// Interaction logic for ReportBrowserListPage.xaml
    /// </summary>
	public partial class LeukemiaLymphomaReportPage : PageFunction<Type>, YellowstonePathology.Shared.Interface.IPersistPageChanges
    {
		private string m_ReportNo;
		private string m_MasterAccessionNo;

		public LeukemiaLymphomaReportPage(string reportNo, string masterAccessionNo)
        {
			this.m_ReportNo = reportNo;
			this.m_MasterAccessionNo = masterAccessionNo;

            InitializeComponent();

			this.DataContext = this;
			this.Loaded += new RoutedEventHandler(LeukemiaLymphomaSignoutPage_Loaded);            
        }
        
		public void LeukemiaLymphomaSignoutPage_Loaded(object sender, RoutedEventArgs e)
		{
			UserInteractionMonitor.Instance.Register(this);
			YellowstonePathology.YpiConnect.Proxy.FlowSignoutServiceProxy flowSignoutServiceProxy = new Proxy.FlowSignoutServiceProxy();
			XElement reportDocument = flowSignoutServiceProxy.GetLeukemiaLymphomaReportDocument(this.m_MasterAccessionNo);
			YellowstonePathology.Document.Result.Data.LeukemiaLymphomaReportData leukemialymphomaReportData = new Document.Result.Data.LeukemiaLymphomaReportData(this.m_ReportNo, reportDocument);
			YellowstonePathology.Document.Result.Xps.LeukemiaLymphomaReport leukemiaLymphomaReport = new Document.Result.Xps.LeukemiaLymphomaReport(leukemialymphomaReportData);

			System.Windows.Controls.DocumentViewer documentViewer = new System.Windows.Controls.DocumentViewer();
			documentViewer.Document = leukemiaLymphomaReport.FixedDocument;
			this.DocumentViewerControl.Content = documentViewer;
		}

		private void HyperlinkBack_Click(object sender, RoutedEventArgs e)
		{
			OnReturn(new ReturnEventArgs<Type>(typeof(LeukemiaLymphomaSignoutPage)));
		}

		public bool OkToSaveOnNavigation(Type pageNavigatingTo)
		{
            bool result = false;
            if (LeukemiaLyphomaNavigationGroup.Instance.IsInGroup(pageNavigatingTo) == true)
            {
                result = false;
            }
            return result;
		}

		public bool OkToSaveOnClose()
		{
			return false;
		}

		public void Save()
		{
		}

		public static bool Save(YellowstonePathology.YpiConnect.Contract.Flow.FlowAccessionCollection flowAccessionCollection)
		{
			bool result = true;
			return result;
		}

		public void UpdateBindingSources()
		{
		}
	}
}
