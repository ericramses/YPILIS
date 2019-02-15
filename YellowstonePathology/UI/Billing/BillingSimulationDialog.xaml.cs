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

namespace YellowstonePathology.UI.Billing
{    
    public partial class BillingSimulationDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private AutomationList m_AutomationList;
        private DateTime m_PostDate;
        private Business.Billing.Model.InsuranceMapCollection m_InsuranceMapCollection;

        public BillingSimulationDialog()
        {
            this.m_InsuranceMapCollection = Business.Billing.Model.InsuranceMapCollection.GetCollection();
            this.m_PostDate = DateTime.Parse("2019-02-11");
            this.m_AutomationList = AutomationList.GetList(this.m_PostDate);
            this.HandlePrimaryInsuranceAuto();

            InitializeComponent();
            this.DataContext = this;
        }

        public DateTime PostDate
        {
            get { return this.m_PostDate; }
            set
            {
                if(this.m_PostDate != value)
                {
                    this.m_PostDate = value;
                    this.NotifyPropertyChanged("PostDate");
                }                
            }
        }

        public AutomationList AutomationList
        {
            get { return this.m_AutomationList; }
        }

        private void ButtonGetList_Click(object sender, RoutedEventArgs e)
        {
            this.m_AutomationList = AutomationList.GetList(this.m_PostDate);
            this.NotifyPropertyChanged("AutomationList");
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void ButtonRun_Click(object sender, RoutedEventArgs e)
        {
            this.HandlePrimaryInsuranceAuto();
        }

        private void HandlePrimaryInsuranceAuto()
        {
            foreach (AutomationListItem item in this.m_AutomationList)
            {
                Business.HL7View.ADTMessages adtMessages = new Business.HL7View.ADTMessages();
                adtMessages = Business.Gateway.AccessionOrderGateway.GetADTMessages(item.MedicalRecord);
                item.PrimaryInsuranceADT = adtMessages.GetPrimaryInsuranceV2();

                if(this.m_InsuranceMapCollection.Exists(item.PrimaryInsuranceADT) == false)
                {
                    item.PrimaryInsuranceMapped = "Not Mapped";
                }
                else
                {
                    Business.Billing.Model.InsuranceMap insuranceMap = this.m_InsuranceMapCollection.GetMap(item.PrimaryInsuranceADT);
                    item.PrimaryInsuranceMapped = insuranceMap.MapsTo;
                }
            }
        }

        private void UpdateMapping()
        {
            foreach (AutomationListItem item in this.m_AutomationList)
            {
                if (this.m_InsuranceMapCollection.Exists(item.PrimaryInsuranceADT) == false)
                {
                    item.PrimaryInsuranceMapped = "Not Mapped";
                }
                else
                {
                    Business.Billing.Model.InsuranceMap insuranceMap = this.m_InsuranceMapCollection.GetMap(item.PrimaryInsuranceADT);
                    item.PrimaryInsuranceMapped = insuranceMap.MapsTo;
                }
            }
        }

        private void MenuItemMapToMedicare_Click(object sender, RoutedEventArgs e)
        {
            AutomationListItem item = (AutomationListItem)this.ListViewAutomationList.SelectedItem;
            this.m_InsuranceMapCollection.TryMap(item.PrimaryInsuranceADT, "Medicare");
            this.UpdateMapping();           
        }

        private void MenuItemMapToMedicaid_Click(object sender, RoutedEventArgs e)
        {
            AutomationListItem item = (AutomationListItem)this.ListViewAutomationList.SelectedItem;
            this.m_InsuranceMapCollection.TryMap(item.PrimaryInsuranceADT, "Medicaid");
            this.UpdateMapping();
        }

        private void MenuItemMapToGovernmental_Click(object sender, RoutedEventArgs e)
        {
            AutomationListItem item = (AutomationListItem)this.ListViewAutomationList.SelectedItem;
            this.m_InsuranceMapCollection.TryMap(item.PrimaryInsuranceADT, "Governmental");
            this.UpdateMapping();
        }

        private void MenuItemMapToCommercial_Click(object sender, RoutedEventArgs e)
        {
            AutomationListItem item = (AutomationListItem)this.ListViewAutomationList.SelectedItem;
            this.m_InsuranceMapCollection.TryMap(item.PrimaryInsuranceADT, "Commercial");
            this.UpdateMapping();
        }

        private void MenuItemUnmap_Click(object sender, RoutedEventArgs e)
        {
            AutomationListItem item = (AutomationListItem)this.ListViewAutomationList.SelectedItem;
            Business.Billing.Model.InsuranceMap insuranceMap = this.m_InsuranceMapCollection.GetMap(item.PrimaryInsuranceADT);
            this.m_InsuranceMapCollection.UnMap(insuranceMap);
            this.UpdateMapping();
        }
    }
}
