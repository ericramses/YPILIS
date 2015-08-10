using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Rules.PanelOrder
{
	public class AcceptArupKrasReflx : Accept
	{
		public AcceptArupKrasReflx()
        {
			this.m_Rule.ActionList.Add(SetResult);
			this.m_Rule.ActionList.Add(AcceptPanel);
            this.m_Rule.ActionList.Add(Save);
        }

		private void SetResult()
        {
			this.m_PanelSetOrder.PanelSetResultOrderCollection.GetPanelSetResultOrder(212).Result = this.m_PanelOrderBeingAccepted.TestOrderCollection.GetTestOrder(211).Result;
			this.m_PanelSetOrder.PanelSetResultOrderCollection.GetPanelSetResultOrder(193).Result = this.m_PanelOrderBeingAccepted.TestOrderCollection.GetTestOrder(193).Result;
		}
	}
}
