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
using System.ComponentModel;
using System.Xml;
using System.ServiceModel;

namespace YellowstonePathology.UI.Cytology
{
	public delegate void CytologyReportNoChangeEventHandler(object sender, EventArgs e);    

	public partial class CytologyResultsWorkspace : UserControl
	{		
		public CommandBinding CommandBindingToggleAccessionLockMode;

		private CytologyUI m_CytologyUI;
		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        private YellowstonePathology.UI.PageNavigationWindow m_PageNavigationWindow;

		public CytologyResultsWorkspace()
		{
			this.m_SystemIdentity = new YellowstonePathology.Business.User.SystemIdentity(YellowstonePathology.Business.User.SystemIdentityTypeEnum.CurrentlyLoggedIn);
			this.m_CytologyUI = new CytologyUI(this.m_SystemIdentity);
            this.DataContext = this.m_CytologyUI;

            InitializeComponent();

			this.TextBoxReportNoSearch.IsEnabled = false;
			this.CommandBindingToggleAccessionLockMode = new CommandBinding(MainWindow.ToggleAccessionLockModeCommand, AlterAccessionLock, CanAlterAccessionLock);
			this.CommandBindings.Add(this.CommandBindingToggleAccessionLockMode);                        
		}        

		public CytologyResultsWorkspace(CytologyUI cytologyUI)
		{
			this.m_SystemIdentity = new YellowstonePathology.Business.User.SystemIdentity(YellowstonePathology.Business.User.SystemIdentityTypeEnum.CurrentlyLoggedIn);
			this.m_CytologyUI = cytologyUI;
            this.DataContext = this.m_CytologyUI;

            InitializeComponent();

			this.TextBoxReportNoSearch.Focus();
			this.CommandBindingToggleAccessionLockMode = new CommandBinding(MainWindow.ToggleAccessionLockModeCommand, AlterAccessionLock, CanAlterAccessionLock);
			this.CommandBindings.Add(this.CommandBindingToggleAccessionLockMode);                    
		}        

		public string ReportNo
		{
			get
			{
				if(this.m_CytologyUI.AccessionOrder != null && this.m_CytologyUI.PanelSetOrderCytology != null)
				{
					return this.m_CytologyUI.PanelSetOrderCytology.ReportNo;
				}
				return string.Empty;
			}
			set
			{
				this.TextBoxReportNoSearch.Text = value;				
			}
		}

		public string MasterAccessionNo
		{
			get
			{
				if (this.m_CytologyUI.AccessionOrder != null)
				{
					return this.m_CytologyUI.AccessionOrder.MasterAccessionNo;
				}
				return null;
			}
		}

		public YellowstonePathology.UI.Cytology.CytologyUI CytologyUI
		{
			get { return this.m_CytologyUI; }
		}        

		public void Save(object target, ExecutedRoutedEventArgs args)
		{			
			this.m_CytologyUI.Save();
		}

        public void SelectAppropriatePanel()
        {
            YellowstonePathology.Business.Test.PanelSetOrderCytology panelSetOrderCytology = (YellowstonePathology.Business.Test.PanelSetOrderCytology)this.m_CytologyUI.AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(15);
			if (this.m_SystemIdentity.User.IsUserInRole(Business.User.SystemUserRoleDescriptionEnum.Pathologist) == true)
            {                
                if (panelSetOrderCytology.PanelOrderCollection.HasPathologistReview() == true)
                {
                    int index = 0;
                    YellowstonePathology.Business.Test.PanelOrder pathologistReviewPanel = panelSetOrderCytology.PanelOrderCollection.GetPathologistReview();
                    foreach (YellowstonePathology.Business.Test.PanelOrder panelOrder in panelSetOrderCytology.PanelOrderCollection)
                    {
                        if (panelOrder.PanelOrderId == pathologistReviewPanel.PanelOrderId)
                        {
                            this.ListBoxResults.SelectedIndex = index;
                        }
                        index += 1;
                    }
                }
            }
			else if (this.m_SystemIdentity.User.IsUserInRole(Business.User.SystemUserRoleDescriptionEnum.Cytotech) == true)
            {
                int index = 0;
                YellowstonePathology.Business.Test.PanelOrder cytotechScreeningPanel = panelSetOrderCytology.PanelOrderCollection.GetPrimaryScreening();
                foreach (YellowstonePathology.Business.Test.PanelOrder panelOrder in panelSetOrderCytology.PanelOrderCollection)
                {
                    if (panelOrder.PanelOrderId == cytotechScreeningPanel.PanelOrderId)
                    {
                        this.ListBoxResults.SelectedIndex = index;
                    }
                    index += 1;
                }
            }
            else
            {
                this.ListBoxResults.SelectedIndex = -1;
            }
        }				

