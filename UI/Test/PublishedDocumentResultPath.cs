﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
	public class PublishedDocumentResultPath : ResultPath
	{
		PublishedDocumentResultPage m_ResultPage;
		YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;

		public PublishedDocumentResultPath(string reportNo,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator)
            : base(pageNavigator)
        {
            this.m_AccessionOrder = accessionOrder;
			this.m_PanelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
		}

        protected override void ShowResultPage()
        {
			this.m_ResultPage = new PublishedDocumentResultPage(this.m_AccessionOrder, this.m_PanelSetOrder);
			this.m_ResultPage.Next += new PublishedDocumentResultPage.NextEventHandler(ResultPage_Next);
			this.m_PageNavigator.Navigate(this.m_ResultPage);
        }

		private void ResultPage_Next(object sender, EventArgs e)
        {
            this.Finished();
        }
	}
}
