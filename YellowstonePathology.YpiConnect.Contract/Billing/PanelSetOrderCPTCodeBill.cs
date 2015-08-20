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
	public class PanelSetOrderCPTCodeBill : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private string m_PanelSetOrderCPTCodeBillId;	
		private string m_ReportNo;
        private int m_ClientId;	
		private int m_Quantity;		
		private string m_CPTCode;
		private string m_Modifier;		
		private string m_FeeType;
        private Nullable<DateTime> m_PostDate;
        private string m_BillTo;
        private string m_BillBy;

        public PanelSetOrderCPTCodeBill()
        {

        }

		[DataMember]
		public string PanelSetOrderCPTCodeBillId
        {
            get { return this.m_PanelSetOrderCPTCodeBillId; }
            set
            {
                if (this.m_PanelSetOrderCPTCodeBillId != value)
                {
                    this.m_PanelSetOrderCPTCodeBillId = value;
                    this.NotifyPropertyChanged("PanelSetOrderCPTCodeBillId");
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

		[DataMember]
		public string BillTo
        {
            get { return this.m_BillTo; }
            set
            {
                if (this.m_BillTo != value)
                {
                    this.m_BillTo = value;
                    this.NotifyPropertyChanged("BillTo");
                }
            }
        }

		[DataMember]
		public string BillBy
        {
            get { return this.m_BillBy; }
            set
            {
                if (this.m_BillBy != value)
                {
                    this.m_BillBy = value;
                    this.NotifyPropertyChanged("BillBy");
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
            this.m_PanelSetOrderCPTCodeBillId = propertyWriter.WriteString("PanelSetOrderCPTCodeBillId");            
			this.m_ReportNo = propertyWriter.WriteString("ReportNo");			
			this.m_Quantity = propertyWriter.WriteInt("Quantity");			
			this.m_CPTCode = propertyWriter.WriteString("CPTCode");
			this.m_Modifier = propertyWriter.WriteString("Modifier");			
			this.m_FeeType = propertyWriter.WriteString("FeeType");
            this.m_PostDate = propertyWriter.WriteNullableDateTime("PostDate");
            this.m_BillTo = propertyWriter.WriteString("BillTo");
            this.m_BillBy = propertyWriter.WriteString("BillBy");            
		}
		
		public void ReadProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyReader propertyReader)
		{
			throw new NotImplementedException("ReadProperties not implemented in PanelSetOrderCPTCodeBill");
		}

        public void From(PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill)
        {
            this.m_ReportNo = panelSetOrderCPTCodeBill.ReportNo;
            this.m_CPTCode = panelSetOrderCPTCodeBill.CPTCode;
            this.m_Quantity = panelSetOrderCPTCodeBill.Quantity;
            this.m_Modifier = panelSetOrderCPTCodeBill.Modifier;
            this.m_FeeType = panelSetOrderCPTCodeBill.FeeType;
            this.m_PostDate = panelSetOrderCPTCodeBill.PostDate;
            this.m_BillTo = panelSetOrderCPTCodeBill.BillTo;
            this.m_BillBy = panelSetOrderCPTCodeBill.BillBy;
        }
        
        public void FromXml(XElement xml)
        {
			throw new NotImplementedException("FromXml not implemented in PanelSetOrderCPTCodeBill");
        }

        public XElement ToXml()
        {
			throw new NotImplementedException("ToXml not implemented in PanelSetOrderCPTCodeBill");
        }
	}
}

















