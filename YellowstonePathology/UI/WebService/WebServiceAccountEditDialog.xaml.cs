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

namespace YellowstonePathology.UI.WebService
{
    /// <summary>
    /// Interaction logic for WebServiceAccountDialog.xaml
    /// </summary>
    public partial class WebServiceAccountEditDialog : Window
    {
        private YellowstonePathology.Business.WebService.WebServiceAccount m_WebServiceAccount;

        public WebServiceAccountEditDialog(YellowstonePathology.Business.WebService.WebServiceAccount webServiceAccount)
        {
            this.m_WebServiceAccount = webServiceAccount;
            DataContext = this;
            InitializeComponent();
        }

        public YellowstonePathology.Business.WebService.WebServiceAccount WebServiceAccount
        {
            get { return this.m_WebServiceAccount; }
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Gateway.WebServiceGateway.UpdateWebServiceAccount(this.m_WebServiceAccount);
            Close();
        }
    }
}
