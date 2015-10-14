using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
    public class ResultPath
    {
        public delegate void FinishEventHandler(object sender, EventArgs e);
        public event FinishEventHandler Finish;        

		public delegate void AuthenticatedEventHandler(object sender, EventArgs e);
		public event AuthenticatedEventHandler Authenticated;        

		protected ResultDialog m_ResultDialog;
        protected YellowstonePathology.UI.Navigation.PageNavigator m_PageNavigator;
		protected YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity; 
               

		public ResultPath(YellowstonePathology.UI.Navigation.PageNavigator pageNavigator, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            this.m_SystemIdentity = systemIdentity;
			this.m_PageNavigator = pageNavigator;         
        }

        public virtual void Start()
        {
			if (Business.User.SystemIdentity.DoesLoggedInUserNeedToScanId() == true)
			{
				this.ShowScanSecurityBadgePage();
			}
			else
			{
				this.m_SystemIdentity = new Business.User.SystemIdentity(Business.User.SystemIdentityTypeEnum.CurrentlyLoggedIn);
				this.Authenticated(this, new EventArgs());
			}			
        }

        public void RegisterCancelATest(IResultPage resultPage)
        {
            resultPage.CancelTest += new CustomEventArgs.EventHandlerDefinitions.CancelTestEventHandler(CancelTest);
        }        
        
        private void CancelTest(object sender, YellowstonePathology.UI.CustomEventArgs.CancelTestEventArgs e)
        {
            CancelATestPath cancelATestPath = new CancelATestPath(e, this.m_PageNavigator, this.m_SystemIdentity);
            cancelATestPath.Finish += new FinishEventHandler(CancellATestPath_Finish);
            cancelATestPath.Back += new CancelATestPath.BackEventHandler(CancellATestPath_Back);
            cancelATestPath.Start();
        }

        private void CancellATestPath_Back(object sender, YellowstonePathology.UI.CustomEventArgs.CancelTestEventArgs e)
        {
            System.Windows.Controls.UserControl userControl = (System.Windows.Controls.UserControl)e.ResultPage;
            this.m_PageNavigator.Navigate(userControl);
        }

        private void CancellATestPath_Finish(object sender, EventArgs e)
        {
            this.Finish(this, new EventArgs());
        }

		private void ShowScanSecurityBadgePage()
		{
			YellowstonePathology.UI.Login.ScanSecurityBadgePage scanSecurityBadgePage = new Login.ScanSecurityBadgePage(System.Windows.Visibility.Collapsed);
			scanSecurityBadgePage.AuthentificationSuccessful += new Login.ScanSecurityBadgePage.AuthentificationSuccessfulEventHandler(ScanSecurityBadgePage_AuthentificationSuccessful);
			this.m_PageNavigator.Navigate(scanSecurityBadgePage);
		}

		protected void ScanSecurityBadgePage_AuthentificationSuccessful(object sender, CustomEventArgs.SystemIdentityReturnEventArgs e)
		{
			this.m_SystemIdentity = e.SystemIdentity;
			this.Authenticated(this, new EventArgs());
		}                 

        public void Finished()
        {
            if (this.Finish != null) this.Finish(this, new EventArgs());
        }        
    }
}
