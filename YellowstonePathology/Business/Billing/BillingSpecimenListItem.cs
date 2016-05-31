using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Data;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Billing
{
    public class BillingSpecimenListItem : ListItem
    {
        private string m_SpecimenOrderId;
		private string m_ReportNo;
		private string m_Description;

        CptBillingCodeList m_CptBillingCodeList;
        Icd9BillingCodeList m_Icd9BillingCodeList;

        public BillingSpecimenListItem()
        {
            this.m_CptBillingCodeList = new CptBillingCodeList();
            this.m_Icd9BillingCodeList = new Icd9BillingCodeList();
        }

        public CptBillingCodeList CptBillingCodeList
        {
            get { return this.m_CptBillingCodeList; }
        }

        public Icd9BillingCodeList Icd9BillingCodeList
        {
            get { return this.m_Icd9BillingCodeList; }
        }

		[YellowstonePathology.Business.CustomAttributes.SqlListItemFieldAttribute("SpecimenOrderId", SqlDbType.VarChar)]
        public string SpecimenOrderId
        {
            get { return this.m_SpecimenOrderId; }
            set
            {
                if (value != this.m_SpecimenOrderId)
                {
                    this.m_SpecimenOrderId = value;
                    this.NotifyPropertyChanged("SpecimenOrderId");
                }
            }
        }

        [YellowstonePathology.Business.CustomAttributes.SqlListItemFieldAttribute("ReportNo", SqlDbType.VarChar)]
        public string ReportNo
        {
            get { return this.m_ReportNo; }
            set
            {
                if (value != this.m_ReportNo)
                {
                    this.m_ReportNo = value;
                    this.NotifyPropertyChanged("ReportNo");
                }
            }
        }

        [YellowstonePathology.Business.CustomAttributes.SqlListItemFieldAttribute("Description", SqlDbType.VarChar)]
        public string Description
        {
            get { return this.m_Description; }
            set
            {
                if (value != this.m_Description)
                {
                    this.m_Description = value;
                    this.NotifyPropertyChanged("Description");
                }
            }
        }

		public void WriteProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyWriter propertyWriter)
		{
			this.m_SpecimenOrderId = propertyWriter.WriteString("SpecimenOrderId");
			this.m_ReportNo = propertyWriter.WriteString("ReportNo");
			this.m_Description = propertyWriter.WriteString("Description");
		}

		public XElement ToXml()
		{
			XElement result = new XElement("BillingSpecimenListItem",
				new XElement("SpecimenOrderId", this.m_SpecimenOrderId),
				new XElement("ReportNo", this.m_ReportNo),
				new XElement("Description", this.m_Description));

			result.Add(this.m_CptBillingCodeList.ToXml());
			result.Add(this.m_Icd9BillingCodeList.ToXml());

			return result;
		}
	}
}
