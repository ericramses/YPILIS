using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace YellowstonePathology.YpiConnect.Client
{
	public class BillingBrowserUI : INotifyPropertyChanged
	{
		public delegate void PropertyChangedNotificationHandler(String info);
		public event PropertyChangedEventHandler PropertyChanged;

		private YellowstonePathology.YpiConnect.Contract.Billing.BillingAccessionCollection m_BillingAccessionCollection;
		private YellowstonePathology.YpiConnect.Proxy.BillingServiceProxy m_BillingServiceProxy;

		public BillingBrowserUI()
		{
			this.m_BillingServiceProxy = new Proxy.BillingServiceProxy();
		}

		public YellowstonePathology.YpiConnect.Contract.Billing.BillingAccessionCollection BillingAccessionCollection
		{
			get { return this.m_BillingAccessionCollection; }
		}

		public void GetTodaysBilling()
		{
            this.m_BillingAccessionCollection = this.m_BillingServiceProxy.GetBillingAccessionsByPostDate(DateTime.Today, YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount);
			this.NotifyPropertyChanged("");
		}

		public void GetYesterdaysBilling()
		{
			DateTime yesterday = DateTime.Today.AddDays(-1);
			if (yesterday.DayOfWeek == DayOfWeek.Sunday) yesterday = yesterday.AddDays(-2);
			if (yesterday.DayOfWeek == DayOfWeek.Saturday) yesterday = yesterday.AddDays(-1);
			this.m_BillingAccessionCollection = this.m_BillingServiceProxy.GetBillingAccessionsByPostDate(yesterday, YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount);
			this.NotifyPropertyChanged("");
		}

        public void GetRecentBilling()
        {
            this.m_BillingAccessionCollection = this.m_BillingServiceProxy.GetRecentBillingAccessions(YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount);
            this.NotifyPropertyChanged("");
        }

		public YellowstonePathology.YpiConnect.Contract.Billing.BillingDetail GetBillingDetail(YellowstonePathology.YpiConnect.Contract.Billing.BillingAccession billingAccession)
		{
			YellowstonePathology.YpiConnect.Contract.Billing.BillingDetail billingDetail = this.m_BillingServiceProxy.GetBillingDetail(billingAccession.ReportNo, false, YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount);
			return billingDetail;
		}

		public void DoPatientNameSearch(object sender, EventArgs e)
		{
			NameSearchEventArgs args = (NameSearchEventArgs)e;
			PatientNameSearch patientNameSearch = args.PatientNameSearch;
			if (string.IsNullOrEmpty(patientNameSearch.LastNameSearchString) == false)
			{
				if (string.IsNullOrEmpty(patientNameSearch.FirstNameSearchString) == false)
				{
					this.DoLastAndFirstNameSearch(patientNameSearch.LastNameSearchString, patientNameSearch.FirstNameSearchString);
				}
				else
				{
					this.DoLastNameSearch(patientNameSearch.LastNameSearchString);
				}
				this.NotifyPropertyChanged("");

				//BillingBrowserPage billingBrowserPage = new BillingBrowserPage();
				//MainWindow mainWindow = (MainWindow)System.Windows.Application.Current.MainWindow;
				//mainWindow.MainFrame.NavigationService.Navigate(billingBrowserPage);
				//ApplicationNavigator.GoToPage(PageNavigationEnum.BillingBrowserPage);
			}
		}

		public void DoLastNameSearch(string lastName)
		{
			this.m_BillingAccessionCollection = this.m_BillingServiceProxy.GetBillingAccessionsByLastName(lastName, YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount);
		}

		public void DoLastAndFirstNameSearch(string lastName, string firstName)
		{
			this.m_BillingAccessionCollection = this.m_BillingServiceProxy.GetBillingAccessionsByLastNameAndFirstName(lastName, firstName, YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount);
		}

		public void DoReportNoSearch(object sender, EventArgs e)
		{
			SearchStringEventArgs args = (SearchStringEventArgs)e;
			this.m_BillingAccessionCollection = this.m_BillingServiceProxy.GetBillingAccessionsByReportNo(args.SearchString, YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount);
			this.NotifyPropertyChanged("");

			//BillingBrowserPage billingBrowserPage = new BillingBrowserPage();
			//MainWindow mainWindow = (MainWindow)System.Windows.Application.Current.MainWindow;
			//mainWindow.MainFrame.NavigationService.Navigate(billingBrowserPage);
			//ApplicationNavigator.GoToPage(PageNavigationEnum.BillingBrowserPage);
		}

		public void DoPatientBirthdateSearch(object sender, EventArgs e)
		{
			SearchStringEventArgs args = (SearchStringEventArgs)e;
			DateTime birthdate;
			bool isValid = DateTime.TryParse(args.SearchString, out birthdate);
			if (isValid)
			{
				this.m_BillingAccessionCollection = this.m_BillingServiceProxy.GetBillingAccessionsByBirthdate(birthdate, YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount);
				this.NotifyPropertyChanged("");


				//BillingBrowserPage billingBrowserPage = new BillingBrowserPage();
				//MainWindow mainWindow = (MainWindow)System.Windows.Application.Current.MainWindow;
				//mainWindow.MainFrame.NavigationService.Navigate(billingBrowserPage);
				//ApplicationNavigator.GoToPage(PageNavigationEnum.BillingBrowserPage);
			}
		}

		public void DoPatientSsnSearch(object sender, EventArgs e)
		{
			SearchStringEventArgs args = (SearchStringEventArgs)e;
			this.m_BillingAccessionCollection = this.m_BillingServiceProxy.GetBillingAccessionsBySsn(args.SearchString, YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount);
			this.NotifyPropertyChanged("");


			BillingBrowserPage billingBrowserPage = new BillingBrowserPage();
			ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(billingBrowserPage);
			//ApplicationNavigator.GoToPage(PageNavigationEnum.BillingBrowserPage);
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
