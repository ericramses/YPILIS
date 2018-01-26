using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class CMyc : ImmunoHistochemistryTest
	{
		public CMyc()
		{
			this.m_TestId = "362";
			this.m_TestName = "c-Myc";
            this.m_TestAbbreviation = "c-Myc";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
