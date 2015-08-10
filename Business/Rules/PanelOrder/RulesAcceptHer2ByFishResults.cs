using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Rules.PanelOrder
{
	class RulesAcceptHer2ByFishResults : BaseRules
	{
		private static RulesAcceptHer2ByFishResults m_Instance;
		private YellowstonePathology.Business.Test.PanelOrder m_PanelOrderItemHer2ByFish;
		private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrderItem;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Core.User.SystemUser m_OrderingUser;
		
		private RulesAcceptHer2ByFishResults()
			: base(typeof(RulesAcceptHer2ByFishResults))
        {
			this.m_PermissionList.Add(YellowstonePathology.Core.User.SystemUserRoleDescriptionEnum.MolecularCaseFinal);
			this.m_PermissionList.Add(YellowstonePathology.Core.User.SystemUserRoleDescriptionEnum.MolecularCaseTech);
			this.m_PermissionList.Add(YellowstonePathology.Core.User.SystemUserRoleDescriptionEnum.Administrator);
			this.m_PermissionList.Add(YellowstonePathology.Core.User.SystemUserRoleDescriptionEnum.Pathologist);
        }

		public static RulesAcceptHer2ByFishResults Instance
		{
			get
			{
				if (m_Instance == null)
				{
					m_Instance = new RulesAcceptHer2ByFishResults();
				}
				return m_Instance;
			}
		}

		public YellowstonePathology.Business.Test.PanelOrder PanelOrderItemHer2ByFish
		{
			get { return this.m_PanelOrderItemHer2ByFish; }
			set { this.m_PanelOrderItemHer2ByFish = value; }
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
			this.m_PanelSetOrderItem = this.m_AccessionOrder.PanelSetOrderCollection.GetCurrent(this.m_PanelOrderItemHer2ByFish.ReportNo);
		}

		public bool PanelHasResult()
		{
            bool result = false;
			YellowstonePathology.Business.MolecularTesting.Her2Result resultItem = (YellowstonePathology.Business.MolecularTesting.Her2Result)m_PanelSetOrderItem.CurrentPanelSetResultOrderItem.ComplexResult;
			if (string.IsNullOrEmpty(resultItem.Result) == false)
			{
				result = true;
			}
			return result;
		}

		public void SetResult()
		{
			double totalHer2SignalsCounted = (double)((YellowstonePathology.Business.MolecularTesting.Her2Result)m_PanelSetOrderItem.CurrentPanelSetResultOrderItem.ComplexResult).TotalHer2SignalsCounted;
			double totalChr17SignalsCounted = (double)((YellowstonePathology.Business.MolecularTesting.Her2Result)m_PanelSetOrderItem.CurrentPanelSetResultOrderItem.ComplexResult).TotalChr17SignalsCounted;
			double dratio = Math.Round(totalHer2SignalsCounted / totalChr17SignalsCounted, 2);

			if (dratio < 1.80)
			{
				((YellowstonePathology.Business.MolecularTesting.Her2Result)m_PanelSetOrderItem.CurrentPanelSetResultOrderItem.ComplexResult).Result = "NEGATIVE (Not amplified)";
			}
			else if (dratio > 2.20)
			{
				((YellowstonePathology.Business.MolecularTesting.Her2Result)m_PanelSetOrderItem.CurrentPanelSetResultOrderItem.ComplexResult).Result = "POSITIVE (Amplified)";
			}
			else
			{
				((YellowstonePathology.Business.MolecularTesting.Her2Result)m_PanelSetOrderItem.CurrentPanelSetResultOrderItem.ComplexResult).Result = "EQUIVOCAL";
			}

			m_PanelSetOrderItem.PanelSetResultOrderItemCollection[0].Result = "Complex Result";
		}

		public void SetAccepted()
		{
			PanelOrderItemHer2ByFish.Accepted = true;
			PanelOrderItemHer2ByFish.AcceptedById = OrderingUser.UserId;
			PanelOrderItemHer2ByFish.AcceptedDate = DateTime.Today;
			PanelOrderItemHer2ByFish.AcceptedTime = DateTime.Now;
		}
	}
}
