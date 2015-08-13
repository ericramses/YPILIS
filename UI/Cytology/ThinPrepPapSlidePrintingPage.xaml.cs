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

namespace YellowstonePathology.UI.Cytology
{
	/// <summary>
	/// Interaction logic for ThinPrepPapSlidePrintingPage.xaml
	/// </summary>
	public partial class ThinPrepPapSlidePrintingPage : UserControl, INotifyPropertyChanged, Business.Interface.IPersistPageChanges
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void NextEventHandler(object sender, EventArgs e);
		public event NextEventHandler Next;

        YellowstonePathology.Business.Specimen.Model.SpecimenOrder m_SpecimenOrder;
        YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;

		private string m_PageHeaderText;

		public ThinPrepPapSlidePrintingPage(YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder, YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.Business.Persistence.ObjectTracker objectTracker)
		{
            this.m_SpecimenOrder = specimenOrder;
            this.m_AccessionOrder = accessionOrder;
            this.m_ObjectTracker = objectTracker;
			this.m_PageHeaderText = "Slide Printing Page ";

			InitializeComponent();

			DataContext = this;
		}

		public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}

        public YellowstonePathology.Business.Specimen.Model.SpecimenOrder SpecimenOrder
        {
            get { return this.m_SpecimenOrder; }
        }

        public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
        }

		public bool OkToSaveOnNavigation(Type pageNavigatingTo)
		{
			return false;
		}

		public bool OkToSaveOnClose()
		{
			return false;
		}

		public void Save()
		{
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

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
			if (this.Next != null) this.Next(this, new EventArgs());
		}

        private void ButtonAddThinPrepSlide_Click(object sender, RoutedEventArgs e)
        {            
            YellowstonePathology.Business.Specimen.Model.ThinPrepSlide thinPrepSlide = new Business.Specimen.Model.ThinPrepSlide();
            if (this.m_SpecimenOrder.AliquotOrderCollection.Exists(thinPrepSlide) == false)
            {
                YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = this.m_SpecimenOrder.AliquotOrderCollection.AddAliquot(thinPrepSlide, this.m_SpecimenOrder, this.m_AccessionOrder.AccessionDate.Value);
                this.m_ObjectTracker.SubmitChanges(this.m_AccessionOrder);
                this.PrintThinPrepSlide(aliquotOrder);
            }
            else
            {
                MessageBox.Show("Cannot add another Thin Prep Slide as one already exists.");
            }            
        }

        private void PrintThinPrepSlide(YellowstonePathology.Business.Test.AliquotOrder aliquotOrder)
        {
            YellowstonePathology.Business.BarcodeScanning.BarcodeVersion2 barcode = new YellowstonePathology.Business.BarcodeScanning.BarcodeVersion2(Business.BarcodeScanning.BarcodePrefixEnum.PSLD, aliquotOrder.AliquotOrderId);
            YellowstonePathology.Business.BarcodeScanning.CytycBarcode cytycBarcode = YellowstonePathology.Business.BarcodeScanning.CytycBarcode.Parse(this.m_AccessionOrder.MasterAccessionNo);
            YellowstonePathology.Business.Label.Model.ThinPrepSlide thinPrepSlide = new Business.Label.Model.ThinPrepSlide(this.m_AccessionOrder.PFirstName, this.m_AccessionOrder.PLastName, barcode, cytycBarcode);
            YellowstonePathology.Business.Label.Model.ThinPrepSlidePrinter thinPrepSlidePrinter = new Business.Label.Model.ThinPrepSlidePrinter();
            thinPrepSlidePrinter.Queue.Enqueue(thinPrepSlide);
            thinPrepSlidePrinter.Print();
        }

        private void ButtonAddPantherTube_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Specimen.Model.PantherAliquot pantherAliquot = new Business.Specimen.Model.PantherAliquot();
            if (this.m_SpecimenOrder.AliquotOrderCollection.Exists(pantherAliquot) == false)
            {
                YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = this.m_SpecimenOrder.AliquotOrderCollection.AddAliquot(pantherAliquot, this.m_SpecimenOrder, this.m_AccessionOrder.AccessionDate.Value);
                this.m_ObjectTracker.SubmitChanges(this.m_AccessionOrder);
            }
            else
            {
                MessageBox.Show("Cannot add another Panther Aliquot as one already exists.");
            }
        }        
	}
}
