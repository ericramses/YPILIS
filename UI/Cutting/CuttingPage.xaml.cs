﻿using System;
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
	public partial class CuttingPage : UserControl, INotifyPropertyChanged 
	{            
		public event PropertyChangedEventHandler PropertyChanged;

        public delegate void FinishedEventHandler(object sender, YellowstonePathology.UI.CustomEventArgs.MasterAccessionNoReturnEventArgs eventArgs);
        public event FinishedEventHandler Finished;

        public delegate void ShowTestOrderSelectionPageEventHandler(object sender, YellowstonePathology.UI.CustomEventArgs.AliquotOrderReturnEventArgs eventArgs);
        public event ShowTestOrderSelectionPageEventHandler ShowTestOrderSelectionPage;        

        public delegate void PageTimedOutEventHandler(object sender, EventArgs eventArgs);
        public event PageTimedOutEventHandler PageTimedOut;

        private System.Windows.Threading.DispatcherTimer m_ListBoxSlidesMouseDownTimer;        

        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.Specimen.Model.SpecimenOrder m_SpecimenOrder;
        private YellowstonePathology.Business.Test.AliquotOrder m_AliquotOrder;
        private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;
        private YellowstonePathology.Business.Test.PanelOrder m_PanelOrder;
        private YellowstonePathology.Business.Test.Model.TestOrder m_TestOrder;        

		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		private YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort m_BarcodeScanPort;

        private System.Windows.Threading.DispatcherTimer m_PageTimeoutTimer;
        private YellowstonePathology.UI.Navigation.PageNavigator m_PageNavigator;
        private YellowstonePathology.Business.Label.Model.HistologySlidePaperLabelPrinter m_HistologySlidePaperLabelPrinter;

        public CuttingPage(YellowstonePathology.Business.Test.AliquotOrder aliquotOrder,
            YellowstonePathology.Business.Test.Model.TestOrder testOrder,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.Business.Label.Model.HistologySlidePaperLabelPrinter histologySlidePaperLabelPrinter,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator)
        {
            this.m_AliquotOrder = aliquotOrder;
            this.m_AccessionOrder = accessionOrder;
            this.m_TestOrder = testOrder;            
            this.m_PageNavigator = pageNavigator;
            this.m_HistologySlidePaperLabelPrinter = histologySlidePaperLabelPrinter;
            
            this.m_SpecimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderByAliquotOrderId(this.m_AliquotOrder.AliquotOrderId);
            this.m_PanelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrderByTestOrderId(this.m_TestOrder.TestOrderId);
            this.m_PanelOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelOrderByTestOrderId(this.m_TestOrder.TestOrderId);

            this.m_SystemIdentity = Business.User.SystemIdentity.Instance;

            this.m_ListBoxSlidesMouseDownTimer = new System.Windows.Threading.DispatcherTimer();
            this.m_ListBoxSlidesMouseDownTimer.Interval = new TimeSpan(0, 0, 0, 0, 750);
            this.m_ListBoxSlidesMouseDownTimer.Tick += new EventHandler(ListBoxSlidesMouseDownTimer_Tick);

			this.m_BarcodeScanPort = YellowstonePathology.Business.BarcodeScanning.BarcodeScanPort.Instance;            
            
			InitializeComponent();
			DataContext = this;

            this.m_PageTimeoutTimer = new System.Windows.Threading.DispatcherTimer();
            this.m_PageTimeoutTimer.Interval = TimeSpan.FromMinutes(15);
            this.m_PageTimeoutTimer.Tick += new EventHandler(PageTimeoutTimer_Tick);
            this.m_PageTimeoutTimer.Start();

            this.Loaded += new RoutedEventHandler(CuttingPage_Loaded);
            this.Unloaded += new RoutedEventHandler(CuttingPage_Unloaded);            
		}

        private void CuttingPage_Loaded(object sender, RoutedEventArgs e)
        {
			this.m_BarcodeScanPort.HistologySlideScanReceived += new Business.BarcodeScanning.BarcodeScanPort.HistologySlideScanReceivedHandler(BarcodeScanPort_HistologySlideScanReceived);
        }

        private void CuttingPage_Unloaded(object sender, RoutedEventArgs e)
        {
            this.m_BarcodeScanPort.HistologySlideScanReceived -= BarcodeScanPort_HistologySlideScanReceived;
            this.m_PageTimeoutTimer.Stop();            
        }

        private void PageTimeoutTimer_Tick(object sender, EventArgs e)
        {            
            this.PageTimedOut(this, new EventArgs());
        }

		public YellowstonePathology.Business.User.SystemIdentity SystemIdentity
        {
            get { return this.m_SystemIdentity; }
        }

        public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
        }

		public YellowstonePathology.Business.Specimen.Model.SpecimenOrder SpecimenOrder
        {
            get { return this.m_SpecimenOrder; }
        }

        public YellowstonePathology.Business.Test.AliquotOrder AliquotOrder
        {
            get { return this.m_AliquotOrder; }
        }

        public YellowstonePathology.Business.Test.PanelSetOrder PanelSetOrder
        {
            get { return this.m_PanelSetOrder; }
        }

        public YellowstonePathology.Business.Test.PanelOrder PanelOrder
        {
            get { return this.m_PanelOrder; }
        }

        public YellowstonePathology.Business.Test.Model.TestOrder TestOrder
        {
            get { return this.m_TestOrder; }
        }

        private void ListBoxSlidesMouseDownTimer_Tick(object sender, EventArgs e)
        {
            this.m_ListBoxSlidesMouseDownTimer.Stop();
            YellowstonePathology.Business.Slide.Model.SlideOrder slideOrder = (YellowstonePathology.Business.Slide.Model.SlideOrder)this.ListBoxSlideOrderCollection.SelectedItem;

            SlideOptionsPage slideOptionsPage = new SlideOptionsPage(slideOrder);
            slideOptionsPage.DeleteSlideOrder += new SlideOptionsPage.DeleteSlideOrderEventHandler(SlideOptionsPage_DeleteSlideOrder);
            slideOptionsPage.PrintSlide += new SlideOptionsPage.PrintSlideEventHandler(SlideOptionsPage_PrintSlide);
            slideOptionsPage.PrintPaperLabel += new SlideOptionsPage.PrintPaperLabelEventHandler(SlideOptionsPage_PrintPaperLabel);
            slideOptionsPage.Close += new SlideOptionsPage.CloseEventHandler(SlideOptionsPage_Close);
            this.m_PageNavigator.Navigate(slideOptionsPage);            
        }

        private void SlideOptionsPage_PrintPaperLabel(object sender, CustomEventArgs.SlideOrderReturnEventArgs eventArgs)
        {
            this.PrintPaperLabel(eventArgs.SlideOrder);
            this.m_PageNavigator.Navigate(this);
        }

        private void SlideOptionsPage_Close(object sender, EventArgs eventArgs)
        {
            this.m_PageNavigator.Navigate(this);
        }

        private void SlideOptionsPage_PrintSlide(object sender, CustomEventArgs.SlideOrderReturnEventArgs eventArgs)
        {            
            this.PrintSlide(eventArgs.SlideOrder);                                       
            this.m_PageNavigator.Navigate(this);
        }

        private void SlideOptionsPage_DeleteSlideOrder(object sender, CustomEventArgs.SlideOrderReturnEventArgs eventArgs)
        {
            YellowstonePathology.Business.Visitor.RemoveSlideOrderVisitor removeSlideOrderVisitor = new Business.Visitor.RemoveSlideOrderVisitor(eventArgs.SlideOrder);
            this.m_AccessionOrder.TakeATrip(removeSlideOrderVisitor);            
            this.m_PageNavigator.Navigate(this);
        }

		private void BarcodeScanPort_HistologySlideScanReceived(Business.BarcodeScanning.Barcode barcode)
        {
            this.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                new Action(
                    delegate()
                    {                                                
                        if (this.m_AliquotOrder.SlideOrderCollection.Exists(barcode.ID) == true)
                        {                                
                            YellowstonePathology.Business.Slide.Model.SlideOrder slideOrder = this.m_AliquotOrder.SlideOrderCollection.Get(barcode.ID);
                            this.AddMaterialTrackingLog(slideOrder);

                            if (slideOrder.Validated == false)
                            {
                                slideOrder.Validate(this.m_SystemIdentity);
                                
                                if (this.m_AliquotOrder.SlideOrderCollection.IsLastSlide(barcode.ID) == true)
                                {
                                    this.Finished(this, new YellowstonePathology.UI.CustomEventArgs.MasterAccessionNoReturnEventArgs(this.m_AccessionOrder.MasterAccessionNo));
                                }                                
                            }                                
                        }                                                    
                        else
                        {
                            MessageBox.Show("This slide does not belong to the current block");
                        }
                    }));
        }

        private void AddMaterialTrackingLog(YellowstonePathology.Business.Slide.Model.SlideOrder slideOrder)
        {
            YellowstonePathology.Business.Facility.Model.FacilityCollection facilityCollection = Business.Facility.Model.FacilityCollection.GetAllFacilities();
            YellowstonePathology.Business.Facility.Model.LocationCollection locationCollection = YellowstonePathology.Business.Facility.Model.LocationCollection.GetAllLocations();
			YellowstonePathology.Business.Facility.Model.Facility thisFacility = facilityCollection.GetByFacilityId(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.FacilityId);
			YellowstonePathology.Business.Facility.Model.Location thisLocation = locationCollection.GetLocation(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.LocationId);

            string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
			YellowstonePathology.Business.MaterialTracking.Model.MaterialTrackingLog materialTrackingLog = new Business.MaterialTracking.Model.MaterialTrackingLog(objectId, slideOrder.SlideOrderId, null, thisFacility.FacilityId, thisFacility.FacilityName,
                thisLocation.LocationId, thisLocation.Description, "Slide Scanned", "Slide Scanned At Cutting", "SlideOrder", this.m_AccessionOrder.MasterAccessionNo, slideOrder.Label, slideOrder.ClientAccessioned);
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(materialTrackingLog, Window.GetWindow(this));            
        }		         

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void ListBoxSlides_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.m_ListBoxSlidesMouseDownTimer.Start();
        }

        private void ListBoxSlides_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.m_ListBoxSlidesMouseDownTimer.Stop();
            if (this.ListBoxSlideOrderCollection.SelectedItems.Count != 0)
            {
                YellowstonePathology.Business.Slide.Model.SlideOrder slideOrder = (YellowstonePathology.Business.Slide.Model.SlideOrder)this.ListBoxSlideOrderCollection.SelectedItem;
                if (slideOrder.Status == YellowstonePathology.Business.Slide.Model.SlideStatusEnum.Created.ToString())
                {
                    if (slideOrder.LabelType == YellowstonePathology.Business.Slide.Model.SlideLabelTypeEnum.DirectPrint.ToString())
                    {
                        this.PrintSlide(slideOrder);
                        slideOrder.Status = YellowstonePathology.Business.Slide.Model.SlideStatusEnum.Printed.ToString();
                        
                    }
                    else if (slideOrder.LabelType == YellowstonePathology.Business.Slide.Model.SlideLabelTypeEnum.PaperLabel.ToString())
                    {
                        slideOrder.Status = YellowstonePathology.Business.Slide.Model.SlideStatusEnum.Printed.ToString();
                        YellowstonePathology.Business.Label.Model.HistologySlidePaperLabel histologySlidePaperLabel = new Business.Label.Model.HistologySlidePaperLabel(slideOrder.SlideOrderId, slideOrder.ReportNo, slideOrder.Label, slideOrder.PatientLastName, slideOrder.TestAbbreviation, slideOrder.Location);
                        this.m_HistologySlidePaperLabelPrinter.Queue.Enqueue(histologySlidePaperLabel);
                        this.ShowTestOrderSelectionPage(this, new CustomEventArgs.AliquotOrderReturnEventArgs(this.m_AliquotOrder));
                    }                    
                }
                else if (slideOrder.Status == YellowstonePathology.Business.Slide.Model.SlideStatusEnum.ClientAccessioned.ToString())
                {
                    MessageBox.Show("This is a client accessioned slide and cannot be printed.");
                }

                this.NotifyPropertyChanged(string.Empty);
            }
        }

        private void PrintSlide(YellowstonePathology.Business.Slide.Model.SlideOrder slideOrder)
        {			
            YellowstonePathology.Business.Label.Model.HistologySlideLabel histologySlideLabel = new Business.Label.Model.HistologySlideLabel(slideOrder.SlideOrderId, slideOrder.ReportNo, slideOrder.Label, slideOrder.PatientLastName, slideOrder.TestAbbreviation, slideOrder.Location, this.m_AccessionOrder);
            YellowstonePathology.Business.Label.Model.ThermoFisherHistologySlidePrinter thermoFisherSlidePrinter = new Business.Label.Model.ThermoFisherHistologySlidePrinter();
            thermoFisherSlidePrinter.Queue.Enqueue(histologySlideLabel);
            thermoFisherSlidePrinter.Print();

            slideOrder.SetAsPrinted(this.m_SystemIdentity);
        }

        private void ButtonAddSlide_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Visitor.AddSlideOrderVisitor addSlideOrderVisitor = new Business.Visitor.AddSlideOrderVisitor(this.m_AliquotOrder, this.m_TestOrder);
            this.m_AccessionOrder.TakeATrip(addSlideOrderVisitor);            
        }

        private void ButtonFinished_Click(object sender, RoutedEventArgs e)
        {
            if (this.Finished != null) this.Finished(this, new YellowstonePathology.UI.CustomEventArgs.MasterAccessionNoReturnEventArgs(this.m_AccessionOrder.MasterAccessionNo));
        }
       
        private void PrintPaperLabel(YellowstonePathology.Business.Slide.Model.SlideOrder slideOrder)
        {
            System.Windows.Controls.PrintDialog printDialog = new System.Windows.Controls.PrintDialog();
            System.Printing.PrintServer printServer = new System.Printing.LocalPrintServer();

			System.Printing.PrintQueue printQueue = printServer.GetPrintQueue(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.HistologySlideLabelPrinter);
            printDialog.PrintQueue = printQueue;

            YellowstonePathology.Business.Label.Model.HistologySlidePaperLabel histologySlidePaperLabel = new Business.Label.Model.HistologySlidePaperLabel(slideOrder.SlideOrderId, 
                slideOrder.ReportNo, slideOrder.Label, slideOrder.PatientLastName, slideOrder.TestAbbreviation, slideOrder.Location);
            YellowstonePathology.Business.Label.Model.HistologySlidePaperLabelPrinter histologySlidePaperLabelPrinter = new Business.Label.Model.HistologySlidePaperLabelPrinter();
            histologySlidePaperLabelPrinter.Queue.Enqueue(histologySlidePaperLabel);
            histologySlidePaperLabelPrinter.Print();
        }
	}
}
