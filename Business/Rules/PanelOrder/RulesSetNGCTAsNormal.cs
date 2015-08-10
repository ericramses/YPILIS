using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Rules.PanelOrder
{
	public class RulesSetNGCTAsNormal : BaseRules
	{
        private static RulesSetNGCTAsNormal m_Instance;
		private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrderItem;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		YellowstonePathology.Business.Test.PanelOrder m_PanelOrderItemNGCT;

        private RulesSetNGCTAsNormal() 
            : base(typeof(RulesSetNGCTAsNormal))
        {
               
        }

		public YellowstonePathology.Business.Test.PanelOrder PanelOrderItemNGCT
        {
            get { return this.m_PanelOrderItemNGCT; }
            set { this.m_PanelOrderItemNGCT = value; }
        }

		public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
		{
			get { return this.m_AccessionOrder; }
			set { this.m_AccessionOrder = value; }
		}

		private void SetPanelSetOrderItem()
		{
			this.m_PanelSetOrderItem = this.m_AccessionOrder.PanelSetOrderCollection.GetCurrent(this.m_PanelOrderItemNGCT.ReportNo);
			this.PanelOrderItemNGCT = this.m_PanelSetOrderItem.PanelOrderCollection.GetByPanelOrderId(this.PanelOrderItemNGCT.PanelOrderId);
		}

		public YellowstonePathology.Domain.Test.Model.TestOrder ERV3Test
        {
			get { return ((YellowstonePathology.Domain.Test.Model.TestOrderCollection)this.PanelOrderItemNGCT.TestOrderCollection).GetTestOrder(29); }
        }

		public YellowstonePathology.Domain.Test.Model.TestOrder NGTest
        {
			get { return ((YellowstonePathology.Domain.Test.Model.TestOrderCollection)this.PanelOrderItemNGCT.TestOrderCollection).GetTestOrder(25); }
        }

		public YellowstonePathology.Domain.Test.Model.TestOrder CTTest
        {
			get { return ((YellowstonePathology.Domain.Test.Model.TestOrderCollection)this.PanelOrderItemNGCT.TestOrderCollection).GetTestOrder(26); }
        }

        public static RulesSetNGCTAsNormal Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new RulesSetNGCTAsNormal();
                }
                return m_Instance;
            }
        }
	}
}
