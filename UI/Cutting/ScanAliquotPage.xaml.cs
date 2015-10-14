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

namespace YellowstonePathology.UI.Cutting
{    
	public partial class ScanAliquotPage : UserControl, YellowstonePathology.Business.Interface.IPersistPageChanges, INotifyPropertyChanged 
	{
		public event PropertyChangedEventHandler PropertyChanged;		

        public delegate void AliquotOrderSelectedEventHandler(object sender, YellowstonePathology.UI.CustomEventArgs.AliquotOrderAccessionOrderReturnEventArgs eventArgs);
        public event AliquotOrderSelectedEventHandler AliquotOrderSelected;          

        public delegate void SignOutEventHandler(object sender, EventArgs e);
        public event SignOutEventHandler SignOut;

        public delegate void  UseLastMasterAccessionNoEventHandler(object sender, YellowstonePathology.UI.CustomEventArgs.MasterAccessionNoReturnEventArgs eventArgs);
        public event UseLastMasterAccessionNoEventHandler UseLastMasterAccessionNo;

        public delegate void ShowMasterAccessionNoEntryPageEventHandler(object sender, EventArgs eventArgs);
        public event ShowMasterAccessionNoEntryPageEventHandler ShowMasterAccessionNoEntryPage;

        public delegate void PageTimedOutEventHandler(object sender, EventArgs eventArgs);
        public event PageTimedOutEventHandler PageTimedOut;

		private YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort m_BarcodeScanPort;
		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Test.AliquotOrder m_AliquotOrder;

        private string m_LastMasterAccessionNo;
        private System.Windows.Threading.DispatcherTimer m_PageTimeoutTimer;

		public ScanAliquotPage(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string lastMasterAccessionNo, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_LastMasterAccessionNo = lastMasterAccessionNo;
            this.m_SystemIdentity = systemIdentity;
			this.m_BarcodeScanPort = YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort.Instance;			

			InitializeComponent();

			DataContext = this;

            this.m_PageTimeoutTimer = new System.Windows.Threading.DispatcherTimer();
            this.m_PageTimeoutTimer.Interval = TimeSpan.FromMinutes(15);
            this.m_PageTimeoutTimer.Tick += new EventHandler(PageTimeoutTimer_Tick);
            this.m_PageTimeoutTimer.Start();

            this.Loaded += new RoutedEventHandler(ScanBlockPage_Loaded);
            this.Unloaded += new RoutedEventHandler(ScanBlockPage_Unloaded);
		}

        private void PageTimeoutTimer_Tick(object sender, EventArgs e)
        {
            this.PageTimedOut(this, new EventArgs());
        } 
        
        private void ScanBlockPage_Loaded(object sender, RoutedEventArgs e)
        {
			this.m_BarcodeScanPort.HistologyBlockScanReceived += new Business.BarcodeScanning.BarcodeScanPort.HistologyBlockScanReceivedHandler(BarcodeScanPort_HistologyBlockScanReceived);
        }

        private void ScanBlockPage_Unloaded(object sender, RoutedEventArgs e)
        {
            this.m_BarcodeScanPort.HistologyBlockScanReceived -= BarcodeScanPort_HistologyBlockScanReceived;
            this.m_PageTimeoutTimer.Stop();
        }

		private void BarcodeScanPort_HistologyBlockScanReceived(Business.BarcodeScanning.Barcode barcode)
        {            
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Input, new System.Threading.ThreadStart(delegate()
            {
                this.HandleBlockScanReceived(barcode.ID);
            }
            ));
        }

        public string LastMasterAccessionNo
        {
            get { return this.m_LastMasterAccessionNo; }
        }

        private void HandleBlockScanReceived(string aliquotOrderId)
        {
            if (this.m_AccessionOrder != null && this.m_AccessionOrder.SpecimenOrderCollection.AliquotOrderExists(aliquotOrderId) == false)
            {                
                this.m_AccessionOrder = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetAccessionOrderByAliquotOrderId(aliquotOrderId);             
            }
            else
            {
                this.m_AccessionOrder = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetAccessionOrderByAliquotOrderId(aliquotOrderId);
            }

            if (this.m_AccessionOrder != null)
            {
                this.m_AliquotOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetAliquotOrder(aliquotOrderId);
                this.AddMaterialTrackingLog(this.m_AliquotOrder);
                this.AliquotOrderSelected(this, new CustomEventArgs.AliquotOrderAccessionOrderReturnEventArgs(this.m_AliquotOrder, this.m_AccessionOrder));
            }
            else
            {
                MessageBox.Show("The block scanned could not be found.");
            }
        }

