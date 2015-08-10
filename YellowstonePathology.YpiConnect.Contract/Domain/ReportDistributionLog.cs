using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.YpiConnect.Contract.Domain
{
    [Table(Name = "tblReportDistributionLog")]
    public class ReportDistributionLog
    {
		private string m_MasterAccessionNo;
        private string m_ReportNo;
        private int m_ClientId;
        private bool m_CaseDistributed;
        private Nullable<DateTime> m_DateDistributed;
        private Nullable<DateTime> m_DateEntered;

        public ReportDistributionLog()
        {

        }

        [Column(Name = "MasterAccessionNo", Storage = "m_MasterAccessionNo")]
		public string MasterAccessionNo
        {
            get { return this.m_MasterAccessionNo; }
            set
            {
                if (this.m_MasterAccessionNo != value)
                {
                    this.m_MasterAccessionNo = value;
                }
            }
        }

        [Column(Name = "ReportNo", Storage = "m_ReportNo", IsPrimaryKey = true, AutoSync = AutoSync.Default)]
        public string ReportNo
        {
            get { return this.m_ReportNo; }
            set
            {
                if (this.m_ReportNo != value)
                {
                    this.m_ReportNo = value;
                }
            }
        }

        [Column(Name = "ClientId", Storage = "m_ClientId")]
        public int ClientId
        {
            get { return this.m_ClientId; }
            set
            {
                if (this.m_ClientId != value)
                {
                    this.m_ClientId = value;
                }
            }
        }

        [Column(Name = "CaseDistributed", Storage = "m_CaseDistributed")]
        public bool CaseDistributed
        {
            get { return this.m_CaseDistributed; }
            set
            {
                if (this.m_CaseDistributed != value)
                {
                    this.m_CaseDistributed = value;
                }
            }
        }

        [Column(Name = "DateDistributed", Storage = "m_DateDistributed")]
        public Nullable<DateTime> DateDistributed
        {
            get { return this.m_DateDistributed; }
            set
            {
                if (this.m_DateDistributed != value)
                {
                    this.m_DateDistributed = value;
                }
            }
        }

        [Column(Name = "DateEntered", Storage = "m_DateEntered")]
        public Nullable<DateTime> DateEntered
        {
            get { return this.m_DateEntered; }
            set
            {
                if (this.m_DateEntered != value)
                {
                    this.m_DateEntered = value;
                }
            }
        }

    }
}
