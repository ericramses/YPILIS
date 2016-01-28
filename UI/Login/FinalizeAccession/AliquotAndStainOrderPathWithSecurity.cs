using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Login.FinalizeAccession
{
	public class AliquotAndStainOrderPathWithSecurity
	{
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;
        
        private LoginPageWindow m_LoginPageWindow;
        private YellowstonePathology.Business.Domain.Lock m_Lock;

        public AliquotAndStainOrderPathWithSecurity(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder)
        {            
            this.m_AccessionOrder = accessionOrder;
			this.m_PanelSetOrder = panelSetOrder;
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
				this.m_SystemIdentity = new Business.User.SystemIdentity(Business.User.SystemIdentityTypeEnum.CurrentlyLoggedIn);
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
                YellowstonePathology.UI.Login.FinalizeAccession.AliquotAndStainOrderPath stainOrderPath = 
                    new AliquotAndStainOrderPath(this.m_AccessionOrder, this.m_PanelSetOrder, this.m_SystemIdentity, this.m_LoginPageWindow.PageNavigator);
				stainOrderPath.Start();
                stainOrderPath.Return += new AliquotAndStainOrderPath.ReturnEventHandler(AliquotAndStainOrderPath_Return);
            }
            else
            {
                this.m_LoginPageWindow.Close();
            }
        }

        private void AliquotAndStainOrderPath_Return(object sender, UI.Navigation.PageNavigationReturnEventArgs e)
        {            			
            this.m_LoginPageWindow.Close();			
		}
	}
}
