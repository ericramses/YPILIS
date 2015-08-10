using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Windows.Controls;

namespace YellowstonePathology.Business.MessageQueues
{
	public class CytologySlideDisposalCommand : MessageCommand
	{
		DateTime m_MessageDate;

		public void SetCommandData(DateTime messageDate)
		{
			this.m_MessageDate = messageDate;
            this.Label = "Daily Cytology Slide Disposal Listing for: " + this.m_MessageDate.ToShortDateString();
		}

		public DateTime MessageDate
		{
			get { return this.m_MessageDate; }
			set { this.m_MessageDate = value; }
		}

		public override void Execute()
		{			
			base.Execute();
            YellowstonePathology.Business.Reports.CytologySlideDisposalReport report = new YellowstonePathology.Business.Reports.CytologySlideDisposalReport(this.m_MessageDate);
            System.Windows.Controls.PrintDialog printDialog = new System.Windows.Controls.PrintDialog();
            printDialog.ShowDialog();
            printDialog.PrintDocument(report.DocumentPaginator, "Cytology Slide Disposal Report for: " + this.m_MessageDate.ToShortDateString());		
		}
	}
}
