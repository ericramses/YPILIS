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

namespace YellowstonePathology.UI.Test
{	
	public partial class SlideTrackingResultPage : ResultControl, INotifyPropertyChanged 
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void NextEventHandler(object sender, EventArgs e);
		public event NextEventHandler Next;

		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private string m_PageHeaderText;

		private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;
        private Business.Slide.Model.VantageSlideCollection m_VantageSlideCollection;
        private YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort m_BarcodeScanPort;
        private string m_ScanIntent;

        public SlideTrackingResultPage(YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder,
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity) : base(panelSetOrder, accessionOrder)
		{
			this.m_PanelSetOrder = panelSetOrder;
			this.m_AccessionOrder = accessionOrder;
			this.m_SystemIdentity = systemIdentity;
            this.m_ScanIntent = "Receive";

            this.m_VantageSlideCollection = new Business.Slide.Model.VantageSlideCollection(this.m_AccessionOrder.MasterAccessionNo);
            this.m_BarcodeScanPort = YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort.Instance;

            this.m_PageHeaderText = "Slide Tracking Result For: " + this.m_AccessionOrder.PatientDisplayName;			

			InitializeComponent();

			DataContext = this;

            this.Loaded += SlideTrackingResultPage_Loaded;

            this.m_ControlsNotDisabledOnFinal.Add(this.ButtonNext);            
            this.m_ControlsNotDisabledOnFinal.Add(this.TextBlockUnfinalResults);
        }

        private void SlideTrackingResultPage_Loaded(object sender, RoutedEventArgs e)
        {
            this.m_BarcodeScanPort.VantageSlideScanReceived += BarcodeScanPort_VantageSlideScanReceived;
        }

        private void BarcodeScanPort_VantageSlideScanReceived(string scanData)
        {
            this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate ()
            {
                this.m_VantageSlideCollection.HandleSlideScan(scanData, this.m_ScanIntent, "YPIBLGS");
            }
            ));
        }

        public string ScanIntent
        {
            get { return this.m_ScanIntent; }
            set { this.m_ScanIntent = value; }
        }

        public Business.Slide.Model.VantageSlideCollection VantageSlideCollection
        {
            get { return this.m_VantageSlideCollection; }
        }

        public YellowstonePathology.Business.Test.PanelSetOrder PanelSetOrder
		{
			get { return this.m_PanelSetOrder; }
		}

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}		

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
			if (this.Next != null) this.Next(this, new EventArgs());
		}

        private void HyperLinkFinalizeResults_Click(object sender, RoutedEventArgs e)
        {

        }

        private void HyperLinkUnfinalResults_Click(object sender, RoutedEventArgs e)
        {

        }

        private void HyperLinkSimulateScan_Click(object sender, RoutedEventArgs e)
        {
            string data = YellowstonePathology.Business.BarcodeScanning.VantageBarcode.SimulateScan();
            this.BarcodeScanPort_VantageSlideScanReceived(data);
        }
    }
}
