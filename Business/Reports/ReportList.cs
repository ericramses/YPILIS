using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Reports
{
	public class ReportList : ObservableCollection<YellowstonePathology.Business.Reports.ReportBase>
	{
        public ReportList()
        {            
            this.Add(new YellowstonePathology.Business.Reports.Billing.YPIISummary());
        }
	}
}
