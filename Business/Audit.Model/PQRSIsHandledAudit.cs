using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class PQRSIsHandledAudit : Audit
    {
        private YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder m_SurgicalTestOrder;
        private bool m_PQRSIsRequired;
        private bool m_PQRSHasBeenResovled;

        public PQRSIsHandledAudit(YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder, bool pqrsIsRequired, bool pqrsHasBeenResovled)
        {
            this.m_SurgicalTestOrder = surgicalTestOrder;
            this.m_PQRSIsRequired = pqrsIsRequired;
            this.m_PQRSHasBeenResovled = pqrsHasBeenResovled;
        }

        public override void Run()
        {
            this.m_Status = AuditStatusEnum.OK;
            this.m_Message.Clear();

            if(this.m_PQRSIsRequired == true && this.m_SurgicalTestOrder.PQRIInstructions == 0)
            {
                if (this.m_PQRSHasBeenResovled == false)
                {
                    this.m_Status = AuditStatusEnum.Failure;
                    this.m_Message.Append("A PQRS code must be applied.");
                }
            }
        }
    }
}
