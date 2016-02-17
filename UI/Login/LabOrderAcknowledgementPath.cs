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
            this.m_LoginPageWindow = new LoginPageWindow();
			this.m_LoginPageWindow.Width = 850;
        }

        public void Start()
        {
            YellowstonePathology.UI.Login.LabOrderAcknowledgementPage labOrderAcknowledgementPage = new LabOrderAcknowledgementPage(this.m_SystemIdentity);
            labOrderAcknowledgementPage.Return += new LabOrderAcknowledgementPage.ReturnEventHandler(LabOrderAcknowledgementPage_Return);
            this.m_LoginPageWindow.PageNavigator.Navigate(labOrderAcknowledgementPage);
            this.m_LoginPageWindow.ShowDialog();
        }        

        private void LabOrderAcknowledgementPage_Return(object sender, UI.Navigation.PageNavigationReturnEventArgs e)
        {
			this.m_LoginPageWindow.Close();
        }
	}
}
