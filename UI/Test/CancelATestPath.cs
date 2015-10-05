using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
    public class CancelATestPath : ResultPath
    {
        public delegate void BackEventHandler(object sender, YellowstonePathology.UI.CustomEventArgs.CancelTestEventArgs e);
        public event BackEventHandler Back;

        private TestCancelledResultPage m_ResultPage;
        private YellowstonePathology.UI.CustomEventArgs.CancelTestEventArgs m_CancelATestEventArgs;

        public CancelATestPath(YellowstonePathology.UI.CustomEventArgs.CancelTestEventArgs cancelATestEventArgs, 
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
            YellowstonePathology.Business.User.SystemIdentity systemIdentity)
            : base(pageNavigator, systemIdentity)
        {
            this.m_CancelATestEventArgs = cancelATestEventArgs;
			this.Authenticated += new AuthenticatedEventHandler(ResultPath_Authenticated);
		}

		private void ResultPath_Authenticated(object sender, EventArgs e)
		{
            CancelTestWarningPage cancelTestWarningPage = new CancelTestWarningPage(this.m_CancelATestEventArgs);
            cancelTestWarningPage.Back += new CancelTestWarningPage.BackEventHandler(CancelTestWarningPage_Back);
            cancelTestWarningPage.CancelTest += new CancelTestWarningPage.CancelTestEventHandler(CancelTestWarningPage_CancelTest);
            this.m_PageNavigator.Navigate(cancelTestWarningPage);
		}

        private void CancelTestWarningPage_CancelTest(object sender, CustomEventArgs.CancelTestEventArgs e)
        {
            YellowstonePathology.Business.Gateway.AccessionOrderGateway.SetPanelSetOrderAsCancelledTest(e.PanelSetOrder.ReportNo);
            YellowstonePathology.Business.Gateway.AccessionOrderGateway.InsertTestCancelledTestOrder(e.PanelSetOrder.ReportNo, e.PanelSetOrder.PanelSetId, e.PanelSetOrder.PanelSetName);

            YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetAccessionOrderByMasterAccessionNo(e.AccessionOrder.MasterAccessionNo);
            YellowstonePathology.Business.Test.TestCancelled.TestCancelledTestOrder testCancelledTestOrder = (YellowstonePathology.Business.Test.TestCancelled.TestCancelledTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(e.PanelSetOrder.ReportNo);
			YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();
            objectTracker.RegisterObject(accessionOrder);
            testCancelledTestOrder.Distribute = false;
            testCancelledTestOrder.NoCharge = true;
            testCancelledTestOrder.Comment = e.ReasonForCancelation;

            this.ShowResultPage(testCancelledTestOrder, accessionOrder, objectTracker);
        }

        private void CancelTestWarningPage_Back(object sender, YellowstonePathology.UI.CustomEventArgs.CancelTestEventArgs e)
        {
            this.Back(this, e);
        }

        private void ShowResultPage(YellowstonePathology.Business.Test.TestCancelled.TestCancelledTestOrder testCancelledTestOrder, 
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Persistence.ObjectTracker objectTracker)
		{            
            this.m_ResultPage = new TestCancelledResultPage(testCancelledTestOrder, accessionOrder, objectTracker, this.m_SystemIdentity);
            this.m_ResultPage.Next += new TestCancelledResultPage.NextEventHandler(ResultPage_Next);
            this.m_PageNavigator.Navigate(this.m_ResultPage);
		}

        private void ResultPage_Next(object sender, EventArgs e)
        {
            this.Finished();
        }        
	}
}
