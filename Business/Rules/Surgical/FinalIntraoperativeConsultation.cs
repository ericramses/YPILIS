using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Rules.Surgical
{
	public class FinalIntraoperativeConsultation : SimpleMessageRule
	{
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.Test.Surgical.IntraoperativeConsultationResult m_IntraoperativeConsultationResult;
		private YellowstonePathology.Business.Specimen.Model.SpecimenOrder m_SpecimenOrder;

		public FinalIntraoperativeConsultation(ExecutionMessage executionMessage, YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.Surgical.IntraoperativeConsultationResult intraoperativeConsultationResult, YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder)
			: base(executionMessage)
		{
			this.m_ExecutionMessage.ContinueExecutionOnHalt = false;
			this.m_AccessionOrder = accessionOrder;
			this.m_IntraoperativeConsultationResult = intraoperativeConsultationResult;
			this.m_SpecimenOrder = specimenOrder;

			this.ActionList.Add(SetStartTime);
			this.ActionList.Add(HasResult);
			this.ActionList.Add(HasCallback);
			this.ActionList.Add(HasPerformedBy);
			this.ActionList.Add(SetEndTime);
		}

		private void SetStartTime()
		{
			this.m_IntraoperativeConsultationResult.StartDate = this.m_AccessionOrder.AccessionTime;
		}

		private void HasResult()
		{
			if (string.IsNullOrEmpty(this.m_IntraoperativeConsultationResult.Result))
			{
				this.m_ExecutionMessage.Message = "The result must be entered before the intraoperative consultation can be finalled.";
				this.m_ExecutionMessage.Halted = true;
			}
		}

		private void HasCallback()
		{
			if (string.IsNullOrEmpty(this.m_IntraoperativeConsultationResult.CallbackContact))
			{
				this.m_ExecutionMessage.Message = "The callback contact for the intraoperative consultation has not been entered.";
				this.m_ExecutionMessage.Halted = true;
			}
		}

		private void HasPerformedBy()
		{
			if (this.m_IntraoperativeConsultationResult.PerformedById == 0)
			{
				this.m_ExecutionMessage.Message = "The pathologist performing the intraoperative consultation has not been selected.";
				this.m_ExecutionMessage.Halted = true;
			}
		}

		private void SetEndTime()
		{
			if (!this.m_IntraoperativeConsultationResult.EndDate.HasValue)
			{
				this.m_IntraoperativeConsultationResult.EndDate = DateTime.Now;
			}
		}
	}
}
