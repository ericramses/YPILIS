using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Text;
using System.Xml.Linq;

namespace ClientWebServices.Domain
{
    [Table(Name = "tblPanelSetOrder")]
	public class PanelSetOrder 
	{
		private int m_MasterAccessionNo;
		private string m_ReportNo;
		private int m_PanelSetId;
		private Nullable<DateTime> m_FinalDate = null;
		private Nullable<DateTime> m_FinalTime = null;
		private bool m_Final = false;
		private Nullable<DateTime> m_OrderDate = null;
		private Nullable<DateTime> m_OrderTime = null;		
		private string m_PanelSetName;
		private int m_FinaledById;
		private System.Nullable<int> m_OrderedById;				
		//private int m_AssignedToId;

        private EntitySet<ReportDistributionLog> m_ReportDistributionLog;	

        public PanelSetOrder()
        {
            this.m_ReportDistributionLog = new EntitySet<ReportDistributionLog>();
        }

        [Association(Storage = "m_ReportDistributionLog", ThisKey = "ReportNo", OtherKey = "ReportNo")]
        public EntitySet<ReportDistributionLog> ReportDistributionLog
        {
            get { return this.m_ReportDistributionLog; }
            set { this.m_ReportDistributionLog.Assign(value); }
        }       

		[Column(Name = "MasterAccessionNo", Storage = "m_MasterAccessionNo")]
		public int MasterAccessionNo
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
				if(this.m_ReportNo != value)
				{
					this.m_ReportNo = value;					
				}
            }
        }

        [Column(Name = "PanelSetId", Storage = "m_PanelSetId")]
        public int PanelSetId
        {
            get { return this.m_PanelSetId; }
            set
            {
                if (this.m_PanelSetId != value)
                {
                    this.m_PanelSetId = value;                    
                }
            }
        }

        [Column(Name = "FinalDate", Storage = "m_FinalDate")]
        public Nullable<DateTime> FinalDate
        {
            get { return this.m_FinalDate; }
            set
            {
                if (this.m_FinalDate != value)
                {
                    this.m_FinalDate = value;                    
                }
            }
        }

        [Column(Name = "FinalTime", Storage = "m_FinalTime")]
        public Nullable<DateTime> FinalTime
        {
            get { return this.m_FinalTime; }
            set
            {
                if (this.m_FinalTime != value)
                {
                    this.m_FinalTime = value;                    
                }
            }
        }

        [Column(Name = "Final", Storage = "m_Final")]
        public bool Final
        {
            get { return this.m_Final; }
            set
            {
                if (this.m_Final != value)
                {
                    this.m_Final = value;                    
                }
            }
        }

		[Column(Name = "OrderDate", Storage = "m_OrderDate")]
		public Nullable<DateTime> OrderDate
		{
			get { return this.m_OrderDate; }
			set
			{
				if (this.m_OrderDate != value)
				{
					this.m_OrderDate = value;					
				}
			}
		}

		[Column(Name = "OrderTime", Storage = "m_OrderTime")]
		public Nullable<DateTime> OrderTime
		{
			get { return this.m_OrderTime; }
			set
			{
				if (this.m_OrderTime != value)
				{
					this.m_OrderTime = value;					
				}
			}
		}		

		[Column(Storage = "m_PanelSetName", DbType = "VarChar(100)")]
		public string PanelSetName
		{
			get
			{
				return this.m_PanelSetName;
			}
			set
			{
				if ((this.m_PanelSetName != value))
				{
					this.m_PanelSetName = value;		
				}
			}
		}

		[Column(Storage = "m_FinaledById", DbType = "Int NOT NULL")]
		public int FinaledById
		{
			get
			{
				return this.m_FinaledById;
			}
			set
			{
				if ((this.m_FinaledById != value))
				{
					this.m_FinaledById = value;					
				}
			}
		}


		[Column(Storage = "m_OrderedById", DbType = "Int")]
		public System.Nullable<int> OrderedById
		{
			get
			{
				return this.m_OrderedById;
			}
			set
			{
				if ((this.m_OrderedById != value))
				{
					this.m_OrderedById = value;					
				}
			}
		}				
	}
}