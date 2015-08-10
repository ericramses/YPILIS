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

namespace YellowstonePathology.YpiConnect.Client                                                           
{
    /// <summary>
	/// Interaction logic for ReportBrowserListPage.xaml
    /// </summary>
	public partial class LeukemiaLymphomaSignoutPage : PageFunction<Boolean>, YellowstonePathology.Shared.Interface.IPersistPageChanges
    {
		private YellowstonePathology.YpiConnect.Contract.Flow.FlowCommentCollection m_FlowCommentCollection;
        private YellowstonePathology.YpiConnect.Contract.Flow.FlowAccessionCollection m_FlowAccessionCollection;
        private YellowstonePathology.YpiConnect.Proxy.FlowSignoutServiceProxy m_FlowSignoutServiceProxy;
		private YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccountCollection m_WebServiceAccountCollection;

		public LeukemiaLymphomaSignoutPage(string masterAccessionNo)
        {
            this.m_FlowSignoutServiceProxy = new Proxy.FlowSignoutServiceProxy();
            this.m_FlowAccessionCollection = this.m_FlowSignoutServiceProxy.GetFlowAccessionCollection(masterAccessionNo);
			this.m_FlowAccessionCollection[0].SetOriginalValues();
			YellowstonePathology.Business.Domain.Persistence.SerializationHelper.SetTrackingState(this.m_FlowAccessionCollection);
			this.m_FlowCommentCollection = new Contract.Flow.FlowCommentCollection();
			YellowstonePathology.Business.Domain.Persistence.SerializationHelper.SetTrackingState(this.m_FlowCommentCollection);

			YellowstonePathology.YpiConnect.Proxy.WebServiceAccountServiceProxy webServiceAccountServiceProxy = new Proxy.WebServiceAccountServiceProxy();
			this.m_WebServiceAccountCollection = webServiceAccountServiceProxy.GetWebServiceAccountCollectionByFacilityId(YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount.FacilityId);

            InitializeComponent();

			this.DataContext = this;
			this.Loaded += new RoutedEventHandler(LeukemiaLymphomaSignoutPage_Loaded);            
        }
        
		public void LeukemiaLymphomaSignoutPage_Loaded(object sender, RoutedEventArgs e)
		{
			UserInteractionMonitor.Instance.Register(this);
		}

        private void HyperlinkGeneral_Click(object sender, RoutedEventArgs e)
        {
		}        

        private void HyperlinkDocuments_Click(object sender, RoutedEventArgs e)
        {
			this.ShowCaseDocumentsPage();
        }

        private void HyperlinkGating_Click(object sender, RoutedEventArgs e)
        {
			this.ShowLeukemiaLymphomaGatingPage();
		}

        private void HyperlinkMarkers_Click(object sender, RoutedEventArgs e)
        {
			this.ShowLeukemiaLymphomaMarkersPage();
		}

        private void HyperlinkResults_Click(object sender, RoutedEventArgs e)
        {
			this.ShowLeukemiaLymphomaResultPage();
		}

		private void HyperlinkViewReport_Click(object sender, RoutedEventArgs e)
		{
			LeukemiaLymphomaReportPage leukemiaLymphomaReportPage = new LeukemiaLymphomaReportPage(this.m_FlowAccessionCollection[0].PanelSetOrderCollection[0].ReportNo, this.m_FlowAccessionCollection[0].MasterAccessionNo);
			this.SetReportPageReturn(leukemiaLymphomaReportPage, this);
			ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(leukemiaLymphomaReportPage);
		}

        private void HyperlinkFinal_Click(object sender, RoutedEventArgs e)
        {
			this.ShowCaseFinalPage();
		}

        private void HyperlinkCaseList_Click(object sender, RoutedEventArgs e)
        {
			Save(this.m_FlowAccessionCollection);
			OnReturn(null);
        }

