using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.UI.Test
{
    public class HematopathologySummaryResultPath : ResultPath
    {
        private HematopathologySummaryResultPage m_HematopathologySummaryResultPage;
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Test.HematopathologySummary.HematopathologySummaryTestOrder m_TestOrder;

        public HematopathologySummaryResultPath(string reportNo, YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
            System.Windows.Window window)
            : base(pageNavigator, window)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_TestOrder = (YellowstonePathology.Business.Test.HematopathologySummary.HematopathologySummaryTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
        }

        protected override void ShowResultPage()
        {
            this.m_HematopathologySummaryResultPage = new HematopathologySummaryResultPage(this.m_TestOrder, this.m_AccessionOrder, this.m_SystemIdentity, this.m_PageNavigator);
            this.m_HematopathologySummaryResultPage.Next += new HematopathologySummaryResultPage.NextEventHandler(HematopathologySummaryResultPage_Next);
            this.m_PageNavigator.Navigate(this.m_HematopathologySummaryResultPage);
        }

        private void HematopathologySummaryResultPage_Next(object sender, EventArgs e)
        {
            this.Finished();
        }
    }
}
