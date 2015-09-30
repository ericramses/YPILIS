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
        private YellowstonePathology.Business.Test.PantherAliquotList m_PantherAliquotList;

        public PantherOrdersDialog()
        {
            this.m_PantherAliquotList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetPantherOrdersNotAliquoted();
            this.m_PantherOrderList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetPantherOrdersNotAcceptedHPV();
            InitializeComponent();
            this.DataContext = this;
        }

        public YellowstonePathology.Business.Test.PantherOrderList PantherOrderList
        {
            get { return this.m_PantherOrderList; }
        }

        public YellowstonePathology.Business.Test.PantherAliquotList PantherAliquotList
        {
            get { return this.m_PantherAliquotList; }
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonPrint_Click(object sender, RoutedEventArgs e)
        {
            PantherAliquotReport pantherAliquotReport = new PantherAliquotReport(this.m_PantherAliquotList);
            pantherAliquotReport.Print();
        }

        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {            
            switch (this.ComboBoxListType.SelectedIndex)
            {                
                case 0:
                    this.m_PantherOrderList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetPantherOrdersNotAcceptedHPV();
                    break;
                case 1:
                    this.m_PantherOrderList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetPantherOrdersNotFinalHPV();
                    break;
                case 2:
                    this.m_PantherOrderList = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetPantherOrdersFinalHPV();
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
