using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Rules.PanelOrder
{
	public class AcceptHPV
	{
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.Test.HPVTWI.PanelSetOrderHPVTWI m_PanelSetOrder;
		private YellowstonePathology.Business.Test.HPVTWI.PanelOrderHPVTWI m_PanelOrderBeingAccepted;

		private YellowstonePathology.Business.Rules.ExecutionStatus m_ExecutionStatus;
		private YellowstonePathology.Business.Rules.Rule m_Rule;
		private YellowstonePathology.Core.User.SystemUser m_OrderingUser;
		private YellowstonePathology.Core.User.SystemUser m_AcceptingUser;

		public AcceptHPV()
        {
			this.m_Rule = new YellowstonePathology.Business.Rules.Rule();

			this.m_Rule.ActionList.Add(HaltIfPanelOrderIsAccepted);
			this.m_Rule.ActionList.Add(HaltIfPanelSetOrderIsFinal);
			this.m_Rule.ActionList.Add(HaltIfAnyResultsAreEmpty);
			this.m_Rule.ActionList.Add(PatientIsLinked);
			
			this.m_Rule.ActionList.Add(SetPositiveResult);
            this.m_Rule.ActionList.Add(SetNegativeResult);
            this.m_Rule.ActionList.Add(SetQNSResult);
            this.m_Rule.ActionList.Add(SetSecondPanelQNS);
            this.m_Rule.ActionList.Add(SetSecondPanelLowFamFoz);
            this.m_Rule.ActionList.Add(SetSecondPanelHighCV);            
            this.m_Rule.ActionList.Add(SetSecondPanelLowGDNA);                        
            this.m_Rule.ActionList.Add(ClearComment);
			this.m_Rule.ActionList.Add(SetIndeterminateComment);
			this.m_Rule.ActionList.Add(SetUnsatisfactoryComment);
			this.m_Rule.ActionList.Add(SetTestInformation);
			this.m_Rule.ActionList.Add(SetReferences);
            this.m_Rule.ActionList.Add(AcceptPanel);
            this.m_Rule.ActionList.Add(AddPanelIfNoResult);
        }

		private void HaltIfPanelSetOrderIsFinal()
		{
			if (this.m_PanelSetOrder.Final == true)
			{
				this.m_ExecutionStatus.AddMessage("Case is already final", true);
			}
		}

		private void HaltIfPanelOrderIsAccepted()
		{
			if (this.m_PanelOrderBeingAccepted.Accepted == true)
			{
				this.m_ExecutionStatus.AddMessage("Panel is already accepted.", true);
			}
		}

		private void HaltIfAnyResultsAreEmpty()
		{
			if (this.m_PanelOrderBeingAccepted.AllResultsAreSet() == false)
			{
				this.m_ExecutionStatus.AddMessage("An empty result was found.", true);
			}
		}

		private void PatientIsLinked()
		{
			if (this.m_AccessionOrder.PatientId == "0")
			{
				this.m_ExecutionStatus.AddMessage("Patient is not linked", true);
			}
		}
        
        private void SetPositiveResult()
        {
            //If one of the three tests is Positive then Positive.
			if(this.m_PanelOrderBeingAccepted.AnyResultPositive() == true)
			{
				this.m_PanelSetOrder.Result = Test.HPVTWI.HPVTWIResult.PositiveResult;
			}
        }

        private void SetNegativeResult()
        {            
            //If all three tests are Negative then the result is Negative
			if (this.m_PanelOrderBeingAccepted.AllResultsNegative() == true)
			{
				this.m_PanelSetOrder.Result = Test.HPVTWI.HPVTWIResult.NegativeResult;
			}
		}

        private void SetQNSResult()
        {
            //If all three tests are QNS then the result is Indeterminate
			if (this.m_PanelOrderBeingAccepted.AllResultsResultsQns() == true)
			{
				this.m_PanelSetOrder.Result = Test.HPVTWI.HPVTWIResult.IndeterminateResult;
			}
        }

        private void SetSecondPanelQNS()
        {
			if (this.m_PanelSetOrder.PanelOrders.Count == 2)
			{
				if (this.m_PanelOrderBeingAccepted.AllResultsResultsQns() == true)
				{
					this.m_PanelSetOrder.Result = Test.HPVTWI.HPVTWIResult.IndeterminateResult;
				}
			}
        }

        private void SetSecondPanelLowFamFoz()
        {
			if (this.m_PanelSetOrder.PanelOrderCollection.Count == 2)
			{
				if (this.m_PanelOrderBeingAccepted.AllResultsLowFamFoz() == true)
				{
					this.m_PanelSetOrder.Result = Test.HPVTWI.HPVTWIResult.IndeterminateResult;
				}
			}
        }

        private void SetSecondPanelHighCV()
        {
			if (this.m_PanelSetOrder.PanelOrderCollection.Count == 2)
			{
				if (this.m_PanelOrderBeingAccepted.AllResultsHighCv() == true)
				{
					this.m_PanelSetOrder.Result = Test.HPVTWI.HPVTWIResult.IndeterminateResult;
				}
			}
        }

        private void SetSecondPanelLowGDNA()
        {
			if (this.m_PanelSetOrder.PanelOrderCollection.Count == 2)
			{
				if (this.m_PanelOrderBeingAccepted.AllResultsLowDna() == true)
				{
					this.m_PanelSetOrder.Result = Test.HPVTWI.HPVTWIResult.Unsatisfactory;
				}
			}
        }

		private void ClearComment()
		{
			this.m_PanelSetOrder.Comment = null;
		}

        private void SetIndeterminateComment()
        {
			if (this.m_PanelSetOrder.Result == Test.HPVTWI.HPVTWIResult.IndeterminateResult)
			{
				this.m_PanelSetOrder.Comment = Test.HPVTWI.HPVTWIResult.IndeterminateComment;
			}
        }

        private void SetUnsatisfactoryComment()
        {
			if (this.m_PanelSetOrder.Result == Test.HPVTWI.HPVTWIResult.Unsatisfactory)
			{
				this.m_PanelSetOrder.Comment = Test.HPVTWI.HPVTWIResult.UnsatisfactoryComment;
			}
		}

		private void SetTestInformation()
		{
			if (string.IsNullOrEmpty(this.m_PanelSetOrder.Result) == false)
			{
				this.m_PanelSetOrder.TestInformation = Test.HPVTWI.HPVTWIResult.TestInformation;
			}
		}

		private void SetReferences()
		{
			if (string.IsNullOrEmpty(this.m_PanelSetOrder.Result) == false)
			{
				this.m_PanelSetOrder.References = Test.HPVTWI.HPVTWIResult.References;
			}
		}

		private void AcceptPanel()
        {
			if (this.m_PanelOrderBeingAccepted.Accepted == false)
			{
				this.m_PanelOrderBeingAccepted.Accepted = true;
				this.m_PanelOrderBeingAccepted.AcceptedById = this.m_AcceptingUser.UserId;
				this.m_PanelOrderBeingAccepted.AcceptedDate = DateTime.Today;
				this.m_PanelOrderBeingAccepted.AcceptedTime = DateTime.Now;
			}
        }        

        private void AddPanelIfNoResult()
        {
			if (string.IsNullOrEmpty(this.m_PanelSetOrder.Result))
			{
				YellowstonePathology.Business.Test.HPVTWI.PanelOrderHPVTWI panelOrder = (YellowstonePathology.Business.Test.HPVTWI.PanelOrderHPVTWI)this.m_PanelSetOrder.PanelOrderCollection.GetNew(this.m_PanelSetOrder.ReportNo, 22, "High Risk HPV TWI", m_OrderingUser.UserId);
				panelOrder.SetDefaults(this.m_PanelSetOrder.ReportNo, m_OrderingUser.UserId);

				DateTime matchTime = DateTime.Now;
				panelOrder.OrderTime = matchTime;

				panelOrder.Acknowledged = true;
				panelOrder.AcknowledgedById = m_OrderingUser.UserId;
				panelOrder.AcknowledgedDate = DateTime.Today;
				panelOrder.AcknowledgedTime = DateTime.Now;
				this.m_PanelSetOrder.PanelOrderCollection.Add(panelOrder);
			}
        }

		public void Execute(YellowstonePathology.Business.Rules.ExecutionStatus executionStatus, Test.AccessionOrder accessionOrder, Test.HPVTWI.PanelOrderHPVTWI panelOrder,
			YellowstonePathology.Core.User.SystemUser orderingUser, YellowstonePathology.Core.User.SystemUser acceptingUser)
		{
			this.m_AccessionOrder = accessionOrder;
			this.m_PanelSetOrder = (Test.HPVTWI.PanelSetOrderHPVTWI)accessionOrder.PanelSetOrderCollection.GetCurrent(panelOrder.ReportNo);
			this.m_PanelOrderBeingAccepted = (Test.HPVTWI.PanelOrderHPVTWI)this.m_PanelSetOrder.PanelOrderCollection.GetByPanelOrderId(panelOrder.PanelOrderId);
			this.m_ExecutionStatus = executionStatus;
			this.m_OrderingUser = orderingUser;
			this.m_AcceptingUser = acceptingUser;
			this.m_Rule.Execute(executionStatus);
		}
	}
}
