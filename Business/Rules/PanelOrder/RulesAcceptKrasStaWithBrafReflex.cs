using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Rules.PanelOrder
{
	public class RulesAcceptKrasStaWithBrafReflex : RulesAcceptKRASSTAResults
	{
		public RulesAcceptKrasStaWithBrafReflex()
		{
			this.m_Rule.ActionList.Clear();
			//from Accept
			this.m_Rule.ActionList.Add(DoesUserHavePermission);
			this.m_Rule.ActionList.Add(HaltIfPanelOrderIsAccepted);
			this.m_Rule.ActionList.Add(HaltIfPanelSetOrderIsFinal);
			this.m_Rule.ActionList.Add(HaltIfAnyResultsAreEmpty);
			this.m_Rule.ActionList.Add(PatientIsLinked);
			//from RulesAcceptKRASSTAResults
			this.m_Rule.ActionList.Add(SetResult);
			this.m_Rule.ActionList.Add(SetKrasResultDetail);
			this.m_Rule.ActionList.Add(SetResultComment);
			this.m_Rule.ActionList.Add(SetReportInterpretation);
			this.m_Rule.ActionList.Add(SetReportMethod);
			this.m_Rule.ActionList.Add(SetReportIndication);
			this.m_Rule.ActionList.Add(AcceptPanel);
			//new for this rule
			this.m_Rule.ActionList.Add(SetBrafNotIndicatedResult);
			this.m_Rule.ActionList.Add(AddBrafPanel);
			this.m_Rule.ActionList.Add(Save);
		}

		private void SetBrafNotIndicatedResult()
		{
			if (this.ResultIsPositive())
			{
				this.m_PanelSetOrder.PanelSetResultOrderCollection.GetPanelSetResultOrder(193).Result = "Not clinically indicated.";
			}
		}

		protected void AddBrafPanel()
		{
			/*if (!this.ResultIsPositive())
			{
				Core.User.SystemIdentity systemIdentity = new Core.User.SystemIdentity(Core.User.SystemIdentityTypeEnum.Blank);
				systemIdentity.SetUser(this.m_OrderingUser.UserId);
				YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = null;
				if (((YellowstonePathology.Domain.Test.Model.TestOrder)this.m_PanelOrderBeingAccepted.TestOrderCollection[0]).AliquotOrderItemList.Count > 0)
				{
					string aliquotOrderId = ((YellowstonePathology.Domain.Test.Model.TestOrder)this.m_PanelOrderBeingAccepted.TestOrderCollection[0]).AliquotOrderItemList[0].AliquotOrderId;
					aliquotOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetAliquotOrder(aliquotOrderId);
				}
				((YellowstonePathology.Domain.Test.KRASBRAFReflex.KRASBRAFReflexTestOrder)this.m_PanelSetOrder).AddBrafPanel(aliquotOrder, systemIdentity);
			}*/
		}
	}
}
