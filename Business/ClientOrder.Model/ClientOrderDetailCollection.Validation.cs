using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.ClientOrder.Model
{
	public partial class ClientOrderDetailCollection
	{        
		public YellowstonePathology.Shared.ValidationResult IsContainerIdNotADuplicate(string clientOrderDetailId, string containerId)
		{
			YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			if (ExistsByContainerId(containerId) == true)
			{
				ClientOrderDetail matchClientOrderDetail = GetByContainerId(containerId);
				if (matchClientOrderDetail.ClientOrderDetailId != clientOrderDetailId)
				{
					validationResult.IsValid = false;
				}
			}

			if(validationResult.IsValid == false)
			{
				validationResult.Message = "The container scanned is part of an existing order and can't be used again. Are you sure you have scanned the correct container?";
			}
			return validationResult;
		}

		public static YellowstonePathology.Shared.ValidationResult IsDataTypeValid(ClientOrderDetail clientOrderDetail, ClientOrderDetailCollection clientOrderDetailCollection)
		{
			Shared.ValidationResult result = new Shared.ValidationResult();
			result.IsValid = true;
			List<YellowstonePathology.Shared.ValidationResult> validationResults = new List<Shared.ValidationResult>();
			validationResults.Add(clientOrderDetailCollection.IsContainerIdNotADuplicate(clientOrderDetail.ClientOrderDetailId, clientOrderDetail.ContainerId));

			foreach (Shared.ValidationResult validationResult in validationResults)
			{
				if (validationResult.IsValid == false)
				{
					result = validationResult;
					break;
				}
			}
			return result;
		}

		public YellowstonePathology.Shared.ValidationResult IsDomainValid()
		{
			Shared.ValidationResult result = new Shared.ValidationResult();
			result.IsValid = true;

            /*
			List<YellowstonePathology.Shared.ValidationResult> validationResults = new List<Shared.ValidationResult>();
			foreach (ClientOrderDetail clientOrderDetail in this)
			{
				validationResults.Add(clientOrderDetail.IsDomainValid());
				validationResults.Add(this.IsContainerIdNotADuplicate(clientOrderDetail.ClientOrderDetailId, clientOrderDetail.ContainerId));
			}

			foreach (Shared.ValidationResult validationResult in validationResults)
			{
				if (validationResult.IsValid == false)
				{
					result = validationResult;
					break;
				}
			}
            */
			return result;
		}

		public YellowstonePathology.Shared.ValidationResult IsDomainValid(ClientOrderDetail clientOrderDetail)
		{
			Shared.ValidationResult result = new Shared.ValidationResult();
			result.IsValid = true;
			List<YellowstonePathology.Shared.ValidationResult> validationResults = new List<Shared.ValidationResult>();
			validationResults.Add(this.IsContainerIdNotADuplicate(clientOrderDetail.ClientOrderDetailId, clientOrderDetail.ContainerId));

			foreach (Shared.ValidationResult validationResult in validationResults)
			{
				if (validationResult.IsValid == false)
				{
					result = validationResult;
					break;
				}
			}
			return result;
		}
	}
}
