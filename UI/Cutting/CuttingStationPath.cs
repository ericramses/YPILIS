using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Cutting
{
    public class CuttingStationPath
    {
        private CuttingWorkspaceWindow m_CuttingWorkspaceWindow;
        private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;        
        private CuttingPage m_CuttingPage;        

        public CuttingStationPath()
        {
            
        }

        public void Start()
        {            
            this.m_CuttingWorkspaceWindow = new CuttingWorkspaceWindow();
            this.ShowScanSecurityBadgePage();
            this.m_CuttingWorkspaceWindow.ShowDialog();
        }        

        private void ShowScanSecurityBadgePage()
        {
            this.m_CuttingWorkspaceWindow.MenuFile.Visibility = System.Windows.Visibility.Visible;
            YellowstonePathology.UI.Login.ScanSecurityBadgePage scanSecurityBadgePage = new Login.ScanSecurityBadgePage(System.Windows.Visibility.Visible);
            this.m_CuttingWorkspaceWindow.PageNavigator.Navigate(scanSecurityBadgePage);
            scanSecurityBadgePage.AuthentificationSuccessful += new Login.ScanSecurityBadgePage.AuthentificationSuccessfulEventHandler(ScanSecurityBadgePage_AuthentificationSuccessful);
            scanSecurityBadgePage.Close += new Login.ScanSecurityBadgePage.CloseEventHandler(ScanSecurityBadgePage_Close);
        }

        private void ScanSecurityBadgePage_Close(object sender, EventArgs e)
        {
            this.m_CuttingWorkspaceWindow.Close();
        }

        private void ScanSecurityBadgePage_AuthentificationSuccessful(object sender, CustomEventArgs.SystemIdentityReturnEventArgs e)
        {
            this.m_CuttingWorkspaceWindow.MenuFile.Visibility = System.Windows.Visibility.Collapsed;
            this.m_SystemIdentity = e.SystemIdentity;
            this.ShowScanAliquotPage(null);
        }

        private void ShowScanAliquotPage(string lastMasterAccessionNo)
        {            
            ScanAliquotPage scanAliquotPage = new ScanAliquotPage(this.m_AccessionOrder, lastMasterAccessionNo, this.m_SystemIdentity);
            scanAliquotPage.AliquotOrderSelected += new ScanAliquotPage.AliquotOrderSelectedEventHandler(ScanAliquotPage_AliquotOrderSelected);
            scanAliquotPage.SignOut += new ScanAliquotPage.SignOutEventHandler(ScanAliquotPage_SignOut);
            scanAliquotPage.ShowMasterAccessionNoEntryPage += new ScanAliquotPage.ShowMasterAccessionNoEntryPageEventHandler(ScanAliquotPage_ShowMasterAccessionNoEntryPage);
            scanAliquotPage.UseLastMasterAccessionNo += new ScanAliquotPage.UseLastMasterAccessionNoEventHandler(ScanAliquotPage_UseLastMasterAccessionNo);
            scanAliquotPage.PageTimedOut += new ScanAliquotPage.PageTimedOutEventHandler(PageTimedOut);
            this.m_CuttingWorkspaceWindow.PageNavigator.Navigate(scanAliquotPage);
        }
        
        private void ScanAliquotPage_UseLastMasterAccessionNo(object sender, CustomEventArgs.MasterAccessionNoReturnEventArgs eventArgs)
        {
            this.HandleMasterAccessionNoFound(eventArgs.MasterAccessionNo);
        }

        private void ScanAliquotPage_ShowMasterAccessionNoEntryPage(object sender, EventArgs eventArgs)
        {
            YellowstonePathology.UI.Cutting.MasterAccessionNoEntryPage masterAccessionNoEntryPage = new MasterAccessionNoEntryPage();
            masterAccessionNoEntryPage.UseThisMasterAccessionNo += new MasterAccessionNoEntryPage.UseThisMasterAccessionNoEventHandler(MasterAccessionNoEntryPage_UseThisMasterAccessionNo);
            masterAccessionNoEntryPage.Close += new MasterAccessionNoEntryPage.CloseEventHandler(MasterAccessionNoEntryPage_Close);
            this.m_CuttingWorkspaceWindow.PageNavigator.Navigate(masterAccessionNoEntryPage);
        }

        private void MasterAccessionNoEntryPage_Close(object sender, EventArgs eventArgs)
        {
            this.ShowScanAliquotPage(null);
        }

        private void MasterAccessionNoEntryPage_UseThisMasterAccessionNo(object sender, CustomEventArgs.MasterAccessionNoReturnEventArgs eventArgs)
        {
            this.HandleMasterAccessionNoFound(eventArgs.MasterAccessionNo);
        }

        private void HandleMasterAccessionNoFound(string masterAccessionNo)
        {
            this.m_AccessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(masterAccessionNo, m_CuttingWorkspaceWindow);			

            YellowstonePathology.Business.Test.AliquotOrderCollection aliquotOrderCollection = this.m_AccessionOrder.SpecimenOrderCollection.GetAliquotOrdersThatHaveTestOrders();
            AliquotOrderSelectionPage aliquotOrderSelectionPage = new AliquotOrderSelectionPage(aliquotOrderCollection, this.m_AccessionOrder);
            aliquotOrderSelectionPage.AliquotOrderSelected += new AliquotOrderSelectionPage.AliquotOrderSelectedEventHandler(AliquotOrderSelectionPage_AliquotOrderSelected);
            aliquotOrderSelectionPage.Back += new AliquotOrderSelectionPage.BackEventHandler(AliquotOrderSelectionPage_Back);
            this.m_CuttingWorkspaceWindow.PageNavigator.Navigate(aliquotOrderSelectionPage);
        }

        private void AliquotOrderSelectionPage_Back(object sender, YellowstonePathology.UI.CustomEventArgs.MasterAccessionNoReturnEventArgs eventArgs)
        {            
            this.ShowScanAliquotPage(eventArgs.MasterAccessionNo);
        }

        private void AliquotOrderSelectionPage_AliquotOrderSelected(object sender, CustomEventArgs.AliquotOrderReturnEventArgs eventArgs)
        {
            this.HandleAliquotOrderFound(eventArgs.AliquotOrder);
        }

        private void ScanAliquotPage_AliquotOrderSelected(object sender, CustomEventArgs.AliquotOrderAccessionOrderReturnEventArgs eventArgs)
        {
            this.m_AccessionOrder = eventArgs.AccessionOrder;			
            this.HandleAliquotOrderFound(eventArgs.AliquotOrder);            
        }

        private void HandleAliquotOrderFound(YellowstonePathology.Business.Test.AliquotOrder aliquotOrder)
        {
            if (aliquotOrder.TestOrderCollection.OnlyHasOneHAndE() == true)
            {
                YellowstonePathology.Business.Test.Model.TestOrder_Base testOrderBase = aliquotOrder.TestOrderCollection.GetTestOrderBase(49);
                YellowstonePathology.Business.Test.Model.TestOrder testOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetTestOrderByTestOrderId(testOrderBase.TestOrderId);
                this.ShowCuttingPage(aliquotOrder, testOrder);
            }
            else
            {
                this.ShowTestOrderSelectionPage(aliquotOrder);
            }
        }

        private void ShowTestOrderSelectionPage(YellowstonePathology.Business.Test.AliquotOrder aliquotOrder)
        {
            TestOrderPanelSetOrderViewCollection testOrderPanelSetOrderViewCollection = new TestOrderPanelSetOrderViewCollection(aliquotOrder.TestOrderCollection, this.m_AccessionOrder);
            TestOrderSelectionPage testOrderSelectionPage = new TestOrderSelectionPage(testOrderPanelSetOrderViewCollection, aliquotOrder, this.m_AccessionOrder);
            testOrderSelectionPage.TestOrderSelected += new TestOrderSelectionPage.TestOrderSelectedEventHandler(TestOrderSelectionPage_TestOrderSelected);
            testOrderSelectionPage.Back += new TestOrderSelectionPage.BackEventHandler(TestOrderSelectionPage_Back);
            this.m_CuttingWorkspaceWindow.PageNavigator.Navigate(testOrderSelectionPage);
        }

        private void TestOrderSelectionPage_Back(object sender, YellowstonePathology.UI.CustomEventArgs.MasterAccessionNoReturnEventArgs eventArgs)
        {
            this.ShowScanAliquotPage(eventArgs.MasterAccessionNo);
        }

        private void TestOrderSelectionPage_TestOrderSelected(object sender, CustomEventArgs.TestOrderAliquotOrderReturnEventArgs eventArgs)
        {
            this.ShowCuttingPage(eventArgs.AliquotOrder, eventArgs.TestOrder);
        }

        private void ScanAliquotPage_SignOut(object sender, EventArgs e)
        {
            this.ShowScanSecurityBadgePage();
        }        

        private void ShowCuttingPage(YellowstonePathology.Business.Test.AliquotOrder aliquotOrder, YellowstonePathology.Business.Test.Model.TestOrder testOrder)
        {            
            this.m_CuttingPage = new CuttingPage(aliquotOrder, testOrder, this.m_AccessionOrder, this.m_SystemIdentity, this.m_CuttingWorkspaceWindow.PageNavigator);
            this.m_CuttingPage.Finished += new CuttingPage.FinishedEventHandler(CuttingPage_Finished);            
            this.m_CuttingPage.ShowTestOrderSelectionPage += new CuttingPage.ShowTestOrderSelectionPageEventHandler(CuttingPage_ShowTestOrderSelectionPage);
            this.m_CuttingPage.PageTimedOut += new CuttingPage.PageTimedOutEventHandler(PageTimedOut);
            this.m_CuttingWorkspaceWindow.PageNavigator.Navigate(this.m_CuttingPage);
        }

        private void PageTimedOut(object sender, EventArgs eventArgs)
        {
            this.ShowScanSecurityBadgePage();
        }

        private void CuttingPage_ShowTestOrderSelectionPage(object sender, CustomEventArgs.AliquotOrderReturnEventArgs eventArgs)
        {
            this.ShowTestOrderSelectionPage(eventArgs.AliquotOrder);
        }                

        private void CuttingPage_Finished(object sender, YellowstonePathology.UI.CustomEventArgs.MasterAccessionNoReturnEventArgs eventArgs)
        {
            this.ShowScanAliquotPage(eventArgs.MasterAccessionNo);
            this.m_CuttingPage = null;
        }       
    }
}
