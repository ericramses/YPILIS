﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.UI.Test
{
    public class ReticulatedPlateletAnalysisV2ResultPath : ResultPath
    {
        ReticulatedPlateletAnalysisV2ResultPage m_ResultPage;
        YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        YellowstonePathology.Business.Test.ReticulatedPlateletAnalysisV2.ReticulatedPlateletAnalysisV2TestOrder m_PanelSetOrder;

        public ReticulatedPlateletAnalysisV2ResultPath(string reportNo,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
            System.Windows.Window window)
            : base(pageNavigator, window)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_PanelSetOrder = (YellowstonePathology.Business.Test.ReticulatedPlateletAnalysisV2.ReticulatedPlateletAnalysisV2TestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
        }

        protected override void ShowResultPage()
        {
            this.m_ResultPage = new ReticulatedPlateletAnalysisV2ResultPage(this.m_PanelSetOrder, this.m_AccessionOrder, this.m_SystemIdentity);
            this.m_ResultPage.Next += ResultPage_Next;
            this.m_PageNavigator.Navigate(this.m_ResultPage);
        }

        private void ResultPage_Next(object sender, EventArgs e)
        {
            this.Finished();
        }
    }
}
