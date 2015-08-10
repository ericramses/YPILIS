using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Xml.Linq;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.YpiConnect.Contract.Billing
{
	[DataContract]
	public class ICD9BillingCode : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		private bool m_BillingIsOpen;

		private string m_Icd9BillingId;
		private string m_SpecimenOrderId;
		private string m_SurgicalSpecimenId;
		private Nullable<DateTime> m_BillingDate;
		private Nullable<DateTime> m_ModifiedDate;
		private int m_Quantity;
		private int m_SpecimenId;
		private int m_UserId;
		private string m_Icd9Code;
		private string m_ReportNo;
		private string m_MasterAccessionNo;
		private string m_DesignatedFor;
		private string m_Source;

		public ICD9BillingCode()
        {
		}

		public Boolean BillingIsOpen
        {
            get { return this.m_BillingIsOpen; }
            set
            {
                if (value != this.m_BillingIsOpen)
                {
                    this.m_BillingIsOpen = value;
                    this.NotifyPropertyChanged("BillingIsOpen");
                }
            }
        }

		public string BillingDateProxy
        {
            get
            {
                if (this.BillingDate.HasValue == false)
                {
                    return null;
                }
                else
                {
                    return this.BillingDate.Value.ToShortDateString();
                }
            }

            set
            {
                string strValue = value.ToString();
                if (strValue == string.Empty)
                {
                    this.BillingDate = null;
                }
                else
                {
                    this.BillingDate = DateTime.Parse(strValue);
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

		[DataMember]
		[PersistentProperty()]
		public string Icd9BillingId
		{
			get { return this.m_Icd9BillingId; }
			set
			{
				if (this.m_Icd9BillingId != value)
				{
					this.m_Icd9BillingId = value;
					this.NotifyPropertyChanged("Icd9BillingId");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public string SpecimenOrderId
		{
			get { return this.m_SpecimenOrderId; }
			set
			{
				if (this.m_SpecimenOrderId != value)
				{
					this.m_SpecimenOrderId = value;
					this.NotifyPropertyChanged("SpecimenOrderId");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public string SurgicalSpecimenId
		{
			get { return this.m_SurgicalSpecimenId; }
			set
			{
				if (this.m_SurgicalSpecimenId != value)
				{
					this.m_SurgicalSpecimenId = value;
					this.NotifyPropertyChanged("SurgicalSpecimenId");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public Nullable<DateTime> BillingDate
		{
			get { return this.m_BillingDate; }
			set
			{
				if (this.m_BillingDate != value)
				{
					this.m_BillingDate = value;
					this.NotifyPropertyChanged("BillingDate");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public Nullable<DateTime> ModifiedDate
		{
			get { return this.m_ModifiedDate; }
			set
			{
				if (this.m_ModifiedDate != value)
				{
					this.m_ModifiedDate = value;
					this.NotifyPropertyChanged("ModifiedDate");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public int Quantity
		{
			get { return this.m_Quantity; }
			set
			{
				if (this.m_Quantity != value)
				{
					this.m_Quantity = value;
					this.NotifyPropertyChanged("Quantity");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public int SpecimenId
		{
			get { return this.m_SpecimenId; }
			set
			{
				if (this.m_SpecimenId != value)
				{
					this.m_SpecimenId = value;
					this.NotifyPropertyChanged("SpecimenId");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public int UserId
		{
			get { return this.m_UserId; }
			set
			{
				if (this.m_UserId != value)
				{
					this.m_UserId = value;
					this.NotifyPropertyChanged("UserId");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public string Icd9Code
		{
			get { return this.m_Icd9Code; }
			set
			{
				if (this.m_Icd9Code != value)
				{
					this.m_Icd9Code = value;
					this.NotifyPropertyChanged("Icd9Code");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
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

		[DataMember]
		[PersistentProperty()]
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
		[PersistentProperty()]
		public string DesignatedFor
		{
			get { return this.m_DesignatedFor; }
			set
			{
				if (this.m_DesignatedFor != value)
				{
					this.m_DesignatedFor = value;
					this.NotifyPropertyChanged("DesignatedFor");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public string Source
		{
			get { return this.m_Source; }
			set
			{
				if (this.m_Source != value)
				{
					this.m_Source = value;
					this.NotifyPropertyChanged("Source");
				}
			}
		}
	}
}
