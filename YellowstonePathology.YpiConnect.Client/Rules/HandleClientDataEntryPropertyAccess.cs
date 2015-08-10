using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.YpiConnect.Client.Rules
{
    public class HandleClientDataEntryPropertyAccess
    {
        YellowstonePathology.Business.ClientOrder.Model.ClientOrder m_ClientOrder;
        YellowstonePathology.Business.Rules.SimpleRule m_Rule;
        YellowstonePathology.Business.Rules.ExecutionStatus m_ExecutionStatus;
		YellowstonePathology.Business.Rules.MethodResult m_MethodResult;

        public HandleClientDataEntryPropertyAccess(YellowstonePathology.Business.Rules.ExecutionStatus executionStatus)
        {
            this.m_ExecutionStatus = executionStatus;
            this.m_ExecutionStatus.ContinueExecutionOnHalt = false;
            this.m_Rule = new Business.Rules.SimpleRule(this.m_ExecutionStatus);            
                       
            this.m_Rule.ActionList.Add(HasOrderBeenReceived);            
            this.m_Rule.ActionList.Add(IsCurrentUserTheOwnerOfThisOrder);            

            this.m_Rule.ActionList.Add(SetAreDemographicsEnabledToTrue);  //Must be last.
        }

        public void HasOrderBeenReceived()
        {
            if (this.m_ClientOrder.Received == true)
            {
				this.m_MethodResult.Success = false;
                //this.m_OrderEntryPage.AreDemographicsEnabled = false;
                //this.m_OrderEntryPage.AreSpecimenEnabled = false;
                //this.m_OrderEntryPage.AreButtonsEnabled = false;
                this.m_ExecutionStatus.AddMessage("Case is accessioned", true);
            }
        }        

        public void IsCurrentUserTheOwnerOfThisOrder()
        {
			if (this.m_ClientOrder.OrderedBy == null || this.m_ClientOrder.OrderedBy.ToUpper() != YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount.DisplayName.ToUpper())
            {
				this.m_MethodResult.Success = false;
				//this.m_OrderEntryPage.AreDemographicsEnabled = false;
                //this.m_OrderEntryPage.AreSpecimenEnabled = false;
                //this.m_OrderEntryPage.AreButtonsEnabled = false;
                this.m_ExecutionStatus.AddMessage("Current user is not owner.", true);
            }
        }                

        public void SetAreDemographicsEnabledToTrue()
        {
			this.m_MethodResult.Success = true;
			//this.m_OrderEntryPage.AreDemographicsEnabled = true;
            //this.m_OrderEntryPage.AreSpecimenEnabled = true;
            //this.m_OrderEntryPage.AreButtonsEnabled = true;
        }

        public void Execute(YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder, YellowstonePathology.Business.Rules.MethodResult methodResult)
        {
            this.m_ClientOrder = clientOrder;
			this.m_MethodResult = methodResult;
            this.m_Rule.Execute();
        }
    }
}
