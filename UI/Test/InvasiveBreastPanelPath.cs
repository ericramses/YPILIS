using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
	public class InvasiveBreastPanelPath : ResultPath
	{
		//public delegate void FinishEventHandler(object sender, EventArgs e);
		//public event FinishEventHandler Finish;

		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;
		//private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		//private YellowstonePathology.UI.Navigation.PageNavigator m_PageNavigator;
		private string m_ReportNo;

		private InvasiveBreastPanelPage m_InvasiveBreastPanelPage;
		private YellowstonePathology.Business.Test.InvasiveBreastPanel.InvasiveBreastPanel m_InvasiveBreastPanel;

		public InvasiveBreastPanelPath(string reportNo, YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.Persistence.ObjectTracker objectTracker,
			YellowstonePathology.UI.Navigation.PageNavigator pageNavigator) : base(pageNavigator)
		{
			this.m_ReportNo = reportNo;
			this.m_AccessionOrder = accessionOrder;
			this.m_ObjectTracker = objectTracker;
			//this.m_SystemIdentity = systemIdentity;
			//this.m_PageNavigator = pageNavigator;

			this.m_InvasiveBreastPanel = (YellowstonePathology.Business.Test.InvasiveBreastPanel.InvasiveBreastPanel)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);
            Authenticated += ResultPath_Authenticated;
        }

        private void ResultPath_Authenticated(object sender, EventArgs e)
        {
            this.m_InvasiveBreastPanelPage = new InvasiveBreastPanelPage(this.m_ReportNo, this.m_AccessionOrder, this.m_ObjectTracker, this.m_SystemIdentity);
            this.m_InvasiveBreastPanelPage.Next += new InvasiveBreastPanelPage.NextEventHandler(InvasiveBreastPanelPage_Next);
            this.m_InvasiveBreastPanelPage.OrderHER2byFISH += new InvasiveBreastPanelPage.OrderHER2byFISHEventHandler(InvasiveBreastPanelPage_OrderHER2byFISH);
            this.ShowResultPage();
        }

        public void ShowResultPage()
		{
			this.m_PageNavigator.Navigate(this.m_InvasiveBreastPanelPage);
		}

		private void InvasiveBreastPanelPage_Next(object sender, EventArgs e)
		{
            //if (this.Finish != null) this.Finish(this, new EventArgs());
            this.Finished();
		}

		private void InvasiveBreastPanelPage_OrderHER2byFISH(object sender, EventArgs e)
		{
			YellowstonePathology.Business.Test.Her2AmplificationByFish.Her2AmplificationByFishTest panelSet = new Business.Test.Her2AmplificationByFish.Her2AmplificationByFishTest();
			this.StartReportOrderPath(panelSet);
		}

		private void StartReportOrderPath(YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet)
		{
			YellowstonePathology.Business.Interface.IOrderTarget orderTarget = this.m_AccessionOrder.SpecimenOrderCollection.GetOrderTarget(this.m_InvasiveBreastPanel.OrderedOnId);
            YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo = new YellowstonePathology.Business.Test.TestOrderInfo(panelSet, orderTarget, true);

			YellowstonePathology.UI.Login.Receiving.ReportOrderPath reportOrderPath = new Login.Receiving.ReportOrderPath(this.m_AccessionOrder, this.m_ObjectTracker, this.m_SystemIdentity, this.m_PageNavigator, PageNavigationModeEnum.Inline);
			reportOrderPath.Finish += new Login.Receiving.ReportOrderPath.FinishEventHandler(ReportOrderPath_Finish);
            reportOrderPath.Start(testOrderInfo);
		}

		private void ReportOrderPath_Finish(object sender, EventArgs e)
		{
			this.m_PageNavigator.Navigate(this.m_InvasiveBreastPanelPage);
		}
	}
}
