using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
	public class FLT3ResultPath : ResultPath
	{
		FLT3ResultPage m_FLT3ResultPage;
		YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		YellowstonePathology.Business.Test.FLT3.PanelSetOrderFLT3 m_PanelSetOrderFLT3;
		private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;

		public FLT3ResultPath(string reportNo,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.Persistence.ObjectTracker objectTracker,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator)
            : base(pageNavigator)
        {
            this.m_AccessionOrder = accessionOrder;
			this.m_PanelSetOrderFLT3 = (YellowstonePathology.Business.Test.FLT3.PanelSetOrderFLT3)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
			this.m_ObjectTracker = objectTracker;
			this.Authenticated += new AuthenticatedEventHandler(ResultPath_Authenticated);
		}

		private void ResultPath_Authenticated(object sender, EventArgs e)
		{
			this.ShowResultPage();
		}

        private void ShowResultPage()
        {
			this.m_FLT3ResultPage = new FLT3ResultPage(this.m_PanelSetOrderFLT3, this.m_AccessionOrder, this.m_ObjectTracker, this.m_SystemIdentity);
			this.m_FLT3ResultPage.Next += new FLT3ResultPage.NextEventHandler(FLT3ResultPage_Next);
			this.m_PageNavigator.Navigate(this.m_FLT3ResultPage);
        }

		private void FLT3ResultPage_Next(object sender, EventArgs e)
        {
            this.Finished();
        }
	}
}
