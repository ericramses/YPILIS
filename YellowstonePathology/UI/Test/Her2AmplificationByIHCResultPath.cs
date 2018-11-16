using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
	public class Her2AmplificationByIHCResultPath : ResultPath
	{
		Her2AmplificationByIHCResultPage m_ResultPage;
		YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		YellowstonePathology.Business.Test.Her2AmplificationByIHC.PanelSetOrderHer2AmplificationByIHC m_PanelSetOrder;

		public Her2AmplificationByIHCResultPath(string reportNo,
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
            System.Windows.Window window)
            : base(pageNavigator, window)
        {
			this.m_AccessionOrder = accessionOrder;
			this.m_PanelSetOrder = (YellowstonePathology.Business.Test.Her2AmplificationByIHC.PanelSetOrderHer2AmplificationByIHC)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
		}

        protected override void ShowResultPage()
		{
			this.m_ResultPage = new Her2AmplificationByIHCResultPage(this.m_PanelSetOrder, this.m_AccessionOrder, this.m_SystemIdentity);
			this.m_ResultPage.Next += new Her2AmplificationByIHCResultPage.NextEventHandler(ResultPage_Next);
            this.m_ResultPage.OrderHER2Summary += ResultPage_OrderHER2Summary;
			this.m_PageNavigator.Navigate(this.m_ResultPage);
		}

        private void ResultPage_OrderHER2Summary(object sender, EventArgs e)
        {
            YellowstonePathology.Business.Test.HER2AmplificationSummary.HER2AmplificationSummaryTest her2AmplificationSummaryTest = new Business.Test.HER2AmplificationSummary.HER2AmplificationSummaryTest();
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget = this.m_AccessionOrder.SpecimenOrderCollection.GetOrderTarget(this.m_PanelSetOrder.OrderedOnId);
            YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo = new YellowstonePathology.Business.Test.TestOrderInfo(her2AmplificationSummaryTest, orderTarget, false);

            YellowstonePathology.UI.Login.Receiving.ReportOrderPath reportOrderPath = new Login.Receiving.ReportOrderPath(this.m_AccessionOrder, this.m_PageNavigator, PageNavigationModeEnum.Inline, this.m_Window);
            reportOrderPath.Finish += new Login.Receiving.ReportOrderPath.FinishEventHandler(ReportOrderPath_Finish);
            reportOrderPath.Start(testOrderInfo);
        }

        private void ReportOrderPath_Finish(object sender, EventArgs e)
        {
            this.m_PageNavigator.Navigate(this.m_ResultPage);
        }

        private void ResultPage_Next(object sender, EventArgs e)
		{
            if (this.ShowHER2SummaryPage() == false)
            {
                this.Finished();
            }
		}

        private bool ShowHER2SummaryPage()
        {
            bool result = false;

            YellowstonePathology.Business.Test.HER2AmplificationSummary.HER2AmplificationSummaryTest summaryTest = new Business.Test.HER2AmplificationSummary.HER2AmplificationSummaryTest();
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(summaryTest.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true) == true)
            {
                Business.Test.PanelSetOrder ishTestOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(summaryTest.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true);
                result = true;
                HER2AmplificationSummaryResultPath summaryPath = new Test.HER2AmplificationSummaryResultPath(ishTestOrder.ReportNo, this.m_AccessionOrder, this.m_PageNavigator, this.m_Window);
                summaryPath.Finish += HER2AmplificationSummaryResultPath_Finish;
                summaryPath.Start();
            }
            return result;
        }

        private void HER2AmplificationSummaryResultPath_Finish(object sender, EventArgs e)
        {
            this.Finished();
        }
    }
}
