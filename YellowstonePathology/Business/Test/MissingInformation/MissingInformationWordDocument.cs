using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Document;

namespace YellowstonePathology.Business.Test.MissingInformation
{
	public class MissingInformationWordDocument : YellowstonePathology.Business.Document.CaseReportV2
	{
        private string m_LetterBody;

        public MissingInformationWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode, string letterBody) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {
            this.m_LetterBody = letterBody;
        }

        public override void Render()
		{                        
			base.m_TemplateName = @"\\CFileServer\documents\ReportTemplates\XmlTemplates\ClientMissingInfoFax.xml";
            base.OpenTemplate();
			this.SetDemographicsV2();

            Business.Client.Model.Client client = Business.Gateway.PhysicianClientGateway.GetClientByClientId(this.m_AccessionOrder.ClientId);
            this.ReplaceText("client_name", this.m_AccessionOrder.ClientName);
            this.ReplaceText("fax_number", YellowstonePathology.Business.Domain.PhoneNumber.Format(client.Fax));
            this.ReplaceText("current_date", DateTime.Today.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
            this.ReplaceText("patient_name", this.m_AccessionOrder.PatientDisplayName);
            this.SetXMLNodeParagraphData("letter_body", this.m_LetterBody);

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
