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
    /// Interaction logic for CPTCodeEditDialog.xaml
    /// </summary>
    public partial class CPTCodeEditDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string m_CptCodeString;
        private string m_HoldToCompareString;
        private YellowstonePathology.Business.Billing.Model.CptCode m_CptCode;

        public CPTCodeEditDialog(YellowstonePathology.Business.Billing.Model.CptCode cptCode)
        {
            this.m_CptCode = cptCode;

            this.m_CptCodeString = this.m_CptCode.ToJSON();
            this.m_HoldToCompareString = this.m_CptCode.ToJSON();
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
            get { return this.m_CptCode.Code; }
        }

        public string CptCodeString
        {
            get { return this.m_CptCodeString; }
            set { this.m_CptCodeString = value; }
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            if (this.CanSave() == true)
            {
                if (this.m_HoldToCompareString != this.m_CptCodeString)
                {
                    MessageBoxResult result = MessageBox.Show("Do you  want to save the changes?", "Save Changes", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                    if (result == MessageBoxResult.Yes)
                    {
                        YellowstonePathology.Business.Billing.Model.CptCode codeToSave = YellowstonePathology.Business.Billing.Model.CptCodeFactory.FromJson(this.m_CptCodeString);
                        codeToSave.Save();
                    }
                }

                this.Close();
            }
        }

        private bool CanSave()
        {
            Business.Rules.MethodResult result = YellowstonePathology.Business.Helper.JSONHelper.IsValidJSONString(this.m_CptCodeString);

            if (result.Success == true)
            {
                JObject jObject = JsonConvert.DeserializeObject<JObject>(this.m_CptCodeString);
                if(jObject["code"] == null)
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
                    else if (this.m_CptCode.Code != codeToCompare)
                    {
                        result.Success = false;
                        result.Message = "The Codes must be the same.";
                    }
                }
            }

            if(result.Success == false)
            {
                MessageBox.Show(result.Message);
            }

            return result.Success;
        }
    }
}
