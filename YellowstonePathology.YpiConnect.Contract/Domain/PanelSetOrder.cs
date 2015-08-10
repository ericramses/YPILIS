using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml.Linq;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.YpiConnect.Contract.Domain
{
	[DataContract]
	public class PanelSetOrder : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		protected Object m_OriginalValues;

		private string m_ReportNo;
		private int m_PanelSetId;
		private string m_PanelSetName;
		private int m_FinaledById;
		private bool m_Final;
		private Nullable<DateTime> m_FinalDate;
		private Nullable<DateTime> m_FinalTime;
		private int m_OrderedById;
		private Nullable<DateTime> m_OrderDate;
		private Nullable<DateTime> m_OrderTime;
		private string m_Signature;
		private int m_AssignedToId;
		private int m_TemplateId;
		private string m_MasterAccessionNo;
		private string m_OriginatingLocation;
		private bool m_Audit;
		private bool m_BillingAudit;
		private bool m_SignatureAudit;
		private string m_ResultDocumentSource;
		private string m_ResultDocumentPath;
		private bool m_Published;
		private Nullable<DateTime> m_DateLastPublished;
		private string m_TechnicalComponentFacilityId;
		private bool m_HasTechnicalComponent;
		private string m_ProfessionalComponentFacilityId;
		private bool m_HasProfessionalComponent;
		private int m_AuditedById;
		private Nullable<DateTime> m_AuditDate;

		public PanelSetOrder()
		{
		}

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		public Object OriginalValues
		{
			get { return this.m_OriginalValues; }
		}

		public virtual bool HasChanges()
		{
			bool result = false;
			if (this.Equals(this.m_OriginalValues) == true)
			{
				result = true;
			}
			return result;
		}

		public virtual void SetOriginalValues()
		{
			this.m_OriginalValues = this.MemberwiseClone();
		}

		[DataMember]
		[PersistentPrimaryKeyProperty(false)]
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
		public string PanelSetName
		{
			get { return this.m_PanelSetName; }
			set
			{
				if (this.m_PanelSetName != value)
				{
					this.m_PanelSetName = value;
					this.NotifyPropertyChanged("PanelSetName");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public int FinaledById
		{
			get { return this.m_FinaledById; }
			set
			{
				if (this.m_FinaledById != value)
				{
					this.m_FinaledById = value;
					this.NotifyPropertyChanged("FinaledById");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public bool Final
		{
			get { return this.m_Final; }
			set
			{
				if (this.m_Final != value)
				{
					this.m_Final = value;
					this.NotifyPropertyChanged("Final");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public Nullable<DateTime> FinalDate
		{
			get { return this.m_FinalDate; }
			set
			{
				if (this.m_FinalDate != value)
				{
					this.m_FinalDate = value;
					this.NotifyPropertyChanged("FinalDate");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public Nullable<DateTime> FinalTime
		{
			get { return this.m_FinalTime; }
			set
			{
				if (this.m_FinalTime != value)
				{
					this.m_FinalTime = value;
					this.NotifyPropertyChanged("FinalTime");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public int OrderedById
		{
			get { return this.m_OrderedById; }
			set
			{
				if (this.m_OrderedById != value)
				{
					this.m_OrderedById = value;
					this.NotifyPropertyChanged("OrderedById");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public Nullable<DateTime> OrderDate
		{
			get { return this.m_OrderDate; }
			set
			{
				if (this.m_OrderDate != value)
				{
					this.m_OrderDate = value;
					this.NotifyPropertyChanged("OrderDate");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public Nullable<DateTime> OrderTime
		{
			get { return this.m_OrderTime; }
			set
			{
				if (this.m_OrderTime != value)
				{
					this.m_OrderTime = value;
					this.NotifyPropertyChanged("OrderTime");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public string Signature
		{
			get { return this.m_Signature; }
			set
			{
				if (this.m_Signature != value)
				{
					this.m_Signature = value;
					this.NotifyPropertyChanged("Signature");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public int AssignedToId
		{
			get { return this.m_AssignedToId; }
			set
			{
				if (this.m_AssignedToId != value)
				{
					this.m_AssignedToId = value;
					this.NotifyPropertyChanged("AssignedToId");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public int TemplateId
		{
			get { return this.m_TemplateId; }
			set
			{
				if (this.m_TemplateId != value)
				{
					this.m_TemplateId = value;
					this.NotifyPropertyChanged("TemplateId");
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
		public string OriginatingLocation
		{
			get { return this.m_OriginatingLocation; }
			set
			{
				if (this.m_OriginatingLocation != value)
				{
					this.m_OriginatingLocation = value;
					this.NotifyPropertyChanged("OriginatingLocation");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public bool Audit
		{
			get { return this.m_Audit; }
			set
			{
				if (this.m_Audit != value)
				{
					this.m_Audit = value;
					this.NotifyPropertyChanged("Audit");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public bool BillingAudit
		{
			get { return this.m_BillingAudit; }
			set
			{
				if (this.m_BillingAudit != value)
				{
					this.m_BillingAudit = value;
					this.NotifyPropertyChanged("BillingAudit");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public bool SignatureAudit
		{
			get { return this.m_SignatureAudit; }
			set
			{
				if (this.m_SignatureAudit != value)
				{
					this.m_SignatureAudit = value;
					this.NotifyPropertyChanged("SignatureAudit");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public string ResultDocumentSource
		{
			get { return this.m_ResultDocumentSource; }
			set
			{
				if (this.m_ResultDocumentSource != value)
				{
					this.m_ResultDocumentSource = value;
					this.NotifyPropertyChanged("ResultDocumentSource");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public string ResultDocumentPath
		{
			get { return this.m_ResultDocumentPath; }
			set
			{
				if (this.m_ResultDocumentPath != value)
				{
					this.m_ResultDocumentPath = value;
					this.NotifyPropertyChanged("ResultDocumentPath");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public bool Published
		{
			get { return this.m_Published; }
			set
			{
				if (this.m_Published != value)
				{
					this.m_Published = value;
					this.NotifyPropertyChanged("Published");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public Nullable<DateTime> DateLastPublished
		{
			get { return this.m_DateLastPublished; }
			set
			{
				if (this.m_DateLastPublished != value)
				{
					this.m_DateLastPublished = value;
					this.NotifyPropertyChanged("DateLastPublished");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public string TechnicalComponentFacilityId
		{
			get { return this.m_TechnicalComponentFacilityId; }
			set
			{
				if (this.m_TechnicalComponentFacilityId != value)
				{
					this.m_TechnicalComponentFacilityId = value;
					this.NotifyPropertyChanged("TechnicalComponentFacilityId");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public bool HasTechnicalComponent
		{
			get { return this.m_HasTechnicalComponent; }
			set
			{
				if (this.m_HasTechnicalComponent != value)
				{
					this.m_HasTechnicalComponent = value;
					this.NotifyPropertyChanged("HasTechnicalComponent");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public string ProfessionalComponentFacilityId
		{
			get { return this.m_ProfessionalComponentFacilityId; }
			set
			{
				if (this.m_ProfessionalComponentFacilityId != value)
				{
					this.m_ProfessionalComponentFacilityId = value;
					this.NotifyPropertyChanged("ProfessionalComponentFacilityId");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public bool HasProfessionalComponent
		{
			get { return this.m_HasProfessionalComponent; }
			set
			{
				if (this.m_HasProfessionalComponent != value)
				{
					this.m_HasProfessionalComponent = value;
					this.NotifyPropertyChanged("HasProfessionalComponent");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public int AuditedById
		{
			get { return this.m_AuditedById; }
			set
			{
				if (this.m_AuditedById != value)
				{
					this.m_AuditedById = value;
					this.NotifyPropertyChanged("AuditedById");
				}
			}
		}

		[DataMember]
		[PersistentProperty()]
		public Nullable<DateTime> AuditDate
		{
			get { return this.m_AuditDate; }
			set
			{
				if (this.m_AuditDate != value)
				{
					this.m_AuditDate = value;
					this.NotifyPropertyChanged("AuditDate");
				}
			}
		}
	}
}