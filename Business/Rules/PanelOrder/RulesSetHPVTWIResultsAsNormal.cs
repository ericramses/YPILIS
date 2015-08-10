using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Rules.PanelOrder
{
    public class RulesSetHPVTWIResultsAsNormal : BaseRules
	{
        private static RulesSetHPVTWIResultsAsNormal m_Instance;

		private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrderItem;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		YellowstonePathology.Business.Test.PanelOrder m_PanelOrderItemHPVTWI;

        private RulesSetHPVTWIResultsAsNormal() 
            : base(typeof(RulesSetHPVTWIResultsAsNormal))
        {
               
        }

		public YellowstonePathology.Business.Test.PanelOrder PanelOrderItemHPVTWI
        {
            get { return this.m_PanelOrderItemHPVTWI; }
            set { this.m_PanelOrderItemHPVTWI = value; }
        }

		public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
		{
			get { return this.m_AccessionOrder; }
			set { this.m_AccessionOrder = value; }
		}

		private void SetPanelSetOrderItem()
		{
			this.m_PanelSetOrderItem = this.m_AccessionOrder.PanelSetOrderCollection.GetCurrent(this.m_PanelOrderItemHPVTWI.ReportNo);
			this.PanelOrderItemHPVTWI = this.m_PanelSetOrderItem.PanelOrderCollection.GetByPanelOrderId(this.PanelOrderItemHPVTWI.PanelOrderId);
		}

		public YellowstonePathology.Domain.Test.Model.TestOrder A5A6Test
        {
			get { return ((YellowstonePathology.Domain.Test.Model.TestOrderCollection)this.PanelOrderItemHPVTWI.TestOrderCollection).GetTestOrder(184); }
        }

		public YellowstonePathology.Domain.Test.Model.TestOrder A7Test
        {
			get { return ((YellowstonePathology.Domain.Test.Model.TestOrderCollection)this.PanelOrderItemHPVTWI.TestOrderCollection).GetTestOrder(185); }
        }

		public YellowstonePathology.Domain.Test.Model.TestOrder A9Test
        {
			get { return ((YellowstonePathology.Domain.Test.Model.TestOrderCollection)this.PanelOrderItemHPVTWI.TestOrderCollection).GetTestOrder(186); }
        }

        public void SetResultsToNormal()
        {
            this.A5A6Test.Result = "Negative";
            this.A7Test.Result = "Negative";
            this.A9Test.Result = "Negative";
        }

        public static RulesSetHPVTWIResultsAsNormal Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new RulesSetHPVTWIResultsAsNormal();
                }
                return m_Instance;
            }
        }
	}
}
