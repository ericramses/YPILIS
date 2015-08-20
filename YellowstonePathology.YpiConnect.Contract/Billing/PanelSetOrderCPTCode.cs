using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using YellowstonePathology.Business.Persistence;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace YellowstonePathology.YpiConnect.Contract.Billing
{
	[DataContract]
	public class PanelSetOrderCPTCode : INotifyPropertyChanged
	{			
        public event PropertyChangedEventHandler PropertyChanged;

		private string m_PanelSetOrderCPTCodeId;
		private string m_ReportNo;
        private int m_ClientId;
		private int m_Quantity;		
		private string m_CPTCode;
		private string m_Modifier;		
		private string m_FeeType;			
        private string m_CodeableType;
        private string m_CodeableDescription;
        private string m_EntryType;
		private string m_SpecimenOrderId;
        private Nullable<DateTime> m_PostDate;

        public PanelSetOrderCPTCode()
        {

        }

		public string PostDateProxy
		{
			get
			{
				if (this.PostDate.HasValue == false)
				{
					return null;
				}
				else
				{
					return this.PostDate.Value.ToShortDateString();
				}
			}

			set
			{
				string strValue = value.ToString();
				if (strValue == string.Empty)
				{
					this.PostDate = null;
				}
				else
				{
					this.PostDate = DateTime.Parse(strValue);
				}
			}
		}

		[DataMember]
		public string PanelSetOrderCPTCodeId
		{
            get { return this.m_PanelSetOrderCPTCodeId; }
			set
			{
                if (this.m_PanelSetOrderCPTCodeId != value)
				{
                    this.m_PanelSetOrderCPTCodeId = value;
                    this.NotifyPropertyChanged("PanelSetOrderCPTCodeId");
				}
			}
		}

		[DataMember]
		public string ReportNo
		{
			get { return this.m_ReportNo; }
			set
			{
				if(this.m_ReportNo != value)
				{
					this.m_ReportNo = value;
					this.NotifyPropertyChanged("ReportNo");
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
		public int Quantity
		{
			get { return this.m_Quantity; }
			set
			{
				if(this.m_Quantity != value)
				{
					this.m_Quantity = value;
					this.NotifyPropertyChanged("Quantity");
				}
			}
		}

		[DataMember]
		public string CPTCode
		{
			get { return this.m_CPTCode; }
			set
			{
				if(this.m_CPTCode != value)
				{
					this.m_CPTCode = value;
					this.NotifyPropertyChanged("CPTCode");
				}
			}
		}

		[DataMember]
		public string Modifier
		{
			get { return this.m_Modifier; }
			set
			{
				if(this.m_Modifier != value)
				{
					this.m_Modifier = value;
					this.NotifyPropertyChanged("Modifier");
				}
			}
		}

		[DataMember]
		public string CodeableDescription
		{
			get { return this.m_CodeableDescription; }
			set
			{
                if (this.m_CodeableDescription != value)
				{
                    this.m_CodeableDescription = value;
                    this.NotifyPropertyChanged("CodeableDescription");
				}
			}
		}

		[DataMember]
		public string FeeType
		{
			get { return this.m_FeeType; }
			set
			{
				if(this.m_FeeType != value)
				{
					this.m_FeeType = value;
					this.NotifyPropertyChanged("FeeType");
				}
			}
		}

		[DataMember]
		public string CodeableType
        {
            get { return this.m_CodeableType; }
            set
            {
                if (this.m_CodeableType != value)
                {
                    this.m_CodeableType = value;
                    this.NotifyPropertyChanged("CodeableType");
                }
            }
        }

		[DataMember]
		public string EntryType
        {
            get { return this.m_EntryType; }
            set
            {
                if (this.m_EntryType != value)
                {
                    this.m_EntryType = value;
                    this.NotifyPropertyChanged("EntryType");
                }
            }
        }

		[DataMember]
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
		public Nullable<DateTime> PostDate
        {
            get { return this.m_PostDate; }
            set
            {
                if (this.m_PostDate != value)
                {
                    this.m_PostDate = value;
                    this.NotifyPropertyChanged("PostDate");
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
		
		public void WriteProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyWriter propertyWriter)
		{
            this.m_PanelSetOrderCPTCodeId = propertyWriter.WriteString("PanelSetOrderCPTCodeId");
			this.m_ReportNo = propertyWriter.WriteString("ReportNo");
            this.m_ClientId = propertyWriter.WriteInt("ClientId");			
			this.m_Quantity = propertyWriter.WriteInt("Quantity");			
			this.m_CPTCode = propertyWriter.WriteString("CPTCode");
			this.m_Modifier = propertyWriter.WriteString("Modifier");			
			this.m_FeeType = propertyWriter.WriteString("FeeType");            
            this.m_CodeableType = propertyWriter.WriteString("CodeableType");
            this.m_CodeableDescription = propertyWriter.WriteString("CodeableDescription");
            this.m_EntryType = propertyWriter.WriteString("EntryType");
			this.m_SpecimenOrderId = propertyWriter.WriteString("SpecimenOrderId");
            this.m_PostDate = propertyWriter.WriteNullableDateTime("PostDate");
		}
		
		public void ReadProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyReader propertyReader)
		{
			throw new NotImplementedException("ReadProperties not implemented in PanelSetOrderCPTCode");
		}
        
        public void FromXml(XElement xml)
        {
			throw new NotImplementedException("FromXml not implemented in PanelSetOrderCPTCode");
        }

        public XElement ToXml()
        {
			throw new NotImplementedException("ToXml not implemented in PanelSetOrderCPTCode");
        }
	}
}

















