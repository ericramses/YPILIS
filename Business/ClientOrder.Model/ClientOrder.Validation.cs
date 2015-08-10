using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.ClientOrder.Model
{
	public partial class ClientOrder
	{
		#region ValidateReportCopyTo
		public static YellowstonePathology.Shared.ValidationResult IsReportCopyToDataTypeValid(string preOpDiagnosis)
		{
			YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public YellowstonePathology.Shared.ValidationResult IsReportCopyToDomainValid()
		{
			YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}
		#endregion

		#region ValidateClinicalHistory
		public static YellowstonePathology.Shared.ValidationResult IsClinicalHistoryDataTypeValid(string postOpDiagnosis)
		{
			YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public YellowstonePathology.Shared.ValidationResult IsClinicalHistoryDomainValid()
		{
			YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}
		#endregion
	}
}
