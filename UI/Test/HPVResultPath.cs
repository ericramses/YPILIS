using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
	public class HPVResultPath : ResultPath
	{
		private HPVResultPage m_HPVResultPage;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;        
		private YellowstonePathology.Business.Test.HPV.HPVTestOrder m_HPVTestOrder;

        public HPVResultPath(string reportNo, YellowstonePathology.Business.Test.AccessionOrder accessionOrder,            
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator)
            : base(pageNavigator)
        {
            this.m_AccessionOrder = accessionOrder;            
            this.m_HPVTestOrder = (YellowstonePathology.Business.Test.HPV.HPVTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
		}

        protected override void ShowResultPage()
		{
			this.m_HPVResultPage = new HPVResultPage(this.m_HPVTestOrder, this.m_AccessionOrder, this.m_SystemIdentity, this.m_PageNavigator);
			this.m_HPVResultPage.Next += new HPVResultPage.NextEventHandler(HPVResultPage_Next);
			this.m_PageNavigator.Navigate(this.m_HPVResultPage);
		}

		private void HPVResultPage_Next(object sender, EventArgs e)
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
				YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology panelSetOrderCytology = (YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(15);
				
				YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder = YellowstonePathology.Business.Persistence.ObjectGateway.Instance.GetClientOrderByClientOrderId(this.m_AccessionOrder.ClientOrderId);
				WomensHealthProfilePage womensHealthProfilePage = new WomensHealthProfilePage(this.m_AccessionOrder, clientOrder, this.m_SystemIdentity, System.Windows.Visibility.Visible);
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
