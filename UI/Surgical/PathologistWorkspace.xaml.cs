using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Data.SqlClient;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

using System.Windows.Controls.Primitives;

namespace YellowstonePathology.UI.Surgical
{
    public partial class PathologistWorkspace : System.Windows.Controls.UserControl
    {        
		private PathologistsReview m_PathologistsReview;
		private PathologistUI m_PathologistUI;
		private Cytology.CytologyResultsWorkspace m_CytologyResultsWorkspace;

		bool m_Loaded;
		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		private YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort m_BarcodeScanPort;
		private MainWindowCommandButtonHandler m_MainWindowCommandButtonHandler;

		public PathologistWorkspace(MainWindowCommandButtonHandler mainWindowCommandButtonHandler)
		{
			this.m_MainWindowCommandButtonHandler = mainWindowCommandButtonHandler;
			this.m_SystemIdentity = new YellowstonePathology.Business.User.SystemIdentity(YellowstonePathology.Business.User.SystemIdentityTypeEnum.CurrentlyLoggedIn);

			m_Loaded = false;
			this.m_PathologistUI = new PathologistUI(this.m_SystemIdentity);

			InitializeComponent();

			this.DataContext = this.m_PathologistUI;

			this.Loaded += new RoutedEventHandler(PathologistWorkspace_Loaded);
			this.Unloaded += new RoutedEventHandler(PathologistWorkspace_Unloaded);

			this.m_BarcodeScanPort = YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort.Instance;
			this.m_BarcodeScanPort.CytologySlideScanReceived += new YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort.CytologySlideScanReceivedHandler(CytologySlideScanReceived);
			this.m_BarcodeScanPort.HistologySlideScanReceived += new Business.BarcodeScanning.BarcodeScanPort.HistologySlideScanReceivedHandler(HistologySlideScanReceived);
			this.m_BarcodeScanPort.HistologyBlockScanReceived += new Business.BarcodeScanning.BarcodeScanPort.HistologyBlockScanReceivedHandler(BarcodeScanPort_HistologyBlockScanReceived);
			this.m_BarcodeScanPort.ThinPrepSlideScanReceived += new Business.BarcodeScanning.BarcodeScanPort.ThinPrepSlideScanReceivedHandler(BarcodeScanPort_ThinPrepSlideScanReceived);
		}

		private void BarcodeScanPort_ThinPrepSlideScanReceived(Business.BarcodeScanning.Barcode barcode)
		{
			this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
				new Action(
					delegate()
					{
						if (barcode.IsValidated == true)
						{
							this.Save();							
							this.DoAliquotOrderIdSearch(barcode.ID, 15);                            
                        }
						else
						{
							System.Windows.MessageBox.Show("The scan did not result in a valid case, please try again.", "Invalid Scan", MessageBoxButton.OK, MessageBoxImage.Exclamation);
						}                         
					}));
		}

		private void PathologistWorkspace_Loaded(object sender, RoutedEventArgs e)
		{
			this.m_MainWindowCommandButtonHandler.Save += new EventHandler(MainWindowCommandButtonHandler_Save);
			this.m_MainWindowCommandButtonHandler.ToggleAccessionLock += new EventHandler(MainWindowCommandButtonHandler_ToggleAccessionLock);
			this.m_MainWindowCommandButtonHandler.ShowCaseDocument += new EventHandler(MainWindowCommandButtonHandler_ShowCaseDocument);
			this.m_MainWindowCommandButtonHandler.ShowOrderForm += new EventHandler(MainWindowCommandButtonHandler_ShowOrderForm);
			this.m_MainWindowCommandButtonHandler.ShowAmendmentDialog += new EventHandler(MainWindowCommandButtonHandler_ShowAmendmentDialog);
			this.m_MainWindowCommandButtonHandler.AssignCase += new EventHandler(MainWindowCommandButtonHandler_AssignCase);
			this.m_MainWindowCommandButtonHandler.ApplicationClosing += new EventHandler(MainWindowCommandButtonHandler_ApplicationClosing);

            this.m_MainWindowCommandButtonHandler.StartProviderDistributionPath += new MainWindowCommandButtonHandler.StartProviderDistributionPathEventHandler(MainWindowCommandButtonHandler_StartProviderDistributionPath);
		}

