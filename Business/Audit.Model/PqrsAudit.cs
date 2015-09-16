using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class PqrsAudit : Audit
    {
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        public PqrsAudit(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;
        }

        public override void Run()
        {
            this.m_Status = AuditStatusEnum.OK;
            YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();
            YellowstonePathology.Business.Surgical.PQRSMeasureCollection pqrsCollection = YellowstonePathology.Business.Surgical.PQRSMeasureCollection.GetAll();
            foreach (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen in surgicalTestOrder.SurgicalSpecimenCollection)
            {
                foreach (YellowstonePathology.Business.Surgical.PQRSMeasure pqrsMeasure in pqrsCollection)
                {
                    int patientAge = YellowstonePathology.Business.Helper.PatientHelper.GetAge(this.m_AccessionOrder.PBirthdate.Value);
                    if (pqrsMeasure.DoesMeasureApply(surgicalTestOrder, surgicalSpecimen, patientAge) == true)
                    {
                        this.m_Status = AuditStatusEnum.Failure;
                        this.m_Message.AppendLine("A PQRS code must be applied.");
                        break;
                    }
                }
                if (this.m_Status == AuditStatusEnum.Failure) break;
            }
        }
    }
}
