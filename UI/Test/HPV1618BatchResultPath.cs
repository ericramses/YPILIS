using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
	public class HPV1618BatchResultPath : ResultPath
	{
		private YellowstonePathology.Business.Search.ReportSearchList m_ReportSearchList;
		private int m_CurrentIndex;

		public HPV1618BatchResultPath(YellowstonePathology.Business.Search.ReportSearchList reportSearchList, YellowstonePathology.UI.Navigation.PageNavigator pageNavigator)
			: base(pageNavigator)
		{
			this.m_CurrentIndex = 0;
			this.m_ReportSearchList = reportSearchList;

			this.Authenticated += new AuthenticatedEventHandler(ResultPath_Authenticated);
		}

		private void ResultPath_Authenticated(object sender, EventArgs e)
		{
			this.GoToNextCase();
		}

		private void GoToNextCase()
		{
			YellowstonePathology.Business.Search.ReportSearchItem reportSearchItem = this.m_ReportSearchList[this.m_CurrentIndex];
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetAccessionOrderByReportNo(reportSearchItem.ReportNo);
			YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();
			objectTracker.RegisterObject(accessionOrder);

			YellowstonePathology.Business.Test.HPV1618.HPV1618TestOrder panelSetOrder = (YellowstonePathology.Business.Test.HPV1618.HPV1618TestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportSearchItem.ReportNo);
			YellowstonePathology.UI.Test.HPV1618ResultPath resultPath = new HPV1618ResultPath(panelSetOrder.ReportNo, accessionOrder, objectTracker, this.m_PageNavigator);
			resultPath.Finish += new FinishEventHandler(ResultPath_Finish);
			resultPath.Start();
		}

		private void ResultPath_Finish(object sender, EventArgs e)
		{
			this.m_CurrentIndex += 1;
			if (this.m_CurrentIndex < this.m_ReportSearchList.Count) this.GoToNextCase();
			else this.Finished();
		}
	}
}
