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
        YellowstonePathology.Business.Test.HER2AmplificationSummary.HER2AmplificationSummaryTestOrder m_PanelSetOrder;

        public HER2AmplificationSummaryResultPath(string reportNo,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
            System.Windows.Window window)
            : base(pageNavigator, window)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_PanelSetOrder = (YellowstonePathology.Business.Test.HER2AmplificationSummary.HER2AmplificationSummaryTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
        }

        protected override void ShowResultPage()
        {
            this.m_ResultPage = new HER2AmplificationSummaryResultPage(this.m_PanelSetOrder, this.m_AccessionOrder, this.m_SystemIdentity, this.m_PageNavigator);
            this.m_ResultPage.Next += new HER2AmplificationSummaryResultPage.NextEventHandler(ResultPage_Next);
            this.m_ResultPage.OrderIHC += ResultPage_OrderIHC;
            this.m_ResultPage.OrderDISH += ResultPage_OrderDISH;

            this.m_PageNavigator.Navigate(this.m_ResultPage);
        }

        private void ResultPage_Next(object sender, EventArgs e)
        {
            this.Finished();
        }

        private void ResultPage_OrderIHC(object sender, EventArgs e)
        {
            YellowstonePathology.Business.Test.Her2AmplificationByIHC.Her2AmplificationByIHCTest test = new Business.Test.Her2AmplificationByIHC.Her2AmplificationByIHCTest();
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget = this.m_AccessionOrder.SpecimenOrderCollection.GetOrderTarget(this.m_PanelSetOrder.OrderedOnId);
            YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo = new YellowstonePathology.Business.Test.TestOrderInfo(test, orderTarget, false);
            YellowstonePathology.Business.Visitor.OrderTestOrderVisitor orderVisitor = new Business.Visitor.OrderTestOrderVisitor(testOrderInfo);
            this.m_AccessionOrder.TakeATrip(orderVisitor);
            orderVisitor.PanelSetOrder.Distribute = false;

            YellowstonePathology.Business.Task.Model.TaskOrder taskOrder = this.m_AccessionOrder.CreateTask(testOrderInfo);
            this.m_AccessionOrder.TaskOrderCollection.Add(taskOrder);
        }

        private void ResultPage_OrderDISH(object sender, EventArgs e)
        {
            YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTest test = new Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTest();
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget = this.m_AccessionOrder.SpecimenOrderCollection.GetOrderTarget(this.m_PanelSetOrder.OrderedOnId);
            YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo = new YellowstonePathology.Business.Test.TestOrderInfo(test, orderTarget, false);
            YellowstonePathology.Business.Visitor.OrderTestOrderVisitor orderVisitor = new Business.Visitor.OrderTestOrderVisitor(testOrderInfo);
            this.m_AccessionOrder.TakeATrip(orderVisitor);
            orderVisitor.PanelSetOrder.Distribute = false;

            YellowstonePathology.Business.Task.Model.TaskOrder taskOrder = this.m_AccessionOrder.CreateTask(testOrderInfo);
            this.m_AccessionOrder.TaskOrderCollection.Add(taskOrder);
        }

        private void SpecimenOrderDetailsPath_Finish(object sender, EventArgs e)
        {
            this.ShowResultPage();
        }
    }
}
