﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
	public class ReviewForAdditionalTestingResultPath : ResultPath
	{
		private ReviewForAdditionalTestingResultPage m_ResultPage;
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;
		private YellowstonePathology.Business.Test.ReviewForAdditionalTesting.ReviewForAdditionalTestingTestOrder m_TestOrder;

		public ReviewForAdditionalTestingResultPath(string reportNo, YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.Persistence.ObjectTracker objectTracker,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator)
            : base(pageNavigator)
        {
            this.m_AccessionOrder = accessionOrder;
			this.m_TestOrder = (YellowstonePathology.Business.Test.ReviewForAdditionalTesting.ReviewForAdditionalTestingTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
			this.m_ObjectTracker = objectTracker;
		}

        protected override void ShowResultPage()
		{
			this.m_ResultPage = new ReviewForAdditionalTestingResultPage(this.m_TestOrder, this.m_AccessionOrder, this.m_ObjectTracker, this.m_SystemIdentity);
			this.m_ResultPage.Next += new ReviewForAdditionalTestingResultPage.NextEventHandler(ResultPage_Next);
            this.m_PageNavigator.Navigate(this.m_ResultPage);
		}

        private void ResultPage_Next(object sender, EventArgs e)
        {
            this.Finished();
		}
	}
}
