using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.TechnicalOnly
{
    public class TechnicalOnlyWordDocument : YellowstonePathology.Business.Interface.ICaseDocument
    {
        private YellowstonePathology.Business.Document.NativeDocumentFormatEnum m_NativeDocumentFormat;

		public void Render(string masterAccessionNo, string reportNo, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveEnum)
        {
            //Do Nothing
        }

        public void Publish()
        {
            //Do Nothing
        }

		public YellowstonePathology.Business.Rules.MethodResult DeleteCaseFiles(YellowstonePathology.Business.OrderIdParser orderIdParser)
		{
            YellowstonePathology.Business.Rules.MethodResult methodResult = new Rules.MethodResult();
            methodResult.Success = true;
            return methodResult;
        }

        public YellowstonePathology.Business.Document.NativeDocumentFormatEnum NativeDocumentFormat
        {
            get { return this.m_NativeDocumentFormat; }
            set { this.m_NativeDocumentFormat = value; }
        }
    }
}
