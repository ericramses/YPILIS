using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Login
{
	public class LabOrderAcknowledgementPath
	{
		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        private LoginPageWindow m_LoginPageWindow;

        public LabOrderAcknowledgementPath()
        {
            this.m_LoginPageWindow = new LoginPageWindow(this.m_SystemIdentity);
			this.m_LoginPageWindow.Width = 850;
        }

        public void Start()
        {
			if (Business.User.SystemIdentity.DoesLoggedInUserNeedToScanId() == true)
			{
				this.ShowScanSecurityBadgePage();
			}
			else
			{
				this.m_SystemIdentity = Business.User.SystemIdentity.Instance;
				this.m_LoginPageWindow.SystemIdentity = this.m_SystemIdentity;
				this.ShowLabOrderAcknowledgementPage();
			}
			this.m_LoginPageWindow.ShowDialog();
        }

        private void ShowScanSecurityBadgePage()
        {
            YellowstonePathology.UI.Login.ScanSecurityBadgePage scanSecurityBadgePage = new ScanSecurityBadgePage(System.Windows.Visibility.Collapsed);
			scanSecurityBadgePage.AuthentificationSuccessful += new ScanSecurityBadgePage.AuthentificationSuccessfulEventHandler(ScanSecurityBadgePage_AuthentificationSuccessful);
            this.m_LoginPageWindow.PageNavigator.Navigate(scanSecurityBadgePage);
        }

		private void ScanSecurityBadgePage_AuthentificationSuccessful(object sender, CustomEventArgs.SystemIdentityReturnEventArgs e)
        {
			this.m_SystemIdentity = e.SystemIdentity;
			this.m_LoginPageWindow.SystemIdentity = this.m_SystemIdentity;
			this.ShowLabOrderAcknowledgementPage();
        }

        private void ShowLabOrderAcknowledgementPage()
        {
            YellowstonePathology.UI.Login.LabOrderAcknowledgementPage labOrderAcknowledgementPage = new LabOrderAcknowledgementPage(this.m_SystemIdentity);
            labOrderAcknowledgementPage.Return += new LabOrderAcknowledgementPage.ReturnEventHandler(LabOrderAcknowledgementPage_Return);
            this.m_LoginPageWindow.PageNavigator.Navigate(labOrderAcknowledgementPage);
        }

        private void LabOrderAcknowledgementPage_Return(object sender, UI.Navigation.PageNavigationReturnEventArgs e)
        {
			this.m_LoginPageWindow.Close();
        }
	}
}
