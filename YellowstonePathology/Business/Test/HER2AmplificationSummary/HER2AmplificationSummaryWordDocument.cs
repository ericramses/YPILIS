using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace YellowstonePathology.Business.Test.HER2AmplificationSummary
{
    public class HER2AmplificationSummaryWordDocument : YellowstonePathology.Business.Document.CaseReportV2
    {
        public HER2AmplificationSummaryWordDocument(Business.Test.AccessionOrder accessionOrder, Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Business.Document.ReportSaveModeEnum reportSaveMode)
            : base(accessionOrder, panelSetOrder, reportSaveMode)
        {

        }

        public override void Render()
        {
            HER2AmplificationByISH.HER2AmplificationByISHTest ishTest = new HER2AmplificationByISH.HER2AmplificationByISHTest();
            HER2AmplificationByISH.HER2AmplificationByISHTestOrder panelSetOrderHer2ByIsh = (HER2AmplificationByISH.HER2AmplificationByISHTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(ishTest.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true);

            if (panelSetOrderHer2ByIsh.Indicator.ToUpper() == "BREAST")
            {
                if (this.m_AccessionOrder.AccessionDate >= DateTime.Parse("1/1/2014") == true)
                {
                    this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\Her2AmplificationSummary.Breast.xml";
                }
                else
                {
                    this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\HER2AmplificationByISH.ASCOPre2014.1.xml";
                }
            }
            else if (panelSetOrderHer2ByIsh.Indicator.ToUpper() == "GASTRIC")
            {
                if (this.m_AccessionOrder.AccessionDate >= DateTime.Parse("1/1/2014") == true)
                {
                    this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\Her2AmplificationByISH.Gastric.2.xml";
                }
                else
                {
                    this.m_TemplateName = @"\\CFileServer\Documents\ReportTemplates\XmlTemplates\HER2AmplificationByISH.ASCOPre2014.1.xml";
                }
            }

            base.OpenTemplate();

            this.SetDemographicsV2();
            this.SetReportDistribution();
            this.SetCaseHistory();

            if (this.m_AccessionOrder.OrderCancelled == false)
            {

                YellowstonePathology.Business.Document.AmendmentSection amendmentSection = new YellowstonePathology.Business.Document.AmendmentSection();
                amendmentSection.SetAmendment(m_PanelSetOrder.AmendmentCollection, this.m_ReportXml, this.m_NameSpaceManager, true);

                YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.m_PanelSetOrder.OrderedOn, this.m_PanelSetOrder.OrderedOnId);
                string collectionDateTimeString = YellowstonePathology.Business.Helper.DateTimeExtensions.CombineDateAndTime(specimenOrder.CollectionDate, specimenOrder.CollectionTime);

                this.SetXmlNodeData("date_time_collected", collectionDateTimeString);

                string result = panelSetOrderHer2ByIsh.Result;
                if (result == null) result = string.Empty;
                if (result.ToUpper() == "NEGATIVE")
                {
                    result += " (see interpretation)";
                }
                this.SetXmlNodeData("test_result", result);

                Her2AmplificationByIHC.PanelSetOrderHer2AmplificationByIHC panelSetOrderHer2AmplificationByIHC = null;
                if (panelSetOrderHer2ByIsh.HER2ByIHCRequired == true)
                {
                    Her2AmplificationByIHC.Her2AmplificationByIHCTest her2AmplificationByIHCTest = new Her2AmplificationByIHC.Her2AmplificationByIHCTest();
                    if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(her2AmplificationByIHCTest.PanelSetId, panelSetOrderHer2ByIsh.OrderedOnId, true) == true)
                    {
                        panelSetOrderHer2AmplificationByIHC = (Her2AmplificationByIHC.PanelSetOrderHer2AmplificationByIHC)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(her2AmplificationByIHCTest.PanelSetId);
                    }
                }

                if (panelSetOrderHer2AmplificationByIHC == null)
                {
                    this.DeleteRow("ihc_score");
                }
                else
                {
                    this.SetXmlNodeData("ihc_score", panelSetOrderHer2AmplificationByIHC.Score + " (per report provided from " +
                        panelSetOrderHer2AmplificationByIHC.ReportNo + ")");
                }

                this.SetXmlNodeData("ish_cells_counted", panelSetOrderHer2ByIsh.CellsCounted.ToString());
                this.SetXmlNodeData("ish_her2_counted", panelSetOrderHer2ByIsh.TotalHer2SignalsCounted.ToString());
                this.SetXmlNodeData("ish_chr17_counted", panelSetOrderHer2ByIsh.TotalChr17SignalsCounted.ToString());

                HER2AmplificationRecount.HER2AmplificationRecountTestOrder recountTestOrder = null;
                HER2AmplificationRecount.HER2AmplificationRecountTest recountTest = new HER2AmplificationRecount.HER2AmplificationRecountTest();
                if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(recountTest.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true) == true)
                {
                    recountTestOrder = (HER2AmplificationRecount.HER2AmplificationRecountTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(recountTest.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true);
                    this.SetXmlNodeData("re_cells_counted", recountTestOrder.CellsCounted.ToString());
                    this.SetXmlNodeData("re_her2_counted", recountTestOrder.Her2SignalsCounted.ToString());
                    this.SetXmlNodeData("re_chr17_counted", recountTestOrder.Chr17SignalsCounted.ToString());

                }
                else
                {
                    this.DeleteRow("Recount");
                    this.DeleteRow("re_cells_counted");
                    this.DeleteRow("re_her2_counted");
                    this.DeleteRow("re_chr17_counted");
                }

                if (panelSetOrderHer2ByIsh.Her2Chr17Ratio.HasValue == true)
                {
                    this.SetXmlNodeData("test_ratio", "HER2/Chr17 Ratio = " + panelSetOrderHer2ByIsh.AverageHer2Chr17Signal);
                }
                else
                {
                    this.DeleteRow("test_ratio");
                }

                if (panelSetOrderHer2ByIsh.AverageHer2NeuSignal.HasValue == true)
                {
                    this.SetXmlNodeData("copy_number", "Average HER2 Copy Number = " + panelSetOrderHer2ByIsh.AverageHer2NeuSignal.Value.ToString());
                }
                else
                {
                    this.DeleteRow("copy_number");
                }

                this.SetXmlNodeData("cell_cnt", panelSetOrderHer2ByIsh.CellsCounted.ToString());

                if (panelSetOrderHer2ByIsh.AverageHer2NeuSignal.HasValue == true)
                {
                    this.SetXmlNodeData("avg_her", panelSetOrderHer2ByIsh.AverageHer2NeuSignal.Value.ToString());
                }
                else
                {
                    this.SetXmlNodeData("avg_her", "Unable to calculate");
                }

                this.SetXmlNodeData("avg_chr", panelSetOrderHer2ByIsh.AverageChr17Signal);

                if (panelSetOrderHer2ByIsh.Her2Chr17Ratio.HasValue == true)
                {
                    this.SetXmlNodeData("tst_ratio", panelSetOrderHer2ByIsh.Her2Chr17Ratio.Value.ToString());
                }
                else
                {
                    this.SetXmlNodeData("tst_ratio", "Unable to calculate");
                }

                this.SetXmlNodeData("obs_cnt", panelSetOrderHer2ByIsh.NumberOfObservers.ToString());

                this.SetXmlNodeData("final_date", YellowstonePathology.Business.BaseData.GetShortDateString(this.m_PanelSetOrder.FinalDate));

                if (string.IsNullOrEmpty(panelSetOrderHer2ByIsh.ResultComment) == false)
                {
                    this.SetXmlNodeData("result_comment", panelSetOrderHer2ByIsh.ResultComment);
                    this.SetXmlNodeData("comment_up", string.Empty);
                }
                else
                {
                    this.DeleteRow("result_comment");
                    this.DeleteRow("comment_up");
                }

                XmlNode tableNode = this.m_ReportXml.SelectSingleNode("descendant::w:tbl[w:tr/w:tc/w:p/w:r/w:t='report_interpretation']", this.m_NameSpaceManager);


                if (panelSetOrderHer2ByIsh.InterpretiveComment != null)
                {
                    this.SetXMLNodeParagraphDataNode(tableNode, "report_interpretation", panelSetOrderHer2ByIsh.InterpretiveComment);
                }
                else
                {
                    this.SetXMLNodeParagraphDataNode(tableNode, "report_interpretation", string.Empty);
                }

                YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetAliquotOrder(this.m_PanelSetOrder.OrderedOnId);
                string blockDescription = string.Empty;
                if (aliquotOrder != null)
                {
                    blockDescription = " - Block " + aliquotOrder.Label;
                }

                SetXmlNodeData("specimen_type", specimenOrder.Description + blockDescription);
                SetXmlNodeData("specimen_fixation", specimenOrder.LabFixation);
                SetXmlNodeData("time_to_fixation", specimenOrder.TimeToFixationHourString);

                this.SetXmlNodeData("report_reference", panelSetOrderHer2ByIsh.ReportReference);
                SetXmlNodeData("duration_of_fixation", specimenOrder.FixationDurationString);

                if (string.IsNullOrEmpty(specimenOrder.FixationComment) == false)
                {
                    this.SetXmlNodeData("fixation_comment", specimenOrder.FixationComment);
                }
                else
                {
                    this.SetXmlNodeData("fixation_comment", string.Empty);
                }

                SetXmlNodeData("report_method", panelSetOrderHer2ByIsh.Method);
                SetXmlNodeData("asr_comment", panelSetOrderHer2ByIsh.ASRComment);
                SetXmlNodeData("sample_adequacy", panelSetOrderHer2ByIsh.SampleAdequacy);
                SetXmlNodeData("date_time_collected", collectionDateTimeString);
                SetXmlNodeData("report_distribution", "No Distribution Selected");

                this.SetXmlNodeData("pathologist_signature", this.m_PanelSetOrder.Signature);
            }
            else
            {
                this.SetXmlNodeData("result_comment", panelSetOrderHer2ByIsh.ResultComment);
                this.SetXmlNodeData("final_date", YellowstonePathology.Business.BaseData.GetShortDateString(this.m_PanelSetOrder.FinalDate));
            }

            this.SaveReport();
        }

        public override void Publish()
        {
            base.Publish();
        }
    }
}
