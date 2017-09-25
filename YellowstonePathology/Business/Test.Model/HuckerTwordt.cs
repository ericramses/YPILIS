using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class HuckerTwort : CytochemicalForMicroorganisms
	{
		public HuckerTwort()
		{
			this.m_TestId = "112";
			this.m_TestName = "Hucker-Twort";
            this.m_TestAbbreviation = "Hucker-Twort";
			this.m_Active = true;
			this.m_NeedsAcknowledgement = true;
            this.m_PerformedByHand = true;
        }
	}
}
