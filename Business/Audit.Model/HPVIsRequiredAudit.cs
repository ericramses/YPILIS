using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Audit.Model
{
    public class HPVIsRequiredAudit : AccessionOrderAudit
    {
        public HPVIsRequiredAudit(YellowstonePathology.Business.Test.AccessionOrder accessionOrder) 
            : base(accessionOrder)
        {

        }

        public override void Run()
        {            
			this.IsMarkedOnWomensHealthProfile(this.m_AccessionOrder);
            this.IsRequiredByReflexOrder(this.m_AccessionOrder);
            this.IsRequiredByStandingOrder(this.m_AccessionOrder);
        }

		private void IsMarkedOnWomensHealthProfile(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
			YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTest womensHealthProfileTest = new YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTest();
			if (accessionOrder.PanelSetOrderCollection.Exists(womensHealthProfileTest.PanelSetId) == true)
            {
				YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder womensHealthProfileTestOrder = (YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(womensHealthProfileTest.PanelSetId);
				if (womensHealthProfileTestOrder.OrderHPV == true)
                {
					YellowstonePathology.Business.Test.HPVTWI.HPVTWITest panelSetHPVTWI = new YellowstonePathology.Business.Test.HPVTWI.HPVTWITest();
                    if (accessionOrder.PanelSetOrderCollection.Exists(panelSetHPVTWI.PanelSetId) == false)
                    {
                        this.m_ActionRequired = true;
                        this.m_Message.AppendLine("The order indicates that an " + panelSetHPVTWI.PanelSetName + " is required but not ordered.");
                    }
                }
            }
        }

        private void IsRequiredByStandingOrder(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
			YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTest womensHealthProfileTest = new YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTest();
			if (accessionOrder.PanelSetOrderCollection.Exists(womensHealthProfileTest.PanelSetId) == true)
            {
				YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder womensHealthProfileTestOrder = (YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(womensHealthProfileTest.PanelSetId);
				YellowstonePathology.Business.Client.Model.StandingOrder standingOrder = YellowstonePathology.Business.Client.Model.StandingOrderCollection.GetByStandingOrderCode(womensHealthProfileTestOrder.HPVStandingOrderCode);
                if (standingOrder.IsRequired(accessionOrder) == true)
                {
					YellowstonePathology.Business.Test.HPVTWI.HPVTWITest panelSetHPVTWI = new YellowstonePathology.Business.Test.HPVTWI.HPVTWITest();
                    if (accessionOrder.PanelSetOrderCollection.Exists(panelSetHPVTWI.PanelSetId) == false)
                    {
                        this.m_ActionRequired = true;
                        this.m_Message.AppendLine("An " + panelSetHPVTWI.PanelSetName + " is required but not ordered.");
                    }
                }
            }
        }

        private void IsRequiredByReflexOrder(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
			YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTest womensHealthProfileTest = new YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTest();
			if (accessionOrder.PanelSetOrderCollection.Exists(womensHealthProfileTest.PanelSetId) == true)
            {
				YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder womensHealthProfileTestOrder = (YellowstonePathology.Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(womensHealthProfileTest.PanelSetId);
				YellowstonePathology.Business.Client.Model.ReflexOrder reflexOrder = YellowstonePathology.Business.Client.Model.ReflexOrderCollection.GetByReflexByOrderCode(womensHealthProfileTestOrder.HPVReflexOrderCode);

                if (reflexOrder.IsRequired(accessionOrder) == true)
                {
					YellowstonePathology.Business.Test.HPVTWI.HPVTWITest panelSetHPVTWI = new YellowstonePathology.Business.Test.HPVTWI.HPVTWITest();
                    if (accessionOrder.PanelSetOrderCollection.Exists(panelSetHPVTWI.PanelSetId) == false)
                    {
                        this.m_ActionRequired = true;
                        this.m_Message.AppendLine("An " + panelSetHPVTWI.PanelSetName + " is required but not ordered.");
                    }
                }
            }
        }
    }
}
