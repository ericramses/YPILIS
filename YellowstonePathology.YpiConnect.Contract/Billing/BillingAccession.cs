using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace YellowstonePathology.YpiConnect.Contract.Billing
{
	[DataContract]
	public class BillingAccession : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private string m_MasterAccessionNo;        
        private DateTime m_AccessionDate;
        private string m_FirstName;
        private string m_LastName;
        private int m_ClientId;
        private string m_ClientName;        
        private int m_PhysicianId;
        private string m_PhysicianName;        
        private string m_PrimaryInsurance;
        private string m_PatientType;
		private string m_ReportNo;
		

        public BillingAccession()
        {
        }        
		
		public void WriteProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyWriter propertyWriter)
        {
            this.m_MasterAccessionNo = propertyWriter.WriteString("MasterAccessionNo");            
            this.m_ClientId = propertyWriter.WriteInt("ClientId");
            this.m_FirstName = propertyWriter.WriteString("FirstName");
            this.m_LastName = propertyWriter.WriteString("LastName");
            this.m_ClientName = propertyWriter.WriteString("ClientName");
            this.m_PhysicianId = propertyWriter.WriteInt("PhysicianId");
            this.m_PhysicianName = propertyWriter.WriteString("PhysicianName");
            this.m_PrimaryInsurance = propertyWriter.WriteString("PrimaryInsurance");
            this.m_PatientType = propertyWriter.WriteString("PatientType");
			this.m_AccessionDate = propertyWriter.WriteDateTime("AccessionDate");
			this.m_ReportNo = propertyWriter.WriteString("ReportNo");
		}

		[DataMember]
		public string MasterAccessionNo
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
		
		[DataMember]
		public DateTime AccessionDate
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

		[DataMember]
		public string FirstName
        {
            get { return this.m_FirstName; }
            set
            {
                if (this.m_FirstName != value)
                {
                    this.m_FirstName = value;
                    this.NotifyPropertyChanged("FirstName");
                }
            }
        }

		[DataMember]
		public string LastName
        {
            get { return this.m_LastName; }
            set
            {
                if (this.m_LastName != value)
                {
                    this.m_LastName = value;
                    this.NotifyPropertyChanged("LastName");
                }
            }
        }

		[DataMember]
		public int ClientId
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

		[DataMember]
		public string ClientName
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

		[DataMember]
		public int PhysicianId
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

		[DataMember]
		public string PhysicianName
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

		[DataMember]
		public string PrimaryInsurance
        {
            get { return this.m_PrimaryInsurance; }
            set 
            {
                if (this.m_PrimaryInsurance != value)
                {
                    this.m_PrimaryInsurance = value;
                    this.NotifyPropertyChanged("PrimaryInsurance");					
				}
            }
        }

		[DataMember]
		public string PatientType
        {
            get { return this.m_PatientType; }
            set
            {
                if (this.m_PatientType != value)
                {
                    this.m_PatientType = value;
                    this.NotifyPropertyChanged("PatientType");
                }
            }
        }
		[DataMember]
		public string ReportNo
		{
			get { return this.m_ReportNo; }
			set
			{
				if (this.m_ReportNo != value)
				{
					this.m_ReportNo = value;
					this.NotifyPropertyChanged("ReportNo");
				}
			}
		}

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}
	}
}
