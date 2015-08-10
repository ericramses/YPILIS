using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Domain;

namespace YellowstonePathology.Business.MessageQueues
{
    public class POCRetensionCommand : MessageCommand
	{
        DateTime m_MessageDate;
        DateTime m_LastMonday;
        DateTime m_LastSunday;

        public void SetCommandData()
        {
            this.m_MessageDate = DateTime.Today;
            this.m_LastMonday = YellowstonePathology.Business.Helper.DateTimeExtensions.StartOfLastWeek(DateTime.Today, DayOfWeek.Monday);
            this.m_LastSunday = this.m_LastMonday.AddDays(7);
            this.Label = "Products of conception retention for the week of: " + this.m_LastMonday.ToShortDateString();
        }               

        public DateTime MessageDate
        {
            get { return this.m_MessageDate; }
            set { this.m_MessageDate = value; }
        }

        public DateTime LastMonday
        {
            get { return this.m_LastMonday; }
            set { this.m_LastMonday = value; }
        }

        public DateTime LastSunday
        {
            get { return this.m_LastSunday; }
            set { this.m_LastSunday = value; }
        }

        public override void Execute()
        {
            try
            {
                base.Execute();
                YellowstonePathology.Business.Reports.POCRetensionReport report = new YellowstonePathology.Business.Reports.POCRetensionReport(this.LastMonday, this.LastSunday);
                System.Windows.Controls.PrintDialog printDialog = new System.Windows.Controls.PrintDialog();
                printDialog.ShowDialog();
                printDialog.PrintDocument(report.DocumentPaginator, "POC Retention Report for: " + this.LastMonday.ToShortDateString());
            }
            catch (Exception e)
            {
                this.ErrorInExecution = true;
                this.ErrorMessage = e.Message;
            }
        }
	}
}
