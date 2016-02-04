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
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator)
            : base(pageNavigator)
        {
            this.m_CancelATestEventArgs = cancelATestEventArgs;
		}

        protected override void ShowResultPage()
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

            YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Persistence.ObjectGatway.Instance.GetByMasterAccessionNo(e.AccessionOrder.MasterAccessionNo, true);
            YellowstonePathology.Business.Test.TestCancelled.TestCancelledTestOrder testCancelledTestOrder = (YellowstonePathology.Business.Test.TestCancelled.TestCancelledTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(e.PanelSetOrder.ReportNo);
            YellowstonePathology.Business.Persistence.ObjectTrackerV2.Instance.RegisterObject(accessionOrder, this);
            testCancelledTestOrder.Distribute = false;
            testCancelledTestOrder.NoCharge = true;
            testCancelledTestOrder.Comment = e.ReasonForCancelation;

            this.ShowTestCancelledResultPage(testCancelledTestOrder, accessionOrder);
        }

        private void CancelTestWarningPage_Back(object sender, YellowstonePathology.UI.CustomEventArgs.CancelTestEventArgs e)
        {
            this.Back(this, e);
        }

        private void ShowTestCancelledResultPage(YellowstonePathology.Business.Test.TestCancelled.TestCancelledTestOrder testCancelledTestOrder, 
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{            
            this.m_ResultPage = new TestCancelledResultPage(testCancelledTestOrder, accessionOrder, this.m_SystemIdentity);
            this.m_ResultPage.Next += new TestCancelledResultPage.NextEventHandler(ResultPage_Next);
            this.m_PageNavigator.Navigate(this.m_ResultPage);
		}

        private void ResultPage_Next(object sender, EventArgs e)
        {
             
            this.Finished();
        }        
	}
}
