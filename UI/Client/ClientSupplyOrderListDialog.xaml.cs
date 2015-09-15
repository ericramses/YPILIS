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

namespace YellowstonePathology.UI.Client
{
    /// <summary>
    /// Interaction logic for ClientSupplyOrderListDialog.xaml
    /// </summary>
    public partial class ClientSupplyOrderListDialog : Window
    {
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
            //YellowstonePathology.Business.Client.
            //YellowstonePathology.UI.Client.ClientSupplyOrderDialog clientSupplyOrderDialog = new ClientSupplyOrderDialog()
        }
    }
}
