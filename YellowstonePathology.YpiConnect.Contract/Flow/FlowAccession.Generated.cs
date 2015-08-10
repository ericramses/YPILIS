using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace YellowstonePathology.YpiConnect.Contract.Flow
{
	public partial class FlowAccession
	{
		#region Serialization
		public void FromXml(XElement xml)
		{
			throw new NotImplementedException("FromXml not implemented in FlowAccession");
		}

		public XElement ToXml()
		{
			throw new NotImplementedException("ToXml not implemented in FlowAccession");
		}
		#endregion

		#region Fields
		//private string m_ReportNo;
		private string m_MasterAccessionNo;
		private Nullable<DateTime> m_CollectionDate;
		private string m_PFirstName;
		private string m_PLastName;
		private string m_PMiddleInitial;
		private string m_PSex;
		private string m_PSSN;
		private Nullable<DateTime> m_PBirthdate;
		//private string m_SpecimenType;
		//private bool m_Final;
		//private Nullable<DateTime> m_FinalDate;
		//private Nullable<DateTime> m_FinalTime;
		//private string m_PathologistSignature;
		//private int m_PathologistId;
		private string m_ClientName;
		private string m_PhysicianName;
		private Nullable<DateTime> m_AccessionDate;
		private Nullable<DateTime> m_AccessionTime;
		//private Nullable<DateTime> m_CollectionTime;
		private string m_SvhMedicalRecord;
		private string m_SvhAccount;
		private string m_PCAN;
		#endregion

		#region Properties
		/*[DataMember]
		public string ReportNo
		{
			get { return this.m_ReportNo; }
			set
			{
				if(this.m_ReportNo != value)
				{
					this.m_ReportNo = value;
					this.NotifyPropertyChanged("ReportNo");
					this.NotifyDBPropertyChanged("ReportNo");
				}
			}
		}*/

		[DataMember]
		public string MasterAccessionNo
		{
			get { return this.m_MasterAccessionNo; }
			set
			{
				if(this.m_MasterAccessionNo != value)
				{
					this.m_MasterAccessionNo = value;
					this.NotifyPropertyChanged("MasterAccessionNo");
					this.NotifyDBPropertyChanged("MasterAccessionNo");
				}
			}
		}

		[DataMember]
		public Nullable<DateTime> CollectionDate
		{
			get { return this.m_CollectionDate; }
			set
			{
				if(this.m_CollectionDate != value)
				{
					this.m_CollectionDate = value;
					this.NotifyPropertyChanged("CollectionDate");
					this.NotifyDBPropertyChanged("CollectionDate");
				}
			}
		}

		[DataMember]
		public string PFirstName
		{
			get { return this.m_PFirstName; }
			set
			{
				if(this.m_PFirstName != value)
				{
					this.m_PFirstName = value;
					this.NotifyPropertyChanged("PFirstName");
					this.NotifyDBPropertyChanged("PFirstName");
				}
			}
		}

		[DataMember]
		public string PLastName
		{
			get { return this.m_PLastName; }
			set
			{
				if(this.m_PLastName != value)
				{
					this.m_PLastName = value;
					this.NotifyPropertyChanged("PLastName");
					this.NotifyDBPropertyChanged("PLastName");
				}
			}
		}

		[DataMember]
		public string PMiddleInitial
		{
			get { return this.m_PMiddleInitial; }
			set
			{
				if(this.m_PMiddleInitial != value)
				{
					this.m_PMiddleInitial = value;
					this.NotifyPropertyChanged("PMiddleInitial");
					this.NotifyDBPropertyChanged("PMiddleInitial");
				}
			}
		}

		[DataMember]
		public string PSex
		{
			get { return this.m_PSex; }
			set
			{
				if(this.m_PSex != value)
				{
					this.m_PSex = value;
					this.NotifyPropertyChanged("PSex");
					this.NotifyDBPropertyChanged("PSex");
				}
			}
		}

		[DataMember]
		public string PSSN
		{
			get { return this.m_PSSN; }
			set
			{
				if(this.m_PSSN != value)
				{
					this.m_PSSN = value;
					this.NotifyPropertyChanged("PSSN");
					this.NotifyDBPropertyChanged("PSSN");
				}
			}
		}

		[DataMember]
		public Nullable<DateTime> PBirthdate
		{
			get { return this.m_PBirthdate; }
			set
			{
				if(this.m_PBirthdate != value)
				{
					this.m_PBirthdate = value;
					this.NotifyPropertyChanged("PBirthdate");
					this.NotifyDBPropertyChanged("PBirthdate");
				}
			}
		}

		/*[DataMember]
		public string SpecimenType
		{
			get { return this.m_SpecimenType; }
			set
			{
				if(this.m_SpecimenType != value)
				{
					this.m_SpecimenType = value;
					this.NotifyPropertyChanged("SpecimenType");
					this.NotifyDBPropertyChanged("SpecimenType");
				}
			}
		}

		[DataMember]
		public bool Final
		{
			get { return this.m_Final; }
			set
			{
				if(this.m_Final != value)
				{
					this.m_Final = value;
					this.NotifyPropertyChanged("Final");
					this.NotifyDBPropertyChanged("Final");
				}
			}
		}

		[DataMember]
		public Nullable<DateTime> FinalDate
		{
			get { return this.m_FinalDate; }
			set
			{
				if(this.m_FinalDate != value)
				{
					this.m_FinalDate = value;
					this.NotifyPropertyChanged("FinalDate");
					this.NotifyDBPropertyChanged("FinalDate");
				}
			}
		}

		[DataMember]
		public Nullable<DateTime> FinalTime
		{
			get { return this.m_FinalTime; }
			set
			{
				if(this.m_FinalTime != value)
				{
					this.m_FinalTime = value;
					this.NotifyPropertyChanged("FinalTime");
					this.NotifyDBPropertyChanged("FinalTime");
				}
			}
		}

		[DataMember]
		public string PathologistSignature
		{
			get { return this.m_PathologistSignature; }
			set
			{
				if(this.m_PathologistSignature != value)
				{
					this.m_PathologistSignature = value;
					this.NotifyPropertyChanged("PathologistSignature");
					this.NotifyDBPropertyChanged("PathologistSignature");
				}
			}
		}

		[DataMember]
		public int PathologistId
		{
			get { return this.m_PathologistId; }
			set
			{
				if(this.m_PathologistId != value)
				{
					this.m_PathologistId = value;
					this.NotifyPropertyChanged("PathologistId");
					this.NotifyDBPropertyChanged("PathologistId");
				}
			}
		}*/

		[DataMember]
		public string ClientName
		{
			get { return this.m_ClientName; }
			set
			{
				if(this.m_ClientName != value)
				{
					this.m_ClientName = value;
					this.NotifyPropertyChanged("ClientName");
					this.NotifyDBPropertyChanged("ClientName");
				}
			}
		}

		[DataMember]
		public string PhysicianName
		{
			get { return this.m_PhysicianName; }
			set
			{
				if(this.m_PhysicianName != value)
				{
					this.m_PhysicianName = value;
					this.NotifyPropertyChanged("PhysicianName");
					this.NotifyDBPropertyChanged("PhysicianName");
				}
			}
		}

		[DataMember]
		public Nullable<DateTime> AccessionDate
		{
			get { return this.m_AccessionDate; }
			set
			{
				if(this.m_AccessionDate != value)
				{
					this.m_AccessionDate = value;
					this.NotifyPropertyChanged("AccessionDate");
					this.NotifyDBPropertyChanged("AccessionDate");
				}
			}
		}

		[DataMember]
		public Nullable<DateTime> AccessionTime
		{
			get { return this.m_AccessionTime; }
			set
			{
				if(this.m_AccessionTime != value)
				{
					this.m_AccessionTime = value;
					this.NotifyPropertyChanged("AccessionTime");
					this.NotifyDBPropertyChanged("AccessionTime");
				}
			}
		}

		/*[DataMember]
		public Nullable<DateTime> CollectionTime
		{
			get { return this.m_CollectionTime; }
			set
			{
				if(this.m_CollectionTime != value)
				{
					this.m_CollectionTime = value;
					this.NotifyPropertyChanged("CollectionTime");
					this.NotifyDBPropertyChanged("CollectionTime");
				}
			}
		}*/

		[DataMember]
		public string SvhMedicalRecord
		{
			get { return this.m_SvhMedicalRecord; }
			set
			{
				if(this.m_SvhMedicalRecord != value)
				{
					this.m_SvhMedicalRecord = value;
					this.NotifyPropertyChanged("SvhMedicalRecord");
					this.NotifyDBPropertyChanged("SvhMedicalRecord");
				}
			}
		}

		[DataMember]
		public string SvhAccount
		{
			get { return this.m_SvhAccount; }
			set
			{
				if(this.m_SvhAccount != value)
				{
					this.m_SvhAccount = value;
					this.NotifyPropertyChanged("SvhAccount");
					this.NotifyDBPropertyChanged("SvhAccount");
				}
			}
		}

		[DataMember]
		public string PCAN
		{
			get { return this.m_PCAN; }
			set
			{
				if(this.m_PCAN != value)
				{
					this.m_PCAN = value;
					this.NotifyPropertyChanged("PCAN");
					this.NotifyDBPropertyChanged("PCAN");
				}
			}
		}

		#endregion

		#region WritePropertiesMethod
		public void WriteProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyWriter propertyWriter)
		{
			//this.m_ReportNo = propertyWriter.WriteString("ReportNo");
			this.m_MasterAccessionNo = propertyWriter.WriteString("MasterAccessionNo");
			this.m_CollectionDate = propertyWriter.WriteNullableDateTime("CollectionDate");
			this.m_PFirstName = propertyWriter.WriteString("PFirstName");
			this.m_PLastName = propertyWriter.WriteString("PLastName");
			this.m_PMiddleInitial = propertyWriter.WriteString("PMiddleInitial");
			this.m_PSex = propertyWriter.WriteString("PSex");
			this.m_PSSN = propertyWriter.WriteString("PSSN");
			this.m_PBirthdate = propertyWriter.WriteNullableDateTime("PBirthdate");
			//this.m_SpecimenType = propertyWriter.WriteString("SpecimenType");
			//this.m_Final = propertyWriter.WriteBoolean("Final");
			//this.m_FinalDate = propertyWriter.WriteNullableDateTime("FinalDate");
			//this.m_FinalTime = propertyWriter.WriteNullableDateTime("FinalTime");
			//this.m_PathologistSignature = propertyWriter.WriteString("PathologistSignature");
			//this.m_PathologistId = propertyWriter.WriteInt("PathologistId");
			this.m_ClientName = propertyWriter.WriteString("ClientName");
			this.m_PhysicianName = propertyWriter.WriteString("PhysicianName");
			this.m_AccessionDate = propertyWriter.WriteNullableDateTime("AccessionDate");
			this.m_AccessionTime = propertyWriter.WriteNullableDateTime("AccessionTime");
			//this.m_CollectionTime = propertyWriter.WriteNullableDateTime("CollectionTime");
			this.m_SvhMedicalRecord = propertyWriter.WriteString("SvhMedicalRecord");
			this.m_SvhAccount = propertyWriter.WriteString("SvhAccount");
			this.m_PCAN = propertyWriter.WriteString("PCAN");
		}
		#endregion

		#region ReadPropertiesMethod
		public void ReadProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyReader propertyReader)
		{
			//propertyReader.ReadString("ReportNo", ReportNo);
			propertyReader.ReadString("MasterAccessionNo", MasterAccessionNo);
			propertyReader.ReadNullableDateTime("CollectionDate", CollectionDate);
			propertyReader.ReadString("PFirstName", PFirstName);
			propertyReader.ReadString("PLastName", PLastName);
			propertyReader.ReadString("PMiddleInitial", PMiddleInitial);
			propertyReader.ReadString("PSex", PSex);
			propertyReader.ReadString("PSSN", PSSN);
			propertyReader.ReadNullableDateTime("PBirthdate", PBirthdate);
			//propertyReader.ReadString("SpecimenType", SpecimenType);
			//propertyReader.ReadBoolean("Final", Final);
			//propertyReader.ReadNullableDateTime("FinalDate", FinalDate);
			//propertyReader.ReadNullableDateTime("FinalTime", FinalTime);
			//propertyReader.ReadString("PathologistSignature", PathologistSignature);
			//propertyReader.ReadInt("PathologistId", PathologistId);
			propertyReader.ReadString("ClientName", ClientName);
			propertyReader.ReadString("PhysicianName", PhysicianName);
			propertyReader.ReadNullableDateTime("AccessionDate", AccessionDate);
			propertyReader.ReadNullableDateTime("AccessionTime", AccessionTime);
			//propertyReader.ReadNullableDateTime("CollectionTime", CollectionTime);
			propertyReader.ReadString("SvhMedicalRecord", SvhMedicalRecord);
			propertyReader.ReadString("SvhAccount", SvhAccount);
			propertyReader.ReadString("PCAN", PCAN);
		}
		#endregion
	}
}
