using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Windows.Controls;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using System.Windows.Xps.Serialization;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.UI.Test
{

    public partial class LabWorkspace : System.Windows.Controls.UserControl
    {        
        public CommandBinding CommandBindingApplicationClosing;
        public CommandBinding CommandBindingSaveChanges;        
        public CommandBinding CommandBindingShowCaseDocument;
        public CommandBinding CommandBindingToggleAccessionLockMode;        
        public CommandBinding CommandBindingShowOrderForm;		
		public CommandBinding CommandBindingPatientLinking;        
		public CommandBinding CommandBindingRemoveTab;		
		public CommandBinding CommandBindingShowPatientEditDialog;
        
        private YellowstonePathology.UI.AmendmentControlV2 m_AmendmentControl;
        private YellowstonePathology.UI.DocumentWorkspace m_DocumentViewer;
		private YellowstonePathology.UI.Common.TreeViewWorkspace m_TreeViewWorkspace;        
		
        private LabUI m_LabUI;

        private DateTime m_WorkDate = DateTime.Today;
		private YellowstonePathology.Business.Domain.Core.Comment m_Comment;

		YellowstonePathology.Business.Logging.ScanLogger m_ScanLogger;

		YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort m_BarcodeScanPort;

        private const string m_SystemIdentityUnknowMessage = "The current operation cannot be performed because the current system user is unknown.";
        private YellowstonePathology.UI.Test.ResultDialog m_ResultDialog;
        private MainWindowCommandButtonHandler m_MainWindowCommandButtonHandler;

        public LabWorkspace(MainWindowCommandButtonHandler mainWindowCommandButtonHandler)
        {
            this.m_MainWindowCommandButtonHandler = mainWindowCommandButtonHandler;
			this.m_SystemIdentity = new YellowstonePathology.Business.User.SystemIdentity(YellowstonePathology.Business.User.SystemIdentityTypeEnum.CurrentlyScannedIn);

            this.CommandBindingApplicationClosing = new CommandBinding(MainWindow.ApplicationClosingCommand, CloseWorkspace);
            this.CommandBindingSaveChanges = new CommandBinding(MainWindow.SaveChangesCommand, SaveData);                        
            this.CommandBindingShowCaseDocument = new CommandBinding(MainWindow.ShowCaseDocumentCommand, ShowCaseDocument);
			this.CommandBindingToggleAccessionLockMode = new CommandBinding(MainWindow.ToggleAccessionLockModeCommand, AlterAccessionLock, CanAlterAccessionLock);
			this.CommandBindingShowOrderForm = new CommandBinding(MainWindow.ShowOrderFormCommand, this.ShowOrderForm, ItemIsSelected);			
			this.CommandBindingPatientLinking = new CommandBinding(MainWindow.PatientLinkingCommand, this.LinkPatient, ItemIsSelected);            
			this.CommandBindingRemoveTab = new CommandBinding(MainWindow.RemoveTabCommand, RemoveTab);
			this.CommandBindingShowPatientEditDialog = new CommandBinding(MainWindow.ShowPatientEditDialogCommand, this.ShowPatientEditDialog);

            this.CommandBindings.Add(this.CommandBindingApplicationClosing);
            this.CommandBindings.Add(this.CommandBindingSaveChanges);           
            this.CommandBindings.Add(this.CommandBindingShowCaseDocument);
            this.CommandBindings.Add(this.CommandBindingToggleAccessionLockMode);
            this.CommandBindings.Add(this.CommandBindingShowOrderForm);            
            this.CommandBindings.Add(this.CommandBindingPatientLinking);            
			this.CommandBindings.Add(this.CommandBindingRemoveTab);			
			this.CommandBindings.Add(this.CommandBindingShowPatientEditDialog);
						
			this.m_LabUI = new LabUI(this.m_SystemIdentity);

			this.m_AmendmentControl = new AmendmentControlV2(this.m_SystemIdentity, string.Empty, this.m_LabUI.AccessionOrder, this.m_LabUI.ObjectTracker);
            this.m_DocumentViewer = new DocumentWorkspace();
			this.m_TreeViewWorkspace = new YellowstonePathology.UI.Common.TreeViewWorkspace(this.m_LabUI.AccessionOrder, this.m_LabUI.ObjectTracker, this.m_SystemIdentity);
            
            InitializeComponent();
            
			this.TabItemAmendment.Content = m_AmendmentControl;
			this.TabItemDocumentWorkspace.Content = this.m_DocumentViewer;
			this.TabItemTreeView.Content = this.m_TreeViewWorkspace;

			this.DataContext = this.m_LabUI;

			this.ComboBoxLogLocation.SelectionChanged -= this.ComboBoxLogLocation_SelectionChanged;
			this.ComboBoxLogLocation.SelectedIndex = 0;
			this.ComboBoxLogLocation.SelectionChanged += this.ComboBoxLogLocation_SelectionChanged;			

			this.m_LabUI.Lock.LockStatusChanged += new YellowstonePathology.Business.Domain.LockStatusChangedEventHandler(AccessionLock_LockStatusChanged);

            this.Loaded +=new RoutedEventHandler(LabWorkspace_Loaded);
            this.Unloaded += new RoutedEventHandler(LabWorkspace_Unloaded);

			this.m_BarcodeScanPort = YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort.Instance;

			this.m_ScanLogger = new YellowstonePathology.Business.Logging.ScanLogger(this.m_SystemIdentity);
            this.m_ScanLogger.Start();
			this.ListViewDocumentList.ItemsSource = this.m_LabUI.CaseDocumentCollection;
		}
        
        public void CloseWorkspace(object target, ExecutedRoutedEventArgs args)
        {                        
            this.Save();
			if (this.m_LabUI != null && m_LabUI.AccessionOrder != null && m_LabUI.PanelSetOrder != null)
			{
				this.m_LabUI.Lock.ReleaseLock();
			}            
		}

        private void MainWindowCommandButtonHandler_StartProviderDistributionPath(object sender, EventArgs e)
        {
            if (this.m_LabUI.AccessionOrder != null)
            {
                YellowstonePathology.UI.Login.FinalizeAccession.ProviderDistributionPath providerDistributionPath =
                    new YellowstonePathology.UI.Login.FinalizeAccession.ProviderDistributionPath(this.m_LabUI.PanelSetOrder.ReportNo, this.m_LabUI.AccessionOrder, this.m_LabUI.ObjectTracker,
                    System.Windows.Visibility.Collapsed, System.Windows.Visibility.Visible, System.Windows.Visibility.Collapsed);
                providerDistributionPath.Start();
            }
        }

        public void LabWorkspace_Unloaded(object sender, RoutedEventArgs e)
        {
            this.Save();
			this.m_SystemIdentity.UserChanged -= this.UserChangedHandler;						
			this.m_BarcodeScanPort.ClientScanReceived -= this.ClientScanReceived;
            this.m_MainWindowCommandButtonHandler.StartProviderDistributionPath -= MainWindowCommandButtonHandler_StartProviderDistributionPath;
		}

		public void RemoveTab(object target, ExecutedRoutedEventArgs args)
		{
			if (this.m_LabUI != null && m_LabUI.AccessionOrder != null && this.m_LabUI.PanelSetOrder != null)
			{
				this.m_LabUI.Lock.ReleaseLock();
			}
		}

        private void ShowOrderForm(object target, ExecutedRoutedEventArgs args)
        {
			this.Save();
			YellowstonePathology.UI.Common.OrderDialog frm = new YellowstonePathology.UI.Common.OrderDialog(this.m_LabUI.AccessionOrder, this.m_LabUI.PanelSetOrder, this.m_SystemIdentity);			
            frm.ShowDialog();
			this.GetAccessionOrder();			
		}

		private void ButtonOrder_Click(object sender, RoutedEventArgs e)
		{
            if (this.m_LabUI.PanelSetOrder.PanelSetId != 31)
            {
                if (this.m_SystemIdentity.IsKnown == true)
                {
                    this.Save();
					YellowstonePathology.UI.Common.OrderDialog dlg = new YellowstonePathology.UI.Common.OrderDialog(this.m_LabUI.AccessionOrder, this.m_LabUI.PanelSetOrder, this.m_SystemIdentity);
                    dlg.ShowDialog();

                    this.GetAccessionOrder();                    
                }
            }
            else
            {
                MessageBox.Show("Technical only orders cannot be placed in this window.");
            }
		}

		private void LabWorkspace_Loaded(object sender, RoutedEventArgs args)
        {            
			if (this.m_SystemIdentity.StationName == "BLGSCASSETTE" || this.m_SystemIdentity.StationName == "HISTOLOGYB")
			{				
				for (int idx = 0; idx < this.ComboBoxPanelSetType.Items.Count; idx++)
				{
                    if (((YellowstonePathology.Business.BatchTypeListItem)ComboBoxPanelSetType.Items[idx]).BatchTypeId == 8)
					{
						this.ComboBoxPanelSetType.SelectedIndex = idx;
						break;
					}
				}                
			}

			if (this.m_LabUI != null && this.m_LabUI.AccessionOrder != null && this.m_LabUI.PanelSetOrder != null)
			{
				if (!String.IsNullOrEmpty(this.m_LabUI.PanelSetOrder.ReportNo))
				{
					if (this.m_LabUI.Lock.LockingMode == Business.Domain.LockModeEnum.AlwaysAttemptLock && !this.m_LabUI.Lock.LockAquired)
					{
						this.m_LabUI.Lock.GetLock();
					}
				}
				((MainWindow)Application.Current.MainWindow).SetLockObject(this.m_LabUI.Lock);
			}

			this.m_SystemIdentity.UserChanged += new Business.User.SystemIdentity.UserChangedHandler(UserChangedHandler);			
			this.m_BarcodeScanPort.ClientScanReceived += ClientScanReceived;

            //Loaded is called twice so I am removing before I add.
            this.m_MainWindowCommandButtonHandler.StartProviderDistributionPath -= MainWindowCommandButtonHandler_StartProviderDistributionPath;
            this.m_MainWindowCommandButtonHandler.StartProviderDistributionPath += new MainWindowCommandButtonHandler.StartProviderDistributionPathEventHandler(MainWindowCommandButtonHandler_StartProviderDistributionPath);
		}

        public void AccessionLock_LockStatusChanged(object sender, EventArgs e)
        {            
			this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate()
			{
				((MainWindow)Application.Current.MainWindow).SetLockObject(this.m_LabUI.Lock);
			}
			));
		}        
		
		public void AlterAccessionLock(object target, ExecutedRoutedEventArgs args)
        {
			this.Save();
			if (this.ListViewCaseList.SelectedItem != null)
            {
				YellowstonePathology.Business.Search.ReportSearchItem item = (YellowstonePathology.Business.Search.ReportSearchItem)this.ListViewCaseList.SelectedItem;
				this.GetCase(item.ReportNo);
				this.m_LabUI.Lock.ToggleLockingMode();
				this.m_LabUI.Lock.SetLockable(this.m_LabUI.AccessionOrder);
                this.CheckEnabled();
                this.m_LabUI.NotifyPropertyChanged("");
            }			
		}

		private void CanAlterAccessionLock(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = false;
			if (((TabItem)this.Parent).IsSelected && this.m_LabUI != null && this.m_LabUI.PanelSetOrder != null && this.m_SystemIdentity.User.UserName != null)
			{
				e.CanExecute = true;
			}
		}

		private void ItemIsSelected(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = false;
			if (((TabItem)this.Parent).IsSelected && this.ListViewCaseList.SelectedItem != null && this.m_LabUI.Lock.LockAquired && this.m_SystemIdentity.User.UserName != null)
			{
				e.CanExecute = true;
			}
		}

		public void LinkPatient(object target, ExecutedRoutedEventArgs args)
		{
			this.ButtonLinkPatient_Click(null, null);
		}

        public void StartProviderDistributionPath(object target, ExecutedRoutedEventArgs args)
        {
            if (this.m_LabUI.PanelSetOrder != null)
            {
                YellowstonePathology.UI.Login.FinalizeAccession.ProviderDistributionPath providerDistributionPath = new Login.FinalizeAccession.ProviderDistributionPath(this.m_LabUI.PanelSetOrder.ReportNo, this.m_LabUI.AccessionOrder, this.m_LabUI.ObjectTracker,
                    System.Windows.Visibility.Collapsed, System.Windows.Visibility.Visible, System.Windows.Visibility.Collapsed);
                providerDistributionPath.Start();
            }
        }		

		public void ShowPatientEditDialog(object target, ExecutedRoutedEventArgs args)
		{			
			if (this.m_LabUI.Lock.LockAquired)
			{
				YellowstonePathology.UI.Common.PatientEditDialog patientEditDialog = new YellowstonePathology.UI.Common.PatientEditDialog(this.m_LabUI.AccessionOrder);
				patientEditDialog.ShowDialog();
			}
		}        

        public void Save()
        {
            ((MainWindow)Application.Current.MainWindow).MoveKeyboardInputToNext();
			this.m_LabUI.Save();
        }

        public void SaveData(object target, ExecutedRoutedEventArgs args)
        {
            this.Save();
            MessageBox.Show("The Lab Workspace has been saved.");
        }

        public void  ShowCaseDocument(object target, ExecutedRoutedEventArgs args)
        {
			string reportNo = string.Empty;
            int panelSetId = 0;
            
            if (this.m_LabUI.PanelSetOrder != null)
            {
                reportNo = this.m_LabUI.PanelSetOrder.ReportNo;
                panelSetId = this.m_LabUI.PanelSetOrder.PanelSetId;
            }
            else
            {
                MessageBox.Show("The current selection does not seem to have a Panel Set Order.");
                return;
            }

			this.GetCase(this.m_LabUI.PanelSetOrder.ReportNo);
			if (this.ListViewCaseList.SelectedItem != null)
            {
				if (this.m_LabUI.AccessionOrder.PanelSetOrderCollection.Count != 0)
                {
                    YellowstonePathology.UI.CaseDocumentViewer caseDocumentViewer = new CaseDocumentViewer();
                    caseDocumentViewer.View(this.m_LabUI.AccessionOrder.MasterAccessionNo, this.m_LabUI.PanelSetOrder.ReportNo, this.m_LabUI.PanelSetOrder.PanelSetId);
                }
                else
                {
                    MessageBox.Show("Not able to generate report");
                }
            }
        }		

		private void CheckEnabled()
		{
			if (this.m_LabUI != null)
			{
				this.m_LabUI.RunWorkspaceEnableRules();
			}
		}

		private void GetAccessionOrder()
		{
			this.m_LabUI.GetAccessionOrder(this.m_LabUI.PanelSetOrder.ReportNo);
			this.RefreshWorkspaces();
		}

		public void GetCase(string reportNo)
        {
            this.Save();

			this.m_LabUI.GetAccessionOrder(reportNo);			

            this.m_DocumentViewer.ClearContent();
			if (this.m_LabUI.CaseDocumentCollection.GetFirstRequisition() != null)
			{
				this.m_DocumentViewer.ShowDocument(this.m_LabUI.CaseDocumentCollection.GetFirstRequisition());
			}
					
			this.ListViewDocumentList.ItemsSource = this.m_LabUI.CaseDocumentCollection;
			this.RefreshWorkspaces();			
		}

		public void ListViewCaseList_SelectionChanged(object sender, RoutedEventArgs args)
        {
            if (this.ListViewCaseList.SelectedItem != null)
            {
				YellowstonePathology.Business.Search.ReportSearchItem item = (YellowstonePathology.Business.Search.ReportSearchItem)this.ListViewCaseList.SelectedItem;
				this.GetCase(item.ReportNo);
            }
        }

		private void RefreshWorkspaces()
		{
			this.m_TreeViewWorkspace = new Common.TreeViewWorkspace(this.m_LabUI.AccessionOrder, this.m_LabUI.ObjectTracker, this.m_SystemIdentity);
			this.TabItemTreeView.Content = this.m_TreeViewWorkspace;

			this.m_AmendmentControl = new AmendmentControlV2(this.m_SystemIdentity, this.m_LabUI.PanelSetOrder.ReportNo, this.m_LabUI.AccessionOrder, this.m_LabUI.ObjectTracker);
			this.TabItemAmendment.Content = this.m_AmendmentControl;			
		}

        public void ButtonGo_Click(object sender, RoutedEventArgs args)
        {
			this.m_LabUI.Save();
            this.m_LabUI.FillCaseList();
            SetListViewToTop();
        }

        public void ButtonFinalizeCase_Click(object sender, RoutedEventArgs args)
        {
			if (this.m_LabUI.PanelSetOrder != null && this.m_LabUI.PanelSetOrder.Final == false)
            {
				YellowstonePathology.Business.Rules.RuleExecutionStatus ruleExecutionStatus = new YellowstonePathology.Business.Rules.RuleExecutionStatus();
				YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = m_LabUI.PanelSetOrder;
				panelSetOrder.Finalize(this.m_LabUI.AccessionOrder, ruleExecutionStatus, this.m_SystemIdentity);

				if (ruleExecutionStatus.ExecutionHalted == true)
				{
					YellowstonePathology.UI.RuleExecutionStatusDialog dialog = new RuleExecutionStatusDialog(ruleExecutionStatus);
					dialog.ShowDialog();
					return;
				}
				this.Save();
				this.m_LabUI.FillCaseList();
			}            
        }

        public void ButtonAccessionLock_Click(object sender, RoutedEventArgs args)
        {
            this.Save();
			if (this.m_LabUI.PanelSetOrder != null && this.m_LabUI.PanelSetOrder.ReportNo != null)
            {
				this.m_LabUI.Lock.ToggleLockingMode();
            }        
        }

        public void MenuItemAcceptResults_Click(object sender, RoutedEventArgs args)
        {
			this.Save();
            Control menuItem = (Control)sender;
            YellowstonePathology.Business.Test.PanelOrder panelOrder = (YellowstonePathology.Business.Test.PanelOrder)menuItem.Tag;
            YellowstonePathology.Business.Rules.RuleExecutionStatus ruleExecutionStatus = new YellowstonePathology.Business.Rules.RuleExecutionStatus();
			panelOrder.AcceptResults(ruleExecutionStatus, this.m_LabUI.AccessionOrder, this.m_SystemIdentity.User);

            if (ruleExecutionStatus.ExecutionHalted == true)
            {
                YellowstonePathology.UI.RuleExecutionStatusDialog ruleExecutionStatusDialog = new RuleExecutionStatusDialog(ruleExecutionStatus);
                ruleExecutionStatusDialog.ShowDialog();
            }

            this.m_LabUI.Save();
			this.m_LabUI.GetAccessionOrder(this.m_LabUI.PanelSetOrder.ReportNo);
		}

		public void MenuItemUnacceptResults_Click(object sender, RoutedEventArgs args)
		{
			//TODO: put this in a rule.  It is here until #ifusedatasets is retired
			if (this.m_LabUI.PanelSetOrder.Final)
			{
				MessageBoxResult result = MessageBox.Show("The case is finaled, do you still want to unaccept the results?", "Case is Finaled", MessageBoxButton.YesNo, MessageBoxImage.Question);
				if (result == MessageBoxResult.No) return;
			}

			this.Save();
			Control menuItem = (Control)sender;
			string panelOrderItemId = ((YellowstonePathology.Business.Test.PanelOrder)menuItem.Tag).PanelOrderId;
			YellowstonePathology.Business.Test.PanelOrder panelOrder = (from pso in this.m_LabUI.AccessionOrder.PanelSetOrderCollection
																				from po in pso.PanelOrderCollection
																				where po.PanelOrderId == panelOrderItemId
																				select po).Single<YellowstonePathology.Business.Test.PanelOrder>();

			if (panelOrder.Accepted)
			{
				panelOrder.UnacceptResults();
				this.GetCase(this.m_LabUI.PanelSetOrder.ReportNo);
			}
		}

		private void ButtomAcceptERPRResults_Click(object sender, RoutedEventArgs e)
		{
			this.Save();
			YellowstonePathology.Business.Rules.RuleExecutionStatus ruleExecutionStatus = new YellowstonePathology.Business.Rules.RuleExecutionStatus();
			foreach (YellowstonePathology.Business.Test.PanelOrder panelOrder in this.m_LabUI.PanelSetOrder.PanelOrderCollection)
			{
				if (panelOrder.PanelId == 62)
				{
					panelOrder.AcceptResults(ruleExecutionStatus, this.m_LabUI.AccessionOrder, this.m_SystemIdentity.User);
					break;
				}
			}

			if (ruleExecutionStatus.ExecutionHalted == true)
			{
				YellowstonePathology.UI.RuleExecutionStatusDialog dialog = new RuleExecutionStatusDialog(ruleExecutionStatus);
				dialog.ShowDialog();
			}
			this.m_LabUI.Save();
			this.m_LabUI.GetAccessionOrder(this.m_LabUI.PanelSetOrder.ReportNo);
		}

		private void ButtomUnacceptERPRResults_Click(object sender, RoutedEventArgs e)
		{
			foreach (YellowstonePathology.Business.Test.PanelOrder panelOrder in this.m_LabUI.PanelSetOrder.PanelOrderCollection)
			{
				if (panelOrder.PanelId == 62)
				{
					panelOrder.UnacceptResults();
					break;
				}
			}
		}
				
        public void MenuItemMarkAsUnassigned_Click(object sender, RoutedEventArgs args)
        {
            Control menuItem = (Control)sender;
			YellowstonePathology.Business.Test.PanelOrder panelOrder = (from pso in this.m_LabUI.AccessionOrder.PanelSetOrderCollection
																				from po in pso.PanelOrderCollection
																				where po.PanelOrderId == ((YellowstonePathology.Business.Test.PanelOrder)menuItem.Tag).PanelOrderId
																				select po).Single<YellowstonePathology.Business.Test.PanelOrder>();
			panelOrder.MarkAsUnassigned();
			this.m_LabUI.Save();
			this.GetAccessionOrder();
		}        

		public void ListViewDocumentList_MouseLeftButtonUp(object sender, RoutedEventArgs args)
		{
			if (this.ListViewDocumentList.SelectedItem != null)
			{
				if (this.m_DocumentViewer.SyncDocuments == true)
				{
					this.RightTabControl.SelectedIndex = 1;
				}
				YellowstonePathology.Business.Document.CaseDocument item = (YellowstonePathology.Business.Document.CaseDocument)this.ListViewDocumentList.SelectedItem;
				this.m_DocumentViewer.ShowDocument(item);
			}
		}		

		public void MenuItemRefreshDocumentList_Click(object sender, RoutedEventArgs args)
		{
			this.Save();			
			this.m_LabUI.RefreshCaseDocumentCollection();
			this.ListViewDocumentList.ItemsSource = this.m_LabUI.CaseDocumentCollection;
		}        

        public void ButtonSearch_Click(object sender, RoutedEventArgs args)
        {            
			this.DoSearch();
		}

		private void Search_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				this.DoSearch();
			}
		}

		private void DoSearch()
		{
            if (radioButtonSearchAccessionNo.IsChecked == true)
            {
                if (this.textBoxSearchReportNo.Text.Length > 4)
                {
					this.m_LabUI.SearchEngine.SetFillByReportNo(this.textBoxSearchReportNo.Text);
					this.m_LabUI.FillCaseList();

                    this.ListViewCaseList.Focus();
					SetListViewToTop();
                }
				else
				{
					MessageBox.Show("This search option requires an Accession Number.",	"Enter an accession number", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				}
			}
            if (radioButtonSearchNotDistributed.IsChecked == true)
            {
				this.m_LabUI.SearchEngine.SetFillByNotDistributed(0);
				this.m_LabUI.FillCaseList();
				this.ListViewCaseList.Focus();
				SetListViewToTop();				
            }
			else if (radioButtonSearchNotFinal.IsChecked.Value)
			{				
				this.m_LabUI.SearchEngine.SetFillByNotFinal(0);
				this.m_LabUI.FillCaseList();
				this.ListViewCaseList.Focus();
				SetListViewToTop();				
			}
            else if (radioButtonSearchInHouseMolecularNotFinal.IsChecked.Value)
            {
                this.m_LabUI.SearchEngine.SetFillByInHouseMolecularPending();
                this.m_LabUI.FillCaseList();
                this.ListViewCaseList.Focus();
                SetListViewToTop();
            }
			else if (radioButtonSearchPatientName.IsChecked.Value)
			{
				if (textBoxSearchPatientName.Text.Length > 0)
				{					
					Surgical.TextSearchHandler textSearchHandler = new Surgical.TextSearchHandler(this.textBoxSearchPatientName.Text);
					object textSearchObject = textSearchHandler.GetSearchObject();
					if (textSearchObject is YellowstonePathology.Business.PatientName)
					{
						YellowstonePathology.Business.PatientName patientName = (YellowstonePathology.Business.PatientName)textSearchObject;
						this.m_LabUI.SearchEngine.SetFillByPatientName(patientName);
						this.m_LabUI.FillCaseList();
						ListViewCaseList.Focus();
						SetListViewToTop();				
					}
				}
				else
				{
					MessageBox.Show("This search option requires at least part of a patient name.",	"Enter Patient Name", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				}
			}			
            else if (this.radioButtonSearchThisMonth.IsChecked.Value == true)
            {
				this.m_LabUI.SearchEngine.SetFillByThisMonth(0);
				this.m_LabUI.FillCaseList();
				this.ListViewCaseList.Focus();
                this.SetListViewToTop();
            }
            else if (this.radioButtonSearchLastMonth.IsChecked.Value == true)
            {
				this.m_LabUI.SearchEngine.SetFillByLastMonth(0);
				this.m_LabUI.FillCaseList();
				this.ListViewCaseList.Focus();
                this.SetListViewToTop();
            }
			else if (this.radioButtonSearchToday.IsChecked.Value == true)
			{
				this.m_LabUI.SearchEngine.SetFillByToday(0);
				this.m_LabUI.FillCaseList();
				this.ListViewCaseList.Focus();
				this.SetListViewToTop();
			}
			else if (this.radioButtonSearchYesterday.IsChecked.Value == true)
			{
				this.m_LabUI.SearchEngine.SetFillByYesterday(0);
				this.m_LabUI.FillCaseList();
				this.ListViewCaseList.Focus();
				this.SetListViewToTop();
			}
			else if (this.radioButtonSearchNotAudited.IsChecked.Value == true)
			{
				if (this.comboBoxAuditType.SelectedItem != null)
				{
					string caseType = ((YellowstonePathology.Business.PanelSet.Model.PanelSetCaseType)this.comboBoxAuditType.SelectedItem).CaseType;
					this.m_LabUI.SearchEngine.SetFillByNotAudited(caseType);
					this.m_LabUI.FillCaseList();
					this.ListViewCaseList.Focus();
					this.SetListViewToTop();
				}
				else
				{
					MessageBox.Show("This search option requires a Case Type selection.",
						"Select a Case Type",
					MessageBoxButton.OK, MessageBoxImage.Exclamation);
				}
			}
			else if (this.radioButtonSearchMasterAccessionNo.IsChecked.Value == true)
			{
				if (textBoxSearchMasterAccessionNo.Text.Length > 0)
				{
					string masterAccessionNo = textBoxSearchMasterAccessionNo.Text;					
					this.m_LabUI.SearchEngine.SetFillByMasterAccessionNo(masterAccessionNo);
					this.m_LabUI.FillCaseList();
					ListViewCaseList.Focus();
					SetListViewToTop();
				}
				else
				{
					MessageBox.Show("This search option requires a Master Accession No.",
						"Enter Master Accession No",
					MessageBoxButton.OK, MessageBoxImage.Exclamation);
				}
			}
		}

		private void TextBoxSearch_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				if (this.TextBoxSearch.Text.Length >= 1)
				{
					Surgical.TextSearchHandler textSearchHandler = new Surgical.TextSearchHandler(this.TextBoxSearch.Text);
					object textSearchObject = textSearchHandler.GetSearchObject();
					if (textSearchObject is YellowstonePathology.Business.ReportNo)
					{
						YellowstonePathology.Business.ReportNo reportNo = (YellowstonePathology.Business.ReportNo)textSearchObject;
						this.m_LabUI.SearchEngine.SetFillByReportNo(reportNo.Value);
					}
					else if (textSearchObject is YellowstonePathology.Business.MasterAccessionNo)
					{
						YellowstonePathology.Business.MasterAccessionNo masterAccessionNo = (YellowstonePathology.Business.MasterAccessionNo)textSearchObject;
						this.m_LabUI.SearchEngine.SetFillByMasterAccessionNo(masterAccessionNo.Value);
					}
					else if (textSearchObject is YellowstonePathology.Business.PatientName)
					{
						YellowstonePathology.Business.PatientName patientName = (YellowstonePathology.Business.PatientName)textSearchObject;
						this.m_LabUI.SearchEngine.SetFillByPatientName(patientName);
					}
					this.m_LabUI.FillCaseList();
					ListViewCaseList.Focus();
					SetListViewToTop();
				}
			}
		}

		private void MenuItemSelectComment_Click(object sender, RoutedEventArgs e)
		{
			MenuItem menuItem = (MenuItem)sender;
			m_Comment = (YellowstonePathology.Business.Domain.Core.Comment)(menuItem.Tag);
		}

		public void ListViewBatchList_SelectionChanged(object sender, RoutedEventArgs args)
		{
			if (listViewBatchList.SelectedItem != null)
			{

				if ((int)listViewBatchList.SelectedValue == 0)
				{
					this.FillUnassignedList();
				}
				else
				{
					this.m_LabUI.SearchEngine.SetFillByBatchId((int)listViewBatchList.SelectedValue);
					this.m_LabUI.FillCaseList();
				}
				ListViewCaseList.Focus();
				SetListViewToTop();
			}
		}		

		public void ButtonShowUnassignedCases_Click(object sender, RoutedEventArgs args)
		{
			if (ComboBoxPanelSetType.SelectedItem != null)
			{
                if (((YellowstonePathology.Business.BatchTypeListItem)ComboBoxPanelSetType.SelectedItem).BatchTypeId != 8)
				{
					this.FillUnassignedList();					
				}
			}
			else
			{
				MessageBox.Show("Select a Batch Type for unassigned cases", "No Batch Type Selected",
					MessageBoxButton.OK, MessageBoxImage.Exclamation);
				ComboBoxPanelSetType.Focus();
			}
		}

		private void FillUnassignedList()
		{
			this.m_LabUI.SearchEngine.SetFillByUnBatchedBatchTypeId((int)ComboBoxPanelSetType.SelectedValue);
			this.m_LabUI.FillCaseList();
			this.ListViewCaseList.Focus();
			SetListViewToTop();
		}

        public void ButtonPrintBatchLog_Click(object sender, RoutedEventArgs args)
        {
            if (listViewBatchList.SelectedItem != null && ListViewCaseList.Items.Count > 0)
            {
				YellowstonePathology.Business.Panel.Model.PanelOrderBatch panelOrderBatch = (YellowstonePathology.Business.Panel.Model.PanelOrderBatch)listViewBatchList.SelectedItem;
                this.m_LabUI.PrintCurrentBatchLog(panelOrderBatch, this.m_LabUI.CaseList);
            }
            else
            {
                MessageBox.Show("No batch selected or the batch has no entries.", "Nothing to print",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }		

		public void ButtonMoveIndeterminateCase_Click(object sender, RoutedEventArgs args)
		{
			m_LabUI.MoveIndeterminateHpvCasesToUnassignedBatch();						
			this.m_LabUI.FillCaseList();
			this.ListViewCaseList.Focus();
			SetListViewToTop();
		}		

		private void ButtonDeleteCurrentHPV_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("Not Implemented in this version", "Not Implemented yet", MessageBoxButton.OK, MessageBoxImage.Exclamation);
		}

		public void ButtonCreateNewBatch_Click(object sender, RoutedEventArgs args)
		{
			if (ComboBoxPanelSetType.SelectedItem != null)
			{
                YellowstonePathology.Business.BatchTypeListItem batchTypeListItem = (YellowstonePathology.Business.BatchTypeListItem)this.ComboBoxPanelSetType.SelectedItem;
				if (batchTypeListItem.BatchIndicator != "Batch")
				{
					MessageBox.Show("Batch not used", batchTypeListItem.BatchTypeDescription + " currently are not batched.", MessageBoxButton.OK, MessageBoxImage.Exclamation);
					return;
				}

                YellowstonePathology.UI.MolecularTesting.PanelOrderBatch panelOrderBatch = new YellowstonePathology.UI.MolecularTesting.PanelOrderBatch(batchTypeListItem);
                panelOrderBatch.ShowDialog();

				if (panelOrderBatch.CurrentPanelOrderBatch != null)
				{
					this.m_LabUI.AddPanelOrderBatch(panelOrderBatch.CurrentPanelOrderBatch);
					this.listViewBatchList.SelectedIndex = -1;
					this.FillUnassignedList();					
				}
			}
			else
			{
				MessageBox.Show("Select a Batch Type to batch",	"No Batch Type Selected", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				ComboBoxPanelSetType.Focus();
			}
		}

        public void ButtonAssignUnassignedCases_Click(object sender, RoutedEventArgs args)
        {
            if (this.listViewBatchList.SelectedItems.Count > 0)
            {
				this.Save();
				this.SelectBatchForUnassignedPanels();
            }
            else
            {
                MessageBox.Show(
                    "Please select a batch to assign the cases to.",
                    "No Batch Selected",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);                
            }
        }

        public void ListViewPanelSetOrder_SelectionChanged(object sender, RoutedEventArgs args)
        {
			if(ListViewResultPanelSetOrder.SelectedIndex > -1)
			{
				this.m_LabUI.SetPanelSetOrder((YellowstonePathology.Business.Test.PanelSetOrder)ListViewResultPanelSetOrder.SelectedItem);
			}
        }

		public void ButtonFinalBatch_Click(object sender, RoutedEventArgs args)
		{
            if (this.listViewBatchList.SelectedItem != null)
            {
				YellowstonePathology.Business.Panel.Model.PanelOrderBatch batch = (YellowstonePathology.Business.Panel.Model.PanelOrderBatch)this.listViewBatchList.SelectedItem;
				if (batch.BatchTypeId == 3) //HPV
				{
					MessageBox.Show("Finalize HPV individually.", "HPV Only", MessageBoxButton.OK, MessageBoxImage.Exclamation);
					listViewBatchList.Focus();
				}
				else if (batch.BatchTypeId == 6) //NGCT
				{
					MessageBox.Show("Finalize NG-CT individually.", "NG-CT Only", MessageBoxButton.OK, MessageBoxImage.Exclamation);
					listViewBatchList.Focus();
				}
				else if (batch.BatchTypeId == 2) //CF
				{
					MessageBox.Show("Finalize CF individually.", "CF Only", MessageBoxButton.OK, MessageBoxImage.Exclamation);
					listViewBatchList.Focus();
				}
				else if (batch.BatchTypeId == 23) //HPV1618
				{
					MessageBox.Show("Finalize HPV Genotypes 16 and 18 individually.", "HPV Genotypes 16 and 18 Only", MessageBoxButton.OK, MessageBoxImage.Exclamation);
					listViewBatchList.Focus();
				}
				else
				{
				    YellowstonePathology.Business.Rules.RuleExecutionStatus ruleExecutionStatus = new YellowstonePathology.Business.Rules.RuleExecutionStatus();
					foreach (YellowstonePathology.Business.Search.ReportSearchItem item in this.m_LabUI.CaseList)
					{
						if (item.MasterAccessionNo == this.m_LabUI.AccessionOrder.MasterAccessionNo)
						{
							YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = this.m_LabUI.AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(item.ReportNo);
							panelSetOrder.Finalize(this.m_LabUI.AccessionOrder, ruleExecutionStatus, this.m_SystemIdentity);
							this.m_LabUI.Save();
						}
						else
						{
							YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();
							YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetAccessionOrderByReportNo(item.ReportNo);
							objectTracker.RegisterObject(accessionOrder);

							YellowstonePathology.Business.Test.PanelSetOrder panelSetOrderToFinalize = accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(item.ReportNo);
							panelSetOrderToFinalize.Finalize(accessionOrder, ruleExecutionStatus, this.m_SystemIdentity);
							objectTracker.SubmitChanges(accessionOrder);
						}
					}
					if (ruleExecutionStatus.ExecutionHalted == true)
					{
						YellowstonePathology.UI.RuleExecutionStatusDialog dialog = new RuleExecutionStatusDialog(ruleExecutionStatus);
						dialog.ShowDialog();
					}
					this.m_LabUI.FillCaseList();
					this.SetListViewToTop();
					System.Windows.MessageBox.Show("Finalize Process Complete.", "Complete", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				}
			}
			else
			{
				MessageBox.Show("Please select a batch to finalize.", "No Batch Selected",	MessageBoxButton.OK, MessageBoxImage.Exclamation);
				listViewBatchList.Focus();
			}
		}		

        private void ComboBoxPanelSetType_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			DateTime startTime = DateTime.Now;
			if (ComboBoxPanelSetType.SelectedItem != null)
			{
                YellowstonePathology.Business.BatchTypeListItem batchTypeListItem = (YellowstonePathology.Business.BatchTypeListItem)this.ComboBoxPanelSetType.SelectedItem;
				if (batchTypeListItem.BatchIndicator == "Day")
				{
					this.m_LabUI.SearchEngine.SetFillByAccessionDate(this.m_WorkDate, batchTypeListItem.BatchTypeId, this.GetFacilityIdFromLocation(this.ComboBoxLogLocation.Text));
					this.m_LabUI.BatchList.Clear();
					this.m_LabUI.FillCaseList();
				}
				else if (batchTypeListItem.BatchIndicator == "Month")
				{
					DateTime startdate = this.m_WorkDate.AddMonths(-1);
					this.m_LabUI.SearchEngine.SetFillByAccessionDateRange(startdate, this.m_WorkDate, batchTypeListItem.BatchTypeId, this.GetFacilityIdFromLocation(this.ComboBoxLogLocation.Text));
					this.m_LabUI.BatchList.Clear();
					this.m_LabUI.FillCaseList();
				}
                else if (batchTypeListItem.BatchTypeId == 24)
                {                    
                    this.m_LabUI.SearchEngine.SetFillByPanelSetId(213); //HPV 16 18 by PCR
                    this.m_LabUI.FillCaseList();
                }
                else if (batchTypeListItem.BatchTypeId == 6)
                {
                    this.m_LabUI.SearchEngine.SetFillByPanelSetId(3); //NGCT
                    this.m_LabUI.FillCaseList();
                }
                else
                {					
					this.m_LabUI.SearchEngine.SetBatchListFillByBatchTypeId(batchTypeListItem.BatchTypeId);
					this.m_LabUI.FillBatchList();

					if (this.listViewBatchList.Items.Count > 0)
					{
						this.listViewBatchList.SelectedIndex = 0;
					}
					else
					{
						this.m_LabUI.SearchEngine.SetFillByUnBatchedBatchTypeId(batchTypeListItem.BatchTypeId);
						this.m_LabUI.FillCaseList();
					}
				}
			}            
		}

		private void ButtonDelete_Click(object sender, RoutedEventArgs e)
		{
			if (this.listViewBatchList.SelectedItem != null)
			{
				YellowstonePathology.Business.Panel.Model.PanelOrderBatch panelOrderBatch = (YellowstonePathology.Business.Panel.Model.PanelOrderBatch)this.listViewBatchList.SelectedItem;
				if(this.m_LabUI.SearchEngine.CanDeleteBatch(panelOrderBatch.PanelOrderBatchId) == false)
				{
					System.Windows.MessageBox.Show(
						"The current batch contains entries and has NOT been deleted.",
						"Batch NOT Deleted",
						MessageBoxButton.OK, MessageBoxImage.Warning);
				}
				else
				{
					this.m_LabUI.RemovePanelOrderBatch(panelOrderBatch);
				}
			}
			else
			{
				MessageBox.Show("Please select a batch to delete.", "No Batch Selected",
					MessageBoxButton.OK, MessageBoxImage.Exclamation);
				listViewBatchList.Focus();
			}
		}

		private void SetListViewToTop()
		{
			if (ListViewCaseList.Items.Count > 0)
			{
				ListViewCaseList.ScrollIntoView(ListViewCaseList.Items[0]);
			}
		}
        
		private void ButtonSetResultsToNormal_Click(object sender, RoutedEventArgs args)
		{
            YellowstonePathology.Business.Rules.RuleExecutionStatus ruleExecutionStatus = new YellowstonePathology.Business.Rules.RuleExecutionStatus();
            
            Control resultsToNormalButton = (Control)sender;
            YellowstonePathology.Business.Test.PanelOrder panelOrder = (YellowstonePathology.Business.Test.PanelOrder)resultsToNormalButton.Tag;                
            panelOrder.SetResultsAsNormal(this.m_LabUI.AccessionOrder, ruleExecutionStatus);

            if (ruleExecutionStatus.ExecutionHalted == true)
            {
                YellowstonePathology.UI.RuleExecutionStatusDialog dialog = new RuleExecutionStatusDialog(ruleExecutionStatus);
                dialog.ShowDialog();
            }            
		}

		private void ButtonAcceptBatchResults_Click(object sender, RoutedEventArgs args)
		{
			this.Save();
            if (this.listViewBatchList.SelectedItem != null)
            {
				YellowstonePathology.Business.Panel.Model.PanelOrderBatch batch = (YellowstonePathology.Business.Panel.Model.PanelOrderBatch)this.listViewBatchList.SelectedItem;				
				if (batch.BatchTypeId == 3) //HPV
				{
					this.m_ResultDialog = new ResultDialog();
					HPVTWIBatchResultPath hpvTWIBatchResultPath = new HPVTWIBatchResultPath(this.m_LabUI.SearchEngine.ReportSearchList, this.m_ResultDialog.PageNavigator, this.m_SystemIdentity);
					hpvTWIBatchResultPath.Finish += new ResultPath.FinishEventHandler(BatchResultPath_Finish);
					hpvTWIBatchResultPath.Start();
					this.m_ResultDialog.ShowDialog();
				}
				else if(batch.BatchTypeId == 6) //NGCT
				{
					this.m_ResultDialog = new ResultDialog();
					NGCTBatchResultPath ngctBatchResultPath = new NGCTBatchResultPath(this.m_LabUI.SearchEngine.ReportSearchList, this.m_ResultDialog.PageNavigator, this.m_SystemIdentity);
					ngctBatchResultPath.Finish += new ResultPath.FinishEventHandler(BatchResultPath_Finish);
					ngctBatchResultPath.Start();
					this.m_ResultDialog.ShowDialog();
				}
				else if (batch.BatchTypeId == 2) //CF
				{
					this.m_ResultDialog = new ResultDialog();
					CysticFibrosisBatchResultPath cysticFibrosisBatchResultPath = new CysticFibrosisBatchResultPath(this.m_LabUI.SearchEngine.ReportSearchList, this.m_ResultDialog.PageNavigator, this.m_SystemIdentity);
					cysticFibrosisBatchResultPath.Finish += new ResultPath.FinishEventHandler(BatchResultPath_Finish);
					cysticFibrosisBatchResultPath.Start();
					this.m_ResultDialog.ShowDialog();
				}
				else if (batch.BatchTypeId == 23) //HPV16/18
				{
					this.m_ResultDialog = new ResultDialog();
					HPV1618BatchResultPath hpv1618BatchResultPath = new HPV1618BatchResultPath(this.m_LabUI.SearchEngine.ReportSearchList, this.m_ResultDialog.PageNavigator, this.m_SystemIdentity);
					hpv1618BatchResultPath.Finish += new ResultPath.FinishEventHandler(BatchResultPath_Finish);
					hpv1618BatchResultPath.Start();
					this.m_ResultDialog.ShowDialog();
				}
				else
				{
					YellowstonePathology.Business.Rules.RuleExecutionStatus ruleExecutionStatus = new YellowstonePathology.Business.Rules.RuleExecutionStatus();
					foreach (YellowstonePathology.Business.Search.ReportSearchItem item in this.m_LabUI.CaseList)
					{
						if (item.MasterAccessionNo == this.m_LabUI.AccessionOrder.MasterAccessionNo)
						{
							YellowstonePathology.Business.Test.PanelSetOrder panelSetOrderWithBatch = this.m_LabUI.PanelSetOrder;

							int count = panelSetOrderWithBatch.PanelOrderCollection.Count;
							for (int idx = 0; idx < count; idx++)
							{
								panelSetOrderWithBatch.PanelOrderCollection[idx].AcceptResults(ruleExecutionStatus, this.m_LabUI.AccessionOrder, this.m_SystemIdentity.User);
							}
							this.m_LabUI.Save();
							this.m_LabUI.GetAccessionOrder(this.m_LabUI.PanelSetOrder.ReportNo);
						}
						else
						{
							YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();
							YellowstonePathology.Business.Test.AccessionOrder logItem = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetAccessionOrderByReportNo(item.ReportNo);
							objectTracker.RegisterObject(logItem);
							YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = logItem.PanelSetOrderCollection.GetPanelSetOrder(item.ReportNo);

							int count = panelSetOrder.PanelOrderCollection.Count;
							for (int idx = 0; idx < count; idx++)
							{
								panelSetOrder.PanelOrderCollection[idx].AcceptResults(ruleExecutionStatus, logItem, this.m_SystemIdentity.User);
							}
							objectTracker.SubmitChanges(logItem);
						}
					}
					if (ruleExecutionStatus.ExecutionHalted == true)
					{
						YellowstonePathology.UI.RuleExecutionStatusDialog dialog = new RuleExecutionStatusDialog(ruleExecutionStatus);
						dialog.ShowDialog();
					}
					else
					{
						MessageBox.Show("Process accepting results is complete.");
					}
				}
			}
            else
            {
                MessageBox.Show("Please select a batch to finalize.", "No Batch Selected", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                listViewBatchList.Focus();
            }						
		}

        private void BatchResultPath_Finish(object sender, EventArgs e)
        {
            this.m_ResultDialog.Close();
        }        
		
		private void ButtonLinkPatient_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Patient.Model.PatientLinker patientLinker = new Business.Patient.Model.PatientLinker(this.m_LabUI.AccessionOrder.MasterAccessionNo,
				this.m_LabUI.PanelSetOrder.ReportNo, 
                this.m_LabUI.AccessionOrder.PFirstName, 
                this.m_LabUI.AccessionOrder.PLastName,
                this.m_LabUI.AccessionOrder.PMiddleInitial,
				this.m_LabUI.AccessionOrder.PSSN, 
                this.m_LabUI.AccessionOrder.PatientId, this.m_LabUI.AccessionOrder.PBirthdate);
			if (patientLinker.IsOkToLink.IsValid == true)
			{
				YellowstonePathology.UI.Common.PatientLinkingDialog patientLinkingDialog = new Common.PatientLinkingDialog(this.m_LabUI.AccessionOrder, Business.Patient.Model.PatientLinkingListModeEnum.AccessionOrder, patientLinker);
				patientLinkingDialog.ShowDialog();
			}
			else
			{
				MessageBox.Show(patientLinker.IsOkToLink.Message, "Missing Information");
			}
		}

		private void ButtonScan_Click(object sender, RoutedEventArgs e)
		{
			this.Save();            
			this.ButtonAcceptAliquotDetails.Focus();
		}		

        public void ButtonPlaceKRASOrderFromSurgicalCase_Click(object sender, RoutedEventArgs args)
        {
			MessageBox.Show("Please use the Tree View and order from the proper Aliquot.");
        }

		public void ButtonPlaceBRAFOrderFromSurgicalCase_Click(object sender, RoutedEventArgs args)
		{
			MessageBox.Show("Please use the Tree View and order from the proper Aliquot.");
		}

        private void ButtonPlaceHPV16OrderFromSurgicalCase_Click(object sender, RoutedEventArgs e)
        {
			MessageBox.Show("Please use the Tree View and order from the proper Aliquot.");
        }

		/*private void ButtonAcknowledge_Click(object sender, RoutedEventArgs e)
		{
            this.Save();
			this.m_LabUI.AcknowledgeOrder();
		}

		private void ButtonRefreshAcknowledgeList_Click(object sender, RoutedEventArgs e)
		{
			this.m_LabUI.FillOrderToAcknowledgeList();
		}*/

		private void SpecimenGrid_CurrentCellChanged(object sender, EventArgs e)
		{
			if (this.SpecimenGrid.CurrentCell.Column != null)
			{
				if (this.SpecimenGrid.CurrentCell.Column.Header != null)
				{
					this.SpecimenGrid.BeginEdit();
				}
			}
		}

		private void SpecimenGrid_PreviewKeyDown(object sender, KeyEventArgs e)
		{
		}

		private void SpecimenGrid_GotFocus(object sender, RoutedEventArgs e)
		{
			if (this.SpecimenGrid.IsEnabled)
			{
				if (this.ListViewCaseList.SelectedItem != null)
				{
					if (SpecimenGrid.Items.Count == 0)
					{
						if (this.SpecimenGrid.CurrentCell.Column == null)
						{
							this.SpecimenGrid.SelectedItem = this.SpecimenGrid.Items[this.SpecimenGrid.Items.Count - 1];
							this.SpecimenGrid.CurrentCell = this.SpecimenGrid.SelectedCells[2];
						}
					}
				}
			}
		}

		private void SpecimenGrid_LostFocus(object sender, RoutedEventArgs e)
		{
			this.SpecimenGrid.SelectedIndex = -1;
		}

		private void ButtonCurrentFixationDetails_Click(object sender, RoutedEventArgs e)
		{
			if (this.ListViewCaseList.SelectedItem != null)
			{
				this.ShowFixationDialog();
			}
		}

		private void ShowFixationDialog()
		{
			Login.FinalizeAccession.FixationPath fixationPath = new Login.FinalizeAccession.FixationPath(this.m_LabUI.AccessionOrder, this.m_SystemIdentity);
			fixationPath.Start();
		}

		private void ButtonAcceptAliquotDetails_Click(object sender, RoutedEventArgs e)
		{

		}				

		private void MainGrid_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Insert)
			{
				if (ListViewCaseList.SelectedItem != null)
				{
					this.SpecimenGrid.Focus();
					if (this.m_LabUI.AccessionOrder.SpecimenOrderCollection.Count > 0)
					{
						this.SpecimenGrid_PreviewKeyDown(sender, e);
					}
				}
			}
		}

		private void TextBoxCollectionDate_KeyUp(object sender, KeyEventArgs e)
		{
            Nullable<DateTime> targetDate = null;
            bool result = YellowstonePathology.Business.Helper.TextBoxHelper.IncrementDate(this.TextBoxCollectionDate.Text, ref targetDate, e);
            if (result == true) this.m_LabUI.AccessionOrder.CollectionDate = targetDate;            
		}

        private void ButtonBack_Click(object sender, RoutedEventArgs args)
        {
			int batchTypeId = 0;
            this.m_WorkDate = this.m_WorkDate.AddDays(-1);
			if (this.ComboBoxPanelSetType.SelectedItem != null)
			{
                YellowstonePathology.Business.BatchTypeListItem batchTypeListItem = (YellowstonePathology.Business.BatchTypeListItem)this.ComboBoxPanelSetType.SelectedItem;
				batchTypeId = batchTypeListItem.BatchTypeId;
			}
			this.m_LabUI.SearchEngine.SetFillByAccessionDate(this.m_WorkDate, batchTypeId, this.GetFacilityIdFromLocation(this.ComboBoxLogLocation.Text));
			this.m_LabUI.FillCaseList();
		}

        private void ButtonForward_Click(object sender, RoutedEventArgs args)
        {
			int batchTypeId = 0;
			this.m_WorkDate = this.m_WorkDate.AddDays(+1);

			if (this.ComboBoxPanelSetType.SelectedItem != null)
			{
                YellowstonePathology.Business.BatchTypeListItem batchTypeListItem = (YellowstonePathology.Business.BatchTypeListItem)this.ComboBoxPanelSetType.SelectedItem;
				batchTypeId = batchTypeListItem.BatchTypeId;
			}
            
            this.m_LabUI.SearchEngine.SetFillByAccessionDate(this.m_WorkDate, batchTypeId, this.GetFacilityIdFromLocation(this.ComboBoxLogLocation.Text));
			this.m_LabUI.FillCaseList();
		}

        private string GetFacilityIdFromLocation(string location)
        {
            string result = null;
            switch (this.ComboBoxLogLocation.Text)
            {
                case "All":
                    result = null;
                    break;
                case "Billings":
                    YellowstonePathology.Business.Facility.Model.YellowstonePathologistBillings ypBlgs = new Business.Facility.Model.YellowstonePathologistBillings();
                    result = ypBlgs.FacilityId;
                    break;
                case "Cody":
                    YellowstonePathology.Business.Facility.Model.YellowstonePathologistCody ypCdy = new Business.Facility.Model.YellowstonePathologistCody();
                    result = ypCdy.FacilityId;
                    break;
            }
            return result;
        }

		private void ButtonMasterLog_Click(object sender, RoutedEventArgs e)
		{
            YellowstonePathology.Business.Reports.Surgical.SurgicalMasterLog report = new YellowstonePathology.Business.Reports.Surgical.SurgicalMasterLog();                        
            report.CreateReport(m_WorkDate);
            report.OpenReport();            
		}				

		private void ButtonSlideLabels_Click(object sender, RoutedEventArgs e)
		{
            /*
            YellowstonePathology.UI.Login.AccessionLabelDialog slideLabels = null;
            if (this.ComboBoxPanelSetType.SelectedItem != null)
            {
				YellowstonePathology.Domain.BatchTypeListItem batchTypeListItem = (YellowstonePathology.Domain.BatchTypeListItem)this.ComboBoxPanelSetType.SelectedItem;
                if (batchTypeListItem.BatchTypeDescription == "Acid Wash")
                {
                    slideLabels = new YellowstonePathology.UI.Login.AccessionLabelDialog(this.m_LabUI.CaseList);
                }
                else
                {
                    slideLabels = new YellowstonePathology.UI.Login.AccessionLabelDialog();
                }
            }
            else
            {
                slideLabels = new YellowstonePathology.UI.Login.AccessionLabelDialog();
            }
            slideLabels.ShowDialog();
            */
		}		

		private void ComboBoxLogLocation_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			int batchTypeId = 0;
			if (this.ComboBoxPanelSetType.SelectedItem != null)
			{
                YellowstonePathology.Business.BatchTypeListItem batchTypeListItem = (YellowstonePathology.Business.BatchTypeListItem)this.ComboBoxPanelSetType.SelectedItem;
				batchTypeId = batchTypeListItem.BatchTypeId;
			}
            
            string selection = ((ComboBoxItem)this.ComboBoxLogLocation.SelectedItem).Content.ToString();            
            switch (selection)
            {
                case "All":
                    this.m_LabUI.SearchEngine.SetFillByAccessionDate(this.m_WorkDate, batchTypeId, null);
                    break;
                case "Billings":
                    YellowstonePathology.Business.Facility.Model.YellowstonePathologistBillings ypBlgs = new Business.Facility.Model.YellowstonePathologistBillings();
                    this.m_LabUI.SearchEngine.SetFillByAccessionDate(this.m_WorkDate, batchTypeId, ypBlgs.FacilityId);
                    break;
                case "Cody":
                    YellowstonePathology.Business.Facility.Model.YellowstonePathologistCody ypCdy = new Business.Facility.Model.YellowstonePathologistCody();
                    this.m_LabUI.SearchEngine.SetFillByAccessionDate(this.m_WorkDate, batchTypeId, ypCdy.FacilityId);
                    break;                
            }			
			this.m_LabUI.FillCaseList();            
		}

        private void ButtonAssignPathologist_Click(object sender, RoutedEventArgs e)
        {
			if (this.m_LabUI.AccessionOrder.PanelSetOrderCollection.Count != 0)
            {				
				if (!this.m_LabUI.PanelSetOrder.Final)
                {
                    YellowstonePathology.UI.Common.PathologistSelectionDialog dialog = new YellowstonePathology.UI.Common.PathologistSelectionDialog(this.m_LabUI.PathologistUsers);
                    if (dialog.ShowDialog() == true)
                    {
						this.m_LabUI.PanelSetOrder.AssignedToId = dialog.SelectedPathologistUser.UserId;                        
						this.m_LabUI.Save();
                    }
                }
                else
                {
                    MessageBox.Show("This case is finaled.");                    
                }
            }
            else
            {
                MessageBox.Show("You must order the Surgical before assigning the Pathologist.");
            }
        }
        
		private void MenuItemBatchAndPrint_Click(object sender, RoutedEventArgs e)
		{
			if (this.ListViewCaseList.SelectedItems.Count > 0)
			{
                YellowstonePathology.Business.BatchTypeListItem batchTypeListItem = (YellowstonePathology.Business.BatchTypeListItem)this.ComboBoxPanelSetType.SelectedItem;
				YellowstonePathology.Business.Panel.Model.PanelOrderBatch panelOrderBatch = this.m_LabUI.AddPanelOrderBatch(batchTypeListItem);

				this.AssignUnassignedPanelsToBatch(panelOrderBatch);

				YellowstonePathology.Business.Search.ReportSearchList selectedItemList = new Business.Search.ReportSearchList();
				foreach (YellowstonePathology.Business.Search.ReportSearchItem item in ListViewCaseList.SelectedItems)
				{
					selectedItemList.Add(item);
				}
				this.m_LabUI.PrintCurrentBatchLog(panelOrderBatch, selectedItemList);
				this.m_LabUI.FillCaseList();
			}
			else
			{
				MessageBox.Show("Select all the cases to batch", "No cases selected");
			}
		}

		private void ContextMenuBatchAndPrint_Opened(object sender, RoutedEventArgs e)
		{
			bool enabled = false;
			if (this.ListViewCaseList.SelectedItem != null && ((YellowstonePathology.Business.Search.ReportSearchItem)this.ListViewCaseList.SelectedItem).ReportNo[0] != 'S') enabled = true;

			ContextMenu contextMenu = (ContextMenu)sender;
			foreach (MenuItem menuItem in contextMenu.Items)
			{
				if (menuItem.Header.ToString() == "Batch And Print")
				{
					menuItem.IsEnabled = enabled;
					break;
				}
			}

			if (this.ListViewCaseList.SelectedItem != null && this.listViewBatchList.SelectedItem != null && (int)this.listViewBatchList.SelectedValue != 0) enabled = true;
			else enabled = false;

			foreach (MenuItem menuItem in contextMenu.Items)
			{
				if (menuItem.Header.ToString() == "Remove from Batch")
				{
					menuItem.IsEnabled = enabled;
					break;
				}
			}
		}

		private void SelectBatchForUnassignedPanels()
		{
			this.m_LabUI.Save();
			BatchAssignmentSelectionDialog dialog = new BatchAssignmentSelectionDialog(this.m_LabUI.SearchEngine.PanelOrderBatchList);
			bool? result = dialog.ShowDialog();
			if (result.HasValue && result.Value)
			{
				YellowstonePathology.Business.Panel.Model.PanelOrderBatch panelOrderBatch = dialog.SelectedPanelOrderBatch;
				this.AssignUnassignedPanelsToBatch(panelOrderBatch);
			}
		}

		private void AssignUnassignedPanelsToBatch(YellowstonePathology.Business.Panel.Model.PanelOrderBatch panelOrderBatch)
		{
			foreach (YellowstonePathology.Business.Search.ReportSearchItem item in this.ListViewCaseList.SelectedItems)
			{
				if(item.MasterAccessionNo == this.m_LabUI.AccessionOrder.MasterAccessionNo)
				{
					YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = this.m_LabUI.AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(item.ReportNo);
					panelSetOrder.PanelOrderCollection.AssignBatchId(panelOrderBatch.PanelOrderBatchId);
					this.m_LabUI.Save();
				}
				else
				{
					YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();
					YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetAccessionOrderByReportNo(item.ReportNo);
					objectTracker.RegisterObject(accessionOrder);
					YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(item.ReportNo);
					panelSetOrder.PanelOrderCollection.AssignBatchId(panelOrderBatch.PanelOrderBatchId);
					objectTracker.SubmitChanges(accessionOrder);
				}
			}
		}								

        private void ButtonCaseHistory_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.UI.Common.CaseHistoryDialog caseHistoryDialog = new Common.CaseHistoryDialog(this.m_LabUI.AccessionOrder);
            caseHistoryDialog.ShowDialog();
        }        

		private void UserChangedHandler()
		{			
			this.ComboCurrentUser.SelectionChanged -= this.ComboCurrentUser_SelectionChanged;
			if (!this.m_SystemIdentity.IsKnown)
			{
				this.SetSelectedUser(-1);                
			}
			else
			{
				int index = 0;
				foreach (YellowstonePathology.Business.User.SystemUser systemUser in this.ComboCurrentUser.Items)
				{
					if (systemUser.UserId == this.m_SystemIdentity.User.UserId)
					{
						this.SetSelectedUser(index);
						break;
					}
					index += 1;
				}
			}
			this.ComboCurrentUser.SelectionChanged += this.ComboCurrentUser_SelectionChanged;
		}

        private void SetSelectedUser(int index)
        {
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                new Action(
                    delegate()
                    {
                        this.ComboCurrentUser.SelectedIndex = index;                        
                    }));            
        }

		private void ComboCurrentUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
            if (this.ComboCurrentUser.SelectedItem != null)
            {
                YellowstonePathology.Business.User.SystemUser selectedUser = (YellowstonePathology.Business.User.SystemUser)this.ComboCurrentUser.SelectedItem;
                this.m_SystemIdentity.SetSelectedUser(selectedUser);
            }
            else
            {
                this.m_SystemIdentity.Clear();                
            }
		}

        private void ButtonContainerLabels_Click(object sender, RoutedEventArgs e)
        {
			YellowstonePathology.UI.Test.BarcodeLabelDialog containerLabelDialog = new BarcodeLabelDialog();
            containerLabelDialog.ShowDialog();
        }                      

        private void SvhMedicalRecordNoReceived(string scanData)
        {
            if (this.m_LabUI.Lock.LockAquired == true)
            {
                if (string.IsNullOrEmpty(this.m_LabUI.AccessionOrder.SvhMedicalRecord) == true)
                {
                    this.m_LabUI.AccessionOrder.SvhMedicalRecord = scanData;
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("Would you like to replace the Svh Medical Record?", "Replace Data.", MessageBoxButton.OKCancel);
                    {
                        if (result == MessageBoxResult.OK)
                        {
                            this.m_LabUI.AccessionOrder.SvhMedicalRecord = scanData;
                        }
                    }
                }
            }
        }

        private void SvhAccountNoReceived(string scanData)
        {
            if (this.m_LabUI.Lock.LockAquired == true)
            {
                if (string.IsNullOrEmpty(this.m_LabUI.AccessionOrder.SvhAccount) == true)
                {
                    this.m_LabUI.AccessionOrder.SvhAccount = scanData;
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show("Would you like to replace the Svh Account Number?", "Replace Data.", MessageBoxButton.OKCancel);
                    {
                        if (result == MessageBoxResult.OK)
                        {
                            this.m_LabUI.AccessionOrder.SvhMedicalRecord = scanData;
                        }
                    }
                }
            }
        }

		private void ClientScanReceived(YellowstonePathology.Business.BarcodeScanning.Barcode barcode)
		{            
            /*
			this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate()
			{
                int clientId = Convert.ToInt32(barcode.ID);
				YellowstonePathology.UI.Common.PhysicianClientSearchDialog dialog = new YellowstonePathology.UI.Common.PhysicianClientSearchDialog(this.m_LabUI.AccessionOrder, clientId);				
				bool? result = dialog.ShowDialog();
				if (result.HasValue && result.Value == true)
				{
					this.m_LabUI.AccessionOrder.SetPhysicianClient(dialog.PhysicianClientListItem);
				}
			}
			));
            */
		}

        private void MenuItemPrintMolecularLabel_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewCaseList.SelectedItems.Count != 0)
            {
                if (string.IsNullOrEmpty(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.MolecularLabelFormat) == false)
                {
					foreach (YellowstonePathology.Business.Search.ReportSearchItem item in this.ListViewCaseList.SelectedItems)
                    {
                        YellowstonePathology.Business.PanelSet.Model.PanelSetCollection panelSetCollection = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetAll();
                        YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = panelSetCollection.GetPanelSet(item.PanelSetId);

                        YellowstonePathology.Business.Label.Model.LabelFormatEnum labelFormat = (YellowstonePathology.Business.Label.Model.LabelFormatEnum)Enum.Parse(typeof(YellowstonePathology.Business.Label.Model.LabelFormatEnum), YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.MolecularLabelFormat);
                        YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.ThinPrepFluid thinPrepFluid = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.ThinPrepFluid();
                        YellowstonePathology.Business.Label.Model.MolecularLabelPrinter molecularLabelPrinter = new Business.Label.Model.MolecularLabelPrinter();
                        YellowstonePathology.Business.Label.Model.Label label = YellowstonePathology.Business.Label.Model.LabelFactory.GetMolecularLabel(labelFormat, item.MasterAccessionNo, item.PFirstName, item.PLastName, item.SpecimenDescription, panelSet, true);
                        molecularLabelPrinter.Queue.Enqueue(label);
                        molecularLabelPrinter.Print();
                    }
                }
                else
                {
                    MessageBox.Show("The label format must first be selected in User Preferences.");
                }
            }
        }        

        private void MenuItemOpenDocumentFolder_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewCaseList.SelectedItems.Count != 0)
            {
				YellowstonePathology.Business.Search.ReportSearchItem reportSearchItem = (YellowstonePathology.Business.Search.ReportSearchItem)this.ListViewCaseList.SelectedItem;
				YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(reportSearchItem.ReportNo);
				string folderPath = YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser);
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.Process p = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo("Explorer.exe", folderPath);
                p.StartInfo = info;
                p.Start();
            }
        }
        
		private void HyperLinkEdit_Click(object sender, RoutedEventArgs e)
		{
			Hyperlink hyperlink = (Hyperlink)sender;
			YellowstonePathology.Business.Test.Model.TestOrder testOrder = (YellowstonePathology.Business.Test.Model.TestOrder)hyperlink.Tag;
			TestResultEditDialog testResultEditDialog = new TestResultEditDialog(testOrder);
			testResultEditDialog.ShowDialog();
		}                      		     

        private void MenuItemOpenPhysicianEntry_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewCaseList.SelectedItems.Count != 0)
            {
				YellowstonePathology.Business.Domain.Physician physician = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianByPhysicianId(this.m_LabUI.AccessionOrder.PhysicianId);
				YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();
                objectTracker.RegisterObject(physician);

                YellowstonePathology.UI.Client.ProviderEntry providerEntry = new Client.ProviderEntry(physician, objectTracker, false);
                providerEntry.ShowDialog();
            }
        }

		private void ButtonPlayDictation_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_LabUI.PanelSetOrder.ReportNo);
			string filePath = YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser);
			string fileName = this.m_LabUI.PanelSetOrder.ReportNo + @".dct";
			string fullFileName = System.IO.Path.Combine(filePath, fileName);
			if (System.IO.File.Exists(fullFileName))
			{
				YellowstonePathology.Business.FileListItem item = new YellowstonePathology.Business.FileListItem(fullFileName);
				YellowstonePathology.Business.FileList.OpenFile(item);
			}
			else
			{
				MessageBox.Show("Dictation file for " + this.m_LabUI.PanelSetOrder.ReportNo + " is not in the case folder.");
			}
		}

        private void ButtonTypingTemplates_Click(object sender, RoutedEventArgs e)
        {
            TypingTemplateDialog typingTemplateDialog = new TypingTemplateDialog();
            bool? result = typingTemplateDialog.ShowDialog();
            if (result.HasValue && result.Value == true)
            {
                this.m_LabUI.AccessionOrder.PanelSetOrderCollection[0].PanelOrderCollection[0].TestOrderCollection[0].Result = typingTemplateDialog.TextBoxTemplateParagraphText.Text;
            }
        }

        private void ButtonAutopsyResults_Click(object sender, RoutedEventArgs e)
        {
            AutopsyResultDialog autopsyResultDialog = new AutopsyResultDialog();
            autopsyResultDialog.ShowDialog();
        }

        private void MenuItemEditResults_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewCaseList.SelectedItem != null)
            {
				YellowstonePathology.Business.Search.ReportSearchItem item = (YellowstonePathology.Business.Search.ReportSearchItem)this.ListViewCaseList.SelectedItem;
                if (item.PanelSetId == 35) //Autopsy
                {
                    AutopsyResultDialog autopsyResultDialog = new AutopsyResultDialog();
                    autopsyResultDialog.ShowDialog();
                }
            }
        }

        private void MenuItemToXml_Click(object sender, RoutedEventArgs e)
        {
            //if (this.ListViewCaseList.SelectedItem != null)
            //{
            //    YellowstonePathology.Domain.ReportSearchItem item = (YellowstonePathology.Domain.ReportSearchItem)this.ListViewCaseList.SelectedItem;
			//    XElement document = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetAccessionOrderDocumentByReportNo(item.ReportNo);
            //    document.Save(@"c:\testing\test.xml");
            //}
        }

        private void MenuItemPublish_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewCaseList.SelectedItem != null)
            {
				YellowstonePathology.Business.Search.ReportSearchItem item = (YellowstonePathology.Business.Search.ReportSearchItem)this.ListViewCaseList.SelectedItem;
                YellowstonePathology.Business.Interface.ICaseDocument caseDocument = YellowstonePathology.Business.Document.DocumentFactory.GetDocument(item.PanelSetId);
				YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(item.ReportNo);
                YellowstonePathology.Business.Rules.MethodResult methodResult = caseDocument.DeleteCaseFiles(orderIdParser);
                if (methodResult.Success == true)
                {
                    caseDocument.Render(item.MasterAccessionNo, item.ReportNo, YellowstonePathology.Business.Document.ReportSaveModeEnum.Normal);
                    caseDocument.Publish();
                    MessageBox.Show("The document has been published");
                }
                else
                {
                    MessageBox.Show(methodResult.Message);
                }
            }
        }

        private void ButtonCodes_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewCaseList.SelectedItem != null)
            {
                /*
                YellowstonePathology.Domain.ReportSearchItem item = (YellowstonePathology.Domain.ReportSearchItem)this.ListViewCaseList.SelectedItem;
				YellowstonePathology.UI.Billing.BillingCodeDialog billingcodeDialog = new Billing.BillingCodeDialog();
				billingcodeDialog.DoSearchByReportNo(item.ReportNo);
				billingcodeDialog.ShowDialog();
                */
            }
        }

        private void MenuItemSendHL7StatusMessage_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewCaseList.SelectedItem != null)
            {
				YellowstonePathology.Business.Search.ReportSearchItem item = (YellowstonePathology.Business.Search.ReportSearchItem)this.ListViewCaseList.SelectedItem;                
                YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection clientOrderCollection = YellowstonePathology.Business.Gateway.ClientOrderGateway.GetClientOrdersByMasterAccessionNo(item.MasterAccessionNo);

                YellowstonePathology.Business.ClientOrder.Model.UniversalServiceCollection universalServiceIdCollection = YellowstonePathology.Business.ClientOrder.Model.UniversalServiceCollection.GetAll();
                YellowstonePathology.Business.ClientOrder.Model.UniversalService universalService = universalServiceIdCollection.GetByUniversalServiceId(clientOrderCollection[0].UniversalServiceId);

                YellowstonePathology.Business.HL7View.EPIC.EpicStatusMessage statusMessage = new Business.HL7View.EPIC.EpicStatusMessage(clientOrderCollection[0], YellowstonePathology.Business.HL7View.OrderStatusEnum.InProcess, universalService);
                YellowstonePathology.Business.Rules.MethodResult result = statusMessage.Send();

                if (result.Success == false)
                {
                    MessageBox.Show(result.Message);
                }
            }
        }

		private void ButtonReportDetails_Click(object sender, RoutedEventArgs e)
		{
			if (this.ListViewCaseList.SelectedItem != null)
			{
				YellowstonePathology.Business.Search.ReportSearchItem reportSearchItem = (YellowstonePathology.Business.Search.ReportSearchItem)this.ListViewCaseList.SelectedItem;
                YellowstonePathology.UI.ReportOrder.ReportOrderDetailPage reportOrderDetailPage = new YellowstonePathology.UI.ReportOrder.ReportOrderDetailPage(this.m_LabUI.AccessionOrder, reportSearchItem.ReportNo, this.m_SystemIdentity);
                reportOrderDetailPage.ShowDialog();
			}
		}        

        private void MenuItemSendMeditechResult_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewCaseList.SelectedItem != null)
            {
				YellowstonePathology.Business.Search.ReportSearchItem item = (YellowstonePathology.Business.Search.ReportSearchItem)this.ListViewCaseList.SelectedItem;
                YellowstonePathology.Business.ReportDistribution.Model.MeditechDistribution meditechDistribution = new Business.ReportDistribution.Model.MeditechDistribution();
                meditechDistribution.Distribute(item.ReportNo);
                MessageBox.Show("Meditech result has been sent");
            }
        }  

        private void MenuItemShowHl7Result_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewCaseList.SelectedItem != null)
            {
				YellowstonePathology.Business.Search.ReportSearchItem item = (YellowstonePathology.Business.Search.ReportSearchItem)this.ListViewCaseList.SelectedItem;
				YellowstonePathology.Business.HL7View.IResultView resultView = YellowstonePathology.Business.HL7View.ResultViewFactory.GetResultView(item.ReportNo, this.m_LabUI.AccessionOrder.ClientId, false);
				if (resultView != null)
				{
					XElement document = resultView.GetDocument();
					ReportDistribution.Hl7ResultDialog hl7ResultDialog = new ReportDistribution.Hl7ResultDialog(document);
					hl7ResultDialog.ShowDialog();
				}
				else
				{
					MessageBox.Show("The client does not receive an HL7 result.  Choose another report to view.");
				}
            }
        }        

        private void MenuItemSendHL7Result_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewCaseList.SelectedItem != null)
            {
				YellowstonePathology.Business.Search.ReportSearchItem item = (YellowstonePathology.Business.Search.ReportSearchItem)this.ListViewCaseList.SelectedItem;                
                YellowstonePathology.Business.HL7View.IResultView resultView = YellowstonePathology.Business.HL7View.ResultViewFactory.GetResultView(item.ReportNo, this.m_LabUI.AccessionOrder.ClientId, false);
                YellowstonePathology.Business.Rules.MethodResult methodResult = new Business.Rules.MethodResult();
                resultView.Send(methodResult);                               
            }
        }

        private void MenuItemSendHL7ResultTest_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewCaseList.SelectedItem != null)
            {
				YellowstonePathology.Business.Search.ReportSearchItem item = (YellowstonePathology.Business.Search.ReportSearchItem)this.ListViewCaseList.SelectedItem;
                YellowstonePathology.Business.HL7View.IResultView resultView = YellowstonePathology.Business.HL7View.ResultViewFactory.GetResultView(item.ReportNo, this.m_LabUI.AccessionOrder.ClientId, true);
                YellowstonePathology.Business.Rules.MethodResult methodResult = new Business.Rules.MethodResult();
                resultView.Send(methodResult);
            }
        }  

        private void MenuItemSendHL7Order_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.HL7View.Orders.ORMO01 ormo01 = new Business.HL7View.Orders.ORMO01(this.m_LabUI.AccessionOrder);
            XElement document = new XElement("HL7Message");
            ormo01.ToXml(document);
        }

		private void ButtonResults_Click(object sender, RoutedEventArgs e)
		{
            YellowstonePathology.Business.User.SystemIdentity systemIdentity = new Business.User.SystemIdentity(Business.User.SystemIdentityTypeEnum.CurrentlyLoggedIn);

            this.m_ResultDialog = new ResultDialog();
            YellowstonePathology.UI.Test.ResultPathFactory resultPathFactory = new Test.ResultPathFactory();
            resultPathFactory.Finished += new Test.ResultPathFactory.FinishedEventHandler(ResultPathFactory_Finished);

            bool started = resultPathFactory.Start(this.m_LabUI.PanelSetOrder, this.m_LabUI.AccessionOrder, this.m_LabUI.ObjectTracker, this.m_ResultDialog.PageNavigator, systemIdentity, System.Windows.Visibility.Collapsed);
            if (started == true)
            {
                this.m_ResultDialog.ShowDialog();
            }
            else
            {
                MessageBox.Show("The result for this case is not available in this view.");
            }
		}

        private void ResultPathFactory_Finished(object sender, EventArgs e)
        {
            this.m_ResultDialog.Close();
        }

		private void ResultPath_Finish(object sender, EventArgs e)
		{
			this.m_ResultDialog.Close();
		}

		private void MenuItemRemoveFromBatch_Click(object sender, RoutedEventArgs e)
		{
			if (this.ListViewCaseList.SelectedItem != null)
			{
				if(listViewBatchList.SelectedItem != null)
				{
					YellowstonePathology.Business.Search.ReportSearchItem item = (YellowstonePathology.Business.Search.ReportSearchItem)this.ListViewCaseList.SelectedItem;
					YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = this.m_LabUI.AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(item.ReportNo);
						panelSetOrder.PanelOrderCollection.RemovePanelFromBatch((int)listViewBatchList.SelectedValue);
						this.m_LabUI.Save();
				}
			}
		}

        private void ButtonShowTecanImportExportDialog_Click(object sender, RoutedEventArgs e)
        {
            TecanImportExportPage tecanImportExportPage = new TecanImportExportPage(this.m_LabUI.CaseList, System.Windows.Visibility.Collapsed, System.Windows.Visibility.Visible);
            tecanImportExportPage.Close += new TecanImportExportPage.CloseEventHandler(TecanImportExportPage_Close);
            this.m_ResultDialog = new ResultDialog();
            this.m_ResultDialog.PageNavigator.Navigate(tecanImportExportPage);
            this.m_ResultDialog.ShowDialog();
        }

        private void TecanImportExportPage_Close(object sender, EventArgs e)
        {
            this.m_ResultDialog.Close();   
        }

        private void MenuItemSendPantherOrder_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewCaseList.SelectedItems.Count != 0)
            {
                //YellowstonePathology.Business.Search.ReportSearchItem reportSearchItem = (YellowstonePathology.Business.Search.ReportSearchItem)this.ListViewAccessionOrders.SelectedItem;
                foreach(YellowstonePathology.Business.Search.ReportSearchItem reportSearchitem in this.ListViewCaseList.SelectedItems)
                {
                    YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetAccessionOrderByReportNo(reportSearchitem.ReportNo);
                    if (accessionOrder.SpecimenOrderCollection.HasThinPrepFluidSpecimen() == true)
                    {
                        YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = accessionOrder.SpecimenOrderCollection.GetThinPrep();
                        if (specimenOrder.AliquotOrderCollection.HasPantherAliquot() == true)
                        {
                            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = specimenOrder.AliquotOrderCollection.GetPantherAliquot();
                            YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportSearchitem.ReportNo);
                            YellowstonePathology.Business.HL7View.Panther.PantherAssay pantherAssay = null;
                            switch (panelSetOrder.PanelSetId)
                            {
                                case 14:
                                    pantherAssay = new Business.HL7View.Panther.PantherAssayHPV();
                                    break;
                                case 3:
                                    pantherAssay = new Business.HL7View.Panther.PantherAssayNGCT();
                                    break;
                                default:
                                    throw new Exception(panelSetOrder.PanelSetName + " is mot implemented yet.");
                            }

                            YellowstonePathology.Business.HL7View.Panther.PantherOrder pantherOrder = new Business.HL7View.Panther.PantherOrder(pantherAssay, specimenOrder, aliquotOrder, accessionOrder, panelSetOrder, YellowstonePathology.Business.HL7View.Panther.PantherActionCode.NewSample);
                            pantherOrder.Send();
                        }
                        else
                        {
                            MessageBox.Show("No Panther aliquot found.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("No Thin Prep Fluid Specimen Found.");
                    }
                }                
            }
        }
    }
}
