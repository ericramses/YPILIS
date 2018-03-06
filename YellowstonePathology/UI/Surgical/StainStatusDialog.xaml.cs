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

namespace YellowstonePathology.UI.Surgical
{
    /// <summary>
    /// Interaction logic for StainStatusDialog.xaml
    /// </summary>
    public partial class StainStatusDialog : Window
    {
        private YellowstonePathology.Business.Test.Model.TestOrderStatusViewCollection m_TestOrderStatusViewCollection;
        public StainStatusDialog(int pathologistId)
        {
            this.FillTestOrderStatusViewCollection(pathologistId);
            InitializeComponent();
            DataContext = this;
        }

        public YellowstonePathology.Business.Test.Model.TestOrderStatusViewCollection TestOrderStatusViewCollection
        {
            get { return this.m_TestOrderStatusViewCollection; }
        }

        private void ButonOk_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void FillTestOrderStatusViewCollection(int pathologistId)
        {
            this.m_TestOrderStatusViewCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetTestOrderStatusViewCollection(pathologistId);
        }
    }
}