        private void AddMaterialTrackingLog(YellowstonePathology.Business.Test.AliquotOrder aliquotOrder)
        {
            YellowstonePathology.Business.Facility.Model.FacilityCollection facilityCollection = Business.Facility.Model.FacilityCollection.GetAllFacilities();
            YellowstonePathology.Business.Facility.Model.LocationCollection locationCollection = YellowstonePathology.Business.Facility.Model.LocationCollection.GetAllLocations();
			YellowstonePathology.Business.Facility.Model.Facility thisFacility = facilityCollection.GetByFacilityId(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.FacilityId);
			YellowstonePathology.Business.Facility.Model.Location thisLocation = locationCollection.GetLocation(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.LocationId);
            
            string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
			YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLog materialTrackingLog = new Business.MaterialTracking.Model.MaterialTrackingLog(objectId, aliquotOrder.AliquotOrderId, null, thisFacility.FacilityId, thisFacility.FacilityName,
                thisLocation.LocationId, thisLocation.Description, this.m_SystemIdentity.User.UserId, this.m_SystemIdentity.User.UserName, "Block Scanned", "Block Scanned At Cutting", "Aliquot", this.m_AccessionOrder.MasterAccessionNo, aliquotOrder.Label, aliquotOrder.ClientAccessioned);
			YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();
            objectTracker.RegisterRootInsert(materialTrackingLog);
            objectTracker.SubmitChanges(materialTrackingLog);	
        }

        private void ButtonShowMasterAccessionNoEntryPage_Click(object sender, RoutedEventArgs e)
        {
            this.ShowMasterAccessionNoEntryPage(this, new EventArgs());
        }  

        private void ButtonPrintImmunos_Click(object sender, RoutedEventArgs e)
        {
            this.PrintImmunoLabels();
        }

        private void PrintImmunoLabels()
        {
            YellowstonePathology.Business.Slide.Model.SlideOrderCollection_Base slideOrderCollection = YellowstonePathology.Business.Gateway.SlideAccessionGateway.GetSlideOrdersWithPrintRequest();
			YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();
            objectTracker.RegisterObject(slideOrderCollection);

            if (slideOrderCollection.Count != 0)
            {
                YellowstonePathology.Business.Label.Model.HistologySlidePaperLabelPrinter histologySlidePaperLabelPrinter = new Business.Label.Model.HistologySlidePaperLabelPrinter();
                foreach (YellowstonePathology.Business.Slide.Model.SlideOrder_Base slideOrder in slideOrderCollection)
                {
                    YellowstonePathology.Business.Label.Model.HistologySlidePaperLabel histologySlidePaperLabel = new Business.Label.Model.HistologySlidePaperLabel(slideOrder.SlideOrderId, slideOrder.ReportNo, slideOrder.Label, slideOrder.PatientLastName, slideOrder.TestAbbreviation, slideOrder.Location);
                    histologySlidePaperLabelPrinter.Queue.Enqueue(histologySlidePaperLabel);                    
                }

                histologySlidePaperLabelPrinter.Print();                

                MessageBoxResult result = MessageBox.Show("Did the slide labels print OK?", "Is it OK to clear print request?", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    slideOrderCollection.SetAsPrinted(this.m_SystemIdentity);
                }
            }
            else
            {
                MessageBox.Show("There are not any labels waiting to be printed.");
            }

            objectTracker.SubmitChanges(slideOrderCollection);
        }        

        private void ButtonLastMasterAccessionNo_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.m_LastMasterAccessionNo) == false)
            {
                this.UseLastMasterAccessionNo(this, new CustomEventArgs.MasterAccessionNoReturnEventArgs(this.m_LastMasterAccessionNo));
            }
        }

        private void ButtonSignOut_Click(object sender, RoutedEventArgs e)
        {
            this.SignOut(this, new EventArgs());
        }

		public void Save()
		{

		}

		public bool OkToSaveOnNavigation(Type pageNavigatingTo)
		{
			return false;
		}

		public bool OkToSaveOnClose()
		{
			return false;
		}

		public void UpdateBindingSources()
		{

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
