using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.View
{
	public partial class RecentAccessionView
	{
		#region Fields
		private string m_ReportNo;
		private string m_MasterAccessionNo;
		private string m_PFirstName;
		private string m_PLastName;
		private DateTime m_AccessionTime;
		private string m_ClientName;
		private string m_PhysicianName;
		private Nullable<DateTime> m_CollectionTime;
		#endregion

		#region Properties
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

		public string MasterAccessionNo
		{
			get { return this.m_MasterAccessionNo; }
			set
			{
				if(this.m_MasterAccessionNo != value)
				{
					this.m_MasterAccessionNo = value;
					this.NotifyPropertyChanged("MasterAccessionNo");
				}
			}
		}

		public string PFirstName
		{
			get { return this.m_PFirstName; }
			set
			{
				if(this.m_PFirstName != value)
				{
					this.m_PFirstName = value;
					this.NotifyPropertyChanged("PFirstName");
				}
			}
		}

		public string PLastName
		{
			get { return this.m_PLastName; }
			set
			{
				if(this.m_PLastName != value)
				{
					this.m_PLastName = value;
					this.NotifyPropertyChanged("PLastName");
				}
			}
		}

		public DateTime AccessionTime
		{
			get { return this.m_AccessionTime; }
			set
			{
				if(this.m_AccessionTime != value)
				{
					this.m_AccessionTime = value;
					this.NotifyPropertyChanged("AccessionTime");
				}
			}
		}

		public string ClientName
		{
			get { return this.m_ClientName; }
			set
			{
				if(this.m_ClientName != value)
				{
					this.m_ClientName = value;
					this.NotifyPropertyChanged("ClientName");
				}
			}
		}

		public string PhysicianName
		{
			get { return this.m_PhysicianName; }
			set
			{
				if(this.m_PhysicianName != value)
				{
					this.m_PhysicianName = value;
					this.NotifyPropertyChanged("PhysicianName");
				}
			}
		}

		public Nullable<DateTime> CollectionTime
		{
			get { return this.m_CollectionTime; }
			set
			{
				if(this.m_CollectionTime != value)
				{
					this.m_CollectionTime = value;
					this.NotifyPropertyChanged("CollectionTime");
				}
			}
		}

		#endregion

		#region WritePropertiesMethod
		public void WriteProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyWriter propertyWriter)
		{
			this.m_ReportNo = propertyWriter.WriteString("ReportNo");
			this.m_MasterAccessionNo = propertyWriter.WriteString("MasterAccessionNo");
			this.m_PFirstName = propertyWriter.WriteString("PFirstName");
			this.m_PLastName = propertyWriter.WriteString("PLastName");
			this.m_AccessionTime = propertyWriter.WriteDateTime("AccessionTime");
			this.m_ClientName = propertyWriter.WriteString("ClientName");
			this.m_PhysicianName = propertyWriter.WriteString("PhysicianName");
			this.m_CollectionTime = propertyWriter.WriteNullableDateTime("CollectionTime");
		}
		#endregion
	}
}
