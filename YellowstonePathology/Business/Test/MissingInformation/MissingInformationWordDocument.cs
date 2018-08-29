using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Document;

namespace YellowstonePathology.Business.Test.MissingInformation
{
	public class MissingInformationWordDocument : YellowstonePathology.Business.Document.CaseReportV2
	{
        private Business.Test.MissingInformation.MissingInformationTestOrder m_MissingInformationTestOrder;

        public MissingInformationWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.MissingInformation.MissingInformationTestOrder missingInformationTestOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, missingInformationTestOrder, reportSaveMode)
        {
            this.m_MissingInformationTestOrder = missingInformationTestOrder;
        }

        public override void Render()
		{                        
			base.m_TemplateName = @"\\CFileServer\documents\ReportTemplates\XmlTemplates\MissingInformation.1.xml";
            base.OpenTemplate();
			this.SetDemographicsV2();
<<<<<<< HEAD
                        
            this.SetXMLNodeParagraphData("letter_body", this.m_MissingInformationTestOrder.LetterBody);
=======

            Business.Client.Model.Client client = Business.Gateway.PhysicianClientGateway.GetClientByClientId(this.m_AccessionOrder.ClientId);
            this.ReplaceText("client_name", this.m_AccessionOrder.ClientName);
            this.ReplaceText("fax_number", YellowstonePathology.Business.Helper.PhoneNumberHelper.FormatWithDashes(client.Fax));
            this.ReplaceText("current_date", DateTime.Today.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
            this.ReplaceText("patient_name", this.m_AccessionOrder.PatientDisplayName);
            this.SetXMLNodeParagraphData("letter_body", this.m_LetterBody);
>>>>>>> c22cb87ed5028b6646dc7928c77f9c8283398b73

            YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(this.m_PanelSetOrder.ReportNo);
            this.m_SaveFileName = Business.Document.CaseDocument.GetCaseFileNameXml(orderIdParser);
            this.m_ReportXml.Save(this.m_SaveFileName);            
		}        

        public override void Publish()
        {
            YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(this.m_PanelSetOrder.ReportNo);
            YellowstonePathology.Business.Helper.FileConversionHelper.ConvertDocumentTo(orderIdParser, CaseDocumentTypeEnum.CaseReport, CaseDocumentFileTypeEnnum.xml, CaseDocumentFileTypeEnnum.doc);
            YellowstonePathology.Business.Helper.FileConversionHelper.ConvertDocumentTo(orderIdParser, CaseDocumentTypeEnum.CaseReport, CaseDocumentFileTypeEnnum.doc, CaseDocumentFileTypeEnnum.xps);
            YellowstonePathology.Business.Helper.FileConversionHelper.ConvertDocumentTo(orderIdParser, CaseDocumentTypeEnum.CaseReport, CaseDocumentFileTypeEnnum.xps, CaseDocumentFileTypeEnnum.tif);
        }
    }
}
