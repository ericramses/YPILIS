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
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;

namespace YellowstonePathology.YpiConnect.Client
{    
    public partial class MainWindow : Window
    {		        
		private ClientServicesUI m_ClientServicesUI;

        public MainWindow()
        {
            this.m_ClientServicesUI = new ClientServicesUI();

            InitializeComponent();

            this.DataContext = this.m_ClientServicesUI;

			ApplicationNavigator.ApplicationContentFrame = this.MainFrame;

            this.MainFrame.NavigationUIVisibility = NavigationUIVisibility.Hidden;
            this.MainFrame.NavigationService.Navigated += new NavigatedEventHandler(ApplicationContentFrame_Navigated);
			this.MainFrame.Navigating += new NavigatingCancelEventHandler(MainFrame_Navigating);

            this.Loaded += new RoutedEventHandler(MainWindow_Loaded);            
            this.Closing += new System.ComponentModel.CancelEventHandler(MainWindow_Closing);
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
			YellowstonePathology.Business.Interface.IPersistPageChanges page = (YellowstonePathology.Business.Interface.IPersistPageChanges)this.MainFrame.NavigationService.Content;
            if (page != null)
            {
				page.UpdateBindingSources();
                page.Save();
            }
        }    

		private void MainFrame_Navigating(object sender, NavigatingCancelEventArgs e)
		{
            if (this.MainFrame.Content != null)
            {
				YellowstonePathology.Business.Interface.IPersistPageChanges navigatingToPage = (YellowstonePathology.Business.Interface.IPersistPageChanges)e.Content;
				YellowstonePathology.Business.Interface.IPersistPageChanges navigatingFromPage = (YellowstonePathology.Business.Interface.IPersistPageChanges)this.MainFrame.Content;
				navigatingFromPage.UpdateBindingSources();
				if (navigatingFromPage.OkToSaveOnNavigation(navigatingToPage.GetType()) == true)
                {
                    navigatingFromPage.Save();
                }
            }      
		}        

        private void ApplicationContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            if (e.Content.GetType() == typeof(YellowstonePathology.YpiConnect.Client.SignInPage) ||
				e.Content.GetType() == typeof(YellowstonePathology.YpiConnect.Client.SignOutPage))
            {
                this.m_ClientServicesUI.SignOutButtonVisibility = System.Windows.Visibility.Collapsed;
                this.m_ClientServicesUI.DisplayName = string.Empty;
            }
            else
            {
                this.m_ClientServicesUI.SignOutButtonVisibility = System.Windows.Visibility.Visible;
                this.m_ClientServicesUI.DisplayName = YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount.DisplayName;
            }            
        }                    

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {            
            YellowstonePathology.YpiConnect.Proxy.WebServiceAccountServiceProxy webServiceAccountServiceProxy = new YpiConnect.Proxy.WebServiceAccountServiceProxy();            
            if (webServiceAccountServiceProxy.Ping() == true)
            {
                SignInPage signInPage = new SignInPage();
                this.MainFrame.Navigate(signInPage);
                UserInteractionMonitor.Instance.TimedOut += new EventHandler(TimedOut);
            }
            else
            {
                MessageBox.Show("The webservice is not responding.");
            }            
        }
        
        public void OpenFolder(string folderPath)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo("Explorer.exe", folderPath);
            p.StartInfo = info;
            p.Start();
        }                 
        
        public void ButtonSignOut_Click(object sender, RoutedEventArgs e)
        {            
            this.Signout();
        }

		private void TimedOut(object sender, EventArgs e)
        {            
            this.Signout();			
        }

		public void Signout()
		{
			YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.SignOut();

			this.m_ClientServicesUI.SignOutButtonVisibility = System.Windows.Visibility.Collapsed;
			this.m_ClientServicesUI.DisplayName = string.Empty;			

			SignOutPage signOutPage = new SignOutPage();
			this.MainFrame.NavigationService.Navigate(signOutPage);

			foreach (Window window in Application.Current.Windows)
			{
				if (window != Application.Current.MainWindow)
				{
					window.Close();
				}
			}
		}
	}
}
