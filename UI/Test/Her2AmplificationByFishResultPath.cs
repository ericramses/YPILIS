using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
	public class Her2AmplificationByFishResultPath : ResultPath
	{
		Her2AmplificationByFishResultPage m_ResultPage;
		YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		YellowstonePathology.Business.Test.PanelSetOrderHer2AmplificationByFish m_PanelSetOrder;
		private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;

		public Her2AmplificationByFishResultPath(string reportNo,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.Persistence.ObjectTracker objectTracker,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator) 
            : base(pageNavigator)
        {
            this.m_AccessionOrder = accessionOrder;
			this.m_PanelSetOrder = (YellowstonePathology.Business.Test.PanelSetOrderHer2AmplificationByFish)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
			this.m_ObjectTracker = objectTracker;
			this.Authenticated += new AuthenticatedEventHandler(ResultPath_Authenticated);
		}

		private void ResultPath_Authenticated(object sender, EventArgs e)
		{
			this.ShowResultPage();
		}

        private void ShowResultPage()
        {
			this.m_ResultPage = new Her2AmplificationByFishResultPage(this.m_PanelSetOrder, this.m_AccessionOrder, this.m_ObjectTracker, this.m_SystemIdentity);
			this.m_ResultPage.Next += new Her2AmplificationByFishResultPage.NextEventHandler(ResultPage_Next);
			this.m_PageNavigator.Navigate(this.m_ResultPage);
        }		

		private void ResultPage_Next(object sender, EventArgs e)
		{
			if (this.StartInvasiveBreastPanelPath() == false) this.Finished();
		}

		private bool StartInvasiveBreastPanelPath()
		{
			bool result = false;
			YellowstonePathology.Business.Test.InvasiveBreastPanel.InvasiveBreastPanelTest panelSet = new YellowstonePathology.Business.Test.InvasiveBreastPanel.InvasiveBreastPanelTest();
			if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSet.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true) == true)
			{
                YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSet.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true);
				result = true;
				InvasiveBreastPanelPath invasiveBreastPanelPath = new InvasiveBreastPanelPath(panelSetOrder.ReportNo, this.m_AccessionOrder, this.m_ObjectTracker, this.m_SystemIdentity, this.m_PageNavigator);
				invasiveBreastPanelPath.Finish += new InvasiveBreastPanelPath.FinishEventHandler(InvasiveBreastPanelPath_Finish);
				invasiveBreastPanelPath.Start();
			}

			return result;
		}

		private void InvasiveBreastPanelPath_Finish(object sender, EventArgs e)
		{
			this.Finished();
		}
	}
}
