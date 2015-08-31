using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.YpiConnect.Contract.Domain
{
    [Table(Name = "tblAccessionOrder")]
    public class AccessionOrder
    {
        string m_MasterAccessionNo;
        DateTime m_AccessionDate = DateTime.Today;
        string m_PLastName = null;
        string m_PFirstName = null;
        string m_PMiddleInitial = null;
        string m_PSex;
        Nullable<DateTime> m_PBirthdate;
        DateTime m_CollectionDate;
        string m_PCAN;
        string m_PSSN;
        int m_ClientId;
		int m_PhysicianId;
        string m_ClientName;
        string m_PhysicianName;        

        private EntitySet<PanelSetOrder> m_PanelSetOrders;	

        public AccessionOrder()
        {
            this.m_PanelSetOrders = new EntitySet<PanelSetOrder>();    
        }

        [Association(Storage = "m_PanelSetOrders", ThisKey = "MasterAccessionNo", OtherKey = "MasterAccessionNo")]
        public EntitySet<PanelSetOrder> PanelSetOrders
        {
            get { return this.m_PanelSetOrders; }
            set { this.m_PanelSetOrders.Assign(value); }
        }        

        [Column(Name = "MasterAccessionNo", Storage = "m_MasterAccessionNo", IsPrimaryKey = true, CanBeNull = false)]
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

        [Column(Name = "AccessionDate", Storage = "m_AccessionDate", CanBeNull = false)]
        public DateTime AccessionDate
        {
            get { return this.m_AccessionDate; }
            set
            {
                if (this.m_AccessionDate != value)
                {
                    this.m_AccessionDate = value;
                }
            }
        }

        [Column(Name = "CollectionDate", Storage = "m_CollectionDate", CanBeNull = false)]
        public DateTime CollectionDate
        {
            get { return this.m_CollectionDate; }
            set
            {
                if (this.m_CollectionDate != value)
                {
                    this.m_CollectionDate = value;
                }
            }
        }

        [Column(Name = "PLastName", Storage = "m_PLastName", CanBeNull = true)]
        public string PLastName
        {
            get { return this.m_PLastName; }
            set
            {
                if (this.m_PLastName != value)
                {
                    this.m_PLastName = value;                    
                }
            }
        }

        [Column(Name = "PFirstName", Storage = "m_PFirstName", CanBeNull = true)]
        public string PFirstName
        {
            get { return this.m_PFirstName; }
            set
            {
                if (this.m_PFirstName != value)
                {
                    this.m_PFirstName = value;                    
                }
            }
        }

        [Column(Name = "PMiddleInitial", Storage = "m_PMiddleInitial", CanBeNull = true)]
        public string PMiddleInitial
        {
            get { return this.m_PMiddleInitial; }
            set
            {
                if (this.m_PMiddleInitial != value)
                {
                    this.m_PMiddleInitial = value;                    
                }
            }
        }

        [Column(Name = "PBirthdate", Storage = "m_PBirthdate", CanBeNull = true)]
        public Nullable<DateTime> PBirthdate
        {
            get { return this.m_PBirthdate; }
            set
            {
                if (this.m_PBirthdate != value)
                {
                    this.m_PBirthdate = value;
                }
            }
        }

        [Column(Name = "PCAN", Storage = "m_PCAN")]
        public string PCAN
        {
            get { return this.m_PCAN; }
            set
            {
                if (this.m_PCAN != value)
                {
                    this.m_PCAN = value;
                }
            }
        }

        [Column(Name = "PSSN", Storage = "m_PSSN")]
        public string PSSN
        {
            get { return this.m_PSSN; }
            set
            {
                if (this.m_PSSN != value)
                {
                    this.m_PSSN = value;
                }
            }
        }

        [Column(Name = "PSex", Storage = "m_PSex")]
        public string PSex
        {
            get { return this.m_PSex; }
            set
            {
                if (this.m_PSex != value)
                {
                    this.m_PSex = value;
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

        [Column(Name = "PhysicianId", Storage = "m_PhysicianId")]
		public int PhysicianId
        {
            get { return this.m_PhysicianId; }
            set
            {
                if (this.m_PhysicianId != value)
                {
                    this.m_PhysicianId = value;                    
                }
            }
        }

        [Column(Name = "ClientName", Storage = "m_ClientName")]
        public string ClientName
        {
            get { return this.m_ClientName; }
            set
            {
                if (this.m_ClientName != value)
                {
                    this.m_ClientName = value;                    
                }
            }
        }

        [Column(Name = "PhysicianName", Storage = "m_PhysicianName")]
        public string PhysicianName
        {
            get { return this.m_PhysicianName; }
            set
            {
                if (this.m_PhysicianName != value)
                {
                    this.m_PhysicianName = value;
                }
            }
        }        
    }
}
