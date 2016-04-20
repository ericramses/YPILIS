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

            YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeIHCPanelTest panelSetLynchSyndromeIHCPanel = new YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeIHCPanelTest();
            YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeIHC panelSetOrderLynchSyndromeIHC = null;
            YellowstonePathology.Business.Test.LynchSyndrome.IHCResult ihcResult = null;
            XmlNode ichTableNode = this.m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='mlh1_result']", this.m_NameSpaceManager);
            XmlNode rowmlh1Node = ichTableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='mlh1_result']", this.m_NameSpaceManager);
            XmlNode rowmsh2Node = ichTableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='msh2_result']", this.m_NameSpaceManager);
            XmlNode rowmsh6Node = ichTableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='msh6_result']", this.m_NameSpaceManager);
            XmlNode rowpms2Node = ichTableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='pms2_result']", this.m_NameSpaceManager);
            XmlNode insertAfterichRow = rowmlh1Node;
            bool ichOrdered = false;
            foreach (YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in this.m_AccessionOrder.SpecimenOrderCollection)
            {
                if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetLynchSyndromeIHCPanel.PanelSetId, specimenOrder.SpecimenOrderId, true) == true)
                {
                    string block = specimenOrder.Description;
                    ichOrdered = true;
                    panelSetOrderLynchSyndromeIHC = (LynchSyndrome.PanelSetOrderLynchSyndromeIHC)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetLynchSyndromeIHCPanel.PanelSetId, specimenOrder.SpecimenOrderId, true);
                    ihcResult = YellowstonePathology.Business.Test.LynchSyndrome.IHCResult.CreateResultFromResultCode(panelSetOrderLynchSyndromeIHC.ResultCode);
                    this.SetIHCResults(ihcResult, panelSetOrderLynchSyndromeIHC.ReportNo, block, ichTableNode, rowmlh1Node, rowmsh2Node, rowmsh6Node, rowpms2Node, insertAfterichRow);
                }
                foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in specimenOrder.AliquotOrderCollection)
                {
                    if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetLynchSyndromeIHCPanel.PanelSetId, aliquotOrder.AliquotOrderId, true) == true)
                    {
                        string block = aliquotOrder.Label;
                        ichOrdered = true;
                        panelSetOrderLynchSyndromeIHC = (LynchSyndrome.PanelSetOrderLynchSyndromeIHC)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetLynchSyndromeIHCPanel.PanelSetId, aliquotOrder.AliquotOrderId, true);
                        ihcResult = YellowstonePathology.Business.Test.LynchSyndrome.IHCResult.CreateResultFromResultCode(panelSetOrderLynchSyndromeIHC.ResultCode);
                        this.SetIHCResults(ihcResult, panelSetOrderLynchSyndromeIHC.ReportNo, block, ichTableNode, rowmlh1Node, rowmsh2Node, rowmsh6Node, rowpms2Node, insertAfterichRow);
                    }
                }
            }

            if(ichOrdered == true)
            {
                ichTableNode.RemoveChild(rowmlh1Node);
                ichTableNode.RemoveChild(rowmsh2Node);
                ichTableNode.RemoveChild(rowmsh6Node);
                ichTableNode.RemoveChild(rowpms2Node);
            }
            else
            {
                base.ReplaceText("ihc_reportno", "Not Included");
                base.ReplaceText("mlh1_result", "Not Included");
                base.ReplaceText("msh2_result", "Not Included");
                base.ReplaceText("msh6_result", "Not Included");
                base.ReplaceText("pms2_result", "Not Included");
            }

            RenderRasRafResults(comprehensiveColonCancerProfile, comprehensiveColonCancerProfileResult);
            RenderSeparateTestResults(comprehensiveColonCancerProfile, comprehensiveColonCancerProfileResult);

            base.ReplaceText("pathologist_signature", comprehensiveColonCancerProfile.Signature);

			this.SaveReport();
		}

        private void RenderRasRafResults(ComprehensiveColonCancerProfile comprehensiveColonCancerProfile,
            ComprehensiveColonCancerProfileResult comprehensiveColonCancerProfileResult)
        {
            if (comprehensiveColonCancerProfileResult.RASRAFIsOrdered == true)
            {
                base.ReplaceText("braf_result", comprehensiveColonCancerProfileResult.RASRAFTestOrder.BRAFResult);
                base.ReplaceText("braf_reportno", comprehensiveColonCancerProfile.ReportNo);

                base.ReplaceText("kras_result", comprehensiveColonCancerProfileResult.RASRAFTestOrder.KRASResult);
                base.ReplaceText("kras_reportno", comprehensiveColonCancerProfile.ReportNo);

                base.ReplaceText("nras_result", comprehensiveColonCancerProfileResult.RASRAFTestOrder.NRASResult);
                base.ReplaceText("nras_reportno", comprehensiveColonCancerProfile.ReportNo);

                base.ReplaceText("hras_result", comprehensiveColonCancerProfileResult.RASRAFTestOrder.HRASResult);
                base.ReplaceText("hras_reportno", comprehensiveColonCancerProfile.ReportNo);
                this.DeleteRow("None Performed");
            }
        }

        private void RenderSeparateTestResults(ComprehensiveColonCancerProfile comprehensiveColonCancerProfile, 
            ComprehensiveColonCancerProfileResult comprehensiveColonCancerProfileResult)
        {
            bool hasMolecularTest = false;
            string test =string.Empty;
            string result = string.Empty;
            string reportNo = string.Empty;
            string block = string.Empty;

            LynchSyndrome.MLH1MethylationAnalysisTest mlh1MethylationAnalysisTest = new LynchSyndrome.MLH1MethylationAnalysisTest();

            XmlNode tableNode = this.m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='mlh1promoter_result']", this.m_NameSpaceManager);
            XmlNode rowResultNode = tableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='mlh1promoter_result']", this.m_NameSpaceManager);
            XmlNode insertAfterRow = rowResultNode;

            foreach (YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in this.m_AccessionOrder.SpecimenOrderCollection)
            {
                if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(mlh1MethylationAnalysisTest.PanelSetId, specimenOrder.SpecimenOrderId, true) == true)
                {
                    LynchSyndrome.PanelSetOrderMLH1MethylationAnalysis panelSetOrderMLH1MethylationAnalysis = (LynchSyndrome.PanelSetOrderMLH1MethylationAnalysis)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(mlh1MethylationAnalysisTest.PanelSetId, specimenOrder.SpecimenOrderId, true);
                    test = "mlh1promoter";
                    reportNo = panelSetOrderMLH1MethylationAnalysis.ReportNo;
                    result = panelSetOrderMLH1MethylationAnalysis.Result;
                    block = specimenOrder.Description;
                    this.SetTestResults(test, result, reportNo, block, tableNode, rowResultNode, insertAfterRow);
                    hasMolecularTest = true;
                }
                foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in specimenOrder.AliquotOrderCollection)
                {
                    if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(mlh1MethylationAnalysisTest.PanelSetId, aliquotOrder.AliquotOrderId, true) == true)
                    {
                        LynchSyndrome.PanelSetOrderMLH1MethylationAnalysis panelSetOrderMLH1MethylationAnalysis = (LynchSyndrome.PanelSetOrderMLH1MethylationAnalysis)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(mlh1MethylationAnalysisTest.PanelSetId, aliquotOrder.AliquotOrderId, true);
                        test = "mlh1promoter";
                        reportNo = panelSetOrderMLH1MethylationAnalysis.ReportNo;
                        result = panelSetOrderMLH1MethylationAnalysis.Result;
                        block = aliquotOrder.Label;
                        this.SetTestResults(test, result, reportNo, block, tableNode, rowResultNode, insertAfterRow);
                        hasMolecularTest = true;
                    }
                }
            }
            tableNode.RemoveChild(rowResultNode);

            KRASStandard.KRASStandardTest krasStandardTest = new KRASStandard.KRASStandardTest();
            tableNode = this.m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='kras_result']", this.m_NameSpaceManager);
            rowResultNode = tableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='kras_result']", this.m_NameSpaceManager);
            insertAfterRow = rowResultNode;

            foreach (YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in this.m_AccessionOrder.SpecimenOrderCollection)
            {
                if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(krasStandardTest.PanelSetId, specimenOrder.SpecimenOrderId, true) == true)
                {
                    KRASStandard.KRASStandardTestOrder krasStandardTestOrder = (KRASStandard.KRASStandardTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(krasStandardTest.PanelSetId, specimenOrder.SpecimenOrderId, true);
                    test = "kras";
                    reportNo = krasStandardTestOrder.ReportNo;
                    result = krasStandardTestOrder.Result;
                    block = specimenOrder.Description;
                    this.SetTestResults(test, result, reportNo, block, tableNode, rowResultNode, insertAfterRow);
                    hasMolecularTest = true;
                }
                foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in specimenOrder.AliquotOrderCollection)
                {
                    if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(krasStandardTest.PanelSetId, aliquotOrder.AliquotOrderId, true) == true)
                    {
                        KRASStandard.KRASStandardTestOrder krasStandardTestOrder = (KRASStandard.KRASStandardTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(krasStandardTest.PanelSetId, aliquotOrder.AliquotOrderId, true);
                        test = "kras";
                        reportNo = krasStandardTestOrder.ReportNo;
                        result = krasStandardTestOrder.Result;
                        block = aliquotOrder.Label;
                        this.SetTestResults(test, result, reportNo, block, tableNode, rowResultNode, insertAfterRow);
                        hasMolecularTest = true;
                    }
                }
            }

            KRASExon23Mutation.KRASExon23MutationTest krasExon23MutationTest = new KRASExon23Mutation.KRASExon23MutationTest();
            insertAfterRow = rowResultNode;

            foreach (YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in this.m_AccessionOrder.SpecimenOrderCollection)
            {
                if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(krasExon23MutationTest.PanelSetId, specimenOrder.SpecimenOrderId, true) == true)
                {
                    KRASExon23Mutation.KRASExon23MutationTestOrder krasExon23MutationTestOrder = (KRASExon23Mutation.KRASExon23MutationTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(krasExon23MutationTest.PanelSetId, specimenOrder.SpecimenOrderId, true);
                    test = "kras";
                    reportNo = krasExon23MutationTestOrder.ReportNo;
                    result = krasExon23MutationTestOrder.Result;
                    block = specimenOrder.Description;
                    this.SetTestResults(test, result, reportNo, block, tableNode, rowResultNode, insertAfterRow);
                    hasMolecularTest = true;
                }
                foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in specimenOrder.AliquotOrderCollection)
                {
                    if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(krasExon23MutationTest.PanelSetId, aliquotOrder.AliquotOrderId, true) == true)
                    {
                        KRASExon23Mutation.KRASExon23MutationTestOrder krasExon23MutationTestOrder = (KRASExon23Mutation.KRASExon23MutationTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(krasExon23MutationTest.PanelSetId, aliquotOrder.AliquotOrderId, true);
                        test = "kras";
                        reportNo = krasExon23MutationTestOrder.ReportNo;
                        result = krasExon23MutationTestOrder.Result;
                        block = aliquotOrder.Label;
                        this.SetTestResults(test, result, reportNo, block, tableNode, rowResultNode, insertAfterRow);
                        hasMolecularTest = true;
                    }
                }
            }

            KRASExon4Mutation.KRASExon4MutationTest krasExon4MutationTest = new KRASExon4Mutation.KRASExon4MutationTest();
            insertAfterRow = rowResultNode;

            foreach (YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in this.m_AccessionOrder.SpecimenOrderCollection)
            {
                if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(krasExon4MutationTest.PanelSetId, specimenOrder.SpecimenOrderId, true) == true)
                {
                    KRASExon4Mutation.KRASExon4MutationTestOrder krasExon4MutationTestOrder = (KRASExon4Mutation.KRASExon4MutationTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(krasExon4MutationTest.PanelSetId, specimenOrder.SpecimenOrderId, true);
                    test = "kras";
                    reportNo = krasExon4MutationTestOrder.ReportNo;
                    result = krasExon4MutationTestOrder.Result;
                    block = specimenOrder.Description;
                    this.SetTestResults(test, result, reportNo, block, tableNode, rowResultNode, insertAfterRow);
                    hasMolecularTest = true;
                }
                foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in specimenOrder.AliquotOrderCollection)
                {
                    if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(krasExon4MutationTest.PanelSetId, aliquotOrder.AliquotOrderId, true) == true)
                    {
                        if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(mlh1MethylationAnalysisTest.PanelSetId, aliquotOrder.AliquotOrderId, true) == true)
                        {
                            KRASExon4Mutation.KRASExon4MutationTestOrder krasExon4MutationTestOrder = (KRASExon4Mutation.KRASExon4MutationTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(krasExon4MutationTest.PanelSetId, aliquotOrder.AliquotOrderId, true);
                            test = "kras";
                            reportNo = krasExon4MutationTestOrder.ReportNo;
                            result = krasExon4MutationTestOrder.Result;
                            block = aliquotOrder.Label;
                            this.SetTestResults(test, result, reportNo, block, tableNode, rowResultNode, insertAfterRow);
                            hasMolecularTest = true;
                        }
                    }
                }
            }
            tableNode.RemoveChild(rowResultNode);

            BRAFV600EK.BRAFV600EKTest brafV600EKTest = new BRAFV600EK.BRAFV600EKTest();
            tableNode = this.m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='braf_result']", this.m_NameSpaceManager);
            rowResultNode = tableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='braf_result']", this.m_NameSpaceManager);
            insertAfterRow = rowResultNode;

            foreach (YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in this.m_AccessionOrder.SpecimenOrderCollection)
            {
                if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(brafV600EKTest.PanelSetId, specimenOrder.SpecimenOrderId, true) == true)
                {
                    BRAFV600EK.BRAFV600EKTestOrder brafV600EKTestOrder = (BRAFV600EK.BRAFV600EKTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(brafV600EKTest.PanelSetId, specimenOrder.SpecimenOrderId, true);
                    test = "braf";
                    reportNo = brafV600EKTestOrder.ReportNo;
                    result = brafV600EKTestOrder.Result;
                    block = specimenOrder.Description;
                    this.SetTestResults(test, result, reportNo, block, tableNode, rowResultNode, insertAfterRow);
                    hasMolecularTest = true;
                }
                foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in specimenOrder.AliquotOrderCollection)
                {
                    if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(brafV600EKTest.PanelSetId, aliquotOrder.AliquotOrderId, true) == true)
                    {
                        BRAFV600EK.BRAFV600EKTestOrder brafV600EKTestOrder = (BRAFV600EK.BRAFV600EKTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(brafV600EKTest.PanelSetId, aliquotOrder.AliquotOrderId, true);
                        test = "braf";
                        reportNo = brafV600EKTestOrder.ReportNo;
                        result = string.IsNullOrEmpty(brafV600EKTestOrder.Result) ? string.Empty : brafV600EKTestOrder.Result;
                        block = aliquotOrder.Label;
                        this.SetTestResults(test, result, reportNo, block, tableNode, rowResultNode, insertAfterRow);
                        hasMolecularTest = true;
                    }
                }
            }
            tableNode.RemoveChild(rowResultNode);

            NRASMutationAnalysis.NRASMutationAnalysisTest nrasMutationAnalysisTest = new NRASMutationAnalysis.NRASMutationAnalysisTest();
            tableNode = this.m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='nras_result']", this.m_NameSpaceManager);
            rowResultNode = tableNode.SelectSingleNode("descendant::w:tr[w:tc/w:p/w:r/w:t='nras_result']", this.m_NameSpaceManager);
            insertAfterRow = rowResultNode;

            foreach (YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in this.m_AccessionOrder.SpecimenOrderCollection)
            {
                if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(nrasMutationAnalysisTest.PanelSetId, specimenOrder.SpecimenOrderId, true) == true)
                {
                    NRASMutationAnalysis.NRASMutationAnalysisTestOrder nrasMutationAnalysisTestOrder = (NRASMutationAnalysis.NRASMutationAnalysisTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(nrasMutationAnalysisTest.PanelSetId, specimenOrder.SpecimenOrderId, true);
                    test = "nras";
                    reportNo = nrasMutationAnalysisTestOrder.ReportNo;
                    result = nrasMutationAnalysisTestOrder.Result;
                    block = specimenOrder.Description;
                    this.SetTestResults(test, result, reportNo, block, tableNode, rowResultNode, insertAfterRow);
                    hasMolecularTest = true;
                }
                foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in specimenOrder.AliquotOrderCollection)
                {
                    if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(nrasMutationAnalysisTest.PanelSetId, aliquotOrder.AliquotOrderId, true) == true)
                    {
                        NRASMutationAnalysis.NRASMutationAnalysisTestOrder nrasMutationAnalysisTestOrder = (NRASMutationAnalysis.NRASMutationAnalysisTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(nrasMutationAnalysisTest.PanelSetId, aliquotOrder.AliquotOrderId, true);
                        test = "nras";
                        reportNo = nrasMutationAnalysisTestOrder.ReportNo;
                        result = nrasMutationAnalysisTestOrder.Result;
                        block = aliquotOrder.Label;
                        this.SetTestResults(test, result, reportNo, block, tableNode, rowResultNode, insertAfterRow);
                        hasMolecularTest = true;
                    }
                }
            }
            tableNode.RemoveChild(rowResultNode);

            if (hasMolecularTest == true)
            {
                this.DeleteRow("None Performed");
            }
            this.DeleteRow("hras_result");

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

        private void SetIHCResults(LynchSyndrome.IHCResult ihcResult, string reportNo, string block, XmlNode ichTableNode, XmlNode rowmlh1Node,
            XmlNode rowmsh2Node, XmlNode rowmsh6Node, XmlNode rowpms2Node, XmlNode insertAfterichRow)
        {
            XmlNode rowmlh1NodeClone = rowmlh1Node.Clone();
            rowmlh1NodeClone.SelectSingleNode("descendant::w:r[w:t='mlh1_result']/w:t", this.m_NameSpaceManager).InnerText = ihcResult.MLH1Result.Description;
            rowmlh1NodeClone.SelectSingleNode("descendant::w:r[w:t='ihc_reportno']/w:t", this.m_NameSpaceManager).InnerText = reportNo;
            rowmlh1NodeClone.SelectSingleNode("descendant::w:r[w:t='ihc_block_label']/w:t", this.m_NameSpaceManager).InnerText = block;
            ichTableNode.InsertAfter(rowmlh1NodeClone, insertAfterichRow);

            XmlNode rowmsh2NodeClone = rowmsh2Node.Clone();
            rowmsh2NodeClone.SelectSingleNode("descendant::w:r[w:t='msh2_result']/w:t", this.m_NameSpaceManager).InnerText = ihcResult.MSH2Result.Description;
            ichTableNode.InsertAfter(rowmsh2NodeClone, rowmlh1NodeClone);

            XmlNode rowmsh6NodeClone = rowmsh6Node.Clone();
            rowmsh6NodeClone.SelectSingleNode("descendant::w:r[w:t='msh6_result']/w:t", this.m_NameSpaceManager).InnerText = ihcResult.MSH6Result.Description;
            ichTableNode.InsertAfter(rowmsh6NodeClone, rowmsh2NodeClone);

            XmlNode rowpms2NodeClone = rowpms2Node.Clone();
            rowpms2NodeClone.SelectSingleNode("descendant::w:r[w:t='pms2_result']/w:t", this.m_NameSpaceManager).InnerText = ihcResult.PMS2Result.Description;
            ichTableNode.InsertAfter(rowpms2NodeClone, rowmsh6NodeClone);

            insertAfterichRow = rowpms2NodeClone;
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
