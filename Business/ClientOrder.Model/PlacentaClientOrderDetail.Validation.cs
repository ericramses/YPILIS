using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Shared.ExtensionMethods;

namespace YellowstonePathology.Business.ClientOrder.Model
{
	public partial class PlacentaClientOrderDetail
	{
		#region GrossExamValidation
		public static Shared.ValidationResult IsGrossExamDataTypeValid(bool grossExam)
		{
			Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public Shared.ValidationResult IsGrossExamDomainValid()
		{
			Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			if (this.GrossExam == false &&
				this.CompleteExam == false &&
				this.Cytogenetics == false &&
				string.IsNullOrEmpty(this.OtherExam) == true)
			{
				validationResult.IsValid = false;
				validationResult.Message = "A Pathology Test Order must be selected.";
			}
			return validationResult;
		}
		#endregion

		#region CompleteExamValidation
		public static Shared.ValidationResult IsCompleteExamDataTypeValid(bool completeExam)
		{
			Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public Shared.ValidationResult IsCompleteExamDomainValid()
		{
			Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			if (this.GrossExam == false &&
				this.CompleteExam == false &&
				this.Cytogenetics == false &&
				string.IsNullOrEmpty(this.OtherExam) == true)
			{
				validationResult.IsValid = false;
				validationResult.Message = "A Pathology Test Order must be selected.";
			}
			return validationResult;
		}
		#endregion

		#region CytogeneticsValidation
		public static Shared.ValidationResult IsCytogeneticsDataTypeValid(bool cytogenetics)
		{
			Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public Shared.ValidationResult IsCytogeneticsDomainValid()
		{
			Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			if (this.GrossExam == false &&
				this.CompleteExam == false &&
				this.Cytogenetics == false &&
				string.IsNullOrEmpty(this.OtherExam) == true)
			{
				validationResult.IsValid = false;
				validationResult.Message = "A Pathology Test Order must be selected.";
			}
			return validationResult;
		}
		#endregion

		#region OtherExamValidation
		public static Shared.ValidationResult IsOtherExamDataTypeValid(string otherExam)
		{
			Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public Shared.ValidationResult IsOtherExamDomainValid()
		{
			Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			if (this.GrossExam == false &&
				this.CompleteExam == false &&
				this.Cytogenetics == false &&
				string.IsNullOrEmpty(this.OtherExam) == true)
			{
				validationResult.IsValid = false;
				validationResult.Message = "A Pathology Test Order must be selected.";
			}
			return validationResult;
		}
		#endregion
	}
}
