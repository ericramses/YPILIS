using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPV1618CompoundStandingOrderRule1 : StandingOrder
    {
        public HPV1618CompoundStandingOrderRule1()
        {
            this.m_StandingOrderCode = "STNDHPV1618PAPNRMLPOSHPVPOS";
            this.m_Description = "Combines HPV1618 Rule 1 and Rule 2";
            this.m_ReflexOrder = new ReflexOrder();
			this.m_ReflexOrder.PanelSet = new YellowstonePathology.Business.Test.HPV1618.HPV1618Test();
            this.m_IsCompoundRule = true;
        }

        public override bool IsRequired(Business.Test.AccessionOrder accessionOrder)
        {
            bool result = false;
            HPV1618ReflexOrderHPVPositive hpv1618ReflexOrderHPVPositive = new HPV1618ReflexOrderHPVPositive();
            if (hpv1618ReflexOrderHPVPositive.IsRequired(accessionOrder) == true)
            {
                result = true;
            }

            HPV1618ReflexOrderPAPNormalHPVPositive hpv1618ReflexOrderPAPNormalHPVPositive = new HPV1618ReflexOrderPAPNormalHPVPositive();
            if (hpv1618ReflexOrderPAPNormalHPVPositive.IsRequired(accessionOrder) == true)
            {
                result = true;
            }
            return result;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine("HPV1618 Compound Rule #1");

            HPV1618ReflexOrderHPVPositive hpv1618ReflexOrderHPVPositive = new HPV1618ReflexOrderHPVPositive();
            result.AppendLine("1.) " + hpv1618ReflexOrderHPVPositive.Description);

            HPV1618ReflexOrderPAPNormalHPVPositive hpv1618ReflexOrderPAPNormalHPVPositive = new HPV1618ReflexOrderPAPNormalHPVPositive();
            result.AppendLine("2.) " + hpv1618ReflexOrderPAPNormalHPVPositive.Description);

            return result.ToString().TrimEnd();
        }

        public override string PatientAge
        {
            get
            {
                HPV1618ReflexOrderHPVPositive hpv1618ReflexOrderHPVPositive = new HPV1618ReflexOrderHPVPositive();
                return hpv1618ReflexOrderHPVPositive.PatientAge;
            }
        }

        public override string PAPResult
        {
            get
            {
                HPV1618ReflexOrderHPVPositive hpv1618ReflexOrderHPVPositive = new HPV1618ReflexOrderHPVPositive();
                return hpv1618ReflexOrderHPVPositive.PAPResult;
            }
        }

        public override string HPVResult
        {
            get
            {
                HPV1618ReflexOrderHPVPositive hpv1618ReflexOrderHPVPositive = new HPV1618ReflexOrderHPVPositive();
                return hpv1618ReflexOrderHPVPositive.HPVResult;
            }
        }

        public override string HPVTesting
        {
            get
            {
                HPV1618ReflexOrderHPVPositive hpv1618ReflexOrderHPVPositive = new HPV1618ReflexOrderHPVPositive();
                return hpv1618ReflexOrderHPVPositive.HPVTesting;
            }
        }

        public override string PatientAgeCompound
        {
            get
            {
                HPV1618ReflexOrderPAPNormalHPVPositive hpv1618ReflexOrderPAPNormalHPVPositive = new HPV1618ReflexOrderPAPNormalHPVPositive();
                return hpv1618ReflexOrderPAPNormalHPVPositive.PatientAge;
            }
        }

        public override string PAPResultCompound
        {
            get
            {
                HPV1618ReflexOrderPAPNormalHPVPositive hpv1618ReflexOrderPAPNormalHPVPositive = new HPV1618ReflexOrderPAPNormalHPVPositive();
                return hpv1618ReflexOrderPAPNormalHPVPositive.PAPResult;
            }
        }

        public override string HPVResultCompound
        {
            get
            {
                HPV1618ReflexOrderPAPNormalHPVPositive hpv1618ReflexOrderPAPNormalHPVPositive = new HPV1618ReflexOrderPAPNormalHPVPositive();
                return hpv1618ReflexOrderPAPNormalHPVPositive.HPVResult;
            }
        }

        public override string HPVTestingCompound
        {
            get
            {
                HPV1618ReflexOrderPAPNormalHPVPositive hpv1618ReflexOrderPAPNormalHPVPositive = new HPV1618ReflexOrderPAPNormalHPVPositive();
                return hpv1618ReflexOrderPAPNormalHPVPositive.HPVTesting;
            }
        }
    }
}
