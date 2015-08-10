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
	public partial class LeukemiaLymphomaGatingPage : PageFunction<Type>, YellowstonePathology.Shared.Interface.IPersistPageChanges
    {
        private YellowstonePathology.YpiConnect.Contract.Flow.FlowAccessionCollection m_FlowAccessionCollection;

        public LeukemiaLymphomaGatingPage(YellowstonePathology.YpiConnect.Contract.Flow.FlowAccessionCollection flowAccessionCollection)
        {
            this.m_FlowAccessionCollection = flowAccessionCollection;

            InitializeComponent();

            this.DataContext = this; 
			Loaded += new RoutedEventHandler(LeukemiaLymphomaGatingPage_Loaded);
        }

		private void LeukemiaLymphomaGatingPage_Loaded(object sender, RoutedEventArgs e)
		{
			UserInteractionMonitor.Instance.Register(this);
		}

		public YellowstonePathology.YpiConnect.Contract.Domain.PanelSetOrderLeukemiaLymphoma PanelSetOrderLeukemiaLymphoma
		{
			get { return (YellowstonePathology.YpiConnect.Contract.Domain.PanelSetOrderLeukemiaLymphoma)this.m_FlowAccessionCollection[0].PanelSetOrderCollection[0]; }
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

		}

		private void HyperlinkMarkers_Click(object sender, RoutedEventArgs e)
		{
			OnReturn(new ReturnEventArgs<Type>(typeof(LeukemiaLymphomaMarkersPage)));
		}

		private void HyperlinkResults_Click(object sender, RoutedEventArgs e)
		{
			OnReturn(new ReturnEventArgs<Type>(typeof(LeukemiaLymphomaResultPage)));
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
