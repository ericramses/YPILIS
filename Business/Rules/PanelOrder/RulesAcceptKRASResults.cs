using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Rules.PanelOrder
{
    public class RulesAcceptKRASResults : BaseRules
	{
        private static RulesAcceptKRASResults m_Instance;
		private YellowstonePathology.Business.Test.PanelOrder m_PanelOrderItemKRAS;
		private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrderItem;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Core.User.SystemUser m_OrderingUser;

        private RulesAcceptKRASResults() 
            : base(typeof(RulesAcceptKRASResults))
        {
			this.m_PermissionList.Add(YellowstonePathology.Core.User.SystemUserRoleDescriptionEnum.MolecularCaseFinal);
			this.m_PermissionList.Add(YellowstonePathology.Core.User.SystemUserRoleDescriptionEnum.MolecularCaseTech);
			this.m_PermissionList.Add(YellowstonePathology.Core.User.SystemUserRoleDescriptionEnum.Administrator);
			this.m_PermissionList.Add(YellowstonePathology.Core.User.SystemUserRoleDescriptionEnum.Pathologist);
        }                

        public static RulesAcceptKRASResults Instance
        {
            get
            {                
                if (m_Instance == null)
                {
                    m_Instance = new RulesAcceptKRASResults();
                }
                return m_Instance;
            }
        }

		public YellowstonePathology.Core.User.SystemUser OrderingUser
		{
			get { return this.m_OrderingUser; }
			set { this.m_OrderingUser = value; }
		}

        public void SetResults()
        {
			//WHC this needs work
			this.m_PanelSetOrderItem.TemplateId = 3;
			string result = PanelOrderItemKRAS.TestOrderCollection[0].Result;
			YellowstonePathology.Business.Test.PanelSetResultOrder panelSetResultOrder = m_PanelSetOrderItem.PanelSetResultOrderCollection[0];
			panelSetResultOrder.Result = result;
			this.m_AccessionOrderGateway.SetTestComments(this.m_PanelSetOrderItem.ReportNo, result);
        }

		public void SetAccepted()
		{
			this.PanelOrderItemKRAS = this.AccessionOrder.CurrentPanelSetOrder.PanelOrderCollection.GetByPanelOrderId(this.PanelOrderItemKRAS.PanelOrderId);
			this.PanelOrderItemKRAS.Accepted = true;
			this.PanelOrderItemKRAS.AcceptedDate = System.DateTime.Today;
			this.PanelOrderItemKRAS.AcceptedTime = System.DateTime.Now;
		}

		public YellowstonePathology.Business.Test.PanelOrder PanelOrderItemKRAS
        {
            get { return this.m_PanelOrderItemKRAS; }
            set { this.m_PanelOrderItemKRAS = value; }
        }

		public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
		{
			get { return this.m_AccessionOrder; }
			set { this.m_AccessionOrder = value; }
		}

		public void SetPanelSetOrderItem()
		{
			this.m_PanelSetOrderItem = this.m_AccessionOrder.PanelSetOrderCollection.GetCurrent(this.PanelOrderItemKRAS.ReportNo);
		}

		public void Save()
		{
		}
	}
}
