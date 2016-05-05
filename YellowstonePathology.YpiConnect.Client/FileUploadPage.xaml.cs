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
	/// Interaction logic for FileUploadPage.xaml
    /// </summary>
	public partial class FileUploadPage : Page, INotifyPropertyChanged, YellowstonePathology.Business.Interface.IPersistPageChanges
	{
		public delegate void PropertyChangedNotificationHandler(String info);
		public event PropertyChangedEventHandler PropertyChanged;

		private YellowstonePathology.YpiConnect.Contract.LocalFileList m_LocalFileList;
		private YellowstonePathology.YpiConnect.Contract.RemoteFileList m_RemoteFileList;

		public FileUploadPage()
        {
			this.m_LocalFileList = new Contract.LocalFileList(false);
			this.m_LocalFileList.Load(YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount);
			this.m_RemoteFileList = new Contract.RemoteFileList();
			this.GetRemoteFileList();

            InitializeComponent();

			MainWindow mainWindow = (MainWindow)System.Windows.Application.Current.MainWindow;
			this.HyperlinkSignOut.Click += new RoutedEventHandler(mainWindow.ButtonSignOut_Click);
			this.DataContext = this;

			Loaded += new RoutedEventHandler(FileUploadPage_Loaded);
        }

		void FileUploadPage_Loaded(object sender, RoutedEventArgs e)
		{
			UserInteractionMonitor.Instance.Register(this);
		}

		protected void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

        private void HyperlinkHome_Click(object sender, RoutedEventArgs e)
        {
			MainWindow mainWindow = (MainWindow)System.Windows.Application.Current.MainWindow;
			HomePage homePage = new HomePage();
			ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(homePage);
		}

        private void HyperlinkUpload_Click(object sender, RoutedEventArgs e)
        {
			this.UploadFile();
        }

        private void HyperlinkRefresh_Click(object sender, RoutedEventArgs e)
        {

        }

        private void HyperlinkSendMessage_Click(object sender, RoutedEventArgs e)
        {
        }

        private void HyperlinkUploadSettings_Click(object sender, RoutedEventArgs e)
        {
			FileUploadSettingsPage fileUploadSettingsPage = new FileUploadSettingsPage();
			ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(fileUploadSettingsPage);
        }

		private void UploadFile()
		{
			if (this.ListViewLocalFiles.SelectedItems.Count > 0)
			{
				YpiConnect.Proxy.FileTransferServiceProxy fileTransferServiceProxy = new Proxy.FileTransferServiceProxy();
				for (int idx = 0; idx < this.ListViewLocalFiles.SelectedItems.Count; idx++)
				{
					YellowstonePathology.YpiConnect.Contract.LocalFile localFile = (YellowstonePathology.YpiConnect.Contract.LocalFile)this.ListViewLocalFiles.SelectedItems[idx];
					localFile.Load();
					fileTransferServiceProxy.Upload(ref localFile, YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount);
				}

				this.GetRemoteFileList();
			}
		}

		public void GetRemoteFileList()
		{
			this.m_RemoteFileList.Clear();
			YpiConnect.Proxy.FileTransferServiceProxy fileTransferServiceProxy = new Proxy.FileTransferServiceProxy();
			YellowstonePathology.YpiConnect.Contract.MethodResult methodResult = fileTransferServiceProxy.GetRemoteFileList(ref this.m_RemoteFileList, YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount);
			NotifyPropertyChanged("");
		}

		public string LocalPath
		{
			get { return YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount.LocalFileUploadDirectory; }
		}

		public string YPIPath
		{
			get { return YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount.RemoteFileUploadDirectory; }
		}

		public YellowstonePathology.YpiConnect.Contract.LocalFileList LocalFileList
		{
			get { return this.m_LocalFileList; }
			set
			{
				if (this.m_LocalFileList != value)
				{
					this.m_LocalFileList = value;
					NotifyPropertyChanged("LocalFileList");
				}
			}
		}

		public YellowstonePathology.YpiConnect.Contract.RemoteFileList RemoteFileList
		{
			get { return this.m_RemoteFileList; }
			set
			{
				if (this.m_RemoteFileList != value)
				{
					this.m_RemoteFileList = value;
					NotifyPropertyChanged("RemoteFileList");
				}
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
