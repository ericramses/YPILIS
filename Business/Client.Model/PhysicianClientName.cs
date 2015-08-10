using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Client.Model
{
	public class PhysicianClientName
	{
		private int m_PhysicianClientId;
		private int m_ClientId;
		private int m_PhysicianId;
		private string m_ClientName;
		private string m_FirstName;
		private string m_LastName;
		private string m_Telephone;
		private string m_Fax;

		public PhysicianClientName()
		{

		}

		[PersistentProperty]
		public int PhysicianClientId
		{
			get { return this.m_PhysicianClientId; }
			set { this.m_PhysicianClientId = value; }
		}

		[PersistentProperty]
		public int ClientId
		{
			get { return this.m_ClientId; }
			set { this.m_ClientId = value; }
		}

		[PersistentProperty]
		public int PhysicianId
		{
			get { return this.m_PhysicianId; }
			set { this.m_PhysicianId = value; }
		}

		[PersistentProperty]
		public string ClientName
		{
			get { return this.m_ClientName; }
			set { this.m_ClientName = value; }
		}

		[PersistentProperty]
		public string FirstName
		{
			get { return this.m_FirstName; }
			set { this.m_FirstName = value; }
		}

		[PersistentProperty]
		public string LastName
		{
			get { return this.m_LastName; }
			set { this.m_LastName = value; }
		}

		[PersistentProperty]
		public string Telephone
		{
			get { return this.m_Telephone; }
			set { this.m_Telephone = value; }
		}

		[PersistentProperty]
		public string Fax
		{
			get { return this.m_Fax; }
			set { this.m_Fax = value; }
		}

		public string PhysicianName
		{
			get
			{
				string result = string.Empty;
				if (string.IsNullOrEmpty(this.m_FirstName) == false) result += this.m_FirstName + ' ';
				result += this.m_LastName;
				return result;
			}
		}

		public void WriteProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyWriter propertyWriter)
		{
			this.m_PhysicianClientId = propertyWriter.WriteInt("PhysicianClientId");
			this.m_ClientId = propertyWriter.WriteInt("ClientId");
			this.m_PhysicianId = propertyWriter.WriteInt("PhysicianId");
			this.m_ClientName = propertyWriter.WriteString("ClientName");
			this.m_FirstName = propertyWriter.WriteString("FirstName");
			this.m_LastName = propertyWriter.WriteString("LastName");
			this.m_Telephone = propertyWriter.WriteString("Telephone");
			this.m_Fax = propertyWriter.WriteString("Fax");
		}
	}
}
