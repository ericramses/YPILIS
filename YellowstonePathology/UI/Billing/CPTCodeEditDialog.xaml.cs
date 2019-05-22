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
        private YellowstonePathology.Business.Billing.Model.CptCode m_CptCode;

        public CPTCodeEditDialog(YellowstonePathology.Business.Billing.Model.CptCode cptCode)
        {
            this.m_CptCode = cptCode;

            this.m_CptCodeString = this.m_CptCode.ToJSON();
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
            if(this.CanSave() == true) this.Close();
        }
        private bool CanSave()
        {
            this.m_CptCodeString = this.m_CptCodeString.Trim();
            if ((this.m_CptCodeString.StartsWith("{") && this.m_CptCodeString.EndsWith("}")) || //For object
                (this.m_CptCodeString.StartsWith("[") && this.m_CptCodeString.EndsWith("]"))) //For array
            {
                try
                {
                    var obj = JToken.Parse(this.m_CptCodeString);
                    return true;
                }
                catch (JsonReaderException jex)
                {
                    MessageBox.Show(jex.Message);
                    return false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return false;
                }
            }
            else
            {
                MessageBox.Show("The JSON string does not start and end wiith '[' and']' or '{' and'}'.");
                return false;
            }
        }
    }
}
