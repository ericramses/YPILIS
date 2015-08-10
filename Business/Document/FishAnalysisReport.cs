using System;
using System.Collections.Generic;
using System.Text;

namespace YellowstonePathology.Business.Document
{
	public class FishAnalysisReport : CaseReportV2
    {
		YellowstonePathology.Business.Test.FishAnalysis.FishAnalysisTestOrder m_ReportOrder;

		public override void Render(string masterAccessionNo, string reportNo, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveEnum)
		{
            this.m_ReportNo = reportNo;
			this.m_ReportSaveEnum = reportSaveEnum;            
        }

        public override void Publish()
        {            
            YellowstonePathology.Business.Helper.FileConversionHelper.SaveXpsReportToTiff(this.m_ReportNo);
        }
    }
}