		public void ClearLock()
		{
			this.m_CytologyUI.ClearLock();
		}

		public void SetFocus()
		{
			this.TextBoxReportNoSearch.Focus();
		}

		private void ButtonShowScreeningImpression_Click(object sender, RoutedEventArgs e)
		{
			Button button = (Button)sender;
			YellowstonePathology.UI.Cytology.ScreeningImpressionSelection screeningImpressionSelection = (YellowstonePathology.UI.Cytology.ScreeningImpressionSelection)button.Tag;
            if (screeningImpressionSelection.ScreeningImpressionVisibility == Visibility.Visible)
			{
                screeningImpressionSelection.ScreeningImpressionVisibility = Visibility.Collapsed;
			}
			else
			{
                screeningImpressionSelection.ScreeningImpressionVisibility = Visibility.Visible;
			}
		}

		private void ButtonShowSpecimenAdequacy_Click(object sender, RoutedEventArgs e)
		{
			Button button = (Button)sender;
			YellowstonePathology.UI.Cytology.SpecimenAdequacySelection specimenAdequacySelection = (YellowstonePathology.UI.Cytology.SpecimenAdequacySelection)button.Tag;
			if (specimenAdequacySelection.SpecimenAdequacyVisibility == Visibility.Visible)
			{
				specimenAdequacySelection.SpecimenAdequacyVisibility = Visibility.Collapsed;
			}
			else
			{
				specimenAdequacySelection.SpecimenAdequacyVisibility = Visibility.Visible;
			}
		}

		private void ButtonShowOtherCondition_Click(object sender, RoutedEventArgs e)
		{
			Button button = (Button)sender;
			YellowstonePathology.UI.Cytology.OtherConditionsSelection otherConditionsSelection = (YellowstonePathology.UI.Cytology.OtherConditionsSelection)button.Tag;
			if (otherConditionsSelection.OtherConditionsVisibility == Visibility.Visible)
			{
				otherConditionsSelection.OtherConditionsVisibility = Visibility.Collapsed;
			}
			else
			{
				otherConditionsSelection.OtherConditionsVisibility = Visibility.Visible;
			}
		}

		public void ListViewItem_MouseDoubleClick(object sender, RoutedEventArgs e)
		{
			if (this.ListViewPatientHistory.SelectedItem != null)
			{
                YellowstonePathology.Business.Domain.PatientHistoryResult patientHistoryResult = (YellowstonePathology.Business.Domain.PatientHistoryResult)this.ListViewPatientHistory.SelectedItem;
				this.m_CytologyUI.ShowHistoryReport(patientHistoryResult.ReportNo);
			}
		}		

		private void BorderPanelSetOrderHeader_Loaded(object sender, RoutedEventArgs e)
		{
			Border border = sender as Border;
			ContentPresenter contentPresenter = border.TemplatedParent as ContentPresenter;
			contentPresenter.HorizontalAlignment = HorizontalAlignment.Stretch;
		}		

		private void TextBoxReportNoSearch_KeyUp(object sender, KeyEventArgs e)
		{
			Surgical.TextSearchHandler textSearchHandler = new Surgical.TextSearchHandler(this.TextBoxReportNoSearch.Text.ToUpper());
			object textSearchObject = textSearchHandler.GetSearchObject();
			string searchText = string.Empty;
			if (e.Key == Key.Enter)
			{
				searchText = this.TextBoxReportNoSearch.Text.ToUpper();
				if (textSearchObject is YellowstonePathology.Business.ReportNo)
				{
					searchText = ((YellowstonePathology.Business.ReportNo)textSearchObject).Value;
				}
				else if (textSearchObject is YellowstonePathology.Business.MasterAccessionNo)
				{
					searchText = ((YellowstonePathology.Business.MasterAccessionNo)textSearchObject).Value;
					YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(searchText);
					searchText = orderIdParser.CreateCyotlogyReportNoFromMasterAccessionNo();
				}
			}
			if (e.Key == Key.Up)
			{
				if (textSearchObject is YellowstonePathology.Business.ReportNo)
				{
					searchText = YellowstonePathology.Business.OrderIdParser.IncrementReportNo(this.TextBoxReportNoSearch.Text, 1);
				}
			}
			if (e.Key == Key.Down)
			{
				if (textSearchObject is YellowstonePathology.Business.ReportNo)
				{
					searchText = YellowstonePathology.Business.OrderIdParser.IncrementReportNo(this.TextBoxReportNoSearch.Text, -1);
				}
			}

			if (string.IsNullOrEmpty(searchText) == false)
			{
				YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(searchText);
				if (orderIdParser.IsValidCytologyReportNo == true)
				{
					this.m_CytologyUI.SetAccessionOrderByReportNo(searchText);
				}
				else System.Windows.MessageBox.Show(searchText + " is not a valid Cytology report number.");
			}
		}

