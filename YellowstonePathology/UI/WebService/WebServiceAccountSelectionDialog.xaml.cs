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
using System.ComponentModel;

namespace YellowstonePathology.UI.WebService
{
    /// <summary>
    /// Interaction logic for WebServiceAccountSelectionDialog.xaml
    /// </summary>
    public partial class WebServiceAccountSelectionDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private YellowstonePathology.Business.WebService.WebServiceAccountCollection m_WebServiceAccountCollection;
        private List<YellowstonePathology.Business.WebService.WebServiceClientView> m_WebServiceClientViewList;
        public WebServiceAccountSelectionDialog()
        {
            this.m_WebServiceAccountCollection = new Business.WebService.WebServiceAccountCollection();
            this.m_WebServiceClientViewList = YellowstonePathology.Business.Gateway.WebServiceGateway.GetWebServiceClientViewList();
            DataContext = this;

            InitializeComponent();
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public YellowstonePathology.Business.WebService.WebServiceAccountCollection WebServiceAccountCollection
        {
            get { return this.m_WebServiceAccountCollection; }
        }

        public List<YellowstonePathology.Business.WebService.WebServiceClientView> WebServiceClientViewList
        {
            get { return this.m_WebServiceClientViewList; }
        }

        private void ComboBoxClientName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(this.ComboBoxClientName.SelectedItem != null)
            {
                YellowstonePathology.Business.WebService.WebServiceClientView webServiceClientView = (Business.WebService.WebServiceClientView)this.ComboBoxClientName.SelectedItem;
                this.FillByClientId(webServiceClientView.ClientId);
            }
        }

        private void TextBoxDisplayName_TextChanged(object sender, TextChangedEventArgs e)
        {
            string displayName = TextBoxDisplayName.Text;
            if(string.IsNullOrEmpty(displayName) == false)
            {
                this.FillByDisplayName(displayName);
            }
        }

        private void ListViewWebServiceAccounts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(this.ListViewWebServiceAccounts.SelectedItem != null)
            {
                YellowstonePathology.Business.WebService.WebServiceAccount webServiceAccount = (YellowstonePathology.Business.WebService.WebServiceAccount)this.ListViewWebServiceAccounts.SelectedItem;
                WebServiceAccountEditDialog dlg = new WebService.WebServiceAccountEditDialog(webServiceAccount.WebServiceAccountId);
                dlg.ShowDialog();
            }
        }

        private void ButtonNew_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Not implemented.");
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void FillByDisplayName(string displayName)
        {
            this.m_WebServiceAccountCollection = YellowstonePathology.Business.Gateway.WebServiceGateway.GetWebServiceAccountsByDisplayName(displayName);
            this.NotifyPropertyChanged("WebServiceAccountCollection");
            this.ListViewWebServiceAccounts.SelectedIndex = -1;
        }

        private void FillByClientId(int clientId)
        {
            this.m_WebServiceAccountCollection = YellowstonePathology.Business.Gateway.WebServiceGateway.GetWebServiceAccountsByClientId(clientId);
            this.NotifyPropertyChanged("WebServiceAccountCollection");
            this.ListViewWebServiceAccounts.SelectedIndex = -1;
        }
    }
}
