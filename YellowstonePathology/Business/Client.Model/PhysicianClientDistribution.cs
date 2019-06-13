using System;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Client.Model
{
	[PersistentClass("tblPhysicianClientDistribution", "YPIDATA")]
	public class PhysicianClientDistribution
	{
		private string m_ObjectId;
		private int m_PhysicianClientDistributionID;
		private string m_PhysicianClientID;
		private string m_DistributionID;
        private string m_DistributionType;

        public PhysicianClientDistribution()
        {

        }

		public PhysicianClientDistribution(string objectId, string physicianClientID, string distributionID, string distributionType)
		{
			this.m_ObjectId = objectId;
			this.m_PhysicianClientID = physicianClientID;
			this.m_DistributionID = distributionID;
            this.m_DistributionType = distributionType;
		}

		[PersistentDocumentIdProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string ObjectId
		{
			get { return this.m_ObjectId; }
			set { this.m_ObjectId = value; }
		}

		[PersistentPrimaryKeyProperty(true)]
		[PersistentDataColumnProperty(false, "11", "null", "int")]
		public int PhysicianClientDistributionID
		{
			get { return this.m_PhysicianClientDistributionID; }
			set { this.m_PhysicianClientDistributionID = value; }
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "nvarchar")]
		public string PhysicianClientID
		{
			get { return this.m_PhysicianClientID; }
			set { this.m_PhysicianClientID = value; }
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "nvarchar")]
		public string DistributionID
		{
			get { return this.m_DistributionID; }
			set { this.m_DistributionID = value; }
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "nvarchar")]
        public string DistributionType
        {
            get { return this.m_DistributionType; }
            set { this.m_DistributionType = value; }
        }
    }
}
