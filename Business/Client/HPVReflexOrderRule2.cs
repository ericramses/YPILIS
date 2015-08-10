using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPVReflexOrderRule2 : ReflexOrder
    {
        int m_PapResult;

        public HPVReflexOrderRule2()
        {
            this.m_RuleNumber = 2;
            this.m_ReflexOrderCode = "RFLXHPVRL2";
            this.m_Description = "Perform reflex HPV testing on patients who are reported with ASCUS results";
            this.m_PanelSet = new YellowstonePathology.Business.Test.HPVTWI.PanelSetHPVTWI();
        }

        public override bool IsRequired(Business.Test.AccessionOrder accessionOrder)
        {
            bool result = false;
			YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapTest panelSetThinPrep = new YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapTest();
            if (accessionOrder.PanelSetOrderCollection.Exists(panelSetThinPrep.PanelSetId) == true)
            {
                YellowstonePathology.Business.Test.PanelSetOrderCytology panelSetOrderCytology = (YellowstonePathology.Business.Test.PanelSetOrderCytology)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetThinPrep.PanelSetId);
                if (panelSetOrderCytology.Final == true)
                {
					if (YellowstonePathology.Business.Cytology.Model.CytologyResultCode.IsDiagnosisASCUS(panelSetOrderCytology.ResultCode) == true)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }              
    }
}
