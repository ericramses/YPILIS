using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.WomensHealthProfile
{
    public class WomensHealthProfileResult
    {
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;

		private YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapTest m_PanelSetThinPrepPap;
        private YellowstonePathology.Business.Test.HPVTWI.PanelSetHPVTWI m_PanelSetHPVTWI;

        private bool m_HasThinPrepPap;
        private bool m_HasHighRiskHPV;
        private StringBuilder m_Method;
        private StringBuilder m_References;

		public WomensHealthProfileResult(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_References = new StringBuilder();
            this.m_Method = new StringBuilder();

            this.m_AccessionOrder = accessionOrder;
			this.m_PanelSetThinPrepPap = new YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapTest();
            this.m_PanelSetHPVTWI = new Business.Test.HPVTWI.PanelSetHPVTWI();

            this.m_HasThinPrepPap = this.m_AccessionOrder.PanelSetOrderCollection.Exists(this.m_PanelSetThinPrepPap.PanelSetId);
            this.m_HasHighRiskHPV = this.m_AccessionOrder.PanelSetOrderCollection.Exists(this.m_PanelSetHPVTWI.PanelSetId);

            this.SetData();
        }

        private void SetData()
        {   
            if(this.m_HasThinPrepPap == true)
            {
                YellowstonePathology.Business.Test.PanelSetOrderCytology panelSetOrderCytology = (YellowstonePathology.Business.Test.PanelSetOrderCytology)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_PanelSetThinPrepPap.PanelSetId);
                this.m_Method.Append("Thin Prep Pap: " + panelSetOrderCytology.Method);                
                this.m_References.Append("Thin Prep Pap: " + panelSetOrderCytology.References);
            }

            if (this.m_HasHighRiskHPV == true)
            {
                YellowstonePathology.Business.Test.HPVTWI.PanelSetOrderHPVTWI panelSetOrderHPVTWI = (YellowstonePathology.Business.Test.HPVTWI.PanelSetOrderHPVTWI)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_PanelSetHPVTWI.PanelSetId);
                if (this.m_HasThinPrepPap == true)
                {
                    this.m_Method.AppendLine();
                    this.m_Method.AppendLine();

                    this.m_References.AppendLine();
                    this.m_References.AppendLine();
                }
                this.m_Method.AppendLine("High Risk HPV: " + panelSetOrderHPVTWI.TestInformation);
                this.m_References.AppendLine("High Risk HPV: " + panelSetOrderHPVTWI.References);
            }            
        }       

        public string Method
        {
            get { return this.m_Method.ToString(); }
        }

        public string References
        {
            get { return this.m_References.ToString(); }
        }
    }
}
