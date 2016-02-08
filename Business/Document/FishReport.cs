using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace YellowstonePathology.Business.Document
{
	public class FISHReport : CaseReportV2
	{
		public override void Render(string masterAccessionNo, string reportNo, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveEnum, object writer)
		{

		}

        public override void Publish()
        {
            base.Publish();
        }
	}
}
