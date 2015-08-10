using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test
{    
    public partial class FNAAdequacyAssessmentResult
	{        
        public static YellowstonePathology.Shared.ValidationResult IsDateDataTypeValid(string date)
        {
            YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
            validationResult.IsValid = false;

            if (string.IsNullOrEmpty(date) == false)
            {
                if (YellowstonePathology.Business.Helper.DateTimeExtensions.IsStringAValidDate(date) == true)
                {
                    validationResult.IsValid = true;
                    validationResult.Message = "The start date entered is not a valid date.";
                }
            }
            
            return validationResult;
        }       

        public YellowstonePathology.Shared.ValidationResult IsEndDateGreaterThanStartDate()
        {
            YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
            validationResult.IsValid = true;
            
            if (this.m_StartDate.HasValue == true && this.m_EndDate.HasValue == true)
            {
                if (this.m_EndDate < this.m_StartDate)
                {
                    validationResult.IsValid = false;
                    validationResult.Message = "The end date must be greater than the start date";
                }
            }
            
            return validationResult;
        }

        public YellowstonePathology.Shared.ValidationResult IsStartDateBlank()
        {
            YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
            validationResult.IsValid = true;

            if (this.m_StartDate.HasValue == true && this.m_EndDate.HasValue == true)
            {
                if (this.m_EndDate < this.m_StartDate)
                {
                    validationResult.IsValid = false;
                    validationResult.Message = "The start date must not be blank.";                
                }
            }

            return validationResult;
        }

        public YellowstonePathology.Shared.ValidationResult IsEndDateBlank()
        {
            YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
            validationResult.IsValid = true;

            if (this.m_StartDate.HasValue == false)
            {                
                validationResult.IsValid = false;
                validationResult.Message = "The end date must not be blank.";                
            }

            return validationResult;
        }

        public YellowstonePathology.Shared.ValidationResult DoesStartDateHaveATime()
        {
            YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
            validationResult.IsValid = true;

            if (YellowstonePathology.Business.Helper.DateTimeExtensions.DoesDateHaveTime(this.m_StartDate) == false)
            {
                validationResult.IsValid = false;
                validationResult.Message = "The start date needs to have a time entered as well as the date.";
            }

            return validationResult;
        }

        public YellowstonePathology.Shared.ValidationResult DoesEndDateHaveATime()
        {
            YellowstonePathology.Shared.ValidationResult validationResult = new Shared.ValidationResult();
            validationResult.IsValid = true;
            
            if (YellowstonePathology.Business.Helper.DateTimeExtensions.DoesDateHaveTime(this.m_EndDate) ==  false)
            {
                validationResult.IsValid = false;
                validationResult.Message = "The end date needs to have a time entered as well as the date.";
            }

            return validationResult;
        }
	}
}
