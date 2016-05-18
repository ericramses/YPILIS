using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for EmbeddingDialog.xaml
    /// </summary>
    public partial class EmbeddingDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private DateTime m_WorkDate;
        private YellowstonePathology.Business.Test.AliquotOrderCollection m_AliquotOrderCollection;
        private YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort m_BarcodeScanPort;

        public EmbeddingDialog()
        {
            this.m_BarcodeScanPort = YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort.Instance;
            this.m_WorkDate = DateTime.Today;
            this.m_AliquotOrderCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetEmbeddingCollection(this.WorkDate);
            InitializeComponent();
            this.DataContext = this;
            this.Loaded += EmbeddingDialog_Loaded;
            this.Unloaded += EmbeddingDialog_Unloaded;
        }

        private void EmbeddingDialog_Unloaded(object sender, RoutedEventArgs e)
        {
            this.m_BarcodeScanPort.HistologyBlockScanReceived -= this.HistologyBlockScanReceived;
        }

        private void EmbeddingDialog_Loaded(object sender, RoutedEventArgs e)
        {
            this.m_BarcodeScanPort.HistologyBlockScanReceived += this.HistologyBlockScanReceived;
            this.ListViewAliquotOrders.SelectionChanged += ListViewAliquotOrders_SelectionChanged;
        }

        private void ListViewAliquotOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(this.ListViewAliquotOrders.SelectedItem != null)
            {
                YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = (YellowstonePathology.Business.Test.AliquotOrder)this.ListViewAliquotOrders.SelectedItem;
                this.HandleVerification(aliquotOrder.AliquotOrderId);
            }
        }

        private void HistologyBlockScanReceived(YellowstonePathology.Business.BarcodeScanning.Barcode barcode)
        {
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate ()
            {
                this.HandleVerification(barcode.ID);
            }
            ));
        }

        private void HandleVerification(string aliquotOrderId)
        {
            if (this.m_AliquotOrderCollection.Exists(aliquotOrderId) == true)
            {
                YellowstonePathology.Business.Test.AliquotOrder listAliquotOrder = this.m_AliquotOrderCollection.Get(aliquotOrderId);
                listAliquotOrder.EmbeddingVerify(YellowstonePathology.Business.User.SystemIdentity.Instance.User);

                YellowstonePathology.Business.Test.AliquotOrder dbAliquotOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAliquotOrder(aliquotOrderId, this);
                dbAliquotOrder.EmbeddingVerify(YellowstonePathology.Business.User.SystemIdentity.Instance.User);
                YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Save();
            }
            else
            {
                MessageBox.Show("Block not found in local collection.");
            }
        }

        public DateTime WorkDate
        {
            get { return this.m_WorkDate; }
            set
            {
                if(this.m_WorkDate != value)
                {
                    this.m_WorkDate = value;
                    this.NotifyPropertyChanged("WorkDate");
                }
            }
        }

        public YellowstonePathology.Business.Test.AliquotOrderCollection AliquotOrderCollection
        {
            get { return this.m_AliquotOrderCollection; }
        }

        private void ButtonAccessionOrderBack_Click(object sender, RoutedEventArgs e)
        {
            this.WorkDate = this.WorkDate.AddDays(-1);
            this.m_AliquotOrderCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetEmbeddingCollection(this.WorkDate);
            this.NotifyPropertyChanged("AliquotOrderCollection");
        }

        private void ButtonAccessionOrderForward_Click(object sender, RoutedEventArgs e)
        {
            this.WorkDate = this.WorkDate.AddDays(1);
            this.m_AliquotOrderCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetEmbeddingCollection(this.WorkDate);
            this.NotifyPropertyChanged("AliquotOrderCollection");
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
