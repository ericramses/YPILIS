using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.RASRAFPanel
{
    [PersistentClass("tblRASRAFPanelTestOrder", "tblPanelSetOrder", "YPIDATA")]
    public class RASRAFPanelTestOrder : YellowstonePathology.Business.Test.PanelSetOrder
    {
        private string m_KRASResult;
        private string m_NRASResult;
        private string m_HRASResult;
        private string m_BRAFResult;
        private string m_KRASMutationName;
        private string m_KRASAlternateNucleotideMutationName;
        private string m_KRASConsequence;
        private string m_KRASAlleleFrequency;
        private string m_KRASReadDepth;
        private string m_KRASPredictedEffectOnProtein;
        private string m_BRAFMutationName;
        private string m_BRAFAlternateNucleotideMutationName;
        private string m_BRAFConsequence;
        private string m_BRAFAlleleFrequency;
        private string m_BRAFReadDepth;
        private string m_BRAFPredictedEffectOnProtein;
        private string m_Method;
        private string m_References;
        private string m_ReportDisclaimer;

        public RASRAFPanelTestOrder()
        {

        }

        public RASRAFPanelTestOrder(string masterAccessionNo, string reportNo, string objectId,
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
            bool distribute,
            YellowstonePathology.Business.User.SystemIdentity systemIdentity)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute, systemIdentity)
		{
        }

        [PersistentProperty()]
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
        public string References
        {
            get { return this.m_References; }
            set
            {
                if (this.m_References != value)
                {
                    this.m_References = value;
                    this.NotifyPropertyChanged("References");
                }
            }
        }

        [PersistentProperty()]
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
        public string KRASResult
        {
            get { return this.m_KRASResult; }
            set
            {
                if (this.m_KRASResult != value)
                {
                    this.m_KRASResult = value;
                    this.NotifyPropertyChanged("KRASResult");
                }
            }
        }

        [PersistentProperty()]
        public string NRASResult
        {
            get { return this.m_NRASResult; }
            set
            {
                if (this.m_NRASResult != value)
                {
                    this.m_NRASResult = value;
                    this.NotifyPropertyChanged("NRASResult");
                }
            }
        }

        [PersistentProperty()]
        public string HRASResult
        {
            get { return this.m_HRASResult; }
            set
            {
                if (this.m_HRASResult != value)
                {
                    this.m_HRASResult = value;
                    this.NotifyPropertyChanged("HRASResult");
                }
            }
        }

        [PersistentProperty()]
        public string BRAFResult
        {
            get { return this.m_BRAFResult; }
            set
            {
                if (this.m_BRAFResult != value)
                {
                    this.m_BRAFResult = value;
                    this.NotifyPropertyChanged("BRAFResult");
                }
            }
        }

        [PersistentProperty()]
        public string KRASMutationName
        {
            get { return this.m_KRASMutationName; }
            set
            {
                if (this.m_KRASMutationName != value)
                {
                    this.m_KRASMutationName = value;
                    this.NotifyPropertyChanged("KRASMutationName");
                }
            }
        }

        [PersistentProperty()]
        public string KRASAlternateNucleotideMutationName
        {
            get { return this.m_KRASAlternateNucleotideMutationName; }
            set
            {
                if (this.m_KRASAlternateNucleotideMutationName != value)
                {
                    this.m_KRASAlternateNucleotideMutationName = value;
                    this.NotifyPropertyChanged("KRASAlternateNucleotideMutationName");
                }
            }
        }

        [PersistentProperty()]
        public string KRASConsequence
        {
            get { return this.m_KRASConsequence; }
            set
            {
                if (this.m_KRASConsequence != value)
                {
                    this.m_KRASConsequence = value;
                    this.NotifyPropertyChanged("KRASConsequence");
                }
            }
        }

        [PersistentProperty()]
        public string KRASAlleleFrequency
        {
            get { return this.m_KRASAlleleFrequency; }
            set
            {
                if (this.m_KRASAlleleFrequency != value)
                {
                    this.m_KRASAlleleFrequency = value;
                    this.NotifyPropertyChanged("KRASAlleleFrequency");
                }
            }
        }

        [PersistentProperty()]
        public string KRASReadDepth
        {
            get { return this.m_KRASReadDepth; }
            set
            {
                if (this.m_KRASReadDepth != value)
                {
                    this.m_KRASReadDepth = value;
                    this.NotifyPropertyChanged("KRASReadDepth");
                }
            }
        }

        [PersistentProperty()]
        public string KRASPredictedEffectOnProtein
        {
            get { return this.m_KRASPredictedEffectOnProtein; }
            set
            {
                if (this.m_KRASPredictedEffectOnProtein != value)
                {
                    this.m_KRASPredictedEffectOnProtein = value;
                    this.NotifyPropertyChanged("KRASPredictedEffectOnProtein");
                }
            }
        }

        [PersistentProperty()]
        public string BRAFMutationName
        {
            get { return this.m_BRAFMutationName; }
            set
            {
                if (this.m_BRAFMutationName != value)
                {
                    this.m_BRAFMutationName = value;
                    this.NotifyPropertyChanged("BRAFMutationName");
                }
            }
        }

        [PersistentProperty()]
        public string BRAFAlternateNucleotideMutationName
        {
            get { return this.m_BRAFAlternateNucleotideMutationName; }
            set
            {
                if (this.m_BRAFAlternateNucleotideMutationName != value)
                {
                    this.m_BRAFAlternateNucleotideMutationName = value;
                    this.NotifyPropertyChanged("BRAFAlternateNucleotideMutationName");
                }
            }
        }

        [PersistentProperty()]
        public string BRAFConsequence
        {
            get { return this.m_BRAFConsequence; }
            set
            {
                if (this.m_BRAFConsequence != value)
                {
                    this.m_BRAFConsequence = value;
                    this.NotifyPropertyChanged("BRAFConsequence");
                }
            }
        }

        [PersistentProperty()]
        public string BRAFAlleleFrequency
        {
            get { return this.m_BRAFAlleleFrequency; }
            set
            {
                if (this.m_BRAFAlleleFrequency != value)
                {
                    this.m_BRAFAlleleFrequency = value;
                    this.NotifyPropertyChanged("BRAFAlleleFrequency");
                }
            }
        }

        [PersistentProperty()]
        public string BRAFReadDepth
        {
            get { return this.m_BRAFReadDepth; }
            set
            {
                if (this.m_BRAFReadDepth != value)
                {
                    this.m_BRAFReadDepth = value;
                    this.NotifyPropertyChanged("BRAFReadDepth");
                }
            }
        }

        [PersistentProperty()]
        public string BRAFPredictedEffectOnProtein
        {
            get { return this.m_BRAFPredictedEffectOnProtein; }
            set
            {
                if (this.m_BRAFPredictedEffectOnProtein != value)
                {
                    this.m_BRAFPredictedEffectOnProtein = value;
                    this.NotifyPropertyChanged("BRAFPredictedEffectOnProtein");
                }
            }
        }
        	
        public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            StringBuilder result = new StringBuilder();
            result.Append("BRAF Result: ");
            result.AppendLine(this.m_BRAFResult);
            result.Append("KRAS Result: ");
            result.AppendLine(this.m_KRASResult);
            result.Append("NRAS Result: ");
            result.AppendLine(this.m_NRASResult);
            result.Append("HRAS Result: ");
            result.AppendLine(this.m_HRASResult);
            return result.ToString();
        }
    }
}
