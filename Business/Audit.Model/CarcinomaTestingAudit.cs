using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class CarcinomaTestingAudit : Audit
    {
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;

        public CarcinomaTestingAudit(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;
        }

        public override void Run()
        {
            this.m_Status = AuditStatusEnum.OK;
            this.m_Message.Clear();
            YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();
            YellowstonePathology.Business.Surgical.CarcinomaMeasureCollection carcinomaMeasureCollection = YellowstonePathology.Business.Surgical.CarcinomaMeasureCollection.GetAll();
            foreach (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen in surgicalTestOrder.SurgicalSpecimenCollection)
            {
                foreach (YellowstonePathology.Business.Surgical.CarcinomaMeasure carcinomaMeasure in carcinomaMeasureCollection)
                {
                    if (carcinomaMeasure.DoesMeasureApply(surgicalTestOrder, surgicalSpecimen) == true)
                    {
                        this.m_Status = AuditStatusEnum.Failure;
                        this.m_Message.AppendLine(carcinomaMeasure.Header);
                        break;
                    }
                }
                if (this.m_Status == AuditStatusEnum.Failure) break;
            }
        }
    }
}
