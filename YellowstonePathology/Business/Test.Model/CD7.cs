﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class CD7 : ImmunoHistochemistryTest
	{
		public CD7()
		{
			this.m_TestId = "180";
			this.m_TestName = "CD7";
            this.m_TestAbbreviation = "CD7";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
		}
	}
}
