using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Rules.PanelOrder
{
    class RulesAcceptCFResults : BaseRules
	{
        private static RulesAcceptCFResults m_Instance;
		YellowstonePathology.Business.Test.PanelOrder m_PanelOrderItemCF;
		private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrderItem;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Core.User.SystemUser m_OrderingUser;
		private const string m_PositiveResultBase = "XXX detected in 1 allele.  Patient is a carrier of a Cystic Fibrosis causing mutation. (see Comment).";
		private string m_PositiveResult;
		private string m_PositiveInterpretation;

		YellowstonePathology.Domain.CommentList m_CommentList;

        private RulesAcceptCFResults() 
            : base(typeof(RulesAcceptCFResults))
        {
			this.m_PermissionList.Add(YellowstonePathology.Core.User.SystemUserRoleDescriptionEnum.MolecularCaseFinal);
			this.m_PermissionList.Add(YellowstonePathology.Core.User.SystemUserRoleDescriptionEnum.MolecularCaseTech);
			this.m_PermissionList.Add(YellowstonePathology.Core.User.SystemUserRoleDescriptionEnum.Administrator);
			this.m_PermissionList.Add(YellowstonePathology.Core.User.SystemUserRoleDescriptionEnum.Pathologist);
        }

		public YellowstonePathology.Core.User.SystemUser OrderingUser
		{
			get { return this.m_OrderingUser; }
			set { this.m_OrderingUser = value; }
		}

        public string PositiveResult
        {
            //get { return "F508 mutation detected in 1 allele.  Patient is a carrier of a Cystic Fibrosis causing mutation. (see Comment)."; }
			get { return this.m_PositiveResult; }
		}

        public string NegativeResult
        {
            get { return "No Cystic Fibrosis causing gene mutations detected (see Comment)."; }
        }

		public string PositiveInterpretation
		{
			get
			{
				if (this.m_CommentList == null)
				{
					this.SetCommentList();
				}
				return m_CommentList.GetCommentListItemByCommentId(10).Comment;
			}
		}

		public string NegativeInterpretation
		{
			get
			{
				if (this.m_CommentList == null)
				{
					this.SetCommentList();
				}
				return m_CommentList.GetCommentListItemByCommentId(9).Comment;
			}
		}

        public void SetPositiveResult()
        {
			this.m_PanelSetOrderItem.PanelSetResultOrderCollection[0].Result = PositiveResult;
        }

        public void SetNegativeResult()
        {
			this.m_PanelSetOrderItem.PanelSetResultOrderCollection[0].Result = NegativeResult;
        }

		public void SetPositiveInterpretation()
		{
			Test.PanelSetOrderComment panelSetOrderComment = this.m_PanelSetOrderItem.PanelSetOrderCommentCollection.GetPanelSetOrderComment("report_interpretation");
			panelSetOrderComment.Comment = this.PositiveInterpretation;
		}

		public void SetNegativeInterpretation()
		{
			Test.PanelSetOrderComment panelSetOrderComment = this.m_PanelSetOrderItem.PanelSetOrderCommentCollection.GetPanelSetOrderComment("report_interpretation");
			panelSetOrderComment.Comment = this.NegativeInterpretation;
		}

        public bool ControlFailedResult()
        {
            bool result = false;

            return result;
        }

        public bool AreAllResultsSet()
        {
            bool result = true;
			foreach (YellowstonePathology.Domain.Test.Model.TestOrder testOrder in this.PanelOrderItemCF.TestOrderCollection)
            {
                if (string.IsNullOrEmpty(testOrder.Result) == true)
                {
                    result = false;
                    break;
                }
            }
            return result;
        }   

        public bool HasPositiveResult()
        {
            bool result = false;
			foreach (YellowstonePathology.Domain.Test.Model.TestOrder testOrder in this.PanelOrderItemCF.TestOrderCollection)
            {
                if (testOrder.Result == "Carrier")
                {
                    result = true;
                    break;
                }
            }
            return result;
        }        

        public void SetAccepted()
        {
			this.PanelOrderItemCF = this.AccessionOrder.CurrentPanelSetOrder.PanelOrderCollection.GetByPanelOrderId(this.PanelOrderItemCF.PanelOrderId);
			this.PanelOrderItemCF.Accepted = true;
            this.PanelOrderItemCF.AcceptedDate = DateTime.Today;
            this.PanelOrderItemCF.AcceptedTime = DateTime.Now;
			this.PanelOrderItemCF.AcceptedById = this.OrderingUser.UserId;
		}

        public static RulesAcceptCFResults Instance
        {
            get
            {
                if (m_Instance == null)
                {
                    m_Instance = new RulesAcceptCFResults();
                }
                return m_Instance;
            }
        }

		public YellowstonePathology.Business.Test.PanelOrder PanelOrderItemCF
        {
            get { return this.m_PanelOrderItemCF; }
            set { this.m_PanelOrderItemCF = value; }
        }

		public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
		{
			get { return this.m_AccessionOrder; }
			set { this.m_AccessionOrder = value; }
		}

		private void SetPanelSetOrderItem()
		{
			this.m_PanelSetOrderItem = this.m_AccessionOrder.PanelSetOrderCollection.GetCurrent(this.m_PanelOrderItemCF.ReportNo);
		}

		public void SetMutationsFound()
		{
			this.m_PositiveResult = string.Empty;
			if (this.m_CommentList == null)
			{
				this.SetCommentList();
			}
			this.m_PositiveInterpretation =  m_CommentList.GetCommentListItemByCommentId(10).Comment;

			StringBuilder mutations = new StringBuilder();
			bool haveFirst = false;
			bool haveMany = false;
			foreach (YellowstonePathology.Domain.Test.Model.TestOrder testOrder in this.PanelOrderItemCF.TestOrderCollection)
			{
				if (testOrder.Result == "Carrier")
				{
					if (haveFirst)
					{
						mutations.Append(", " + testOrder.TestName);
						haveMany = true;
					}
					else
					{
						mutations.Append(testOrder.TestName);
						haveFirst = true;
					}
				}
			}

			if (haveMany)
			{
				mutations.Append(" mutations were");
			}
			else
			{
				mutations.Append(" mutation was");
			}

			this.m_PositiveResult = m_PositiveResultBase.Replace("XXX", mutations.ToString());
			this.m_PositiveInterpretation = this.m_PositiveInterpretation.Replace("XXX", mutations.ToString());
		}

		private void SetCommentList()
		{
			m_CommentList = Gateway.LocalDataGateway.GetCommentList();
		}

		private void Save()
		{
		}
	}
}
