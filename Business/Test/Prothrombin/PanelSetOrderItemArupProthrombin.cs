using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Persistence;

namespace YellowstonePathology.Business.Test.Prothrombin
{
	[PersistentClass(true, "tblPanelSetOrder", "YPIDATA")]
	public class PanelSetOrderItemArupProthrombin : PanelSetOrder
	{
        public PanelSetOrderItemArupProthrombin()
        {
            
        }		

		public PanelSetOrderItemArupProthrombin(string masterAccessionNo, string reportNo,
			YellowstonePathology.Domain.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Domain.IOrderTarget orderTarget,
			bool distribute,
			YellowstonePathology.Core.User.SystemIdentity systemIdentity)
			: base(masterAccessionNo, reportNo, panelSet, orderTarget, distribute, systemIdentity)
		{
			this.AddDefaultChildren(reportNo, systemIdentity);
		}

		public void AddDefaultChildren(string reportNo, Core.User.SystemIdentity systemIdentity)
        {
			PanelOrderArupProthrombin panelOrder = new PanelOrderArupProthrombin();
			panelOrder.ReportNo = reportNo;
			panelOrder.PanelOrderId = this.m_PanelOrderCollection.GetNextId(this.ReportNo);
            panelOrder.PanelId = 60;
			panelOrder.PanelName = "ARUP: Prothrombin";

			panelOrder.OrderedById = systemIdentity.User.UserId;
			panelOrder.OrderDate = DateTime.Today;
			panelOrder.OrderTime = DateTime.Now;

			panelOrder.Acknowledged = true;
			panelOrder.AcknowledgedById = systemIdentity.User.UserId;
			panelOrder.AcknowledgedDate = DateTime.Today;
			panelOrder.AcknowledgedTime = DateTime.Now;

			panelOrder.AddDefaultChildren(systemIdentity);
			this.PanelOrderCollection.Add(panelOrder);

			YellowstonePathology.Business.Test.PanelSetResultOrder panelSetResultOrder = this.m_PanelSetResultOrderCollection.GetNextItem(this.ReportNo);
			panelSetResultOrder.ReportNo = this.ReportNo;
			panelSetResultOrder.TestId = 272;
			panelSetResultOrder.TestName = "ARUP: Prothrombin";
            this.PanelSetResultOrderCollection.Add(panelSetResultOrder);

			PanelSetOrderComment interpretation = this.PanelSetOrderCommentCollection.GetNextItem(reportNo);
			interpretation.CommentName = "Interpretation";
			interpretation.DocumentCommentId = 1;
			interpretation.PlaceHolder = "report_interpretation";
			interpretation.ReportNo = this.ReportNo;
			this.PanelSetOrderCommentCollection.Add(interpretation);

			PanelSetOrderComment reportIndication = this.PanelSetOrderCommentCollection.GetNextItem(reportNo);
			reportIndication.CommentName = "Report Indication";
			reportIndication.DocumentCommentId = 8;
			reportIndication.PlaceHolder = "report_indication";
			reportIndication.ReportNo = this.ReportNo;
			this.PanelSetOrderCommentCollection.Add(reportIndication);
		}		

		public override void Finalize(AccessionOrder accessionOrder, Rules.RuleExecutionStatus ruleExecutionStatus, Core.User.SystemIdentity systemIdentity)
		{
			base.Finalize(accessionOrder, ruleExecutionStatus, systemIdentity);
		}
	}
}
