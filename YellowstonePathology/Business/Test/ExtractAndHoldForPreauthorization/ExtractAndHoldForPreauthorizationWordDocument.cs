using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Document;

namespace YellowstonePathology.Business.Test.ExtractAndHoldForPreauthorization
{
	public class ExtractAndHoldForPreauthorizationWordDocument : YellowstonePathology.Business.Document.CaseReportV2
	{
        public ExtractAndHoldForPreauthorizationWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
		{            
            Business.Test.ExtractAndHoldForPreauthorization.ExtractAndHoldForPreauthorizationTestOrder testOrder = (Business.Test.ExtractAndHoldForPreauthorization.ExtractAndHoldForPreauthorizationTestOrder)this.m_PanelSetOrder;
			base.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\PreauthorizationNotification.1.xml";
			base.OpenTemplate();
			this.SetDemographicsV2();
			this.SetXmlNodeData("additional_testing", testOrder.Comment);

            YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(this.m_PanelSetOrder.ReportNo);
            this.m_SaveFileName = Business.Document.CaseDocument.GetCaseFileNameXMLPreAuth(orderIdParser);
            this.m_ReportXml.Save(this.m_SaveFileName);            
		}        

        public override void Publish()
        {
            YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(this.m_PanelSetOrder.ReportNo);
            YellowstonePathology.Business.Helper.FileConversionHelper.ConvertDocumentTo(orderIdParser, CaseDocumentTypeEnum.PreauthorizationRequest, CaseDocumentFileTypeEnnum.xml, CaseDocumentFileTypeEnnum.doc);
            YellowstonePathology.Business.Helper.FileConversionHelper.ConvertDocumentTo(orderIdParser, CaseDocumentTypeEnum.PreauthorizationRequest, CaseDocumentFileTypeEnnum.doc, CaseDocumentFileTypeEnnum.xps);
            YellowstonePathology.Business.Helper.FileConversionHelper.ConvertDocumentTo(orderIdParser, CaseDocumentTypeEnum.PreauthorizationRequest, CaseDocumentFileTypeEnnum.xps, CaseDocumentFileTypeEnnum.tif);
        }
    }
}