        private void MainWindowCommandButtonHandler_StartProviderDistributionPath(object sender, EventArgs e)
        {
            if (this.m_PathologistUI.AccessionOrder != null)
            {
                YellowstonePathology.UI.Login.FinalizeAccession.ProviderDistributionPath providerDistributionPath = new YellowstonePathology.UI.Login.FinalizeAccession.ProviderDistributionPath(this.m_PathologistUI.PanelSetOrder.ReportNo, this.m_PathologistUI.AccessionOrder, this.m_PathologistUI.ObjectTracker,
                    System.Windows.Visibility.Collapsed, System.Windows.Visibility.Visible, System.Windows.Visibility.Collapsed);
                providerDistributionPath.Start();
            }
        }

		public void PathologistWorkspace_Unloaded(object sender, RoutedEventArgs e)
		{
			this.m_MainWindowCommandButtonHandler.Save -= MainWindowCommandButtonHandler_Save;
			this.m_MainWindowCommandButtonHandler.ToggleAccessionLock -= MainWindowCommandButtonHandler_ToggleAccessionLock;
			this.m_MainWindowCommandButtonHandler.ShowCaseDocument -= MainWindowCommandButtonHandler_ShowCaseDocument;
			this.m_MainWindowCommandButtonHandler.ShowOrderForm -= MainWindowCommandButtonHandler_ShowOrderForm;
			this.m_MainWindowCommandButtonHandler.ShowAmendmentDialog -= MainWindowCommandButtonHandler_ShowAmendmentDialog;
			this.m_MainWindowCommandButtonHandler.AssignCase -= MainWindowCommandButtonHandler_AssignCase;
			this.m_MainWindowCommandButtonHandler.ApplicationClosing -= MainWindowCommandButtonHandler_ApplicationClosing;

			this.Save();

            this.m_MainWindowCommandButtonHandler.StartProviderDistributionPath -= MainWindowCommandButtonHandler_StartProviderDistributionPath;
		}

		private void MainWindowCommandButtonHandler_Save(object sender, EventArgs e)
		{
			this.DisplaySaveYourWorkMessage();
			this.Save();
		}

		private void DisplaySaveYourWorkMessage()
		{
			this.StatusBarTextBlockSaveNotification.Text = "Saving your work.";
			System.Timers.Timer timer = new System.Timers.Timer();
			timer.Interval = 1000;
			timer.Elapsed += new System.Timers.ElapsedEventHandler(ShowSavingWorkMessageTimer_Elapsed);
			timer.Start();
		}

		private void ShowSavingWorkMessageTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			System.Timers.Timer timer = (System.Timers.Timer)sender;
			timer.Stop();

