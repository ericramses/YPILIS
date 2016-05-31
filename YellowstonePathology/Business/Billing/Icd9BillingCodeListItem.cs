using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Billing
{
	public class Icd9BillingCodeListItem : ListItem
	{
		private string m_Icd9BillingId;
		private string m_Icd9Code;
		private int m_Quantity;
		private string m_CptBillingId;
		private string m_SpecimenOrderId;
		private string m_ReportNo;
		private string m_MasterAccessionNo;

		public Icd9BillingCodeListItem()
		{
		}

		[YellowstonePathology.Business.CustomAttributes.SqlListItemFieldAttribute("Icd9BillingId", SqlDbType.VarChar)]
		public string Icd9BillingId
		{
			get { return this.m_Icd9BillingId; }
			set
			{
				if (value != this.m_Icd9BillingId)
				{
					this.m_Icd9BillingId = value;
					this.NotifyPropertyChanged("Icd9BillingId");
				}
			}
		}

		[YellowstonePathology.Business.CustomAttributes.SqlListItemFieldAttribute("Icd9Code", SqlDbType.VarChar)]
		public string Icd9Code
		{
			get { return this.m_Icd9Code; }
			set
			{
				if (value != this.m_Icd9Code)
				{
					this.m_Icd9Code = value;
					this.NotifyPropertyChanged("Icd9Code");
				}
			}
		}

		[YellowstonePathology.Business.CustomAttributes.SqlListItemFieldAttribute("Quantity", SqlDbType.Int)]
		public int Quantity
		{
			get { return this.m_Quantity; }
			set
			{
				if (value != this.m_Quantity)
				{
					this.m_Quantity = value;
					this.NotifyPropertyChanged("Quantity");
				}
			}
		}

		[YellowstonePathology.Business.CustomAttributes.SqlListItemFieldAttribute("CptBillingId", SqlDbType.VarChar)]
		public string CptBillingId
		{
			get { return this.m_CptBillingId; }
			set
			{
				if (value != this.m_CptBillingId)
				{
					this.m_CptBillingId = value;
					this.NotifyPropertyChanged("CptBillingId");
				}
			}
		}

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

		public string MasterAccessionNo
		{
			get { return this.m_MasterAccessionNo; }
			set
			{
				if (value != this.m_MasterAccessionNo)
				{
					this.m_MasterAccessionNo = value;
					this.NotifyPropertyChanged("MasterAccessionNo");
				}
			}
		}

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

		public override void Fill(SqlDataReader dr)
		{
			this.m_Icd9BillingId = BaseData.GetStringValue("Icd9BillingId", dr);
			this.m_Icd9Code = BaseData.GetStringValue("Icd9Code", dr);
			this.m_Quantity = BaseData.GetIntValue("Quantity", dr);
			this.m_CptBillingId = BaseData.GetStringValue("CptBillingId", dr);
			this.m_ReportNo = BaseData.GetStringValue("ReportNo", dr);
			this.m_MasterAccessionNo = BaseData.GetStringValue("MasterAccessionNo", dr);
		}

		public void WriteProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyWriter propertyWriter)
		{
			this.m_Icd9BillingId = propertyWriter.WriteString("Icd9BillingId");
			this.m_Icd9Code = propertyWriter.WriteString("Icd9Code");
			this.m_Quantity = propertyWriter.WriteInt("Quantity");
			this.m_CptBillingId = propertyWriter.WriteString("CptBillingId");
			this.m_SpecimenOrderId = propertyWriter.WriteString("SpecimenOrderId");
			this.m_ReportNo = propertyWriter.WriteString("ReportNo");
			this.m_MasterAccessionNo = propertyWriter.WriteString("MasterAccessionNo");
		}

		public XElement ToXml()
		{
			XElement result = new XElement("Icd9BillingCodeListItem",
				new XElement("Icd9BillingId", this.m_Icd9BillingId),
				new XElement("Icd9Code", this.m_Icd9Code),
				new XElement("Quantity", this.m_Quantity.ToString()),
				new XElement("CptBillingId", this.m_CptBillingId),
				new XElement("SpecimenOrderId", this.m_SpecimenOrderId),
				new XElement("ReportNo", this.m_ReportNo),
				new XElement("MasterAccessionNo", this.m_MasterAccessionNo));

			return result;
		}
	}
}
