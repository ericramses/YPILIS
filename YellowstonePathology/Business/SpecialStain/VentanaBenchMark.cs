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
        private bool m_IsWetProtocol;
        private bool m_IsDualProtocol;

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
        public bool IsWetProtocol
        {
            get { return this.m_IsWetProtocol; }
            set
            {
                if (this.m_IsWetProtocol != value)
                {
                    this.m_IsWetProtocol = value;
                    this.NotifyPropertyChanged("IsWetProtocol");
                }
            }
        }

        [PersistentProperty(false)]
        public bool IsDualProtocol
        {
            get { return this.m_IsDualProtocol; }
            set
            {
                if (this.m_IsDualProtocol != value)
                {
                    this.m_IsDualProtocol = value;
                    this.NotifyPropertyChanged("IsDualProtocol");
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
