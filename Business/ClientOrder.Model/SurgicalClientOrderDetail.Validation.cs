using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.ClientOrder.Model
{
	public partial class SurgicalClientOrderDetail
	{
		#region OrderImmediateExamValidation
		public static Shared.ValidationResult IsOrderImmediateExamDataTypeValid(bool? orderImmediateExam)
		{
			Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public Shared.ValidationResult IsOrderImmediateExamDomainValid()
		{
			Shared.ValidationResult validationResult = new Shared.ValidationResult();
			if (this.OrderImmediateExam.HasValue)
			{
				validationResult.IsValid = true;
			}
			else
			{
				validationResult.IsValid = false;
				validationResult.Message = "You must indicate if you need an Immediate exam or not.";
			}
			return validationResult;
		}
		#endregion

		#region OrderFrozenSectionValidation
		public static Shared.ValidationResult IsOrderFrozenSectionDataTypeValid(bool? orderFrozenSection)
		{
			Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public Shared.ValidationResult IsOrderFrozenSectionDomainValid()
		{
			Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			if (this.OrderImmediateExam.HasValue &&
				this.OrderImmediateExam.Value == true)
			{
				if (this.OrderFrozenSection.HasValue == false)
				{
					validationResult.IsValid = false;
					validationResult.Message = "Order Frozen Section not selected.";
				}
			}
			return validationResult;
		}
		#endregion

		#region CallbackNumberValidation
		public static YellowstonePathology.Shared.ValidationResult IsCallbackNumberDataTypeValid(string callbackNumber)
		{
			YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public YellowstonePathology.Shared.ValidationResult IsCallbackNumberDomainValid()
		{
			YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			if (this.OrderImmediateExam.HasValue &&
				this.OrderImmediateExam.Value == true)
			{
				if(string.IsNullOrEmpty(this.CallbackNumber) == true)
				{
					validationResult.IsValid = false;
					validationResult.Message = "The callback phone number field cannot be left blank when an Immediate Exam is ordered.";
				}
			}
			return validationResult;
		}
		#endregion
	}
}
