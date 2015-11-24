using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class PQRSIsHandledAudit : Audit
    {
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;

        public PQRSIsHandledAudit(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;
        }

        public override void Run()
        {
            this.m_Status = AuditStatusEnum.OK;
            this.m_Message.Clear();

            YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();
            if(surgicalTestOrder.PQRSIsIndicated == true)
            {
                if (surgicalTestOrder.PQRSNotApplicable == false)
                {
                    YellowstonePathology.Business.Surgical.PQRSMeasureCollection pqrsCollection = YellowstonePathology.Business.Surgical.PQRSMeasureCollection.GetAll();
                    YellowstonePathology.Business.Surgical.PQRSMeasure useThisPQRSMeasure = null;
                    int patientAge = YellowstonePathology.Business.Helper.PatientHelper.GetAge(this.m_AccessionOrder.PBirthdate.Value);
                    foreach (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen in surgicalTestOrder.SurgicalSpecimenCollection)
                    {
                        foreach (YellowstonePathology.Business.Surgical.PQRSMeasure pqrsMeasure in pqrsCollection)
                        {
                            if (pqrsMeasure.DoesMeasureApply(surgicalTestOrder, surgicalSpecimen, patientAge) == true)
                            {
                                useThisPQRSMeasure = pqrsMeasure;
                                break;
                            }
                        }

                        if(useThisPQRSMeasure != null)
                        {
                            if (this.MeasureCodeExists(useThisPQRSMeasure, surgicalTestOrder.PanelSetOrderCPTCodeCollection) == false)
                            {
                                this.m_Status = AuditStatusEnum.Failure;
                                this.m_Message.Append("A PQRS code must be applied.");
                            }
                            break;
                        }
                    }
                }
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
