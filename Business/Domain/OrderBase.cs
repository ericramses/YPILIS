using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Reflection;
using System.Runtime.Serialization;
using System.IO;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Domain
{
    public class OrderBase : INotifyPropertyChanged, Interface.IOrder
	{
        protected delegate void PropertyChangedNotificationHandler(String info);
        public event PropertyChangedEventHandler PropertyChanged;

        protected string m_MasterAccessionNo;
        protected int m_SpecimenLogId;
        protected int m_LoggedById;
        protected int m_AccessionedById;
        protected bool m_Accessioned;
        protected Nullable<DateTime> m_AccessionDate;
        protected Nullable<DateTime> m_AccessionTime;
        protected Nullable<DateTime> m_CollectionDate;
        protected Nullable<DateTime> m_CollectionTime;        
        protected string m_PatientId;
        protected string m_PLastName;
        protected string m_PFirstName;
        protected string m_PMiddleInitial;
        protected Nullable<DateTime> m_PBirthdate;
        protected string m_PAddress1;
        protected string m_PAddress2;
        protected string m_PCity;
        protected string m_PState;
        protected string m_PZipCode;
        protected string m_PPhoneNumberHome;
        protected string m_PPhoneNumberBusiness;
        protected string m_PMaritalStatus;
        protected string m_PRace;
        protected string m_PSex;
        protected string m_PSSN;
        protected string m_PCAN;
        protected string m_PSuffix;
        protected string m_ClinicalHistory;
        protected int m_ClientId;
        protected string m_ClientName;
        protected int m_PhysicianId;
        protected string m_PhysicianName;
        protected string m_SvhAccount;
        protected string m_SvhMedicalRecord;
        protected string m_PatientType;
        protected string m_PrimaryInsurance;
        protected string m_SecondaryInsurance;
		protected string m_ClientOrderId;        
        protected bool m_PhysicianInterpretation;
		protected bool m_RequisitionVerified;
        protected bool m_OrderCancelled;
        protected string m_ExternalOrderId;        

		protected XElement m_OrderInstructions;
		protected XElement m_OrderInstructionsUpdate;

        public OrderBase()
        {

        }

		public virtual string MasterAccessionNo
        {
            get { return this.m_MasterAccessionNo; }
            set
            {
                if (this.m_MasterAccessionNo != value)
                {
                    this.m_MasterAccessionNo = value;
                    this.NotifyPropertyChanged("MasterAccessionNo");
                }
            }
        }

        public virtual int SpecimenLogId
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

        public virtual int LoggedById
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

        public virtual int AccessionedById
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

        public virtual bool Accessioned
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


        public virtual Nullable<DateTime> AccessionDate
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

        public virtual Nullable<DateTime> AccessionTime
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

        public virtual Nullable<DateTime> CollectionDate
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

        public virtual Nullable<DateTime> CollectionTime
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

        public virtual string PatientId
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

        public virtual string PLastName
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

        public virtual string PFirstName
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

        public virtual string PMiddleInitial
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

        public virtual Nullable<DateTime> PBirthdate
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

        public virtual string PAddress1
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

        public virtual string PAddress2
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

        public virtual string PCity
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

        public virtual string PState
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

        public virtual string PZipCode
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

        public virtual string PPhoneNumberHome
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

        public virtual string PPhoneNumberBusiness
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

        public virtual string PMaritalStatus
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

        public virtual string PRace
        {
            get { return this.m_PRace; }
            set
            {
                if (this.m_PRace != value)
                {
                    this.m_PRace = value;
                    this.NotifyPropertyChanged("PRace");
                }
            }
        }

        public virtual string PSex
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

        public virtual string PSSN
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

        public virtual string PCAN
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

        public virtual string PSuffix
        {
            get { return this.m_PSuffix; }
            set
            {
                if (this.m_PSuffix != value)
                {
                    this.m_PSuffix = value;
                    this.NotifyPropertyChanged("PSuffix");
                }
            }
        }
        
        public virtual string ClinicalHistory
        {
            get { return this.m_ClinicalHistory; }
            set
            {
                if (this.m_ClinicalHistory != value)
                {
                    this.m_ClinicalHistory = value;
                    this.NotifyPropertyChanged("ClinicalHistory");
                }
            }
        }

        public virtual int ClientId
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

        public virtual string ClientName
        {
            get { return this.m_ClientName; }
            set
            {
                if (this.m_ClientName != value)
                {
                    this.m_ClientName = value;
                    this.NotifyPropertyChanged("ClientName");
                    this.NotifyPropertyChanged("ProviderDisplayName");
                }
            }
        }

        public virtual int PhysicianId
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

        public virtual string PhysicianName
        {
            get { return this.m_PhysicianName; }
            set
            {
                if (this.m_PhysicianName != value)
                {
                    this.m_PhysicianName = value;
                    this.NotifyPropertyChanged("PhysicianName");
                    this.NotifyPropertyChanged("ProviderDisplayName");
                }
            }
        }

        public virtual string ProviderDisplayName
        {
            get
            {
                StringBuilder result = new StringBuilder();

                return result.ToString();
            }
        }

        public virtual string SvhAccount
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

        public virtual string SvhMedicalRecord
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

        public virtual string PatientType
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

        public virtual string PrimaryInsurance
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

		public virtual XElement OrderInstructions
		{
			get
			{
				if (this.m_OrderInstructions == null)
				{
					this.m_OrderInstructions = new XElement(this.m_OrderInstructionsUpdate);
				}
				return this.m_OrderInstructions;
			}
			set
			{
				if (this.m_OrderInstructions != value)
				{
					this.m_OrderInstructions = value;
				}
			}
		}

		public virtual XElement OrderInstructionsUpdate
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

        public virtual string SecondaryInsurance
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

		public virtual string ClientOrderId
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

        public virtual bool PhysicianInterpretation
        {
            get { return this.m_PhysicianInterpretation; }
            set
            {
                if ((this.m_PhysicianInterpretation != value))
                {
                    this.m_PhysicianInterpretation = value;
                    this.NotifyPropertyChanged("PhysicianInterpretation");
                }
            }
        }

        public virtual bool OrderCancelled
        {
            get { return this.m_OrderCancelled; }
            set
            {
                if ((this.m_OrderCancelled != value))
                {
                    this.m_OrderCancelled = value;
                    this.NotifyPropertyChanged("OrderCancelled");
                }
            }
        }

		public virtual bool RequisitionVerified
		{
			get { return this.m_RequisitionVerified; }
			set
			{
				if ((this.m_RequisitionVerified != value))
				{
					this.m_RequisitionVerified = value;
					this.NotifyPropertyChanged("RequisitionVerified");
				}
			}
		}

        public virtual string ExternalOrderId
        {
            get { return this.m_ExternalOrderId; }
            set
            {
                if ((this.m_ExternalOrderId != value))
                {
                    this.m_ExternalOrderId = value;
                    this.NotifyPropertyChanged("ExternalOrderId");
                }
            }
        }     

        public virtual XElement ToXml()
        {
            throw new NotImplementedException("Not Implemented Here");
        }

        public virtual void FromXml(XElement xml)
        {
            throw new NotImplementedException("Not Implemented Here");
        }
		
		public virtual string PatientName
        {
            get
            {
				return YellowstonePathology.Shared.Helper.PatientHelper.GetPatientName(this.PLastName, this.PFirstName, this.PMiddleInitial);				
            }
        }

        public virtual string PatientDisplayName
        {
            get
            {
				return YellowstonePathology.Shared.Helper.PatientHelper.GetPatientDisplayName(this.PLastName, this.PFirstName, this.PMiddleInitial);				
            }
        }

        public virtual void SubmitChanges(YellowstonePathology.Business.DataContext.YpiDataBase dataContext)
        {
			if (this.m_OrderInstructions != null)
			{
				if (this.m_OrderInstructions.Value != this.m_OrderInstructionsUpdate.Value)
					this.m_OrderInstructionsUpdate = new XElement(this.m_OrderInstructions);
			}
			dataContext.SubmitChanges();
            this.NotifyPropertyChanged(string.Empty);
        }

        public virtual void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

		public string PLastNameProxy
		{
			get { return this.m_PLastName; }
			set
			{
				if (this.m_PLastName != value)
				{
					this.m_PLastName = value;
					this.NotifyPropertyChanged("PLastName");
					this.NotifyPropertyChanged("PatientName");
					this.NotifyPropertyChanged("PLastNameProxy");
				}
			}
		}

		public string PFirstNameProxy
		{
			get { return this.m_PFirstName; }
			set
			{
				if (this.m_PFirstName != value)
				{
					this.m_PFirstName = value;
					this.NotifyPropertyChanged("PFirstName");
					this.NotifyPropertyChanged("PatientName");
					this.NotifyPropertyChanged("PFirstNameProxy");
				}
			}
		}

		public string ProviderNameString
		{
			get
			{
				StringBuilder providerString = new StringBuilder();
				providerString.Append(this.PhysicianName);
				if (string.IsNullOrEmpty(this.ClientName) == false)
				{
					providerString.Append(" - " + this.ClientName);
				}
				return providerString.ToString();
			}
		}

		public void Refresh(YellowstonePathology.Business.DataContext.YpiDataBase dataContext)
		{
			this.OrderInstructions = null;
			dataContext.Refresh(RefreshMode.OverwriteCurrentValues, this);
		}

		public virtual void Save()
		{
			throw new NotImplementedException("Save not implemented in Domain.OrderBase.");
		}

		public virtual void GetKeyLock(YellowstonePathology.Business.Domain.KeyLock keyLock)
		{
			throw new NotImplementedException("Save not implemented in Domain.OrderBase.");
		}		

		public string PatientAccessionAge
		{
			get { return YellowstonePathology.Shared.Helper.PatientHelper.GetAccessionAge(this.PBirthdate, this.AccessionDate.Value); }
		}

		public Nullable<DateTime> AccessionDateTime
		{
			get
			{
				return YellowstonePathology.Business.Helper.DateTimeExtensions.DateTimeFromDateAndTime(this.CollectionDate, this.CollectionTime);
			}

			set
			{
				Nullable<DateTime> dt = null;
				Nullable<DateTime> dtTime = null;
				YellowstonePathology.Business.Helper.DateTimeExtensions.DateTimeToDateAndTime(value, ref dt, ref dtTime);
				this.AccessionDate = dt;
				this.AccessionTime = dtTime;
				NotifyPropertyChanged("AccessionDateTime");
			}
		}

		public void SetPhysicianClient(YellowstonePathology.Business.Client.PhysicianClient physicianClient)
		{
            this.ClientId = physicianClient.ClientId;
            this.PhysicianId = physicianClient.PhysicianId;
            this.PhysicianName = physicianClient.PhysicianName;
            this.ClientName = physicianClient.ClientName;
		}
	}
}
