﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.BRAFMutationAnalysis
{
    [PersistentClass("tblBRAFMutationAnalysisTestOrder", "tblPanelSetOrder", "YPIDATA")]
    public class BRAFMutationAnalysisTestOrder : YellowstonePathology.Business.Test.PanelSetOrder, YellowstonePathology.Business.Interface.ISolidTumorTesting
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

        public override void SetPreviousResults(PanelSetOrder pso)
        {
            Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisTestOrder panelSetOrder = (Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisTestOrder)pso;
            panelSetOrder.Result = this.m_Result;
            panelSetOrder.Interpretation = this.m_Interpretation;
            panelSetOrder.Indication = this.m_Indication;
            panelSetOrder.IndicationComment = this.m_IndicationComment;
            panelSetOrder.Comment = this.m_Comment;
            panelSetOrder.Method = this.m_Method;
            panelSetOrder.ReportDisclaimer = this.m_ReportDisclaimer;
            base.SetPreviousResults(pso);
        }

        public override void ClearPreviousResults()
        {
            this.m_Result = null;
            this.m_Interpretation = null;
            this.m_Indication = null;
            this.m_IndicationComment = null;
            this.m_Comment = null;
            this.m_Method = null;
            this.m_ReportDisclaimer = null;
            base.ClearPreviousResults();
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

        public string GetSummaryResult()
        {
            string brafResult = null;
            if (string.IsNullOrEmpty(this.Result) == false)
            {
                YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisNotDetectedResult notDetectedResult = new BRAFMutationAnalysisNotDetectedResult();
                YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisDetectedResult detectedResult = new BRAFMutationAnalysisDetectedResult();

                if (this.Result.ToUpper().Contains("NOT DETECTED"))
                {
                    brafResult = YellowstonePathology.Business.Test.TestResult.NotDetected;
                }
                else if (this.Result.ToUpper().Contains("DETECTED"))
                {
                    brafResult = YellowstonePathology.Business.Test.TestResult.Detected;
                }
            }
            return brafResult;
        }

        /*public void SetSummaryResult(YellowstonePathology.Business.Test.LynchSyndrome.LSERule lSERule)
        {
            if (string.IsNullOrEmpty(this.Result) == false)
            {
                YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisNotDetectedResult notDetectedResult = new BRAFMutationAnalysisNotDetectedResult();
                YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisDetectedResult detectedResult = new BRAFMutationAnalysisDetectedResult();

                if (this.Result.ToUpper().Contains("NOT DETECTED"))
                {
                    lSERule.BRAFResult = YellowstonePathology.Business.Test.LynchSyndrome.LSEResultEnum.NotDetected;
                }
                else if (this.Result.ToUpper().Contains("DETECTED"))
                {
                    lSERule.BRAFResult = YellowstonePathology.Business.Test.LynchSyndrome.LSEResultEnum.Detected;
                }
            }
        }*/

        public override Audit.Model.AuditResult IsOkToSetPreviousResults(PanelSetOrder panelSetOrder, AccessionOrder accessionOrder)
        {
            Audit.Model.AuditResult result = base.IsOkToSetPreviousResults(panelSetOrder, accessionOrder);
            if (result.Status == Audit.Model.AuditStatusEnum.OK)
            {
                BRAFMutationAnalysisTestOrder pso = (BRAFMutationAnalysisTestOrder)panelSetOrder;
                this.DoesFinalSummaryResultMatch(accessionOrder, pso.Result, result);
                if (result.Status == Audit.Model.AuditStatusEnum.Warning)
                {
                    result.Message += AskSetPreviousResults;
                }
            }

            return result;
        }

        public override Audit.Model.AuditResult IsOkToAccept(AccessionOrder accessionOrder)
        {
            Audit.Model.AuditResult result = base.IsOkToAccept(accessionOrder);
            if (result.Status == Audit.Model.AuditStatusEnum.OK)
            {
                if (string.IsNullOrEmpty(this.Result) == true)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Failure;
                    result.Message = UnableToAccept;
                }
            }

            if (result.Status == Audit.Model.AuditStatusEnum.OK)
            {
                if (string.IsNullOrEmpty(this.Indication) == true)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Failure;
                    result.Message = "The results cannot be accepted because the BRAF indicator is not set.";
                }
            }

            if (result.Status == Audit.Model.AuditStatusEnum.OK)
            {
                YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexTest krasStandardReflexTest = new KRASStandardReflex.KRASStandardReflexTest();
                if (accessionOrder.PanelSetOrderCollection.Exists(krasStandardReflexTest.PanelSetId, this.OrderedOnId, true) == false)
                {
                    if (string.IsNullOrEmpty(this.TumorNucleiPercentage) == true)
                    {
                        result.Status = Audit.Model.AuditStatusEnum.Failure;
                        result.Message = "This case cannot be accepted because the Tumor Nuclei Percent is not set.";
                    }
                }
            }

            if (result.Status == Audit.Model.AuditStatusEnum.OK)
            {
                this.DoesFinalSummaryResultMatch(accessionOrder, this.m_Result, result);
                if (result.Status == Audit.Model.AuditStatusEnum.Warning)
                {
                    result.Message += AskAccept;
                }
            }

            return result;
        }

        public override YellowstonePathology.Business.Audit.Model.AuditResult IsOkToFinalize(Test.AccessionOrder accessionOrder)
        {
            Audit.Model.AuditResult result = base.IsOkToFinalize(accessionOrder);
            if (result.Status == Audit.Model.AuditStatusEnum.OK)
            {
                if (string.IsNullOrEmpty(this.m_Result) == true)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Failure;
                    result.Message = UnableToFinal;
                }
            }

            if (result.Status == Audit.Model.AuditStatusEnum.OK)
            {
                if (string.IsNullOrEmpty(this.Indication) == true)
                {
                    result.Status = Audit.Model.AuditStatusEnum.Failure;
                    result.Message = "The results cannot be finalized because the BRAF indicator is not set.";
                }
            }

            if (result.Status == Audit.Model.AuditStatusEnum.OK)
            {
                YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexTest krasStandardReflexTest = new KRASStandardReflex.KRASStandardReflexTest();
                if (accessionOrder.PanelSetOrderCollection.Exists(krasStandardReflexTest.PanelSetId, this.OrderedOnId, true) == false)
                {
                    if (string.IsNullOrEmpty(this.TumorNucleiPercentage) == true)
                    {
                        result.Status = Audit.Model.AuditStatusEnum.Failure;
                        result.Message = "This case cannot be finalized because the Tumor Nuclei Percent is not set.";
                    }
                }
            }

            if (result.Status == Audit.Model.AuditStatusEnum.OK)
            {
                this.DoesFinalSummaryResultMatch(accessionOrder, this.m_Result, result);
                if (result.Status == Audit.Model.AuditStatusEnum.Warning)
                {
                    result.Message += AskFinal;
                }
            }

            return result;
        }

        private void DoesFinalSummaryResultMatch(AccessionOrder accessionOrder, string result, Audit.Model.AuditResult auditResult)
        {
            Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisTest egfrToALKReflexAnalysisTest = new EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisTest();

            if (accessionOrder.PanelSetOrderCollection.Exists(egfrToALKReflexAnalysisTest.PanelSetId) == true)
            {
                Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisTestOrder egfrToALKReflexAnalysisTestOrder = (EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(egfrToALKReflexAnalysisTest.PanelSetId);
                egfrToALKReflexAnalysisTestOrder.DoesBRAFMutationAnalysisResultMatch(result, auditResult);
            }
        }
    }
}
