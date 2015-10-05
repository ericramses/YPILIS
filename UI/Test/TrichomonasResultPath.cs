using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
    public class TrichomonasResultPath : ResultPath
    {
        TrichomonasResultPage m_ResultPage;
        YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;
		private YellowstonePathology.Business.Test.Trichomonas.TrichomonasTestOrder m_TestOrder;

        public TrichomonasResultPath(string reportNo, YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.Persistence.ObjectTracker objectTracker,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
            YellowstonePathology.Business.User.SystemIdentity systemIdentity)
            : base(pageNavigator, systemIdentity)
        {
            this.m_AccessionOrder = accessionOrder;
			this.m_TestOrder = (YellowstonePathology.Business.Test.Trichomonas.TrichomonasTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
			this.m_ObjectTracker = objectTracker;
			this.Authenticated += new AuthenticatedEventHandler(ResultPath_Authenticated);
		}

		private void ResultPath_Authenticated(object sender, EventArgs e)
		{
			this.ShowResultPage();
		}

		private void ShowResultPage()
		{
			this.m_ResultPage = new TrichomonasResultPage(this.m_TestOrder, this.m_AccessionOrder, this.m_ObjectTracker, this.m_SystemIdentity);
			this.m_ResultPage.Next += new TrichomonasResultPage.NextEventHandler(TrichomonasResultPage_Next);
            this.m_PageNavigator.Navigate(this.m_ResultPage);
		}

		private void TrichomonasResultPage_Next(object sender, EventArgs e)
        {
			if (this.ShowWomensHealthProfilePagePage() == false)
			{
				this.Finished();
			}
		}

		private bool ShowWomensHealthProfilePagePage()
		{
			bool result = false;
			if (this.m_AccessionOrder.PanelSetOrderCollection.HasWomensHealthProfileOrder() == true)
			{
				YellowstonePathology.Business.Domain.Physician physician = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianByPhysicianId(this.m_AccessionOrder.PhysicianId);

				YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder = YellowstonePathology.Business.Gateway.ClientOrderGateway.GetClientOrderByClientOrderId(this.m_AccessionOrder.ClientOrderId);
				WomensHealthProfilePage womensHealthProfilePage = new WomensHealthProfilePage(this.m_AccessionOrder, this.m_ObjectTracker, clientOrder, this.m_SystemIdentity);
				womensHealthProfilePage.Next += new WomensHealthProfilePage.NextEventHandler(WomensHealthProfilePage_Next);
				this.m_PageNavigator.Navigate(womensHealthProfilePage);
				result = true;
			}
			return result;
		}

		private void WomensHealthProfilePage_Next(object sender, EventArgs e)
		{
			this.Finished();
		}
	}
}
