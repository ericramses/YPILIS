using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace YellowstonePathology.UI.Test
{
    public class ResultDisabler
    {
        public ResultDisabler() { }

        public static void Disable(object o)
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
            else if(o is Panel)
            {
                Panel panel = (Panel)o;
                foreach (UIElement element in panel.Children)
                {
                    Disable(element);
                }
            }

            else if (o is ContentControl)
            {
                ContentControl contentControl = (ContentControl)o;
                if (contentControl.Content != null)
                {
                    Disable(contentControl.Content);
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
