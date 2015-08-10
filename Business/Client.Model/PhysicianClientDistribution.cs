using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Text;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Xml.Serialization;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Client.Model
{
	[PersistentClass("tblPhysicianClientDistribution", "YPIDATA")]
	public class PhysicianClientDistribution
	{
		private string m_ObjectId;
		private int m_PhysicianClientDistributionID;
		private int m_PhysicianClientID;
		private int m_DistributionID;

        public PhysicianClientDistribution()
        {

        }

		public PhysicianClientDistribution(string objectId, int physicianClientID, int distributionID)
		{
			this.m_ObjectId = objectId;
			this.m_PhysicianClientID = physicianClientID;
			this.m_DistributionID = distributionID;
		}

		[PersistentDocumentIdProperty()]
		public string ObjectId
		{
			get { return this.m_ObjectId; }
			set { this.m_ObjectId = value; }
		}

		[PersistentPrimaryKeyProperty(true)]
		public int PhysicianClientDistributionID
		{
			get { return this.m_PhysicianClientDistributionID; }
			set { this.m_PhysicianClientDistributionID = value; }
		}

		[PersistentProperty()]
		public int PhysicianClientID
		{
			get { return this.m_PhysicianClientID; }
			set { this.m_PhysicianClientID = value; }
		}

		[PersistentProperty()]
		public int DistributionID
		{
			get { return this.m_DistributionID; }
			set { this.m_DistributionID = value; }
		}

        public void WriteProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyWriter propertyWriter)
        {
			this.m_PhysicianClientDistributionID = propertyWriter.WriteInt("PhysicianClientDistributionID");
			this.m_PhysicianClientID = propertyWriter.WriteInt("PhysicianClientID");
			this.m_DistributionID = propertyWriter.WriteInt("DistributionID");
			this.m_ObjectId = propertyWriter.WriteString("ObjectId");
		}

        public void ReadProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyReader propertyReader)
        {
			propertyReader.ReadInt("PhysicianClientDistributionID", PhysicianClientDistributionID);
			propertyReader.ReadInt("PhysicianClientID", PhysicianClientID);
			propertyReader.ReadInt("DistributionID", DistributionID);
			propertyReader.ReadString("ObjectId", ObjectId);
		}
	}
}
