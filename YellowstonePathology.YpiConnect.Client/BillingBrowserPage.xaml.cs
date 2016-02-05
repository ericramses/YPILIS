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
	/// Interaction logic for BillingPage.xaml
	/// </summary>
	public partial class BillingBrowserPage : Page, YellowstonePathology.Business.Interface.IPersistPageChanges
	{		
		private BillingBrowserUI m_BillingBrowserUI;

		public BillingBrowserPage()
		{
			InitializeComponent();

			this.m_BillingBrowserUI = new BillingBrowserUI();
			this.m_BillingBrowserUI.GetYesterdaysBilling();
			this.DataContext = this.m_BillingBrowserUI;

			Loaded += new RoutedEventHandler(BillingBrowserPage_Loaded);
		}

		private void BillingBrowserPage_Loaded(object sender, RoutedEventArgs e)
		{
			UserInteractionMonitor.Instance.Register(this);
		}

		private void HyperlinkBillingDetails_Click(object sender, RoutedEventArgs e)
		{
			if (this.ListViewSearchResults.SelectedItems.Count != 0)
			{
				YellowstonePathology.YpiConnect.Contract.Billing.BillingAccession billingAccession = (YellowstonePathology.YpiConnect.Contract.Billing.BillingAccession)this.ListViewSearchResults.SelectedItem;
				YellowstonePathology.YpiConnect.Contract.Billing.BillingDetail billingDetail = this.m_BillingBrowserUI.GetBillingDetail(billingAccession);

				BillingDetailPage billingDetailPage = new BillingDetailPage(billingAccession, billingDetail);
				ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(billingDetailPage);
			}
		}		

		private void HyperlinkSendMessage_Click(object sender, RoutedEventArgs e)
		{
			YpiConnect.Contract.Message message = new Contract.Message("Billing@ypii.com", YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount);
			if (this.ListViewSearchResults.SelectedItem != null)
			{
				YellowstonePathology.YpiConnect.Contract.Billing.BillingAccession billingAccession = (YellowstonePathology.YpiConnect.Contract.Billing.BillingAccession)this.ListViewSearchResults.SelectedItem;
				message.ReportNo = billingAccession.ReportNo;
				message.PatientName = billingAccession.FirstName + " " + billingAccession.LastName;
			}
			MessagePage messagePage = new MessagePage(message);
			ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(messagePage);
		}

		private void HyperlinkHome_Click(object sender, RoutedEventArgs e)
		{
			MainWindow mainWindow = (MainWindow)System.Windows.Application.Current.MainWindow;
			HomePage homePage = new HomePage();
			ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(homePage);
		}
		
        private void HyperlinkTodaysBilling_Click(object sender, RoutedEventArgs e)
        {
            this.m_BillingBrowserUI.GetTodaysBilling();
		}

        private void HyperlinkYesterdaysBilling_Click(object sender, RoutedEventArgs e)
        {
			this.m_BillingBrowserUI.GetYesterdaysBilling();
		}

		private void HyperlinkPatientNameSearch_Click(object sender, RoutedEventArgs e)
		{
			PatientNameSearch patientNameSearch = new PatientNameSearch();
			PatientNameSearchPage patientNameSearchPage = new PatientNameSearchPage(patientNameSearch);
			patientNameSearchPage.DoPatientNameSearch += new EventHandler(this.m_BillingBrowserUI.DoPatientNameSearch);
			ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(patientNameSearchPage);
		}

        private void HyperlinkReportNoSearch_Click(object sender, RoutedEventArgs e)
        {
			ReportNoSearchPage reportNoSearchPage = new ReportNoSearchPage();
			reportNoSearchPage.DoReportNoSearch += new EventHandler(this.m_BillingBrowserUI.DoReportNoSearch);
			ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(reportNoSearchPage);
		}

		private void HyperlinkSsnSearch_Click(object sender, RoutedEventArgs e)
		{
			PatientSsnSearchPage patientSsnSearchPage = new PatientSsnSearchPage();
			patientSsnSearchPage.DoSsnSearch += new EventHandler(this.m_BillingBrowserUI.DoPatientSsnSearch);
			ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(patientSsnSearchPage);
		}

		private void HyperlinkBirthdateSearch_Click(object sender, RoutedEventArgs e)
		{
			PatientBirthdateSearchPage patientBirthdateSearchPage = new PatientBirthdateSearchPage();
			patientBirthdateSearchPage.DoBirthdateSearch += new EventHandler(this.m_BillingBrowserUI.DoPatientBirthdateSearch);
			ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(patientBirthdateSearchPage);
		}

        private void HyperlinkRecentCasesSearch_Click(object sender, RoutedEventArgs e)
        {
            this.m_BillingBrowserUI.GetRecentBilling();
        }

		private void ListViewSearchResults_DoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (this.ListViewSearchResults.SelectedItems.Count != 0)
			{
				YellowstonePathology.YpiConnect.Contract.Billing.BillingAccession billingAccession = (YellowstonePathology.YpiConnect.Contract.Billing.BillingAccession)this.ListViewSearchResults.SelectedItem;
				YellowstonePathology.YpiConnect.Contract.Billing.BillingDetail billingDetail = this.m_BillingBrowserUI.GetBillingDetail(billingAccession);

				BillingDetailPage billingDetailPage = new BillingDetailPage(billingAccession, billingDetail);
				ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(billingDetailPage);
			}
		}

		public void Save(bool releaseLock)
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
