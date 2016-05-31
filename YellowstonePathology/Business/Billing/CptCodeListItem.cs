using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;

namespace YellowstonePathology.Business.Billing
{
	public class CptCodeListItem :INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private string m_CptCode;
        private int m_BillingReportOrder;

        public CptCodeListItem()
        {        
        }

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
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
        
		[YellowstonePathology.Business.CustomAttributes.SqlListItemFieldAttribute("BillingReportOrder", SqlDbType.VarChar)]
        public int BillingReportOrder
        {
            get { return this.m_BillingReportOrder; }
            set
            {
                if (value != this.m_BillingReportOrder)
                {
                    this.m_BillingReportOrder = value;
                    this.NotifyPropertyChanged("BillingReportOrder");
                }
            }
        }

		public void WriteProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyWriter propertyWriter)
		{
			this.m_BillingReportOrder = propertyWriter.WriteInt("BillingReportOrder");
			this.m_CptCode = propertyWriter.WriteString("CptCode");
		}

		public void Fill(SqlDataReader dr)
		{
			this.m_CptCode = BaseData.GetStringValue("CptCode", dr);
			this.m_BillingReportOrder = BaseData.GetIntValue("BillingReportOrder", dr);
		}
	}
}
