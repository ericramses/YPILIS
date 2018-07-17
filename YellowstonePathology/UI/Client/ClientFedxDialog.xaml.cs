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

namespace YellowstonePathology.UI.Client
{
    /// <summary>
    /// Interaction logic for ClientFedxDialog.xaml
    /// </summary>
    public partial class ClientFedxDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Business.Task.Model.TaskOrder m_TaskOrder;
        private Business.Task.Model.TaskOrderDetailFedexShipment m_TaskOrderDetailFedexShipment;
        private YellowstonePathology.Business.Facility.Model.FacilityCollection m_FacilityCollection;
        private List<string> m_PaymentTypeList;

        public ClientFedxDialog()
        {
            this.m_TaskOrder = new Business.Task.Model.TaskOrder();
            this.m_TaskOrderDetailFedexShipment = new Business.Task.Model.TaskOrderDetailFedexShipment();
            this.m_TaskOrderDetailFedexShipment.TaskId = "FDXSHPMNT";
            this.m_TaskOrder.TaskOrderDetailCollection.Add(this.m_TaskOrderDetailFedexShipment);

            this.m_FacilityCollection = Business.Facility.Model.FacilityCollection.Instance;

            this.m_PaymentTypeList = new List<string>();
            this.m_PaymentTypeList.Add("SENDER");
            this.m_PaymentTypeList.Add("THIRD_PARTY");
            this.m_PaymentTypeList.Add("RECIPIENT");

            InitializeComponent();

            DataContext = this;
        }

        public Business.Task.Model.TaskOrder TaskOrder
        {
            get { return this.m_TaskOrder; }
        }

        public YellowstonePathology.Business.Facility.Model.FacilityCollection FacilityCollection
        {
            get { return this.m_FacilityCollection; }
        }

        public List<string> PaymentTypeList
        {
            get { return this.m_PaymentTypeList; }
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void ComboboxShipToFacility_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.IsLoaded == true)
            {
                ComboBox comboBox = (ComboBox)sender;
                if (comboBox.SelectionBoxItem != null)
                {
                    Business.Facility.Model.Facility facility = (Business.Facility.Model.Facility)comboBox.SelectedItem;
                    if (facility != null)
                    {
                        this.m_TaskOrderDetailFedexShipment.SetShipTo(facility);
                    }
                }
                this.NotifyPropertyChanged(string.Empty);
            }
        }

        private void HyperLinkGetTrackingNumber_Click(object sender, RoutedEventArgs e)
        {
            this.m_TaskOrderDetailFedexShipment.ValidateObject();
            if (this.m_TaskOrderDetailFedexShipment.ValidationErrors.Count == 0)
            {
                if (this.IsOKToGetTrackingNumber(this.m_TaskOrderDetailFedexShipment) == true)
                {
                    Business.Facility.Model.Facility facility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId(this.m_TaskOrderDetailFedexShipment.ShipToFacilityId);
                    Business.MaterialTracking.Model.FedexAccountProduction fedExAccount = new Business.MaterialTracking.Model.FedexAccountProduction();
                    Business.MaterialTracking.Model.FedexShipmentRequest shipmentRequest = new Business.MaterialTracking.Model.FedexShipmentRequest(facility, fedExAccount, this.m_TaskOrderDetailFedexShipment.PaymentType, this.m_TaskOrderDetailFedexShipment.ServiceType, this.m_TaskOrderDetailFedexShipment);
                    Business.MaterialTracking.Model.FedexProcessShipmentReply result = shipmentRequest.RequestShipment();

                    if (result.RequestWasSuccessful == true)
                    {
                        this.m_TaskOrderDetailFedexShipment.TrackingNumber = result.TrackingNumber;
                        this.m_TaskOrderDetailFedexShipment.ZPLII = Business.Label.Model.ZPLPrinterTCP.DecodeZPLFromBase64(result.ZPLII);
                    }
                    else
                    {
                        MessageBox.Show("There was a problem with this shipping request.");
                    }
                }
                else
                {
                    MessageBox.Show("We are unable to get the tracking number at this point because there are problems with the data.");
                }
            }
            else
            {
                string message = "We are unable to get the tracking number at this point because" + Environment.NewLine + this.m_TaskOrderDetailFedexShipment.Errors;
                MessageBox.Show(message);
            }
        }

        private bool IsOKToGetTrackingNumber(Business.Task.Model.TaskOrderDetailFedexShipment taskOrderDetail)
        {
            bool result = true;
            if (string.IsNullOrEmpty(taskOrderDetail.TrackingNumber) == false) result = false;
            if (string.IsNullOrEmpty(taskOrderDetail.ShipToAddress1) == true) result = false;
            if (string.IsNullOrEmpty(taskOrderDetail.ShipToCity) == true) result = false;
            if (string.IsNullOrEmpty(taskOrderDetail.ShipToState) == true) result = false;
            if (string.IsNullOrEmpty(taskOrderDetail.ShipToZip) == true) result = false;
            if (string.IsNullOrEmpty(taskOrderDetail.PaymentType) == true) result = false;
            return result;
        }

        private void HyperLinkPrintLabel_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.m_TaskOrderDetailFedexShipment.ZPLII) == false)
            {
                if (string.IsNullOrEmpty(Business.User.UserPreferenceInstance.Instance.UserPreference.FedExLabelPrinter) == false)
                {
                    Business.Label.Model.ZPLPrinterTCP zplPrinter = new Business.Label.Model.ZPLPrinterTCP(Business.User.UserPreferenceInstance.Instance.UserPreference.FedExLabelPrinter);
                    zplPrinter.Print(this.m_TaskOrderDetailFedexShipment.ZPLII);
                    this.m_TaskOrderDetailFedexShipment.LabelHasBeenPrinted = true;
                }
                else
                {
                    MessageBox.Show("You need to go into User Preferences and choose your FedEx label printer before your label can be printed.");
                }
            }
            else
            {
                MessageBox.Show("The label cannot be printed until a tracking number exists.");
            }
        }

        private void HyperLinkCancelShipment_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.m_TaskOrderDetailFedexShipment.TrackingNumber) == false)
            {
                Business.MaterialTracking.Model.FedexAccountProduction fedExAccount = new Business.MaterialTracking.Model.FedexAccountProduction();
                Business.MaterialTracking.Model.FedexDeleteShipmentRequest deleteShipmentRequest = new Business.MaterialTracking.Model.FedexDeleteShipmentRequest(fedExAccount, this.m_TaskOrderDetailFedexShipment.TrackingNumber);
                Business.MaterialTracking.Model.FedexDeleteShipmentReply result = deleteShipmentRequest.Post();

                if (result.RequestWasSuccessful == true)
                {
                    this.m_TaskOrderDetailFedexShipment.ZPLII = null;
                    this.m_TaskOrderDetailFedexShipment.TrackingNumber = null;
                    this.m_TaskOrderDetailFedexShipment.LabelHasBeenPrinted = false;
                }
                else
                {
                    MessageBox.Show("There was a problem with this Request.");
                }
            }
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
