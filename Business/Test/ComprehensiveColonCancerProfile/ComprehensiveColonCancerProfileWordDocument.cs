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
        public ComprehensiveColonCancerProfileWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode) 
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
		{			
			this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\ComprehensiveColonCancerProfile.2.xml";
			this.OpenTemplate();
			this.SetDemographicsV2();
			this.SetReportDistribution();

			ComprehensiveColonCancerProfile comprehensiveColonCancerProfile = (ComprehensiveColonCancerProfile)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_PanelSetOrder.ReportNo);
			ComprehensiveColonCancerProfileResult comprehensiveColonCancerProfileResult = new ComprehensiveColonCancerProfileResult(this.m_AccessionOrder, comprehensiveColonCancerProfile);
            YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();

            base.ReplaceText("report_interpretation", comprehensiveColonCancerProfile.Interpretation);
			base.ReplaceText("ajcc_stage", surgicalTestOrder.AJCCStage);

            YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeEvaluationTest lynchSyndromeEvaluationTest = new LynchSyndrome.LynchSyndromeEvaluationTest();
            XmlNode diagnosisTableNode = this.m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='specimen_description']", this.m_NameSpaceManager);
            XmlNode rowSpecimenNode = diagnosisTableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='specimen_description']", this.m_NameSpaceManager);
            XmlNode rowDiagnosisNode = diagnosisTableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='specimen_diagnosis']", this.m_NameSpaceManager);
            XmlNode insertAfterRow = rowSpecimenNode;
            foreach (YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in this.m_AccessionOrder.SpecimenOrderCollection)
            {
                if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(lynchSyndromeEvaluationTest.PanelSetId, specimenOrder.SpecimenOrderId, true) == true)
                {
                    this.SetSpecimenDescription(specimenOrder, specimenOrder.SpecimenOrderId, diagnosisTableNode, rowSpecimenNode, rowDiagnosisNode, insertAfterRow, surgicalTestOrder);
                }
                foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in specimenOrder.AliquotOrderCollection)
                {
                    if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(lynchSyndromeEvaluationTest.PanelSetId, aliquotOrder.AliquotOrderId, true) == true)
                    {
                        this.SetSpecimenDescription(specimenOrder, aliquotOrder.AliquotOrderId, diagnosisTableNode, rowSpecimenNode, rowDiagnosisNode, insertAfterRow, surgicalTestOrder);
                    }
                }
            }
            diagnosisTableNode.RemoveChild(rowSpecimenNode);
            diagnosisTableNode.RemoveChild(rowDiagnosisNode);

            bool ichOrdered = false;
            bool hasMolecularTest = false;

            XmlNode tableNode = this.m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='mlh1_result']", this.m_NameSpaceManager);
            XmlNode rowResultNode = tableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='mlh1promoter_result']", this.m_NameSpaceManager);

            foreach (YellowstonePathology.Business.Test.PanelSetOrder testOrder in this.m_AccessionOrder.PanelSetOrderCollection)
            {
                YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimen = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderByOrderTarget(testOrder.OrderedOnId);
                if (specimen != null)
                {
                    YellowstonePathology.Business.Test.AliquotOrder aliquot = specimen.AliquotOrderCollection.GetByAliquotOrderId(testOrder.OrderedOnId);
                    if (testOrder is YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeIHC)
                    {
                        XmlNode rowmlh1Node = tableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='mlh1_result']", this.m_NameSpaceManager);
                        XmlNode rowmsh2Node = tableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='msh2_result']", this.m_NameSpaceManager);
                        XmlNode rowmsh6Node = tableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='msh6_result']", this.m_NameSpaceManager);
                        XmlNode rowpms2Node = tableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='pms2_result']", this.m_NameSpaceManager);
                        insertAfterRow = rowmlh1Node;
                        ichOrdered = true;
                        YellowstonePathology.Business.Test.LynchSyndrome.IHCResult ihcResult = YellowstonePathology.Business.Test.LynchSyndrome.IHCResult.CreateResultFromResultCode(((LynchSyndrome.PanelSetOrderLynchSyndromeIHC)testOrder).ResultCode);
                        this.SetIHCResults(ihcResult, testOrder.ReportNo, aliquot.Label, tableNode, rowmlh1Node, rowmsh2Node, rowmsh6Node, rowpms2Node, insertAfterRow);
                    }
                    else if (testOrder is LynchSyndrome.PanelSetOrderMLH1MethylationAnalysis)
                    {
                        tableNode = this.m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='mlh1promoter_result']", this.m_NameSpaceManager); insertAfterRow = rowResultNode;
                        insertAfterRow = rowResultNode;
                        this.SetTestResults("mlh1promoter", ((LynchSyndrome.PanelSetOrderMLH1MethylationAnalysis)testOrder).Result, testOrder.ReportNo, aliquot.Label, tableNode, rowResultNode, insertAfterRow);
                        hasMolecularTest = true;
                    }
                    else if (testOrder is KRASStandard.KRASStandardTestOrder)
                    {
                        tableNode = this.m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='kras_result']", this.m_NameSpaceManager);
                        rowResultNode = tableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='kras_result']", this.m_NameSpaceManager);
                        insertAfterRow = rowResultNode;
                        this.SetTestResults("kras", ((KRASStandard.KRASStandardTestOrder)testOrder).Result, testOrder.ReportNo, aliquot.Label, tableNode, rowResultNode, insertAfterRow);
                        hasMolecularTest = true;
                    }
                    else if (testOrder is KRASExon23Mutation.KRASExon23MutationTestOrder)
                    {
                        tableNode = this.m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='kras_result']", this.m_NameSpaceManager);
                        rowResultNode = tableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='kras_result']", this.m_NameSpaceManager);
                        insertAfterRow = rowResultNode;
                        this.SetTestResults("kras", ((KRASExon23Mutation.KRASExon23MutationTestOrder)testOrder).Result, testOrder.ReportNo, aliquot.Label, tableNode, rowResultNode, insertAfterRow);
                        hasMolecularTest = true;
                    }
                    else if (testOrder is KRASExon4Mutation.KRASExon4MutationTestOrder)
                    {
                        tableNode = this.m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='kras_result']", this.m_NameSpaceManager);
                        rowResultNode = tableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='kras_result']", this.m_NameSpaceManager);
                        insertAfterRow = rowResultNode;
                        this.SetTestResults("kras", ((KRASExon4Mutation.KRASExon4MutationTestOrder)testOrder).Result, testOrder.ReportNo, aliquot.Label, tableNode, rowResultNode, insertAfterRow);
                        hasMolecularTest = true;
                    }
                    else if (testOrder is BRAFV600EK.BRAFV600EKTestOrder)
                    {
                        tableNode = this.m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='braf_result']", this.m_NameSpaceManager);
                        rowResultNode = tableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='braf_result']", this.m_NameSpaceManager);
                        insertAfterRow = rowResultNode;
                        this.SetTestResults("braf", ((BRAFV600EK.BRAFV600EKTestOrder)testOrder).Result, testOrder.ReportNo, aliquot.Label, tableNode, rowResultNode, insertAfterRow);
                        hasMolecularTest = true;
                    }
                    else if (testOrder is NRASMutationAnalysis.NRASMutationAnalysisTestOrder)
                    {
                        tableNode = this.m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='nras_result']", this.m_NameSpaceManager);
                        rowResultNode = tableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='nras_result']", this.m_NameSpaceManager);
                        insertAfterRow = rowResultNode;
                        this.SetTestResults("nras", ((NRASMutationAnalysis.NRASMutationAnalysisTestOrder)testOrder).Result, testOrder.ReportNo, aliquot.Label, tableNode, rowResultNode, insertAfterRow);
                        hasMolecularTest = true;
                    }
                    else if (testOrder is RASRAFPanel.RASRAFPanelTestOrder)
                    {
                        tableNode = this.m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='kras_result']", this.m_NameSpaceManager);
                        rowResultNode = tableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='kras_result']", this.m_NameSpaceManager);
                        insertAfterRow = rowResultNode;
                        this.SetTestResults("kras", ((RASRAFPanel.RASRAFPanelTestOrder)testOrder).KRASResult, testOrder.ReportNo, aliquot.Label, tableNode, rowResultNode, insertAfterRow);

                        tableNode = this.m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='braf_result']", this.m_NameSpaceManager);
                        rowResultNode = tableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='braf_result']", this.m_NameSpaceManager);
                        insertAfterRow = rowResultNode;
                        this.SetTestResults("braf", ((RASRAFPanel.RASRAFPanelTestOrder)testOrder).BRAFResult, testOrder.ReportNo, aliquot.Label, tableNode, rowResultNode, insertAfterRow);

                        tableNode = this.m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='nras_result']", this.m_NameSpaceManager);
                        rowResultNode = tableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='nras_result']", this.m_NameSpaceManager);
                        insertAfterRow = rowResultNode;
                        this.SetTestResults("nras", ((RASRAFPanel.RASRAFPanelTestOrder)testOrder).NRASResult, testOrder.ReportNo, aliquot.Label, tableNode, rowResultNode, insertAfterRow);

                        tableNode = this.m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='hras_result']", this.m_NameSpaceManager);
                        rowResultNode = tableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='hras_result']", this.m_NameSpaceManager);
                        insertAfterRow = rowResultNode;
                        this.SetTestResults("hras", ((RASRAFPanel.RASRAFPanelTestOrder)testOrder).HRASResult, testOrder.ReportNo, aliquot.Label, tableNode, rowResultNode, insertAfterRow);
                        hasMolecularTest = true;
                    }
                }
            }

            this.DeleteRow("mlh1promoter_result");
            this.DeleteRow("kras_result");
            this.DeleteRow("braf_result");
            this.DeleteRow("nras_result");
            this.DeleteRow("hras_result");

            if (ichOrdered == true)
            {
                this.DeleteRow("mlh1_result");
                this.DeleteRow("msh2_result");
                this.DeleteRow("msh6_result");
                this.DeleteRow("pms2_result");
            }
            else
            {
                base.ReplaceText("ihc_reportno", "Not Included");
                base.ReplaceText("mlh1_result", "Not Included");
                base.ReplaceText("msh2_result", "Not Included");
                base.ReplaceText("msh6_result", "Not Included");
                base.ReplaceText("pms2_result", "Not Included");
            }

            if(hasMolecularTest == true)
            {
                this.DeleteRow("None Performed");
            }

            base.ReplaceText("pathologist_signature", comprehensiveColonCancerProfile.Signature);

			this.SaveReport();
		}


        private void SetSpecimenDescription(YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder, string orderTargetId,
            XmlNode diagnosisTableNode, XmlNode rowSpecimenNode, XmlNode rowDiagnosisNode, XmlNode insertAfterRow,
            YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder)
        {
            YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen = surgicalTestOrder.SurgicalSpecimenCollection.GetBySpecimenOrderId(specimenOrder.SpecimenOrderId);
            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetAliquotOrder(orderTargetId);
            string specimenDescription = specimenOrder.Description;
            if (aliquotOrder != null) specimenDescription += ": " + aliquotOrder.Label;

            XmlNode rowSpecimenNodeClone = rowSpecimenNode.Clone();
            rowSpecimenNodeClone.SelectSingleNode("descendant::w:r[w:t='specimen_description']/w:t", this.m_NameSpaceManager).InnerText = specimenDescription;
            rowSpecimenNodeClone.SelectSingleNode("descendant::w:r[w:t='surgical_reportno']/w:t", this.m_NameSpaceManager).InnerText = surgicalTestOrder.ReportNo;
            diagnosisTableNode.InsertAfter(rowSpecimenNodeClone, insertAfterRow);

            XmlNode rowDiagnosisNodeClone = rowDiagnosisNode.Clone();
            string diagnosis = surgicalSpecimen.Diagnosis;

            this.SetXMLNodeParagraphDataNode(rowDiagnosisNodeClone, "specimen_diagnosis", diagnosis);
            diagnosisTableNode.InsertAfter(rowDiagnosisNodeClone, rowSpecimenNodeClone);

            insertAfterRow = rowDiagnosisNodeClone;
        }

        private void SetIHCResults(LynchSyndrome.IHCResult ihcResult, string reportNo, string block, XmlNode tableNode, XmlNode rowmlh1Node,
            XmlNode rowmsh2Node, XmlNode rowmsh6Node, XmlNode rowpms2Node, XmlNode insertAfterRow)
        {
            XmlNode rowmlh1NodeClone = rowmlh1Node.Clone();
            rowmlh1NodeClone.SelectSingleNode("descendant::w:r[w:t='mlh1_result']/w:t", this.m_NameSpaceManager).InnerText = ihcResult.MLH1Result.Description;
            rowmlh1NodeClone.SelectSingleNode("descendant::w:r[w:t='ihc_reportno']/w:t", this.m_NameSpaceManager).InnerText = reportNo;
            rowmlh1NodeClone.SelectSingleNode("descendant::w:r[w:t='ihc_block_label']/w:t", this.m_NameSpaceManager).InnerText = block;
            tableNode.InsertAfter(rowmlh1NodeClone, insertAfterRow);

            XmlNode rowmsh2NodeClone = rowmsh2Node.Clone();
            rowmsh2NodeClone.SelectSingleNode("descendant::w:r[w:t='msh2_result']/w:t", this.m_NameSpaceManager).InnerText = ihcResult.MSH2Result.Description;
            tableNode.InsertAfter(rowmsh2NodeClone, rowmlh1NodeClone);

            XmlNode rowmsh6NodeClone = rowmsh6Node.Clone();
            rowmsh6NodeClone.SelectSingleNode("descendant::w:r[w:t='msh6_result']/w:t", this.m_NameSpaceManager).InnerText = ihcResult.MSH6Result.Description;
            tableNode.InsertAfter(rowmsh6NodeClone, rowmsh2NodeClone);

            XmlNode rowpms2NodeClone = rowpms2Node.Clone();
            rowpms2NodeClone.SelectSingleNode("descendant::w:r[w:t='pms2_result']/w:t", this.m_NameSpaceManager).InnerText = ihcResult.PMS2Result.Description;
            tableNode.InsertAfter(rowpms2NodeClone, rowmsh6NodeClone);

            insertAfterRow = rowpms2NodeClone;
        }

        private void SetTestResults(string test, string result, string reportNo, string block, XmlNode tableNode, XmlNode rowNode, XmlNode insertAfterRow)
        {
            XmlNode rowNodeClone = rowNode.Clone();
            rowNodeClone.SelectSingleNode("descendant::w:r[w:t='" + test + "_result']/w:t", this.m_NameSpaceManager).InnerText = result;
            rowNodeClone.SelectSingleNode("descendant::w:r[w:t='" + test + "_reportno']/w:t", this.m_NameSpaceManager).InnerText = reportNo;
            rowNodeClone.SelectSingleNode("descendant::w:r[w:t='" + test + "_label']/w:t", this.m_NameSpaceManager).InnerText = block;
            tableNode.InsertAfter(rowNodeClone, insertAfterRow);

            insertAfterRow = rowNodeClone;
        }

        public override void Publish()
		{
			base.Publish();
		}
	}
}
