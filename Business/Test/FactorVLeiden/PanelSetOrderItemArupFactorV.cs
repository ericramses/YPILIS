using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Persistence;

namespace YellowstonePathology.Business.Test.FactorV
{
	[PersistentClass(true, "tblPanelSetOrder", "YPIDATA")]
	public class PanelSetOrderItemArupFactorV : PanelSetOrder
	{
        public PanelSetOrderItemArupFactorV()
        {
            
        }		

		public PanelSetOrderItemArupFactorV(string masterAccessionNo, string reportNo,
			YellowstonePathology.Domain.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Domain.IOrderTarget orderTarget,
			YellowstonePathology.Core.User.SystemIdentity systemIdentity)
			: base(masterAccessionNo, reportNo, panelSet, orderTarget, systemIdentity)
		{
            this.AddDefaultChildren(reportNo, systemIdentity);
		}		

		public void AddDefaultChildren(string reportNo, Core.User.SystemIdentity systemIdentity)
		{
			PanelOrderArupFactorV panelOrder = new PanelOrderArupFactorV();
			panelOrder.ReportNo = reportNo;
			panelOrder.PanelOrderId = this.m_PanelOrderCollection.GetNextId(this.ReportNo);
			panelOrder.PanelId = 53;
			panelOrder.PanelName = "ARUP: FACV";

			panelOrder.OrderedById = systemIdentity.User.UserId;
			panelOrder.OrderDate = DateTime.Today;
			panelOrder.OrderTime = DateTime.Now;

			panelOrder.Acknowledged = true;
			panelOrder.AcknowledgedById = systemIdentity.User.UserId;
			panelOrder.AcknowledgedDate = DateTime.Today;
			panelOrder.AcknowledgedTime = DateTime.Now;

			panelOrder.AddDefaultChildren(systemIdentity);
			this.PanelOrderCollection.Add(panelOrder);

			PanelSetResultOrder resultOrder = this.PanelSetResultOrderCollection.GetNextItem(this.ReportNo);
			resultOrder.ReportNo = this.ReportNo;
			resultOrder.TestId = 242;
			resultOrder.TestName = "Factor V Leiden";
			this.PanelSetResultOrderCollection.Add(resultOrder);


			PanelSetOrderComment interpretation = this.PanelSetOrderCommentCollection.GetNextItem(reportNo);
			interpretation.CommentName = "Interpretation";
			interpretation.DocumentCommentId = 1;
			interpretation.PlaceHolder = "report_interpretation";
			interpretation.ReportNo = this.ReportNo;
			this.PanelSetOrderCommentCollection.Add(interpretation);

			PanelSetOrderComment reportComment = this.PanelSetOrderCommentCollection.GetNextItem(reportNo);
			reportComment.CommentName = "Report Comment";
			reportComment.DocumentCommentId = 4;
			reportComment.PlaceHolder = "report_comment";
			reportComment.ReportNo = this.ReportNo;
			this.PanelSetOrderCommentCollection.Add(reportComment);

			PanelSetOrderComment reportIndication = this.PanelSetOrderCommentCollection.GetNextItem(reportNo);
			reportIndication.CommentName = "Report Indication";
			reportIndication.DocumentCommentId = 8;
			reportIndication.PlaceHolder = "report_indication";
			reportIndication.ReportNo = this.ReportNo;
			this.PanelSetOrderCommentCollection.Add(reportIndication);
		}
	}
}
