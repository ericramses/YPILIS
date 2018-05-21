using System;
using System.ComponentModel;

namespace YellowstonePathology.Business.Test.Model
{
    public class Stain : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public const string DualStainBase = "DualStain";
        public const string UnknownBase = "Unknown";
        public const string CytochemicalForMicroorganismsBase = "CytochemicalForMicroorganisms";
        public const string CytochemicalBase = "Cytochemical";
        public const string ImmunoHistochemistryBase = "IHC";
        public const string GradedBase = "GradedStain";
        public const string SpecialStainBase = "SpecialStain";

        private string m_StainId;
        private string m_StainType;
        private string m_OrderComment;
        private string m_TestId;
        private string m_StainName;
        private string m_StainAbbreviation;
        private string m_AliquotType;
        private string m_DefaultResult;
        private string m_HistologyDisplayString;
        private int m_StainResultGroupId;
        private bool m_IsBillable;
        private bool m_HasGCode;
        private bool m_IsDualOrder;
        private bool m_HasCptCodeLevels;
        private bool m_Active;
        private bool m_NeedsAcknowledgement;
        private bool m_UseWetProtocol;
        private bool m_PerformedByHand;
        private bool m_RequestForAdditionalReport;


        public Stain() { }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public string StainId
        {
            get { return this.m_StainId; }
            set
            {
                if (this.m_StainId != value)
                {
                    this.m_StainId = value;
                    this.NotifyPropertyChanged("StainId");
                }
            }
        }

        public string StainType
        {
            get { return this.m_StainType; }
            set
            {
                if (this.m_StainType != value)
                {
                    this.m_StainType = value;
                    this.NotifyPropertyChanged("StainType");
                }
            }
        }

        public string OrderComment
        {
            get { return this.m_OrderComment; }
            set
            {
                if (this.m_OrderComment != value)
                {
                    this.m_OrderComment = value;
                    this.NotifyPropertyChanged("OrderComment");
                }
            }
        }

        public string TestId
        {
            get { return this.m_TestId; }
            set
            {
                if (this.m_TestId != value)
                {
                    this.m_TestId = value;
                    this.NotifyPropertyChanged("TestId");
                }
            }
        }

        public string StainName
        {
            get { return this.m_StainName; }
            set
            {
                if (this.m_StainName != value)
                {
                    this.m_StainName = value;
                    this.NotifyPropertyChanged("StainName");
                }
            }
        }

        public string StainAbbreviation
        {
            get { return this.m_StainAbbreviation; }
            set
            {
                if (this.m_StainAbbreviation != value)
                {
                    this.m_StainAbbreviation = value;
                    this.NotifyPropertyChanged("StainAbbreviation");
                }
            }
        }

        public string AliquotType
        {
            get { return this.m_AliquotType; }
            set
            {
                if (this.m_AliquotType != value)
                {
                    this.m_AliquotType = value;
                    this.NotifyPropertyChanged("AliquotType");
                }
            }
        }

        public string DefaultResult
        {
            get { return this.m_DefaultResult; }
            set
            {
                if (this.m_DefaultResult != value)
                {
                    this.m_DefaultResult = value;
                    this.NotifyPropertyChanged("DefaultResult");
                }
            }
        }

        public string HistologyDisplayString
        {
            get { return this.m_HistologyDisplayString; }
            set
            {
                if (this.m_HistologyDisplayString != value)
                {
                    this.m_HistologyDisplayString = value;
                    this.NotifyPropertyChanged("HistologyDisplayString");
                }
            }
        }

        public int StainResultGroupId
        {
            get { return this.m_StainResultGroupId; }
            set
            {
                if (this.m_StainResultGroupId != value)
                {
                    this.m_StainResultGroupId = value;
                    this.NotifyPropertyChanged("StainResultGroupId");
                }
            }
        }

        public bool IsBillable
        {
            get { return this.m_IsBillable; }
            set
            {
                if (this.m_IsBillable != value)
                {
                    this.m_IsBillable = value;
                    this.NotifyPropertyChanged("IsBillable");
                }
            }
        }

        public bool HasGCode
        {
            get { return this.m_HasGCode; }
            set
            {
                if (this.m_HasGCode != value)
                {
                    this.m_HasGCode = value;
                    this.NotifyPropertyChanged("HasGCode");
                }
            }
        }

        public bool IsDualOrder
        {
            get { return this.m_IsDualOrder; }
            set
            {
                if (this.m_IsDualOrder != value)
                {
                    this.m_IsDualOrder = value;
                    this.NotifyPropertyChanged("IsDualOrder");
                }
            }
        }

        public bool HasCptCodeLevels
        {
            get { return this.m_HasCptCodeLevels; }
            set
            {
                if (this.m_HasCptCodeLevels != value)
                {
                    this.m_HasCptCodeLevels = value;
                    this.NotifyPropertyChanged("HasCptCodeLevels");
                }
            }
        }

        public bool Active
        {
            get { return this.m_Active; }
            set
            {
                if (this.m_Active != value)
                {
                    this.m_Active = value;
                    this.NotifyPropertyChanged("Active");
                }
            }
        }

        public bool NeedsAcknowledgement
        {
            get { return this.m_NeedsAcknowledgement; }
            set
            {
                if (this.m_NeedsAcknowledgement != value)
                {
                    this.m_NeedsAcknowledgement = value;
                    this.NotifyPropertyChanged("NeedsAcknowledgement");
                }
            }
        }

        public bool UseWetProtocol
        {
            get { return this.m_UseWetProtocol; }
            set
            {
                if (this.m_UseWetProtocol != value)
                {
                    this.m_UseWetProtocol = value;
                    this.NotifyPropertyChanged("UseWetProtocol");
                }
            }
        }

        public bool PerformedByHand
        {
            get { return this.m_PerformedByHand; }
            set
            {
                if (this.m_PerformedByHand != value)
                {
                    this.m_PerformedByHand = value;
                    this.NotifyPropertyChanged("PerformedByHand");
                }
            }
        }

        public bool RequestForAdditionalReport
        {
            get { return this.m_RequestForAdditionalReport; }
            set
            {
                if (this.m_RequestForAdditionalReport != value)
                {
                    this.m_RequestForAdditionalReport = value;
                    this.NotifyPropertyChanged("RequestForAdditionalReport");
                }
            }
        }
    }
}