		private void ButtonShowReportComment_Click(object sender, RoutedEventArgs e)
		{
			Button button = (Button)sender;
			YellowstonePathology.UI.Cytology.ReportCommentSelection reportCommentSelection = (YellowstonePathology.UI.Cytology.ReportCommentSelection)button.Tag;
			if (reportCommentSelection.ReportCommentVisibility == Visibility.Visible)
			{
				reportCommentSelection.ReportCommentVisibility = Visibility.Collapsed;
			}
			else
			{
				reportCommentSelection.ReportCommentVisibility = Visibility.Visible;
			}
		}

		public void AlterAccessionLock(object target, ExecutedRoutedEventArgs args)
		{
			this.m_CytologyUI.AlterAccessionLock();
		}

		public void CanAlterAccessionLock(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = this.m_CytologyUI.CanAlterAccessionLock();
		}

		public void CloseWorkspace(object target, ExecutedRoutedEventArgs args)
		{
			this.m_CytologyUI.Save();
			this.m_CytologyUI.ClearLock();
		}

		private void TextBoxReportNoSearch_GotFocus(object sender, RoutedEventArgs e)
		{
			if (this.TextBoxReportNoSearch.Text.Length > 0)
			{
				this.TextBoxReportNoSearch.SelectionStart = TextBoxReportNoSearch.Text.Length - 1;
			}
		}		

		private void ButtonDotReviewDelete_Click(object sender, RoutedEventArgs e)
		{
			MessageBoxResult result = System.Windows.MessageBox.Show("Delete selected Dot Review?", "Delete?", MessageBoxButton.OKCancel);
			if (result == MessageBoxResult.OK)
			{
				YellowstonePathology.Business.Rules.ExecutionStatus executionStatus = new YellowstonePathology.Business.Rules.ExecutionStatus();
				YellowstonePathology.Business.Interface.IPanelOrder panelOrderToDelete = (YellowstonePathology.Business.Interface.IPanelOrder)this.ListBoxResults.SelectedItem;
				this.m_CytologyUI.DeletePanelOrder(panelOrderToDelete, executionStatus);
			}
		}

		private void ButtonAcidWashDelete_Click(object sender, RoutedEventArgs e)
		{
			MessageBoxResult result = System.Windows.MessageBox.Show("Delete selected Acid wash?", "Delete?", MessageBoxButton.OKCancel);
			if (result == MessageBoxResult.OK)
			{
				YellowstonePathology.Business.Rules.ExecutionStatus executionStatus = new YellowstonePathology.Business.Rules.ExecutionStatus();
				YellowstonePathology.Business.Interface.IPanelOrder panelOrderToDelete = (YellowstonePathology.Business.Interface.IPanelOrder)this.ListBoxResults.SelectedItem;
				this.m_CytologyUI.DeletePanelOrder(panelOrderToDelete, executionStatus);
			}
		}

		private void ButtonAgree_Click(object sender, RoutedEventArgs e)
		{
			if (this.ListBoxResults.SelectedItem != null)
			{
                Type objectType = this.ListBoxResults.SelectedItem.GetType();
                if (typeof(YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology).IsAssignableFrom(objectType) == true)
                {				
					YellowstonePathology.Business.Rules.ExecutionStatus executionStatus = new YellowstonePathology.Business.Rules.ExecutionStatus();
					YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology cytologyPanelOrder = (YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology)this.ListBoxResults.SelectedItem;
					this.m_CytologyUI.SetResultToAgree(cytologyPanelOrder, executionStatus);
					if (executionStatus.Halted == true && executionStatus.ShowMessage == true)
					{
						System.Windows.MessageBox.Show(executionStatus.ExecutionMessagesString);
					}
				}
			}
		}

