using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Audit.Model
{
    public class HPVReflexRuleRequiresCheck : AccessionOrderAudit
    {
        public HPVReflexRuleRequiresCheck(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
            : base(accessionOrder)
        { }

        public override void Run()
        {
            this.DoesHPVAuditRequireHPVOrder();
        }

        private void DoesHPVAuditRequireHPVOrder()
        {
            YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTest womensHealthProfileTest = new YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTest();
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(womensHealthProfileTest.PanelSetId) == true)
            {
                YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder womensHealthProfileTestOrder = (YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(womensHealthProfileTest.PanelSetId);
                if (womensHealthProfileTestOrder.HPVReflexOrderCode == "RFLXHPVRL17")
                {
                    YellowstonePathology.Business.Audit.Model.HPVIsRequiredAudit hpvAudit = new HPVIsRequiredAudit(this.m_AccessionOrder);
                    hpvAudit.Run();
                    if (hpvAudit.ActionRequired == false)
                    {
                        YellowstonePathology.Business.Client.Model.HPVReflexOrderRule14 oldReflexOrder = new Client.Model.HPVReflexOrderRule14();
                        if (oldReflexOrder.IsRequired(this.m_AccessionOrder) == true)
                        {
                            YellowstonePathology.Business.Client.Model.HPVReflexOrderRule17 reflexOrder = new Client.Model.HPVReflexOrderRule17();
                            if (reflexOrder.IsRequired(this.m_AccessionOrder) == false)
                            {
                                this.ActionRequired = true;
                            }
                        }
                    }
                }
            }
        }
    }
}
