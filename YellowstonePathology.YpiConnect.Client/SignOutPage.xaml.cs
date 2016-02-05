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
	public partial class SignOutPage : UserControl, INotifyPropertyChanged, YellowstonePathology.Business.Interface.IPersistPageChanges
    {
		public delegate void PropertyChangedNotificationHandler(String info);
        public event PropertyChangedEventHandler PropertyChanged;
                        
        public SignOutPage()
        {                        
            InitializeComponent();            
            this.DataContext = this;            
        }
        
        private void ButtonSignIn_Click(object sender, RoutedEventArgs e)
        {
			SignInPage signInPage = new SignInPage();
			ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(signInPage);
        }

        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
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
