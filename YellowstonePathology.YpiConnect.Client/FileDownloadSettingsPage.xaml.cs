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
using System.ComponentModel;

namespace YellowstonePathology.YpiConnect.Client
{
    /// <summary>
	/// Interaction logic for FileDownloadSettingsPage.xaml
    /// </summary>
	public partial class FileDownloadSettingsPage : Page, INotifyPropertyChanged, YellowstonePathology.Shared.Interface.IPersistPageChanges
	{
		public delegate void PropertyChangedNotificationHandler(String info);
		public event PropertyChangedEventHandler PropertyChanged;

		private string m_LocalFileDownloadDirectory;
		private Visibility m_CommentVisibility;

		public FileDownloadSettingsPage()
        {
            InitializeComponent();

			if (YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount.EnableFileDownload)
			{
				this.m_CommentVisibility = System.Windows.Visibility.Collapsed;
				this.CheckBoxEnabled.IsChecked = true;
				this.LocalFileDownloadDirectory = YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount.LocalFileDownloadDirectory;
			}
			else
			{
				this.m_CommentVisibility = System.Windows.Visibility.Visible;
				this.CheckBoxEnabled.IsChecked = false;
			}

			this.DataContext = this;

			Loaded += new RoutedEventHandler(FileDownloadSettingsPage_Loaded);
        }

		void FileDownloadSettingsPage_Loaded(object sender, RoutedEventArgs e)
		{
			UserInteractionMonitor.Instance.Register(this);
		}

		private void HyperlinkBack_Click(object sender, RoutedEventArgs e)
		{
			ApplicationNavigator.ApplicationContentFrame.NavigationService.GoBack();
		}

		public string LocalFileDownloadDirectory
		{
			get { return this.m_LocalFileDownloadDirectory; }
			set
			{
				this.m_LocalFileDownloadDirectory = value;
				NotifyPropertyChanged("LocalFileDownloadDirectory");
			}
		}

		public bool DataEnabled
		{
			get { return YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount.EnableFileDownload; }
		}

		public Visibility CommentVisibility
		{
			get { return this.m_CommentVisibility; }
		}

		private void ButtonBrowse_Click(object sender, RoutedEventArgs e)
		{
			FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
			folderBrowserDialog.ShowDialog();
			if (string.IsNullOrEmpty(folderBrowserDialog.SelectedPath) == false)
			{
				this.LocalFileDownloadDirectory = folderBrowserDialog.SelectedPath;
			}
		}

		private void ButtonSetDefault_Click(object sender, RoutedEventArgs e)
		{
			this.LocalFileDownloadDirectory = @"C:\Program Files\Yellowstone Pathology Institute\Client Services\Reports";
		}

		private void ButtonSave_Click(object sender, RoutedEventArgs e)
		{
			if (!string.IsNullOrEmpty(this.LocalFileDownloadDirectory))
			{
				YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount.LocalFileDownloadDirectory = LocalFileDownloadDirectory;
				YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.Save();
				FileDownloadSettingsSavedPage fileDownloadSettingsSavedPage = new FileDownloadSettingsSavedPage();
				ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(fileDownloadSettingsSavedPage);
			}
			else
			{
				System.Windows.MessageBox.Show("Enter a valid directory for the File Download Path.", "Path not valid");
			}
		}

		protected void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
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
