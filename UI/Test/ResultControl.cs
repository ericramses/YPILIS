using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace YellowstonePathology.UI.Test
{
    public class ResultControl : UserControl
    {
        private YellowstonePathology.Business.Test.PanelSetOrder m_TestOrder;
        private bool m_DisableRequired;

        public ResultControl(YellowstonePathology.Business.Test.PanelSetOrder testOrder)
        {
            this.m_TestOrder = testOrder;
            if (this.m_TestOrder.Final == true && this.m_TestOrder.TestOrderReportDistributionCollection.HasDistributedItems())
            {
                this.m_DisableRequired = true;
            }

            this.Loaded += ResultControl_Loaded;
        }

        public ResultControl()
        {

        }

        private void ResultControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.m_DisableRequired) DisableContents(this.Content);
        }

        private void DisableContents(object o)
        {
            if (o is Button)
            {
                Button button = (Button)o;
                string s = ((Button)o).Content.ToString();
                if (s.Contains("Next") ||
                    s.Contains("Back") ||
                    s.Contains("Close") ||
                    s.Contains("Finish"))
                {
                    button.IsEnabled = true;
                }
            }
            else if (o is Panel)
            {
                Panel panel = (Panel)o;
                foreach (UIElement element in panel.Children)
                {
                    DisableContents(element);
                }
            }

            else if (o is ContentControl)
            {
                ContentControl contentControl = (ContentControl)o;
                if (contentControl.Content != null)
                {
                    DisableContents(contentControl.Content);
                }
                else
                {
                    contentControl.IsEnabled = false;
                }
            }
            else
            {
                ((UIElement)o).IsEnabled = false;
            }

        }
    }
}
