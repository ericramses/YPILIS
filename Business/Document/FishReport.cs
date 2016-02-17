using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace YellowstonePathology.Business.Document
{
	public class FISHReport : CaseReportV2
	{
        public FISHReport(Business.Test.AccessionOrder accessionOrder, string reportNo, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, reportNo, reportSaveMode)
        {

        }

        public override void Render()
		{

		}

        public override void Publish()
        {
            base.Publish();
        }
	}
}
