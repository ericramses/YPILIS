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
		private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;

		public JAK2Exon1214ResultPath(string reportNo,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.Persistence.ObjectTracker objectTracker,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator)
            : base(pageNavigator)
        {
            this.m_AccessionOrder = accessionOrder;
			this.m_PanelSetOrder = (YellowstonePathology.Business.Test.JAK2Exon1214.JAK2Exon1214TestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
			this.m_ObjectTracker = objectTracker;
		}

        protected override void ShowResultPage()
        {
			this.m_ResultPage = new JAK2Exon1214ResultPage(this.m_PanelSetOrder, this.m_AccessionOrder, this.m_ObjectTracker, this.m_SystemIdentity);
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
			YellowstonePathology.Business.Test.MPNExtendedReflex.MPNExtendedReflexTest panelSetMPNExtendedReflex = new YellowstonePathology.Business.Test.MPNExtendedReflex.MPNExtendedReflexTest();
			if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetMPNStandardReflex.PanelSetId) == true)
			{
				result = true;
				YellowstonePathology.Business.Test.MPNStandardReflex.PanelSetOrderMPNStandardReflex panelSetOrderMPNStandardReflex = (YellowstonePathology.Business.Test.MPNStandardReflex.PanelSetOrderMPNStandardReflex)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetMPNStandardReflex.PanelSetId);
				Test.MPNStandardReflexPath MPNStandardReflexPath = new Test.MPNStandardReflexPath(panelSetOrderMPNStandardReflex.ReportNo, this.m_AccessionOrder, this.m_ObjectTracker, this.m_PageNavigator);
				MPNStandardReflexPath.Finish += new Test.MPNStandardReflexPath.FinishEventHandler(MPNStandardReflexPath_Finish);
				MPNStandardReflexPath.Back += new MPNStandardReflexPath.BackEventHandler(MPNStandardReflexPath_Back);
				MPNStandardReflexPath.Start(this.m_SystemIdentity);
			}
			else if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetMPNExtendedReflex.PanelSetId) == true)
            {
                result = true;
				YellowstonePathology.Business.Test.MPNExtendedReflex.PanelSetOrderMPNExtendedReflex panelSetOrderMPNExtendedReflex = (YellowstonePathology.Business.Test.MPNExtendedReflex.PanelSetOrderMPNExtendedReflex)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetMPNExtendedReflex.PanelSetId);
				Test.MPNExtendedReflexPath MPNExtendedReflexPath = new Test.MPNExtendedReflexPath(panelSetOrderMPNExtendedReflex.ReportNo, this.m_AccessionOrder, this.m_ObjectTracker, this.m_PageNavigator);
				MPNExtendedReflexPath.Finish += new Test.MPNExtendedReflexPath.FinishEventHandler(MPNExtendedReflexPath_Finish);
				MPNExtendedReflexPath.Back += new MPNExtendedReflexPath.BackEventHandler(MPNExtendedReflexPath_Back);
				MPNExtendedReflexPath.Start(this.m_SystemIdentity);
			}
            return result;
        }

		private void MPNStandardReflexPath_Back(object sender, EventArgs e)
		{
			this.ShowResultPage();
		}

		private void MPNStandardReflexPath_Finish(object sender, EventArgs e)
		{
			base.Finished();
		}

		private void MPNExtendedReflexPath_Back(object sender, EventArgs e)
        {
            this.ShowResultPage();
        }

		private void MPNExtendedReflexPath_Finish(object sender, EventArgs e)
        {
            base.Finished();
		}

		private void ReportOrderPath_Finish(object sender, EventArgs e)
		{
			this.ShowReflexTestPage();
		}
	}
}
