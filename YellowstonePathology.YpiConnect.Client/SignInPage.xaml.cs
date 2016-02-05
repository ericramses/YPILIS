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
	/// Interaction logic for SignInPage.xaml
    /// </summary>
	public partial class SignInPage : Page, INotifyPropertyChanged, YellowstonePathology.Business.Interface.IPersistPageChanges
    {
        public delegate void PropertyChangedNotificationHandler(String info);
        public event PropertyChangedEventHandler PropertyChanged;

        private string m_DisplayMessage;

        private int m_SignInFailureAttempts = 0;
        private int m_SignInFailureAttemptThreshold = 3;

        private string m_AppVersion;
		private bool m_FromMessageBox;        

		public SignInPage()
        {
            this.m_AppVersion = "version: 3.0.0.0";
            
            InitializeComponent();            

            this.DataContext = this;            
            this.Loaded += new RoutedEventHandler(SignInPage_Loaded);            
            PasswordBoxPassword.KeyUp +=new KeyEventHandler(PasswordBoxPassword_KeyUp);
        }

        public string AppVersion
        {
            get { return this.m_AppVersion; }
        }

        private void SignInPage_Loaded(object sender, RoutedEventArgs e)
        {            
            YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.ReadSavedSettings();			
			this.TextBoxUserName.Text = YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount.UserName;
            this.PasswordBoxPassword.Password = YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount.Password;

			if(string.IsNullOrEmpty(this.TextBoxUserName.Text))
			{
				this.TextBoxUserName.Focus();
			}
			else
			{
	            this.PasswordBoxPassword.Focus();            
			}            
        }                

        public string DisplayMessage
        {
            get { return this.m_DisplayMessage; }
            set 
            {
                if (this.m_DisplayMessage != value)
                {
                    this.m_DisplayMessage = value;
                    this.NotifyPropertyChanged("DisplayMessage");
                }
            }
        }

        private void SignIn()
        {            
            string userName = this.TextBoxUserName.Text;
            string password = this.PasswordBoxPassword.Password;            

            YellowstonePathology.YpiConnect.Proxy.WebServiceAccountServiceProxy proxy = new YpiConnect.Proxy.WebServiceAccountServiceProxy();
            YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount webServiceAccount = proxy.GetWebServiceAccount(userName, password);
            YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.SignIn(webServiceAccount);

            if (webServiceAccount.IsKnown == true)
            {
                Page page = null;
                switch (YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount.InitialPage)
                {
                    case "PathologyDashboard":
                        page = new PathologistSignoutPage();
                        break;
                    case "OrderBrowser":
                        page = new OrderEntry.OrderBrowserPage();
                        break;
                    case "ReportBrowser":
                        page = new ReportBrowserPage();
                        break;
                    case "BillingBrowser":
                        page = new BillingBrowserPage();
                        break;
                    case "FileUpload":
                        page = new FileUploadPage();
                        break;
                }

                ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(page);
                YellowstonePathology.YpiConnect.Client.UserInteractionMonitor.Instance.Start();
            }
            else
            {
                this.m_SignInFailureAttempts += 1;
                this.PasswordBoxPassword.Password = string.Empty;
                this.DisplayMessage = "";

                if (this.m_SignInFailureAttempts >= this.m_SignInFailureAttemptThreshold)
                {
                    SignInFailurePage signInFailurePage = new SignInFailurePage();
                    ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(signInFailurePage);                    
                }
                else
                {
                    this.m_FromMessageBox = true;
                    MessageBox.Show("We could not log you in.  Make sure your username and password are correct, then type your password again.  Letters in passwords must be typed using the correct case.", "Logon failed");
                    this.PasswordBoxPassword.Focus();
                }
            }            
		}

        private void ButtonSignIn_Click(object sender, RoutedEventArgs e)
        {
            this.SignIn();
        }

        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void PasswordBoxPassword_KeyUp(object sender, KeyEventArgs e)
        {
			if (this.m_FromMessageBox)
			{
				this.m_FromMessageBox = false;
			}
			else
			{
				if (e.Key == Key.Enter)
				{
					this.SignIn();
				}
			}
        }

        private void TextBoxUserName_GotFocus(object sender, RoutedEventArgs e)
        {
            this.TextBoxUserName.SelectAll();
        }

        private void PasswordBoxPassword_GotFocus(object sender, RoutedEventArgs e)
        {
            this.PasswordBoxPassword.SelectAll();
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
