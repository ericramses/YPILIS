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
    /// Interaction logic for WebServiceAccountClientEditDialog.xaml
    /// </summary>
    public partial class WebServiceAccountClientEditDialog : Window
    {
        private List<YellowstonePathology.Business.WebService.WebServiceClientView> m_WebServiceClientViews;
        private YellowstonePathology.Business.WebService.WebServiceAccount  m_WebServiceAccount;
        private YellowstonePathology.Business.WebService.WebServiceAccountClient m_WebServiceAccountClient;

        public WebServiceAccountClientEditDialog(YellowstonePathology.Business.WebService.WebServiceAccount webServiceAccount,
            YellowstonePathology.Business.WebService.WebServiceAccountClient webServiceAccountClient)
        {
            this.m_WebServiceAccount = webServiceAccount;
            this.m_WebServiceAccountClient = webServiceAccountClient;
            if (this.m_WebServiceAccountClient == null) this.m_WebServiceAccountClient = new Business.WebService.WebServiceAccountClient();
            this.m_WebServiceClientViews = YellowstonePathology.Business.Gateway.WebServiceGateway.GetWebServiceClientViews();

            InitializeComponent();

            this.DataContext = this;
            Loaded += WebServiceAccountClientEditDialog_Loaded;
        }

        private void WebServiceAccountClientEditDialog_Loaded(object sender, RoutedEventArgs e)
        {
            if(this.m_WebServiceAccountClient.WebServiceAccountClientId == 0)
            {
                this.ListViewWebServiceClientViews.SelectedIndex = -1;
            }
            else
            {
                foreach(YellowstonePathology.Business.WebService.WebServiceClientView webServiceClientView in this.m_WebServiceClientViews)
                {
                    if(webServiceClientView.ClientId == this.m_WebServiceAccountClient.ClientId)
                    {
                        this.ListViewWebServiceClientViews.SelectedItem = webServiceClientView;
                    }
                }
            }
        }

        public List<YellowstonePathology.Business.WebService.WebServiceClientView> WebServiceClientViews
        {
            get { return this.m_WebServiceClientViews; }
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.WebService.WebServiceClientView webServiceClientView = (YellowstonePathology.Business.WebService.WebServiceClientView)this.ListViewWebServiceClientViews.SelectedItem;
            if (webServiceClientView != null)
            {
                this.m_WebServiceAccountClient.ClientId = webServiceClientView.ClientId;
                if (this.m_WebServiceAccountClient.WebServiceAccountClientId == 0)
                {
                    int id = YellowstonePathology.Business.Gateway.WebServiceGateway.GetNextWebServiceAccountClientId();
                    this.m_WebServiceAccountClient.WebServiceAccountClientId = id;
                    this.m_WebServiceAccountClient.WebServiceAccountId = this.m_WebServiceAccount.WebServiceAccountId;
                    this.m_WebServiceAccount.WebServiceAccountClientCollection.Add(this.m_WebServiceAccountClient);
                }

                Close();
            }
            else
            {
                MessageBox.Show("A client must be selected.");
            }
        }
    }
}
