using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.AdditionalTestingNotification
{
	public class AdditionalTestingNotificationWordDocument : YellowstonePathology.Business.Document.CaseReportV2
	{
        public AdditionalTestingNotificationWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
		{                        
			base.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\AdditionalTestingNotification.1.xml";
			base.OpenTemplate();
			this.SetDemographicsV2();
			this.SetXmlNodeData("additional_testing", this.m_PanelSetOrder.PanelSetName);			            
			this.SaveReport(true);            
		}				
	}
}
