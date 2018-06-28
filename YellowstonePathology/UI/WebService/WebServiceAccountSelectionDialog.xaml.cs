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

        private List<YellowstonePathology.Business.WebService.WebServiceAccountView> m_WebServiceAccountViewList;
        public WebServiceAccountSelectionDialog()
        {
            this.m_WebServiceAccountViewList = YellowstonePathology.Business.Gateway.WebServiceGateway.GetWebServiceAccountViewList();
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

        public List<YellowstonePathology.Business.WebService.WebServiceAccountView> WebServiceAccountViewList
        {
            get { return this.m_WebServiceAccountViewList; }
        }

        private void ListViewWebServiceAccounts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(this.ListViewWebServiceAccounts.SelectedItem != null)
            {
                YellowstonePathology.Business.WebService.WebServiceAccountView webServiceAccountView = (YellowstonePathology.Business.WebService.WebServiceAccountView)this.ListViewWebServiceAccounts.SelectedItem;
                WebServiceAccountEditDialog dlg = new WebService.WebServiceAccountEditDialog(webServiceAccountView.WebServiceAccountId);
                dlg.ShowDialog();
            }
        }

        private void ButtonNew_Click(object sender, RoutedEventArgs e)
        {
            WebServiceAccountEditDialog dlg = new WebService.WebServiceAccountEditDialog();
            dlg.ShowDialog();
            this.m_WebServiceAccountViewList = YellowstonePathology.Business.Gateway.WebServiceGateway.GetWebServiceAccountViewList();
            NotifyPropertyChanged("WebServiceAccountViewList");
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            //YellowstonePathology.Business.Gateway.WebServiceGateway.UpDateSqlServerFromMySQL();
            MessageBox.Show("Not implemented.");
        }
    }
}
