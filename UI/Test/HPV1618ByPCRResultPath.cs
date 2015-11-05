using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
	public class HPV1618ByPCRResultPath : ResultPath
	{
		HPV1618ByPCRResultPage m_HPV1618ByPCRResultPage;
		YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		YellowstonePathology.Business.Test.HPV1618ByPCR.HPV1618ByPCRTestOrder m_HPV1618ByPCRTestOrder;
		private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;

		public HPV1618ByPCRResultPath(string reportNo,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.Persistence.ObjectTracker objectTracker,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator)
            : base(pageNavigator)
        {
            this.m_AccessionOrder = accessionOrder;
			this.m_HPV1618ByPCRTestOrder = (YellowstonePathology.Business.Test.HPV1618ByPCR.HPV1618ByPCRTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
			this.m_ObjectTracker = objectTracker;
		}

        protected override void ShowResultPage()
        {
			this.m_HPV1618ByPCRResultPage = new HPV1618ByPCRResultPage(this.m_HPV1618ByPCRTestOrder, this.m_AccessionOrder, this.m_ObjectTracker, this.m_SystemIdentity, this.m_PageNavigator);
			this.m_HPV1618ByPCRResultPage.Next += new HPV1618ByPCRResultPage.NextEventHandler(HPV1618ByPCRResultPage_Next);
			this.m_PageNavigator.Navigate(this.m_HPV1618ByPCRResultPage);
        }

		private void HPV1618ByPCRResultPage_Next(object sender, EventArgs e)
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
                womensHealthProfilePage.Back += new WomensHealthProfilePage.BackEventHandler(WomensHealthProfilePage_Back);
                womensHealthProfilePage.Finished += new WomensHealthProfilePage.FinishedEventHandler(WomensHealthProfilePage_Finished);
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
