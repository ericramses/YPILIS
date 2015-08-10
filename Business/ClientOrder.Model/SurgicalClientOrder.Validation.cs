using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.ClientOrder.Model
{
	public partial class SurgicalClientOrder
	{
		#region ValidatePreOpDiagnosis
		public static YellowstonePathology.Shared.ValidationResult IsPreOpDiagnosisDataTypeValid(string preOpDiagnosis)
		{
			YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public YellowstonePathology.Shared.ValidationResult IsPreOpDiagnosisDomainValid()
		{
			YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			if (string.IsNullOrEmpty(this.m_PreOpDiagnosis) == true)
			{
				validationResult.IsValid = false;
				validationResult.Message = "Please enter a preOp diagnosis.";
			}
			return validationResult;
		}
		#endregion

		#region ValidatePostOpDiagnosis
		public static YellowstonePathology.Shared.ValidationResult IsPostOpDiagnosisDataTypeValid(string postOpDiagnosis)
		{
			YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public YellowstonePathology.Shared.ValidationResult IsPostOpDiagnosisDomainValid()
		{
			YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}
		#endregion
	}
}
