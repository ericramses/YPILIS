using System;
using System.Collections.Generic;
using System.Text;

namespace YellowstonePathology.Business.Document
{
	public class ReferenceLabReport : CaseReportV2
    {
		public ReferenceLabReport(Business.Test.AccessionOrder accessionOrder, string reportNo, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, reportNo, reportSaveMode)
		{
			this.m_NativeDocumentFormat = NativeDocumentFormatEnum.XPS;
		}

        public YellowstonePathology.Business.Rules.MethodResult DeleteCaseFiles()
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = new Rules.MethodResult();
            methodResult.Success = true;
            return methodResult;
        }

		public override Rules.MethodResult DeleteCaseFiles(YellowstonePathology.Business.OrderIdParser orderIdParser)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = new Rules.MethodResult();
            methodResult.Success = true;
            return methodResult;
        }		

        public override void Publish()
        {
			YellowstonePathology.Business.Helper.FileConversionHelper.SaveXpsReportToTiff(this.m_PanelSetOrder.ReportNo);
        }
    }
}
