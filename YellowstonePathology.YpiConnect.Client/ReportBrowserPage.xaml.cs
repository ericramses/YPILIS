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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace YellowstonePathology.YpiConnect.Client
{
    /// <summary>
    /// Interaction logic for ReportBrowser.xaml
    /// </summary>
	public partial class ReportBrowserPage : Page, YellowstonePathology.Business.Interface.IPersistPageChanges
    {
		private ReportBrowserUI m_ReportBrowserUI;
        
		public ReportBrowserPage()
        {
            this.m_ReportBrowserUI = new ReportBrowserUI();

			InitializeComponent();
            
            this.DataContext = this.m_ReportBrowserUI;
            this.m_ReportBrowserUI.DoCasesNotDownloadedSearch();
			Loaded += new RoutedEventHandler(ReportBrowserPage_Loaded);
        }

		private void ReportBrowserPage_Loaded(object sender, RoutedEventArgs e)
		{
			UserInteractionMonitor.Instance.Register(this);
		}

		public ReportBrowserUI ReportBrowserUI
		{
			get { return this.m_ReportBrowserUI; }
		}

        private void HyperlinkViewDocument_Click(object sender, RoutedEventArgs e)
        {
			if (this.ListViewSearchResults.SelectedItems.Count != 0)
            {
				YellowstonePathology.YpiConnect.Contract.Search.SearchResult searchResult = (YellowstonePathology.YpiConnect.Contract.Search.SearchResult)this.ListViewSearchResults.SelectedItem;
                if (searchResult.FinalTime.HasValue == true)
                {
                    this.m_ReportBrowserUI.ViewDocument(searchResult);
                }
                else
                {
                    MessageBox.Show("This Report is not final and cannot be downloaded.");
                }
            }             
        }
        
        private void HyperlinkRecentCases_Click(object sender, RoutedEventArgs e)
        {
            this.m_ReportBrowserUI.DoClientRecentCaseSearch();
            this.m_ReportBrowserUI.SortByPatientName();
            this.SetListViewToTop();
        }

        private void HyperlinkPatientNameSearch_Click(object sender, RoutedEventArgs e)
        {
			PatientNameSearch patientNameSearch = new PatientNameSearch();
			PatientNameSearchPage patientNameSearchPage = new PatientNameSearchPage(patientNameSearch);
			patientNameSearchPage.DoPatientNameSearch += new EventHandler(this.DoPatientNameSearch);
			ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(patientNameSearchPage);
			this.SetListViewToTop();
        }

        private void HyperlinkSelectAll_Click(object sender, RoutedEventArgs e)
        {            
		    this.ListViewSearchResults.SelectAll();         
        }        

        private void HyperlinkPrint_Click(object sender, RoutedEventArgs e)
        {            
			if (this.ListViewSearchResults.SelectedItems.Count != 0)
            {
                List<YellowstonePathology.YpiConnect.Contract.MethodResult> methodResultList = new List<Contract.MethodResult>();
				foreach (YellowstonePathology.YpiConnect.Contract.Search.SearchResult searchResult in this.ListViewSearchResults.SelectedItems)
                {
                    CaseDocument caseDocument = new CaseDocument(searchResult.ReportNo, Contract.CaseDocumentTypeEnum.XPS);
                    YellowstonePathology.YpiConnect.Contract.MethodResult methodResult = caseDocument.Print();
                    if (methodResult.Success == false)
                    {
                        methodResultList.Add(methodResult);
                    }
                }
                if (methodResultList.Count == 0)
                {
                    MessageBox.Show("Your Document(s) have been sent to the printer.");
                }
                else
                {
                    MessageBox.Show("We were unable to print " + methodResultList.Count.ToString() + " of your documents.");
                }
            }            
        }

        private void HyperlinkAcknowledge_Click(object sender, RoutedEventArgs e)
        {            
			if (this.ListViewSearchResults.SelectedItems.Count != 0)
            {
                string reportDistributionLogIdString = GetReportDistributionLogIdStringList();
                if (string.IsNullOrEmpty(reportDistributionLogIdString) == false)
                {
                    this.m_ReportBrowserUI.DoAcknowledgeDistributions(reportDistributionLogIdString);
                }
            }            
        }

        private string GetReportDistributionLogIdStringList()
        {            
            StringBuilder result = new StringBuilder();
			foreach (YellowstonePathology.YpiConnect.Contract.Search.SearchResult searchResult in this.ListViewSearchResults.SelectedItems)
            {
                if (string.IsNullOrEmpty(searchResult.ReportDistributionLogId) == false)
                {
                    result.Append(searchResult.ReportDistributionLogId + ", ");
                }            
            }
            if (result.Length != 0)
            {
                result.Remove(result.Length - 2, 2);
            }
            return result.ToString();                     
        }                

        private void HyperlinkCasesNotAcknowledged_Click(object sender, RoutedEventArgs e)
        {
            this.m_ReportBrowserUI.DoCasesNotAcknowledgedSearch();
            this.SetListViewToTop();
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

        private void HyperlinkHome_Click(object sender, RoutedEventArgs e)
        {
			HomePage homePage = new HomePage();
			ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(homePage);
        }

        private void HyperlinkDownload_Click(object sender, RoutedEventArgs e)
        {
			if (this.ListViewSearchResults.SelectedItems.Count != 0)
			{
				if (YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount.EnableFileDownload == true &&
					string.IsNullOrEmpty(YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount.LocalFileDownloadDirectory) == false)
				{
					if (this.ListViewSearchResults.SelectedItems.Count != 0)
					{
						Mouse.OverrideCursor = Cursors.Wait;
						foreach (YellowstonePathology.YpiConnect.Contract.Search.SearchResult searchResult in this.ListViewSearchResults.SelectedItems)
						{
                            YellowstonePathology.YpiConnect.Contract.CaseDocumentTypeEnum caseDocumentType = (YellowstonePathology.YpiConnect.Contract.CaseDocumentTypeEnum)Enum.Parse(typeof(YellowstonePathology.YpiConnect.Contract.CaseDocumentTypeEnum), YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount.DownloadFileType);
							CaseDocument caseDocument = new CaseDocument(searchResult.ReportNo, caseDocumentType);
							YellowstonePathology.YpiConnect.Contract.MethodResult methodResult = caseDocument.Save();
							if (methodResult.Success == false)
							{
								string message = "There was an error trying to download the file for " + searchResult.ReportNo + 
									".  A message about the error has been sent to YPII support. \n\nWould you like to continue with any other downloads?";
								MessageBoxResult result = MessageBox.Show(message, "Download Error", MessageBoxButton.YesNo, MessageBoxImage.Question);
								if (result == MessageBoxResult.No)
								{
									break;
								}
							}
						}
						Mouse.OverrideCursor = null;
						MessageBox.Show("Download complete.");
					}
				}
				else
				{
					FileDownloadSettingsPage fileDownloadSettingsPage = new FileDownloadSettingsPage();
					ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(fileDownloadSettingsPage);
				}
			}
        }

		private void FileDownloadSettingsPage_DownloadSettingsSaved(object sender, EventArgs e)
		{
			FileDownloadSettingsSavedPage fileDownloadSettingsSavedPage = new FileDownloadSettingsSavedPage();
			ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(fileDownloadSettingsSavedPage);
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

        private void HyperlinkViewMessages_Click(object sender, RoutedEventArgs e)
        {
            ViewMessagesPage viewMessagePage = new ViewMessagesPage();
            ApplicationNavigator.ApplicationContentFrame.Navigate(viewMessagePage);
        }        

		public void DoPatientNameSearch(object sender, EventArgs e)
		{
			NameSearchEventArgs args = (NameSearchEventArgs)e;
			PatientNameSearch patientNameSearch = args.PatientNameSearch;
			if (string.IsNullOrEmpty(patientNameSearch.LastNameSearchString) == false)
			{
				if (string.IsNullOrEmpty(patientNameSearch.FirstNameSearchString) == false)
				{
					this.m_ReportBrowserUI.DoClientLastAndFirstNameSearch(patientNameSearch.LastNameSearchString, patientNameSearch.FirstNameSearchString);
				}
				else
				{
					this.m_ReportBrowserUI.DoClientLastNameSearch(patientNameSearch.LastNameSearchString);
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
				this.m_ReportBrowserUI.DoClientDateOfBirthSearch(birthdate);
				this.SetListViewToTop();
			}
		}

		public void DoPatientSsnSearch(object sender, EventArgs e)
		{
			SearchStringEventArgs args = (SearchStringEventArgs)e;
			this.m_ReportBrowserUI.DoClientSSNSearch(args.SearchString);
			this.SetListViewToTop();
		}

		private void ListViewSearchResults_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (this.ListViewSearchResults.SelectedItem != null)
			{
				this.m_ReportBrowserUI.ViewDocument((YellowstonePathology.YpiConnect.Contract.Search.SearchResult)this.ListViewSearchResults.SelectedItem);                
			}
		}

        private void MenuItemShowResultSummaryText_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewSearchResults.SelectedItem != null)
            {
                YellowstonePathology.YpiConnect.Contract.Search.SearchResult searchResult = (YellowstonePathology.YpiConnect.Contract.Search.SearchResult)this.ListViewSearchResults.SelectedItem;
                if (searchResult.FinalTime.HasValue == true)
                {
                    YellowstonePathology.YpiConnect.Proxy.FileTransferServiceProxy fileTransferServiceProxy = new Proxy.FileTransferServiceProxy();
                    string summaryResultString = fileTransferServiceProxy.GetSummaryResultString(searchResult.ReportNo);
                    ResultSummaryTextDialog resultSummaryTextDialog = new ResultSummaryTextDialog(summaryResultString);
                    resultSummaryTextDialog.ShowDialog();
                }
                else
                {
                    MessageBox.Show("The result summary is not available until the case is final.");
                }
            }
        }

		private void SetListViewToTop()
		{
			if (this.ListViewSearchResults.Items.Count > 0)
			{
				this.ListViewSearchResults.ScrollIntoView(this.ListViewSearchResults.Items[0]);
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
