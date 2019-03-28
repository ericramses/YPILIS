using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;

namespace YellowstonePathology.UI.Stain
{
    /// <summary>
    /// Interaction logic for PathologistsScanDialog.xaml
    /// </summary>
    public partial class PathologistsScanDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort m_BarcodeScanPort;
        private YellowstonePathology.Business.Surgical.AssignmentScanCollection m_AssignmentScanCollection;

        private List<string> m_SlideOrderIds;
        private int idx;

        public PathologistsScanDialog()
        {
            this.m_AssignmentScanCollection = new Business.Surgical.AssignmentScanCollection();

            //this.AddTestIds();

            InitializeComponent();
            DataContext = this;

            this.m_BarcodeScanPort = YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort.Instance;
            this.m_BarcodeScanPort.HistologySlideScanReceived += new Business.BarcodeScanning.BarcodeScanPort.HistologySlideScanReceivedHandler(HistologySlideScanReceived);

        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public YellowstonePathology.Business.Surgical.AssignmentScanCollection AssignmentScanCollection
        {
            get { return this.m_AssignmentScanCollection; }
        }

        private void HistologySlideScanReceived(YellowstonePathology.Business.BarcodeScanning.Barcode barcode)
        {
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                new Action(
                    delegate ()
                    {

                        if (barcode.IsValidated == true)
                        {
                            this.ChangeListing(barcode.ID);
                        }
                        else
                        {
                            MessageBox.Show("The scanner did not read the barcode correctly.  Please try again.", "Invalid Scan", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        }
                    }));
        }

        public void ChangeListing(string slideOrderId)
        {
            if (this.m_AssignmentScanCollection.Exists(slideOrderId) == false)
            {
                YellowstonePathology.Business.Surgical.AssignmentScan assignmentScan = new Business.Surgical.AssignmentScan();
                assignmentScan.SlideOrderId = slideOrderId;
                this.m_AssignmentScanCollection.Add(assignmentScan);
            }
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonAssign_Click(object sender, RoutedEventArgs e)
        {
            foreach(YellowstonePathology.Business.Surgical.AssignmentScan assignmentScan in this.m_AssignmentScanCollection)
            {
                YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(assignmentScan.SlideOrderId);
                string masterAccessionNo = orderIdParser.MasterAccessionNo;
                YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(masterAccessionNo, this);
                YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder = accessionOrder.PanelSetOrderCollection.GetSurgical();
                assignmentScan.ReportNo = surgicalTestOrder.ReportNo;
                if(surgicalTestOrder.AssignedToId == 0)
                {
                    assignmentScan.AssignedTo = YellowstonePathology.Business.User.SystemIdentity.Instance.User.UserName;
                    surgicalTestOrder.AssignedToId = YellowstonePathology.Business.User.SystemIdentity.Instance.User.UserId;
                }
                else
                {
                    assignmentScan.AssignedTo = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetSystemUserById(surgicalTestOrder.AssignedToId).UserName;
                }
                YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(this);
            }
        }

        private void ButtonNewScan_Click(object sender, RoutedEventArgs e)
        {
            if (this.idx >= this.m_SlideOrderIds.Count) MessageBox.Show("end of list");
            else
            {
                YellowstonePathology.Business.BarcodeScanning.Barcode barcode = new Business.BarcodeScanning.Barcode();
                barcode.ID = this.m_SlideOrderIds[idx];
                barcode.IsValidated = true;
                this.HistologySlideScanReceived(barcode);
                this.idx++;
            }
        }

        private void AddTestIds()
        {
            this.idx = 0;
            this.m_SlideOrderIds = new List<string>();
            this.m_SlideOrderIds.Add("19-7917.1A1");
            this.m_SlideOrderIds.Add("19-412.1A1");
            this.m_SlideOrderIds.Add("19-412.2A1");
            this.m_SlideOrderIds.Add("19-412.3A1");
            this.m_SlideOrderIds.Add("19-412.4A1");
            this.m_SlideOrderIds.Add("19-412.5A1");
            this.m_SlideOrderIds.Add("19-412.6A1");
            this.m_SlideOrderIds.Add("19-412.7A1");
            this.m_SlideOrderIds.Add("19-412.8A1");
            this.m_SlideOrderIds.Add("19-412.9A1");
            this.m_SlideOrderIds.Add("19-4120.1A1");
            this.m_SlideOrderIds.Add("19-4121.1A1");
            this.m_SlideOrderIds.Add("19-4122.1A1");
            this.m_SlideOrderIds.Add("19-4122.1B1");
            this.m_SlideOrderIds.Add("19-4123.1A1");
            this.m_SlideOrderIds.Add("19-4124.1A1");
            this.m_SlideOrderIds.Add("19-4124.1B1");
            this.m_SlideOrderIds.Add("19-4125.1A1");
            this.m_SlideOrderIds.Add("19-4126.1A1");
            this.m_SlideOrderIds.Add("19-4127.1A1");
            this.m_SlideOrderIds.Add("19-4128.1A1");
            this.m_SlideOrderIds.Add("19-4128.1B1");
            this.m_SlideOrderIds.Add("19-4128.1C1");
            this.m_SlideOrderIds.Add("19-4128.1D1");
            this.m_SlideOrderIds.Add("19-4129.1A1");
            this.m_SlideOrderIds.Add("19-4129.1B1");
            this.m_SlideOrderIds.Add("19-4129.2A1");
            this.m_SlideOrderIds.Add("19-4129.2B1");
            this.m_SlideOrderIds.Add("19-4129.3A1");
        }
    }
}
