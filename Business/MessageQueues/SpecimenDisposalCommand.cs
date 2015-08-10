using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Windows.Controls;

namespace YellowstonePathology.Business.MessageQueues
{
	public class SpecimenDisposalCommand : MessageCommand
	{
		DateTime m_MessageDate;

		public void SetCommandData(DateTime messageDate)
		{
			this.m_MessageDate = messageDate;
			this.Label = "Daily Specimen Disposal Listing for: " + this.m_MessageDate.ToShortDateString();
		}

		public DateTime MessageDate
		{
			get { return this.m_MessageDate; }
			set { this.m_MessageDate = value; }
		}

		public override void Execute()
		{
			try
			{
				base.Execute();
				YellowstonePathology.Business.Reports.SpecimenDisposalReport report = new YellowstonePathology.Business.Reports.SpecimenDisposalReport(this.m_MessageDate);
				System.Windows.Controls.PrintDialog printDialog = new System.Windows.Controls.PrintDialog();
				printDialog.ShowDialog();
				printDialog.PrintDocument(report.DocumentPaginator, "Specimen Disposal Report for: " + this.m_MessageDate.ToShortDateString());
			}
			catch (Exception e)
			{
				this.ErrorInExecution = true;
				this.ErrorMessage = e.Message;
			}
		}
	}
}
