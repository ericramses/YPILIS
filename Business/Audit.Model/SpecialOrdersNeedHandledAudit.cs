/*
 * Created by SharpDevelop.
 * User: william.copland
 * Date: 12/29/2015
 * Time: 11:53 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace YellowstonePathology.Business.Audit.Model
{
	/// <summary>
	/// Description of SpecialOrderNeedsHandledAudit.
	/// </summary>
	public class SpecialOrdersNeedHandledAudit : Audit
	{
		private Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.SpecialStain.StandingStainOrderList m_CurrentStainOrderList;
		private YellowstonePathology.Business.SpecialStain.StandingStainOrderList m_StandingStainOrderList;        
		
		public SpecialOrdersNeedHandledAudit(Test.AccessionOrder accessionOrder)
		{
			this.m_AccessionOrder = accessionOrder;
			m_StandingStainOrderList = new YellowstonePathology.Business.SpecialStain.StandingStainOrderList();
			m_StandingStainOrderList.Fill();
			m_CurrentStainOrderList = new YellowstonePathology.Business.SpecialStain.StandingStainOrderList();
		}

        public override void Run()
        {
            this.m_Status = AuditStatusEnum.OK;
            
            if(this.PhysicianHasStandingOrder() == true)
            {
            	if(this.FillStainOrderList() == true)
            	{
            		if(this.StainOrderExists() == false)
            		{
            			this.m_Status = AuditStatusEnum.Failure;
            			this.m_Message.Append("A " + m_CurrentStainOrderList[0].TestName + " needs to be ordered for this case.");
            		}
            	}
            }
        }

		private bool PhysicianHasStandingOrder()
		{
			bool result = false;
			foreach (YellowstonePathology.Business.SpecialStain.StandingStainOrderListItem standingStainOrderListItem in this.m_StandingStainOrderList)
			{
				if (this.m_AccessionOrder.PhysicianId == standingStainOrderListItem.PhysicianId)
				{
					result = true;
					break;
				}
			}
			
			return result;
		}

		private bool FillStainOrderList()
		{
			bool result = false;
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
							result = true;
						}
					}
				}
			}

			return result;
		}

		private bool StainOrderExists()
		{
			bool result = false;
			foreach(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder in this.m_AccessionOrder.PanelSetOrderCollection)
			{
				YellowstonePathology.Business.Test.Model.TestOrderCollection testOrderCollection = panelSetOrder.GetTestOrders();
				if(testOrderCollection.Exists(this.m_CurrentStainOrderList[0].TestId) == true)
				{
					result = true;
					break;
				}
			}
			
			return result;
		}
	}
}
