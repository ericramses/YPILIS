using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace YellowstonePathology.UI.Billing
{
    /// <summary>
    /// Interaction logic for ICDCodeEditDialog.xaml
    /// </summary>
    public partial class ICDCodeEditDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string m_ICDCodeString;
        private string m_HoldToCompareString;
        private YellowstonePathology.Business.Billing.Model.ICDCode m_ICDCode;

        public ICDCodeEditDialog(YellowstonePathology.Business.Billing.Model.ICDCode icdCode)
        {
            this.m_ICDCode = icdCode;

            this.m_ICDCodeString = this.m_ICDCode.ToJSON();
            this.m_HoldToCompareString = this.m_ICDCode.ToJSON();
            InitializeComponent();

            DataContext = this;
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public string Code
        {
            get { return this.m_ICDCode.Code; }
        }

        public string ICDCodeString
        {
            get { return this.m_ICDCodeString; }
            set { this.m_ICDCodeString = value; }
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            if (this.CanSave() == true)
            {
                if (this.m_HoldToCompareString != this.m_ICDCodeString)
                {
                    MessageBoxResult result = MessageBox.Show("Do you  want to save the changes?", "Save Changes", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                    if (result == MessageBoxResult.Yes)
                    {
                        YellowstonePathology.Business.Billing.Model.ICDCode codeToSave = YellowstonePathology.Business.Billing.Model.ICDCodeFactory.FromJson(this.m_ICDCodeString);
                        codeToSave.Save();
                    }
                }

                this.Close();
            }
        }

        private bool CanSave()
        {
            Business.Rules.MethodResult result = YellowstonePathology.Business.Helper.JSONHelper.IsValidJSONString(this.m_ICDCodeString);

            if (result.Success == true)
            {
                JObject jObject = JsonConvert.DeserializeObject<JObject>(this.m_ICDCodeString);
                if (jObject["code"] == null)
                {
                    result.Success = false;
                    result.Message = "The Code must be present.";
                }
                else
                {
                    string codeToCompare = jObject["code"].ToString();
                    if (string.IsNullOrEmpty(codeToCompare) == true)
                    {
                        result.Success = false;
                        result.Message = "The Code must be present.";
                    }
                    else if (this.m_ICDCode.Code != codeToCompare)
                    {
                        result.Success = false;
                        result.Message = "The Codes must be the same.";
                    }
                }
            }

            if (result.Success == false)
            {
                MessageBox.Show(result.Message);
            }

            return result.Success;
        }
    }
}
