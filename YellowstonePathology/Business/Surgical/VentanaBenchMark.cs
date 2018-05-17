using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Surgical
{
    [PersistentClass("tblVentanaBenchMark", true, "YPIDATA")]
    public class VentanaBenchMark : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected string m_VentanaBenchMarkId;
        protected int m_BarcodeNumber;
        protected string m_StainerType;
        protected string m_StainName;
        //protected string m_Procedure;
        protected string m_ProtocolName;
        protected string m_YPITestId;
        protected bool m_IsWetProtocol;
        protected bool m_IsDualProtocol;        

        public VentanaBenchMark()
        {

        }

        [PersistentPrimaryKeyProperty(false)]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string VentanaBenchMarkId
        {
            get { return this.m_VentanaBenchMarkId; }
            set
            {
                if (this.m_VentanaBenchMarkId != value)
                {
                    this.m_VentanaBenchMarkId = value;
                    this.NotifyPropertyChanged("VentanaBenchMarkId");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "11", "null", "int")]
        public int BarcodeNumber
        {
            get { return this.m_BarcodeNumber; }
            set
            {
                if (this.m_BarcodeNumber != value)
                {
                    this.m_BarcodeNumber = value;
                    this.NotifyPropertyChanged("BarcodeNumber");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "100", "null", "varchar")]
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

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "100", "null", "varchar")]
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

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "100", "null", "varchar")]
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

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "100", "null", "varchar")]
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

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "0", "tinyint")]
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

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "0", "tinyint")]
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
