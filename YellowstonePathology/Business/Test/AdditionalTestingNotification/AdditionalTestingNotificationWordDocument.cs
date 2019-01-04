using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Document;

namespace YellowstonePathology.Business.Test.AdditionalTestingNotification
{
	public class AdditionalTestingNotificationWordDocument : YellowstonePathology.Business.Document.CaseReportV2
	{
        private string m_SendToName;

        public AdditionalTestingNotificationWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode, string sendToName) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {
            this.m_SendToName = sendToName;
        }

        public override void Render()
		{            
            base.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\AdditionalTestingNotification.3.xml";
			base.OpenTemplate();
			this.SetDemographicsV2();
			this.SetXmlNodeData("additional_testing", this.m_PanelSetOrder.PanelSetName);

            if (string.IsNullOrEmpty(this.m_SendToName) == false)
            {
                this.ReplaceText("send_to", this.m_SendToName);
            }
            else
            {
                this.DeleteRow("send_to");
            }

            YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(this.m_PanelSetOrder.ReportNo);
            this.m_SaveFileName = Business.Document.CaseDocument.GetCaseFileNameXMLNotify(orderIdParser);
            this.m_ReportXml.Save(this.m_SaveFileName);
        }

        public override void Publish()
        {            
            YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(this.m_PanelSetOrder.ReportNo);            
            YellowstonePathology.Business.Helper.FileConversionHelper.ConvertDocumentTo(orderIdParser, CaseDocumentTypeEnum.AdditionalTestingNotification, CaseDocumentFileTypeEnnum.xml, CaseDocumentFileTypeEnnum.doc);
            YellowstonePathology.Business.Helper.FileConversionHelper.ConvertDocumentTo(orderIdParser, CaseDocumentTypeEnum.AdditionalTestingNotification, CaseDocumentFileTypeEnnum.doc, CaseDocumentFileTypeEnnum.xps);
            YellowstonePathology.Business.Helper.FileConversionHelper.ConvertDocumentTo(orderIdParser, CaseDocumentTypeEnum.AdditionalTestingNotification, CaseDocumentFileTypeEnnum.xps, CaseDocumentFileTypeEnnum.tif);
        }
    }
}
