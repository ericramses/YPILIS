using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Domain.Billing
{
	[PersistentClass("tblPanelSetOrder", "YPIDATA")]
	public class BillingReport : DomainBase
    {
		private string m_ObjectId;
		private string m_ReportNo;
		private string m_MasterAccessionNo;
        private int m_PanelSetId;
        private string m_PanelSetName;        
        private DateTime m_OrderDate;
        private Nullable<DateTime> m_FinalDate;
        private string m_AssignedTo;
		private bool m_Audit;
        private bool m_HasProfessionalComponent;
        private string m_ProfessionalComponentFacilityId;
        private bool m_HasTechnicalComponent;
        private string m_TechnicalComponentFacilityId;        
        
        private YellowstonePathology.Business.Billing.CptBillingCodeItemCollection m_CptBillingCodeCollection;       

        public BillingReport()
        {
            this.m_CptBillingCodeCollection = new Business.Billing.CptBillingCodeItemCollection();            
        }        

        public YellowstonePathology.Business.Billing.CptBillingCodeItemCollection CptBillingCodeCollection
        {
            get { return this.m_CptBillingCodeCollection; }
        }

		[PersistentDocumentIdProperty()]
		public string ObjectId
		{
			get { return this.m_ObjectId; }
			set
			{
				if (this.m_ObjectId != value)
				{
					this.m_ObjectId = value;
					this.NotifyPropertyChanged("ObjectId");
				}
			}
		}

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

        public DateTime OrderDate
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

        public string AssignedTo
        {
            get { return this.m_AssignedTo; }
            set
            {
                if (this.m_AssignedTo != value)
                {
                    this.m_AssignedTo = value;
                    this.NotifyPropertyChanged("AssignedTo");
                }
            }
        }

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
        
        public void WriteProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyWriter propertyWriter)
        {
            this.m_ReportNo = propertyWriter.WriteString("ReportNo");
            this.m_MasterAccessionNo = propertyWriter.WriteString("MasterAccessionNo");
            this.m_PanelSetId = propertyWriter.WriteInt("PanelSetId");
            this.m_PanelSetName = propertyWriter.WriteString("PanelSetName");            
            this.m_OrderDate = propertyWriter.WriteDateTime("OrderDate");
            this.m_FinalDate = propertyWriter.WriteNullableDateTime("FinalDate");
            this.m_AssignedTo = propertyWriter.WriteString("AssignedTo");
			this.m_Audit = propertyWriter.WriteBoolean("Audit");
            this.m_HasProfessionalComponent = propertyWriter.WriteBoolean("HasProfessionalComponent");
            this.m_ProfessionalComponentFacilityId = propertyWriter.WriteString("ProfessionalComponentFacilityId");
            this.m_HasTechnicalComponent = propertyWriter.WriteBoolean("HasTechnicalComponent");
            this.m_TechnicalComponentFacilityId = propertyWriter.WriteString("TechnicalComponentFacilityId");
			this.m_ObjectId = propertyWriter.WriteString("ObjectId");
		}
	}
}
