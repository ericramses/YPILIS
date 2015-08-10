using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Rules.PanelOrder
{
	class AcceptArupCysticFibrosis : Accept
	{
		public AcceptArupCysticFibrosis()
        {                                                
            this.m_Rule.ActionList.Add(SetResult);
			this.m_Rule.ActionList.Add(AcceptPanel);
            this.m_Rule.ActionList.Add(Save);
        }
        
        private void SetResult()
        {
			this.m_PanelSetOrder.PanelSetResultOrderCollection.GetPanelSetResultOrder(34).Result = this.m_PanelOrderBeingAccepted.TestOrderCollection.GetTestOrder(34).Result;
        }
	}
}
