using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Login
{
	public class SpecimenOrderDetailsPath
	{
        public delegate void FinishEventHandler(object sender, EventArgs e);
        public event FinishEventHandler Finish;

		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;		
		private YellowstonePathology.Business.Domain.Lock m_Lock;
		private YellowstonePathology.Business.Specimen.Model.SpecimenOrder m_SpecimenOrder;
        private YellowstonePathology.UI.Navigation.PageNavigator m_PageNavigator;

		public SpecimenOrderDetailsPath(YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			string containerId,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator)
		{
			this.m_AccessionOrder = accessionOrder;
            this.m_PageNavigator = pageNavigator;
            this.m_SpecimenOrder = accessionOrder.SpecimenOrderCollection.GetSpecimenOrderByContainerId(containerId);
		}

        public SpecimenOrderDetailsPath(YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator)
        {
            this.m_SpecimenOrder = specimenOrder;
            this.m_AccessionOrder = accessionOrder;
            this.m_PageNavigator = pageNavigator;
        }
        
		public void Start()
		{
			if (Business.User.SystemIdentity.DoesLoggedInUserNeedToScanId() == true)
            {
                this.ShowScanSecurityBadgePage();
            }
            else
            {
				this.m_SystemIdentity = new Business.User.SystemIdentity(Business.User.SystemIdentityTypeEnum.CurrentlyLoggedIn);				
                this.ShowCaseLockPage();
            }			
		}

		public void Start(YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            this.m_SystemIdentity = systemIdentity;
			this.m_SystemIdentity = new Business.User.SystemIdentity(Business.User.SystemIdentityTypeEnum.CurrentlyLoggedIn);
            this.ShowCaseLockPage();            
        }

		private void ShowScanSecurityBadgePage()
		{
            YellowstonePathology.UI.Login.ScanSecurityBadgePage scanSecurityBadgePage = new ScanSecurityBadgePage(System.Windows.Visibility.Collapsed);
			this.m_PageNavigator.Navigate(scanSecurityBadgePage);
			scanSecurityBadgePage.AuthentificationSuccessful += new ScanSecurityBadgePage.AuthentificationSuccessfulEventHandler(ScanSecurityBadgePage_AuthentificationSuccessful);
		}

		private void ScanSecurityBadgePage_AuthentificationSuccessful(object sender, CustomEventArgs.SystemIdentityReturnEventArgs e)
		{
			this.m_SystemIdentity = e.SystemIdentity;			
			this.ShowCaseLockPage();
		}

		private void ShowCaseLockPage()
		{
			this.m_Lock = new Business.Domain.Lock(this.m_SystemIdentity);
			CaseLockPage caseLockPage = new CaseLockPage(this.m_PageNavigator, this.m_Lock, this.m_AccessionOrder);
			caseLockPage.Return += new CaseLockPage.ReturnEventHandler(CaseLockPage_Return);
			caseLockPage.AttemptCaseLock();
		}

		private void CaseLockPage_Return(object sender, UI.Navigation.PageNavigationReturnEventArgs e)
		{
			CaseLockPage caseLockPage = (CaseLockPage)sender;
			if (caseLockPage.Lock.LockAquired == true)
			{
				this.ShowAccessionedSpecimenPage();
			}
			else
			{
                if (this.Finish != null) this.Finish(this, new EventArgs());
			}
		}

		private void ShowAccessionedSpecimenPage()
		{
            SpecimenOrderDetailsPage specimenOrderDetailsPage = new SpecimenOrderDetailsPage(this.m_AccessionOrder, this.m_SpecimenOrder);
            specimenOrderDetailsPage.Next += new SpecimenOrderDetailsPage.NextEventHandler(SpecimenOrderDetailsPage_Finish);
            specimenOrderDetailsPage.Back += new SpecimenOrderDetailsPage.BackEventHandler(SpecimenOrderDetailsPage_Back);
            this.m_PageNavigator.Navigate(specimenOrderDetailsPage);
		}

        private void SpecimenOrderDetailsPage_Back(object sender, EventArgs e)
        {
            if (this.Finish != null) this.Finish(this, new EventArgs());
        }

        private void SpecimenOrderDetailsPage_Finish(object sender, EventArgs e)
        {
            if (this.Finish != null) this.Finish(this, new EventArgs());
        }		
	}
}
