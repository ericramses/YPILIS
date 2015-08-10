using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Rules.PanelOrder
{
	public class AcceptArupNgct : Accept
	{
		public AcceptArupNgct()
        {                                                
            this.m_Rule.ActionList.Add(SetResult);
			this.m_Rule.ActionList.Add(AcceptPanel);
            this.m_Rule.ActionList.Add(Save);
        }
        
        private void SetResult()
        {
			this.m_PanelSetOrder.PanelSetResultOrderCollection.GetPanelSetResultOrder(25).Result = this.m_PanelOrderBeingAccepted.TestOrderCollection.GetTestOrder(25).Result;
			this.m_PanelSetOrder.PanelSetResultOrderCollection.GetPanelSetResultOrder(26).Result = this.m_PanelOrderBeingAccepted.TestOrderCollection.GetTestOrder(26).Result;
        }
	}
}
