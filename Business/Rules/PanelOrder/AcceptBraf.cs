using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Rules.PanelOrder
{
	public class AcceptBraf : Accept
	{
		protected YellowstonePathology.Domain.CommentList m_CommentList;
		protected string m_Indicator;

		public AcceptBraf()
        {
			this.m_CommentList = Gateway.LocalDataGateway.GetCommentList();

			this.m_Rule.ActionList.Add(IndicatorIsSet);
			this.m_Rule.ActionList.Add(SetPositiveResult);
			this.m_Rule.ActionList.Add(SetNegativeResult);			
			this.m_Rule.ActionList.Add(SetResultCommentPapillaryThyroid);			
			this.m_Rule.ActionList.Add(SetReportIndicationCRC);
			this.m_Rule.ActionList.Add(SetReportIndicationPapillaryThyroid);
			this.m_Rule.ActionList.Add(SetReportIndicationMetastaticMelanoma);
			this.m_Rule.ActionList.Add(SetReportInterpretationCRC);
			this.m_Rule.ActionList.Add(SetReportInterpretationPapillaryThyroid);
			this.m_Rule.ActionList.Add(SetReportInterpretationMetastaticMelanoma);
			this.m_Rule.ActionList.Add(SetReportReferenceCRC);
			this.m_Rule.ActionList.Add(SetReportReferencePapillaryThyroid);
			this.m_Rule.ActionList.Add(SetReportReferenceMetastaticMelanoma);
			this.m_Rule.ActionList.Add(AcceptPanel);
			this.m_Rule.ActionList.Add(Save);
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

		protected void IndicatorIsSet()
		{
			YellowstonePathology.Business.Test.PanelSetResultOrder panelSetResultOrder = m_PanelSetOrder.PanelSetResultOrderCollection.GetPanelSetResultOrder(214);
			m_Indicator = panelSetResultOrder.Result;
			if (string.IsNullOrEmpty(m_Indicator))
			{
				this.m_ExecutionStatus.AddMessage("The Braf indicator is not set.", true);
			}
		}

		protected void SetPositiveResult()
		{
			if (this.ResultIsPositive())
			{
				this.m_PanelSetOrder.TemplateId = 6;
				string result = m_PanelOrderBeingAccepted.TestOrderCollection.GetTestOrder(193).Result;
				YellowstonePathology.Business.Test.PanelSetResultOrder panelSetResultOrder = m_PanelSetOrder.PanelSetResultOrderCollection.GetPanelSetResultOrder(193);
				panelSetResultOrder.Result = result;
			}
		}

		protected void SetNegativeResult()
		{
			if (this.ResultIsNegative())
			{
				this.m_PanelSetOrder.TemplateId = 6;
				string result = m_PanelOrderBeingAccepted.TestOrderCollection.GetTestOrder(193).Result;
				YellowstonePathology.Business.Test.PanelSetResultOrder panelSetResultOrder = this.m_PanelSetOrder.PanelSetResultOrderCollection.GetPanelSetResultOrder(193);
				panelSetResultOrder.Result = result;
			}
		}		

		protected virtual void SetResultCommentPapillaryThyroid()
		{
			if (this.PapillaryThyroidIndicated())
			{
				if (this.ResultIsPositive())
				{
					this.m_PanelSetOrder.PanelSetOrderCommentCollection.GetPanelSetOrderComment("result_comment").Comment = m_CommentList.GetCommentListItemByCommentId(16).Comment;
				}
				if (this.ResultIsNegative())
				{
					this.m_PanelSetOrder.PanelSetOrderCommentCollection.GetPanelSetOrderComment("result_comment").Comment = m_CommentList.GetCommentListItemByCommentId(15).Comment;
				}
			}
		}		

		protected virtual void SetReportIndicationCRC()
		{
			if (this.CRCIndicated())
			{
				this.m_PanelSetOrder.PanelSetOrderCommentCollection.GetPanelSetOrderComment("report_indication").Comment = m_CommentList.GetCommentListItemByCommentId(52).Comment;
			}
		}

		protected virtual void SetReportIndicationPapillaryThyroid()
		{
			if (this.PapillaryThyroidIndicated())
			{
				if (this.ResultIsPositive())
				{
					this.m_PanelSetOrder.PanelSetOrderCommentCollection.GetPanelSetOrderComment("report_indication").Comment = m_CommentList.GetCommentListItemByCommentId(51).Comment;
				}
				else if (this.ResultIsNegative())
				{
					this.m_PanelSetOrder.PanelSetOrderCommentCollection.GetPanelSetOrderComment("report_indication").Comment = m_CommentList.GetCommentListItemByCommentId(50).Comment;
				}
			}
		}

		protected virtual void SetReportIndicationMetastaticMelanoma()
		{
			if (this.MetastaticMelanomaIndicated())
			{
				this.m_PanelSetOrder.PanelSetOrderCommentCollection.GetPanelSetOrderComment("report_indication").Comment = m_CommentList.GetCommentListItemByCommentId(91).Comment;
			}
		}

		protected virtual void SetReportInterpretationCRC()
		{
			if (this.CRCIndicated())
			{
				if (this.ResultIsPositive())
				{
					this.m_PanelSetOrder.PanelSetOrderCommentCollection.GetPanelSetOrderComment("report_interpretation").Comment = m_CommentList.GetCommentListItemByCommentId(47).Comment;
				}
				else if (this.ResultIsNegative())
				{
					this.m_PanelSetOrder.PanelSetOrderCommentCollection.GetPanelSetOrderComment("report_interpretation").Comment = m_CommentList.GetCommentListItemByCommentId(28).Comment;
				}
			}
		}

		protected virtual void SetReportInterpretationPapillaryThyroid()
		{
			if (this.PapillaryThyroidIndicated())
			{
				if (this.ResultIsPositive())
				{
					this.m_PanelSetOrder.PanelSetOrderCommentCollection.GetPanelSetOrderComment("report_interpretation").Comment = m_CommentList.GetCommentListItemByCommentId(14).Comment;
				}
				else if (this.ResultIsNegative())
				{
					this.m_PanelSetOrder.PanelSetOrderCommentCollection.GetPanelSetOrderComment("report_interpretation").Comment = m_CommentList.GetCommentListItemByCommentId(13).Comment;
				}
			}
		}

		protected virtual void SetReportInterpretationMetastaticMelanoma()
		{
			if (this.MetastaticMelanomaIndicated())
			{
				if (this.ResultIsPositive())
				{
					this.m_PanelSetOrder.PanelSetOrderCommentCollection.GetPanelSetOrderComment("report_interpretation").Comment = m_CommentList.GetCommentListItemByCommentId(87).Comment;
				}
				else if (this.ResultIsNegative())
				{
					this.m_PanelSetOrder.PanelSetOrderCommentCollection.GetPanelSetOrderComment("report_interpretation").Comment = m_CommentList.GetCommentListItemByCommentId(86).Comment;
				}
			}
		}

		protected virtual void SetReportReferenceCRC()
		{
			if (this.CRCIndicated())
			{
				this.m_PanelSetOrder.PanelSetOrderCommentCollection.GetPanelSetOrderComment("report_reference").Comment = m_CommentList.GetCommentListItemByCommentId(49).Comment;
			}
		}

		protected virtual void SetReportReferencePapillaryThyroid()
		{
			if (this.PapillaryThyroidIndicated())
			{
				this.m_PanelSetOrder.PanelSetOrderCommentCollection.GetPanelSetOrderComment("report_reference").Comment = m_CommentList.GetCommentListItemByCommentId(23).Comment;
			}
		}

		protected virtual void SetReportReferenceMetastaticMelanoma()
		{
			if (this.MetastaticMelanomaIndicated())
			{
				this.m_PanelSetOrder.PanelSetOrderCommentCollection.GetPanelSetOrderComment("report_reference").Comment = m_CommentList.GetCommentListItemByCommentId(90).Comment;
			}
		}

		protected bool ResultIsPositive()
		{
			if (m_PanelOrderBeingAccepted.TestOrderCollection.GetTestOrder(193).Result == "BRAF Mutation V600E DETECTED.")
			{
				return true;
			}
			return false;
		}

		protected bool ResultIsNegative()
		{
			if (this.m_PanelOrderBeingAccepted.TestOrderCollection.GetTestOrder(193).Result == "BRAF Mutation V600E NOT DETECTED.")
			{
				return true;
			}
			return false;
		}

		protected bool CRCIndicated()
		{
			if (m_Indicator.Contains("Colo"))
			{
				return true;
			}
			return false;
		}

		protected bool PapillaryThyroidIndicated()
		{
			if (m_Indicator.Contains("Papillary Thyroid"))
			{
				return true;
			}
			return false;
		}

		protected bool MetastaticMelanomaIndicated()
		{
			if (m_Indicator.Contains("Metastatic melanoma"))
			{
				return true;
			}
			return false;
		}
	}
}
