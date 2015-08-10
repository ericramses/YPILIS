using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.ClientOrder.Model
{
	public partial class CytologyClientOrder
	{
		#region ValidateReflexHPV
		public static YellowstonePathology.Shared.ValidationResult IsReflexHPVDataTypeValid(bool orderReflexHPV)
		{
			YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public YellowstonePathology.Shared.ValidationResult IsReflexHPVDomainValid()
		{
			YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}
		#endregion

		#region ValidateRoutineHPVTesting
		public static YellowstonePathology.Shared.ValidationResult IsRoutineHPVTestingDataTypeValid(bool orderRoutineHPVTesting)
		{
			YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public YellowstonePathology.Shared.ValidationResult IsRoutineHPVTestingDomainValid()
		{
			YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}
		#endregion

		#region ValidateNGCTTesting
		public static YellowstonePathology.Shared.ValidationResult IsNGCTTestingDataTypeValid(bool orderNGCTTesting)
		{
			YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public YellowstonePathology.Shared.ValidationResult IsNGCTTestingDomainValid()
		{
			YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}
		#endregion

		#region ValidateScreeningType
		public static YellowstonePathology.Shared.ValidationResult IsScreeningTypeDataTypeValid(string screeningType)
		{
			YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public YellowstonePathology.Shared.ValidationResult IsScreeningTypeDomainValid()
		{
			YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			if (string.IsNullOrEmpty(this.ScreeningType) == true)
			{
				validationResult.IsValid = false;
				validationResult.Message = "You must select a screening type before you can continue.";
			}
			return validationResult;
		}
		#endregion

		#region ValidateLMP
		public static YellowstonePathology.Shared.ValidationResult IsLMPBindingDataTypeValid(string lMP)
		{
			YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public YellowstonePathology.Shared.ValidationResult IsLMPDomainValid()
		{
			YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}
		#endregion

		#region ValidateHysterectomy
		public static YellowstonePathology.Shared.ValidationResult IsHysterectomyDataTypeValid(bool hysterectomy)
		{
			YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public YellowstonePathology.Shared.ValidationResult IsHysterectomyDomainValid()
		{
			YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}
		#endregion

		#region ValidateCervixPresent
		public static YellowstonePathology.Shared.ValidationResult IsCervixPresentDataTypeValid(bool cervixPresent)
		{
			YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public YellowstonePathology.Shared.ValidationResult IsCervixPresentDomainValid()
		{
			YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}
		#endregion

		#region ValidateAbnormalBleeding
		public static YellowstonePathology.Shared.ValidationResult IsAbnormalBleedingDataTypeValid(bool abnormalBleeding)
		{
			YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public YellowstonePathology.Shared.ValidationResult IsAbnormalBleedingDomainValid()
		{
			YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}
		#endregion

		#region ValidateBirthControl
		public static YellowstonePathology.Shared.ValidationResult IsBirthControlDataTypeValid(bool birthControl)
		{
			YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public YellowstonePathology.Shared.ValidationResult IsBirthControlDomainValid()
		{
			YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}
		#endregion

		#region ValidateHormoneTherapy
		public static YellowstonePathology.Shared.ValidationResult IsHormoneTherapyDataTypeValid(bool hormoneTherapy)
		{
			YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public YellowstonePathology.Shared.ValidationResult IsHormoneTherapyDomainValid()
		{
			YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}
		#endregion

		#region ValidatePreviousNormalPap
		public static YellowstonePathology.Shared.ValidationResult IsPreviousNormalPapDataTypeValid(bool previousNormalPap)
		{
			YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public YellowstonePathology.Shared.ValidationResult IsPreviousNormalPapDomainValid()
		{
			YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}
		#endregion

		#region ValidatePreviousNormalPapDate
		public static YellowstonePathology.Shared.ValidationResult IsPreviousNormalPapDateDataTypeValid(string previousNormalPapDate)
		{
			YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public YellowstonePathology.Shared.ValidationResult IsPreviousNormalPapDateDomainValid()
		{
			YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}
		#endregion

		#region ValidatePreviousAbnormalPap
		public static YellowstonePathology.Shared.ValidationResult IsPreviousAbnormalPapDataTypeValid(bool previousAbnormalPap)
		{
			YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public YellowstonePathology.Shared.ValidationResult IsPreviousAbnormalPapDomainValid()
		{
			YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}
		#endregion

		#region ValidatePreviousAbnormalPapDate
		public static YellowstonePathology.Shared.ValidationResult IsPreviousAbnormalPapDateDataTypeValid(string previousAbnormalPapDate)
		{
			YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public YellowstonePathology.Shared.ValidationResult IsPreviousAbnormalPapDateDomainValid()
		{
			YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}
		#endregion

		#region ValidatePreviousBiopsy
		public static YellowstonePathology.Shared.ValidationResult IsPreviousBiopsyDataTypeValid(bool previousBiopsy)
		{
			YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public YellowstonePathology.Shared.ValidationResult IsPreviousBiopsyDomainValid()
		{
			YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}
		#endregion

		#region ValidatePreviousBiopsyDate
		public static YellowstonePathology.Shared.ValidationResult IsPreviousBiopsyDateDataTypeValid(string previousBiopsyDate)
		{
			YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public YellowstonePathology.Shared.ValidationResult IsPreviousBiopsyDateDomainValid()
		{
			YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}
		#endregion

		#region ValidatePrenatal
		public static YellowstonePathology.Shared.ValidationResult IsPrenatalDataTypeValid(bool prenatal)
		{
			YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public YellowstonePathology.Shared.ValidationResult IsPrenatalDomainValid()
		{
			YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}
		#endregion

		#region ValidatePostpartum
		public static YellowstonePathology.Shared.ValidationResult IsPostpartumDataTypeValid(bool postpartum)
		{
			YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public YellowstonePathology.Shared.ValidationResult IsPostpartumDomainValid()
		{
			YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}
		#endregion

		#region ValidatePostmenopausal
		public static YellowstonePathology.Shared.ValidationResult IsPostmenopausalDataTypeValid(bool postmenopausal)
		{
			YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public YellowstonePathology.Shared.ValidationResult IsPostmenopausalDomainValid()
		{
			YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}
		#endregion

		#region ValidateIcd9Code
		public static YellowstonePathology.Shared.ValidationResult IsIcd9CodeDataTypeValid(string icd9Code)
		{
			YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public YellowstonePathology.Shared.ValidationResult IsIcd9CodeDomainValid()
		{
			YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			if (string.IsNullOrEmpty(this.m_Icd9Code) == true)
			{
				validationResult.IsValid = false;
				validationResult.Message = "You must enter an ICD9 code before you can continue.";
			}
			return validationResult;
		}
		#endregion

		#region ValidateTrichomonasVaginalis
		public static YellowstonePathology.Shared.ValidationResult IsTrichomonasVaginalisDataTypeValid(bool trichomonasVaginalis)
		{
			YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}

		public YellowstonePathology.Shared.ValidationResult IsTrichomonasVaginalisDomainValid()
		{
			YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
			validationResult.IsValid = true;
			return validationResult;
		}
		#endregion
	}
}