		private void ButtonScreeningFinal_Click(object sender, RoutedEventArgs e)
		{
			if (this.ListBoxResults.SelectedItem != null)
			{
                Type objectType = this.ListBoxResults.SelectedItem.GetType();
                if (typeof(YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology).IsAssignableFrom(objectType) == true)
                {
					YellowstonePathology.Business.Rules.ExecutionStatus executionStatus = new YellowstonePathology.Business.Rules.ExecutionStatus();
					YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology cytologyPanelOrder = (YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology)this.ListBoxResults.SelectedItem;
					this.m_CytologyUI.ScreeningFinal(cytologyPanelOrder, executionStatus);					
					try
					{
						YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology panelOrder = (YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology)this.ListBoxResults.SelectedItem;
						panelOrder.NotifyPropertyChanged("");
					}
					catch { }

					if (executionStatus.Halted == true && executionStatus.ShowMessage == true)
					{
						System.Windows.MessageBox.Show(executionStatus.ExecutionMessagesString);
					}
				}
			}
		}        

		private void ButtonAddCytotechReview_Click(object sender, RoutedEventArgs e)
		{
			if (this.ListBoxResults.SelectedItem != null)
			{
				Type objectType = this.ListBoxResults.SelectedItem.GetType();
                if (typeof(YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology).IsAssignableFrom(objectType) == true)
                {				
					this.m_CytologyUI.AddPeerReview("Cytotech Review", (YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology)this.ListBoxResults.SelectedItem);
				}
			}
		}

		private void ButtonAddPathologistReview_Click(object sender, RoutedEventArgs e)
		{
            //YellowstonePathology.Business.User.SystemIdentity systemIdentity = new YellowstonePathology.Business.User.SystemIdentity(YellowstonePathology.Business.User.SystemIdentityTypeEnum.CurrentlyLoggedIn);
            //YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapScreeningPanel thinPrepPapScreeningPanel = new YellowstonePathology.Business.Test.ThinPrepPap.ThinPrepPapScreeningPanel();
            //thinPrepPapScreeningPanel.ScreeningType = "Primary Screening";

            //string panelOrderId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            //YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology panelOrderCytology = new YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology(this.m_CytologyUI.PanelSetOrderCytology.ReportNo, panelOrderId, panelOrderId, thinPrepPapScreeningPanel, systemIdentity.User.UserId);            
            //this.m_CytologyUI.PanelSetOrderCytology.PanelOrderCollection.Add(panelOrderCytology);

            //return;

			if (this.ListBoxResults.SelectedItem != null)
			{
                Type objectType = this.ListBoxResults.SelectedItem.GetType();
                if (typeof(YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology).IsAssignableFrom(objectType) == true)
                {
					this.m_CytologyUI.AddPeerReview("Pathologist Review", (YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology)this.ListBoxResults.SelectedItem);
				}
			}
		}

		private void ButtonFinalSatNegECC_Click(object sender, RoutedEventArgs e)
		{
			if (this.ListBoxResults.SelectedItem != null)
			{
                Type objectType = this.ListBoxResults.SelectedItem.GetType();
                if (typeof(YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology).IsAssignableFrom(objectType) == true)
                {
					YellowstonePathology.Business.Rules.ExecutionStatus executionStatus = new YellowstonePathology.Business.Rules.ExecutionStatus();
					YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology selectedPanelOrder = (YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology)this.ListBoxResults.SelectedItem;
					this.m_CytologyUI.SetResult("51101", selectedPanelOrder, executionStatus);
					if (executionStatus.Halted == false)
					{
						this.m_CytologyUI.ScreeningFinal(selectedPanelOrder, executionStatus);
					}
					if (executionStatus.Halted == true && executionStatus.ShowMessage == true)
					{
						System.Windows.MessageBox.Show(executionStatus.ExecutionMessagesString);
					}
				}
			}
		}

