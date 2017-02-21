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

namespace YellowstonePathology.UI
{    
    public partial class AssignmentWorkspace : UserControl
    {        
        private MainWindowCommandButtonHandler m_MainWindowCommandButtonHandler;
        private TabItem m_Writer;
        private bool m_LoadedHasRun;
        private Login.Receiving.LoginPageWindow m_LoginPageWindow;
        private YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort m_BarcodeScanPort;
        private AssignmentScanCollection m_AssignmentScanCollection;
        private Business.User.SystemUserCollection m_SystemUsers;

        public AssignmentWorkspace(MainWindowCommandButtonHandler mainWindowCommandButtonHandler, TabItem writer)
        {
            this.m_SystemUsers = Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection;
            this.m_AssignmentScanCollection = new AssignmentScanCollection();
            this.m_LoadedHasRun = false;
            this.m_MainWindowCommandButtonHandler = mainWindowCommandButtonHandler;            
            this.m_Writer = writer;

            InitializeComponent();

            this.DataContext = this;            

            this.Loaded += new RoutedEventHandler(LoginWorkspace_Loaded);
            this.Unloaded += new RoutedEventHandler(LoginWorkspace_Unloaded);
        }

        private void LoginWorkspace_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.m_LoadedHasRun == false)
            {                
                this.m_MainWindowCommandButtonHandler.RemoveTab += MainWindowCommandButtonHandler_RemoveTab;

                this.m_BarcodeScanPort = YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort.Instance;
                this.m_BarcodeScanPort.HistologySlideScanReceived += new Business.BarcodeScanning.BarcodeScanPort.HistologySlideScanReceivedHandler(HistologySlideScanReceived);
                this.m_BarcodeScanPort.HistologyBlockScanReceived += new Business.BarcodeScanning.BarcodeScanPort.HistologyBlockScanReceivedHandler(BarcodeScanPort_HistologyBlockScanReceived);
                this.m_BarcodeScanPort.ContainerScanReceived += new Business.BarcodeScanning.BarcodeScanPort.ContainerScanReceivedHandler(BarcodeScanPort_ContainerScanReceived);                
                this.m_BarcodeScanPort.ThinPrepSlideScanReceived += BarcodeScanPort_ThinPrepSlideScanReceived;
            }
            this.m_LoadedHasRun = true;
        }

        public AssignmentScanCollection AssignmentScanCollection
        {
            get { return this.m_AssignmentScanCollection; }
        }

        public Business.User.SystemUserCollection SystemUsers
        {
            get { return this.m_SystemUsers; }
        }

        private void HistologySlideScanReceived(YellowstonePathology.Business.BarcodeScanning.Barcode barcode)
        {
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                new Action(
                    delegate ()
                    {
                        if(this.ListViewUsers.SelectedItem != null)
                        {
                            if(this.m_AssignmentScanCollection.Exists(barcode.ID) == false)
                            {
                                Business.User.SystemUser assignTo = (Business.User.SystemUser)this.ListViewUsers.SelectedItem;
                                AssignmentScan assigmentScan = new AssignmentScan(barcode, assignTo, Business.User.SystemIdentity.Instance.User);
                                this.m_AssignmentScanCollection.Add(assigmentScan);
                            }                            
                        }
                        else
                        {
                            MessageBox.Show("Please select a user for assignment.");
                        }                        
                    }));
        }

        private void BarcodeScanPort_HistologyBlockScanReceived(Business.BarcodeScanning.Barcode barcode)
        {
            /*
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                new Action(
                    delegate ()
                    {
                        if (this.m_MaterialTrackingLogViewCollection.MaterialIdExists(barcode.ID) == false)
                        {
                            YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLog materialTrackingLog = this.AddMaterialTrackingLogScan(barcode.ID, "Aliquot", this.m_MaterialTrackingBatch.MaterialTrackingBatchId);
                            YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogView materialTrackingLogView = new Business.MaterialTracking.Model.MaterialTrackingLogView();
                            materialTrackingLogView.FromScannedItemView(materialTrackingLog);
                            this.m_MaterialTrackingLogViewCollection.Add(materialTrackingLogView);
                            this.NotifyPropertyChanged("MaterialCount");
                        }
                        else
                        {
                            this.SelectMaterialTrackingLogView(barcode.ID);
                        }
                    }));
                    */
        }        

        private void BarcodeScanPort_ThinPrepSlideScanReceived(Business.BarcodeScanning.Barcode barcode)
        {
            /*
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                new Action(
                    delegate ()
                    {
                        if (this.m_MaterialTrackingLogViewCollection.MaterialIdExists(barcode.ID) == false)
                        {
                            YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLog materialTrackingLog = this.AddMaterialTrackingLogScan(barcode.ID, "PSLD", this.m_MaterialTrackingBatch.MaterialTrackingBatchId);
                            YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogView materialTrackingLogView = new Business.MaterialTracking.Model.MaterialTrackingLogView();
                            materialTrackingLogView.FromScannedItemView(materialTrackingLog);
                            this.m_MaterialTrackingLogViewCollection.Add(materialTrackingLogView);
                            this.NotifyPropertyChanged("MaterialCount");
                        }
                        else
                        {
                            this.SelectMaterialTrackingLogView(barcode.ID);
                        }
                    }));
                    */
        }

        private void BarcodeScanPort_ContainerScanReceived(Business.BarcodeScanning.ContainerBarcode containerBarcode)
        {
            /*
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                new Action(
                    delegate ()
                    {
                        if (this.m_MaterialTrackingLogViewCollection.MaterialIdExists(containerBarcode.ID) == false)
                        {
                            YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLog materialTrackingLog = this.AddMaterialTrackingLogScan(containerBarcode.ID, "Container", this.m_MaterialTrackingBatch.MaterialTrackingBatchId);
                            YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLogView materialTrackingLogView = new Business.MaterialTracking.Model.MaterialTrackingLogView();
                            materialTrackingLogView.FromScannedItemView(materialTrackingLog);
                            this.m_MaterialTrackingLogViewCollection.Add(materialTrackingLogView);
                            this.NotifyPropertyChanged("MaterialCount");
                        }
                        else
                        {
                            this.SelectMaterialTrackingLogView(containerBarcode.ID);
                        }
                    }));
                    */
        }

        private void MainWindowCommandButtonHandler_RemoveTab(object sender, EventArgs e)
        {
            Business.Persistence.DocumentGateway.Instance.Push(this.m_Writer);
        }        

        private void LoginWorkspace_Unloaded(object sender, RoutedEventArgs e)
        {
            this.m_LoadedHasRun = false;            
        }                

        private void TaskOrderPath_Close(object sender, EventArgs e)
        {
            this.m_LoginPageWindow.Close();
        }

        private void ButtonAssign_Click(object sender, RoutedEventArgs e)
        {
            foreach(AssignmentScan assignmentScan in this.m_AssignmentScanCollection)
            {
                string masterAccessionNo = Business.Gateway.AccessionOrderGateway.GetMasterAccessionNoFromAliquotOrderId(assignmentScan.ScanId);
                if(string.IsNullOrEmpty(masterAccessionNo) == false)
                {
                    Business.Test.AccessionOrder accessionOrder = Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(masterAccessionNo, this);
                    if(accessionOrder.AccessionLock.IsLockAquiredByMe == true)
                    {
                        if (accessionOrder.PanelSetOrderCollection.HasSurgical() == true)
                        {
                            Business.Test.PanelSetOrder panelSetOrder = accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(masterAccessionNo + ".S");
                            if (panelSetOrder.AssignedToId != 0)
                            {
                                panelSetOrder.AssignedToId = assignmentScan.AssignedToId;
                                assignmentScan.Assigned = true;
                            }
                            else
                            {
                                assignmentScan.Comment = "This case is already assigned.";
                            }
                        }
                        else
                        {
                            assignmentScan.Comment = "This process currently only works for Surgicals.";
                        }
                    }
                    else
                    {
                        assignmentScan.Comment = "The lock is currently held by: " + accessionOrder.AccessionLock.Address;
                    }
                    
                }
                else
                {
                    assignmentScan.Comment = "Couldn't find master accessionno for this scan.";
                }
            }
        }

        private void MenuItemDeleteScan_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
