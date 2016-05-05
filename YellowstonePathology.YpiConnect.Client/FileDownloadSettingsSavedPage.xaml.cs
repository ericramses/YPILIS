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
	public partial class FileDownloadSettingsSavedPage : Page, YellowstonePathology.Business.Interface.IPersistPageChanges
    {
		public FileDownloadSettingsSavedPage()
        {
            InitializeComponent();

			Loaded += new RoutedEventHandler(FileDownloadSettingsSavedPage_Loaded);
        }

		void FileDownloadSettingsSavedPage_Loaded(object sender, RoutedEventArgs e)
		{
			UserInteractionMonitor.Instance.Register(this);
		}

		private void HyperlinkBack_Click(object sender, RoutedEventArgs e)
		{
			ApplicationNavigator.ApplicationContentFrame.NavigationService.RemoveBackEntry();
			ApplicationNavigator.ApplicationContentFrame.NavigationService.GoBack();
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
