using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPVCompoundStandingOrderRule4 : StandingOrder
    {
        public HPVCompoundStandingOrderRule4()
        {
            this.m_StandingOrderCode = "STNDHPVCMPRL4";
            this.m_Description = "Combines Rule 2 and Rule 12";
            this.m_ReflexOrder = new ReflexOrder();
			this.m_ReflexOrder.PanelSet = new YellowstonePathology.Business.Test.HPV.HPVTest();
            this.m_IsCompoundRule = true;
        }

        public override bool IsRequired(Business.Test.AccessionOrder accessionOrder)
        {
            bool result = false;
            HPVReflexOrderRule2 hpvReflexOrderRule2 = new HPVReflexOrderRule2();
            if (hpvReflexOrderRule2.IsRequired(accessionOrder) == true)
            {
                result = true;
            }

            HPVReflexOrderRule12 hpvReflexOrderRule12 = new HPVReflexOrderRule12();
            if (hpvReflexOrderRule12.IsRequired(accessionOrder) == true)
            {
                result = true;
            }
            return result;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("Compound Rule #4");

            HPVReflexOrderRule2 hpvReflexOrderRule2 = new HPVReflexOrderRule2();
            result.AppendLine("2.) " + hpvReflexOrderRule2.Description);

            HPVReflexOrderRule12 hpvReflexOrderRule12 = new HPVReflexOrderRule12();
            result.AppendLine("12.) " + hpvReflexOrderRule12.Description);

            return result.ToString().TrimEnd();
        }

        public override string PatientAge
        {
            get
            {
                HPVReflexOrderRule2 hpvReflexOrderRule2 = new HPVReflexOrderRule2();
                return hpvReflexOrderRule2.PatientAge;
            }
        }

        public override string PAPResult
        {
            get
            {
                HPVReflexOrderRule2 hpvReflexOrderRule2 = new HPVReflexOrderRule2();
                return hpvReflexOrderRule2.PAPResult;
            }
        }

        public override string HPVResult
        {
            get
            {
                HPVReflexOrderRule2 hpvReflexOrderRule2 = new HPVReflexOrderRule2();
                return hpvReflexOrderRule2.HPVResult;
            }
        }

        public override string HPVTesting
        {
            get
            {
                HPVReflexOrderRule2 hpvReflexOrderRule2 = new HPVReflexOrderRule2();
                return hpvReflexOrderRule2.HPVTesting;
            }
        }

        public override string PatientAgeCompound
        {
            get
            {
                HPVReflexOrderRule12 hpvReflexOrderRule12 = new HPVReflexOrderRule12();
                return hpvReflexOrderRule12.PatientAge;
            }
        }

        public override string PAPResultCompound
        {
            get
            {
                HPVReflexOrderRule12 hpvReflexOrderRule12 = new HPVReflexOrderRule12();
                return hpvReflexOrderRule12.PAPResult;
            }
        }

        public override string HPVResultCompound
        {
            get
            {
                HPVReflexOrderRule12 hpvReflexOrderRule12 = new HPVReflexOrderRule12();
                return hpvReflexOrderRule12.HPVResult;
            }
        }

        public override string HPVTestingCompound
        {
            get
            {
                HPVReflexOrderRule12 hpvReflexOrderRule12 = new HPVReflexOrderRule12();
                return hpvReflexOrderRule12.HPVTesting;
            }
        }
    }
}
