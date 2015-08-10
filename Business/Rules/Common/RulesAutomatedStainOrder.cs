using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Rules.Common
{
	public class RulesAutomatedStainOrder
	{
		private Rule m_Rule;
		private ExecutionStatus m_ExecutionStatus;
		private Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.SpecialStain.StandingStainOrderList m_CurrentStainOrderList;
		private YellowstonePathology.Business.SpecialStain.StandingStainOrderList m_StandingStainOrderList;        

		public RulesAutomatedStainOrder()
		{
			m_StandingStainOrderList = new YellowstonePathology.Business.SpecialStain.StandingStainOrderList();
			m_StandingStainOrderList.Fill();
			m_CurrentStainOrderList = new YellowstonePathology.Business.SpecialStain.StandingStainOrderList();
			this.m_Rule = new Rule();
			
			this.m_Rule.ActionList.Add(PhysicianHasStandingOrder);
			this.m_Rule.ActionList.Add(FillStainOrderList);
			this.m_Rule.ActionList.Add(StainOrderExists);
			this.m_Rule.ActionList.Add(SetMessage);
		}        		

		private void PhysicianHasStandingOrder()
		{
			bool hasStandingOrder = false;
			foreach (YellowstonePathology.Business.SpecialStain.StandingStainOrderListItem standingStainOrderListItem in this.m_StandingStainOrderList)
			{
				if (this.m_AccessionOrder.PhysicianId == standingStainOrderListItem.PhysicianId)
				{
					hasStandingOrder = true;
					break;
				}
			}

			if (!hasStandingOrder)
			{
				this.m_ExecutionStatus.Halted = true;
			}
		}

		private void FillStainOrderList()
		{
			bool keywordFound = false;
			foreach (YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in this.m_AccessionOrder.SpecimenOrderCollection)
			{
				foreach (YellowstonePathology.Business.SpecialStain.StandingStainOrderListItem standingStainOrderListItem in this.m_StandingStainOrderList)
				{
					if (this.m_AccessionOrder.PhysicianId == standingStainOrderListItem.PhysicianId)
					{
						if (specimenOrder.Description.ToUpper().Contains(standingStainOrderListItem.Keyword))
						{
							this.m_CurrentStainOrderList.Add(standingStainOrderListItem);
							this.m_CurrentStainOrderList[this.m_CurrentStainOrderList.Count - 1].SpecimenId = specimenOrder.SpecimenOrderId;
							keywordFound = true;
						}
					}
				}
			}

			if (!keywordFound)
			{
				this.m_ExecutionStatus.Halted = true;
			}
		}

		private void StainOrderExists()
		{
			foreach(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder in this.m_AccessionOrder.PanelSetOrderCollection)
			{
				foreach (YellowstonePathology.Business.Test.PanelOrder panelOrder in panelSetOrder.PanelOrderCollection)
				{
					foreach (YellowstonePathology.Business.Test.Model.TestOrder testOrder in panelOrder.TestOrderCollection)
					{
						if (testOrder.TestName == this.m_CurrentStainOrderList[0].TestName)
						{
							this.m_ExecutionStatus.Halted = true;
							return;
						}
					}
				}
			}
		}

		private void SetMessage()
		{
			this.m_ExecutionStatus.AddMessage("A " + m_CurrentStainOrderList[0].TestName + " needs to be ordered for this case.", false);
			this.m_ExecutionStatus.ReturnValue = this.m_CurrentStainOrderList[0].TestId;
		}

		public void Execute(ExecutionStatus executionStatus, Test.AccessionOrder accessionOrder)
		{
			this.m_ExecutionStatus = executionStatus;
			this.m_Rule.ExecutionStatus = this.m_ExecutionStatus;
			this.m_AccessionOrder = accessionOrder;
			this.m_Rule.Execute();
		}
	}	
}
