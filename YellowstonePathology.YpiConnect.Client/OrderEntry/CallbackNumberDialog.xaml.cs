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

namespace YellowstonePathology.YpiConnect.Client.OrderEntry
{
    /// <summary>
    /// Interaction logic for CallbackNumberDialog.xaml
    /// </summary>
    public partial class CallbackNumberDialog : Window
    {
        string m_CallbackNumber;

        public CallbackNumberDialog()
        {
            InitializeComponent();
            this.DataContext = this;
            this.Loaded += new RoutedEventHandler(CallbackNumberDialog_Loaded);
        }

        private void CallbackNumberDialog_Loaded(object sender, RoutedEventArgs e)
        {
            this.TextBoxCallbackNumber.Focus();
        }

        public string CallbackNumber
        {
            get { return this.m_CallbackNumber; }
            set { this.m_CallbackNumber = value; }
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
