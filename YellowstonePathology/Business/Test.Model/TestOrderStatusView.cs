using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.Model
{
    public class TestOrderStatusView : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string m_ReportNo;
        private string m_TestName;
        private string m_TestStatus;
        private DateTime? m_TestStatusUpdateTime;

        public TestOrderStatusView() { }

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
        public string TestName
        {
            get { return this.m_TestName; }
            set
            {
                if (this.m_TestName != value)
                {
                    this.m_TestName = value;
                    this.NotifyPropertyChanged("TestName");
                }
            }
        }

        [PersistentProperty()]
        public string TestStatus
        {
            get { return this.m_TestStatus; }
            set
            {
                if (this.m_TestStatus != value)
                {
                    this.m_TestStatus = value;
                    this.NotifyPropertyChanged("TestStatus");
                }
            }
        }

        [PersistentProperty()]
        public DateTime? TestStatusUpdateTime
        {
            get { return this.m_TestStatusUpdateTime; }
            set
            {
                if (this.m_TestStatusUpdateTime != value)
                {
                    this.m_TestStatusUpdateTime = value;
                    this.NotifyPropertyChanged("TestStatusUpdateTime");
                }
            }
        }
    }
}
