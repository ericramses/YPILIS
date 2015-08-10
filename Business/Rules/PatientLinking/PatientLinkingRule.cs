using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Rules.PatientLinking
{
	public class PatientLinkingRule
	{
		protected YellowstonePathology.Business.Rules.Rule m_Rule;
        protected YellowstonePathology.Business.Rules.ExecutionStatus m_ExecutionStatus;
        protected YellowstonePathology.Business.Interface.IOrder m_Order;

        public PatientLinkingRule()
        {
            this.m_Rule = new YellowstonePathology.Business.Rules.Rule();			
			this.m_Rule.ActionList.Add(this.FirstNameIsPresent);
			this.m_Rule.ActionList.Add(this.LastNameIsPresent);
		}

		protected void FirstNameIsPresent()
		{
			if (string.IsNullOrEmpty(this.m_Order.PFirstName) == true)
			{
				this.m_ExecutionStatus.AddMessage("Enter a first name then attempt to link.", true);
			}
		}

		protected void LastNameIsPresent()
		{
			if (string.IsNullOrEmpty(this.m_Order.PLastName) == true)
			{
				this.m_ExecutionStatus.AddMessage("Enter a last name then attempt to link.", true);
			}
		}

        public void Execute(YellowstonePathology.Business.Rules.ExecutionStatus executionStatus, YellowstonePathology.Business.Interface.IOrder order)
		{
			this.m_ExecutionStatus = executionStatus;
			this.m_Order = order;
			this.m_Rule.Execute(executionStatus);
		}
	}
}
