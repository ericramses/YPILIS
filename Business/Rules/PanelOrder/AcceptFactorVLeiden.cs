using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Rules.PanelOrder
{
	public class AcceptFactorVLeiden : Accept
	{
		int m_IntrepretationId;
		YellowstonePathology.Domain.Test.Model.TestOrder m_ResultTestOrder;

		public AcceptFactorVLeiden()
		{
			m_IntrepretationId = 0;

			this.m_Rule.ActionList.Add(this.AcceptPanel);
			this.m_Rule.ActionList.Add(this.GetResultTestOrder);
			this.m_Rule.ActionList.Add(this.SetResult);
			this.m_Rule.ActionList.Add(this.SetReportComment);
			this.m_Rule.ActionList.Add(this.SetInterpretation);
			this.m_Rule.ActionList.Add(this.SetIndication);
			this.m_Rule.ActionList.Add(this.Save);
		}

		private void GetResultTestOrder()
		{
			this.m_ResultTestOrder = YellowstonePathology.Business.Helper.PanelOrderHelper.GetTestOrderByTestId(this.m_PanelOrderBeingAccepted.TestOrderCollection, 242);
		}

		private void SetResult()
		{
			if (m_ResultTestOrder.Result == "Heterozygous Mutation Detected")
			{
				m_IntrepretationId = 60;
			}
			if (m_ResultTestOrder.Result == "Homozygous Mutation Detected")
			{
				m_IntrepretationId = 63;
			}
			if (m_ResultTestOrder.Result == "Mutation Not Detected")
			{
				m_IntrepretationId = 59;
			}
			if (m_ResultTestOrder.Result == "Indeterminate")
			{
				m_IntrepretationId = 62;
			}
			if (m_ResultTestOrder.Result == "NO RESULT – Insufficient DNA for analysis")
			{
				m_IntrepretationId = 61;
			}

			this.m_PanelSetOrder.PanelSetResultOrderCollection[0].Result = m_ResultTestOrder.Result;
		}

		private void SetReportComment()
		{
			YellowstonePathology.Business.Test.PanelSetOrderComment panelSetOrderComment = YellowstonePathology.Business.Helper.PanelOrderHelper.GetPanelSetOrderCommentByCommentName(this.m_PanelSetOrder.PanelSetOrderCommentCollection, "Report Comment");
			if (m_IntrepretationId == 62)
			{
				if (string.IsNullOrEmpty(panelSetOrderComment.Comment))
				{
					panelSetOrderComment.Comment = "???";
				}
			}
		}

		private void SetInterpretation()
		{
			YellowstonePathology.Business.Test.PanelSetOrderComment panelSetOrderComment = YellowstonePathology.Business.Helper.PanelOrderHelper.GetPanelSetOrderCommentByCommentName(this.m_PanelSetOrder.PanelSetOrderCommentCollection, "Interpretation");
			if (m_IntrepretationId > 0)
			{
				YellowstonePathology.Domain.CommentListItem comment = Gateway.LocalDataGateway.GetCommentListItemById(m_IntrepretationId);
				panelSetOrderComment.Comment = comment.Comment;

			}
		}

		private void SetIndication()
		{
			YellowstonePathology.Business.Test.PanelSetOrderComment panelSetOrderComment = YellowstonePathology.Business.Helper.PanelOrderHelper.GetPanelSetOrderCommentByCommentName(this.m_PanelSetOrder.PanelSetOrderCommentCollection, "Report Indication");
			if (string.IsNullOrEmpty(panelSetOrderComment.Comment))
			{
				panelSetOrderComment.Comment = "???";
			}
		}
	}
}
