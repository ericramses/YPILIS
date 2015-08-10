using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Billing
{
	public class CptBillingCodeListItem : ListItem
	{
		private string m_CptCode;
		private Nullable<DateTime> m_BillingDate;
		private int m_Quantity;
		private string m_PrimaryInsurance;
		private string m_SecondaryInsurance;
		private string m_PatientType;
		private string m_BillingType;
		private bool m_NoCharge;
		private string m_Modifier;
		private string m_Description;
		private string m_FeeType;
		private int m_CodeOrder;
		private string m_CptBillingCodeId;
		private string m_SpecimenOrderId;
		private string m_ReportNo;
		private string m_MasterAccessionNo;

		public CptBillingCodeListItem()
		{
		}

		[YellowstonePathology.Business.CustomAttributes.SqlListItemFieldAttribute("CptBillingCodeId", SqlDbType.VarChar)]
		public string CptBillingCodeId
		{
			get { return this.m_CptBillingCodeId; }
			set
			{
				if (value != this.m_CptBillingCodeId)
				{
					this.m_CptBillingCodeId = value;
					this.NotifyPropertyChanged("CptBillingCodeId");
				}
			}
		}

		[YellowstonePathology.Business.CustomAttributes.SqlListItemFieldAttribute("CptCode", SqlDbType.VarChar)]
		public string CptCode
		{
			get { return this.m_CptCode; }
			set
			{
				if (value != this.m_CptCode)
				{
					this.m_CptCode = value;
					this.NotifyPropertyChanged("CptCode");
				}
			}
		}

		[YellowstonePathology.Business.CustomAttributes.SqlListItemFieldAttribute("BillingDate", SqlDbType.VarChar)]
		public Nullable<DateTime> BillingDate
		{
			get { return this.m_BillingDate; }
			set
			{
				if (value != this.m_BillingDate)
				{
					this.m_BillingDate = value;
					this.NotifyPropertyChanged("BillingDate");
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

		[YellowstonePathology.Business.CustomAttributes.SqlListItemFieldAttribute("PrimaryInsurance", SqlDbType.VarChar)]
		public string PrimaryInsurance
		{
			get { return this.m_PrimaryInsurance; }
			set
			{
				if (value != this.m_PrimaryInsurance)
				{
					this.m_PrimaryInsurance = value;
					this.NotifyPropertyChanged("PrimaryInsurance");
				}
			}
		}

		[YellowstonePathology.Business.CustomAttributes.SqlListItemFieldAttribute("SecondaryInsurance", SqlDbType.VarChar)]
		public string SecondaryInsurance
		{
			get { return this.m_SecondaryInsurance; }
			set
			{
				if (value != this.m_SecondaryInsurance)
				{
					this.m_SecondaryInsurance = value;
					this.NotifyPropertyChanged("SecondaryInsurance");
				}
			}
		}

		[YellowstonePathology.Business.CustomAttributes.SqlListItemFieldAttribute("PatientType", SqlDbType.VarChar)]
		public string PatientType
		{
			get { return this.m_PatientType; }
			set
			{
				if (value != this.m_PatientType)
				{
					this.m_PatientType = value;
					this.NotifyPropertyChanged("PatientType");
				}
			}
		}

		[YellowstonePathology.Business.CustomAttributes.SqlListItemFieldAttribute("BillingType", SqlDbType.VarChar)]
		public string BillingType
		{
			get { return this.m_BillingType; }
			set
			{
				if (value != this.m_BillingType)
				{
					this.m_BillingType = value;
					this.NotifyPropertyChanged("BillingType");
				}
			}
		}

		[YellowstonePathology.Business.CustomAttributes.SqlListItemFieldAttribute("NoCharge", SqlDbType.Bit)]
		public bool NoCharge
		{
			get { return this.m_NoCharge; }
			set
			{
				if (value != this.m_NoCharge)
				{
					this.m_NoCharge = value;
					this.NotifyPropertyChanged("NoCharge");
				}
			}
		}

		[YellowstonePathology.Business.CustomAttributes.SqlListItemFieldAttribute("Modifier", SqlDbType.VarChar)]
		public string Modifier
		{
			get { return this.m_Modifier; }
			set
			{
				if (value != this.m_Modifier)
				{
					this.m_Modifier = value;
					this.NotifyPropertyChanged("Modifier");
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

		[YellowstonePathology.Business.CustomAttributes.SqlListItemFieldAttribute("FeeType", SqlDbType.VarChar)]
		public string FeeType
		{
			get { return this.m_FeeType; }
			set
			{
				if (value != this.m_FeeType)
				{
					this.m_FeeType = value;
					this.NotifyPropertyChanged("FeeType");
				}
			}
		}

		[YellowstonePathology.Business.CustomAttributes.SqlListItemFieldAttribute("CodeOrder", SqlDbType.Int)]
		public int CodeOrder
		{
			get { return this.m_CodeOrder; }
			set
			{
				if (value != this.m_CodeOrder)
				{
					this.m_CodeOrder = value;
					this.NotifyPropertyChanged("CodeOrder");
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

		public void WriteProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyWriter propertyWriter)
		{
			this.m_CptBillingCodeId = propertyWriter.WriteString("CptBillingId");
			this.m_CptCode = propertyWriter.WriteString("CptCode");
			this.m_BillingDate = propertyWriter.WriteNullableDateTime("BillingDate");
			this.m_Quantity = propertyWriter.WriteInt("Quantity");
			this.m_PrimaryInsurance = propertyWriter.WriteString("PrimaryInsurance");
			this.m_SecondaryInsurance = propertyWriter.WriteString("SecondaryInsurance");
			this.PatientType = propertyWriter.WriteString("PatientType");
			this.m_BillingType = propertyWriter.WriteString("BillingType");
			this.m_NoCharge = propertyWriter.WriteBoolean("NoCharge");
			this.m_Modifier = propertyWriter.WriteString("Modifier");
			this.m_Description = propertyWriter.WriteString("Description");
			this.m_FeeType = propertyWriter.WriteString("FeeType");
			this.m_SpecimenOrderId = propertyWriter.WriteString("SpecimenOrderId");
			this.m_ReportNo = propertyWriter.WriteString("ReportNo");
			this.m_MasterAccessionNo = propertyWriter.WriteString("MasterAccessionNo");
		}

		public XElement ToXml()
		{
			XElement result = new XElement("CptBillingCodeListItem",
				new XElement("CptBillingId", this.m_CptBillingCodeId),
				new XElement("CptCode", this.m_CptCode),
				new XElement("BillingDate", this.m_BillingDate.HasValue ? this.m_BillingDate.Value.ToShortDateString() : string.Empty),
				new XElement("Quantity", this.m_Quantity.ToString()),
				new XElement("PrimaryInsurance", this.m_PrimaryInsurance),
				new XElement("SecondaryInsurance", this.m_SecondaryInsurance),
				new XElement("PatientType", this.PatientType),
				new XElement("BillingType", this.m_BillingType),
				new XElement("NoCharge", Convert.ToString(this.m_NoCharge)),
				new XElement("Modifier", this.m_Modifier),
				new XElement("Description", this.m_Description),
				new XElement("FeeType", this.m_FeeType),
				new XElement("SpecimenOrderId", this.m_SpecimenOrderId),
				new XElement("ReportNo", this.m_ReportNo),
				new XElement("MasterAccessionNo", this.m_MasterAccessionNo));

			return result;
		}
	}
}
