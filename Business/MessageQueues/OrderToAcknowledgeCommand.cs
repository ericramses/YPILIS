using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.MessageQueues
{
	public class OrderToAcknowledgeCommand : MessageCommand
	{
		private string m_MasterAccessionNo;
		private string m_PanelOrderId;
		private string m_ReportNo;
		private string m_TestName;
		private int m_AcknowledgedById;
		private Nullable<DateTime> m_AcknowledgementDate;
		private Nullable<DateTime> m_AcknowledgementTime;

		public void SetCommandData(string masterAccessionNo, string panelOrderId, string reportNo, string testName)
		{
			this.m_MasterAccessionNo = masterAccessionNo;
			this.m_PanelOrderId = panelOrderId;
			this.m_ReportNo = reportNo;
			m_AcknowledgedById = 0;
			m_AcknowledgementDate = null;
			m_AcknowledgementTime = null;
			this.Label = m_TestName + " to acknowledge for " + m_ReportNo;
		}

		public string MasterAccessionNo
		{
			get { return this.m_MasterAccessionNo; }
			set { this.m_MasterAccessionNo = value; }
		}

		public string PanelOrderId
		{
			get { return this.m_PanelOrderId; }
			set { this.m_PanelOrderId = value; }
		}

		public string ReportNo
		{
			get { return this.m_ReportNo; }
			set { this.m_ReportNo = value; }
		}

		public string TestName
		{
			get { return this.m_TestName; }
			set { this.m_TestName = value; }
		}

		public int AcknowledgedById
		{
			get { return this.m_AcknowledgedById; }
			set { this.m_AcknowledgedById = value; }
		}

		public Nullable<DateTime> AcknowledgementDate
		{
			get { return this.m_AcknowledgementDate; }
			set { this.m_AcknowledgementDate = value; }
		}

		public Nullable<DateTime> AcknowledgementTime
		{
			get { return this.m_AcknowledgementTime; }
			set { this.m_AcknowledgementTime = value; }
		}      
	}
}
