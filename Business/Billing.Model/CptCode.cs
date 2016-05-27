using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Billing.Model
{
    public class CptCode
    {
        protected string m_Code;        
        protected string m_Description;
        protected FeeScheduleEnum m_FeeSchedule;
        protected bool m_HasTechnicalComponent;
        protected bool m_HasProfessionalComponent;
        protected string m_Modifier;
        protected bool m_IsBillable;
        protected string m_GCode;
        protected bool m_HasMedicareQuantityLimit;
        protected int m_MedicareQuantityLimit;
        protected CPTCodeTypeEnum m_CodeType;
          
        public CptCode()
        {
            this.m_HasMedicareQuantityLimit = false;            
        }

        [PersistentProperty()]
        public string Code
        {
            get { return this.m_Code; }
        }

        [PersistentProperty()]
        public string Description
        {
            get { return this.m_Description; }
        }

        [PersistentProperty()]
        public string Modifier
        {
            get { return this.m_Modifier; }
            set { this.m_Modifier = value; }
        }

        [PersistentProperty()]
        public FeeScheduleEnum FeeSchedule
        {
            get { return this.m_FeeSchedule; }
        }

        [PersistentProperty()]
        public bool HasTechnicalComponent
        {
            get { return this.m_HasTechnicalComponent; }
        }

        [PersistentProperty()]
        public bool HasProfessionalComponent
        {
            get { return this.m_HasProfessionalComponent; }
        }

        [PersistentProperty()]
        public CPTCodeTypeEnum CodeType
        {
            get { return this.m_CodeType; }
        }

        [PersistentProperty()]
        public bool IsBillable
        {
            get { return this.m_IsBillable; }
        }

        [PersistentProperty()]
        public string GCode
        {
            get { return this.m_GCode; }
        }

        [PersistentProperty()]
        public bool HasMedicareQuantityLimit
        {
            get { return this.m_HasMedicareQuantityLimit; }
        }

        [PersistentProperty()]
        public int MedicareQuantityLimit
        {
            get { return this.m_MedicareQuantityLimit; }
        }

        public bool HasBillableProfessionalComponent()
        {
            bool result = true;
            if (this.Modifier == "26") result = true;
            if (string.IsNullOrEmpty(this.Modifier) == true)
            {
                if (this.m_HasProfessionalComponent == true)
                {
                    result = true;
                }
            }
            return result;
        }

        public bool HasBillableTechnicalComponent()
        {
            bool result = false;
            if (this.Modifier == "TC") result = true;
            if (string.IsNullOrEmpty(this.Modifier) == true)
            {
                if (this.m_HasTechnicalComponent == true)
                {
                    result = true;
                }
            }
            return result;
        }

        public virtual string GetModifier(YellowstonePathology.Business.Billing.Model.BillingComponentEnum billingComponent)
        {
            string result = null;
            switch (billingComponent)
            {
                case BillingComponentEnum.Professional:
                    if (this.m_HasTechnicalComponent == true && this.m_HasProfessionalComponent == true)
                    {
                        result = YellowstonePathology.Business.Billing.Model.CPTCodeModifier.TwentySix;
                    }
                    else if (this.m_HasTechnicalComponent == false && this.m_HasProfessionalComponent == true)
                    {
                        result = null;
                    }
                    else if (this.m_HasTechnicalComponent == true && this.m_HasProfessionalComponent == false)
                    {
                        throw new Exception("This code does not have a professional component.");
                    }
                    break;
                case BillingComponentEnum.Technical:
                    if (this.m_HasTechnicalComponent == true && this.m_HasProfessionalComponent == true)
                    {
                        result = YellowstonePathology.Business.Billing.Model.CPTCodeModifier.TechnicalComponent;
                    }
                    else if (this.m_HasTechnicalComponent == true && this.m_HasProfessionalComponent == false)
                    {
                        result = null;
                    }
                    else if (this.m_HasTechnicalComponent == false && this.m_HasProfessionalComponent == true)
                    {
                        throw new Exception("This code does not have a technical component.");
                    }
                    break;
                case BillingComponentEnum.Global:
                    result = null;
                    break;
            }
            return result;
        }

        public static CptCode Clone(CptCode cptCodeIn)
        {
            return (CptCode)cptCodeIn.MemberwiseClone();
        }
    }
}
