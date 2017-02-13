using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.PMLRARAByFish
{
    [PersistentClass("tblPMLRARAByFishTestOrder", "tblPanelSetOrder", "YPIDATA")]
    public class PMLRARAByFishTestOrder : PanelSetOrder
    {
        public static string PositiveResult = "Positive";
        public static string NegativeResult = "Negative";
        public static string PositiveResultCode = "PMLRRFSHPSTV";
        public static string NegativeResultCode = "PMLRRFSHNGTV";

        private string m_Result;
        private string m_Interpretation;
        private string m_ProbeSetDetail;
        private string m_NucleiScored;
        private string m_ASR;
        public PMLRARAByFishTestOrder()
        {

        }

        public PMLRARAByFishTestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
            bool distribute)
            : base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
        {
            this.m_ASR = "NeoGenomics Laboratories FISH test uses either FDA cleared and/or analyte specific reagent (ASR) probes. This test was " +
                "developed and its performance characteristics determined by NeoGenomics Laboratories in Irvine CA. It has not been cleared or " +
                "approved by the U.S.Food and Drug Administration (FDA).  The FDA has determined that such clearance or approval is not necessary.  " +
                "This test is used for clinical purposes and should not be regarded as investigational or for research.  This laboratory is " +
                "regulated under CLIA '88 as qualified to perform high complexity testing.  Interphase FISH does not include examination of the " +
                "entire chromosomal complement.  Clinically significant anomalies detectable by routine banded cytogenetic analysis may still be " +
                "present.  Consider reflex banded cytogenetic analysis.";
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
        [PersistentDataColumnProperty(true, "5000", "null", "varchar")]
        public string ProbeSetDetail
        {
            get { return this.m_ProbeSetDetail; }
            set
            {
                if (this.m_ProbeSetDetail != value)
                {
                    this.m_ProbeSetDetail = value;
                    this.NotifyPropertyChanged("ProbeSetDetail");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string NucleiScored
        {
            get { return this.m_NucleiScored; }
            set
            {
                if (this.m_NucleiScored != value)
                {
                    this.m_NucleiScored = value;
                    this.NotifyPropertyChanged("NucleiScored");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1000", "null", "varchar")]
        public string ASR
        {
            get { return this.m_ASR; }
            set
            {
                if (this.m_ASR != value)
                {
                    this.m_ASR = value;
                    this.NotifyPropertyChanged("ASR");
                }
            }
        }

        public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("Result: " + this.m_Result);
            result.AppendLine();

            result.AppendLine("Interpretation: " + this.m_Interpretation);
            result.AppendLine();

            result.AppendLine("Probe Set Detail: " + this.m_ProbeSetDetail);
            result.AppendLine();

            result.AppendLine("Nuclei Scored: " + this.m_NucleiScored);
            result.AppendLine();

            return result.ToString();
        }

        public override YellowstonePathology.Business.Rules.MethodResult IsOkToAccept()
        {
            YellowstonePathology.Business.Rules.MethodResult result = base.IsOkToAccept();
            if (result.Success == true)
            {
                if (string.IsNullOrEmpty(this.m_Result) == true)
                {
                    result.Success = false;
                    result.Message = "The results cannot be accepted because the result is not set.";
                }
            }
            return result;
        }
    }
}
