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

namespace YellowstonePathology.YpiConnect.Client
{
    /// <summary>
	/// Interaction logic for HomePage.xaml
    /// </summary>
	public partial class HomePage : Page, YellowstonePathology.Shared.Interface.IPersistPageChanges
    {        
        public HomePage()
        {
            InitializeComponent();
			this.Loaded += new RoutedEventHandler(HomePage_Loaded);
			MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
			this.HyperlinkSignOut.Click += new RoutedEventHandler(mainWindow.ButtonSignOut_Click);
        }

		private void HomePage_Loaded(object sender, RoutedEventArgs e)
		{
			UserInteractionMonitor.Instance.Register(this);
		}

		private void HyperlinkHome_Click(object sender, RoutedEventArgs e)
		{
		}

        private void HyperlinkReportBrowser_Click(object sender, RoutedEventArgs e)
        {
			if (YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount.EnableReportBrowser == true)
			{
				ReportBrowserPage reportBrowserPage = new ReportBrowserPage();
				ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(reportBrowserPage);
			}
			else
			{
				FeatureNotEnabledPage featureNotEnabledPage = new FeatureNotEnabledPage("Report Browser");
				ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(featureNotEnabledPage);
			}
		}

        private void HyperlinkOrderBrowser_Click(object sender, RoutedEventArgs e)
        {
			if (YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount.EnableOrderEntry == true)
			{
				OrderEntry.OrderBrowserPage orderBrowserPage = new OrderEntry.OrderBrowserPage();
				ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(orderBrowserPage);
			}
			else
			{
				FeatureNotEnabledPage featureNotEnabledPage = new FeatureNotEnabledPage("Order Browser");
				ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(featureNotEnabledPage);
			}
		}

        private void HyperlinkBillingBrowser_Click(object sender, RoutedEventArgs e)
        {
			if (YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount.EnableBillingBrowser == true)
			{
				BillingBrowserPage billingBrowserPage = new BillingBrowserPage();
				ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(billingBrowserPage);
			}
			else
			{
				FeatureNotEnabledPage featureNotEnabledPage = new FeatureNotEnabledPage("Billing Browser");
				ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(featureNotEnabledPage);
			}
		}

		private void HyperlinkFileDownload_Click(object sender, RoutedEventArgs e)
		{			
            FileDownloadSettingsPage fileDownloadSettingsPage = new FileDownloadSettingsPage();
            ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(fileDownloadSettingsPage);			
		}

        private void HyperlinkFileUpload_Click(object sender, RoutedEventArgs e)
        {
			if (YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount.EnableFileUpload == true &&
				string.IsNullOrEmpty(YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount.LocalFileUploadDirectory) == false)
			{
				FileUploadPage fileUploadPage = new FileUploadPage();
				ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(fileUploadPage);
			}
			else
			{
				FileUploadSettingsPage fileUploadSettingsPage = new FileUploadSettingsPage();
				ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(fileUploadSettingsPage);
			}
		}

        private void HyperlinkContactUs_Click(object sender, RoutedEventArgs e)
        {
			ContactUsPage contactUsPage = new ContactUsPage();
			ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(contactUsPage);
		}

        private void HyperlinkDesktopShortcuts_Click(object sender, RoutedEventArgs e)
        {
            DesktopShortcutPage desktopShortcutPage = new DesktopShortcutPage();
			ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(desktopShortcutPage);
        }

		private void HyperlinkPathologistSignOut_Click(object sender, RoutedEventArgs e)
		{
			PathologistSignoutPage pathologistSignoutPage = new PathologistSignoutPage();
			ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(pathologistSignoutPage);
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
