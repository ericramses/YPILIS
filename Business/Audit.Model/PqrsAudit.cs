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
            int patientAge = YellowstonePathology.Business.Helper.PatientHelper.GetAge(this.m_AccessionOrder.PBirthdate.Value);
            foreach (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen in surgicalTestOrder.SurgicalSpecimenCollection)
            {
                foreach (YellowstonePathology.Business.Surgical.PQRSMeasure pqrsMeasure in pqrsCollection)
                {
                    if (pqrsMeasure.DoesMeasureApply(surgicalTestOrder, surgicalSpecimen, patientAge) == true)
                    {
                        YellowstonePathology.Business.Test.PanelSetOrderCPTCodeCollection panelSetOrderCPTCodeCollection = surgicalTestOrder.PanelSetOrderCPTCodeCollection.GetSpecimenOrderCollection(surgicalSpecimen.SpecimenOrderId);
                        if (this.MeasureCodeExists(pqrsMeasure, panelSetOrderCPTCodeCollection) == false)
                        {
                            this.m_Status = AuditStatusEnum.Failure;
                            this.m_Message.AppendLine("A PQRS code must be applied.");
                            break;
                        }
                    }
                }
                if (this.m_Status == AuditStatusEnum.Failure) break;
            }
        }

        private bool MeasureCodeExists(YellowstonePathology.Business.Surgical.PQRSMeasure pqrsMeasure,
            YellowstonePathology.Business.Test.PanelSetOrderCPTCodeCollection panelSetOrderCPTCodeCollection)
        {
            bool result = false;
            foreach (YellowstonePathology.Business.Billing.Model.PQRSCode pqrsCode in pqrsMeasure.PQRSCodeCollection)
            {
                if (panelSetOrderCPTCodeCollection.Exists(pqrsCode.Code, 1) == true)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
    }
}
