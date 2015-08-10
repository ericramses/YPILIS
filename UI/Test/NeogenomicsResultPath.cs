using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
    public class NeogenomicsResultPath
    {
        private ResultDialog m_PrimaryWindow;
        private ResultDialog m_SecondaryWindow;     
   
        NeogenomicsResultPage m_NeogenomicsResultPage;
        NeogenomicsResultTextPage m_NeogenomicsResultTextPage;

        public NeogenomicsResultPath()
        {
            
            
        }

        public void Start()
        {
            this.m_PrimaryWindow = new ResultDialog();            
            if(this.m_PrimaryWindow.PageNavigator.HasDualMonitors() == true)
            {                
                this.m_SecondaryWindow = new ResultDialog();
                this.m_SecondaryWindow.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

                this.m_NeogenomicsResultTextPage = new NeogenomicsResultTextPage();
                this.m_SecondaryWindow.PageNavigator.Navigate(this.m_NeogenomicsResultTextPage);
                this.m_PrimaryWindow.PageNavigator.ShowSecondMonitorWindow(this.m_SecondaryWindow);
            }
            this.ShowResultPage();
            this.m_PrimaryWindow.Show();
        }

        private void ShowResultPage()
        {
            this.m_NeogenomicsResultPage = new NeogenomicsResultPage();            
            this.m_NeogenomicsResultPage.ShowResult += new NeogenomicsResultPage.ShowResultEventHandler(NeogenomicsResultPage_ShowResult);
            this.m_NeogenomicsResultPage.Next += new NeogenomicsResultPage.NextEventHandler(NeogenomicsResultPage_Next);
            this.m_PrimaryWindow.PageNavigator.Navigate(this.m_NeogenomicsResultPage);            
        }

        private void NeogenomicsResultPage_Next(object sender, CustomEventArgs.NeogenomicsResultReturnEventArgs e)
        {
            NeogenomicsResultAccessionPage neogenomicsResultAccessionPage = new NeogenomicsResultAccessionPage(e.NeogenomicsResult);
            neogenomicsResultAccessionPage.Next += new NeogenomicsResultAccessionPage.NextEventHandler(NeogenomicsResultAccessionPage_Next);
            neogenomicsResultAccessionPage.Back += new NeogenomicsResultAccessionPage.BackEventHandler(NeogenomicsResultAccessionPage_Back);
            this.m_PrimaryWindow.PageNavigator.Navigate(neogenomicsResultAccessionPage);
        }

        private void NeogenomicsResultAccessionPage_Back(object sender, EventArgs e)
        {
            this.m_NeogenomicsResultTextPage.Clear();
            this.ShowResultPage();
        }

        private void NeogenomicsResultAccessionPage_Next(object sender, CustomEventArgs.ReportNoReturnEventArgs e)
        {
            /*
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetAccessionOrderByReportNo(e.ReportNo);

            YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new Persistence.ObjectTracker();
            objectTracker.RegisterObject(accessionOrder);

            YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(e.ReportNo);
            ResultPathFactory resultPathFactory = new ResultPathFactory();

            YellowstonePathology.Business.User.SystemIdentity systemIdentity = new Business.User.SystemIdentity(Business.User.SystemIdentityTypeEnum.CurrentlyLoggedIn);
            resultPathFactory.Start(panelSetOrder, accessionOrder, objectTracker, this.m_PrimaryWindow, this.m_PrimaryWindow.PageNavigator, systemIdentity, System.Windows.Visibility.Visible);
            */
        }

        private void NeogenomicsResultPage_ShowResult(object sender, CustomEventArgs.NeogenomicsResultReturnEventArgs e)
        {
            this.m_NeogenomicsResultTextPage.ShowResultText(e.NeogenomicsResult);
        }
    }
}
