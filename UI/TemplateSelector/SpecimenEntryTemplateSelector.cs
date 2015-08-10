using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace YellowstonePathology.UI.TemplateSelector
{
	public class SpecimenEntryTemplateSelector : DataTemplateSelector
	{
		public override DataTemplate SelectTemplate(object item, DependencyObject container)
		{
			YellowstonePathology.UI.MainWindow mainWindow = Application.Current.MainWindow as YellowstonePathology.UI.MainWindow;
			if (item != null)
			{
				switch (((YellowstonePathology.Domain.PanelSet.Model.PanelSetListItem)item).PanelSetName)
				{
					case "HER2 Amplification by FISH":
						return mainWindow.LabWorkspace.FindResource("DataTemplateSpecimenEntryComplex") as DataTemplate;
					default:
						return mainWindow.LabWorkspace.FindResource("DataTemplateSpecimenEntrySimple") as DataTemplate;
				}
			}
			return mainWindow.LabWorkspace.FindResource("DataTemplateSpecimenEntrySimple") as DataTemplate;
		}
	}
}
