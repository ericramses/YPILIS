using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Xml.Linq;
using YellowstonePathology.Persistence;

namespace YellowstonePathology.Business.Billing
{
    public class SvhIncomingBillingData : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

		private int m_SvhIncomingBillingDataId;
		private DateTime m_DateReceived;
		private string m_MedicalRecordNo;
		private string m_AccountNo;
		private string m_AccountBaseClass;
		private string m_InsurancePlan1;
		private string m_BillingData;

		public SvhIncomingBillingData()
		{
		}

        public SvhIncomingBillingData(string accountNo, string medicalRecordNo, string billingData, string accountBaseClass, string insurancePlan1)
        {
            this.AccountNo = accountNo;
            this.MedicalRecordNo = medicalRecordNo;
            this.BillingData = billingData;
            this.AccountBaseClass = accountBaseClass;
            this.InsurancePlan1 = insurancePlan1;
            this.DateReceived = DateTime.Now;
        }

		public void SetAccessionData(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			XElement billingElement = XElement.Parse(this.m_BillingData);
			string medicalRecordNo = billingElement.Element("MedicalRecordNo").Value;			
			accessionOrder.PAddress1 = billingElement.Element("PatientAddress1").Value;
			accessionOrder.PAddress2 = billingElement.Element("PatientAddress2").Value;
			accessionOrder.PCity = billingElement.Element("PatientCity").Value;
			accessionOrder.PState = billingElement.Element("PatientState").Value;
			accessionOrder.PZipCode = billingElement.Element("PatientZIP").Value;
			string phone =  billingElement.Element("PatientHomePhone").Value; 
			if(!string.IsNullOrEmpty(phone))
			{
				if (phone.Length > 12) phone = phone.Substring(0, 12);
				phone = phone.Replace("-", "");
			}
			accessionOrder.PPhoneNumberHome = phone;
			accessionOrder.InsurancePlan1 = billingElement.Element("InsurancePlan1").Value;
			accessionOrder.InsurancePlan2 = billingElement.Element("InsurancePlan2").Value;            
		}

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

		[PersistentProperty()]
		public int SvhIncomingBillingDataId
		{
			get { return this.m_SvhIncomingBillingDataId; }
			set
			{
				if(this.m_SvhIncomingBillingDataId != value)
				{
					this.m_SvhIncomingBillingDataId = value;
					this.NotifyPropertyChanged("SvhIncomingBillingDataId");
				}
			}
		}

		[PersistentProperty()]
		public DateTime DateReceived
		{
			get { return this.m_DateReceived; }
			set
			{
				if(this.m_DateReceived != value)
				{
					this.m_DateReceived = value;
					this.NotifyPropertyChanged("DateReceived");
				}
			}
		}

		[PersistentProperty()]
		public string MedicalRecordNo
		{
			get { return this.m_MedicalRecordNo; }
			set
			{
				if(this.m_MedicalRecordNo != value)
				{
					this.m_MedicalRecordNo = value;
					this.NotifyPropertyChanged("MedicalRecordNo");
				}
			}
		}

		[PersistentProperty()]
		public string AccountNo
		{
			get { return this.m_AccountNo; }
			set
			{
				if(this.m_AccountNo != value)
				{
					this.m_AccountNo = value;
					this.NotifyPropertyChanged("AccountNo");
				}
			}
		}

		[PersistentProperty()]
		public string AccountBaseClass
		{
			get { return this.m_AccountBaseClass; }
			set
			{
				if(this.m_AccountBaseClass != value)
				{
					this.m_AccountBaseClass = value;
					this.NotifyPropertyChanged("AccountBaseClass");
				}
			}
		}

		[PersistentProperty()]
		public string InsurancePlan1
		{
			get { return this.m_InsurancePlan1; }
			set
			{
				if(this.m_InsurancePlan1 != value)
				{
					this.m_InsurancePlan1 = value;
					this.NotifyPropertyChanged("InsurancePlan1");
				}
			}
		}

		[PersistentProperty()]
		public string BillingData
		{
			get { return this.m_BillingData; }
			set
			{
				if(this.m_BillingData != value)
				{
					this.m_BillingData = value;
					this.NotifyPropertyChanged("BillingData");
				}
			}
		}
	}
}
