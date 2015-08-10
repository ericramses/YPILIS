using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Domain.Billing
{
    public class BillingComponentNoBilling : BillingComponent
    {
        public BillingComponentNoBilling(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder) :
            base(panelSetOrder)           
        {

        }

        public override void Post(BillableObject billableObject)
        {
            //Do Nothing
        }
    }
}
