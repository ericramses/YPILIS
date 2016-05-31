using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Domain.Billing
{
    public class BillingRule
    {
        public BillingRule()
        {

        }

        public virtual void Run(CptBillingCode cptBillingCode)
        {
            throw new Exception("Not Implemented Here");
        }        
    }
}
