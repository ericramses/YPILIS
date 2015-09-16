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

namespace YellowstonePathology.UI.Client
{
    /// <summary>
    /// Interaction logic for ClientSupplyOrderListDialog.xaml
    /// </summary>
    public partial class ClientSupplyOrderListDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private YellowstonePathology.Business.Client.Model.ClientSupplyOrderCollection m_ClientSupplyOrderCollection;

        public ClientSupplyOrderListDialog()
        {
            this.m_ClientSupplyOrderCollection = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientSupplyOrderCollection();
            InitializeComponent();
            this.DataContext = this;
        }

        public YellowstonePathology.Business.Client.Model.ClientSupplyOrderCollection ClientSupplyOrderCollection
        {
            get { return this.m_ClientSupplyOrderCollection; }
        }

        private void ListViewClientSupplyOrders_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            YellowstonePathology.Business.Client.Model.ClientSupplyOrder clientSupplyOrder = (YellowstonePathology.Business.Client.Model.ClientSupplyOrder)this.ListViewClientSupplyOrders.SelectedItem;
            YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new Business.Persistence.ObjectTracker();
            objectTracker.RegisterObject(clientSupplyOrder);
            YellowstonePathology.UI.Client.ClientSupplyOrderDialog clientSupplyOrderDialog = new ClientSupplyOrderDialog(clientSupplyOrder, objectTracker);
            clientSupplyOrderDialog.ShowDialog();
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewClientSupplyOrders.SelectedItem != null)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to delete this supply order?", "Delete?", MessageBoxButton.YesNo);
                if(messageBoxResult == MessageBoxResult.Yes)
                {
                    YellowstonePathology.Business.Client.Model.ClientSupplyOrder clientSupplyOrder = (YellowstonePathology.Business.Client.Model.ClientSupplyOrder)this.ListViewClientSupplyOrders.SelectedItem;
                    YellowstonePathology.Business.Persistence.ObjectTracker ot = new Business.Persistence.ObjectTracker();
                    ot.RegisterRootDelete(clientSupplyOrder);
                    ot.SubmitChanges(clientSupplyOrder);
                    this.m_ClientSupplyOrderCollection = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientSupplyOrderCollection();
                    this.NotifyPropertyChanged("ClientSupplyOrderCollection");
                }
            }
            else
            {
                MessageBox.Show("You must select an item before deleting.");
            }
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