			this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
				new Action(
					delegate()
					{

						this.StatusBarTextBlockSaveNotification.Text = string.Empty;
					}));
		}

		private void MainWindowCommandButtonHandler_ToggleAccessionLock(object sender, EventArgs e)
		{
			if (this.m_PathologistUI.AccessionOrder != null)
			{
				this.Save();
				if (this.m_CytologyResultsWorkspace != null)
				{
					this.m_CytologyResultsWorkspace.AlterAccessionLock(null, null);
				}
				else
				{
					this.m_PathologistUI.AlterAccessionLock();
				}
				this.CheckEnabled();
			}
		}

		private void MainWindowCommandButtonHandler_ShowCaseDocument(object sender, EventArgs e)
		{
            this.ShowCaseDocument();
		}

		private void MainWindowCommandButtonHandler_ShowOrderForm(object sender, EventArgs e)
		{
			if (this.m_PathologistUI.FieldEnabler.IsUnprotectedEnabled == true)
				this.ShowStainOrderForm();
		}

		private void MainWindowCommandButtonHandler_ShowAmendmentDialog(object sender, EventArgs e)
		{
			if (this.m_PathologistUI.Lock.LockAquired)
			{
				this.m_PathologistUI.ShowAmendmentDialog();
			}
		}

		private void MainWindowCommandButtonHandler_AssignCase(object sender, EventArgs e)
		{
			YellowstonePathology.Business.Rules.RuleExecutionStatus ruleExecutionStatus = this.m_PathologistUI.AssignCurrentUser();
			if (ruleExecutionStatus.ExecutionHalted == true)
			{
				YellowstonePathology.UI.RuleExecutionStatusDialog dialog = new RuleExecutionStatusDialog(ruleExecutionStatus);
				dialog.ShowDialog();
			}
			this.m_PathologistUI.DoGenericSearch();
		}

		private void MainWindowCommandButtonHandler_ApplicationClosing(object sender, EventArgs e)
		{
			this.Save();
			this.m_PathologistUI.Lock.ReleaseLock();
		}

		private void ButtonStainOrder_Click(object sender, RoutedEventArgs e)
		{
			this.ShowStainOrderForm();
		}

		private void ShowStainOrderForm()
		{
			this.Save();

			YellowstonePathology.UI.Common.OrderDialog orderDiaglog = new YellowstonePathology.UI.Common.OrderDialog(this.m_PathologistUI.AccessionOrder, this.m_PathologistUI.PanelSetOrder, this.m_SystemIdentity);
			orderDiaglog.ShowDialog();

			this.Save();
			this.LoadData();
		}

		private void ItemIsSelected(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = false;
			if (((TabItem)this.Parent).IsSelected && this.ListViewSearchResults.SelectedItem != null && this.m_PathologistUI.CanPlaceOrder())
			{
				e.CanExecute = true;
			}
		}
		public YellowstonePathology.UI.Surgical.PathologistUI PathologistUI
		{
			get { return this.m_PathologistUI; }
		}

		public void SaveData(object target, ExecutedRoutedEventArgs args)
		{
			this.Save();
			MessageBox.Show("The Pathologist workspace has been saved.");
		}

		public void Save()
		{
			if (this.m_PathologistsReview != null)
			{
				this.m_PathologistUI.Save();
			}
			else if (this.m_CytologyResultsWorkspace != null)
			{
				this.m_CytologyResultsWorkspace.CytologyUI.Save();
			}
		}

		private void CanShowCaseDocument(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = false;
			if (((TabItem)this.Parent).IsSelected)
			{
				if (this.ListViewSearchResults.SelectedItem != null)
				{
					e.CanExecute = true;
				}
			}
		}

		public void ShowCaseDocument()
		{
			this.Save();
			if (this.ListViewSearchResults.SelectedItem != null)
			{
				YellowstonePathology.Business.Search.PathologistSearchResult item = (YellowstonePathology.Business.Search.PathologistSearchResult)this.ListViewSearchResults.SelectedItem;
				YellowstonePathology.UI.CaseDocumentViewer caseDocumentViewer = new CaseDocumentViewer();
				caseDocumentViewer.View(item.MasterAccessionNo, item.ReportNo, item.PanelSetId);
			}
		}

		private void CytologySlideScanReceived(YellowstonePathology.Business.BarcodeScanning.CytycBarcode barcode)
		{
			this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
				new Action(
					delegate()
					{
						if (barcode.IsValidated == true)
						{

							this.Save();
							bool found = false;
							for (int i = 0; i < this.ListViewSearchResults.Items.Count; i++)
							{
								YellowstonePathology.Business.Search.PathologistSearchResult psr = (YellowstonePathology.Business.Search.PathologistSearchResult)this.ListViewSearchResults.Items[i];
								if (psr.ReportNo == barcode.ReportNo)
								{
									this.ListViewSearchResults.SelectedIndex = i;
									found = true;
								}
							}
							if (found == false)
							{
								this.m_PathologistUI.DoReportNoSearch(barcode.ReportNo);
								if (this.m_PathologistUI.Search.Results.Count != 0)
								{
									this.ListViewSearchResults.SelectedIndex = 0;
								}
							}
						}
						else
						{
							System.Windows.MessageBox.Show("The scan did not result in a valid case, please try again.", "Invalid Scan", MessageBoxButton.OK, MessageBoxImage.Exclamation);
						}
					}));
		}

		private void HistologySlideScanReceived(YellowstonePathology.Business.BarcodeScanning.Barcode barcode)
		{
			this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
				new Action(
					delegate()
					{

						if (barcode.IsValidated == true)
						{
							this.Save();
							this.DoSlideOrderIdSearch(barcode.ID);
						}
						else
						{
							MessageBox.Show("The scanner did not read the barcode correctly.  Please try again.");
						}
					}));
		}

		private void BarcodeScanPort_HistologyBlockScanReceived(YellowstonePathology.Business.BarcodeScanning.Barcode barcode)
		{
			this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
				new Action(
					delegate()
					{
						this.Save();
						this.DoAliquotOrderIdSearch(barcode.ID, 13);
					}));
		}

		private void TextBoxSearchANPN_MouseUp(object sender, MouseButtonEventArgs e)
		{
			TextBoxSearchANPN.SelectAll();
		}

		public void TextBoxSearchANPN_KeyUp(object sender, KeyEventArgs args)
		{
			if (args.Key == Key.Return)
			{
				if (this.TextBoxSearchANPN.Text.Length >= 1)
				{
					this.m_PathologistUI.Save();
					TextSearchHandler textSearchHandler = new TextSearchHandler(this.TextBoxSearchANPN.Text);
					object textSearchObject = textSearchHandler.GetSearchObject();
					if (textSearchObject is YellowstonePathology.Business.ReportNo)
					{
						YellowstonePathology.Business.ReportNo reportNo = (YellowstonePathology.Business.ReportNo)textSearchObject;
						this.m_PathologistUI.DoReportNoSearch(reportNo.Value);
					}
					else if (textSearchObject is YellowstonePathology.Business.MasterAccessionNo)
					{
						YellowstonePathology.Business.MasterAccessionNo masterAccessionNo = (YellowstonePathology.Business.MasterAccessionNo)textSearchObject;
						this.m_PathologistUI.DoMasterAccessionNoSearch(masterAccessionNo.Value);
					}
					else if (textSearchObject is YellowstonePathology.Business.PatientName)
					{
						YellowstonePathology.Business.PatientName patientName = (YellowstonePathology.Business.PatientName)textSearchObject;
						this.m_PathologistUI.DoPatientNameSearch(patientName.FirstName, patientName.LastName);
					}
				}

				if (this.ListViewSearchResults.Items.Count == 1)
				{
					this.ListViewSearchResults.SelectedIndex = 0;
				}
			}
		}

		private void DoSlideOrderIdSearch(string slideOrderId)
		{
            YellowstonePathology.Business.Search.PathologistSearchResultCollection pathologistSearchResultCollection = this.m_PathologistUI.DoSlideOrderIdSearch(slideOrderId);
            if (pathologistSearchResultCollection.Count != 0)
            {
                YellowstonePathology.Business.Search.PathologistSearchResult pathologistSearchResult = pathologistSearchResultCollection.GetPrimaryResult();
                for (int i = 0; i < this.ListViewSearchResults.Items.Count; i++)
                {
                    YellowstonePathology.Business.Search.PathologistSearchResult psr = (YellowstonePathology.Business.Search.PathologistSearchResult)this.ListViewSearchResults.Items[i];
                    if (psr.ReportNo == pathologistSearchResult.ReportNo)
                    {
                        this.ListViewSearchResults.SelectedIndex = i;                 
                        break;
                    }
                }                
            }
            else
            {
                MessageBox.Show("We were unable to find the scanned slide in the database.");
            }
		}

		private void DoAliquotOrderIdSearch(string aliquotOrderId, int panelSetId)
		{            
			YellowstonePathology.Business.Search.PathologistSearchResult result = this.m_PathologistUI.DoAliquotOrderIdSearch(aliquotOrderId, panelSetId);
			if (result != null)
			{
				for (int i = 0; i < this.ListViewSearchResults.Items.Count; i++)
				{
					YellowstonePathology.Business.Search.PathologistSearchResult psr = (YellowstonePathology.Business.Search.PathologistSearchResult)this.ListViewSearchResults.Items[i];
					if (psr.ReportNo == result.ReportNo)
					{
						this.ListViewSearchResults.SelectedIndex = i;
					}
				}
			}
		}

		private void DoCytologyReportNoSearch(string reportNo)
		{

		}

		private void ListViewSearchResults_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			this.Save();
			LoadData();
		}

		private void LoadData()
		{
			if (this.ListViewSearchResults.SelectedItem != null)
			{
				YellowstonePathology.Business.Search.PathologistSearchResult item = (YellowstonePathology.Business.Search.PathologistSearchResult)this.ListViewSearchResults.SelectedItem;
				if (item.PanelSetId == 15)
				{
					this.m_PathologistUI.GetAccessionOrderByReportNo(item.ReportNo);
				}
				else
				{
					this.m_PathologistUI.GetAccessionOrder(item.MasterAccessionNo, item.ReportNo);
				}
				this.SetReviewResult();
			}
		}

		private void SetReviewResult()
		{
			switch (this.m_PathologistUI.PanelSetOrder.PanelSetId)
			{                
				case 15:
					this.m_PathologistsReview = null;
					this.m_CytologyResultsWorkspace = new Cytology.CytologyResultsWorkspace();
					this.m_CytologyResultsWorkspace.CytologyUI.SetAccessionOrder(this.m_PathologistUI.AccessionOrder, this.m_PathologistUI.PanelSetOrder.ReportNo);
					this.m_CytologyResultsWorkspace.CytologyUI.Lock = this.m_PathologistUI.Lock;
					this.m_CytologyResultsWorkspace.CytologyUI.ObjectTracker = this.m_PathologistUI.ObjectTracker;
					this.m_CytologyResultsWorkspace.SetReportNo(this.m_PathologistUI.PanelSetOrder.ReportNo);

					YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection clientOrderCollection = YellowstonePathology.Business.Gateway.ClientOrderGateway.GetClientOrdersByMasterAccessionNo(this.m_PathologistUI.AccessionOrder.MasterAccessionNo);
					this.m_CytologyResultsWorkspace.CytologyUI.DataLoadResult.DataLoadStatusEnum = YellowstonePathology.Business.Domain.DataLoadStatusEnum.Successful;
					switch (clientOrderCollection.Count)
					{
						case 0:
							this.m_CytologyResultsWorkspace.CytologyUI.ClientOrder = null;
							this.m_CytologyResultsWorkspace.CytologyUI.DataLoaded();
							break;
						case 1:
						case 2:
							this.m_CytologyResultsWorkspace.CytologyUI.ClientOrder = clientOrderCollection[0];
							this.m_CytologyResultsWorkspace.CytologyUI.DataLoaded();
							break;
					}

					this.ContentControlReview.Content = this.m_CytologyResultsWorkspace;
					this.m_CytologyResultsWorkspace.SelectAppropriatePanel();
					break;
				default:
					this.m_CytologyResultsWorkspace = null;
					this.m_PathologistsReview = new PathologistsReview(this.m_PathologistUI, this.m_SystemIdentity);
					this.ContentControlReview.Content = this.m_PathologistsReview;
					break;
			}
		}

		public Cytology.CytologyResultsWorkspace CytologyResultsWorkspace
		{
			get { return this.m_CytologyResultsWorkspace; }
		}

		private void ButtonSearch_Click(object sender, RoutedEventArgs e)
		{
			if (this.ListViewSearchResults.SelectedItem != null)
			{
				this.m_PathologistUI.DoPatientIdSearch((YellowstonePathology.Business.Search.PathologistSearchResult)this.ListViewSearchResults.SelectedItem);
			}
		}

		public void DoSearch()
		{
			this.m_PathologistUI.DoGenericSearch();
		}

		private void CheckEnabled()
		{
			this.m_PathologistUI.CheckEnabled();
		}

		private void Grid_PreviewKeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.PageUp || e.Key == Key.Prior)
			{
				e.Handled = true;
				if (this.ListViewSearchResults.SelectedIndex > 0)
				{
					this.ListViewSearchResults.SelectedIndex = this.ListViewSearchResults.SelectedIndex - 1;
				}
			}
			else if (e.Key == Key.PageDown || e.Key == Key.Next)
			{
				e.Handled = true;
				if (this.ListViewSearchResults.SelectedIndex < this.ListViewSearchResults.Items.Count)
				{
					this.ListViewSearchResults.SelectedIndex = this.ListViewSearchResults.SelectedIndex + 1;
				}
			}
		}

		private void GridPathologist_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.F7)
			{
				TraversalRequest traversalRequestNext = new TraversalRequest(FocusNavigationDirection.Next);				
				UIElement keyboardFocus = Keyboard.FocusedElement as UIElement;
				
				if(keyboardFocus != null)
				{
					keyboardFocus.MoveFocus(traversalRequestNext);
				}						
				
				this.m_PathologistUI.SpellCheckCurrentItem();										
				e.Handled = true;
			}
		}

		private void PathologistUserControl_Loaded(object sender, RoutedEventArgs e)
		{
			if (this.m_PathologistUI != null)
			{
				if (!m_Loaded)
				{
					this.comboBoxSearchPathologistUser.SelectionChanged += this.comboBoxSearchPathologistUser_SelectionChanged;
					this.comboPanelSetType.SelectionChanged += this.comboPanelSetType_SelectionChanged;
					if (this.m_SystemIdentity.User.IsUserInRole(Business.User.SystemUserRoleDescriptionEnum.Pathologist))
					{
						this.ComboFinal.SelectionChanged += this.ComboFinal_SelectionChanged;
						this.ComboFinal.SelectedIndex = 0;
					}
					else
					{
						this.ComboFinal.SelectedIndex = 0;
						this.ComboFinal.SelectionChanged += this.ComboFinal_SelectionChanged;
					}
					m_Loaded = true;
				}

				if (string.IsNullOrEmpty(this.m_PathologistUI.ReportNo) == false)
				{
					if (this.m_PathologistUI.Lock.LockingMode == Business.Domain.LockModeEnum.AlwaysAttemptLock && !this.m_PathologistUI.Lock.LockAquired)
					{
						this.m_PathologistUI.Lock.GetLock();
					}
				}
				((MainWindow)Application.Current.MainWindow).SetLockObject(this.m_PathologistUI.Lock);
			}
		}

		private void ButtonRefresh_Click(object sender, RoutedEventArgs e)
		{
			this.m_PathologistUI.DoGenericSearch();
		}

		public void ButtonViewDocument_Click(object sender, RoutedEventArgs args)
		{
			this.ShowCaseDocument();
		}

		private void Any_RequestBringIntoView(object sender, RequestBringIntoViewEventArgs e)
		{
			e.Handled = true;
		}

		private void HyperlinkAcceptPeerReview_Click(object sender, RoutedEventArgs e)
		{
			Hyperlink ctrl = (Hyperlink)sender;
			YellowstonePathology.Business.Test.PanelOrder panelOrder = (YellowstonePathology.Business.Test.PanelOrder)ctrl.Tag;

			if (panelOrder.Accepted == true)
			{
				panelOrder.Accepted = false;
				panelOrder.AcceptedDate = null;
				panelOrder.AcceptedTime = null;
				panelOrder.AcceptedById = 0;
			}
			else
			{
				panelOrder.Accepted = true;
				panelOrder.AcceptedDate = DateTime.Today;
				panelOrder.AcceptedTime = DateTime.Now;
				panelOrder.AcceptedById = this.m_SystemIdentity.User.UserId;
			}
		}

		private void ComboFinal_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (this.ComboFinal.SelectedItem != null)
			{
				this.m_PathologistUI.DoGenericSearch();
			}
		}

		private void comboPanelSetType_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (this.comboPanelSetType.SelectedItem != null)
			{
				this.m_PathologistUI.DoGenericSearch();
			}
		}

		private void comboBoxSearchPathologistUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (this.comboBoxSearchPathologistUser.SelectedItem != null)
			{
				this.m_PathologistUI.DoGenericSearch();
			}
		}

		private void ButtonRedoSearch_Click(object sender, RoutedEventArgs e)
		{
			this.comboBoxSearchPathologistUser.SelectionChanged -= this.comboBoxSearchPathologistUser_SelectionChanged;
			this.comboPanelSetType.SelectionChanged -= this.comboPanelSetType_SelectionChanged;
			this.ComboFinal.SelectionChanged -= this.ComboFinal_SelectionChanged;

			this.comboBoxSearchPathologistUser.SelectedIndex = 1;
			if (this.m_SystemIdentity.User.IsUserInRole(YellowstonePathology.Business.User.SystemUserRoleDescriptionEnum.Pathologist))
			{
				for (int idx = 2; idx < this.comboBoxSearchPathologistUser.Items.Count; idx++)
				{
					if (((YellowstonePathology.Business.User.SystemUser)this.comboBoxSearchPathologistUser.Items[idx]).UserId == this.m_SystemIdentity.User.UserId)
					{
						this.comboBoxSearchPathologistUser.SelectedIndex = idx;
						break;
					}
				}
			}

			this.comboPanelSetType.SelectedIndex = 0;
			this.ComboFinal.SelectedIndex = 0;
			this.m_PathologistUI.DoGenericSearch();

			this.comboBoxSearchPathologistUser.SelectionChanged += this.comboBoxSearchPathologistUser_SelectionChanged;
			this.comboPanelSetType.SelectionChanged += this.comboPanelSetType_SelectionChanged;
			this.ComboFinal.SelectionChanged += this.ComboFinal_SelectionChanged;
		}

		private void ButtonReportOrder_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder = this.m_PathologistUI.AccessionOrder;
			YellowstonePathology.Business.ClientOrder.Model.ClientOrderCollection clientOrderCollection = YellowstonePathology.Business.Gateway.ClientOrderGateway.GetClientOrdersByMasterAccessionNo(accessionOrder.MasterAccessionNo);

			if (clientOrderCollection.Count != 0)
			{
				this.m_PathologistUI.ObjectTracker.RegisterObject(clientOrderCollection[0]);
				Login.Receiving.AccessionOrderPath accessionOrderPath = new Login.Receiving.AccessionOrderPath(accessionOrder, clientOrderCollection[0], this.m_PathologistUI.ObjectTracker, PageNavigationModeEnum.Standalone);
				accessionOrderPath.Start();
				this.m_PathologistUI.ObjectTracker.Deregister(clientOrderCollection[0]);
			}
			else
			{
				MessageBox.Show("No Client Order was found.  Please contact Sid.");
			}
		}

		private void HyperLinkOpenDocumentFolder_Click(object sender, RoutedEventArgs e)
		{
			if (this.m_PathologistUI.AccessionOrder != null)
			{
				YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_PathologistUI.ReportNo);
				string folderPath = YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser);
				System.Diagnostics.Process process = new System.Diagnostics.Process();
				System.Diagnostics.Process p = new System.Diagnostics.Process();
				System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo("Explorer.exe", folderPath);
				p.StartInfo = info;
				p.Start();
			}
		}

        private void ButtonPeerReview_Click(object sender, RoutedEventArgs e)
        {            
            if (this.m_PathologistUI.AccessionOrder.PanelSetOrderCollection.HasSurgical() == true)
            {
                YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder = (YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder)this.m_PathologistUI.AccessionOrder.PanelSetOrderCollection.GetSurgical();
                PeerReviewDialog peerReviewDialog = new PeerReviewDialog();
                PeerReviewResultPage peerReviewResultPage = new PeerReviewResultPage(surgicalTestOrder, this.m_PathologistUI.AccessionOrder, this.m_PathologistUI.ObjectTracker, this.m_SystemIdentity);
                peerReviewDialog.PageNavigator.Navigate(peerReviewResultPage);
                peerReviewDialog.ShowDialog();
            }
            else
            {
                MessageBox.Show("Only cases with Surgical Pathology can have Peer Review");
            }            
        }

        private void MenuItemShowAllTestsForThisCase_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewSearchResults.SelectedItem != null)
            {
                YellowstonePathology.Business.Search.PathologistSearchResult item = (YellowstonePathology.Business.Search.PathologistSearchResult)this.ListViewSearchResults.SelectedItem;
                this.m_PathologistUI.DoMasterAccessionNoSearch(item.MasterAccessionNo);
            }
        }
	}
}
