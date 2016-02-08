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

namespace YellowstonePathology.UI.Login.ReceiveSpecimen
{   
    public partial class OrderSelectionPage : UserControl, INotifyPropertyChanged 
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public delegate void BackEventHandler(object sender, EventArgs e);
        public event BackEventHandler Back;

        public delegate void OrderTestEventHandler(object sender, YellowstonePathology.UI.CustomEventArgs.PanelSetReturnEventArgs e);
        public event OrderTestEventHandler OrderTest;
                
        private YellowstonePathology.Business.PanelSet.Model.PanelSetCollection m_PanelSetCollectionView;
        private YellowstonePathology.Business.Facility.Model.FacilityCollection m_FacilityCollection;

        public OrderSelectionPage(YellowstonePathology.Business.Facility.Model.Facility facility)
        {                        
            this.m_PanelSetCollectionView = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetByFacility(facility);
            this.m_FacilityCollection = Business.Facility.Model.FacilityCollection.GetAllFacilities();

            InitializeComponent();

            this.DataContext = this;            
        }

        public YellowstonePathology.Business.PanelSet.Model.PanelSetCollection PanelSetCollectionView
        {
            get { return this.m_PanelSetCollectionView; }
        }

        public YellowstonePathology.Business.Facility.Model.FacilityCollection FacilityCollection
        {
            get { return this.m_FacilityCollection; }
        }

        public bool OkToSaveOnNavigation(Type pageNavigatingTo)
        {
            return true;
        }

        public bool OkToSaveOnClose()
        {
            return true;
        }

        public void Save(bool releaseLock)
        {
            
        }

        public void UpdateBindingSources()
        {

        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            this.Back(this, new EventArgs());
        }              

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void ButtonOrderTest_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewPanelSets.SelectedItem != null)
            {
                YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = (YellowstonePathology.Business.PanelSet.Model.PanelSet)this.ListViewPanelSets.SelectedItem;
                this.OrderTest(this, new YellowstonePathology.UI.CustomEventArgs.PanelSetReturnEventArgs(panelSet));                
            }   
        }

        private void ListBoxFacilities_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ListBoxFacilities.SelectedItem != null)
            {
                YellowstonePathology.Business.Facility.Model.Facility facility = (YellowstonePathology.Business.Facility.Model.Facility)this.ListBoxFacilities.SelectedItem;
                this.m_PanelSetCollectionView = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetByFacility(facility);
                this.NotifyPropertyChanged("PanelSetCollectionView");
            }
        }        
    }
}
