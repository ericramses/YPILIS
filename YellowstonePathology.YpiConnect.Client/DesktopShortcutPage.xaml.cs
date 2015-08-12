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

namespace YellowstonePathology.YpiConnect.Client
{
    /// <summary>
    /// Interaction logic for LoginDialog.xaml
    /// </summary>
	public partial class DesktopShortcutPage : Page, YellowstonePathology.Business.Interface.IPersistPageChanges
    {
        public DesktopShortcutPage()
        {
            InitializeComponent();

			MainWindow mainWindow = (MainWindow)System.Windows.Application.Current.MainWindow;
			this.HyperlinkSignOut.Click += new RoutedEventHandler(mainWindow.ButtonSignOut_Click);
			Loaded += new RoutedEventHandler(DesktopShortcutPage_Loaded);
        }

		void DesktopShortcutPage_Loaded(object sender, RoutedEventArgs e)
		{
			UserInteractionMonitor.Instance.Register(this);
		}

		private void HyperlinkHome_Click(object sender, RoutedEventArgs e)
		{
			HomePage homePage = new HomePage();
			ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(homePage);
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
			if (YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount.EnableFileDownload == true &&
				string.IsNullOrEmpty(YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount.LocalFileDownloadDirectory) == false)
			{
				FileDownloadPage fileDownloadPage = new FileDownloadPage();
				ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(fileDownloadPage);
			}
			else
			{
				FileDownloadSettingsPage fileDownloadSettingsPage = new FileDownloadSettingsPage();
				ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(fileDownloadSettingsPage);
			}
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

		public void Save()
		{
		}

        private void HyperLinkReportsDesktopShortcut(object sender, RoutedEventArgs e)
        {
            this.AddDesktopShortcut("https://www.YellowstonePathology.com/YpiConnect/ClientApplication/Version/2.0.0.0/Reports/YellowstonePathology.YpiConnect.Client.Application");
        }        

        private void HyperLinkSCLHSDesktopShortcut(object sender, RoutedEventArgs e)
        {
            this.AddDesktopShortcut("https://www.YellowstonePathology.com/YpiConnect/ClientApplication/Version/2.0.0.0/SCLHS/YellowstonePathology.YpiConnect.Client.Application");
        }        

        private void HyperLinkBillingDesktopShortcut(object sender, RoutedEventArgs e)
        {
            this.AddDesktopShortcut("https://www.YellowstonePathology.com/YpiConnect/ClientApplication/Version/2.0.0.0/Billing/YellowstonePathology.YpiConnect.Client.Application");
        }

        private void HyperLinkPathologistDesktopShortcut(object sender, RoutedEventArgs e)
        {
            this.AddDesktopShortcut("https://www.YellowstonePathology.com/YpiConnect/ClientApplication/Version/2.0.0.0/Pathologist/YellowstonePathology.YpiConnect.Client.Application");
        }

        private void AddDesktopShortcut(string targetPath)
        {
            try
            {
                /*
                YellowstonePathology.YpiConnect.Proxy.FileTransferServiceProxy fileTransferServiceProxy = new Proxy.FileTransferServiceProxy();
                string remoteIconFullPath = @"c:\YPIConnect\ClientApplication\Version\2.0.0.0\Ypi.ico";
                YellowstonePathology.YpiConnect.Contract.RemoteFile remoteFile = new Contract.RemoteFile(remoteIconFullPath);
                YellowstonePathology.YpiConnect.Contract.MethodResult methodResult = fileTransferServiceProxy.Download(ref remoteFile, YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount);

                string appDirectoryName = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
                string localIconFullPath = System.IO.Path.Combine(appDirectoryName, "YPI.ICO");
                YellowstonePathology.YpiConnect.Contract.LocalFile localFile = new Contract.LocalFile(remoteFile, localIconFullPath);
                localFile.Save();

                string shortcutPath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "YPI Connect.lnk");
                IWshRuntimeLibrary.WshShell shell = new IWshRuntimeLibrary.WshShellClass();
                IWshRuntimeLibrary.WshShortcut cl = (IWshRuntimeLibrary.WshShortcut)shell.CreateShortcut(shortcutPath);
                cl.IconLocation = localIconFullPath;
                cl.TargetPath = targetPath;
                cl.Description = "YPI Connect";
                cl.Save();

                MessageBox.Show("A YPI Connect Icon has been added to your desktop.");
                */
            }
            catch
            {
                MessageBox.Show("We were unable to add the application desktop icon.");
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
