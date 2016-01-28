using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
	public class TCellClonalityByPCRResultPath : ResultPath
	{
		TCellClonalityByPCRResultPage m_TCellClonalityByPCRResultPage;
		YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		YellowstonePathology.Business.Test.TCellClonalityByPCR.PanelSetOrderTCellClonalityByPCR m_PanelSetOrderTCellClonalityByPCR;

		public TCellClonalityByPCRResultPath(string reportNo,
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.UI.Navigation.PageNavigator pageNavigator)
            : base(pageNavigator)
        {
			this.m_AccessionOrder = accessionOrder;
			this.m_PanelSetOrderTCellClonalityByPCR = (YellowstonePathology.Business.Test.TCellClonalityByPCR.PanelSetOrderTCellClonalityByPCR)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
		}

        protected override void ShowResultPage()
		{
			this.m_TCellClonalityByPCRResultPage = new TCellClonalityByPCRResultPage(this.m_PanelSetOrderTCellClonalityByPCR, this.m_AccessionOrder, this.m_SystemIdentity);
			this.m_TCellClonalityByPCRResultPage.Next += new TCellClonalityByPCRResultPage.NextEventHandler(TCellClonalityByPCRResultPage_Next);
			this.m_PageNavigator.Navigate(this.m_TCellClonalityByPCRResultPage);
		}

		private void TCellClonalityByPCRResultPage_Next(object sender, EventArgs e)
		{
			this.Finished();
		}

	}
}
