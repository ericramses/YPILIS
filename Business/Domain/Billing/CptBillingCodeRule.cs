using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Domain.Billing
{
    public class CptBillingCodeRule
    {
        YellowstonePathology.Business.Billing.Model.CptCode m_CptCode;
        YellowstonePathology.Business.Billing.Model.BillingTypeEnum m_BillTechnicalComponentTo;
        YellowstonePathology.Business.Billing.Model.BillingTypeEnum m_BillProfessionalComponentTo;

        public CptBillingCodeRule()
        {

        }        
    }
}
