using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.UI.Test
{
    public class HER2AmplificationSummaryResultPath : ResultPath
    {
        HER2AmplificationSummaryResultPage m_ResultPage;
        YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;

        public HER2AmplificationSummaryResultPath(string reportNo,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
            System.Windows.Window window)
            : base(pageNavigator, window)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_PanelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
        }

        protected override void ShowResultPage()
        {
            this.m_ResultPage = new HER2AmplificationSummaryResultPage(this.m_PanelSetOrder, this.m_AccessionOrder, this.m_SystemIdentity);
            this.m_ResultPage.Next += new HER2AmplificationSummaryResultPage.NextEventHandler(ResultPage_Next);
            this.m_PageNavigator.Navigate(this.m_ResultPage);
        }

        private void ResultPage_Next(object sender, EventArgs e)
        {
            if (this.ShowISHResultPage() == false)
            {
                this.Finished();
            }
        }

        private bool ShowISHResultPage()
        {
            bool result = false;

            YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTest ishTest = new Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTest();
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(ishTest.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true) == true)
            {
                Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTestOrder ishTestOrder = (Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(ishTest.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true);
                result = true;
                HER2AmplificationByISHResultPath ishPath = new Test.HER2AmplificationByISHResultPath(ishTestOrder.ReportNo, this.m_AccessionOrder, this.m_PageNavigator, this.m_Window);
                ishPath.Finish += HER2AmplificationByISHResultPath_Finish;
                ishPath.Start();
            }
                return result;
        }

        private void HER2AmplificationByISHResultPath_Finish(object sender, EventArgs e)
        {
            this.Finished();
        }
    }
}
