﻿using System;
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
        private string m_MothersHeightFeet;
        private string m_MothersHeightInches;
        private string m_MothersWeight;
        private string m_MothersBloodVolume;
        private string m_FetalBleed;
        private string m_FetalBleedReferenceRange;
        private string m_RhImmuneGlobulin;
        private string m_ReportComment;
        private string m_ASRComment;


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
                    CalculateFetalBleed();
                    NotifyPropertyChanged("HbFPercent");
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
        public string FetalBleedReferenceRange
        {
            get { return this.m_FetalBleedReferenceRange; }
            set
            {
                if (this.m_FetalBleedReferenceRange != value)
                {
                    this.m_FetalBleedReferenceRange = value;
                    NotifyPropertyChanged("FetalBleedReferenceRange");
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
                    this.CalculateFetalBleed();
                    NotifyPropertyChanged("MothersBloodVolume");
                }
            }
        }

        public string FetalBleed
        {
            get { return this.m_FetalBleed; }
            set
            {
                if (this.m_FetalBleed != value)
                {
                    this.m_FetalBleed = value;
                    CalculateRecommendedNumberOfVials();
                    NotifyPropertyChanged("FetalBleed");
                }
            }
        }

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

        private void CalculateMothersBloodVolume()
        {
            if (string.IsNullOrEmpty(this.m_MothersWeight) == true ||
                string.IsNullOrEmpty(this.m_MothersHeightFeet) == true) this.MothersBloodVolume = "5000";
            else
            {
                string inches = string.IsNullOrEmpty(this.m_MothersHeightInches) ? "0" : this.m_MothersHeightInches;
                string mothersWeightCleaned = this.CleanInputForParse(this.m_MothersWeight);
                string mothersHeightFeetCleaned = this.CleanInputForParse(this.m_MothersHeightFeet);
                string mothersHeightInchesCleaned = this.CleanInputForParse(inches);
                if (string.IsNullOrEmpty(mothersWeightCleaned) == false &&
                    string.IsNullOrEmpty(mothersHeightFeetCleaned) == false &&
                    string.IsNullOrEmpty(mothersHeightInchesCleaned) == false)
                {
                    double heightFeet = double.Parse(mothersHeightFeetCleaned);
                    double heightInches = double.Parse(mothersHeightInchesCleaned);
                    double totalInches = heightFeet * 12 + heightInches;
                    double weightInLbs = double.Parse(mothersWeightCleaned);

                    double bloodVolume = ((0.005835 * totalInches * totalInches * totalInches) + (15 * weightInLbs)) + 183;
                    this.MothersBloodVolume = Math.Round(bloodVolume).ToString();
                }
            }
        }

        private void CalculateFetalBleed()
        {
            if (string.IsNullOrEmpty(this.m_MothersWeight) == true ||
                string.IsNullOrEmpty(this.m_MothersHeightFeet) == true)
            {
                this.m_MothersBloodVolume = "5000";
                this.NotifyPropertyChanged("MothersBloodVolume");
            }

            if (string.IsNullOrEmpty(this.m_HbFPercent) == false)
            {
                string hbFPercentCleaned = this.CleanInputForParse(this.m_HbFPercent);
                string mothersBloodVolumeCleaned = this.CleanInputForParse(this.m_MothersBloodVolume);

                if (string.IsNullOrEmpty(hbFPercentCleaned) == false && string.IsNullOrEmpty(mothersBloodVolumeCleaned) == false)
                {
                    double percentFetalCells = double.Parse(hbFPercentCleaned);
                    double mothersBloodVolume = double.Parse(mothersBloodVolumeCleaned);
                    double fetalBleed = percentFetalCells * mothersBloodVolume * 0.01;
                    this.FetalBleed = Math.Round(fetalBleed, 2).ToString();
                }
            }
            else
            {
                this.FetalBleed = null;
            }
        }

        private void CalculateRecommendedNumberOfVials()
        {
            if (string.IsNullOrEmpty(this.m_FetalBleed) == false)
            {
                string fetalBleedCleaned = this.CleanInputForParse(this.m_FetalBleed);
                if (string.IsNullOrEmpty(fetalBleedCleaned) == false)
                {
                    double fetalbleed = double.Parse(fetalBleedCleaned);
                    double vials = fetalbleed / 30;
                    double partialValue = vials - Math.Truncate(vials);
                    int additionalVials = partialValue >= 0.5 ? 2 : 1;
                    int recommendedNumberOfVials = (int)Math.Truncate(vials) + additionalVials;
                    this.RhImmuneGlobulin = recommendedNumberOfVials.ToString();
                }
            }
            else
            {
                this.RhImmuneGlobulin = null;
            }
        }

        private string CleanInputForParse(string input)
        {
            string result = string.Empty;
            bool haveDecimal = false;
            foreach(char c in input)
            {
                if (char.IsNumber(c) == true)
                {
                    result = result + c;
                }
                else if(c == '.' && haveDecimal == false)
                {
                    haveDecimal = true;
                    result = result + c;
                }
            }

            double testValue = 0;
            bool canParse = double.TryParse(result, out testValue);

            return canParse == true ? result : string.Empty;
        }

        public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("Hb-F percent: " + this.m_HbFPercent);
            result.AppendLine();

            result.AppendLine("Fetal Bleed: " + this.m_FetalBleed);
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
                if (string.IsNullOrEmpty(this.m_FetalBleed) == true)
                {
                    result.Status = AuditStatusEnum.Failure;
                    result.Message += "Fetal Bleed" + Environment.NewLine;
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
