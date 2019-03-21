using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPVCompoundStandingOrderRule1 : StandingOrder
    {
        public HPVCompoundStandingOrderRule1()
        {
            this.m_StandingOrderCode = "STNDHPVCMPRL1";
            this.m_Description = "Combines Rule 1 and Rule 10";
            this.m_ReflexOrder = new ReflexOrder();
			this.m_ReflexOrder.PanelSet = new YellowstonePathology.Business.Test.HPV.HPVTest();
            this.m_IsCompoundRule = true;
        }

        public override bool IsRequired(Business.Test.AccessionOrder accessionOrder)
        {
            bool result = false;
            HPVReflexOrderRule1 hpvReflexOrderRule1 = new HPVReflexOrderRule1();
            if (hpvReflexOrderRule1.IsRequired(accessionOrder) == true)
            {
                result = true;
            }

            HPVReflexOrderRule10 hpvReflexOrderRule10 = new HPVReflexOrderRule10();
            if (hpvReflexOrderRule10.IsRequired(accessionOrder) == true)
            {
                result = true;
            }
            return result;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("Compound Rule #1");

            HPVReflexOrderRule1 hpvReflexOrderRule1 = new HPVReflexOrderRule1();
            result.AppendLine("1.) " + hpvReflexOrderRule1.Description);

            HPVReflexOrderRule10 hpvReflexOrderRule10 = new HPVReflexOrderRule10();
            result.AppendLine("10.) " + hpvReflexOrderRule10.Description);

            return result.ToString().TrimEnd();
        }

        public override string PatientAge
        {
            get
            {
                HPVReflexOrderRule1 hpvReflexOrderRule1 = new HPVReflexOrderRule1();
                return hpvReflexOrderRule1.PatientAge;
            }
        }

        public override string PAPResult
        {
            get
            {
                HPVReflexOrderRule1 hpvReflexOrderRule1 = new HPVReflexOrderRule1();
                return hpvReflexOrderRule1.PAPResult;
            }
        }

        public override string HPVResult
        {
            get
            {
                HPVReflexOrderRule1 hpvReflexOrderRule1 = new HPVReflexOrderRule1();
                return hpvReflexOrderRule1.HPVResult;
            }
        }

        public override string HPVTesting
        {
            get
            {
                HPVReflexOrderRule1 hpvReflexOrderRule1 = new HPVReflexOrderRule1();
                return hpvReflexOrderRule1.HPVTesting;
            }
        }

        public override string PatientAgeCompound
        {
            get
            {
                HPVReflexOrderRule10 hpvReflexOrderRule10 = new HPVReflexOrderRule10();
                return hpvReflexOrderRule10.PatientAge;
            }
        }

        public override string PAPResultCompound
        {
            get
            {
                HPVReflexOrderRule10 hpvReflexOrderRule10 = new HPVReflexOrderRule10();
                return hpvReflexOrderRule10.PAPResult;
            }
        }

        public override string HPVResultCompound
        {
            get
            {
                HPVReflexOrderRule10 hpvReflexOrderRule10 = new HPVReflexOrderRule10();
                return hpvReflexOrderRule10.HPVResult;
            }
        }

        public override string HPVTestingCompound
        {
            get
            {
                HPVReflexOrderRule10 hpvReflexOrderRule10 = new HPVReflexOrderRule10();
                return hpvReflexOrderRule10.HPVTesting;
            }
        }
    }
}
