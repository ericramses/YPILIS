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
	/// Interaction logic for PatientNameSearchPage.xaml
    /// </summary>
	public partial class PatientNameSearchPage : Page, YellowstonePathology.Business.Interface.IPersistPageChanges
    {
		public event EventHandler DoPatientNameSearch;

		private PatientNameSearch m_PatientNameSearch;

		public PatientNameSearchPage(PatientNameSearch patientNameSearch)
        {
            this.m_PatientNameSearch = patientNameSearch;
            InitializeComponent();
            this.DataContext = this.m_PatientNameSearch;
			this.Loaded += new RoutedEventHandler(PatientNameSearchPage_Loaded);
        }

		private void PatientNameSearchPage_Loaded(object sender, RoutedEventArgs e)
        {
			UserInteractionMonitor.Instance.Register(this);
			this.TextBoxPatientName.Focus();
        }

		private void HyperlinkBack_Click(object sender, RoutedEventArgs e)
		{
			ApplicationNavigator.ApplicationContentFrame.NavigationService.GoBack();
		}

        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
			NameSearchEventArgs args = new NameSearchEventArgs(this.m_PatientNameSearch);
			this.DoPatientNameSearch(this, args);
			ApplicationNavigator.ApplicationContentFrame.NavigationService.GoBack();
		}

        private void TextBoxPatientName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                this.ButtonSearch.Focus();
				NameSearchEventArgs args = new NameSearchEventArgs(this.m_PatientNameSearch);
				this.DoPatientNameSearch(this, args);
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
