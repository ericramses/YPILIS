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

        public WebServiceAccountEditDialog(int webServiceAccountId)
        {
            this.m_WebServiceAccount = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullWebServiceAccount(webServiceAccountId, this);
            DataContext = this;
            InitializeComponent();
        }

        public WebServiceAccountEditDialog()
        {
            this.m_WebServiceAccount = new Business.WebService.WebServiceAccount();
            DataContext = this;
            InitializeComponent();
        }

        public YellowstonePathology.Business.WebService.WebServiceAccount WebServiceAccount
        {
            get { return this.m_WebServiceAccount; }
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = this.CanSave();
            if (methodResult.Success == true)
            {
                if (this.m_WebServiceAccount.WebServiceAccountId == 0)
                {
                    int id = YellowstonePathology.Business.Gateway.WebServiceGateway.GetNextWebServiceAccountId();
                    this.m_WebServiceAccount.WebServiceAccountId = id;
                    YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(this.m_WebServiceAccount, this);
                }

                YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(this);
                Close();
            }
            else
            {
                MessageBox.Show(methodResult.Message);
            }
        }

        private YellowstonePathology.Business.Rules.MethodResult CanSave()
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = new Business.Rules.MethodResult();
            if(string.IsNullOrEmpty(this.m_WebServiceAccount.UserName) == true)
            {
                methodResult.Success = false;
                methodResult.Message = "A UserName is required" + Environment.NewLine;
            }
            if(string.IsNullOrEmpty(this.m_WebServiceAccount.Password) == true)
            {
                methodResult.Success = false;
                methodResult.Message = "A Password is required";
            }
            return methodResult;
        }
    }
}
