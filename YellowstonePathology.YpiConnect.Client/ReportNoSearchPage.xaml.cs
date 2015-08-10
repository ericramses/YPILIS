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
	/// Interaction logic for ReportNoSearchPage.xaml
    /// </summary>
	public partial class ReportNoSearchPage : Page, YellowstonePathology.Shared.Interface.IPersistPageChanges
    {
        public event EventHandler DoReportNoSearch;

		public ReportNoSearchPage()
        {
            InitializeComponent();
            this.DataContext = this;
			this.Loaded += new RoutedEventHandler(ReportNoSearchPage_Loaded);
        }

		private void HyperlinkBack_Click(object sender, RoutedEventArgs e)
		{
			ApplicationNavigator.ApplicationContentFrame.NavigationService.GoBack();
		}

		private void ReportNoSearchPage_Loaded(object sender, RoutedEventArgs e)
        {
			UserInteractionMonitor.Instance.Register(this);
			this.TextBoxReportNo.Focus();
        }

        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
			SearchStringEventArgs args = new SearchStringEventArgs(this.TextBoxReportNo.Text);
			this.DoReportNoSearch(this, args);
			ApplicationNavigator.ApplicationContentFrame.NavigationService.GoBack();
		}

		private void TextBoxReportNo_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.ButtonSearch.Focus();
				SearchStringEventArgs args = new SearchStringEventArgs(this.TextBoxReportNo.Text);
				this.DoReportNoSearch(this, args);
				ApplicationNavigator.ApplicationContentFrame.NavigationService.GoBack();
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