		private void ButtonFinalSatNegNoECC_Click(object sender, RoutedEventArgs e)
		{
			if (this.ListBoxResults.SelectedItem != null)
			{
                Type objectType = this.ListBoxResults.SelectedItem.GetType();
                if (typeof(YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology).IsAssignableFrom(objectType) == true)
                {
					YellowstonePathology.Business.Rules.ExecutionStatus executionStatus = new YellowstonePathology.Business.Rules.ExecutionStatus();
					YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology selectedPanelOrder = (YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology)this.ListBoxResults.SelectedItem;
					this.m_CytologyUI.SetResult("51001", selectedPanelOrder, executionStatus);
					if (executionStatus.Halted == false)
					{
						this.m_CytologyUI.ScreeningFinal(selectedPanelOrder, executionStatus);
					}
					if (executionStatus.Halted == true && executionStatus.ShowMessage == true)
					{
						System.Windows.MessageBox.Show(executionStatus.ExecutionMessagesString);
					}
				}
			}
		}

		private void ButtonFinalSatNeg_Click(object sender, RoutedEventArgs e)
		{
			if (this.ListBoxResults.SelectedItem != null)
			{
                Type objectType = this.ListBoxResults.SelectedItem.GetType();
                if (typeof(YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology).IsAssignableFrom(objectType) == true)
                {
					YellowstonePathology.Business.Rules.ExecutionStatus executionStatus = new YellowstonePathology.Business.Rules.ExecutionStatus();
					YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology selectedPanelOrder = (YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology)this.ListBoxResults.SelectedItem;
					this.m_CytologyUI.SetResult("51501", selectedPanelOrder, executionStatus);

					if (executionStatus.Halted == false)
					{
						this.m_CytologyUI.ScreeningFinal(selectedPanelOrder, executionStatus);
					}
					if (executionStatus.Halted == true && executionStatus.ShowMessage == true)
					{
						System.Windows.MessageBox.Show(executionStatus.ExecutionMessagesString);
					}
				}
			}
		}

		private void ButtonFinalSatReactive_Click(object sender, RoutedEventArgs e)
		{
			if (this.ListBoxResults.SelectedItem != null)
			{
                Type selectedObjectType = this.ListBoxResults.SelectedItem.GetType();
                if (typeof(YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology).IsAssignableFrom(selectedObjectType) == true)
                {
                    YellowstonePathology.Business.Rules.ExecutionStatus executionStatus = new YellowstonePathology.Business.Rules.ExecutionStatus();
                    YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology selectedPanelOrder = (YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology)this.ListBoxResults.SelectedItem;
                    this.m_CytologyUI.SetResult("51102", selectedPanelOrder, executionStatus);
                    if (executionStatus.Halted == false)
                    {
                        this.m_CytologyUI.ScreeningFinal(selectedPanelOrder, executionStatus);                        
                    }
                    if (executionStatus.Halted == true && executionStatus.ShowMessage == true)
                    {
                        System.Windows.MessageBox.Show(executionStatus.ExecutionMessagesString);
                    }
                }				
			}
		}

		private void ButtonSetSatNegECC_Click(object sender, RoutedEventArgs e)
		{
			if (this.ListBoxResults.SelectedItem != null)
			{
                Type objectType = this.ListBoxResults.SelectedItem.GetType();
                if (typeof(YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology).IsAssignableFrom(objectType) == true)
                {
					YellowstonePathology.Business.Rules.ExecutionStatus executionStatus = new YellowstonePathology.Business.Rules.ExecutionStatus();
					YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology selectedPanelOrder = (YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology)this.ListBoxResults.SelectedItem;
					this.m_CytologyUI.SetResult("51101", selectedPanelOrder, executionStatus);
					if (executionStatus.Halted == true && executionStatus.ShowMessage == true)
					{
						System.Windows.MessageBox.Show(executionStatus.ExecutionMessagesString);
					}
				}
			}
		}

		private void ButtonSetSatNegNoECC_Click(object sender, RoutedEventArgs e)
		{
			if (this.ListBoxResults.SelectedItem != null)
			{
                Type objectType = this.ListBoxResults.SelectedItem.GetType();
                if (typeof(YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology).IsAssignableFrom(objectType) == true)
                {
					YellowstonePathology.Business.Rules.ExecutionStatus executionStatus = new YellowstonePathology.Business.Rules.ExecutionStatus();
					YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology selectedPanelOrder = (YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology)this.ListBoxResults.SelectedItem;
					this.m_CytologyUI.SetResult("51001", selectedPanelOrder, executionStatus);
					if (executionStatus.Halted == true && executionStatus.ShowMessage == true)
					{
						System.Windows.MessageBox.Show(executionStatus.ExecutionMessagesString);
					}
				}
			}
		}

