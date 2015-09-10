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
	public class CptBillingCodeItem : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

        private YellowstonePathology.YpiConnect.Contract.Billing.ICD9BillingCodeCollection m_ICD9BillingCodeCollection;

		private string m_CptBillingId;
		private string m_ReportNo;
		private string m_MasterAccessionNo;
		private string m_SpecimenOrderId;
		private string m_SurgicalSpecimenId;
		private bool m_NoCharge;
		private bool m_Ordered14DaysPostDischarge;
		private Nullable<DateTime> m_BillingDate;
		private Nullable<DateTime> m_ModifiedDate;
		private int m_SpecimenId;
		private int m_StainOrderId;
		private int m_Quantity;
		private int m_ClientId;
		private int m_BillingTypeId;
		private int m_PanelSetId;
		private string m_TestOrderId;
		private string m_CptCode;
		private string m_Modifier;
		private string m_Description;
		private string m_FeeType;
		private string m_ProfessionalCharge;
		private string m_PCCharge;
		private string m_BillingType;
		private string m_RuleApplied;
		private string m_CaseType;
		private string m_PatientType;
		private string m_PrimaryInsurance;
		private string m_SecondaryInsurance;
		private string m_TechnicalCharge;
		private string m_CaseOrigination;		

		public CptBillingCodeItem()
        {
			this.m_ICD9BillingCodeCollection = new ICD9BillingCodeCollection();
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
		public YellowstonePathology.YpiConnect.Contract.Billing.ICD9BillingCodeCollection ICD9BillingCodeCollection
		{
			get { return this.m_ICD9BillingCodeCollection; }
			set { this.m_ICD9BillingCodeCollection = value; }
		}

		[DataMember]
		[PersistentProperty()]
		public string CptBillingId
		{
			get { return this.m_CptBillingId; }
			set
			{
				if (this.m_CptBillingId != value)
				{
					this.m_CptBillingId = value;
					this.NotifyPropertyChanged("CptBillingId");
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
		public bool NoCharge
		{
			get { return this.m_NoCharge; }
			set
			{
				if (this.m_NoCharge != value)
				{
					this.m_NoCharge = value;
					this.NotifyPropertyChanged("NoCharge");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public bool Ordered14DaysPostDischarge
		{
			get { return this.m_Ordered14DaysPostDischarge; }
			set
			{
				if (this.m_Ordered14DaysPostDischarge != value)
				{
					this.m_Ordered14DaysPostDischarge = value;
					this.NotifyPropertyChanged("Ordered14DaysPostDischarge");
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
		public int StainOrderId
		{
			get { return this.m_StainOrderId; }
			set
			{
				if (this.m_StainOrderId != value)
				{
					this.m_StainOrderId = value;
					this.NotifyPropertyChanged("StainOrderId");
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
		[PersistentProperty()]
		public int BillingTypeId
		{
			get { return this.m_BillingTypeId; }
			set
			{
				if (this.m_BillingTypeId != value)
				{
					this.m_BillingTypeId = value;
					this.NotifyPropertyChanged("BillingTypeId");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public int PanelSetId
		{
			get { return this.m_PanelSetId; }
			set
			{
				if (this.m_PanelSetId != value)
				{
					this.m_PanelSetId = value;
					this.NotifyPropertyChanged("PanelSetId");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public string TestOrderId
		{
			get { return this.m_TestOrderId; }
			set
			{
				if (this.m_TestOrderId != value)
				{
					this.m_TestOrderId = value;
					this.NotifyPropertyChanged("TestOrderId");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public string CptCode
		{
			get { return this.m_CptCode; }
			set
			{
				if (this.m_CptCode != value)
				{
					this.m_CptCode = value;
					this.NotifyPropertyChanged("CptCode");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public string Modifier
		{
			get { return this.m_Modifier; }
			set
			{
				if (this.m_Modifier != value)
				{
					this.m_Modifier = value;
					this.NotifyPropertyChanged("Modifier");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public string Description
		{
			get { return this.m_Description; }
			set
			{
				if (this.m_Description != value)
				{
					this.m_Description = value;
					this.NotifyPropertyChanged("Description");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public string FeeType
		{
			get { return this.m_FeeType; }
			set
			{
				if (this.m_FeeType != value)
				{
					this.m_FeeType = value;
					this.NotifyPropertyChanged("FeeType");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public string ProfessionalCharge
		{
			get { return this.m_ProfessionalCharge; }
			set
			{
				if (this.m_ProfessionalCharge != value)
				{
					this.m_ProfessionalCharge = value;
					this.NotifyPropertyChanged("ProfessionalCharge");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public string PCCharge
		{
			get { return this.m_PCCharge; }
			set
			{
				if (this.m_PCCharge != value)
				{
					this.m_PCCharge = value;
					this.NotifyPropertyChanged("PCCharge");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public string BillingType
		{
			get { return this.m_BillingType; }
			set
			{
				if (this.m_BillingType != value)
				{
					this.m_BillingType = value;
					this.NotifyPropertyChanged("BillingType");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public string RuleApplied
		{
			get { return this.m_RuleApplied; }
			set
			{
				if (this.m_RuleApplied != value)
				{
					this.m_RuleApplied = value;
					this.NotifyPropertyChanged("RuleApplied");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public string CaseType
		{
			get { return this.m_CaseType; }
			set
			{
				if (this.m_CaseType != value)
				{
					this.m_CaseType = value;
					this.NotifyPropertyChanged("CaseType");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
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
		[PersistentProperty()]
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
		[PersistentProperty()]
		public string SecondaryInsurance
		{
			get { return this.m_SecondaryInsurance; }
			set
			{
				if (this.m_SecondaryInsurance != value)
				{
					this.m_SecondaryInsurance = value;
					this.NotifyPropertyChanged("SecondaryInsurance");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public string TechnicalCharge
		{
			get { return this.m_TechnicalCharge; }
			set
			{
				if (this.m_TechnicalCharge != value)
				{
					this.m_TechnicalCharge = value;
					this.NotifyPropertyChanged("TechnicalCharge");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public string CaseOrigination
		{
			get { return this.m_CaseOrigination; }
			set
			{
				if (this.m_CaseOrigination != value)
				{
					this.m_CaseOrigination = value;
					this.NotifyPropertyChanged("CaseOrigination");
				}
			}
		}

		/*[DataMember]
		[PersistentProperty()]
		public string PatientBilling
		{
			get { return this.m_PatientBilling; }
			set
			{
				if (this.m_PatientBilling != value)
				{
					this.m_PatientBilling = value;
					this.NotifyPropertyChanged("PatientBilling");
				}
			}
		}*/
	}
}
