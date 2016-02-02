using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Login.FinalizeAccession
{
	public class PatientLinkingPath
	{
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        private LoginPageWindow m_LoginPageWindow;
        private YellowstonePathology.Business.Domain.Lock m_Lock;
		private string m_ReportNo;

		public PatientLinkingPath(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo)
        {
            this.m_AccessionOrder = accessionOrder;
			this.m_ReportNo = reportNo;
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
                YellowstonePathology.Business.Persistence.ObjectTrackerV2.Instance.RegisterObject(this.m_AccessionOrder, this);
                YellowstonePathology.Business.Patient.Model.PatientLinker patientLinker = new Business.Patient.Model.PatientLinker(this.m_AccessionOrder.MasterAccessionNo,
				this.m_ReportNo,
				this.m_AccessionOrder.PFirstName,
                this.m_AccessionOrder.PLastName, this.m_AccessionOrder.PMiddleInitial, this.m_AccessionOrder.PSSN,
				this.m_AccessionOrder.PatientId, this.m_AccessionOrder.PBirthdate);
				FinalizeAccession.PatientLinkingPage patientLinkingPage = new FinalizeAccession.PatientLinkingPage(this.m_AccessionOrder, true, Business.Patient.Model.PatientLinkingListModeEnum.AccessionOrder, patientLinker);
				patientLinkingPage.Return += new FinalizeAccession.PatientLinkingPage.ReturnEventHandler(PatientLinkingPage_Return);
				this.m_LoginPageWindow.PageNavigator.Navigate(patientLinkingPage);
            }
            else
            {
                this.m_LoginPageWindow.Close();
            }
        }

		private void PatientLinkingPage_Return(object sender, UI.Navigation.PageNavigationReturnEventArgs e)
        {
            YellowstonePathology.Business.Persistence.ObjectTrackerV2.Instance.SubmitChanges(this.m_AccessionOrder, this);
            YellowstonePathology.Business.Persistence.ObjectTrackerV2.Instance.CleanUp(this);
            this.m_Lock.ReleaseUserLocks();
            this.m_LoginPageWindow.Close();
        }       
    }
}
