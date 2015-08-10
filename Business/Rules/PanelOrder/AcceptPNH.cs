using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Rules.PanelOrder
{
	public class AcceptPNH : Accept
	{
		int m_CommentId;
		protected PNHComplexResult m_PNHComplexResult;
		YellowstonePathology.Domain.Test.Model.Test m_PanelSetResultTest;
		List<YellowstonePathology.Business.Test.AccessionOrder> m_AccessionOrders;

		public AcceptPNH()
        {
			m_CommentId = 0;
			m_PanelSetResultTest = this.m_TestCollection.GetTest(198);
			m_PNHComplexResult = new PNHComplexResult();

			this.m_Rule.ActionList.Add(this.AcceptPanel);
			this.m_Rule.ActionList.Add(this.SetTotals);
			this.m_Rule.ActionList.Add(this.SetNegativeResult);
			this.m_Rule.ActionList.Add(this.SetSmallPositiveResult);
			this.m_Rule.ActionList.Add(this.SetSignificantPositiveResult);
			this.m_Rule.ActionList.Add(this.SetGpiDeficientResult);
			this.m_Rule.ActionList.Add(this.GetPreviousAccessions);
			this.m_Rule.ActionList.Add(this.SetPersistentResult);
			this.m_Rule.ActionList.Add(this.SetNegativeWithPreviousPositiveResult);
			this.m_Rule.ActionList.Add(this.SetReportComment);
			this.m_Rule.ActionList.Add(this.Save);
		}

		private void SetTotals()
		{
			this.m_PNHComplexResult.SetTotals(this.m_PanelOrderBeingAccepted);
		}

		private void SetNegativeResult()
		{
			if (this.m_PNHComplexResult.IsNegativeResult)
			{
				// no comment
				this.m_PanelSetOrder.PanelSetResultOrderCollection[0].Result = YellowstonePathology.Business.Helper.PanelOrderHelper.GetTestResultByResultId(m_PanelSetResultTest, 27).Result;
				this.m_PanelSetOrder.TemplateId = 17;
			}
		}

		private void SetSmallPositiveResult()
		{
			if (this.m_PNHComplexResult.IsSmallPositiveResult)
			{
				m_CommentId = 56;
				this.m_PanelSetOrder.PanelSetResultOrderCollection[0].Result = YellowstonePathology.Business.Helper.PanelOrderHelper.GetTestResultByResultId(m_PanelSetResultTest, 28).Result;
				this.m_PanelSetOrder.TemplateId = 18;
			}
		}

		private void SetSignificantPositiveResult()
		{
			if (this.m_PNHComplexResult.IsSignificantPositiveResult)
			{
				m_CommentId = 57;
				this.m_PanelSetOrder.PanelSetResultOrderCollection[0].Result = YellowstonePathology.Business.Helper.PanelOrderHelper.GetTestResultByResultId(m_PanelSetResultTest, 29).Result;
				this.m_PanelSetOrder.TemplateId = 18;
			}
		}

		private void SetGpiDeficientResult()
		{
			if (this.m_PNHComplexResult.IsGpiDeficientResult)
			{
				m_CommentId = 55;
				this.m_PanelSetOrder.PanelSetResultOrderCollection[0].Result = YellowstonePathology.Business.Helper.PanelOrderHelper.GetTestResultByResultId(m_PanelSetResultTest, 50).Result;
				this.m_PanelSetOrder.TemplateId = 18;
			}
		}

		private void GetPreviousAccessions()
		{
			m_AccessionOrders = this.m_PNHComplexResult.GetPreviousAccessions(this.m_AccessionOrder.PatientId);
		}

		private void SetPersistentResult()
		{
			if (this.m_PNHComplexResult.IsPersistentResult(this.m_AccessionOrders, this.m_AccessionOrder.MasterAccessionNo, this.m_PanelSetOrder.OrderDate.Value))
			{
				//if have previous and previous is small positive or previous is significant positive then result will be Persistent PNH clone identified
				// use current and the three lastest results
				m_CommentId = 58;
				this.m_PanelSetOrder.PanelSetResultOrderCollection[0].Result = YellowstonePathology.Business.Helper.PanelOrderHelper.GetTestResultByResultId(m_PanelSetResultTest, 51).Result;
			}
		}

		private void SetNegativeWithPreviousPositiveResult()
		{
			if (this.m_PNHComplexResult.IsNegativeWithPreviousPositiveResult(this.m_AccessionOrders, this.m_AccessionOrder.MasterAccessionNo, this.m_PanelSetOrder.OrderDate.Value))
			{
				m_CommentId = 69;
				this.m_PanelSetOrder.PanelSetResultOrderCollection[0].Result = YellowstonePathology.Business.Helper.PanelOrderHelper.GetTestResultByResultId(m_PanelSetResultTest, 56).Result;
				this.m_PanelSetOrder.TemplateId = 18;
			}
		}

		private void SetReportComment()
		{
			YellowstonePathology.Business.Test.PanelSetOrderComment panelSetOrderComment = YellowstonePathology.Business.Helper.PanelOrderHelper.GetPanelSetOrderCommentByCommentName(this.m_PanelSetOrder.PanelSetOrderCommentCollection, "Report Comment");
			panelSetOrderComment.Comment = string.Empty;

			if(m_CommentId > 0)
			{
				YellowstonePathology.Domain.CommentListItem comment = Gateway.LocalDataGateway.GetCommentListItemById(m_CommentId);
				string result = comment.Comment;
				result = result.Replace("GRANULOCYTETOTAL", this.m_PNHComplexResult.GranulocytesTotal.ToString("F"));
				result = result.Replace("MONOCYTETOTAL", this.m_PNHComplexResult.MonocytesTotal.ToString("F"));
				result = result.Replace("REDBLOODTOTAL", this.m_PNHComplexResult.RedBloodTotal.ToString("F"));
				panelSetOrderComment.Comment = result;
			}
		}

		private void SetSpecimenComment()
		{
			YellowstonePathology.Business.Test.PanelSetOrderComment panelSetOrderComment = YellowstonePathology.Business.Helper.PanelOrderHelper.GetPanelSetOrderCommentByCommentName(this.m_PanelSetOrder.PanelSetOrderCommentCollection, "Result Comment");
			panelSetOrderComment.Comment = "Whole Blood";
		}
	}
}
