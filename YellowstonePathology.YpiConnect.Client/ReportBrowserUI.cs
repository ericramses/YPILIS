using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace YellowstonePathology.YpiConnect.Client
{
    public class ReportBrowserUI : INotifyPropertyChanged
    {
        public delegate void PropertyChangedNotificationHandler(String info);
        public event PropertyChangedEventHandler PropertyChanged;

        private YellowstonePathology.YpiConnect.Contract.Search.SearchResultCollection m_SearchResults;

        YellowstonePathology.YpiConnect.Proxy.SearchServiceProxy m_SearchServiceProxy;
        YellowstonePathology.YpiConnect.Proxy.FileTransferServiceProxy m_FileTransferServiceProxy;		

        public ReportBrowserUI()
        {		
            this.m_SearchServiceProxy = new YpiConnect.Proxy.SearchServiceProxy();
			this.m_FileTransferServiceProxy = new YpiConnect.Proxy.FileTransferServiceProxy();            
        }		

        public void SortByPatientName()
        {
            this.m_SearchResults.Sort(PatientNameComparison);            
        }

        private Comparison<YellowstonePathology.YpiConnect.Contract.Search.SearchResult> PatientNameComparison = 
            delegate(YellowstonePathology.YpiConnect.Contract.Search.SearchResult result1, YellowstonePathology.YpiConnect.Contract.Search.SearchResult result2)
        {
            return result1.PatientName.CompareTo(result2.PatientName);
        };

        public void DoCasesNotDownloadedSearch()
        {            
            YellowstonePathology.YpiConnect.Contract.Search.Search search = new YpiConnect.Contract.Search.Search();
            search.SearchType = YellowstonePathology.YpiConnect.Contract.Search.SearchTypeEnum.NotDownloaded;
			search.WebServiceAccount = YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount;
            this.m_SearchResults = this.m_SearchServiceProxy.ExecuteClientSearch(search);
            this.NotifyPropertyChanged("SearchResults");		
        }

        public void DoAcknowledgeDistributions(string reportDistributionLogIdStringList)
        {            
            this.m_SearchServiceProxy.AcknowledgeDistributions(reportDistributionLogIdStringList);
			this.DoCasesNotAcknowledgedSearch();			
        }

        public void DoCasesNotAcknowledgedSearch()
        {            
            YellowstonePathology.YpiConnect.Contract.Search.Search search = new YpiConnect.Contract.Search.Search();
            search.SearchType = YellowstonePathology.YpiConnect.Contract.Search.SearchTypeEnum.NotAcknowledged;
			search.WebServiceAccount = YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount;
            this.m_SearchResults = this.m_SearchServiceProxy.ExecuteClientSearch(search);
			this.NotifyPropertyChanged("");		
        }

        public void DoClientRecentCaseSearch()
        {
            YellowstonePathology.YpiConnect.Contract.Search.Search search = new YellowstonePathology.YpiConnect.Contract.Search.Search();
            search.SearchType = YellowstonePathology.YpiConnect.Contract.Search.SearchTypeEnum.RecentCases;
            search.SearchParameters = new List<object>();
            search.SearchParameters.Add(DateTime.Today.AddDays(-30));
			search.WebServiceAccount = YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount;
            this.m_SearchResults = this.m_SearchServiceProxy.ExecuteClientSearch(search);
			this.NotifyPropertyChanged("");         			
        }

		public void DoPathologistRecentCaseSearch()
		{
			YellowstonePathology.YpiConnect.Contract.Search.Search search = new YellowstonePathology.YpiConnect.Contract.Search.Search();
			search.SearchType = YellowstonePathology.YpiConnect.Contract.Search.SearchTypeEnum.RecentCases;
			search.SearchParameters = new List<object>();
			search.SearchParameters.Add(DateTime.Today.AddDays(-30));
            search.SearchParameters.Add(YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount.SystemUserId);
			search.WebServiceAccount = YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount;
			this.m_SearchResults = this.m_SearchServiceProxy.ExecutePathologistSearch(search);
			this.NotifyPropertyChanged("");
		}

		public void DoFacilityRecentCaseSearch()
		{
			YellowstonePathology.YpiConnect.Contract.Search.Search search = new YellowstonePathology.YpiConnect.Contract.Search.Search();
			search.SearchType = YellowstonePathology.YpiConnect.Contract.Search.SearchTypeEnum.RecentCasesForFacilityId;
			search.SearchParameters = new List<object>();
			search.SearchParameters.Add(DateTime.Today.AddDays(-30));
			search.SearchParameters.Add(YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount.FacilityId);
			search.WebServiceAccount = YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount;
			this.m_SearchResults = this.m_SearchServiceProxy.ExecutePathologistSearch(search);
			this.NotifyPropertyChanged("");
		}

        public void DoClientLastNameSearch(string lastName)
        {
            YellowstonePathology.YpiConnect.Contract.Search.Search search = new YellowstonePathology.YpiConnect.Contract.Search.Search();
            search.SearchType = YellowstonePathology.YpiConnect.Contract.Search.SearchTypeEnum.PatientLastNameSearch;
            search.SearchParameters = new List<object>();
            search.SearchParameters.Add(lastName);
			search.WebServiceAccount = YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount;
            this.m_SearchResults = this.m_SearchServiceProxy.ExecuteClientSearch(search);
			this.NotifyPropertyChanged("");			
        }

		public void DoPathologistLastNameSearch(string lastName)
		{
			YellowstonePathology.YpiConnect.Contract.Search.Search search = new YellowstonePathology.YpiConnect.Contract.Search.Search();
			search.SearchType = YellowstonePathology.YpiConnect.Contract.Search.SearchTypeEnum.PatientLastNameSearch;
			search.SearchParameters = new List<object>();
			search.SearchParameters.Add(lastName);
            search.SearchParameters.Add(YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount.SystemUserId);
			search.WebServiceAccount = YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount;
			this.m_SearchResults = this.m_SearchServiceProxy.ExecutePathologistSearch(search);
			this.NotifyPropertyChanged("");
		}

		public void DoClientLastAndFirstNameSearch(string lastName, string firstName)
		{
			YellowstonePathology.YpiConnect.Contract.Search.Search search = new YellowstonePathology.YpiConnect.Contract.Search.Search();
			search.SearchType = YellowstonePathology.YpiConnect.Contract.Search.SearchTypeEnum.PatientLastAndFirstNameSearch;
			search.SearchParameters = new List<object>();
			search.SearchParameters.Add(lastName);
			search.SearchParameters.Add(firstName);
			search.WebServiceAccount = YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount;
			this.m_SearchResults = this.m_SearchServiceProxy.ExecuteClientSearch(search);
			this.NotifyPropertyChanged("");
		}

        public void DoPathologistLastAndFirstNameSearch(string lastName, string firstName)
        {
            YellowstonePathology.YpiConnect.Contract.Search.Search search = new YellowstonePathology.YpiConnect.Contract.Search.Search();
            search.SearchType = YellowstonePathology.YpiConnect.Contract.Search.SearchTypeEnum.PatientLastAndFirstNameSearch;
            search.SearchParameters = new List<object>();
			search.SearchParameters.Add(lastName);
            search.SearchParameters.Add(firstName);
            search.SearchParameters.Add(YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount.SystemUserId);
			search.WebServiceAccount = YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount;
			this.m_SearchResults = this.m_SearchServiceProxy.ExecutePathologistSearch(search);
			this.NotifyPropertyChanged("");                     			
        }

        public void DoClientDateOfBirthSearch(DateTime dateOfBirth)
        {
            YellowstonePathology.YpiConnect.Contract.Search.Search search = new YellowstonePathology.YpiConnect.Contract.Search.Search();
            search.SearchType = YellowstonePathology.YpiConnect.Contract.Search.SearchTypeEnum.DateOfBirth;
            search.SearchParameters = new List<object>();
            search.SearchParameters.Add(dateOfBirth);
			search.WebServiceAccount = YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount;
            this.m_SearchResults = this.m_SearchServiceProxy.ExecuteClientSearch(search);
			this.NotifyPropertyChanged("");         			
        }

		public void DoPathologistDateOfBirthSearch(DateTime dateOfBirth)
		{
			YellowstonePathology.YpiConnect.Contract.Search.Search search = new YellowstonePathology.YpiConnect.Contract.Search.Search();
			search.SearchType = YellowstonePathology.YpiConnect.Contract.Search.SearchTypeEnum.DateOfBirth;
			search.SearchParameters = new List<object>();
			search.SearchParameters.Add(dateOfBirth);
            search.SearchParameters.Add(YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount.SystemUserId);
			search.WebServiceAccount = YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount;
			this.m_SearchResults = this.m_SearchServiceProxy.ExecutePathologistSearch(search);
			this.NotifyPropertyChanged("");
		}

		public void DoClientSSNSearch(string ssn)
		{
			YellowstonePathology.YpiConnect.Contract.Search.Search search = new YellowstonePathology.YpiConnect.Contract.Search.Search();
			search.SearchType = YellowstonePathology.YpiConnect.Contract.Search.SearchTypeEnum.SocialSecurityNumber;
			search.SearchParameters = new List<object>();
			search.SearchParameters.Add(ssn);
			search.WebServiceAccount = YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount;
			this.m_SearchResults = this.m_SearchServiceProxy.ExecuteClientSearch(search);
			this.NotifyPropertyChanged("");
		}

        public void DoPathologistSSNSearch(string ssn)
        {
            YellowstonePathology.YpiConnect.Contract.Search.Search search = new YellowstonePathology.YpiConnect.Contract.Search.Search();
            search.SearchType = YellowstonePathology.YpiConnect.Contract.Search.SearchTypeEnum.SocialSecurityNumber;
            search.SearchParameters = new List<object>();
			search.SearchParameters.Add(ssn);
            search.SearchParameters.Add(YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount.SystemUserId);
			search.WebServiceAccount = YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount;
			this.m_SearchResults = this.m_SearchServiceProxy.ExecutePathologistSearch(search);
			this.NotifyPropertyChanged("");         			
        }

		public void Refresh()
		{
		}

        public void ViewDocument(YellowstonePathology.YpiConnect.Contract.Search.SearchResult searchResult)
        {
            CaseDocument caseDocument = new CaseDocument(searchResult.ReportNo, YellowstonePathology.YpiConnect.Contract.CaseDocumentTypeEnum.XPS);

            YellowstonePathology.YpiConnect.Contract.MethodResult methodResult = caseDocument.Download();                        
            if (methodResult.Success == true)
            {
                XpsDocumentViewer xpsDocumentViewer = new XpsDocumentViewer();
                xpsDocumentViewer.LoadDocument(caseDocument.XpsDocument);
				xpsDocumentViewer.ShowDialog();
            }
            else
            {
                System.Windows.MessageBox.Show(methodResult.Message);
            }            
        }                              

        public List<YpiConnect.Contract.Search.SearchResult> SearchResults
        {
            get { return this.m_SearchResults; }            
        }

        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
	}
}
