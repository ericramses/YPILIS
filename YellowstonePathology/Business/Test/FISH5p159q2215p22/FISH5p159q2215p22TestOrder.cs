﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.FISH5p159q2215p22
{
    [PersistentClass("tblFISH5p159q2215p22TestOrder", "tblPanelSetOrder", "YPIDATA")]
    public class FISH5p159q2215p22TestOrder : YellowstonePathology.Business.Test.PanelSetOrder
    {
        protected string ReportMethod = "Method needs to be set.";
        protected string References = "References need to be set.";
        protected string TestDevelopment = "The performance characteristics of this test have been determined by NeoGenomics Laboratories.  " +
            "This test has not been approved by the FDA. The FDA has determined such clearance or approval is not necessary.  This " +
            "laboratory is CLIA certified to perform high complexity clinical testing.";

        private string m_Result;
        private string m_Interpretation;
        private string m_Method;
        private string m_ReportDisclaimer;
        private string m_ProbeComment;

        public FISH5p159q2215p22TestOrder()
        {
        }

        public FISH5p159q2215p22TestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
            bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{
            this.Method = ReportMethod;
            this.ReportReferences = References;

            StringBuilder disclaimer = new StringBuilder();
            disclaimer.AppendLine(this.GetLocationPerformedComment() + Environment.NewLine);
            disclaimer.AppendLine(TestDevelopment);
            this.ReportDisclaimer = disclaimer.ToString();
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
        [PersistentDataColumnProperty(true, "1000", "null", "varchar")]
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
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
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

        [PersistentProperty()]
        public string ProbeComment
        {
            get { return this.m_ProbeComment; }
            set
            {
                if (this.m_ProbeComment != value)
                {
                    this.m_ProbeComment = value;
                    this.NotifyPropertyChanged("ProbeComment");
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

            return result.ToString();
        }
    }
}
