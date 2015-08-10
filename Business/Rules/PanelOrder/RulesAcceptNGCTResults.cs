using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Rules.PanelOrder
{
	public class RulesAcceptNGCTResults : BaseRules
	{
        private static RulesAcceptNGCTResults m_Instance;
		private YellowstonePathology.Business.Test.PanelOrder m_PanelOrderItemNGCT;
		private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrderItem;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Core.User.SystemUser m_OrderingUser;

        private int m_NGCTPanelCount;

        private RulesAcceptNGCTResults() 
            : base(typeof(RulesAcceptNGCTResults))
        {
			this.m_PermissionList.Add(YellowstonePathology.Core.User.SystemUserRoleDescriptionEnum.MolecularCaseFinal);
			this.m_PermissionList.Add(YellowstonePathology.Core.User.SystemUserRoleDescriptionEnum.MolecularCaseTech);
			this.m_PermissionList.Add(YellowstonePathology.Core.User.SystemUserRoleDescriptionEnum.Administrator);
			this.m_PermissionList.Add(YellowstonePathology.Core.User.SystemUserRoleDescriptionEnum.Pathologist);
        }                

        public static RulesAcceptNGCTResults Instance
        {
            get
            {                
                if (m_Instance == null)
                {
                    m_Instance = new RulesAcceptNGCTResults();
                }
                return m_Instance;
            }
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

		public YellowstonePathology.Core.User.SystemUser OrderingUser
		{
			get { return this.m_OrderingUser; }
			set { this.m_OrderingUser = value; }
		}

		public void SetPanelSetOrderItem()
		{
			this.m_AccessionOrder.SetCurrentPanelSetOrder(this.m_PanelOrderItemNGCT.ReportNo);
			this.m_PanelSetOrderItem = this.m_AccessionOrder.PanelSetOrderCollection.GetCurrent(this.m_PanelOrderItemNGCT.ReportNo);
		}

        public bool PanelIsFirstRun
        {
            get
            {
                bool result = false;                
                if (this.NGCTPanelCount == 1)
                {
                    result = true;
                }
                return result;
            }
        }

        public bool PanelIsSecondRun
        {
            get
            {
                bool result = false;
                if (this.NGCTPanelCount == 2)
                {                    
                    result = true;                    
                }
                return result;
            }
        }        

        private int NGCTPanelCount
        {
            get { return this.m_NGCTPanelCount; }
        }

        public void SetNGCTPanelCount()
        {
            int ngctPanelCount = 0;
            foreach (YellowstonePathology.Business.Test.PanelOrder panelOrder in this.m_PanelSetOrderItem.PanelOrderCollection)
            {
                switch (panelOrder.PanelId)
                {
                    case 3:
                    case 6:
                    case 7:
                    case 8:
                        ngctPanelCount += 1;
                        break;
                }
            }
            this.m_NGCTPanelCount = ngctPanelCount;
        }

		public YellowstonePathology.Domain.Test.Model.TestOrder CurrentERV3Result
        {
			get { return ((YellowstonePathology.Domain.Test.Model.TestOrderCollection)this.PanelOrderItemNGCT.TestOrderCollection).GetTestOrder(29); }
        }

		public YellowstonePathology.Domain.Test.Model.TestOrder CurrentERV3NGResult
        {
			get { return ((YellowstonePathology.Domain.Test.Model.TestOrderCollection)this.PanelOrderItemNGCT.TestOrderCollection).GetTestOrder(32); }
        }

		public YellowstonePathology.Domain.Test.Model.TestOrder CurrentERV3CTResult
        {
			get { return ((YellowstonePathology.Domain.Test.Model.TestOrderCollection)this.PanelOrderItemNGCT.TestOrderCollection).GetTestOrder(33); }
        }

		public YellowstonePathology.Domain.Test.Model.TestOrder CurrentNGResult
        {
			get { return ((YellowstonePathology.Domain.Test.Model.TestOrderCollection)this.PanelOrderItemNGCT.TestOrderCollection).GetTestOrder(25); }
        }

		public YellowstonePathology.Domain.Test.Model.TestOrder CurrentCTResult
        {
			get { return ((YellowstonePathology.Domain.Test.Model.TestOrderCollection)this.PanelOrderItemNGCT.TestOrderCollection).GetTestOrder(26); }
        }

        public void InsertConfirmatoryPanelOrder()
        {
			string reportNo = this.m_PanelOrderItemNGCT.ReportNo;
			YellowstonePathology.Business.Test.NGCT.NGCTConfirmatoryPanel confirmatoryPanel = (YellowstonePathology.Business.Test.NGCT.NGCTConfirmatoryPanel)this.m_PanelSetOrderItem.PanelOrderCollection.GetNew(reportNo, 6, "NG + ERV3 and CT + ERV3", this.m_OrderingUser.UserId);
            confirmatoryPanel.SetDefaults();
            confirmatoryPanel.AddDefaultChildren();			
			this.m_PanelSetOrderItem.PanelOrderCollection.Add(confirmatoryPanel);
		}

        public void InsertRepeatPanelOrder()
        {
			//string reportNo = this.m_PanelOrderItemNGCT.ReportNo;
			//int panelId = 3;
			//string aliquotOrderId = ((YellowstonePathology.Domain.Test.Model.TestOrder)this.PanelOrderItemNGCT.TestOrderCollection[0]).AliquotOrderItemList[0].AliquotOrderId;
            //YellowstonePathology.Business.Tools.ObjectTool.InsertPanelOrder(reportNo, panelId, aliquotOrderId, this.OrderingUser);
		}        

        public void SetResults()
        {
			this.m_PanelSetOrderItem.TemplateId = 2;
			foreach (YellowstonePathology.Business.Test.PanelSetResultOrder panelSetResultOrder in this.m_PanelSetOrderItem.PanelSetResultOrderCollection)
            {
                if (panelSetResultOrder.TestId == this.CurrentNGResult.TestId)
                {
                    panelSetResultOrder.Result = this.CurrentNGResult.Result;
                }

                if (panelSetResultOrder.TestId == this.CurrentCTResult.TestId)
                {
                    panelSetResultOrder.Result = this.CurrentCTResult.Result;
                }
            }
        }

        public void DisplayMessage(string message)
        {
            System.Windows.MessageBox.Show(message);
        }

        public bool ResultsAreConfirmed()
        {
			YellowstonePathology.Domain.Test.Model.TestOrder previousNGResult = this.m_PanelSetOrderItem.PanelOrderCollection[0].TestOrderCollection.GetTestOrder(25);
			YellowstonePathology.Domain.Test.Model.TestOrder previousCTResult = this.m_PanelSetOrderItem.PanelOrderCollection[0].TestOrderCollection.GetTestOrder(26);

            bool result = false;
            if (CurrentERV3NGResult.Result == "Positive" && CurrentERV3CTResult.Result == "Positive")
            {
                if (CurrentCTResult.Result == "Negative" && CurrentNGResult.Result == previousNGResult.Result)
                {
                    result = true;
                }
                if (CurrentNGResult.Result == "Negative" && CurrentCTResult.Result == previousCTResult.Result)
                {
                    result = true;
                }
                if (CurrentCTResult.Result == previousCTResult.Result && CurrentNGResult.Result == previousNGResult.Result)
                {
                    result = true;
                }                
                result = true;
            }
            return result;
        }

		public void SetAccepted()
		{
			this.PanelOrderItemNGCT = this.AccessionOrder.CurrentPanelSetOrder.PanelOrderCollection.GetByPanelOrderId(this.PanelOrderItemNGCT.PanelOrderId);
			this.PanelOrderItemNGCT.AcceptedDate = System.DateTime.Today;
			this.PanelOrderItemNGCT.AcceptedTime = System.DateTime.Now;
			this.PanelOrderItemNGCT.Accepted = true;
			this.PanelOrderItemNGCT.AcceptedById = this.OrderingUser.UserId;
		}		
	}
}
