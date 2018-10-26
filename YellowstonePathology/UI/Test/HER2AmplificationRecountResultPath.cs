using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.UI.Test
{
    public class HER2AmplificationRecountResultPath : ResultPath
    {
        HER2AmplificationRecountResultPage m_ResultPage;
        YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        YellowstonePathology.Business.Test.HER2AmplificationRecount.HER2AmplificationRecountTestOrder m_PanelSetOrder;

        public HER2AmplificationRecountResultPath(string reportNo,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
            System.Windows.Window window)
            : base(pageNavigator, window)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_PanelSetOrder = (YellowstonePathology.Business.Test.HER2AmplificationRecount.HER2AmplificationRecountTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
        }

        protected override void ShowResultPage()
        {
            this.m_ResultPage = new HER2AmplificationRecountResultPage(this.m_PanelSetOrder, this.m_AccessionOrder, this.m_SystemIdentity, this.m_PageNavigator);
            this.m_ResultPage.Next += new HER2AmplificationRecountResultPage.NextEventHandler(ResultPage_Next);

            this.m_PageNavigator.Navigate(this.m_ResultPage);
        }

        private void ResultPage_Next(object sender, EventArgs e)
        {
            if (this.ShowReflexTestPage() == false)
            {
                this.Finished();
            }
        }

        private bool ShowReflexTestPage()
        {
            bool result = false;
            YellowstonePathology.Business.Test.HER2AmplificationSummary.HER2AmplificationSummaryTest her2AmplificationSummaryTest = new Business.Test.HER2AmplificationSummary.HER2AmplificationSummaryTest();
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(her2AmplificationSummaryTest.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true) == true)
            {
                YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(her2AmplificationSummaryTest.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true);
                result = true;
                YellowstonePathology.UI.Test.HER2AmplificationSummaryResultPath resultPath = new Test.HER2AmplificationSummaryResultPath(panelSetOrder.ReportNo, this.m_AccessionOrder, this.m_PageNavigator, this.m_Window);
                resultPath.Finish += new Test.HER2AmplificationSummaryResultPath.FinishEventHandler(ResultPath_Finish);
                resultPath.Start();
            }
            return result;
        }

        private void ResultPath_Finish(object sender, EventArgs e)
        {
            base.Finished();
        }
    }
}
