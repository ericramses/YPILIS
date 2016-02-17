using System;
using System.Collections.Generic;
using System.Text;

namespace YellowstonePathology.Business.Document
{
	public class FishAnalysisReport : CaseReportV2
    {
        public FishAnalysisReport(Business.Test.AccessionOrder accessionOrder, string reportNo, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, reportNo, reportSaveMode)
        {

        }

        public override void Render()
		{
            
        }

        public override void Publish()
        {            
            YellowstonePathology.Business.Helper.FileConversionHelper.SaveXpsReportToTiff(this.m_PanelSetOrder.ReportNo);
        }
    }
}
