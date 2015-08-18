using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
	public class LynchSyndromeEvaluationWordDocument : YellowstonePathology.Business.Document.CaseReportV2
    {
		public override void Render(string masterAccessionNo, string reportNo, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveEnum)
        {
            int molecularTestCount = 0;
            this.m_ReportNo = reportNo;
            this.m_ReportSaveEnum = reportSaveEnum;
			this.m_AccessionOrder = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetAccessionOrderByMasterAccessionNo(masterAccessionNo);
			this.m_PanelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);

            this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\LynchSyndromeEvaluation.5.xml";
            this.OpenTemplate();
            this.SetDemographicsV2();
            this.SetReportDistribution();

			YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeEvaluationTestOrder panelSetOrderLynchSyndromeEvaluation = (YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeEvaluationTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_ReportNo);            
            base.ReplaceText("report_interpretation", panelSetOrderLynchSyndromeEvaluation.Interpretation);
            base.ReplaceText("report_comment", panelSetOrderLynchSyndromeEvaluation.Comment);                

            YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeIHCPanelTestOrder panelSetOrderLynchSyndromeIHC = (YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeIHCPanelTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(102);
            if (panelSetOrderLynchSyndromeIHC != null)
            {                
                base.ReplaceText("mlh1_result", panelSetOrderLynchSyndromeIHC.MLH1Result);
                base.ReplaceText("msh2_result", panelSetOrderLynchSyndromeIHC.MSH2Result);
                base.ReplaceText("msh6_result", panelSetOrderLynchSyndromeIHC.MSH6Result);
                base.ReplaceText("pms2_result", panelSetOrderLynchSyndromeIHC.PMS2Result);
            }

            if (panelSetOrderLynchSyndromeEvaluation.BRAFIsIndicated == true)
            {
                molecularTestCount += 1;
                if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(18) == true) //BRAF
                {
                    YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTest brafV600EKTest = new YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTest();
                    YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTestOrder panelSetOrderBraf = (YellowstonePathology.Business.Test.BRAFV600EK.BRAFV600EKTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(brafV600EKTest.PanelSetId);

					base.ReplaceText("braf_result", panelSetOrderBraf.Result);
                    base.ReplaceText("molecular_analysis_header", "Molecular Analysis");
                }
            }
            else
            {
                this.DeleteRow("braf_result");                
            }

			YellowstonePathology.Business.Test.LynchSyndrome.MLH1MethylationAnalysisTest panelSetMLH1 = new YellowstonePathology.Business.Test.LynchSyndrome.MLH1MethylationAnalysisTest();
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetMLH1.PanelSetId) == true)
            {
                molecularTestCount += 1;
				YellowstonePathology.Business.Test.LynchSyndrome.MLH1MethylationAnalysisTestOrder panelSetOrderMLH1MethylationAnalysis = (YellowstonePathology.Business.Test.LynchSyndrome.MLH1MethylationAnalysisTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetMLH1.PanelSetId);
                base.ReplaceText("mlh1methylation_result", panelSetOrderMLH1MethylationAnalysis.Result);
                base.ReplaceText("molecular_analysis_header", "Molecular Analysis");
            }
            else
            {
                this.DeleteRow("mlh1methylation_result");                
            }

            if (molecularTestCount == 0)
            {
                this.DeleteRow("molecular_analysis_header");
            }

            base.ReplaceText("report_references", panelSetOrderLynchSyndromeEvaluation.References);            
			base.ReplaceText("report_method", panelSetOrderLynchSyndromeEvaluation.Method);
            base.ReplaceText("pathologist_signature", panelSetOrderLynchSyndromeEvaluation.Signature);

            this.SaveReport();
        }
        
        public override void Publish()
        {
            base.Publish();
        }        
    }
}
