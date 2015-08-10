using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace YellowstonePathology.YpiConnect.Client.OrderEntry
{
    public class SvhPatientId : INotifyPropertyChanged
    {
        public delegate void PropertyChangedNotificationHandler(String info);
        public event PropertyChangedEventHandler PropertyChanged;
        
        string m_MedicalRecordNumber;
        string m_AccountNumber;

        public SvhPatientId()
        {

        }        

        public string MedicalRecordNumber
        {
            get { return this.m_MedicalRecordNumber; }
            set
            {
                if (this.m_MedicalRecordNumber != value)
                {
                    this.m_MedicalRecordNumber = value;
                    this.NotifyPropertyChanged("MedicalRecordNumber");
                }
            }
        }

        public string AccountNumber
        {
            get { return this.m_AccountNumber; }
            set
            {
                if (this.m_AccountNumber != value)
                {
                    this.m_AccountNumber = value;
                    this.NotifyPropertyChanged("AccountNumber");
                }
            }
        }

        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
