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

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for PantherOrdersDialog.xaml
    /// </summary>
    public partial class PantherOrdersDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private YellowstonePathology.Business.Test.PantherOrderList m_PantherOrderList;

        public PantherOrdersDialog()
        {
            this.m_PantherOrderList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetPantherOrdersNotAliquoted();
            InitializeComponent();
            this.DataContext = this;
        }

        public YellowstonePathology.Business.Test.PantherOrderList PantherOrderList
        {
            get { return this.m_PantherOrderList; }
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonPrint_Click(object sender, RoutedEventArgs e)
        {
            PantherOrdersReport pantherOrdersReport = new PantherOrdersReport(this.m_PantherOrderList);
            pantherOrdersReport.Print();
        }

        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            string selectedValue = this.ComboBoxListType.SelectedValue.ToString();
            switch (selectedValue)
            {
                case "Not Aliquoted":
                    this.m_PantherOrderList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetPantherOrdersNotAliquoted();
                    break;
                case "Not Accepted":
                    this.m_PantherOrderList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetPantherOrdersNotAccepted();
                    break;
                case "Not Final":
                    this.m_PantherOrderList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetPantherOrdersNotFinal();
                    break;
                case "Final":
                    this.m_PantherOrderList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetPantherOrdersFinal();
                    break;
            }
            this.NotifyPropertyChanged("PantherOrderList");
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
