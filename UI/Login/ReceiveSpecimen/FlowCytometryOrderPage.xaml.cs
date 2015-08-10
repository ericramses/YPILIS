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
    /// <summary>
    /// 
    /// </summary>
    public partial class FlowCytometryOrderPage : UserControl, INotifyPropertyChanged, Shared.Interface.IPersistPageChanges
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public delegate void ReturnEventHandler(object sender, UI.Navigation.PageNavigationReturnEventArgs e);
        public event ReturnEventHandler Return;

        private YellowstonePathology.Business.PanelSet.Model.FlowCytometry.FlowCytometryPanelSetCollection m_FlowCytometryPanelSetCollection;

        public FlowCytometryOrderPage()
        {
            this.m_FlowCytometryPanelSetCollection = new Business.PanelSet.Model.FlowCytometry.FlowCytometryPanelSetCollection();
            InitializeComponent();
            this.DataContext = this;            
        }

        public YellowstonePathology.Business.PanelSet.Model.FlowCytometry.FlowCytometryPanelSetCollection FlowCytometryPanelSetCollection
        {
            get { return this.m_FlowCytometryPanelSetCollection; }
            set { this.m_FlowCytometryPanelSetCollection = value; }
        }

        public bool OkToSaveOnNavigation(Type pageNavigatingTo)
        {
            return true;
        }

        public bool OkToSaveOnClose()
        {
            return true;
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

        private void TileLLP_MouseUp(object sender, MouseButtonEventArgs e)
        {
            UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Finish, 20);
            this.Return(this, args);
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Back, null);
            this.Return(this, args);
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListBoxFlowCytometryPanelSets.SelectedItems.Count != 0)
            {
                YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = (YellowstonePathology.Business.PanelSet.Model.PanelSet)this.ListBoxFlowCytometryPanelSets.SelectedItem;
                UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Next, panelSet);
                this.Return(this, args);
            }
            else
            {
                MessageBox.Show("You must select a Flow Cytometry test type to continue.");
            }
        }                 
    }
}