		private void ButtonSetSatNeg_Click(object sender, RoutedEventArgs e)
		{
			if (this.ListBoxResults.SelectedItem != null)
			{
                Type objectType = this.ListBoxResults.SelectedItem.GetType();
                if (typeof(YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology).IsAssignableFrom(objectType) == true)
                {
					YellowstonePathology.Business.Rules.ExecutionStatus executionStatus = new YellowstonePathology.Business.Rules.ExecutionStatus();
					YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology selectedPanelOrder = (YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology)this.ListBoxResults.SelectedItem;
					this.m_CytologyUI.SetResult("51501", selectedPanelOrder, executionStatus);
					if (executionStatus.Halted == true && executionStatus.ShowMessage == true)
					{
						System.Windows.MessageBox.Show(executionStatus.ExecutionMessagesString);
					}
				}
			}
		}

		private void ButtonSetSatReactive_Click(object sender, RoutedEventArgs e)
		{
			if (this.ListBoxResults.SelectedItem != null)
			{
                Type objectType = this.ListBoxResults.SelectedItem.GetType();
                if (typeof(YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology).IsAssignableFrom(objectType) == true)
                {
					YellowstonePathology.Business.Rules.ExecutionStatus executionStatus = new YellowstonePathology.Business.Rules.ExecutionStatus();
					YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology selectedPanelOrder = (YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology)this.ListBoxResults.SelectedItem;
					this.m_CytologyUI.SetResult("51102", selectedPanelOrder, executionStatus);
					if (executionStatus.Halted == true && executionStatus.ShowMessage == true)
					{
						System.Windows.MessageBox.Show(executionStatus.ExecutionMessagesString);
					}
				}
			}
		}

		private void ButtonAddDotReview_Click(object sender, RoutedEventArgs e)
		{
			if (this.ListBoxResults.SelectedItem != null)
			{
                Type objectType = this.ListBoxResults.SelectedItem.GetType();
                if (typeof(YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology).IsAssignableFrom(objectType) == true)
                {
					DotReviewCommentDialog dlg = new DotReviewCommentDialog();
					dlg.ShowDialog();
					if (dlg.DialogResult.HasValue && dlg.DialogResult.Value)
					{
						this.m_CytologyUI.OrderDotReviewPanelOrder(dlg.Comment, (YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology)this.ListBoxResults.SelectedItem);
					}
				}
			}
		}

		private void ButtonAddAcidWash_Click(object sender, RoutedEventArgs e)
		{
			if (this.ListBoxResults.SelectedItem != null)
			{
                Type objectType = this.ListBoxResults.SelectedItem.GetType();
                if (typeof(YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology).IsAssignableFrom(objectType) == true)
                {
					YellowstonePathology.Business.Rules.ExecutionStatus executionStatus = new YellowstonePathology.Business.Rules.ExecutionStatus();
					YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology selectedCytologyPanelOrder = (YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology)this.ListBoxResults.SelectedItem;
					this.m_CytologyUI.AddAcidWashPanelOrder(selectedCytologyPanelOrder, executionStatus);
					if (executionStatus.Halted == true)
					{
						MessageBox.Show(executionStatus.ExecutionMessagesString);
					}
				}
			}
		}

		private void ButtonClearCase_Click(object sender, RoutedEventArgs e)
		{
			if (this.ListBoxResults.SelectedItem != null)
			{                
                if(this.m_CytologyUI.PanelSetOrderCytology.Final == false)
                {
                    Type objectType = this.ListBoxResults.SelectedItem.GetType();
                    if (typeof(YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology).IsAssignableFrom(objectType) == true)
                    {
                        YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology panelOrderCytology = (YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology)this.ListBoxResults.SelectedItem;
                        if (panelOrderCytology.Accepted == false)
                        {
                            YellowstonePathology.Business.Rules.ExecutionStatus executionStatus = new YellowstonePathology.Business.Rules.ExecutionStatus();
                            this.m_CytologyUI.ClearCase(panelOrderCytology, executionStatus);
                            if (executionStatus.Halted == true)
                            {
                                MessageBox.Show(executionStatus.ExecutionMessagesString);
                            }
                        }
                        else
                        {
                            MessageBox.Show("You canno clear this screening because it is final.");
                        }
				    }
                }
                else
                {
                    MessageBox.Show("You cannot clear this case because it is final.");
                }
			}
		}

