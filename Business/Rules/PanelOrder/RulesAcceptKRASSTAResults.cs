using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Rules.PanelOrder
{
	public class RulesAcceptKRASSTAResults : Accept
	{
		private YellowstonePathology.Domain.CommentList m_CommentList;

		public RulesAcceptKRASSTAResults()
		{
			this.m_Rule.ActionList.Add(SetResult);
			this.m_Rule.ActionList.Add(SetKrasResultDetail);
			this.m_Rule.ActionList.Add(SetResultComment);
			this.m_Rule.ActionList.Add(SetReportInterpretation);
			this.m_Rule.ActionList.Add(SetReportMethod);
			this.m_Rule.ActionList.Add(SetReportIndication);
			this.m_Rule.ActionList.Add(AcceptPanel);
			this.m_Rule.ActionList.Add(Save);

			m_CommentList = Gateway.LocalDataGateway.GetCommentList();
		}

		protected override void DoesUserHavePermission()
		{
			if (!this.m_AcceptingUser.IsUserInRole(Core.User.SystemUserRoleDescriptionEnum.Pathologist) &&
				!this.m_AcceptingUser.IsUserInRole(Core.User.SystemUserRoleDescriptionEnum.Administrator) &&
				!this.m_AcceptingUser.IsUserInRole(Core.User.SystemUserRoleDescriptionEnum.MolecularCaseTech) &&
				!this.m_AcceptingUser.IsUserInRole(Core.User.SystemUserRoleDescriptionEnum.MolecularCaseFinal))
			{
				this.m_ExecutionStatus.AddMessage("It appears you do not have permission to accept the results for this case type.", true);
			}
		}

		protected void SetResult()
		{
			string result = m_PanelOrderBeingAccepted.TestOrderCollection[0].Result;
			YellowstonePathology.Business.Test.PanelSetResultOrder panelSetResultOrder = m_PanelSetOrder.PanelSetResultOrderCollection.GetPanelSetResultOrder(212);
			if (!this.ResultIsPositive())
			{
				panelSetResultOrder.Result = result;
			}
			else
			{
				panelSetResultOrder.Result = "KRAS mutation DETECTED.";
			}
		}

		protected virtual void SetKrasResultDetail()
		{
			if (this.ResultIsPositive())
			{
				string result = m_PanelOrderBeingAccepted.TestOrderCollection[0].Result;
				this.m_PanelSetOrder.PanelSetOrderCommentCollection.GetPanelSetOrderComment("kras_result_detail").Comment = "Mutation " + result.Remove(0, 24) + ".";
			}
		}

		protected virtual void SetResultComment()
		{
			if (this.ResultIsPositive() == false)
			{
                this.m_PanelSetOrder.PanelSetOrderCommentCollection.GetPanelSetOrderComment("result_comment").Comment = m_CommentList.GetCommentListItemByCommentId(6).Comment;                
			}			
		}

		protected virtual void SetReportInterpretation()
		{
			if (this.ResultIsPositive())
			{
				this.m_PanelSetOrder.PanelSetOrderCommentCollection.GetPanelSetOrderComment("report_interpretation").Comment = m_CommentList.GetCommentListItemByCommentId(32).Comment;
			}
			else
			{
				this.m_PanelSetOrder.PanelSetOrderCommentCollection.GetPanelSetOrderComment("report_interpretation").Comment = m_CommentList.GetCommentListItemByCommentId(34).Comment;
			}
		}

		protected virtual void SetReportMethod()
		{
			this.m_PanelSetOrder.PanelSetOrderCommentCollection.GetPanelSetOrderComment("report_method").Comment = m_CommentList.GetCommentListItemByCommentId(95).Comment;
		}

		protected virtual void SetReportIndication()
		{
			this.m_PanelSetOrder.PanelSetOrderCommentCollection.GetPanelSetOrderComment("report_indication").Comment = m_CommentList.GetCommentListItemByCommentId(54).Comment;
		}

		protected bool ResultIsPositive()
		{
			bool isPositive = false;
			string result = this.m_PanelOrderBeingAccepted.TestOrderCollection[0].Result;
			isPositive = false;
			if (result.ToUpper().Contains("NOT DETECTED"))
			{
				isPositive = false;
			}

			if (result.ToUpper().Contains("MUTATION DETECTED"))
			{
				isPositive = true;
			}
			return isPositive;
		}
	}
}
