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
    public partial class SimulationDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private SimulationList m_SimulationList;
        private DateTime m_PostDate;
        private Business.Billing.Model.InsuranceMapCollection m_InsuranceMapCollection;

        public SimulationDialog()
        {
            this.m_InsuranceMapCollection = Business.Billing.Model.InsuranceMapCollection.GetCollection();
            this.m_PostDate = DateTime.Today;
            this.GetList();

            InitializeComponent();
            this.DataContext = this;
        }

        public void GetList()
        {
            this.m_SimulationList = SimulationList.GetList(this.m_PostDate);
            this.HandlePrimaryInsuranceSimulation();
            this.m_SimulationList.HandlePatientTypeSimulation();
            this.NotifyPropertyChanged("SimulationList");
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

        public SimulationList SimulationList
        {
            get { return this.m_SimulationList; }
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
                
        private void HandlePrimaryInsuranceSimulation()
        {
            foreach (SimulationListItem item in this.m_SimulationList)
            {
                Business.HL7View.ADTMessages adtMessages = new Business.HL7View.ADTMessages();
                adtMessages = Business.Gateway.AccessionOrderGateway.GetADTMessages(item.MedicalRecord);
                item.PrimaryInsuranceADT = adtMessages.GetPrimaryInsuranceV2();

                if(this.m_InsuranceMapCollection.Exists(item.PrimaryInsuranceADT) == false)
                {
                    item.PrimaryInsuranceSim = "Not Mapped";
                }
                else
                {
                    Business.Billing.Model.InsuranceMap insuranceMap = this.m_InsuranceMapCollection.GetMap(item.PrimaryInsuranceADT);
                    item.PrimaryInsuranceSim = insuranceMap.MapsTo;
                }
            }
        }

        private void UpdateMapping()
        {
            foreach (SimulationListItem item in this.m_SimulationList)
            {
                if (this.m_InsuranceMapCollection.Exists(item.PrimaryInsuranceADT) == false)
                {
                    item.PrimaryInsuranceSim = "Not Mapped";
                }
                else
                {
                    Business.Billing.Model.InsuranceMap insuranceMap = this.m_InsuranceMapCollection.GetMap(item.PrimaryInsuranceADT);
                    item.PrimaryInsuranceSim = insuranceMap.MapsTo;
                }
            }
        }

        private void MenuItemMapToMedicare_Click(object sender, RoutedEventArgs e)
        {
            SimulationListItem item = (SimulationListItem)this.ListViewSimulationList.SelectedItem;
            this.m_InsuranceMapCollection.TryMap(item.PrimaryInsuranceADT, "Medicare");
            this.UpdateMapping();           
        }

        private void MenuItemMapToMedicaid_Click(object sender, RoutedEventArgs e)
        {
            SimulationListItem item = (SimulationListItem)this.ListViewSimulationList.SelectedItem;
            this.m_InsuranceMapCollection.TryMap(item.PrimaryInsuranceADT, "Medicaid");
            this.UpdateMapping();
        }

        private void MenuItemMapToGovernmental_Click(object sender, RoutedEventArgs e)
        {
            SimulationListItem item = (SimulationListItem)this.ListViewSimulationList.SelectedItem;
            this.m_InsuranceMapCollection.TryMap(item.PrimaryInsuranceADT, "Governmental");
            this.UpdateMapping();
        }

        private void MenuItemMapToCommercial_Click(object sender, RoutedEventArgs e)
        {
            SimulationListItem item = (SimulationListItem)this.ListViewSimulationList.SelectedItem;
            this.m_InsuranceMapCollection.TryMap(item.PrimaryInsuranceADT, "Commercial");
            this.UpdateMapping();
        }

        private void MenuItemUnmap_Click(object sender, RoutedEventArgs e)
        {
            SimulationListItem item = (SimulationListItem)this.ListViewSimulationList.SelectedItem;
            Business.Billing.Model.InsuranceMap insuranceMap = this.m_InsuranceMapCollection.GetMap(item.PrimaryInsuranceADT);
            this.m_InsuranceMapCollection.UnMap(insuranceMap);
            this.UpdateMapping();
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            this.m_PostDate = this.m_PostDate.AddDays(-1);
            this.GetList();
            this.NotifyPropertyChanged("PostDate");
        }

        private void ButtonForward_Click(object sender, RoutedEventArgs e)
        {
            this.m_PostDate = this.m_PostDate.AddDays(1);
            this.GetList();
            this.NotifyPropertyChanged("PostDate");
        }
    }
}
