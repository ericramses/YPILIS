using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace YellowstonePathology.YpiConnect.Client.Rules
{
    public class ContainerIdValidation 
    {
        string m_ContainerId;
        YellowstonePathology.Business.ClientOrder.Model.ClientOrder m_ClientOrder;
		YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection m_ClientOrderCollection;

        YellowstonePathology.Business.Rules.SimpleRule m_Rule;
        YellowstonePathology.Business.Rules.ExecutionStatus m_ExecutionStatus;

        public ContainerIdValidation(YellowstonePathology.Business.Rules.ExecutionStatus executionStatus)
        {
            this.m_ExecutionStatus = executionStatus;
            this.m_Rule = new Business.Rules.SimpleRule(this.m_ExecutionStatus);            
            
            this.m_ExecutionStatus.ContinueExecutionOnHalt = false;
            this.m_ExecutionStatus.SuccessMessage = "The container id is valid.";            

            this.m_Rule.ActionList.Add(IsContainerIdLengthValid);
			this.m_Rule.ActionList.Add(IsContainerIdFormatValid);
            this.m_Rule.ActionList.Add(DoesConainerIdExistInCurrentOrder);
            this.m_Rule.ActionList.Add(DoesConainerIdExistInOrderCollection);            
        }        

        private void IsContainerIdLengthValid()
        {
            if (string.IsNullOrEmpty(this.m_ContainerId) == false && this.m_ContainerId.Length == 40 && this.m_ContainerId.StartsWith("CTNR"))
            {
                this.m_ExecutionStatus.Halted = false;
                this.m_ExecutionStatus.SuccessMessage = "The container ID is valid.";
            }
            else
            {
                this.m_Rule.ExecutionStatus.Halted = true;
                this.m_ExecutionStatus.FailureMessage = "The container ID is not valid.";
            }
        }

		private void IsContainerIdFormatValid()
		{
			Regex isGuid = new Regex(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$", RegexOptions.Compiled);
			bool valid = true;

			if (isGuid.IsMatch(this.m_ContainerId.Substring(4))) 
			{
				valid = true;
			}
				
			if(valid == false)
			{
				this.m_Rule.ExecutionStatus.Halted = true;
				this.m_ExecutionStatus.FailureMessage = "The container ID format is not valid.";
			}
		}

        private void DoesConainerIdExistInCurrentOrder()
        {
            foreach (YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder in this.m_ClientOrderCollection)
            {
                foreach (YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail in clientOrder.ClientOrderDetailCollection)
                {
                    if (clientOrderDetail.ContainerId == this.m_ContainerId)
                    {
						if (clientOrderDetail.Inactive == true)
						{
							this.m_ExecutionStatus.FailureMessage = "The container scanned was previously marked inactive.";
							this.m_ExecutionStatus.Halted = true;
						}
						else
						{
							this.m_ExecutionStatus.FailureMessage = "The container scanned already exists in this order.";
							this.m_ExecutionStatus.Halted = true;
						}
						break;
                    }
                }
            }
        }

        private void DoesConainerIdExistInOrderCollection()
        {
            foreach (YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder in this.m_ClientOrderCollection)
            {
                foreach (YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail in this.m_ClientOrder.ClientOrderDetailCollection)
                {
                    if (clientOrderDetail.ContainerId == this.m_ContainerId)
                    {
						this.m_ExecutionStatus.FailureMessage = "This container already exist in an order for " + this.m_ClientOrder.PFirstName + " " + this.m_ClientOrder.PLastName;
						this.m_ExecutionStatus.Halted = true;
						break;
                    }
                }
            }
        }

		public void Execute(string containerId, YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder, YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection clientOrderCollection)
        {
            this.m_ContainerId = containerId;
            this.m_ClientOrder = clientOrder;
            this.m_ClientOrderCollection = clientOrderCollection;
            this.m_Rule.Execute();
        }        
    }
}
