using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace YellowstonePathology.UI.TemplateSelector
{
	public class SummaryReportTemplateSelector : DataTemplateSelector
	{
		public override DataTemplate SelectTemplate(object item, DependencyObject container)
		{
			Window window = null;
			foreach (Window currentWindow in Application.Current.Windows)
			{
				if (currentWindow.Name == "SummaryReportsWindow")
				{
					window = currentWindow;
					break;
				}
			}
			if (item != null)
			{
				switch (((YellowstonePathology.Business.Domain.AccessionSummary)item).ReportType)
				{
					case "Breast Cancer":
						return ((Common.SummaryReportsDialog)window).FindResource("DataTemplateBreastCancer") as DataTemplate;
					case "Colorectal Cancer":
						return ((Common.SummaryReportsDialog)window).FindResource("DataTemplateColorectalCancer") as DataTemplate;
					case "Hematopathology":
						return ((Common.SummaryReportsDialog)window).FindResource("DataTemplateHematopathology") as DataTemplate;
					default:
						return ((Common.SummaryReportsDialog)window).FindResource("DataTemplateBlank") as DataTemplate;
				}
			}
			return ((Common.SummaryReportsDialog)window).FindResource("DataTemplateBlank") as DataTemplate;
		}
	}
}
