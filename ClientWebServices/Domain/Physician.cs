using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace ClientWebServices.Domain
{   
	[Table (Name="tblPhysician")]    
	public class Physician 
	{
		int m_PhysicianID;		
		string m_MDFirstName;
		string m_MDLastName;
        string m_FullName;
		bool m_Active;		

		public Physician()
		{ }
                

		[Column(Name = "PhysicianID", Storage = "m_PhysicianID", IsPrimaryKey = true)]
		public int PhysicianID
		{
			get { return this.m_PhysicianID; }
			set { this.m_PhysicianID = value; }
		}

		[Column(Name = "MDFirstName", Storage = "m_MDFirstName")]
		public string MDFirstName
		{
			get { return this.m_MDFirstName; }
			set { this.m_MDFirstName = value; }
		}

		[Column(Name = "MDLastName", Storage = "m_MDLastName")]
		public string MDLastName
		{
			get { return this.m_MDLastName; }
			set { this.m_MDLastName = value; }
		}

        [Column(Name = "FullName", Storage = "m_FullName")]
        public string FullName
        {
            get { return this.m_FullName; }
            set { this.m_FullName = value; }
        }

		[Column(Name = "Active", Storage = "m_Active")]
		public bool Active
		{
			get { return this.m_Active; }
			set { this.m_Active = value; }
		}		
	}
}
