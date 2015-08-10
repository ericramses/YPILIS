using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Rules.PanelOrder
{
    public class AcceptMthfr : Accept
    {
        string m_Test677Result;
        string m_Test1298Result;
        int m_IntrepretationId;

		public AcceptMthfr()
		{
			this.m_Rule.ActionList.Add(this.AcceptPanel);
            this.m_Rule.ActionList.Add(this.SetResults);
 			this.m_Rule.ActionList.Add(this.ChooseInterpretationId);
            this.m_Rule.ActionList.Add(this.SetInterpretation);
            this.m_Rule.ActionList.Add(this.SetReportComment);
            this.m_Rule.ActionList.Add(this.SetIndication);
			this.m_Rule.ActionList.Add(this.Save);
		}

		private void SetResults()
		{
			foreach (YellowstonePathology.Domain.Test.Model.TestOrder testOrder in this.m_PanelOrderBeingAccepted.TestOrderCollection)
            {
                if (testOrder.TestId == 250)
                {
                    m_Test677Result = testOrder.Result;
                }
                if (testOrder.TestId == 251)
                {
                    m_Test1298Result = testOrder.Result;
                }
            }
			foreach (Test.PanelSetResultOrder panelSetResultOrder in this.m_PanelSetOrder.PanelSetResultOrderCollection)
            {
                if (panelSetResultOrder.TestId == 250)
                {
                    panelSetResultOrder.Result = m_Test677Result;
                }
                if (panelSetResultOrder.TestId == 251)
                {
                    panelSetResultOrder.Result = m_Test1298Result;
                }
            }
		}

        private void ChooseInterpretationId()
        {
            m_IntrepretationId = 0;
            if (m_Test677Result == "Indeterminate" || m_Test1298Result == "Indeterminate")
            {
                m_IntrepretationId = 83;
            }
            if (m_Test677Result == "Insufficient DNA to perform analysis" || m_Test1298Result == "Insufficient DNA to perform analysis")
            {
                m_IntrepretationId = 84;
            }
            if (m_Test677Result == "Mutation Not Detected" && m_Test1298Result == "Mutation Not Detected")
            {
                m_IntrepretationId = 74;
            }
            if (m_Test677Result == "Mutation Not Detected" && m_Test1298Result == "Heterozygous Mutation Detected")
            {
                m_IntrepretationId = 79;
            }
            if (m_Test677Result == "Mutation Not Detected" && m_Test1298Result == "Homozygous Mutation Detected")
            {
                m_IntrepretationId = 82;
            }
            if (m_Test677Result == "Heterozygous Mutation Detected" && m_Test1298Result == "Mutation Not Detected")
            {
                m_IntrepretationId = 76;
            }
            if (m_Test677Result == "Homozygous Mutation Detected" && m_Test1298Result == "Mutation Not Detected")
            {
                m_IntrepretationId = 81;
            }
            if (m_Test677Result == "Heterozygous Mutation Detected" && m_Test1298Result == "Heterozygous Mutation Detected")
            {
                m_IntrepretationId = 77;
            }
            if (m_Test677Result == "Heterozygous Mutation Detected" && m_Test1298Result == "Homozygous Mutation Detected")
            {
                m_IntrepretationId = 78;
            }
            if (m_Test677Result == "Homozygous Mutation Detected" && m_Test1298Result == "Homozygous Mutation Detected")
            {
                m_IntrepretationId = 80;
            }
            if (m_Test677Result == "Homozygous Mutation Detected" && m_Test1298Result == "Heterozygous Mutation Detected")
            {
                m_IntrepretationId = 75;
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

		private void SetReportComment()
		{
			YellowstonePathology.Business.Test.PanelSetOrderComment panelSetOrderComment = YellowstonePathology.Business.Helper.PanelOrderHelper.GetPanelSetOrderCommentByCommentName(this.m_PanelSetOrder.PanelSetOrderCommentCollection, "Result Comment");
			if (m_IntrepretationId == 83)
			{
				if (string.IsNullOrEmpty(panelSetOrderComment.Comment))
				{
					panelSetOrderComment.Comment = "???";
				}
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
