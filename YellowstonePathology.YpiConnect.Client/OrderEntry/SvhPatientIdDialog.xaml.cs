using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;

namespace YellowstonePathology.YpiConnect.Client.OrderEntry
{
    /// <summary>
    /// Interaction logic for SvhStartOrderDialog.xaml
    /// </summary>
    public partial class SvhPatientIdDialog : Window, INotifyPropertyChanged
    {
        public delegate void PropertyChangedNotificationHandler(String info);
        public event PropertyChangedEventHandler PropertyChanged;

        string m_KeyboardInput;
        SvhPatientId m_SvhPatientId;

        public SvhPatientIdDialog(SvhPatientId svhPatientId)
        {            
            this.m_SvhPatientId = svhPatientId;
            InitializeComponent();
            this.DataContext = this;            
        }

        public SvhPatientId SvhPatientId
        {
            get { return this.m_SvhPatientId; }
            set { this.m_SvhPatientId = value; }
        }

        public string KeyboardInput
        {
            get { return this.m_KeyboardInput; }
        }

        private void ButtonCreateOrder_Click(object sender, RoutedEventArgs e)
        {
            int mrnLength = this.TextBlockSvhMRN.Text.Trim().Length;
            int acctLength = this.TextBlockSVHAccount.Text.Trim().Length;
            if (mrnLength == 8 && acctLength == 9)
            {
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Can't create an order without a valid MRN and AccountNo.");
            }
        }   

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {            
            this.m_KeyboardInput = KeyConverter.HandleKeyboardInput(e.Key, this.m_KeyboardInput);

            string input = this.m_KeyboardInput.ToString();
            if ((input.StartsWith("00") == true || input.StartsWith("31") == true) && input.Length == 8)
            {
                this.m_SvhPatientId.MedicalRecordNumber = input;
                this.m_KeyboardInput = string.Empty;       
            }
            else if ((input.StartsWith("70") == true || input.StartsWith("30") == true) && input.Length == 9)
            {
                this.m_SvhPatientId.AccountNumber = input;
                this.m_KeyboardInput = string.Empty;
            }
            else if ((input.StartsWith("V70") == true || input.StartsWith("V30") == true) && input.Length == 10)
            {
                this.m_SvhPatientId.AccountNumber = input.Substring(1);
                this.m_KeyboardInput = string.Empty;
            }

            this.NotifyPropertyChanged("KeyboardInput");
        } 
        
        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            this.m_KeyboardInput = string.Empty;
            this.m_SvhPatientId.AccountNumber = string.Empty;
            this.m_SvhPatientId.MedicalRecordNumber = string.Empty;
            this.NotifyPropertyChanged("KeyboardInput");
        }
    }
}
