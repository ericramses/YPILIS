using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPVCompoundStandingOrderRule5 : StandingOrder
    {
        public HPVCompoundStandingOrderRule5()
        {
            this.m_StandingOrderCode = "STNDHPVCMPRL5";
            this.m_Description = "Combines Rule 15 and Rule 16";
            this.m_ReflexOrder = new ReflexOrder();
			this.m_ReflexOrder.PanelSet = new YellowstonePathology.Business.Test.HPV.HPVTest();
            this.m_IsCompoundRule = true;
        }

        public override bool IsRequired(Business.Test.AccessionOrder accessionOrder)
        {
            bool result = false;
            HPVReflexOrderRule15 hpvReflexOrderRule15 = new HPVReflexOrderRule15();
            if (hpvReflexOrderRule15.IsRequired(accessionOrder) == true)
            {
                result = true;
            }

            HPVReflexOrderRule16 hpvReflexOrderRule16 = new HPVReflexOrderRule16();
            if (hpvReflexOrderRule16.IsRequired(accessionOrder) == true)
            {
                result = true;
            }
            return result;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("Compound Rule #5");

            HPVReflexOrderRule15 hpvReflexOrderRule15 = new HPVReflexOrderRule15();
            result.AppendLine("15.) " + hpvReflexOrderRule15.Description);

            HPVReflexOrderRule16 hpvReflexOrderRule16 = new HPVReflexOrderRule16();
            result.AppendLine("16.) " + hpvReflexOrderRule16.Description);

            return result.ToString().TrimEnd();
        }

        public override string PatientAge
        {
            get
            {
                HPVReflexOrderRule15 hpvReflexOrderRule15 = new HPVReflexOrderRule15();
                return hpvReflexOrderRule15.PatientAge;
            }
        }

        public override string PAPResult
        {
            get
            {
                HPVReflexOrderRule15 hpvReflexOrderRule15 = new HPVReflexOrderRule15();
                return hpvReflexOrderRule15.PAPResult;
            }
        }

        public override string HPVResult
        {
            get
            {
                HPVReflexOrderRule15 hpvReflexOrderRule15 = new HPVReflexOrderRule15();
                return hpvReflexOrderRule15.HPVResult;
            }
        }

        public override string PatientAgeCompound
        {
            get
            {
                HPVReflexOrderRule16 hpvReflexOrderRule16 = new HPVReflexOrderRule16();
                return hpvReflexOrderRule16.PatientAge;
            }
        }

        public override string PAPResultCompound
        {
            get
            {
                HPVReflexOrderRule16 hpvReflexOrderRule16 = new HPVReflexOrderRule16();
                return hpvReflexOrderRule16.PAPResult;
            }
        }

        public override string HPVResultCompound
        {
            get
            {
                HPVReflexOrderRule16 hpvReflexOrderRule16 = new HPVReflexOrderRule16();
                return hpvReflexOrderRule16.HPVResult;
            }
        }
    }
}
