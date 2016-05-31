using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace YellowstonePathology.Business.Domain.Billing
{	
	public class BillingSpecimen : DomainBase
	{
		private string m_SpecimenOrderId;
		private string m_MasterAccessionNo;
        private Nullable<DateTime> m_CollectionDate;
        private int m_SpecimenNumber;
        private string m_Description;		

        private BillingReportCollection m_BillingReportCollection;

        public BillingSpecimen()
        {
            this.m_BillingReportCollection = new BillingReportCollection();
        }
		
		public BillingReport GetBillingReport(string reportNo)
		{
			BillingReport result = null;
			foreach (BillingReport billingReport in this.BillingReportCollection)
			{
				if (billingReport.ReportNo == reportNo)
				{
					result = billingReport;
					break;
				}
			}
			return result;
		}

        public BillingReportCollection BillingReportCollection
        {
            get { return this.m_BillingReportCollection; }
        }

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

        public int SpecimenNumber
        {
            get { return this.m_SpecimenNumber; }
            set 
            {
                if (this.m_SpecimenNumber != value)
                {
                    this.m_SpecimenNumber = value;
                    this.NotifyPropertyChanged("SpecimenNumber");					
				}
            }
        }

        public Nullable<DateTime> CollectionDate
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

        public void WriteProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyWriter propertyWriter)
        {
			this.m_SpecimenOrderId = propertyWriter.WriteString("SpecimenOrderId");
			this.m_MasterAccessionNo = propertyWriter.WriteString("MasterAccessionNo");
            this.m_CollectionDate = propertyWriter.WriteNullableDateTime("CollectionDate");
            this.m_SpecimenNumber = propertyWriter.WriteInt("SpecimenNumber");
            this.m_Description = propertyWriter.WriteString("Description");            
        }
	}
}
