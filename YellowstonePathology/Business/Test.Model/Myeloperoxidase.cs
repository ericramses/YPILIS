using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class Myeloperoxidase : ImmunoHistochemistryTest
	{
		public Myeloperoxidase()
		{
			this.m_TestId = "126";
			this.m_TestName = "Myeloperoxidase";
            this.m_TestAbbreviation = "Myelop";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
