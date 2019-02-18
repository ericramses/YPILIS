﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
	class JAK2Exon1214ResultPath : ResultPath
    {
		JAK2Exon1214ResultPage m_ResultPage;
        YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		YellowstonePathology.Business.Test.JAK2Exon1214.JAK2Exon1214TestOrder m_PanelSetOrder;

		public JAK2Exon1214ResultPath(string reportNo,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
            System.Windows.Window window)
            : base(pageNavigator, window)
        {
            this.m_AccessionOrder = accessionOrder;
			this.m_PanelSetOrder = (YellowstonePathology.Business.Test.JAK2Exon1214.JAK2Exon1214TestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
		}

        protected override void ShowResultPage()
        {
			this.m_ResultPage = new JAK2Exon1214ResultPage(this.m_PanelSetOrder, this.m_AccessionOrder, this.m_SystemIdentity);
			this.m_ResultPage.Next += new JAK2Exon1214ResultPage.NextEventHandler(ResultPage_Next);
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
            YellowstonePathology.Business.Test.MPNStandardReflex.MPNStandardReflexTest panelSetMPNStandardReflex = new YellowstonePathology.Business.Test.MPNStandardReflex.MPNStandardReflexTest();
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetMPNStandardReflex.PanelSetId) == true)
            {
                result = true;
                YellowstonePathology.Business.Test.MPNStandardReflex.PanelSetOrderMPNStandardReflex panelSetOrderMPNStandardReflex = (YellowstonePathology.Business.Test.MPNStandardReflex.PanelSetOrderMPNStandardReflex)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetMPNStandardReflex.PanelSetId);
                Test.MPNStandardReflexPath MPNStandardReflexPath = new Test.MPNStandardReflexPath(panelSetOrderMPNStandardReflex.ReportNo, this.m_AccessionOrder, this.m_PageNavigator, this.m_Window);
                MPNStandardReflexPath.Finish += new Test.MPNStandardReflexPath.FinishEventHandler(ReflexPath_Finish);
                MPNStandardReflexPath.Back += new MPNStandardReflexPath.BackEventHandler(ReflexPath_Back);
                MPNStandardReflexPath.Start();
            }
            return result;
        }

        private void ReflexPath_Back(object sender, EventArgs e)
        {
            this.ShowResultPage();
        }

        private void ReflexPath_Finish(object sender, EventArgs e)
        {
            base.Finished();
        }
    }
}
