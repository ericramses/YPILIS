using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test
{
    public partial class AccessionOrder
    {
        public YellowstonePathology.Shared.ValidationResult IsMedicalRecordNoValid()
        {
            YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
            validationResult.IsValid = true;

            YellowstonePathology.Business.Client.Model.WestParkHospitalGroup westParkHospitalGroup = new YellowstonePathology.Business.Client.Model.WestParkHospitalGroup();
            if (westParkHospitalGroup.Exists(this.m_ClientId) == true)
            {
                if (string.IsNullOrEmpty(this.m_SvhMedicalRecord) == false)
                {
                    if (this.SvhMedicalRecord.Length != 8)
                    {
                        validationResult.IsValid = false;
                        validationResult.Message = "West Park Hostpital medical record numbers must be 8 characters long.";
                    }
                    else if (this.SvhMedicalRecord.StartsWith("W") == false)
                    {
                        validationResult.IsValid = false;
                        validationResult.Message = "West Park Hostpital medical record numbers must start with the letter W.";
                    }
                }
                else
                {
                    validationResult.IsValid = false;
                    validationResult.Message = "West Park Hostpital medical record number must not be blank.";
                }
            }
            return validationResult;
        }

        public YellowstonePathology.Shared.ValidationResult IsAccountNoValid()
        {
            YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
            validationResult.IsValid = true;

            YellowstonePathology.Business.Client.Model.WestParkHospitalGroup westParkHospitalGroup = new YellowstonePathology.Business.Client.Model.WestParkHospitalGroup();
            if (westParkHospitalGroup.Exists(this.m_ClientId) == true)
            {
                if (string.IsNullOrEmpty(this.m_SvhAccount) == false)
                {
                    if (this.SvhAccount.Length != 9)
                    {
                        validationResult.IsValid = false;
                        validationResult.Message = "West Park Hostpital account numbers must be 9 characters long.";
                    }
                    else if (this.SvhAccount.StartsWith("A") == false)
                    {
                        validationResult.IsValid = false;
                        validationResult.Message = "West Park Hostpital account numbers must start with the letter A.";
                    }
                }
                else
                {
                    validationResult.IsValid = false;
                    validationResult.Message = "The West Park Hostpital account number must not be blank.";
                }
            }
            return validationResult;
        }
    }
}
