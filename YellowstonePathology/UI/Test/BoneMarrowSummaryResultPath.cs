using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.UI.Test
{
    public class BoneMarrowSummaryResultPath : ResultPath
    {
        private BoneMarrowSummaryResultPage m_BoneMarrowSummaryResultPage;
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Test.PanelSetOrder m_TestOrder;

        public BoneMarrowSummaryResultPath(string reportNo, YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
            System.Windows.Window window)
            : base(pageNavigator, window)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_TestOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
        }

        protected override void ShowResultPage()
        {
            this.m_BoneMarrowSummaryResultPage = new BoneMarrowSummaryResultPage(this.m_TestOrder, this.m_AccessionOrder, this.m_SystemIdentity, this.m_PageNavigator);
            this.m_BoneMarrowSummaryResultPage.Next += new BoneMarrowSummaryResultPage.NextEventHandler(BoneMarrowSummaryResultPage_Next);
            this.m_PageNavigator.Navigate(this.m_BoneMarrowSummaryResultPage);
        }

        private void BoneMarrowSummaryResultPage_Next(object sender, EventArgs e)
        {
            this.Finished();
        }
    }
}
