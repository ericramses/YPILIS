﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.BRAFMutationAnalysis
{
    [PersistentClass("tblBRAFMutationAnalysisTestOrder", "tblPanelSetOrder", "YPIDATA")]
    public class BRAFMutationAnalysisTestOrder : YellowstonePathology.Business.Test.PanelSetOrder
    {
        private string m_Result;
        private string m_Interpretation;
        private string m_Comment;
        private string m_Indication;
        private string m_IndicationComment;
        private string m_TumorNucleiPercentage;
        private string m_Method;
        private string m_ReportDisclaimer;

        public BRAFMutationAnalysisTestOrder()
        {

        }

        public BRAFMutationAnalysisTestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
            bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{
            this.m_ReportDisclaimer = "The performance characteristics of this test have been determined by NeoGenomics Laboratories. This test " +
                "has not been approved by the FDA. The FDA has determined such clearance or approval is not necessary. This laboratory is CLIA " +
                "certified to perform high complexity clinical testing.";
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string Result
        {
            get { return this.m_Result; }
            set
            {
                if (this.m_Result != value)
                {
                    this.m_Result = value;
                    this.NotifyPropertyChanged("Result");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "5000", "null", "varchar")]
        public string Interpretation
        {
            get { return this.m_Interpretation; }
            set
            {
                if (this.m_Interpretation != value)
                {
                    this.m_Interpretation = value;
                    this.NotifyPropertyChanged("Interpretation");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
        public string Comment
        {
            get { return this.m_Comment; }
            set
            {
                if (this.m_Comment != value)
                {
                    this.m_Comment = value;
                    this.NotifyPropertyChanged("Comment");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string Indication
        {
            get { return this.m_Indication; }
            set
            {
                if (this.m_Indication != value)
                {
                    this.m_Indication = value;
                    this.NotifyPropertyChanged("Indication");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
        public string IndicationComment
        {
            get { return this.m_IndicationComment; }
            set
            {
                if (this.m_IndicationComment != value)
                {
                    this.m_IndicationComment = value;
                    this.NotifyPropertyChanged("IndicationComment");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string TumorNucleiPercentage
        {
            get { return this.m_TumorNucleiPercentage; }
            set
            {
                if (this.m_TumorNucleiPercentage != value)
                {
                    this.m_TumorNucleiPercentage = value;
                    this.NotifyPropertyChanged("TumorNucleiPercentage");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "5000", "null", "varchar")]
        public string Method
        {
            get { return this.m_Method; }
            set
            {
                if (this.m_Method != value)
                {
                    this.m_Method = value;
                    this.NotifyPropertyChanged("Method");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1000", "null", "varchar")]
        public string ReportDisclaimer
        {
            get { return this.m_ReportDisclaimer; }
            set
            {
                if (this.m_ReportDisclaimer != value)
                {
                    this.m_ReportDisclaimer = value;
                    this.NotifyPropertyChanged("ReportDisclaimer");
                }
            }
        }

        public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            StringBuilder result = new StringBuilder();
            result.Append("Result: ");
            result.AppendLine(this.m_Result);
            result.AppendLine();
            result.Append("Result Comment: ");
            result.AppendLine(this.m_Comment);
            result.AppendLine();
            result.Append("Interpretation: ");
            result.AppendLine(this.m_Interpretation);
            result.AppendLine();
            result.Append("Indication: ");
            result.AppendLine(this.m_Indication);
            result.AppendLine();
            result.Append("Tumor Nuclei Percent: ");
            result.AppendLine(this.m_TumorNucleiPercentage);
            return result.ToString();
        }

        public void SetSummaryResult(YellowstonePathology.Business.Test.LynchSyndrome.LSEResult lSEResult)
        {
            if (string.IsNullOrEmpty(this.Result) == false)
            {
                YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisNotDetectedResult notDetectedResult = new BRAFMutationAnalysisNotDetectedResult();
                YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisDetectedResult detectedResult = new BRAFMutationAnalysisDetectedResult();

                if (this.ResultCode == notDetectedResult.ResultCode)
                {
                    lSEResult.BrafResult = YellowstonePathology.Business.Test.LynchSyndrome.LSEResultEnum.Negative;
                }
                else if (this.ResultCode == detectedResult.ResultCode)
                {
                    lSEResult.BrafResult = YellowstonePathology.Business.Test.LynchSyndrome.LSEResultEnum.Positive;
                }
            }
        }
    }
}
