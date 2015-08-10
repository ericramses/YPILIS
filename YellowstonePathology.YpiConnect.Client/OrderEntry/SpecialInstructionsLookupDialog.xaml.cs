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
    public partial class SpecialInstructionsLookupDialog : Window
    {
        YellowstonePathology.YpiConnect.Proxy.ClientOrderServiceProxy m_ClientOrderServiceProxy;        
        YellowstonePathology.Domain.ClientOrder.Model.ClientOrder m_ClientOrder;
        List<string> m_Instructions;

        public SpecialInstructionsLookupDialog(YellowstonePathology.Domain.ClientOrder.Model.ClientOrder clientOrder, YellowstonePathology.YpiConnect.Proxy.ClientOrderServiceProxy clientOrderServiceProxy)
        {
            this.m_ClientOrder = clientOrder;
            this.m_ClientOrderServiceProxy = clientOrderServiceProxy;

            this.m_Instructions = new List<string>();
            this.m_Instructions.Add("Microbiology has been ordered");

            InitializeComponent();            
            
            this.DataContext = this;            
        }

        public List<string> Instructions
        {
            get { return this.m_Instructions; }            
        }

        private void ButtonSelect_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            if (this.ListViewInstructions.SelectedItems.Count != 0)
            {
               //this.m_ClientOrder.SpecialInstructions = (string)this.ListViewInstructions.SelectedItem;
            }
            this.Close();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }        
    }
}
