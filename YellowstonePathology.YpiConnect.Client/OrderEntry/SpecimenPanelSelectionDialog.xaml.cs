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
using System.Windows.Shapes;
using System.ComponentModel;

namespace YellowstonePathology.YpiConnect.Client.OrderEntry
{    
    public partial class SpecimenPanelSelectionDialog : Window, INotifyPropertyChanged
    {
        public delegate void PropertyChangedNotificationHandler(String info);
        public event PropertyChangedEventHandler PropertyChanged;

        YellowstonePathology.Domain.ClientOrder.Model.ClientOrder m_ClientOrder;
        YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity m_ApplicationIdentity;

        YellowstonePathology.YpiConnect.Contract.Order.SpecimenPanelCollection m_SpecimenPanelCollection;

        public SpecimenPanelSelectionDialog(YellowstonePathology.Domain.ClientOrder.Model.ClientOrder clientOrder)
        {
            this.m_SpecimenPanelCollection = new YpiConnect.Contract.Order.SpecimenPanelCollection();
            this.m_ClientOrder = clientOrder;
            this.m_ApplicationIdentity = YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance;

            InitializeComponent();

            this.DataContext = this;
        }

        public YellowstonePathology.YpiConnect.Contract.Order.SpecimenPanelCollection SpecimenPanelCollection
        {
            get { return this.m_SpecimenPanelCollection; }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void ButtonCreate_Click(object sender, RoutedEventArgs e)
        {            
            if (this.ListViewPanels.SelectedItems.Count != 0)
            {
                YellowstonePathology.YpiConnect.Contract.Order.SpecimenPanel specimenPanel = (YellowstonePathology.YpiConnect.Contract.Order.SpecimenPanel)this.ListViewPanels.SelectedItem;
                specimenPanel.CreatePanel(this.m_ClientOrder);
                SpecimenPanelAssociationDialog specimenPanelAssociationDialog = new SpecimenPanelAssociationDialog(this.m_ClientOrder);
				specimenPanelAssociationDialog.ShowDialog();
                this.Close();
            }
        }        
    }
}
