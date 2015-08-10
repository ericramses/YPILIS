using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Rules.PanelSetOrder
{
	public class RulesSignatureButtonText : BaseRules
	{
		private static Rules.PanelSetOrder.RulesSignatureButtonText m_Instance;
		private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrderItem;
		YellowstonePathology.Business.Domain.Lock m_Lock;
		YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;


		private RulesSignatureButtonText()
			: base(typeof(YellowstonePathology.Business.Rules.PanelSetOrder.RulesSignatureButtonText))
        { }

		public static RulesSignatureButtonText Instance
        {
            get
            {                
                if (m_Instance == null)
                {
					m_Instance = new Rules.PanelSetOrder.RulesSignatureButtonText();
                }
                return m_Instance;
            }
        }

		public YellowstonePathology.Business.Test.PanelSetOrder PanelSetOrderItem
        {
			get { return this.m_PanelSetOrderItem; }
			set { this.m_PanelSetOrderItem = value; }
        }

		public YellowstonePathology.Business.Domain.Lock Lock
		{
			get { return this.m_Lock; }
			set { this.m_Lock = value; }
		}		

        private bool CaseIsDistributed
        {
            get
            {
				return !this.PanelSetOrderItem.TestOrderReportDistributionCollection.HasUndistributedItems();                
            }
        }

        public void PathologistIdFromParent()
        {
            //if (this.m_PanelSetOrderItem.AssignedToId == 0)
            //{
            //	if (this.m_PanelSetOrderItem.Parent.PathologistId > 0)
            //	{
            //		this.m_PanelSetOrderItem.AssignedToId = this.m_PanelSetOrderItem.Parent.PathologistId;
            //	}
            //}
        }

		public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
		{
			set { this.m_AccessionOrder = value; }
		}
	}
}
