using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.HPVTWI
{
	public class HPVTWILowgDNAResult : HPVTWIResult
	{
        public HPVTWILowgDNAResult()
		{
            this.m_PreliminaryResultCode = "HPVTWILGDNA";
            this.m_A5A6Result = HPVTWIResult.LowDnaResult;
            this.m_A7Result = HPVTWIResult.LowDnaResult;
            this.m_A9Result = HPVTWIResult.LowDnaResult;

            this.m_OveralResultCode = "HPVTWIUNSTSFCTRY";
            this.m_OveralResult = HPVTWIResult.Unsatisfactory;
            this.m_Comment = HPVTWIResult.InsufficientComment;
		}

		public override void AcceptResults(YellowstonePathology.Business.Test.HPVTWI.HPVTWITestOrder panelSetOrder,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity)
		{
			if (panelSetOrder.PanelOrderCollection.Count == 2 && panelSetOrder.PanelOrderCollection[0].Accepted == false)
			{
				panelSetOrder.PanelOrderCollection[0].AcceptResults(systemIdentity.User);
			}
			else
			{
				base.AcceptResults(panelSetOrder, systemIdentity);
			}
		}
	}
}
