using System;
using System.Collections.Generic;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.SpecialStain
{
    [PersistentClass("tblVentanaBenchMark", true, "YPIDATA")]
    public class VentanaBenchMark : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int m_BarcodeNumber;
        private string m_StainerType;
        private string m_StainName;
        private string m_Procedure;
        private string m_ProtocolName;
        private string m_YPITestId;
        private string m_StainModifier;
        private bool m_UseWetProtocol;
        private bool m_UseBrownProtocol;

        public VentanaBenchMark()
        { }

        [PersistentPrimaryKeyProperty(false)]
        public int BarcodeNumber
        {
            get { return this.m_BarcodeNumber; }
            set
            {
                if(this.m_BarcodeNumber != value)
                {
                    this.m_BarcodeNumber = value;
                    this.NotifyPropertyChanged("BarcodeNumber");
                }
            }
        }

        [PersistentProperty(false)]
        public string StainerType
        {
            get { return this.m_StainerType; }
            set
            {
                if (this.m_StainerType != value)
                {
                    this.m_StainerType = value;
                    this.NotifyPropertyChanged("StainerType");
                }
            }
        }

        [PersistentProperty(false)]
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

        [PersistentProperty(false)]
        public string Procedure
        {
            get { return this.m_Procedure; }
            set
            {
                if (this.m_Procedure != value)
                {
                    this.m_Procedure = value;
                    this.NotifyPropertyChanged("Procedure");
                }
            }
        }

        [PersistentProperty(false)]
        public string ProtocolName
        {
            get { return this.m_ProtocolName; }
            set
            {
                if (this.m_ProtocolName != value)
                {
                    this.m_ProtocolName = value;
                    this.NotifyPropertyChanged("ProtocolName");
                }
            }
        }

        [PersistentProperty(false)]
        public string YPITestId
        {
            get { return this.m_YPITestId; }
            set
            {
                if (this.m_YPITestId != value)
                {
                    this.m_YPITestId = value;
                    this.NotifyPropertyChanged("YPITestId");
                }
            }
        }

        [PersistentProperty(false)]
        public string StainModifier
        {
            get { return this.m_StainModifier; }
            set
            {
                if (this.m_StainModifier != value)
                {
                    this.m_StainModifier = value;
                    this.NotifyPropertyChanged("StainModifier");
                }
            }
        }

        [PersistentProperty(false)]
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

        [PersistentProperty(false)]
        public bool UseBrownProtocol
        {
            get { return this.m_UseBrownProtocol; }
            set
            {
                if (this.m_UseBrownProtocol != value)
                {
                    this.m_UseBrownProtocol = value;
                    this.NotifyPropertyChanged("UseBrownProtocol");
                }
            }
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
