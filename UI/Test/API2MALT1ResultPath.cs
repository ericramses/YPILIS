using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
	public class API2MALT1ResultPath : ResultPath
	{
		API2MALT1ResultPage m_ResultPage;
		YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		YellowstonePathology.Business.Test.API2MALT1.API2MALT1TestOrder m_PanelSetOrder;
		private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;

		public API2MALT1ResultPath(string reportNo,
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.Persistence.ObjectTracker objectTracker,
			YellowstonePathology.UI.Navigation.PageNavigator pageNavigator)
            : base(pageNavigator)
        {
			this.m_AccessionOrder = accessionOrder;
			this.m_PanelSetOrder = (YellowstonePathology.Business.Test.API2MALT1.API2MALT1TestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
			this.m_ObjectTracker = objectTracker;
		}

        protected override void ShowResultPage()
		{
			this.m_ResultPage = new API2MALT1ResultPage((YellowstonePathology.Business.Test.API2MALT1.API2MALT1TestOrder)this.m_PanelSetOrder, this.m_AccessionOrder, this.m_ObjectTracker, this.m_SystemIdentity);
			this.m_ResultPage.Next += new API2MALT1ResultPage.NextEventHandler(ResultsPage_Next);
			this.m_PageNavigator.Navigate(this.m_ResultPage);
		}

        private void ResultsPage_Next(object sender, EventArgs e)
		{
			this.Finished();
		}
	}
}
