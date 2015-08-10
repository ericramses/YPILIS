using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Domain
{
	[Table(Name = "tblAccessionOrder")]
	public class SimpleAccessionOrderLinq : YellowstonePathology.Business.Domain.OrderBase
	{
		public SimpleAccessionOrderLinq()
		{

		}		

		[Column(Name = "SpecimenLogId", Storage = "m_SpecimenLogId")]
		public override int SpecimenLogId
		{
			get { return this.m_SpecimenLogId; }
			set
			{
				if (this.m_SpecimenLogId != value)
				{
					this.m_SpecimenLogId = value;
					this.NotifyPropertyChanged("SpecimenLogId");
				}
			}
		}

		[Column(Name = "LoggedById", Storage = "m_LoggedById")]
		public override int LoggedById
		{
			get { return this.m_LoggedById; }
			set
			{
				if (this.m_LoggedById != value)
				{
					this.m_LoggedById = value;
					this.NotifyPropertyChanged("LoggedById");
				}
			}
		}

		[Column(Name = "Accessioned", Storage = "m_Accessioned")]
		public override bool Accessioned
		{
			get { return this.m_Accessioned; }
			set
			{
				if (this.m_Accessioned != value)
				{
					this.m_Accessioned = value;
					this.NotifyPropertyChanged("Accessioned");
				}
			}
		}

		[Column(Name = "AccessionedById", Storage = "m_AccessionedById")]
		public override int AccessionedById
		{
			get { return this.m_AccessionedById; }
			set
			{
				if (this.m_AccessionedById != value)
				{
					this.m_AccessionedById = value;
					this.NotifyPropertyChanged("AccessionedById");
				}
			}
		}



		[Column(Name = "PatientId", Storage = "m_PatientId")]
		public override string PatientId
		{
			get { return this.m_PatientId; }
			set
			{
				if (this.m_PatientId != value)
				{
					this.m_PatientId = value;
					this.NotifyPropertyChanged("PatientId");
				}
			}
		}

		[Column(Name = "CollectionDate", Storage = "m_CollectionDate")]
		public override Nullable<DateTime> CollectionDate
		{
			get { return this.m_CollectionDate; }
			set
			{
				if (this.m_CollectionDate != value)
				{
					this.m_CollectionDate = value;
					this.NotifyPropertyChanged("CollectionDate");
				}
			}
		}

		[Column(Name = "CollectionTime", Storage = "m_CollectionTime")]
		public override Nullable<DateTime> CollectionTime
		{
			get { return this.m_CollectionTime; }
			set
			{
				if (this.m_CollectionTime != value)
				{
					this.m_CollectionTime = value;
					this.NotifyPropertyChanged("CollectionTime");
				}
			}
		}

		[Column(Name = "AccessionDate", Storage = "m_AccessionDate", CanBeNull = false)]
		public override Nullable<DateTime> AccessionDate
		{
			get { return this.m_AccessionDate; }
			set
			{
				if (this.m_AccessionDate != value)
				{
					this.m_AccessionDate = value;
					this.NotifyPropertyChanged("AccessionDate");
				}
			}
		}

		[Column(Name = "AccessionTime", Storage = "m_AccessionTime", CanBeNull = false)]
		public override Nullable<DateTime> AccessionTime
		{
			get { return this.m_AccessionTime; }
			set
			{
				if (this.m_AccessionTime != value)
				{
					this.m_AccessionTime = value;
					this.NotifyPropertyChanged("AccessionTime");
				}
			}
		}

		[Column(Name = "PLastName", Storage = "m_PLastName", CanBeNull = true)]
		public override string PLastName
		{
			get { return this.m_PLastName; }
			set
			{
				if (this.m_PLastName != value)
				{
					this.m_PLastName = value;
					this.NotifyPropertyChanged("PLastName");
				}
			}
		}

		[Column(Name = "PFirstName", Storage = "m_PFirstName", CanBeNull = true)]
		public override string PFirstName
		{
			get { return this.m_PFirstName; }
			set
			{
				if (this.m_PFirstName != value)
				{
					this.m_PFirstName = value;
					this.NotifyPropertyChanged("PFirstName");
				}
			}
		}

		[Column(Name = "PMiddleInitial", Storage = "m_PMiddleInitial", CanBeNull = true)]
		public override string PMiddleInitial
		{
			get { return this.m_PMiddleInitial; }
			set
			{
				if (this.m_PMiddleInitial != value)
				{
					this.m_PMiddleInitial = value;
					this.NotifyPropertyChanged("PMiddleInitial");
				}
			}
		}

		[Column(Name = "PBirthdate", Storage = "m_PBirthdate")]
		public override Nullable<DateTime> PBirthdate
		{
			get { return this.m_PBirthdate; }
			set
			{
				if (this.m_PBirthdate != value)
				{
					this.m_PBirthdate = value;
					this.NotifyPropertyChanged("PBirthdate");
				}
			}
		}

		[Column(Name = "PAddress1", Storage = "m_PAddress1")]
		public override string PAddress1
		{
			get { return this.m_PAddress1; }
			set
			{
				if (this.m_PAddress1 != value)
				{
					this.m_PAddress1 = value;
					this.NotifyPropertyChanged("PAddress1");
				}
			}
		}

		[Column(Name = "PAddress2", Storage = "m_PAddress2")]
		public override string PAddress2
		{
			get { return this.m_PAddress2; }
			set
			{
				if (this.m_PAddress2 != value)
				{
					this.m_PAddress2 = value;
					this.NotifyPropertyChanged("PAddress2");
				}
			}
		}

		[Column(Name = "PCity", Storage = "m_PCity")]
		public override string PCity
		{
			get { return this.m_PCity; }
			set
			{
				if (this.m_PCity != value)
				{
					this.m_PCity = value;
					this.NotifyPropertyChanged("PCity");
				}
			}
		}

		[Column(Name = "PState", Storage = "m_PState")]
		public override string PState
		{
			get { return this.m_PState; }
			set
			{
				if (this.m_PState != value)
				{
					this.m_PState = value;
					this.NotifyPropertyChanged("PState");
				}
			}
		}

		[Column(Name = "PZipCode", Storage = "m_PZipCode")]
		public override string PZipCode
		{
			get { return this.m_PZipCode; }
			set
			{
				if (this.m_PZipCode != value)
				{
					this.m_PZipCode = value;
					this.NotifyPropertyChanged("PZipCode");
				}
			}
		}

		[Column(Name = "PPhoneNumberHome", Storage = "m_PPhoneNumberHome", CanBeNull = true)]
		public override string PPhoneNumberHome
		{
			get { return this.m_PPhoneNumberHome; }
			set
			{
				if (this.m_PPhoneNumberHome != value)
				{
					this.m_PPhoneNumberHome = value;
					this.NotifyPropertyChanged("PPhoneNumberHome");
				}
			}
		}

		[Column(Name = "PPhoneNumberBusiness", Storage = "m_PPhoneNumberBusiness", CanBeNull = true)]
		public override string PPhoneNumberBusiness
		{
			get { return this.m_PPhoneNumberBusiness; }
			set
			{
				if (this.m_PPhoneNumberBusiness != value)
				{
					this.m_PPhoneNumberBusiness = value;
					this.NotifyPropertyChanged("PPhoneNumberBusiness");
				}
			}
		}

		[Column(Name = "PMaritalStatus", Storage = "m_PMaritalStatus", CanBeNull = true)]
		public override string PMaritalStatus
		{
			get { return this.m_PMaritalStatus; }
			set
			{
				if (this.m_PMaritalStatus != value)
				{
					this.m_PMaritalStatus = value;
					this.NotifyPropertyChanged("PMaritalStatus");
				}
			}
		}

		[Column(Name = "PRace", Storage = "m_PRace", CanBeNull = true)]
		public override string PRace
		{
			get { return this.m_PRace; }
			set
			{
				if (this.m_PRace != value)
				{
					this.m_PRace = value;
					this.NotifyPropertyChanged("PPRace");
				}
			}
		}

		[Column(Name = "PSex", Storage = "m_PSex", CanBeNull = true)]
		public override string PSex
		{
			get { return this.m_PSex; }
			set
			{
				if (this.m_PSex != value)
				{
					this.m_PSex = value;
					this.NotifyPropertyChanged("PSex");
				}
			}
		}

		[Column(Name = "PSSN", Storage = "m_PSSN", CanBeNull = true)]
		public override string PSSN
		{
			get { return this.m_PSSN; }
			set
			{
				if (this.m_PSSN != value)
				{
					this.m_PSSN = value;
					this.NotifyPropertyChanged("PSSN");
				}
			}
		}

		[Column(Name = "PCAN", Storage = "m_PCAN", CanBeNull = true)]
		public override string PCAN
		{
			get { return this.m_PCAN; }
			set
			{
				if (this.m_PCAN != value)
				{
					this.m_PCAN = value;
					this.NotifyPropertyChanged("PCAN");
				}
			}
		}



		[Column(Name = "ClientId", Storage = "m_ClientId")]
		public override int ClientId
		{
			get { return this.m_ClientId; }
			set
			{
				if (this.m_ClientId != value)
				{
					this.m_ClientId = value;
					this.NotifyPropertyChanged("ClientId");
				}
			}
		}

		[Column(Name = "PhysicianId", Storage = "m_PhysicianId")]
		public override int PhysicianId
		{
			get { return this.m_PhysicianId; }
			set
			{
				if (this.m_PhysicianId != value)
				{
					this.m_PhysicianId = value;
					this.NotifyPropertyChanged("PhysicianId");
				}
			}
		}

		[Column(Name = "ClientName", Storage = "m_ClientName")]
		public override string ClientName
		{
			get { return this.m_ClientName; }
			set
			{
				if (this.m_ClientName != value)
				{
					this.m_ClientName = value;
					this.NotifyPropertyChanged("ClientName");
				}
			}
		}

		[Column(Name = "PhysicianName", Storage = "m_PhysicianName")]
		public override string PhysicianName
		{
			get { return this.m_PhysicianName; }
			set
			{
				if (this.m_PhysicianName != value)
				{
					this.m_PhysicianName = value;
					this.NotifyPropertyChanged("PhysicianName");
				}
			}
		}

		[Column(Storage = "m_SvhAccount", DbType = "VarChar(50)")]
		public override string SvhAccount
		{
			get
			{
				return this.m_SvhAccount;
			}
			set
			{
				if ((this.m_SvhAccount != value))
				{
					this.m_SvhAccount = value;
					this.NotifyPropertyChanged("SvhAccount");
				}
			}
		}

		[Column(Storage = "m_SvhMedicalRecord", DbType = "VarChar(50)")]
		public override string SvhMedicalRecord
		{
			get
			{
				return this.m_SvhMedicalRecord;
			}
			set
			{
				if ((this.m_SvhMedicalRecord != value))
				{
					this.m_SvhMedicalRecord = value;
					this.NotifyPropertyChanged("SvhMedicalRecord");
				}
			}
		}

		[Column(Storage = "m_PatientType", DbType = "VarChar(20)")]
		public override string PatientType
		{
			get
			{
				return this.m_PatientType;
			}
			set
			{
				if ((this.m_PatientType != value))
				{
					this.m_PatientType = value;
					this.NotifyPropertyChanged("PatientType");
				}
			}
		}

		[Column(Storage = "m_PrimaryInsurance", DbType = "VarChar(50)")]
		public override string PrimaryInsurance
		{
			get
			{
				return this.m_PrimaryInsurance;
			}
			set
			{
				if ((this.m_PrimaryInsurance != value))
				{
					this.m_PrimaryInsurance = value;
					this.NotifyPropertyChanged("PrimaryInsurance");
				}
			}
		}

		[Column(Storage = "m_SecondaryInsurance", DbType = "VarChar(50)")]
		public override string SecondaryInsurance
		{
			get
			{
				return this.m_SecondaryInsurance;
			}
			set
			{
				if ((this.m_SecondaryInsurance != value))
				{
					this.m_SecondaryInsurance = value;
					this.NotifyPropertyChanged("SecondaryInsurance");
				}
			}
		}

		[Column(Storage = "m_PSuffix", DbType = "VarChar(50)")]
		public override string PSuffix
		{
			get
			{
				return this.m_PSuffix;
			}
			set
			{
				if ((this.m_PSuffix != value))
				{
					this.m_PSuffix = value;
					this.NotifyPropertyChanged("PSuffix");
				}
			}
		}

		[Column(Storage = "m_ClinicalHistory", DbType = "VarChar(MAX)")]
		public override string ClinicalHistory
		{
			get
			{
				return this.m_ClinicalHistory;
			}
			set
			{
				if ((this.m_ClinicalHistory != value))
				{
					this.m_ClinicalHistory = value;
					this.NotifyPropertyChanged("ClinicalHistory");
				}
			}
		}

		[Column(Storage = "m_ClientOrderId", DbType = "VarChar(50)")]
		public override string ClientOrderId
		{
			get
			{
				return this.m_ClientOrderId;
			}
			set
			{
				if ((this.m_ClientOrderId != value))
				{
					this.m_ClientOrderId = value;
					this.NotifyPropertyChanged("ClientOrderId");
				}
			}
		}

		[Column(Name = "OrderInstructions", Storage = "m_OrderInstructionsUpdate", DbType = "xml", UpdateCheck = UpdateCheck.Never)]
		public override XElement OrderInstructionsUpdate
		{
			get { return this.m_OrderInstructionsUpdate; }
			set
			{
				if (this.m_OrderInstructionsUpdate != value)
				{
					this.m_OrderInstructionsUpdate = value;
				}
			}
		}
	}
}