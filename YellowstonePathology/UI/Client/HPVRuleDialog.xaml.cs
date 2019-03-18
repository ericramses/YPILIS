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

namespace YellowstonePathology.UI.Client
{
    /// <summary>
    /// Interaction logic for HPVRuleDialog.xaml
    /// </summary>
    public partial class HPVRuleDialog : Window
    {
        private YellowstonePathology.Business.Client.Model.StandingOrderViewCollection m_StandingOrderViewCollection;

        public HPVRuleDialog(YellowstonePathology.Business.Client.Model.StandingOrderViewTypeEnum viewType)
        {
            this.m_StandingOrderViewCollection = new Business.Client.Model.StandingOrderViewCollection(viewType);
            InitializeComponent();
            DataContext = this;
        }

        public YellowstonePathology.Business.Client.Model.StandingOrderViewCollection StandingOrderViewCollection
        {
            get { return this.m_StandingOrderViewCollection; }
        }

        private void ListBoxStandingOrders_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(this.ListViewStandingOrders.SelectedItem != null)
            {
                YellowstonePathology.Business.Client.Model.StandingOrderView view = (Business.Client.Model.StandingOrderView)this.ListViewStandingOrders.SelectedItem;
                MessageBox.Show(view.ReflexDescription);
            }
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
