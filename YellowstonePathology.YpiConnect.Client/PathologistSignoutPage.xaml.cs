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
using System.Xml;
using System.Xml.Linq;
using System.ComponentModel;

namespace YellowstonePathology.YpiConnect.Client
{
	public partial class PathologistSignoutPage : Page, YellowstonePathology.Business.Interface.IPersistPageChanges
    {
		private ReportBrowserUI m_ReportBrowserUI;
        
        public PathologistSignoutPage()
        {            
            this.m_ReportBrowserUI = new ReportBrowserUI();
            InitializeComponent();
			
			this.DataContext = this.m_ReportBrowserUI;
			this.m_ReportBrowserUI.DoFacilityRecentCaseSearch();
			Loaded += new RoutedEventHandler(PathologistSignoutPage_Loaded);
		}

		private void PathologistSignoutPage_Loaded(object sender, RoutedEventArgs e)
		{
			UserInteractionMonitor.Instance.Register(this);
		}

        private void HyperlinkHome_Click(object sender, RoutedEventArgs e)
        {
			HomePage homePage = new HomePage();
			ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(homePage);
		}

        private void HyperlinkSendMessage_Click(object sender, RoutedEventArgs e)
        {
			YpiConnect.Contract.Message message = new Contract.Message("ClientCommunication@ypii.com", YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount);
			if (this.ListViewSearchResults.SelectedItem != null)
			{
				YpiConnect.Contract.Search.SearchResult searchResult = (YpiConnect.Contract.Search.SearchResult)this.ListViewSearchResults.SelectedItem;
				message.ReportNo = searchResult.ReportNo;
				message.PatientName = searchResult.PatientName;
			}
			MessagePage messagePage = new MessagePage(message);
			ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(messagePage);
		}

		private void HyperlinkRefresh_Click(object sender, RoutedEventArgs e)
		{
		}

        private void HyperlinkPatientNameSearch_Click(object sender, RoutedEventArgs e)
        {
			PatientNameSearch patientNameSearch = new PatientNameSearch();
			PatientNameSearchPage patientNameSearchPage = new PatientNameSearchPage(patientNameSearch);
			patientNameSearchPage.DoPatientNameSearch += new EventHandler(this.DoPatientNameSearch);
			ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(patientNameSearchPage);
		}

        private void HyperlinkSsnSearch_Click(object sender, RoutedEventArgs e)
        {
			PatientSsnSearchPage patientSsnSearchPage = new PatientSsnSearchPage();
			patientSsnSearchPage.DoSsnSearch += new EventHandler(this.DoPatientSsnSearch);
			ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(patientSsnSearchPage);
		}

        private void HyperlinkBirthdateSearch_Click(object sender, RoutedEventArgs e)
        {
			PatientBirthdateSearchPage patientBirthdateSearchPage = new PatientBirthdateSearchPage();
			patientBirthdateSearchPage.DoBirthdateSearch += new EventHandler(this.DoPatientBirthdateSearch);
			ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(patientBirthdateSearchPage);
		}

        private void HyperlinkRecentCases_Click(object sender, RoutedEventArgs e)
        {
			this.m_ReportBrowserUI.DoFacilityRecentCaseSearch();
			this.m_ReportBrowserUI.SortByPatientName();
			this.SetListViewToTop();
		}

        private void HyperlinkDetails_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewSearchResults.SelectedItems.Count != 0)
            {
				// Marked out temporarily
                //YellowstonePathology.YpiConnect.Contract.Search.SearchResult searchResult = (YellowstonePathology.YpiConnect.Contract.Search.SearchResult)this.ListViewSearchResults.SelectedItem;
				//LeukemiaLymphomaSignoutPage leukemiaLymphomaSignoutPage = new LeukemiaLymphomaSignoutPage(searchResult.MasterAccessionNo);
				//leukemiaLymphomaSignoutPage.KeepAlive = true;
				//leukemiaLymphomaSignoutPage.Return += new ReturnEventHandler<bool>(LeukemiaLymphomaSignoutPage_Return);
				//ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(leukemiaLymphomaSignoutPage);
            }
        }

		private void LeukemiaLymphomaSignoutPage_Return(object sender, ReturnEventArgs<bool> e)
		{

		}

		public void DoPatientNameSearch(object sender, EventArgs e)
		{
			NameSearchEventArgs args = (NameSearchEventArgs)e;
			PatientNameSearch patientNameSearch = args.PatientNameSearch;
			if (string.IsNullOrEmpty(patientNameSearch.LastNameSearchString) == false)
			{
				if (string.IsNullOrEmpty(patientNameSearch.FirstNameSearchString) == false)
				{
					this.m_ReportBrowserUI.DoPathologistLastAndFirstNameSearch(patientNameSearch.LastNameSearchString, patientNameSearch.FirstNameSearchString);
				}
				else
				{
					this.m_ReportBrowserUI.DoPathologistLastNameSearch(patientNameSearch.LastNameSearchString);
				}

				this.SetListViewToTop();
			}
		}

		public void DoPatientBirthdateSearch(object sender, EventArgs e)
		{
			SearchStringEventArgs args = (SearchStringEventArgs)e;
			DateTime birthdate;
			bool isValid = DateTime.TryParse(args.SearchString, out birthdate);
			if (isValid)
			{
				this.m_ReportBrowserUI.DoPathologistDateOfBirthSearch(birthdate);
				this.SetListViewToTop();
			}
		}

		public void DoPatientSsnSearch(object sender, EventArgs e)
		{
			SearchStringEventArgs args = (SearchStringEventArgs)e;
			this.m_ReportBrowserUI.DoPathologistSSNSearch(args.SearchString);
			this.SetListViewToTop();
		}

		private void SetListViewToTop()
		{
			if (this.ListViewSearchResults.Items.Count > 0)
			{
				this.ListViewSearchResults.ScrollIntoView(this.ListViewSearchResults.Items[0]);
			}
		}

		private void ListViewSearchResults_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			this.ViewDocument();
		}

		public void Save()
		{

		}

		private void ViewDocument()
		{
			if (this.ListViewSearchResults.SelectedItem != null)
			{
				// Marked out temporarily
				//YellowstonePathology.YpiConnect.Contract.Search.SearchResult searchResult = (YellowstonePathology.YpiConnect.Contract.Search.SearchResult)this.ListViewSearchResults.SelectedItem;
				//LeukemiaLymphomaReportPage leukemiaLymphomaReportPage = new LeukemiaLymphomaReportPage(searchResult.ReportNo, searchResult.MasterAccessionNo);
				//leukemiaLymphomaReportPage.Return += new ReturnEventHandler<Type>(LeukemiaLymphomaReportPage_Return);
				//ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(leukemiaLymphomaReportPage);
			}
		}

		private void LeukemiaLymphomaReportPage_Return(object sender, ReturnEventArgs<Type> e)
		{
		}

        private void HyperlinkViewDocument_Click(object sender, RoutedEventArgs e)
        {
			this.ViewDocument();
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
