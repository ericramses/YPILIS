﻿using System;
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
        protected CptCodeModifier m_Modifier;
        protected bool m_IsBillable;
        protected string m_GCode;
        protected bool m_HasMedicareQuantityLimit;
        protected int m_MedicareQuantityLimit;
        protected CPTCodeTypeEnum m_CodeType;
        protected string m_SVHCDMCode;
        protected string m_SVHCDMDescription;

        protected List<CptCodeModifier> m_Modifiers;


        public CptCode()
        {            
            this.m_HasMedicareQuantityLimit = false;
        }

        public string DisplayCode
        {
            get
            {
                string result = this.m_Code;
                if(this.m_Modifier != null)
                {
                    if(string.IsNullOrEmpty(this.m_Modifier.Modifier) == false)
                    {
                        result += " - " + this.m_Modifier.Modifier;
                    }
                }
                return result;
            }
        }

        [PersistentProperty()]
        public string Code
        {
            get { return this.m_Code; }
            set { this.m_Code = value; }
        }

        [PersistentProperty()]
        public string Description
        {
            get { return this.m_Description; }
            set { this.m_Description = value; }
        }

        [PersistentProperty()]
        public CptCodeModifier Modifier
        {
            get { return this.m_Modifier; }
            set { this.m_Modifier = value; }
        }

        [PersistentProperty()]
        public FeeScheduleEnum FeeSchedule
        {
            get { return this.m_FeeSchedule; }
            set { this.m_FeeSchedule = value; }
        }

        [PersistentProperty()]
        public bool HasTechnicalComponent
        {
            get { return this.m_HasTechnicalComponent; }
            set { this.m_HasTechnicalComponent = value; }
        }

        [PersistentProperty()]
        public bool HasProfessionalComponent
        {
            get { return this.m_HasProfessionalComponent; }
            set { this.m_HasProfessionalComponent = value; }
        }

        [PersistentProperty()]
        public CPTCodeTypeEnum CodeType
        {
            get { return this.m_CodeType; }
            set { this.m_CodeType = value; }
        }

        [PersistentProperty()]
        public bool IsBillable
        {
            get { return this.m_IsBillable; }
            set { this.m_IsBillable = value; }
        }

        [PersistentProperty()]
        public string GCode
        {
            get { return this.m_GCode; }
            set { this.m_GCode = value; }
        }

        [PersistentProperty()]
        public bool HasMedicareQuantityLimit
        {
            get { return this.m_HasMedicareQuantityLimit; }
            set { this.m_HasMedicareQuantityLimit = value; }
        }

        [PersistentProperty()]
        public int MedicareQuantityLimit
        {
            get { return this.m_MedicareQuantityLimit; }
            set { this.m_MedicareQuantityLimit = value; }
        }

        [PersistentProperty()]
        public string SVHCDMCode
        {
            get { return this.m_SVHCDMCode; }
            set { this.m_SVHCDMCode = value; }
        }

        [PersistentProperty()]
        public string SVHCDMDescription
        {
            get { return this.m_SVHCDMDescription; }
            set { this.m_SVHCDMDescription = value; }
        }

        [PersistentProperty()]
        public List<CptCodeModifier> Modifiers
        {
            get { return this.m_Modifiers; }
            set { this.m_Modifiers = value; }
        }

        public bool HasBillableProfessionalComponent()
        {
            bool result = true;
            if (this.Modifier != null && this.Modifier.Modifier == "26") result = true;
            else if (this.m_HasProfessionalComponent == true)
            {
                result = true;
            }
            return result;
        }

        public bool HasBillableTechnicalComponent()
        {
            bool result = false;
            if (this.Modifier != null && this.Modifier.Modifier == "TC") result = true;
            else if (this.m_HasTechnicalComponent == true)
            {
                result = true;
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
                        result = null;
                        //throw new Exception("This code does not have a professional component.");
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

        public virtual CptCode Clone(CptCode cptCodeIn)
        {
            return (CptCode)cptCodeIn.MemberwiseClone();
        }

        public virtual void SetModifier(string modifier)
        {
            if (string.IsNullOrEmpty(modifier) == false)
            {
                CptCodeModifier cptCodeModifier = new CptCodeModifier();
                cptCodeModifier.Modifier = modifier;
                this.Modifier = cptCodeModifier;
            }
        }
    }
}
