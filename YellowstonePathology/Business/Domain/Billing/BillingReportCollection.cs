using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace YellowstonePathology.Business.Domain.Billing
{	
	public class BillingReportCollection : ObservableCollection<BillingReport>
    {
		public BillingReportCollection()
		{
		}

		public BillingReport GetBillingReport(string reportNo)
		{
			BillingReport result = null;
			foreach (BillingReport billingReport in this)
			{
				if (billingReport.ReportNo == reportNo)
				{
					result = billingReport;
					break;
				}
			}
			return result;
		}
	}
}
