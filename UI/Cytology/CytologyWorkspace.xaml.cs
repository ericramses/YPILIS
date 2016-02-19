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
    public partial class CytologyWorkspace : UserControl
    {                
		public delegate void CytologyReportNoChanged();
        
        public CommandBinding CommandBindingPatientLinking;
        public CommandBinding CommandBindingShowCaseDocument;        
		public CommandBinding CommandBindingApplicationClosing;
		public CommandBinding CommandBindingShowPatientEditDialog;
		public CommandBinding CommandBindingShowBillingEditDialog;
		public CommandBinding CommandBindingShowAmendmentDialog;
        
		private UI.Cytology.CytologyResultsWorkspace m_CytologyResultsWorkspace;

        private CytologyUI m_CytologyUI;
        private YellowstonePathology.UI.DocumentWorkspace m_DocumentViewer;

		private YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort m_BarcodeScanPort;
		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;

        private YellowstonePathology.UI.LabEventsControlTab m_LabEventsControlTab;
        private MainWindowCommandButtonHandler m_MainWindowCommandButtonHandler;
        private TabItem m_Writer;

        public CytologyWorkspace(MainWindowCommandButtonHandler mainWindowCommandButtonHandler, TabItem writer)
        {
            this.m_MainWindowCommandButtonHandler = mainWindowCommandButtonHandler;
            this.m_SystemIdentity = YellowstonePathology.Business.User.SystemIdentity.Instance;
            this.m_BarcodeScanPort = YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort.Instance;

            this.m_Writer = writer;
            this.m_CytologyUI = new CytologyUI(this.m_SystemIdentity, this.m_Writer);
			this.m_CytologyResultsWorkspace = new CytologyResultsWorkspace(this.m_CytologyUI);
			this.m_CytologyUI.AccessionChanged += new CytologyUI.AccessionChangedEventHandler(CytologyUI_AccessionChanged);
            
            this.CommandBindingShowCaseDocument = new CommandBinding(MainWindow.ShowCaseDocumentCommand, this.m_CytologyUI.ShowCaseDocument);            
			this.CommandBindingApplicationClosing = new CommandBinding(MainWindow.ApplicationClosingCommand, this.CloseWorkspace);
			this.CommandBindingShowPatientEditDialog = new CommandBinding(MainWindow.ShowPatientEditDialogCommand, this.m_CytologyUI.ShowPatientEditDialog);
			this.CommandBindingShowAmendmentDialog = new CommandBinding(MainWindow.ShowAmendmentDialogCommand, this.m_CytologyUI.ShowAmendmentDialog, ItemIsSelected);
                        
            this.CommandBindings.Add(this.CommandBindingShowCaseDocument);			
			this.CommandBindings.Add(this.CommandBindingApplicationClosing);
			this.CommandBindings.Add(this.CommandBindingShowPatientEditDialog);
			this.CommandBindings.Add(this.CommandBindingShowAmendmentDialog);

            this.m_DocumentViewer = new DocumentWorkspace();
            
            InitializeComponent();

            this.DataContext = this.m_CytologyUI;
            this.ContentControlDocumentViewer.Content = this.m_DocumentViewer;
			this.m_BarcodeScanPort.CytologySlideScanReceived += new YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort.CytologySlideScanReceivedHandler(CytologySlideScanReceived);
			this.m_BarcodeScanPort.ThinPrepSlideScanReceived += new Business.BarcodeScanning.BarcodeScanPort.ThinPrepSlideScanReceivedHandler(BarcodeScanPort_ThinPrepSlideScanReceived);

            this.Loaded += new RoutedEventHandler(CytologyWorkspace_Loaded);
            this.Unloaded += new RoutedEventHandler(CytologyWorkspace_Unloaded);

            this.ResultsGrid.Children.Add(this.m_CytologyResultsWorkspace);
            this.m_LabEventsControlTab = new LabEventsControlTab(this.m_SystemIdentity);
            this.TabControlLeft.Items.Add(this.m_LabEventsControlTab);
        }

        private void CytologyWorkspace_Unloaded(object sender, RoutedEventArgs e)
        {
            this.m_MainWindowCommandButtonHandler.StartProviderDistributionPath -= MainWindowCommandButtonHandler_StartProviderDistributionPath;
            this.m_MainWindowCommandButtonHandler.Save -= MainWindowCommandButtonHandler_Save;
            this.m_MainWindowCommandButtonHandler.ShowAmendmentDialog -= this.m_CytologyResultsWorkspace.CytologyUI.ShowAmendmentDialog;
            this.m_MainWindowCommandButtonHandler.Refresh -= MainWindowCommandButtonHandler_Refresh;
        }

        private void BarcodeScanPort_ThinPrepSlideScanReceived(Business.BarcodeScanning.Barcode barcode)
        {
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                new Action(
                    delegate()
                    {
                        YellowstonePathology.UI.MainWindow mainWindow = Application.Current.MainWindow as YellowstonePathology.UI.MainWindow;
                        if (((TabItem)mainWindow.TabControlLeftWorkspace.SelectedItem).Tag.ToString() == "Cytology")
                        {
                            if (barcode.IsValidated == true)
                            {                                
                                if (this.m_CytologyUI.SetAccessionOrderByAliquotOrderId(barcode.ID))
                                {
									this.m_CytologyResultsWorkspace.ReportNo = this.m_CytologyUI.PanelSetOrderCytology.ReportNo;
                                }
                            }
                            else
                            {
                                System.Windows.MessageBox.Show("The scanner did not read the label correctly.", "Scan not successful.", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                            }
                        }
                    }));     
        }

        private void CytologyWorkspace_Loaded(object sender, RoutedEventArgs e)
        {            
            this.ComboBoxSearchType.SelectedIndex = 0;
            this.ComboBoxScreenerSelection.SelectedIndex = this.m_CytologyUI.GetScreenerIndex();

            this.m_MainWindowCommandButtonHandler.StartProviderDistributionPath += new MainWindowCommandButtonHandler.StartProviderDistributionPathEventHandler(MainWindowCommandButtonHandler_StartProviderDistributionPath);
            this.m_MainWindowCommandButtonHandler.Save += new MainWindowCommandButtonHandler.SaveEventHandler(MainWindowCommandButtonHandler_Save);
            this.m_MainWindowCommandButtonHandler.ShowAmendmentDialog += this.m_CytologyResultsWorkspace.CytologyUI.ShowAmendmentDialog;
            this.m_MainWindowCommandButtonHandler.Refresh += MainWindowCommandButtonHandler_Refresh;
            this.ListViewSearchResults.SelectedIndex = -1;

            Keyboard.Focus(this.m_CytologyResultsWorkspace.TextBoxReportNoSearch);
        }

        private void MainWindowCommandButtonHandler_Refresh(object sender, EventArgs e)
        {
        }

        private void MainWindowCommandButtonHandler_Save(object sender, EventArgs e)
        {
            if (this.m_CytologyUI.AccessionOrder != null)
            {
                MainWindow.MoveKeyboardFocusNextThenBack();
                YellowstonePathology.Business.Persistence.DocumentGateway.Instance.ReleaseLock(this.m_CytologyUI.AccessionOrder, this.m_Writer);
                if (this.m_CytologyUI.AccessionOrder.IsLockAquiredByMe == false)
                {
                    this.m_CytologyUI.NotifyPropertyChanged(string.Empty);
                }
            }
        }

        private void MainWindowCommandButtonHandler_StartProviderDistributionPath(object sender, EventArgs e)
        {
            if (this.m_CytologyUI.AccessionOrder != null)
            {
                YellowstonePathology.UI.Login.FinalizeAccession.ProviderDistributionPath providerDistributionPath =
					new YellowstonePathology.UI.Login.FinalizeAccession.ProviderDistributionPath(this.m_CytologyUI.PanelSetOrderCytology.ReportNo, this.m_CytologyUI.AccessionOrder,
                    System.Windows.Visibility.Collapsed, System.Windows.Visibility.Visible, System.Windows.Visibility.Collapsed);
                providerDistributionPath.Start();
            }
        }

		private void CytologySlideScanReceived(YellowstonePathology.Business.BarcodeScanning.CytycBarcode cytycBarcode)
        {            
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                new Action(
                    delegate()
                    {
                        YellowstonePathology.UI.MainWindow mainWindow = Application.Current.MainWindow as YellowstonePathology.UI.MainWindow;
                        if (((TabItem)mainWindow.TabControlLeftWorkspace.SelectedItem).Tag.ToString() == "Cytology")
                        {

                            if (cytycBarcode.IsValidated == true)
							{
                                if (this.m_CytologyUI.SetAccessionOrderByReportNo(cytycBarcode.ReportNo))
								{
                                    this.m_CytologyResultsWorkspace.ReportNo = cytycBarcode.ReportNo;									
								}
							}
							else
							{
								System.Windows.MessageBox.Show("The scanner did not read the label correctly.", "Scan not successful.", MessageBoxButton.OK, MessageBoxImage.Exclamation);
							}
                        }
                    }));            
        }        		

		private void CytologyUI_AccessionChanged(object sender, EventArgs e)
		{
			this.m_CytologyResultsWorkspace.TextBoxReportNoSearch.Text = this.m_CytologyUI.PanelSetOrderCytology.ReportNo;
			YellowstonePathology.Business.Document.CaseDocumentCollection caseDocumentCollection = new Business.Document.CaseDocumentCollection(this.m_CytologyUI.AccessionOrder, this.m_CytologyUI.PanelSetOrderCytology.ReportNo);
			this.m_DocumentViewer.ShowDocument(caseDocumentCollection.GetFirstRequisition());			
			this.m_LabEventsControlTab.SetCurrentOrder(this.m_CytologyUI.AccessionOrder);
            this.m_CytologyResultsWorkspace.SelectAppropriatePanel();
		}

		public void Save(object target, ExecutedRoutedEventArgs args)
        {
            IInputElement focusedElement = Keyboard.FocusedElement;
            FrameworkElement element = (FrameworkElement)focusedElement;
            element.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));      
            this.m_CytologyUI.Save(false);
        }       

        public void StartProviderDistributionPath(object target, ExecutedRoutedEventArgs args)
        {
            string reportNo = this.m_CytologyUI.PanelSetOrderCytology.ReportNo;
            YellowstonePathology.UI.Login.FinalizeAccession.ProviderDistributionPath providerDistributionPath = new Login.FinalizeAccession.ProviderDistributionPath(reportNo, this.m_CytologyUI.AccessionOrder,
                System.Windows.Visibility.Collapsed, System.Windows.Visibility.Visible, System.Windows.Visibility.Collapsed);
            providerDistributionPath.Start();
        }

        public UI.Cytology.CytologyResultsWorkspace CytologyResultsWorkspace
		{
			get { return this.m_CytologyResultsWorkspace; }
		}

        private void ListViewSearchResult_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
			DateTime hit = DateTime.Now;
            if (this.ListViewSearchResults.SelectedItems.Count != 0)
            {
                YellowstonePathology.Business.Search.CytologyScreeningSearchResult selectedSearchResult = (YellowstonePathology.Business.Search.CytologyScreeningSearchResult)this.ListViewSearchResults.SelectedItem;                
                this.m_CytologyUI.SetAccessionOrder(selectedSearchResult);				
			}
			DateTime done = DateTime.Now;
			TimeSpan elapsed = done - hit;
			Console.WriteLine(" *** ElapsedTime = " + elapsed.TotalMilliseconds.ToString() + " ***");
        }
                
		private void ButtonGetList_Click(object sender, RoutedEventArgs e)
		{
            if (this.ComboBoxSearchType.SelectedItem != null)
            {
                string selectedSearch = (string)this.ComboBoxSearchType.SelectedItem;
				int screenedById = 0;
				if (this.ComboBoxScreenerSelection.SelectedItem != null)
				{
					screenedById = (int)this.ComboBoxScreenerSelection.SelectedValue;
				}
                this.m_CytologyUI.DoListSearch(selectedSearch, screenedById);

                this.TextBlockCount.Text = "Count: " + this.m_CytologyUI.Search.Results.Count.ToString();                
            }
        }		

		public void CloseWorkspace(object target, ExecutedRoutedEventArgs args)
		{
			this.m_CytologyUI.Save(true);
        }

        private void ButtonAssignTo_Click(object sender, RoutedEventArgs e)
        {
			YellowstonePathology.Business.User.SystemUser systemUser = (YellowstonePathology.Business.User.SystemUser)this.ComboBoxAssignedToSelection.SelectedItem;
			System.Windows.MessageBoxResult result = System.Windows.MessageBox.Show("Assign selected cases to " + systemUser.DisplayName + "?", "Assign selected cases?", MessageBoxButton.OKCancel);
            if (result == MessageBoxResult.OK)
            {
                List<YellowstonePathology.Business.Search.CytologyScreeningSearchResult> cytologyScreeningSearchResult = new List<YellowstonePathology.Business.Search.CytologyScreeningSearchResult>();
                foreach (YellowstonePathology.Business.Search.CytologyScreeningSearchResult listItem in this.ListViewSearchResults.SelectedItems)
                {
                    cytologyScreeningSearchResult.Add(listItem);
                }
                if (cytologyScreeningSearchResult.Count > 0)
                {
                    this.m_CytologyUI.AssignScreenings(cytologyScreeningSearchResult, systemUser);
                }
                else
                {
                    System.Windows.MessageBox.Show("No items selected to assign.");
                }
            }
        }

        private void ButtonPrintList_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.UI.Cytology.ScreeningReport screeningReport;

            if (this.ListViewSearchResults.SelectedItems.Count != 0)
            {
                List<YellowstonePathology.Business.Search.CytologyScreeningSearchResult> resultList = new List<YellowstonePathology.Business.Search.CytologyScreeningSearchResult>();
                foreach (YellowstonePathology.Business.Search.CytologyScreeningSearchResult item in this.ListViewSearchResults.SelectedItems)
                {
                    resultList.Add(item);
                }
                screeningReport = new ScreeningReport(resultList);                
            }
            else
            {
                screeningReport = new ScreeningReport(this.m_CytologyUI.Search.Results);                
            }

            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() != true) return;
            printDialog.PrintDocument(screeningReport.DocumentPaginator, "Screening Report");
        }

		public void ItemIsSelected(object sender, CanExecuteRoutedEventArgs e)
		{
			e.CanExecute = false;
			if (((TabItem)this.Parent).IsSelected && this.m_CytologyUI.CanSave)
			{
				e.CanExecute = true;
			}
		}
	}
}
