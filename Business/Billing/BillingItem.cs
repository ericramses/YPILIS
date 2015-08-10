using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace YellowstonePathology.Business.Billing
{
	public class BillingItem
	{
		private string m_ReportNo;
		private Nullable<DateTime> m_BillingDate;
		private string m_Problem;
		private Nullable<DateTime> m_FinalDate;
        private string m_PanelSetName;

		public BillingItem()
		{
		}

		public string ReportNo
		{
			get { return this.m_ReportNo; }
			set { this.m_ReportNo = value; }
		}

		public Nullable<DateTime> FinalDate
		{
			get { return this.m_FinalDate; }
			set { this.m_FinalDate = value; }
		}

		public string Problem
		{
			get { return this.m_Problem; }
			set { this.m_Problem = value; }
		}

		public Nullable<DateTime> BillingDate
		{
			get { return this.m_BillingDate; }
			set { this.m_BillingDate = value; }
		}

        public string PanelSetName
		{
            get { return this.m_PanelSetName; }
            set { this.m_PanelSetName = value; }
		}
	}
}
