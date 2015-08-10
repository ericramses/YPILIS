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
using System.ComponentModel;

namespace YellowstonePathology.YpiConnect.Client
{
	public partial class CaseFinalPage : PageFunction<Type>, YellowstonePathology.Shared.Interface.IPersistPageChanges, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;        
		private YellowstonePathology.YpiConnect.Contract.Flow.FlowAccessionCollection m_FlowAccessionCollection;

		public CaseFinalPage(YellowstonePathology.YpiConnect.Contract.Flow.FlowAccessionCollection flowAccessionCollection)
        {
			this.m_FlowAccessionCollection = flowAccessionCollection;
            InitializeComponent();
			this.DataContext = this;

			Loaded += new RoutedEventHandler(CaseFinalPage_Loaded);
        }

		protected void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		public YellowstonePathology.YpiConnect.Contract.Flow.FlowAccession FlowAccession
		{
			get { return this.m_FlowAccessionCollection[0]; }
		}

		public string FinalUnfinalButtonText
		{
			get
			{
				string result = "Final";
				if (this.m_FlowAccessionCollection[0].PanelSetOrderCollection[0].Final == true)
				{
					result = "Unfinal";
				}
				return result;
			}
		}

		private void CaseFinalPage_Loaded(object sender, RoutedEventArgs e)
		{
			UserInteractionMonitor.Instance.Register(this);
		}

		private void LeukemiaLymphomaGatingPage_Loaded(object sender, RoutedEventArgs e)
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
			OnReturn(new ReturnEventArgs<Type>(typeof(LeukemiaLymphomaResultPage)));
		}		

		private void HyperlinkCaseList_Click(object sender, RoutedEventArgs e)
		{
			OnReturn(new ReturnEventArgs<Type>(typeof(PathologistSignoutPage)));
		}

		private void HyperlinkViewReport_Click(object sender, RoutedEventArgs e)
		{
			OnReturn(new ReturnEventArgs<Type>(typeof(LeukemiaLymphomaReportPage)));
		}

		public void Save()
		{
			LeukemiaLymphomaSignoutPage.Save(this.m_FlowAccessionCollection);
		}

		private void ButtonFinalUnfinal_Click(object sender, RoutedEventArgs e)
		{
			if (this.m_FlowAccessionCollection[0].PanelSetOrderCollection[0].Final == false)
			{
				this.SignReport();
			}
			else
			{
				this.UnsignReport();
			}
			NotifyPropertyChanged("FinalUnfinalButtonText");
		}

		private void SignReport()
		{
			if (this.m_FlowAccessionCollection[0].PanelSetOrderCollection[0].FinaledById != 0)
            {
				this.m_FlowAccessionCollection[0].PanelSetOrderCollection[0].Final = true;
				this.m_FlowAccessionCollection[0].PanelSetOrderCollection[0].FinalDate = DateTime.Parse(DateTime.Now.ToShortDateString());
				this.m_FlowAccessionCollection[0].PanelSetOrderCollection[0].FinalTime = DateTime.Parse(DateTime.Now.ToShortTimeString());
				this.m_FlowAccessionCollection[0].PanelSetOrderCollection[0].Signature = YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount.Signature;
				this.m_FlowAccessionCollection[0].PanelSetOrderCollection[0].FinaledById = YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount.SystemUserId;
                LeukemiaLymphomaSignoutPage.Save(this.m_FlowAccessionCollection);
            }
            else
            {
                MessageBox.Show("You must take ownership of the case before you can final the case. Use the general page to take ownership of the case.");
            }
		}

		private void UnsignReport()
		{
			this.m_FlowAccessionCollection[0].PanelSetOrderCollection[0].Final = false;
			this.m_FlowAccessionCollection[0].PanelSetOrderCollection[0].FinalDate = null;
			this.m_FlowAccessionCollection[0].PanelSetOrderCollection[0].FinalTime = null;
			this.m_FlowAccessionCollection[0].PanelSetOrderCollection[0].FinaledById = 0;
			this.m_FlowAccessionCollection[0].PanelSetOrderCollection[0].Signature = string.Empty;
			LeukemiaLymphomaSignoutPage.Save(this.m_FlowAccessionCollection);
		}

        private void HyperlinkFinal_Click(object sender, RoutedEventArgs e)
        {

        }

        private void HyperlinkSave_Click(object sender, RoutedEventArgs e)
        {
            LeukemiaLymphomaSignoutPage.Save(this.m_FlowAccessionCollection);
        }

		public bool OkToSaveOnNavigation(Type pageNavigatingTo)
		{
			return true;
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
