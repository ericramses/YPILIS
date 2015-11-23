﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
	class MPNExtendedReflexPath: ResultPath
	{
		public delegate void BackEventHandler(object sender, EventArgs e);
		public event BackEventHandler Back;

		MPNExtendedReflexPage m_MPNExtendedReflexPage;
        YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		YellowstonePathology.Business.Test.MPNExtendedReflex.PanelSetOrderMPNExtendedReflex m_PanelSetOrderMPNExtendedReflex;
		private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;

		public MPNExtendedReflexPath(string reportNo,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.Persistence.ObjectTracker objectTracker,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator)
            : base(pageNavigator)
        {
            this.m_AccessionOrder = accessionOrder;
			this.m_PanelSetOrderMPNExtendedReflex = (YellowstonePathology.Business.Test.MPNExtendedReflex.PanelSetOrderMPNExtendedReflex)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
			this.m_ObjectTracker = objectTracker;
		}

        protected override void ShowResultPage()
        {
			this.m_MPNExtendedReflexPage = new MPNExtendedReflexPage(this.m_AccessionOrder, this.m_ObjectTracker, this.m_SystemIdentity);
			this.m_MPNExtendedReflexPage.Back += new MPNExtendedReflexPage.BackEventHandler(MPNExtendedReflexPage_Back);
			this.m_MPNExtendedReflexPage.Finish += new MPNExtendedReflexPage.FinishEventHandler(MPNExtendedReflexPage_Finish);
            this.m_MPNExtendedReflexPage.OrderTest += new MPNExtendedReflexPage.OrderTestEventHandler(MPNExtendedReflexPage_OrderTest);			
			this.m_PageNavigator.Navigate(this.m_MPNExtendedReflexPage);
        }

        private void MPNExtendedReflexPage_OrderTest(object sender, YellowstonePathology.UI.CustomEventArgs.TestOrderInfoEventArgs e)
        {                        
            YellowstonePathology.UI.Login.Receiving.ReportOrderPath reportOrderPath = new Login.Receiving.ReportOrderPath(this.m_AccessionOrder, this.m_ObjectTracker, this.m_SystemIdentity, this.m_PageNavigator, PageNavigationModeEnum.Inline);
            reportOrderPath.Finish += new Login.Receiving.ReportOrderPath.FinishEventHandler(ReportOrderPath_Finish);
            reportOrderPath.Start(e.TestOrderInfo);
        }

		private void MPNExtendedReflexPage_Back(object sender, EventArgs e)
		{
			if (this.Back != null) this.Back(this, new EventArgs());
			else this.Finished();
		}

		private void MPNExtendedReflexPage_Finish(object sender, EventArgs e)
        {
            this.Finished();
        }				

		private void ReportOrderPath_Finish(object sender, EventArgs e)
		{
			this.m_PageNavigator.Navigate(this.m_MPNExtendedReflexPage);
		}
	}
}
