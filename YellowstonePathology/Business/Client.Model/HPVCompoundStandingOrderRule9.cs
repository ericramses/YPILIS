using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPVCompoundStandingOrderRule9 : StandingOrder
    {
        public HPVCompoundStandingOrderRule9()
        {
            this.m_StandingOrderCode = "STNDHPVCMPRL9";
            this.m_Description = "Combines Rule 3 and Rule 13";
            this.m_ReflexOrder = new ReflexOrder();
			this.m_ReflexOrder.PanelSet = new YellowstonePathology.Business.Test.HPV.HPVTest();
            this.m_IsCompoundRule = true;
        }

        public override bool IsRequired(Business.Test.AccessionOrder accessionOrder)
        {
            bool result = false;
            HPVReflexOrderRule3 hpvReflexOrderRule3 = new HPVReflexOrderRule3();
            if (hpvReflexOrderRule3.IsRequired(accessionOrder) == true)
            {
                result = true;
            }

            HPVReflexOrderRule13 hpvReflexOrderRule13 = new HPVReflexOrderRule13();
            if (hpvReflexOrderRule13.IsRequired(accessionOrder) == true)
            {
                result = true;
            }
            return result;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("Compound Rule #9");

            HPVReflexOrderRule3 hpvReflexOrderRule3 = new HPVReflexOrderRule3();
            result.AppendLine("3.) " + hpvReflexOrderRule3.Description);

            HPVReflexOrderRule13 hpvReflexOrderRule13 = new HPVReflexOrderRule13();
            result.AppendLine("13.) " + hpvReflexOrderRule13.Description);

            return result.ToString().TrimEnd();
        }

        public override string PatientAge
        {
            get
            {
                HPVReflexOrderRule3 hpvReflexOrderRule3 = new HPVReflexOrderRule3();
                return hpvReflexOrderRule3.PatientAge;
            }
        }

        public override string PAPResult
        {
            get
            {
                HPVReflexOrderRule3 hpvReflexOrderRule3 = new HPVReflexOrderRule3();
                return hpvReflexOrderRule3.PAPResult;
            }
        }

        public override string HPVResult
        {
            get
            {
                HPVReflexOrderRule3 hpvReflexOrderRule3 = new HPVReflexOrderRule3();
                return hpvReflexOrderRule3.HPVResult;
            }
        }

        public override string HPVTesting
        {
            get
            {
                HPVReflexOrderRule3 hpvReflexOrderRule3 = new HPVReflexOrderRule3();
                return hpvReflexOrderRule3.HPVTesting;
            }
        }

        public override string PatientAgeCompound
        {
            get
            {
                HPVReflexOrderRule13 hpvReflexOrderRule13 = new HPVReflexOrderRule13();
                return hpvReflexOrderRule13.PatientAge;
            }
        }

        public override string PAPResultCompound
        {
            get
            {
                HPVReflexOrderRule13 hpvReflexOrderRule13 = new HPVReflexOrderRule13();
                return hpvReflexOrderRule13.PAPResult;
            }
        }

        public override string HPVResultCompound
        {
            get
            {
                HPVReflexOrderRule13 hpvReflexOrderRule13 = new HPVReflexOrderRule13();
                return hpvReflexOrderRule13.HPVResult;
            }
        }

        public override string HPVTestingCompound
        {
            get
            {
                HPVReflexOrderRule13 hpvReflexOrderRule13 = new HPVReflexOrderRule13();
                return hpvReflexOrderRule13.HPVTesting;
            }
        }
    }
}
