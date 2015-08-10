using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace YellowstonePathology.UI.Test
{
	/// <summary>
	/// Interaction logic for BatchAssignmentSelectionDialog.xaml
	/// </summary>
	public partial class BatchAssignmentSelectionDialog : Window
	{
		private YellowstonePathology.Business.Panel.Model.PanelOrderBatchList m_PanelOrderBatchList;
		private YellowstonePathology.Business.Panel.Model.PanelOrderBatch m_SelectedPanelOrderBatch;

		public BatchAssignmentSelectionDialog(YellowstonePathology.Business.Panel.Model.PanelOrderBatchList panelOrderBatchList)
		{
			this.m_PanelOrderBatchList = panelOrderBatchList;
			InitializeComponent();
			this.DataContext = this;
		}

		public YellowstonePathology.Business.Panel.Model.PanelOrderBatchList BatchList
		{
			get { return this.m_PanelOrderBatchList; }
		}

		public YellowstonePathology.Business.Panel.Model.PanelOrderBatch SelectedPanelOrderBatch
		{
			get { return this.m_SelectedPanelOrderBatch; }
		}

		private void ButtonOk_Click(object sender, RoutedEventArgs e)
		{
			if (this.listViewBatchList.SelectedItem != null)
			{
				YellowstonePathology.Business.Panel.Model.PanelOrderBatch item = (YellowstonePathology.Business.Panel.Model.PanelOrderBatch)this.listViewBatchList.SelectedItem;
				if (item.PanelOrderBatchId > 0)
				{
					this.m_SelectedPanelOrderBatch = item;
					this.DialogResult = true;
					Close();
				}
				else
				{
					MessageBox.Show("Invalid batch selected.", "Unassigned batch not valid", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				}
			}
			else
			{
				MessageBox.Show("Select a batch for assignment.", "No batch selected", MessageBoxButton.OK, MessageBoxImage.Exclamation);
			}
		}

		private void ButtonCancel_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = false;
			Close();
		}
	}
}
