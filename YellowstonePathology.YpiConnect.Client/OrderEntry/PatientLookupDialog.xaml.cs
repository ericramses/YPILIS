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
    public partial class PatientLookupDialog : Window
    {
        YellowstonePathology.YpiConnect.Proxy.ClientOrderServiceProxy m_ClientOrderServiceProxy;        
        YellowstonePathology.Domain.ClientOrder.Model.ClientOrder m_ClientOrder;

        public PatientLookupDialog(YellowstonePathology.Domain.ClientOrder.Model.ClientOrder clientOrder, YellowstonePathology.YpiConnect.Proxy.ClientOrderServiceProxy clientOrderServiceProxy)
        {
            this.m_ClientOrder = clientOrder;
            this.m_ClientOrderServiceProxy = clientOrderServiceProxy;
           
            InitializeComponent();            
            
            this.DataContext = this;            
        }        

        private void ButtonSelect_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;            
            this.Close();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void TextBoxPatientName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }                
    }
}
