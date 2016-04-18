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
			this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\ComprehensiveColonCancerProfile.1.xml";
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
                    ichOrdered = true;
                    panelSetOrderLynchSyndromeIHC = (LynchSyndrome.PanelSetOrderLynchSyndromeIHC)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetLynchSyndromeIHCPanel.PanelSetId, specimenOrder.SpecimenOrderId, true);
                    ihcResult = YellowstonePathology.Business.Test.LynchSyndrome.IHCResult.CreateResultFromResultCode(panelSetOrderLynchSyndromeIHC.ResultCode);
                    this.SetIHCResults(ihcResult, panelSetOrderLynchSyndromeIHC.ReportNo, ichTableNode, rowmlh1Node, rowmsh2Node, rowmsh6Node, rowpms2Node, insertAfterichRow);
                }
                foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in specimenOrder.AliquotOrderCollection)
                {
                    if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSetLynchSyndromeIHCPanel.PanelSetId, aliquotOrder.AliquotOrderId, true) == true)
                    {
                        ichOrdered = true;
                        panelSetOrderLynchSyndromeIHC = (LynchSyndrome.PanelSetOrderLynchSyndromeIHC)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetLynchSyndromeIHCPanel.PanelSetId, aliquotOrder.AliquotOrderId, true);
                        ihcResult = YellowstonePathology.Business.Test.LynchSyndrome.IHCResult.CreateResultFromResultCode(panelSetOrderLynchSyndromeIHC.ResultCode);
                        this.SetIHCResults(ihcResult, panelSetOrderLynchSyndromeIHC.ReportNo, ichTableNode, rowmlh1Node, rowmsh2Node, rowmsh6Node, rowpms2Node, insertAfterichRow);
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

            DateTime testChangeDate = DateTime.Parse("11/23/2015");
            if(comprehensiveColonCancerProfile.OrderDate.Value > testChangeDate)
            {
                RenderRasRafResults(comprehensiveColonCancerProfile, comprehensiveColonCancerProfileResult);
            }
            else
            {
                RenderSeparateTestResults(comprehensiveColonCancerProfile, comprehensiveColonCancerProfileResult);
            }
                        
            if (comprehensiveColonCancerProfileResult.PanelSetOrderMLH1MethylationAnalysis != null)
			{
				base.ReplaceText("mlh1promoter_result", comprehensiveColonCancerProfileResult.PanelSetOrderMLH1MethylationAnalysis.Result);
				base.ReplaceText("mlh1promoter_reportno", comprehensiveColonCancerProfileResult.PanelSetOrderMLH1MethylationAnalysis.ReportNo);
			}
			else
			{
				this.DeleteRow("mlh1promoter_result");
			}

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
            else
            {
                this.DeleteRow("kras_result");
                this.DeleteRow("braf_result");
                this.DeleteRow("nras_result");
                this.DeleteRow("hras_result");
            }
        }

        private void RenderSeparateTestResults(ComprehensiveColonCancerProfile comprehensiveColonCancerProfile, 
            ComprehensiveColonCancerProfileResult comprehensiveColonCancerProfileResult)
        {
            bool deleteKrasResult = true;
            bool hasMolecularTest = false;
            if (comprehensiveColonCancerProfileResult.KRASStandardIsOrderd == true)
            {
                base.ReplaceText("kras_result", comprehensiveColonCancerProfileResult.KRASStandardTestOrder.Result);
                base.ReplaceText("kras_reportno", comprehensiveColonCancerProfileResult.KRASStandardTestOrder.ReportNo);
                deleteKrasResult = false;
                hasMolecularTest = true;
            }

            if (comprehensiveColonCancerProfileResult.BRAFV600EKIsOrdered == true)
            {
                base.ReplaceText("braf_result", comprehensiveColonCancerProfileResult.BRAFV600EKTestOrder.Result);
                base.ReplaceText("braf_reportno", comprehensiveColonCancerProfileResult.BRAFV600EKTestOrder.ReportNo);
                hasMolecularTest = true;
            }
            else
            {
                this.DeleteRow("braf_result");
            }

            if (comprehensiveColonCancerProfileResult.KRASExon23MutationIsOrdered == true)
            {
                base.ReplaceText("kras_result", comprehensiveColonCancerProfileResult.KRASExon23MutationTestOrder.Result);
                base.ReplaceText("kras_reportno", comprehensiveColonCancerProfileResult.KRASExon23MutationTestOrder.ReportNo);
                deleteKrasResult = false;
                hasMolecularTest = true;
            }

            if (comprehensiveColonCancerProfileResult.KRASExon4MutationIsOrdered == true)
            {
                base.ReplaceText("kras_result", comprehensiveColonCancerProfileResult.KRASExon4MutationTestOrder.Result);
                base.ReplaceText("kras_reportno", comprehensiveColonCancerProfileResult.KRASExon4MutationTestOrder.ReportNo);
                deleteKrasResult = false;
                hasMolecularTest = true;
            }

            if (comprehensiveColonCancerProfileResult.NRASMutationAnalysisIsOrdered == true)
            {
                base.ReplaceText("nras_result", comprehensiveColonCancerProfileResult.NRASMutationAnalysisTestOrder.Result);
                base.ReplaceText("nras_reportno", comprehensiveColonCancerProfileResult.NRASMutationAnalysisTestOrder.ReportNo);
                hasMolecularTest = true;
            }
            else
            {
                this.DeleteRow("nras_result");
            }

            if(deleteKrasResult == true)
            {
                this.DeleteRow("kras_result");
            }

            if(hasMolecularTest == true)
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

        private void SetIHCResults(LynchSyndrome.IHCResult ihcResult, string reportNo, XmlNode ichTableNode, XmlNode rowmlh1Node,
            XmlNode rowmsh2Node, XmlNode rowmsh6Node, XmlNode rowpms2Node, XmlNode insertAfterichRow)
        {
            XmlNode rowmlh1NodeClone = rowmlh1Node.Clone();
            rowmlh1NodeClone.SelectSingleNode("descendant::w:r[w:t='mlh1_result']/w:t", this.m_NameSpaceManager).InnerText = ihcResult.MLH1Result.Description;
            rowmlh1NodeClone.SelectSingleNode("descendant::w:r[w:t='ihc_reportno']/w:t", this.m_NameSpaceManager).InnerText = reportNo;
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

        public override void Publish()
		{
			base.Publish();
		}
	}
}
