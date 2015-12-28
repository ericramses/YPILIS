using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace YellowstonePathology.UI.Test
{
    public class ResultControl : UserControl
    {
        private YellowstonePathology.Business.Test.PanelSetOrder m_TestOrder;
        private bool m_DisableRequired;

        public ResultControl(YellowstonePathology.Business.Test.PanelSetOrder testOrder)
        {
            this.m_TestOrder = testOrder;
            if (this.m_TestOrder.Final == true &&
                (this.m_TestOrder.Distribute == false || this.m_TestOrder.TestOrderReportDistributionCollection.HasDistributedItems()))
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
                FrameworkElement frameworkElement = (FrameworkElement)o;
            	this.SetEnableForRemainActiveInName(frameworkElement);
            }
            else if (o is TextBlock)
            {
                FrameworkElement frameworkElement = (FrameworkElement)o;
            	this.SetEnableForRemainActiveInName(frameworkElement);
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
        
        private void SetEnableForRemainActiveInName(FrameworkElement frameworkElement)
        {
        	if(frameworkElement.Visibility == Visibility.Visible)
        	{
	            string s = frameworkElement.Name;
	            if(string.IsNullOrEmpty(s) == true)
	            {
	            	frameworkElement.IsEnabled = false;
	            }
	            else if (s.Contains("RemainActive"))
	            {
	                frameworkElement.IsEnabled = true;
	            }
	            else
	            {
	            	frameworkElement.IsEnabled = false;
	            }
        	}
        	else
        	{
            	frameworkElement.IsEnabled = false;
        	}
        }
    }
}
