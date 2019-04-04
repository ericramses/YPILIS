using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Audit.Model;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.FetalHemoglobinV2
{
    [PersistentClass("tblFetalHemoglobinV2TestOrder", "tblPanelSetOrder", "YPIDATA")]
    public class FetalHemoglobinV2TestOrder : YellowstonePathology.Business.Test.PanelSetOrder
    {
        private string m_HbFPercent;
        private string m_HbFReferenceRange;
        private string m_FetalMaternalBleed;
        private string m_RhImmuneGlobulin;
        private string m_ReferenceRange;
        private string m_ReportComment;
        private string m_ASRComment;
        private string m_MothersHeightFeet;
        private string m_MothersHeightInches;
        private string m_MothersWeight;
        private string m_MothersBloodVolume;
        private string m_PercentFetalBlood;
        private string m_RecommendedNumberOfVials;


        public FetalHemoglobinV2TestOrder()
        { }

        public FetalHemoglobinV2TestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
            bool distribute)
            : base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
        {
            this.m_HbFReferenceRange = "Less than or equal to 0.04%";
            this.m_ASRComment = "Tests utilizing Analytic Specific Reagents (ASR’s) were developed and performance characteristics determined " +
                "by Yellowstone Pathology Institute, Inc.  They have not been cleared or approved by the U.S. Food and Drug Administration.  The " +
                "FDA has determined that such clearance or approval is not necessary.  ASR’s may be used for clinical purposes and should not " +
                "be regarded as investigational or for research.  This laboratory is certified under the Clinical Laboratory Improvement " +
                "Amendments of 1988 (CLIA-88) as qualified to perform high complexity clinical laboratory testing.";
        }

        [PersistentProperty()]
        public string HbFPercent
        {
            get { return this.m_HbFPercent; }
            set
            {
                if (this.m_HbFPercent != value)
                {
                    this.m_HbFPercent = value;
                    NotifyPropertyChanged("HbFPercent");
                }
            }
        }

        [PersistentProperty()]
        public string FetalMaternalBleed
        {
            get { return this.m_FetalMaternalBleed; }
            set
            {
                if (this.m_FetalMaternalBleed != value)
                {
                    this.m_FetalMaternalBleed = value;
                    NotifyPropertyChanged("FetalMaternalBleed");
                }
            }
        }

        [PersistentProperty()]
        public string RhImmuneGlobulin
        {
            get { return this.m_RhImmuneGlobulin; }
            set
            {
                if (this.m_RhImmuneGlobulin != value)
                {
                    this.m_RhImmuneGlobulin = value;
                    NotifyPropertyChanged("RhImmuneGlobulin");
                }
            }
        }

        [PersistentProperty()]
        public string ReportComment
        {
            get { return this.m_ReportComment; }
            set
            {
                if (this.m_ReportComment != value)
                {
                    this.m_ReportComment = value;
                    NotifyPropertyChanged("ReportComment");
                }
            }
        }

        [PersistentProperty()]
        public string HbFReferenceRange
        {
            get { return this.m_HbFReferenceRange; }
            set
            {
                if (this.m_HbFReferenceRange != value)
                {
                    this.m_HbFReferenceRange = value;
                    NotifyPropertyChanged("HbFReferenceRange");
                }
            }
        }

        [PersistentProperty()]
        public string ReferenceRange
        {
            get { return this.m_ReferenceRange; }
            set
            {
                if (this.m_ReferenceRange != value)
                {
                    this.m_ReferenceRange = value;
                    NotifyPropertyChanged("ReferenceRange");
                }
            }
        }

        [PersistentProperty()]
        public string ASRComment
        {
            get { return this.m_ASRComment; }
            set
            {
                if (this.m_ASRComment != value)
                {
                    this.m_ASRComment = value;
                    NotifyPropertyChanged("ASRComment");
                }
            }
        }

        [PersistentProperty()]
        public string MothersHeightFeet
        {
            get { return this.m_MothersHeightFeet; }
            set
            {
                if (this.m_MothersHeightFeet != value)
                {
                    this.m_MothersHeightFeet = value;
                    this.CalculateMothersBloodVolume();
                    NotifyPropertyChanged("MothersHeightFeet");
                }
            }
        }

        [PersistentProperty()]
        public string MothersHeightInches
        {
            get { return this.m_MothersHeightInches; }
            set
            {
                if (this.m_MothersHeightInches != value)
                {
                    this.m_MothersHeightInches = value;
                    this.CalculateMothersBloodVolume();
                    NotifyPropertyChanged("MothersHeightInches");
                }
            }
        }

        [PersistentProperty()]
        public string MothersWeight
        {
            get { return this.m_MothersWeight; }
            set
            {
                if (this.m_MothersWeight != value)
                {
                    this.m_MothersWeight = value;
                    this.CalculateMothersBloodVolume();
                    NotifyPropertyChanged("MothersWeight");
                }
            }
        }

        public string MothersBloodVolume
        {
            get { return this.m_MothersBloodVolume; }
            set
            {
                if (this.m_MothersBloodVolume != value)
                {
                    this.m_MothersBloodVolume = value;
                    NotifyPropertyChanged("MothersBloodVolume");
                }
            }
        }

        public string PercentFetalBlood
        {
            get { return this.m_PercentFetalBlood; }
            set
            {
                if (this.m_PercentFetalBlood != value)
                {
                    this.m_PercentFetalBlood = value;
                    NotifyPropertyChanged("PercentFetalBlood");
                }
            }
        }

        public string RecommendedNumberOfVials
        {
            get { return this.m_RecommendedNumberOfVials; }
            set
            {
                if (this.m_RecommendedNumberOfVials != value)
                {
                    this.m_RecommendedNumberOfVials = value;
                    NotifyPropertyChanged("RecommendedNumberOfVials");
                }
            }
        }

        private void CalculateMothersBloodVolume()
        {

        }

        private void CalculatePercentFetalBlood()
        {

        }

        private void CalculateRecommendedNumberOfVials()
        {

        }

        public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("Hb-F percent: " + this.m_HbFPercent);
            result.AppendLine();

            result.AppendLine("Fetal-Maternal Bleed: " + this.m_FetalMaternalBleed);
            result.AppendLine();

            result.AppendLine("Rh Immune Globulin: " + this.m_RhImmuneGlobulin);
            result.AppendLine();

            return result.ToString();
        }

        public override AuditResult IsOkToAccept(AccessionOrder accessionOrder)
        {
            AuditResult result = base.IsOkToAccept(accessionOrder);

            if (result.Status == AuditStatusEnum.OK)
            {
                if (string.IsNullOrEmpty(this.m_HbFPercent) == true)
                {
                    result.Status = AuditStatusEnum.Failure;
                    result.Message += "Hb-F percent" + Environment.NewLine;
                }
                if (string.IsNullOrEmpty(this.m_FetalMaternalBleed) == true)
                {
                    result.Status = AuditStatusEnum.Failure;
                    result.Message += "Fetal-Maternal Bleed" + Environment.NewLine;
                }
                if (string.IsNullOrEmpty(this.m_RhImmuneGlobulin) == true)
                {
                    result.Status = AuditStatusEnum.Failure;
                    result.Message += "Rh Immune Globulin";
                }
            }

            if (result.Status == AuditStatusEnum.Failure)
            {
                result.Message = "Unable to accept as the following result/s are not set: " + Environment.NewLine + result.Message;
            }

            return result;
        }
    }
}
