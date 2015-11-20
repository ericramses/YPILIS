using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Login.FinalizeAccession
{
	public class ProviderDistributionPath
	{
        public delegate void NextEventHandler(object sender, EventArgs e);
        public event NextEventHandler Next;        

        public delegate void BackEventHandler(object sender, EventArgs e);
        public event BackEventHandler Back;

		private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private string m_ReportNo;
        private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        private LoginPageWindow m_LoginPageWindow;
        private YellowstonePathology.Business.Domain.Lock m_Lock;

        private System.Windows.Visibility m_NextButtonVisibility;
        private System.Windows.Visibility m_CloseButtonVisibility;
        private System.Windows.Visibility m_BackButtonVisibility;

        public ProviderDistributionPath(string reportNo, YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.Business.Persistence.ObjectTracker objectTracker,
            System.Windows.Visibility nextButtonVisibility,
            System.Windows.Visibility closeButtonVisibility,
            System.Windows.Visibility backButtonVisibility)
        {
            this.m_ReportNo = reportNo;
            this.m_AccessionOrder = accessionOrder;
            this.m_ObjectTracker = objectTracker;

            this.m_NextButtonVisibility = nextButtonVisibility;
            this.m_CloseButtonVisibility = closeButtonVisibility;
            this.m_BackButtonVisibility = backButtonVisibility;
        }        

        public void Start()
        {
            this.m_LoginPageWindow = new LoginPageWindow(this.m_SystemIdentity);
            this.m_LoginPageWindow.Width = System.Windows.SystemParameters.PrimaryScreenWidth * 0.80;

			if (Business.User.SystemIdentity.DoesLoggedInUserNeedToScanId() == true)
			{
				this.ShowScanSecurityBadgePage();
			}
			else
			{
				this.m_SystemIdentity = new Business.User.SystemIdentity(Business.User.SystemIdentityTypeEnum.CurrentlyLoggedIn);
				this.m_LoginPageWindow.SystemIdentity = this.m_SystemIdentity;
				this.ShowCaseLockPage();
			}
			this.m_LoginPageWindow.ShowDialog();
        }

        public void Start(YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            this.m_SystemIdentity = systemIdentity;
            this.m_LoginPageWindow = new LoginPageWindow(this.m_SystemIdentity);
            this.m_LoginPageWindow.Width = System.Windows.SystemParameters.PrimaryScreenWidth * 0.80;

            this.ShowCaseLockPage();
            this.m_LoginPageWindow.ShowDialog();
        }

        private void ShowScanSecurityBadgePage()
        {
            YellowstonePathology.UI.Login.ScanSecurityBadgePage scanSecurityBadgePage = new ScanSecurityBadgePage(System.Windows.Visibility.Collapsed);
			this.m_LoginPageWindow.PageNavigator.Navigate(scanSecurityBadgePage);
			scanSecurityBadgePage.AuthentificationSuccessful += new ScanSecurityBadgePage.AuthentificationSuccessfulEventHandler(ScanSecurityBadgePage_AuthentificationSuccessful);
		}

		private void ScanSecurityBadgePage_AuthentificationSuccessful(object sender, CustomEventArgs.SystemIdentityReturnEventArgs e)
		{
			this.m_SystemIdentity = e.SystemIdentity;
			this.m_LoginPageWindow.SystemIdentity = this.m_SystemIdentity;
			this.ShowCaseLockPage();
		}

		private void ShowCaseLockPage()
		{
			this.m_Lock = new Business.Domain.Lock(this.m_SystemIdentity);
			CaseLockPage caseLockPage = new CaseLockPage(this.m_LoginPageWindow.PageNavigator, this.m_Lock, this.m_AccessionOrder);
			caseLockPage.Return += new CaseLockPage.ReturnEventHandler(CaseLockPage_Return);
			caseLockPage.AttemptCaseLock();
		}

        private void CaseLockPage_Return(object sender, UI.Navigation.PageNavigationReturnEventArgs e)
        {
            CaseLockPage caseLockPage = (CaseLockPage)sender;
            if (caseLockPage.Lock.LockAquired == true)
            {				
                FinalizeAccession.ProviderDistributionPage providerDistributionPage = new FinalizeAccession.ProviderDistributionPage(this.m_ReportNo, this.m_AccessionOrder, this.m_ObjectTracker, this.m_LoginPageWindow.PageNavigator, 
                   this.m_NextButtonVisibility, this.m_CloseButtonVisibility, this.m_BackButtonVisibility);
                providerDistributionPage.Close += new FinalizeAccession.ProviderDistributionPage.CloseEventHandler(ProviderDetailPage_Close);
                providerDistributionPage.Next += new ProviderDistributionPage.NextEventHandler(ProviderDistributionPage_Next);
                providerDistributionPage.Back += new ProviderDistributionPage.BackEventHandler(ProviderDistributionPage_Back);
                this.m_LoginPageWindow.PageNavigator.Navigate(providerDistributionPage);
            }
            else
            {
                this.m_LoginPageWindow.Close();
            }
        }

        private void ProviderDistributionPage_Next(object sender, EventArgs e)
        {
            if (this.Next != null) this.Next(this, new EventArgs());   
        }

        private void ProviderDistributionPage_Back(object sender, EventArgs e)
        {
            if (this.Back != null) this.Back(this, new EventArgs());   
        }

		private void ProviderDetailPage_Close(object sender, EventArgs e)
        {
			this.m_Lock.ReleaseUserLocks();
            this.m_LoginPageWindow.Close();			
		}
	}
}
