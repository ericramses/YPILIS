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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace YellowstonePathology.YpiConnect.Client
{
    /// <summary>
	/// Interaction logic for ReportBrowserListPage.xaml
    /// </summary>
	public partial class LeukemiaLymphomaResultPage : PageFunction<Type>, YellowstonePathology.Shared.Interface.IPersistPageChanges
    {
        private YellowstonePathology.YpiConnect.Contract.Flow.FlowAccessionCollection m_FlowAccessionCollection;
		private YellowstonePathology.YpiConnect.Contract.Flow.FlowCommentCollection m_FlowCommentCollection;

        public LeukemiaLymphomaResultPage(YellowstonePathology.YpiConnect.Contract.Flow.FlowAccessionCollection flowAccessionCollection, YellowstonePathology.YpiConnect.Contract.Flow.FlowCommentCollection flowCommentCollection)
        {
            this.m_FlowAccessionCollection = flowAccessionCollection;
			this.m_FlowCommentCollection = flowCommentCollection;

            InitializeComponent();
            this.DataContext = this;

			Loaded += new RoutedEventHandler(LeukemiaLymphomaResultPage_Loaded);
        }

		public YellowstonePathology.YpiConnect.Contract.Flow.FlowAccession FlowAccession
		{
			get { return this.m_FlowAccessionCollection[0]; }
		}

		public YellowstonePathology.YpiConnect.Contract.Domain.PanelSetOrderLeukemiaLymphoma PanelSetOrderLeukemiaLymphoma
		{
			get { return (YellowstonePathology.YpiConnect.Contract.Domain.PanelSetOrderLeukemiaLymphoma)this.m_FlowAccessionCollection[0].PanelSetOrderCollection[0]; }
		}

		void LeukemiaLymphomaResultPage_Loaded(object sender, RoutedEventArgs e)
		{
			UserInteractionMonitor.Instance.Register(this);
		}

		private void HyperlinkGeneral_Click(object sender, RoutedEventArgs e)
		{
			OnReturn(new ReturnEventArgs<Type>(typeof(LeukemiaLymphomaSignoutPage)));
		}

		private void HyperlinkDocuments_Click(object sender, RoutedEventArgs e)
		{
			OnReturn(new ReturnEventArgs<Type>(typeof(CaseDocumentsPage)));
		}

		private void HyperlinkGating_Click(object sender, RoutedEventArgs e)
		{
			OnReturn(new ReturnEventArgs<Type>(typeof(LeukemiaLymphomaGatingPage)));
		}

		private void HyperlinkMarkers_Click(object sender, RoutedEventArgs e)
		{
			OnReturn(new ReturnEventArgs<Type>(typeof(LeukemiaLymphomaMarkersPage)));
		}

		private void HyperlinkResults_Click(object sender, RoutedEventArgs e)
		{
		}

		private void HyperlinkFinal_Click(object sender, RoutedEventArgs e)
		{
			OnReturn(new ReturnEventArgs<Type>(typeof(CaseFinalPage)));
		}

		private void HyperlinkCaseList_Click(object sender, RoutedEventArgs e)
		{
			OnReturn(new ReturnEventArgs<Type>(typeof(PathologistSignoutPage)));
		}

		private void HyperlinkViewReport_Click(object sender, RoutedEventArgs e)
		{
			OnReturn(new ReturnEventArgs<Type>(typeof(LeukemiaLymphomaReportPage)));
		}

		public void ComboBoxLeukemiaResult_SelectionChanged(object sender, RoutedEventArgs args)
		{
			if (this.ComboBoxLeukemiaResult.SelectedValue != null)
			{
				ComboBoxItem item = (ComboBoxItem)this.ComboBoxLeukemiaResult.SelectedValue;
				switch (item.Content.ToString())
				{
					case "Normal":
						this.StackPanelCellDescription.Visibility = Visibility.Collapsed;
						this.StackPanelBTCellSelection.Visibility = Visibility.Collapsed;
						break;
					case "Abnormal":
						this.StackPanelCellDescription.Visibility = Visibility.Visible;
						this.StackPanelBTCellSelection.Visibility = Visibility.Visible;
						if (this.ComboBoxCellDescription.SelectedValue != null)
						{
							ComboBoxItem abnormalItem = (ComboBoxItem)this.ComboBoxCellDescription.SelectedValue;
						}
						break;
				}
			}
		}

		public void ComboBoxCellDescription_SelectionChanged(object sender, RoutedEventArgs args)
		{
			if (this.ComboBoxCellDescription.SelectedValue != null)
			{
				ComboBoxItem item = (ComboBoxItem)this.ComboBoxCellDescription.SelectedValue;
				switch (item.Content.ToString())
				{
					case "Lymphocytes":
						this.StackPanelBTCellSelection.Visibility = Visibility.Visible;
						break;
					default:
						this.StackPanelBTCellSelection.Visibility = Visibility.Collapsed;
						break;
				}				
			}
		}

		public void ButtonGenerateComment_Click(object sender, RoutedEventArgs args)
		{
            List<YellowstonePathology.Shared.Interface.IFlowMarker> flowMarkers = this.PanelSetOrderLeukemiaLymphoma.FlowMarkerCollection.ToList();
			YellowstonePathology.Shared.Helper.FlowCommentHelper comment = new YellowstonePathology.Shared.Helper.FlowCommentHelper(this.m_FlowAccessionCollection[0].SpecimenOrderCollection[0].Description, this.PanelSetOrderLeukemiaLymphoma, flowMarkers);
			comment.SetInterpretiveComment();
		}

		public void ButtonAppendComment_Click(object sender, RoutedEventArgs args)
		{
			if (this.m_FlowCommentCollection.Count == 0)
			{
				YellowstonePathology.YpiConnect.Proxy.FlowSignoutServiceProxy flowSignoutServiceProxy = new Proxy.FlowSignoutServiceProxy();
				YellowstonePathology.YpiConnect.Contract.Flow.FlowCommentCollection flowCommentCollection = flowSignoutServiceProxy.GetFlowComments();
				foreach(YellowstonePathology.YpiConnect.Contract.Flow.FlowComment flowComment in flowCommentCollection)
				{
					this.m_FlowCommentCollection.Add(flowComment);
				}
			}

			LeukemiaLymphomaCommentDialog leukemiaLymphomaCommentDialog = new LeukemiaLymphomaCommentDialog(this.m_FlowCommentCollection);
			bool? result = leukemiaLymphomaCommentDialog.ShowDialog();
			if (result.HasValue && result.Value == true)
			{
				List<YellowstonePathology.Shared.Interface.IFlowMarker> flowMarkers = this.PanelSetOrderLeukemiaLymphoma.FlowMarkerCollection.ToList();
				YellowstonePathology.Shared.Helper.FlowCommentHelper comment = new YellowstonePathology.Shared.Helper.FlowCommentHelper(this.m_FlowAccessionCollection[0].SpecimenOrderCollection[0].Description, this.PanelSetOrderLeukemiaLymphoma, flowMarkers);
				YellowstonePathology.YpiConnect.Contract.Flow.FlowComment item = leukemiaLymphomaCommentDialog.SelectedComment;
				comment.AddComment(item.Category, item.Impression, item.Comment);
			}
		}

        private void HyperlinkSave_Click(object sender, RoutedEventArgs e)
        {
			this.Save();
        }

		public bool OkToSaveOnNavigation(Type pageNavigatingTo)
		{
            bool result = true;
            if (LeukemiaLyphomaNavigationGroup.Instance.IsInGroup(pageNavigatingTo) == true)
            {
                result = false;
            }
            return result;
		}

		public bool OkToSaveOnClose()
		{
			return true;
		}

		public void Save()
		{
			LeukemiaLymphomaSignoutPage.Save(this.m_FlowAccessionCollection);
		}

		public void UpdateBindingSources()
		{
		}
	}
}
