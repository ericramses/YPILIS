using System;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Surgical
{
    public class AssignmentScan :INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string m_ReportNo;
        private string m_SlideOrderId;
        private string m_AssignedTo;

        public AssignmentScan()
        {
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        [PersistentProperty()]
        public string ReportNo
        {
            get { return this.m_ReportNo; }
            set
            {
                if(this.m_ReportNo != value)
                {
                    this.m_ReportNo = value;
                    this.NotifyPropertyChanged("ReportNo");
                }
            }
        }

        [PersistentProperty()]
        public string SlideOrderId
        {
            get { return this.m_SlideOrderId; }
            set
            {
                if (this.m_SlideOrderId != value)
                {
                    this.m_SlideOrderId = value;
                    this.NotifyPropertyChanged("SlideOrderId");
                }
            }
        }

        public string AssignedTo
        {
            get { return this.m_AssignedTo; }
            set
            {
                if (this.m_AssignedTo != value)
                {
                    this.m_AssignedTo = value;
                    this.NotifyPropertyChanged("AssignedTo");
                }
            }
        }
    }
}
