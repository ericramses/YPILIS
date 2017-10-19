using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class PAX5CD5DualStain : DualStain
	{
		public PAX5CD5DualStain()
		{
            this.m_TestId = "PAX5CD5";
			this.m_TestName = "PAX5/CD5";
            this.m_TestAbbreviation = "PAX5/CD5";
			this.m_FirstTest = new CD5();
			this.m_SecondTest = new PAX5();
            this.m_IsDualOrder = true;
            this.m_NeedsAcknowledgement = true;			
		}
	}
}