		private void ButtonUnFinalScreening_Click(object sender, RoutedEventArgs e)
		{
			if (this.ListBoxResults.SelectedItem != null)
			{
                Type objectType = this.ListBoxResults.SelectedItem.GetType();
                if (typeof(YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology).IsAssignableFrom(objectType) == true)
                {
					YellowstonePathology.Business.Rules.ExecutionStatus executionStatus = new YellowstonePathology.Business.Rules.ExecutionStatus();
					this.m_CytologyUI.ScreeningUnfinal((YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology)this.ListBoxResults.SelectedItem, executionStatus);
					if (executionStatus.Halted == true)
					{
						MessageBox.Show(executionStatus.ExecutionMessagesString);
					}
				}
			}
		}

		private void ButtonDeleteItem_Click(object sender, RoutedEventArgs e)
		{
			if (this.ListBoxResults.SelectedItem != null)
			{
				YellowstonePathology.Business.Rules.ExecutionStatus executionStatus = new YellowstonePathology.Business.Rules.ExecutionStatus();
				YellowstonePathology.Business.Interface.IPanelOrder panelOrderToDelete = (YellowstonePathology.Business.Interface.IPanelOrder)this.ListBoxResults.SelectedItem;
				this.m_CytologyUI.DeletePanelOrder(panelOrderToDelete, executionStatus);
				if (executionStatus.Halted == true)
				{
					MessageBox.Show(executionStatus.ExecutionMessagesString);
				}
			}
		}

		public bool BarcodeScanReceived(YellowstonePathology.Business.BarcodeScanning.BarcodeScan barCodeItem)
		{            
			bool returnValue = false;            
			return returnValue;
		}

        private void ContextMenuPublish_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_CytologyUI.AccessionOrder != null)
            {
				string reportNo = this.m_CytologyUI.PanelSetOrderCytology.ReportNo;
				int panelSetId = this.m_CytologyUI.PanelSetOrderCytology.PanelSetId;
				string masterAccessionNo = this.m_CytologyUI.AccessionOrder.MasterAccessionNo;

                YellowstonePathology.Business.Interface.ICaseDocument caseDocument = YellowstonePathology.Business.Document.DocumentFactory.GetDocument(panelSetId);
                caseDocument.Render(masterAccessionNo, reportNo, YellowstonePathology.Business.Document.ReportSaveModeEnum.Normal);
                caseDocument.Publish();                
                MessageBox.Show("The document has been published");
            }
        }

		private void ButtonWomensHealthProfile_Click(object sender, RoutedEventArgs e)
        {
			if (this.m_CytologyUI.AccessionOrder.PanelSetOrderCollection.HasWomensHealthProfileOrder() == true)
            {
                YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder = null;
                YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection clientOrders = YellowstonePathology.Business.Gateway.ClientOrderGateway.GetClientOrdersByMasterAccessionNo(this.m_CytologyUI.AccessionOrder.MasterAccessionNo);

                if (clientOrders.Count > 0)
                {
                    clientOrder = clientOrders[0];
                }

                this.m_PageNavigationWindow = new PageNavigationWindow(this.m_SystemIdentity);
				YellowstonePathology.UI.Login.WomensHealthProfilePath womensHealthProfilePath = new YellowstonePathology.UI.Login.WomensHealthProfilePath(this.m_CytologyUI.AccessionOrder, this.m_CytologyUI.ObjectTracker, clientOrder, this.m_PageNavigationWindow.PageNavigator, this.m_SystemIdentity);
                womensHealthProfilePath.Finished += new Login.WomensHealthProfilePath.FinishedEventHandler(WomensHealthProfilePath_Finished);
				womensHealthProfilePath.Start();
                this.m_PageNavigationWindow.ShowDialog();
            }
            else
            {
				MessageBox.Show("A Womens Health Profile has not been ordered.");
            }
        }

        private void WomensHealthProfilePath_Finished(object sender, EventArgs e)
        {
            this.m_PageNavigationWindow.Close();
        }

		public void SetReportNo(string reportNo)
		{
			this.TextBoxReportNoSearch.Text = reportNo;
		}
	}
}
