using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
	public class HPVTWIResultPath : ResultPath
	{
        public delegate void NextEventHandler(object sender, EventArgs e);
        public event NextEventHandler Next;

		private HPVTWIResultPage m_HPVTWIResultPage;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private Microsoft.Office.Interop.Excel.Application m_ExcelApplication;
        private Microsoft.Office.Interop.Excel.Workbook m_WorkBook;
		private YellowstonePathology.Business.Test.HPVTWI.PanelSetOrderHPVTWI m_PanelSetOrderHPVTWI;
		private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;

        public HPVTWIResultPath(string reportNo, YellowstonePathology.Business.Test.AccessionOrder accessionOrder,            
            YellowstonePathology.Business.Persistence.ObjectTracker objectTracker,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator)
            : base(pageNavigator)
        {
            this.m_AccessionOrder = accessionOrder;            
            this.m_PanelSetOrderHPVTWI = (YellowstonePathology.Business.Test.HPVTWI.PanelSetOrderHPVTWI)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
            this.m_ObjectTracker = objectTracker;
            this.Authenticated += new AuthenticatedEventHandler(ResultPath_Authenticated);
        }        

        public HPVTWIResultPath(string reportNo, YellowstonePathology.Business.Test.AccessionOrder accessionOrder, 
            Microsoft.Office.Interop.Excel.Application excelApplication,
            Microsoft.Office.Interop.Excel.Workbook workBook,
            YellowstonePathology.Business.Persistence.ObjectTracker objectTracker, 
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator) 
            : base(pageNavigator)
		{
			this.m_AccessionOrder = accessionOrder;
            this.m_ExcelApplication = excelApplication;
            this.m_WorkBook = workBook;
			this.m_PanelSetOrderHPVTWI = (YellowstonePathology.Business.Test.HPVTWI.PanelSetOrderHPVTWI)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
			this.m_ObjectTracker = objectTracker;
			this.Authenticated += new AuthenticatedEventHandler(ResultPath_Authenticated);
		}        

		private void ResultPath_Authenticated(object sender, EventArgs e)
		{
			this.ShowResultPage();
		}

		private void ShowResultPage()
		{
			this.m_HPVTWIResultPage = new HPVTWIResultPage(this.m_PanelSetOrderHPVTWI, this.m_AccessionOrder, this.m_ExcelApplication, 
                this.m_WorkBook, this.m_ObjectTracker, this.m_SystemIdentity, this.m_PageNavigator);
			this.m_HPVTWIResultPage.Next += new HPVTWIResultPage.NextEventHandler(HPVTWIResultPage_Next);
			this.m_PageNavigator.Navigate(this.m_HPVTWIResultPage);
		}

		private void HPVTWIResultPage_Next(object sender, EventArgs e)
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
