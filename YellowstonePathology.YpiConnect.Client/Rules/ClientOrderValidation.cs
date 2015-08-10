using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.YpiConnect.Client.Rules
{
    public class ClientOrderValidation
    {
        YellowstonePathology.Business.ClientOrder.Model.ClientOrder m_ClientOrder;
        YellowstonePathology.Business.Rules.SimpleRule m_Rule;
        YellowstonePathology.Business.Rules.ExecutionStatus m_ExecutionStatus;

        public ClientOrderValidation(YellowstonePathology.Business.Rules.ExecutionStatus executionStatus)
        {
            this.m_ExecutionStatus = executionStatus;
            this.m_Rule = new Business.Rules.SimpleRule(this.m_ExecutionStatus);
            
            this.m_ExecutionStatus.ContinueExecutionOnHalt = true;
            this.m_ExecutionStatus.SuccessMessage = "The client order has been successfully validated.";
            this.m_ExecutionStatus.FailureMessage = "We were unable to validate the client order because of the following issues:";

            this.m_Rule.ActionList.Add(IsPreOpNull);
            this.m_Rule.ActionList.Add(IsBirthdateValid);
            this.m_Rule.ActionList.Add(IsSvhMedicalRecordValid);
            this.m_Rule.ActionList.Add(IsSvhAccountNoValid);
            this.m_Rule.ActionList.Add(DoesFirstNameExist);            
            this.m_Rule.ActionList.Add(DoesLastNameExist);
            this.m_Rule.ActionList.Add(DoesProviderExist);
            this.m_Rule.ActionList.Add(DoAllSpecimenHaveDescriptions);
			this.m_Rule.ActionList.Add(IsPlacentalQuestionnaireNeeded);
        }

        public void IsPreOpNull()
        {
            //if (string.IsNullOrEmpty(this.m_ClientOrder.PreOpDiagnosis) == true)
            //{
            //    this.m_ExecutionStatus.AddMessage("The Pre-OP diagnosis must be filled out before you can submit this order.", true);
            //}
        }

        public void DoesFirstNameExist()
        {
            if (string.IsNullOrEmpty(this.m_ClientOrder.PFirstName) == true)
            {
                this.m_ExecutionStatus.AddMessage("The patient first name has not been entered.", true);
            }
        }

        public void DoesLastNameExist()
        {
            if (string.IsNullOrEmpty(this.m_ClientOrder.PLastName) == true)
            {
                this.m_ExecutionStatus.AddMessage("The patient last name has not been entered.", true);
            }
        }

        public void DoesProviderExist()
        {
            if (string.IsNullOrEmpty(this.m_ClientOrder.ProviderName) == true)
            {
                this.m_ExecutionStatus.AddMessage("The provider name has not been entered.", true);
            }
        }

        public void IsBirthdateValid()
        {
            if (this.m_ClientOrder.PBirthdate.HasValue == false)
            {
                this.m_ExecutionStatus.AddMessage("The patient birthdate is not valid", true);
            }
            else
            {
                if (this.m_ClientOrder.PBirthdate > DateTime.Today)
                {
                    this.m_ExecutionStatus.AddMessage("The patient birthdate is in the future", true);
                }
            }
        }

        public void IsSvhMedicalRecordValid()
        {
            if (string.IsNullOrEmpty(this.m_ClientOrder.SvhMedicalRecord) == true)
            {
                this.m_ExecutionStatus.AddMessage("The medical record number is not valid", true);
            }
        }

        public void IsSvhAccountNoValid()
        {
            if (string.IsNullOrEmpty(this.m_ClientOrder.SvhAccountNo) == true)
            {
                this.m_ExecutionStatus.AddMessage("The account number is not valid", true);
            }
        }

        public void DoAllSpecimenHaveDescriptions()
        {
            foreach (YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail in this.m_ClientOrder.ClientOrderDetailCollection)
            {
                if (clientOrderDetail.Inactive == false)
                {
                    if (string.IsNullOrEmpty(clientOrderDetail.Description) == true)
                    {
                        this.m_ExecutionStatus.AddMessage("There is a specimen that doesn't have a description.", true);
                        break;
                    }
                }
            }
        }

		public void IsPlacentalQuestionnaireNeeded()
		{
			foreach (YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail in this.m_ClientOrder.ClientOrderDetailCollection)
			{
                if (clientOrderDetail.Inactive == false)
                {
					if (string.IsNullOrEmpty(clientOrderDetail.Description) == false)
					{
						if (clientOrderDetail.Description.ToUpper().Contains("PLACENTA") == true)
						{
							//YellowstonePathology.YpiConnect.Proxy.ClientOrderServiceProxy proxy = new Proxy.ClientOrderServiceProxy();
							//YellowstonePathology.Business.ClientOrder.Model.PlacentalPathologyQuestionnaireCollection placentalPathologyQuestionnaireCollection = proxy.GetPlacentalPathologyQuestionnaireByClientOrderId(this.m_ClientOrder.ClientOrderId);

							//if (placentalPathologyQuestionnaireCollection.Count == 0 ||
							//	(placentalPathologyQuestionnaireCollection.Count > 0 && placentalPathologyQuestionnaireCollection[0].DateSubmitted == null))
							//{
							//	this.m_ExecutionStatus.AddMessage("A Placenta Pathology Questionnaire must be submitted for this order.\nPlease select Placental Questionnaire to create and submit this form.", true);
							//	break;
							//}
						}
					}
				}
			}
		}

        public void Execute(YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder)
        {
            this.m_ClientOrder = clientOrder;
            this.m_Rule.Execute();
        }
    }
}
