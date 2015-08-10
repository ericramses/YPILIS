using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Rules.PanelOrder
{
	public class AcceptBCellClonality
	{
		YellowstonePathology.Business.Rules.Rule m_Rule;
		YellowstonePathology.Business.Rules.ExecutionStatus m_ExecutionStatus;
		YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrderItem;
		YellowstonePathology.Core.User.SystemUser m_SystemUser;

        bool m_ResultIsSet;

		YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;

		public AcceptBCellClonality()
		{
            this.m_ResultIsSet = false;
			this.m_Rule = new YellowstonePathology.Business.Rules.Rule();
			this.m_Rule.ActionList.Add(this.UserHasPermission);
			this.m_Rule.ActionList.Add(this.IsFinal);
			this.m_Rule.ActionList.Add(this.IsAlreadyAccepted);
			this.m_Rule.ActionList.Add(this.ResultsArePresent);
			this.m_Rule.ActionList.Add(this.SetPositiveResult);
            this.m_Rule.ActionList.Add(this.SetIndeterminateResult);
            this.m_Rule.ActionList.Add(this.SetNegativeResult);
			this.m_Rule.ActionList.Add(this.SetIntrepretation);
			this.m_Rule.ActionList.Add(this.SetAccepted);
			this.m_Rule.ActionList.Add(this.SaveChanges);
		}

		private void UserHasPermission()
		{
			YellowstonePathology.Core.User.SystemUserRoleDescriptionList systemUserRoleDescriptionList = new Core.User.SystemUserRoleDescriptionList();
			systemUserRoleDescriptionList.Add(Core.User.SystemUserRoleDescriptionEnum.Administrator);
			systemUserRoleDescriptionList.Add(Core.User.SystemUserRoleDescriptionEnum.AmendmentSigner);
			systemUserRoleDescriptionList.Add(Core.User.SystemUserRoleDescriptionEnum.HistologyLog);
			systemUserRoleDescriptionList.Add(Core.User.SystemUserRoleDescriptionEnum.MedTech);
			systemUserRoleDescriptionList.Add(Core.User.SystemUserRoleDescriptionEnum.MolecularCaseFinal);
			systemUserRoleDescriptionList.Add(Core.User.SystemUserRoleDescriptionEnum.Pathologist);
			systemUserRoleDescriptionList.Add(Core.User.SystemUserRoleDescriptionEnum.MolecularCaseTech);

			if (!this.m_SystemUser.IsUserInRole(systemUserRoleDescriptionList))
			{
				this.m_ExecutionStatus.AddMessage("It appears you do not have permission to accept results on this panel.", true);
			}
		}

		private void IsFinal()
		{
			if (m_PanelSetOrderItem.Final)
			{
				this.m_ExecutionStatus.AddMessage(m_PanelSetOrderItem.ReportNo + " is already finaled.", true);
			}
		}

		private void IsAlreadyAccepted()
		{
			if (this.m_PanelSetOrderItem.CurrentPanelOrder.Accepted)
			{
				this.m_ExecutionStatus.AddMessage(m_PanelSetOrderItem.ReportNo + " B Cell Clonality By PCR results have already been accepted.", true);
			}
		}

		private void ResultsArePresent()
		{
			foreach (YellowstonePathology.Domain.Test.Model.TestOrder item in this.m_PanelSetOrderItem.CurrentPanelOrder.TestOrderCollection)
			{
				if (string.IsNullOrEmpty(item.Result))
				{
					this.m_ExecutionStatus.AddMessage("A test result is not set", true);
				}
			}
		}

        private void SetPositiveResult()
        {
			foreach (YellowstonePathology.Domain.Test.Model.TestOrder item in this.m_PanelSetOrderItem.CurrentPanelOrder.TestOrderCollection)
            {
                if (item.Result.ToUpper().StartsWith("CLONAL") == true)
                {
                    string result = "Positive for a monoclonal B-cell population.";
					YellowstonePathology.Business.Test.PanelSetResultOrder panelSetResultOrder = m_PanelSetOrderItem.PanelSetResultOrderCollection.GetPanelSetResultOrder(254);
                    panelSetResultOrder.Result = result;
                    this.m_ResultIsSet = true;
                    break;
                }
            }
        }

		private void SetIndeterminateResult()
		{
            if (this.m_ResultIsSet == false)
            {
                string result = "Negative for a monoclonal B-cell population.";
				foreach (YellowstonePathology.Domain.Test.Model.TestOrder item in this.m_PanelSetOrderItem.CurrentPanelOrder.TestOrderCollection)
                {
                    if (item.Result.ToUpper().StartsWith("INDETERMINATE") == true)
                    {
                        result = "Indeterminate";
                        this.m_ResultIsSet = true;
                        break;
                    }
                }

				YellowstonePathology.Business.Test.PanelSetResultOrder panelSetResultOrder = m_PanelSetOrderItem.PanelSetResultOrderCollection.GetPanelSetResultOrder(254);
                panelSetResultOrder.Result = result;
            }
		}

        private void SetNegativeResult()
        {
            if (this.m_ResultIsSet == false)
            {
                bool resultIsNegative = true;
				foreach (YellowstonePathology.Domain.Test.Model.TestOrder item in this.m_PanelSetOrderItem.CurrentPanelOrder.TestOrderCollection)
                {
                    if (item.Result.ToUpper().StartsWith("NOT CLONAL") == false)
                    {
                        resultIsNegative = false;
                        break;
                    }
                }
                if (resultIsNegative == true)
                {
                    string result = "Negative for a monoclonal B-cell population.";
					YellowstonePathology.Business.Test.PanelSetResultOrder panelSetResultOrder = m_PanelSetOrderItem.PanelSetResultOrderCollection.GetPanelSetResultOrder(254);
                    panelSetResultOrder.Result = result;
                }
            }
        }

		private void SetIntrepretation()
		{
			YellowstonePathology.Business.Test.PanelSetResultOrder panelSetResultOrder = m_PanelSetOrderItem.PanelSetResultOrderCollection.GetPanelSetResultOrder(254);
			YellowstonePathology.Domain.CommentList commentList = GetCommentList();
			if(panelSetResultOrder.Result.IndexOf("Positive") > -1)
			{
				this.m_PanelSetOrderItem.PanelSetOrderCommentCollection.GetPanelSetOrderComment("report_interpretation").Comment = commentList.GetCommentListItemByCommentId(92).Comment;
			}
			else if(panelSetResultOrder.Result.IndexOf("Negative") > -1)
			{
				this.m_PanelSetOrderItem.PanelSetOrderCommentCollection.GetPanelSetOrderComment("report_interpretation").Comment = commentList.GetCommentListItemByCommentId(93).Comment;
			}
			else
			{
				this.m_PanelSetOrderItem.PanelSetOrderCommentCollection.GetPanelSetOrderComment("report_interpretation").Comment = commentList.GetCommentListItemByCommentId(94).Comment;
			}
		}

		private void SetAccepted()
		{
			this.m_PanelSetOrderItem.CurrentPanelOrder.Accepted = true;
			this.m_PanelSetOrderItem.CurrentPanelOrder.AcceptedDate = DateTime.Today;
			this.m_PanelSetOrderItem.CurrentPanelOrder.AcceptedTime = DateTime.Now;
			this.m_PanelSetOrderItem.CurrentPanelOrder.AcceptedById = m_SystemUser.UserId;
		}

		private void SaveChanges()
		{
		}

		private YellowstonePathology.Domain.CommentList GetCommentList()
		{
			YellowstonePathology.Domain.CommentList commentList = new YellowstonePathology.Domain.CommentList();
			commentList = Gateway.LocalDataGateway.GetCommentList();
			return commentList;
		}

		public void Execute(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder, YellowstonePathology.Core.User.SystemUser systemUser, YellowstonePathology.Business.Rules.ExecutionStatus executionStatus)
		{
			this.m_AccessionOrder = accessionOrder;
			this.m_PanelSetOrderItem = panelSetOrder;
			this.m_SystemUser = systemUser;
			this.m_ExecutionStatus = executionStatus;
			this.m_Rule.Execute(this.m_ExecutionStatus);
		}
	}
}
