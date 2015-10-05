using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Text;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Monitor.Model
{
	public class MissingInformation : INotifyPropertyChanged
    {
		public event PropertyChangedEventHandler PropertyChanged;

        private string m_ReportNo;
        private string m_TestName;
        private DateTime m_OrderTime;
        private string m_FirstCallComment;
        private string m_SecondCallComment;
        private Nullable<DateTime> m_ExpectedFinalTime;
        

        public MissingInformation()
        {

		}

        [PersistentProperty()]
        public string ReportNo
        {
            get { return this.m_ReportNo; }
            set
            {
                if (this.m_ReportNo != value)
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
        public DateTime OrderTime
        {
            get { return this.m_OrderTime; }
            set
            {
                if (this.m_OrderTime != value)
                {
                    this.m_OrderTime = value;
                    this.NotifyPropertyChanged("OrderTime");
                }
            }
        }

        [PersistentProperty()]
        public string FirstCallComment
        {
            get { return this.m_FirstCallComment; }
            set
            {
                if (this.m_FirstCallComment != value)
                {
                    this.m_FirstCallComment = value;
                    this.NotifyPropertyChanged("FirstCallComment");
                }
            }
        }

        [PersistentProperty()]
        public string SecondCallComment
        {
            get { return this.m_SecondCallComment; }
            set
            {
                if (this.m_SecondCallComment != value)
                {
                    this.m_SecondCallComment = value;
                    this.NotifyPropertyChanged("SecondCallComment");
                }
            }
        }

        [PersistentProperty()]
        public Nullable<DateTime> ExpectedFinalTime
        {
            get { return this.m_ExpectedFinalTime; }
            set
            {
                if (this.m_ExpectedFinalTime != value)
                {
                    this.m_ExpectedFinalTime = value;
                    this.NotifyPropertyChanged("ExpectedFinalTime");
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
