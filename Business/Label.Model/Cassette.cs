using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Label.Model
{
	public class Cassette 
	{
        private YellowstonePathology.Business.Test.AliquotOrder m_AliquotOrder;
		protected string m_Prefix = "$";
		protected string m_Delimeter = "#";
		protected string m_CassetteColumnDelimiter = "H";
		protected string m_CompanyId = "YPII";
		protected string m_ScanningPrefix = "ALQ";
		protected int m_CassetteColumn;
		protected string m_BlockTitle;
		protected string m_PatientInitials;		
		protected bool m_PrintRequested;
		protected bool m_Verified;
		protected string m_ReportNo;
		protected string m_MasterAccessionNo;        

        public Cassette()
		{
            
		}

        public YellowstonePathology.Business.Test.AliquotOrder AliquotOrder
        {
            get { return this.m_AliquotOrder; }
        }

		public string ReportNo
		{
			get
			{
				return this.m_ReportNo;
			}
			set
			{
				if (value != this.m_ReportNo)
				{
					this.m_ReportNo = value;					
				}
			}
		}

		public string MasterAccessionNo
		{
			get
			{
				return this.m_MasterAccessionNo;
			}
			set
			{
				if (value != this.m_MasterAccessionNo)
				{
					this.m_MasterAccessionNo = value;					
				}
			}
		}

        public string BlockTitle
        {
			get { return this.m_BlockTitle; }
			set
			{
				if (value != this.m_BlockTitle)
				{
					this.m_BlockTitle = value;					
				}
			}
        }
		
        public string PatientInitials
        {
			get { return this.m_PatientInitials; }
			set
			{
				if (value != this.m_PatientInitials)
				{
					this.m_PatientInitials = value;					
				}
			}
        }

        public int CassetteColumn
        {
            get { return this.m_CassetteColumn; }
			set
			{
				if (value != this.m_CassetteColumn)
				{
					this.m_CassetteColumn = value;					
				}
			}
        }

		public bool PrintRequested
		{
			get { return this.m_PrintRequested; }
			set
			{
				if (value != this.m_PrintRequested)
				{
					this.m_PrintRequested = value;					
				}
			}
		}

		public bool Verified
		{
			get { return this.m_Verified; }
			set
			{
				if (value != this.m_Verified)
				{
					this.m_Verified = value;					
				}
			}
		}

		public string FormattedCassetteColumn
		{
			get { return this.m_CassetteColumnDelimiter + this.m_CassetteColumn.ToString(); }
		}

        public string CompanyId
        {
            get { return this.m_CompanyId; }
        }

        public string ScanningId
        {
            get { return this.m_ScanningPrefix + this.m_AliquotOrder.AliquotOrderId; }            
        }

        public void FromAliquotOrder(YellowstonePathology.Business.Test.AliquotOrder aliquotOrder, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_AliquotOrder = aliquotOrder;
			YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(accessionOrder.MasterAccessionNo);

            YellowstonePathology.Business.PatientName patientName = new PatientName(accessionOrder.PLastName, accessionOrder.PFirstName);
            this.m_MasterAccessionNo = orderIdParser.MasterAccessionNo;
            this.m_AliquotOrder = aliquotOrder;
            this.m_BlockTitle = aliquotOrder.PrintLabel;
            this.m_Verified = aliquotOrder.GrossVerified;
            this.m_PatientInitials = patientName.GetInitials();
            this.m_CassetteColumn = accessionOrder.PrintMateColumnNumber;
        }

        public override string ToString()
        {
			YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(this.m_MasterAccessionNo);

            StringBuilder result = new StringBuilder(this.m_Prefix + this.m_Delimeter);
            result.Append(this.FormattedCassetteColumn + this.m_Delimeter);
            result.Append(orderIdParser.MasterAccessionNo + this.m_Delimeter);
            result.Append(this.BlockTitle + this.m_Delimeter);
            result.Append(this.PatientInitials + this.m_Delimeter);
            result.Append(this.CompanyId + this.m_Delimeter);
            result.Append(this.ScanningId + this.m_Delimeter);
            result.Append(orderIdParser.MasterAccessionNoYear.Value.ToString() + this.m_Delimeter);
            result.Append(orderIdParser.MasterAccessionNoNumber.Value.ToString());
            return result.ToString();
        }        
	}
}
