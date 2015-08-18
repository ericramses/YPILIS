using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.HPVTWI
{
	public class HPVTWILowgDNARepeatResult : HPVTWIResult
	{
        public HPVTWILowgDNARepeatResult()
		{
            this.m_PreliminaryResultCode = "HPVTWILGDNA";
            this.m_A5A6Result = HPVTWIResult.LowDnaResult;
            this.m_A7Result = HPVTWIResult.LowDnaResult;
            this.m_A9Result = HPVTWIResult.LowDnaResult;

            this.m_OveralResultCode = "HPVTWIUNSTSFCTRY";
            this.m_OveralResult = HPVTWIResult.Unsatisfactory;
            this.m_Comment = HPVTWIResult.InsufficientComment;
		}

		public override void SetResult(YellowstonePathology.Business.Test.HPVTWI.HPVTWITestOrder panelSetOrder,
			YellowstonePathology.Business.Test.HPVTWI.PanelOrderHPVTWI panelOrder,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity)
		{
			panelOrder.A5A6Result = this.m_A5A6Result;
			panelOrder.A7Result = this.m_A7Result;
			panelOrder.A9Result = this.m_A9Result;
			panelOrder.ResultCode = this.m_PreliminaryResultCode;

			YellowstonePathology.Business.Rules.MethodResult methodResult = HPVTWIResult.IsOkToAddSecondPanelOrder(panelSetOrder);
			if (methodResult.Success == true)
			{
				this.AddSecondPanelOrder(panelSetOrder, systemIdentity);
				panelSetOrder.ResultCode = null;
				panelSetOrder.Result = null;
				panelSetOrder.TestInformation = null;
				panelSetOrder.References = null;
				panelSetOrder.Comment = null;
			}
			else
			{
				panelSetOrder.ResultCode = this.m_OveralResultCode;
				panelSetOrder.Result = this.m_OveralResult;
				panelSetOrder.TestInformation = TestInformation;
				panelSetOrder.References = References;
				panelSetOrder.Comment = this.m_Comment;
			}
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
