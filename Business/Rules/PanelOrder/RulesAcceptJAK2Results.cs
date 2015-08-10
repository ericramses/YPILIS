using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Rules.PanelOrder
{
	class RulesAcceptJAK2Results : BaseRules
	{
		private static RulesAcceptJAK2Results m_Instance;
		private YellowstonePathology.Core.User.SystemUser m_OrderingUser;
		private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrderItem;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.Test.PanelOrder m_PanelOrderItemJAK2;

		private RulesAcceptJAK2Results()
			: base(typeof(RulesAcceptJAK2Results))
        {
			this.m_PermissionList.Add(YellowstonePathology.Core.User.SystemUserRoleDescriptionEnum.MolecularCaseFinal);
			this.m_PermissionList.Add(YellowstonePathology.Core.User.SystemUserRoleDescriptionEnum.MolecularCaseTech);
			this.m_PermissionList.Add(YellowstonePathology.Core.User.SystemUserRoleDescriptionEnum.Administrator);
			this.m_PermissionList.Add(YellowstonePathology.Core.User.SystemUserRoleDescriptionEnum.Pathologist);
        }

		public static RulesAcceptJAK2Results Instance
        {
            get
            {                
                if (m_Instance == null)
                {
					m_Instance = new RulesAcceptJAK2Results();
                }
                return m_Instance;
            }
        }

		public YellowstonePathology.Business.Test.PanelOrder PanelOrderItemJAK2
        {
			get { return this.m_PanelOrderItemJAK2; }
			set { this.m_PanelOrderItemJAK2 = value; }
        }

		public YellowstonePathology.Core.User.SystemUser OrderingUser
		{
			get { return this.m_OrderingUser; }
			set { this.m_OrderingUser = value; }
		}

		public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
		{
			get { return this.m_AccessionOrder; }
			set { this.m_AccessionOrder = value; }
		}

		private void SetPanelSetOrderItem()
		{
			this.m_PanelSetOrderItem = this.m_AccessionOrder.PanelSetOrderCollection.GetCurrent(this.m_PanelOrderItemJAK2.ReportNo);
		}

		public bool PanelHasResult()
		{
			if (PanelOrderItemJAK2.TestOrderCollection[0].Result == string.Empty)
			{
				return false;
			}
			return true;
		}

        public void SetResults()
        {//WHC this needs work
			this.m_PanelSetOrderItem.TemplateId = 1;
			string result = PanelOrderItemJAK2.TestOrderCollection[0].Result;
			YellowstonePathology.Business.Test.PanelSetResultOrder panelSetResultOrder = this.m_PanelSetOrderItem.PanelSetResultOrderCollection[0];
			panelSetResultOrder.Result = result;
			YellowstonePathology.Business.Gateway.AccessionOrderGateway gateway = new Gateway.AccessionOrderGateway();
			gateway.SetTestComments(this.PanelOrderItemJAK2.ReportNo, result);
        }

		public void SetAccepted()
		{
			PanelOrderItemJAK2 = this.AccessionOrder.CurrentPanelSetOrder.PanelOrderCollection.GetByPanelOrderId(m_PanelOrderItemJAK2.PanelOrderId);
			PanelOrderItemJAK2.Accepted = true;
			PanelOrderItemJAK2.AcceptedById = this.OrderingUser.UserId;
			PanelOrderItemJAK2.AcceptedDate = DateTime.Today;
			PanelOrderItemJAK2.AcceptedTime = DateTime.Now;
		}
	}
}
