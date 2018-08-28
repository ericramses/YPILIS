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
        private List<YellowstonePathology.Business.WebService.WebServiceAccountView> m_LimitedWebServiceAccountViewList;
        public WebServiceAccountSelectionDialog()
        {
            this.m_WebServiceAccountViewList = YellowstonePathology.Business.Gateway.WebServiceGateway.GetWebServiceAccountViewList();
            this.m_LimitedWebServiceAccountViewList = this.m_WebServiceAccountViewList;
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

        public List<YellowstonePathology.Business.WebService.WebServiceAccountView> LimitedWebServiceAccountViewList
        {
            get { return this.m_LimitedWebServiceAccountViewList; }
        }

        private void ListViewWebServiceAccounts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(this.ListViewWebServiceAccounts.SelectedItem != null)
            {
                YellowstonePathology.Business.WebService.WebServiceAccountView webServiceAccountView = (YellowstonePathology.Business.WebService.WebServiceAccountView)this.ListViewWebServiceAccounts.SelectedItem;
                WebServiceAccountEditDialog dlg = new WebService.WebServiceAccountEditDialog(webServiceAccountView.WebServiceAccountId);
                dlg.ShowDialog();
                this.m_WebServiceAccountViewList = YellowstonePathology.Business.Gateway.WebServiceGateway.GetWebServiceAccountViewList();
                this.RefreshLimitedWebServiceAccountViewList();
            }
        }

        private void ButtonNew_Click(object sender, RoutedEventArgs e)
        {
            WebServiceAccountEditDialog dlg = new WebService.WebServiceAccountEditDialog(0);
            dlg.ShowDialog();
            this.m_WebServiceAccountViewList = YellowstonePathology.Business.Gateway.WebServiceGateway.GetWebServiceAccountViewList();
            this.RefreshLimitedWebServiceAccountViewList();
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonUpdate_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Gateway.WebServiceGateway.UpDateSqlServerFromMySQL();
            MessageBox.Show("MS Sql Server Updated from MySql tables WebServiceAccount and WebServiceAccountClient.");
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewWebServiceAccounts.SelectedItem != null)
            {
                YellowstonePathology.Business.WebService.WebServiceAccountView webServiceAccountView = (YellowstonePathology.Business.WebService.WebServiceAccountView)this.ListViewWebServiceAccounts.SelectedItem;
                YellowstonePathology.Business.WebService.WebServiceAccount webServiceAccount = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullWebServiceAccount(webServiceAccountView.WebServiceAccountId, this);
                YellowstonePathology.Business.Persistence.DocumentGateway.Instance.DeleteDocument(webServiceAccount, this);
                this.m_WebServiceAccountViewList = YellowstonePathology.Business.Gateway.WebServiceGateway.GetWebServiceAccountViewList();
                this.RefreshLimitedWebServiceAccountViewList();
            }
        }

        private void TextBoxSearchName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (this.TextBoxSearchName.Text.Length > 0)
                {
                    this.m_LimitedWebServiceAccountViewList = this.GetViewsWithMatchingText(this.TextBoxSearchName.Text);
                    this.NotifyPropertyChanged("LimitedWebServiceAccountViewList");
                }
            }
        }

        private void ButtonClearSearch_Click(object sender, RoutedEventArgs e)
        {
            this.TextBoxSearchName.Text = string.Empty;
            this.RefreshLimitedWebServiceAccountViewList();
        }

        private void RefreshLimitedWebServiceAccountViewList()
        {
            if (string.IsNullOrEmpty(this.TextBoxSearchName.Text) == true)
            {
                this.m_LimitedWebServiceAccountViewList = this.m_WebServiceAccountViewList;
            }
            else
            {
                this.m_LimitedWebServiceAccountViewList = this.GetViewsWithMatchingText(this.TextBoxSearchName.Text);
            }
            this.NotifyPropertyChanged("LimitedWebServiceAccountViewList");
        }

        public List<YellowstonePathology.Business.WebService.WebServiceAccountView> GetViewsWithMatchingText(string searchText)
        {
            List<YellowstonePathology.Business.WebService.WebServiceAccountView> result = new List<YellowstonePathology.Business.WebService.WebServiceAccountView>();

            string upper = searchText.ToUpper();
            foreach (YellowstonePathology.Business.WebService.WebServiceAccountView view in this.m_WebServiceAccountViewList)
            {
                string matchUpper = view.DisplayName.ToUpper();
                if (matchUpper.StartsWith(upper))
                {
                    result.Add(view);
                }
            }
            return result;
        }
    }
}
