using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
	public class HPV1618ResultPath : ResultPath
	{
		HPV1618ResultPage m_HPV1618ResultPage;
		YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		YellowstonePathology.Business.Test.HPV1618.PanelSetOrderHPV1618 m_PanelSetOrderHPV1618;
		private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;

		public HPV1618ResultPath(string reportNo,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.Persistence.ObjectTracker objectTracker,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator)
            : base(pageNavigator)
        {
            this.m_AccessionOrder = accessionOrder;
			this.m_PanelSetOrderHPV1618 = (YellowstonePathology.Business.Test.HPV1618.PanelSetOrderHPV1618)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
			this.m_ObjectTracker = objectTracker;
			this.Authenticated += new AuthenticatedEventHandler(ResultPath_Authenticated);
		}

		private void ResultPath_Authenticated(object sender, EventArgs e)
		{
			this.ShowResultPage();
		}

        private void ShowResultPage()
        {
			this.m_HPV1618ResultPage = new HPV1618ResultPage(this.m_PanelSetOrderHPV1618, this.m_AccessionOrder, this.m_ObjectTracker, this.m_SystemIdentity, this.m_PageNavigator);
			this.m_HPV1618ResultPage.Next += new HPV1618ResultPage.NextEventHandler(HPV1618ResultPage_Next);
			this.m_PageNavigator.Navigate(this.m_HPV1618ResultPage);
        }

		private void HPV1618ResultPage_Next(object sender, EventArgs e)
        {
			if (this.ShowWomensHealthProfilePage() == false)
			{
				this.Finished();
			}
		}

		private bool ShowWomensHealthProfilePage()
		{
			bool result = false;
			if (this.m_AccessionOrder.PanelSetOrderCollection.HasWomensHealthProfileOrder() == true)
			{
				YellowstonePathology.Business.Domain.Physician physician = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianByPhysicianId(this.m_AccessionOrder.PhysicianId);

				YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder = YellowstonePathology.Business.Gateway.ClientOrderGateway.GetClientOrderByClientOrderId(this.m_AccessionOrder.ClientOrderId);
				WomensHealthProfilePage womensHealthProfilePage = new WomensHealthProfilePage(this.m_AccessionOrder, this.m_ObjectTracker, clientOrder, this.m_SystemIdentity, System.Windows.Visibility.Visible);
				womensHealthProfilePage.Finished += new WomensHealthProfilePage.FinishedEventHandler(WomensHealthProfilePage_Finished);
                womensHealthProfilePage.Back += new WomensHealthProfilePage.BackEventHandler(WomensHealthProfilePage_Back);
                this.m_PageNavigator.Navigate(womensHealthProfilePage);
				result = true;
			}
			return result;
		}

		private void WomensHealthProfilePage_Finished(object sender, EventArgs e)
		{
			this.Finished();
		}

        private void WomensHealthProfilePage_Back(object sender, EventArgs e)
        {
            this.ShowResultPage();
        }
    }
}
