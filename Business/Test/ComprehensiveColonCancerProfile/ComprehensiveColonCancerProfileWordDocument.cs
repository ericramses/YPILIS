using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.ComprehensiveColonCancerProfile
{
	public class ComprehensiveColonCancerProfileWordDocument : YellowstonePathology.Business.Document.CaseReportV2
	{
		public override void Render(string masterAccessionNo, string reportNo, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveEnum)
		{
			this.m_ReportNo = reportNo;
			this.m_ReportSaveEnum = reportSaveEnum;
			this.m_AccessionOrder = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetAccessionOrderByMasterAccessionNo(masterAccessionNo);
			this.m_PanelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);

			this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\ComprehensiveColonCancerProfile.xml";
			this.OpenTemplate();
			this.SetDemographicsV2();
			this.SetReportDistribution();

			ComprehensiveColonCancerProfile comprehensiveColonCancerProfile = (ComprehensiveColonCancerProfile)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
			ComprehensiveColonCancerProfileResult comprehensiveColonCancerProfileResult = new ComprehensiveColonCancerProfileResult(this.m_AccessionOrder, comprehensiveColonCancerProfile);

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderByOrderTarget(comprehensiveColonCancerProfile.OrderedOnId);
            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetAliquotOrder(comprehensiveColonCancerProfile.OrderedOnId);
            string specimenDescription = specimenOrder.Description;
            if (aliquotOrder != null) specimenDescription += ": " + aliquotOrder.Label;

			base.ReplaceText("report_interpretation", comprehensiveColonCancerProfile.Interpretation);
            base.ReplaceText("specimen_description", specimenDescription);
			base.ReplaceText("surgical_report_no", comprehensiveColonCancerProfileResult.PanelSetOrderSurgical.ReportNo);
			base.ReplaceText("specimen_diagnosis", comprehensiveColonCancerProfileResult.SurgicalSpecimen.Diagnosis);
			base.ReplaceText("ajcc_stage", comprehensiveColonCancerProfileResult.PanelSetOrderSurgical.AJCCStage);
			base.ReplaceText("mlh1_result", comprehensiveColonCancerProfileResult.IHCResult.MLH1Result.Description);
			base.ReplaceText("msh2_result", comprehensiveColonCancerProfileResult.IHCResult.MSH2Result.Description);
			base.ReplaceText("msh6_result", comprehensiveColonCancerProfileResult.IHCResult.MSH6Result.Description);
			base.ReplaceText("pms2_result", comprehensiveColonCancerProfileResult.IHCResult.PMS2Result.Description);
			base.ReplaceText("ihc_report_no", comprehensiveColonCancerProfileResult.PanelSetOrderLynchSyndromeIHC.ReportNo);

			if (comprehensiveColonCancerProfileResult.PanelSetOrderMLH1MethylationAnalysis != null)
			{
				base.ReplaceText("mlh1_report_result", comprehensiveColonCancerProfileResult.PanelSetOrderMLH1MethylationAnalysis.Result);
				base.ReplaceText("mlh1_report_no", comprehensiveColonCancerProfileResult.PanelSetOrderMLH1MethylationAnalysis.ReportNo);
			}
			else
			{
				this.DeleteRow("mlh1_report_result");
			}

            if (comprehensiveColonCancerProfileResult.KRASStandardIsOrderd == true)
            {
                base.ReplaceText("kras_result", comprehensiveColonCancerProfileResult.KRASStandardTestOrder.Result);
                base.ReplaceText("kras_report_no", comprehensiveColonCancerProfileResult.KRASStandardTestOrder.ReportNo);
            }
            else
            {
                this.DeleteRow("kras_result");
            }

            if (comprehensiveColonCancerProfileResult.BRAFV600EKIsOrdered == true)
            {
                base.ReplaceText("braf_result", comprehensiveColonCancerProfileResult.BRAFV600EKTestOrder.Result);
                base.ReplaceText("braf_report_no", comprehensiveColonCancerProfileResult.BRAFV600EKTestOrder.ReportNo);
            }
            else
            {
                this.DeleteRow("braf_result");
            }			

			base.ReplaceText("pathologist_signature", comprehensiveColonCancerProfile.Signature);

			this.SaveReport();
		}

		public override void Publish()
		{
			base.Publish();
		}
	}
}