		private void Page_Return(object sender, ReturnEventArgs<Type> e)
		{
			if(e.Result == typeof(CaseDocumentsPage))
			{
				this.ShowCaseDocumentsPage();
			}
			else if (e.Result == typeof(LeukemiaLymphomaGatingPage))
			{
				this.ShowLeukemiaLymphomaGatingPage();
			}
			else if (e.Result == typeof(LeukemiaLymphomaMarkersPage))
			{
				this.ShowLeukemiaLymphomaMarkersPage();
			}
			else if (e.Result == typeof(LeukemiaLymphomaResultPage))
			{
				this.ShowLeukemiaLymphomaResultPage();
			}
			else if (e.Result == typeof(CaseFinalPage))
			{
				this.ShowCaseFinalPage();
			}
			else if (e.Result == typeof(LeukemiaLymphomaReportPage))
			{
				LeukemiaLymphomaReportPage leukemiaLymphomaReportPage = new LeukemiaLymphomaReportPage(this.m_FlowAccessionCollection[0].PanelSetOrderCollection[0].ReportNo, this.m_FlowAccessionCollection[0].MasterAccessionNo);
				this.SetReportPageReturn(leukemiaLymphomaReportPage, sender);
				ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(leukemiaLymphomaReportPage);
			}
			else if (e.Result == typeof(PathologistSignoutPage))
			{
				Save(this.m_FlowAccessionCollection);
				OnReturn(null);
			}
		}

		private void SetReportPageReturn(LeukemiaLymphomaReportPage leukemiaLymphomaReportPage, object caller)
		{
			switch (caller.GetType().Name)
			{
				case "LeukemiaLymphomaGatingPage":
					leukemiaLymphomaReportPage.Return += new ReturnEventHandler<Type>(LeukemiaLymphomaReportPageGating_Return);
					break;
				case "LeukemiaLymphomaMarkersPage":
					leukemiaLymphomaReportPage.Return += new ReturnEventHandler<Type>(LeukemiaLymphomaReportPageMarkers_Return);
					break;
				case "LeukemiaLymphomaResultPage":
					leukemiaLymphomaReportPage.Return += new ReturnEventHandler<Type>(LeukemiaLymphomaReportPageResult_Return);
					break;
				case "CaseFinalPage":
					leukemiaLymphomaReportPage.Return += new ReturnEventHandler<Type>(LeukemiaLymphomaReportPageFinal_Return);
					break;
				case "LeukemiaLymphomaSignoutPage":
					leukemiaLymphomaReportPage.Return += new ReturnEventHandler<Type>(LeukemiaLymphomaReportPageSignout_Return);
					break;
				case "CaseDocumentsPage":
					leukemiaLymphomaReportPage.Return += new ReturnEventHandler<Type>(LeukemiaLymphomaReportPageDocuments_Return);
					break;
			}
		}

		private void LeukemiaLymphomaReportPageDocuments_Return(object sender, ReturnEventArgs<Type> e)
		{
			this.ShowCaseDocumentsPage();
		}

		private void LeukemiaLymphomaReportPageGating_Return(object sender, ReturnEventArgs<Type> e)
		{
			this.ShowLeukemiaLymphomaGatingPage();
		}

		private void LeukemiaLymphomaReportPageMarkers_Return(object sender, ReturnEventArgs<Type> e)
		{
			this.ShowLeukemiaLymphomaMarkersPage();
		}

		private void LeukemiaLymphomaReportPageResult_Return(object sender, ReturnEventArgs<Type> e)
		{
			this.ShowLeukemiaLymphomaResultPage();
		}

		private void LeukemiaLymphomaReportPageFinal_Return(object sender, ReturnEventArgs<Type> e)
		{
			this.ShowCaseFinalPage();
		}

		private void LeukemiaLymphomaReportPageSignout_Return(object sender, ReturnEventArgs<Type> e)
		{
		}

		private void ShowCaseDocumentsPage()
		{
			CaseDocumentsPage caseDocumentsPage = new CaseDocumentsPage(this.m_FlowAccessionCollection[0].CaseDocumentList);
			caseDocumentsPage.Return += new ReturnEventHandler<Type>(Page_Return);
			ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(caseDocumentsPage);
		}

