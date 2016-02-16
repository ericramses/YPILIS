using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Login.FinalizeAccession
{
	public class AssignmentPath
	{
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        private LoginPageWindow m_LoginPageWindow;
        private YellowstonePathology.Business.Domain.Lock m_Lock;

		public AssignmentPath(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;
        }        

        public void Start()
        {
            this.m_LoginPageWindow = new LoginPageWindow(this.m_SystemIdentity);
			if (Business.User.SystemIdentity.DoesLoggedInUserNeedToScanId() == true)
			{
				this.ShowScanSecurityBadgePage();
			}
			else
			{
				this.m_SystemIdentity = Business.User.SystemIdentity.Instance;
				this.m_LoginPageWindow.SystemIdentity = this.m_SystemIdentity;
				this.ShowCaseLockPage();
			}
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
				FinalizeAccession.AssignmentPage AssignmentPage = new FinalizeAccession.AssignmentPage(this.m_AccessionOrder);
				AssignmentPage.Return += new FinalizeAccession.AssignmentPage.ReturnEventHandler(AssignmentPage_Return);
				this.m_LoginPageWindow.PageNavigator.Navigate(AssignmentPage);
            }
            else
            {
                this.m_LoginPageWindow.Close();
            }
        }

		private void AssignmentPage_Return(object sender, UI.Navigation.PageNavigationReturnEventArgs e)
        {
			this.m_Lock.ReleaseUserLocks();
            this.m_LoginPageWindow.Close();			
		}
	}
}
