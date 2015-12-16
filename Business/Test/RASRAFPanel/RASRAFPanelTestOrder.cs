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