		private void ShowLeukemiaLymphomaGatingPage()
		{
			LeukemiaLymphomaGatingPage leukemiaLymphomaGatingPage = new LeukemiaLymphomaGatingPage(this.m_FlowAccessionCollection);
			leukemiaLymphomaGatingPage.Return += new ReturnEventHandler<Type>(Page_Return);
			ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(leukemiaLymphomaGatingPage);
		}

		private void ShowLeukemiaLymphomaMarkersPage()
		{
			LeukemiaLymphomaMarkersPage leukemiaLymphomaMarkersPage = new LeukemiaLymphomaMarkersPage(this.m_FlowAccessionCollection);
			leukemiaLymphomaMarkersPage.Return += new ReturnEventHandler<Type>(Page_Return);
			ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(leukemiaLymphomaMarkersPage);
		}

		private void ShowLeukemiaLymphomaResultPage()
		{
			LeukemiaLymphomaResultPage leukemiaLymphomaResultPage = new LeukemiaLymphomaResultPage(this.m_FlowAccessionCollection, this.m_FlowCommentCollection);
			leukemiaLymphomaResultPage.Return += new ReturnEventHandler<Type>(Page_Return);
			ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(leukemiaLymphomaResultPage);
		}

		private void ShowCaseFinalPage()
		{
			CaseFinalPage caseFinalPage = new CaseFinalPage(this.m_FlowAccessionCollection);
			caseFinalPage.Return += new ReturnEventHandler<Type>(Page_Return);
			ApplicationNavigator.ApplicationContentFrame.NavigationService.Navigate(caseFinalPage);
		}

		private void Show()
		{
		}

        private void HyperlinkSave_Click(object sender, RoutedEventArgs e)
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

		public void Save()
		{
            Save(this.m_FlowAccessionCollection);
		}

		public static bool Save(YellowstonePathology.YpiConnect.Contract.Flow.FlowAccessionCollection flowAccessionCollection)
		{
			bool result = true;
			YellowstonePathology.YpiConnect.Contract.Flow.FlowAccessionSubmitter submitter = new Contract.Flow.FlowAccessionSubmitter();
			submitter.BeginSubmit(flowAccessionCollection, Business.Domain.Persistence.PropertyReaderFilterEnum.External);
			if (submitter.HasChanges() == true)
			{
				Mouse.OverrideCursor = Cursors.Wait;
				YellowstonePathology.YpiConnect.Proxy.FlowSignoutServiceProxy proxy = new Proxy.FlowSignoutServiceProxy();
				YellowstonePathology.Business.Rules.MethodResult methodResult = proxy.SubmitChanges(submitter);
				if (methodResult.Success == true)
				{
					submitter.EndSubmit();
				}
				else
				{
					result = false;
					MessageBox.Show(methodResult.Message);
				}
				Mouse.OverrideCursor = null;
			}
			return result;
		}

		public YellowstonePathology.YpiConnect.Contract.Flow.FlowAccessionCollection FlowAccessionCollection
		{
			get { return this.m_FlowAccessionCollection; }
		}

		public YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccountCollection WebServiceAccountCollection
		{
			get { return this.m_WebServiceAccountCollection; }
		}

		private void ButtonAssign_Click(object sender, RoutedEventArgs e)
		{
			int currentPathologistId = this.m_FlowAccessionCollection[0].PanelSetOrderCollection[0].AssignedToId;
			int assignToId = YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount.SystemUserId;
			
			if(currentPathologistId == 0)
			{
				this.m_FlowAccessionCollection[0].PanelSetOrderCollection[0].AssignedToId = assignToId;
			}
			else if(currentPathologistId != assignToId)
			{
				MessageBoxResult result = MessageBox.Show("This case is already assigned.  Are you sure you want to take ownership?", "Currently assigned", MessageBoxButton.YesNo, MessageBoxImage.Question);
				if (result == MessageBoxResult.Yes)
				{
					this.m_FlowAccessionCollection[0].PanelSetOrderCollection[0].AssignedToId = assignToId;
				}
			}
		}

		public void UpdateBindingSources()
		{
		}
	}
}
