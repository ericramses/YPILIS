using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI
{
    public partial class PantherOrdersReport 
    {
        private System.Drawing.Printing.PrintDocument m_PrintDocument;        
        private Queue<YellowstonePathology.Business.Test.PanelSetOrder> m_PanelSetOrderQueue;

        public PantherOrdersReport(YellowstonePathology.Business.Test.PanelSetOrderCollection panelSetOrderCollection)
        {
            this.m_PanelSetOrderQueue = new Queue<Business.Test.PanelSetOrder>();
            foreach(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder in panelSetOrderCollection)
            {
                this.m_PanelSetOrderQueue.Enqueue(panelSetOrder);
            }
        }

        public void Print()
        {
            this.m_PrintDocument = new System.Drawing.Printing.PrintDocument();
            this.m_PrintDocument.PrinterSettings = new System.Drawing.Printing.PrinterSettings();
            this.m_PrintDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrintDocument_PrintPage);
            this.m_PrintDocument.Print();
        }

        private void PrintDocument_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            int count = 1;
            int x = 50;
            int y = 50;

            this.DrawHeader(x, y, e);
            y = y + 50;

            for (int i = 0; i < 4; i++)
            {
                YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = this.m_PanelSetOrderQueue.Dequeue();
                this.DrawLine(x, y, panelSetOrder, e);
                y = y + 30;
                count += 1;
                if (this.m_PanelSetOrderQueue.Count == 0) break;
            }

            if (this.m_PanelSetOrderQueue.Count == 0)
            {
                e.HasMorePages = false;
            }
            else
            {
                e.HasMorePages = true;
            }
        }

        private void DrawHeader(int x, int y, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString("Panther Orders Not Aliquoted", new System.Drawing.Font("Verdana", 12, System.Drawing.FontStyle.Bold), System.Drawing.Brushes.Black, new System.Drawing.PointF(x, y));            
        }

        private void DrawLine(int x, int y, YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawString(panelSetOrder.ReportNo, new System.Drawing.Font("Verdana", 9), System.Drawing.Brushes.Black, new System.Drawing.PointF(x, y));
            e.Graphics.DrawString(panelSetOrder.PanelSetName, new System.Drawing.Font("Verdana", 9), System.Drawing.Brushes.Black, new System.Drawing.PointF(x + 150, y));            
        }
    }
}
