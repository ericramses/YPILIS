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

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for PantherOrdersDialog.xaml
    /// </summary>
    public partial class PantherOrdersDialog : Window
    {
        private YellowstonePathology.Business.Test.PanelSetOrderCollection m_PantherOrderCollection;
        public PantherOrdersDialog()
        {
            this.m_PantherOrderCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetPantherOrdersNotAliquoted();
            InitializeComponent();
            this.DataContext = this;
        }

        public YellowstonePathology.Business.Test.PanelSetOrderCollection PantherOrderCollection
        {
            get { return this.m_PantherOrderCollection; }
        }
        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonPrint_Click(object sender, RoutedEventArgs e)
        {
            PantherOrdersReport pantherOrdersReport = new PantherOrdersReport(this.m_PantherOrderCollection);
            pantherOrdersReport.Print();
        }
    }
}
