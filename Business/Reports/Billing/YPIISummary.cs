using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;

namespace YellowstonePathology.Business.Reports.Billing
{
	class YPIISummary : YellowstonePathology.Business.Reports.ReportBase
	{               
        DateTime m_StartDate;
        DateTime m_EndDate;
        string m_Location;

        public YPIISummary()
        {
            this.m_ReportName = "YPII Billing Summary";
            this.m_ReportCategory = ReportCategory.Billing;

            this.m_StartDate = DateTime.Parse("1/" + DateTime.Today.Day.ToString() + "/" + DateTime.Today.Year.ToString());
            this.m_EndDate = DateTime.Today;
            this.m_Location = "Billings";
        }	      		

        public DateTime StartDate
        {
            get { return this.m_StartDate; }
            set { this.m_StartDate = value; }
        }

        public DateTime EndDate
        {
            get { return this.m_EndDate; }
            set { this.m_EndDate = value; }
        }

        public string Location
        {
            get { return this.m_Location; }
            set { this.m_Location = value; }
        }

        public override void SetParameterUIVisibility(List<UIParameterListItem> uiParameterList)
        {
            foreach (UIParameterListItem listItem in uiParameterList)
            {
                switch (listItem.Name)
                {
                    case "StartDate":
                    case "EndDate":
                    case "BillingLocation":
                        listItem.Visibility = System.Windows.Visibility.Visible;
                        break;
                    default:
                        listItem.Visibility = System.Windows.Visibility.Collapsed;
                        break;
                }                
            }
        }

        public override void PublishReport()
        {
               
        }

        public override void ViewReport()
        {
            YPIISummaryList list = new YPIISummaryList();
			list.FillByDateRange(this.m_StartDate, this.m_EndDate);
            System.Windows.MessageBox.Show(list.Count.ToString());
        }        
    }

	[XmlType("YPIISummaryList")]
	public class YPIISummaryList : ObservableCollection<YPIISummaryListItem>
    {

        public YPIISummaryList()
        {
        }

        public void FillByDateRange(DateTime startDate, DateTime endDate)
        {
			this.Clear();
			YPIISummaryList summaryList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetYPIISummaryListByDateRange(startDate, endDate);
			if (summaryList != null)
			{
				foreach (YPIISummaryListItem item in summaryList) this.Add(item);
			}
        }
    }

	[XmlType("YPIISummaryListItem")]
	public class YPIISummaryListItem : ListItem
	{
		string m_CptCode;

		public YPIISummaryListItem()
		{
		}

		public string CptCode
		{
			get { return this.m_CptCode; }
			set { this.m_CptCode = value; }
		}
	}
}
