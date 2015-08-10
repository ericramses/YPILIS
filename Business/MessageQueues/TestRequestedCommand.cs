using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Windows.Controls;

namespace YellowstonePathology.Business.MessageQueues
{
	public class TestRequestedCommand : MessageCommand
	{
		string m_ReportNo;
		string m_TestName;
		DateTime m_MessageDate;
        YellowstonePathology.Core.User.SystemUser m_SystemUser;

		public void SetCommandData(string reportNo, string testName, YellowstonePathology.Core.User.SystemUser systemUser)
		{
			this.m_ReportNo = reportNo;
			this.m_TestName = testName;
            this.m_SystemUser = systemUser;
			this.Label = testName + " has been requested from " + reportNo;
			this.m_MessageDate = DateTime.Now;
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

		public DateTime MessageDate
		{
			get { return this.m_MessageDate; }
			set { this.m_MessageDate = value; }
		}

		public YellowstonePathology.Core.User.SystemUser SystemUser
		{
			get { return this.m_SystemUser; }
			set { this.m_SystemUser = value; }
		}

		public override void Execute()
		{
			base.Execute();
			YellowstonePathology.Business.Domain.OrderCommentLog.LogEvent(0, string.Empty, ReportNo + ", " + TestName, this.m_SystemUser,
				YellowstonePathology.Business.Domain.OrderCommentEnum.SpecimenReceivedFromHistology);
		}
	}
}
