using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPVReflexOrderRule141 : ReflexOrder
    {
        public HPVReflexOrderRule141()
        {
            this.m_RuleNumber = 141;
            this.m_ReflexOrderCode = "RFLXHPVRL141";
            this.m_Description = "Perform reflex HPV testing on patients who are reported with ASCUS.";
			this.m_PanelSet = new YellowstonePathology.Business.Test.HPV.HPVTest();
            this.m_PatientAge = HPVRuleValues.Any;
            this.m_PAPResult = HPVRuleValues.PAPResultASCUS;
            this.m_HPVResult = HPVRuleValues.NotUsed;
            this.m_HPVTesting = HPVRuleValues.Any;
        }

        public override bool IsRequired(Business.Test.AccessionOrder accessionOrder)
        {
            bool result = false;
			YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapTest panelSetThinPrep = new YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapTest();
            if (accessionOrder.PanelSetOrderCollection.Exists(panelSetThinPrep.PanelSetId) == true)
            {
				YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology panelSetOrderCytology = (YellowstonePathology.Business.Test.ThinPrepPap.PanelSetOrderCytology)accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSetThinPrep.PanelSetId);
                if (panelSetOrderCytology.Final == true)
                {
					if (YellowstonePathology.Business.Cytology.Model.CytologyResultCode.IsDiagnosisThreeOrBetter(panelSetOrderCytology.ResultCode) == true)
                    {                        
                        result = true;                        
                    }
                }
            }
            return result;
        }        
    }
}
