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
	public partial class LeukemiaLymphomaMarkersPage : PageFunction<Type>, YellowstonePathology.Shared.Interface.IPersistPageChanges
    {
        private YellowstonePathology.YpiConnect.Contract.Flow.FlowAccessionCollection m_FlowAccessionCollection;

        public LeukemiaLymphomaMarkersPage(YellowstonePathology.YpiConnect.Contract.Flow.FlowAccessionCollection flowAccessionCollection)
        {
            this.m_FlowAccessionCollection = flowAccessionCollection;

            InitializeComponent();
            this.DataContext = this;

			Loaded += new RoutedEventHandler(LeukemiaLymphomaMarkersPage_Loaded);
        }

		private void LeukemiaLymphomaMarkersPage_Loaded(object sender, RoutedEventArgs e)
		{
			UserInteractionMonitor.Instance.Register(this);
		}

		public YellowstonePathology.YpiConnect.Contract.Domain.PanelSetOrderLeukemiaLymphoma PanelSetOrderLeukemiaLymphoma
		{
			get { return (YellowstonePathology.YpiConnect.Contract.Domain.PanelSetOrderLeukemiaLymphoma)this.m_FlowAccessionCollection[0].PanelSetOrderCollection[0]; }
		}

		private void ButtonAdd_Click(object sender, RoutedEventArgs e)
		{
            FlowMarkerDialog flowMarkerDialog = new FlowMarkerDialog(this.m_FlowAccessionCollection[0]);
			bool? result = flowMarkerDialog.ShowDialog();
			if (result.HasValue && result.Value == true)
			{
				List<YellowstonePathology.YpiConnect.Contract.Flow.Marker> markers = flowMarkerDialog.SelectedMarkers;
				YellowstonePathology.YpiConnect.Contract.Domain.PanelSetOrderLeukemiaLymphoma panelSetOrderLeukemiaLymphoma = (YellowstonePathology.YpiConnect.Contract.Domain.PanelSetOrderLeukemiaLymphoma)m_FlowAccessionCollection[0].PanelSetOrderCollection[0];
				foreach (YellowstonePathology.YpiConnect.Contract.Flow.Marker marker in markers)
				{
					panelSetOrderLeukemiaLymphoma.FlowMarkerCollection.Add(marker, panelSetOrderLeukemiaLymphoma.ReportNo);
				}
			}
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
		}

		private void HyperlinkResults_Click(object sender, RoutedEventArgs e)
		{
			OnReturn(new ReturnEventArgs<Type>(typeof(LeukemiaLymphomaResultPage)));
		}

		private void HyperlinkCoding_Click(object sender, RoutedEventArgs e)
		{

		}

		private void HyperlinkAmendments_Click(object sender, RoutedEventArgs e)
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

		private void HyperlinkSave_Click(object sender, RoutedEventArgs e)
		{
			this.Save();
		}

		public void Save()
		{
            LeukemiaLymphomaSignoutPage.Save(this.m_FlowAccessionCollection);
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

		public void UpdateBindingSources()
		{
		}
	}
}
