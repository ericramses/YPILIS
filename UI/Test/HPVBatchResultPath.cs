using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
	public class HPVBatchResultPath : ResultPath
	{
		private YellowstonePathology.Business.Search.ReportSearchList m_ReportSearchList;
        private int m_CurrentIndex;
        private Microsoft.Office.Interop.Excel.Application m_ExcelApplication;
        private Microsoft.Office.Interop.Excel.Workbook m_WorkBook;

		public HPVBatchResultPath(YellowstonePathology.Business.Search.ReportSearchList reportSearchList, 
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
            YellowstonePathology.Business.User.SystemIdentity systemIdentity)
            : base(pageNavigator, systemIdentity)
        {
            this.m_CurrentIndex = 0;
            this.m_ReportSearchList = reportSearchList;
			this.Authenticated += new AuthenticatedEventHandler(ResultPath_Authenticated);
		}        

		private void ResultPath_Authenticated(object sender, EventArgs e)
		{
            this.ShowTecanImportExportDialog();
		}

        private void ShowTecanImportExportDialog()
        {
            YellowstonePathology.UI.Test.TecanImportExportPage tecanImportExportPage = new TecanImportExportPage(null, System.Windows.Visibility.Visible, System.Windows.Visibility.Collapsed);
            tecanImportExportPage.Next += new TecanImportExportPage.NextEventHandler(TecanImportExportPage_Next);
            this.m_PageNavigator.Navigate(tecanImportExportPage);
        }

        private void TecanImportExportPage_Next(object sender, CustomEventArgs.ExcelSpreadsheetReturnEventArgs e)
        {
            this.m_ExcelApplication = e.ExcelApplication;
            this.m_WorkBook = e.WorkBook;           
            this.GoToNextCase();
        }

		private void GoToNextCase()
		{
			YellowstonePathology.Business.Search.ReportSearchItem reportSearchItem = this.m_ReportSearchList[this.m_CurrentIndex];
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetAccessionOrderByReportNo(reportSearchItem.ReportNo);
			YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();
			objectTracker.RegisterObject(accessionOrder);

			YellowstonePathology.Business.Test.HPV.HPVTestOrder panelSetOrder = (YellowstonePathology.Business.Test.HPV.HPVTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportSearchItem.ReportNo);			
            YellowstonePathology.UI.Test.HPVResultPath hpvResultPath = new HPVResultPath(panelSetOrder.ReportNo, accessionOrder, objectTracker, this.m_PageNavigator, this.m_SystemIdentity);
            hpvResultPath.Finish += new FinishEventHandler(HPVResultPath_Finish);
            hpvResultPath.Start();
		}

        private void HPVResultPath_Finish(object sender, EventArgs e)
        {
            this.m_CurrentIndex += 1;
            if (this.m_CurrentIndex < this.m_ReportSearchList.Count) this.GoToNextCase();
            else this.Finished();
        }		
	}
}
