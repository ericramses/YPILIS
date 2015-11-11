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

namespace YellowstonePathology.UI.Test
{
    /// <summary>
    /// Interaction logic for AcidWashOrdersDialog.xaml
    /// </summary>
    public partial class AcidWashOrdersDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private YellowstonePathology.Business.Test.ThinPrepPap.AcidWashSearchList m_AcidWashSearchList;

        public AcidWashOrdersDialog()
        {
            this.m_AcidWashSearchList = Business.Gateway.ReportSearchGateway.GetAcidWashSearchList(DateTime.Today.AddMonths(-3));

            InitializeComponent();

            DataContext = this;
        }

        public Business.Test.ThinPrepPap.AcidWashSearchList AcidWashSearchList
        {
            get { return this.m_AcidWashSearchList; }
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void ListViewCaseList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.ListViewCaseList.SelectedItem != null)
            {
                Business.Test.ThinPrepPap.AcidWashSearchItem acidWashSearchItem = (Business.Test.ThinPrepPap.AcidWashSearchItem)this.ListViewCaseList.SelectedItem;
                AcidWashResultDialog acidWashResultDialog = new AcidWashResultDialog(acidWashSearchItem.ReportNo);
                acidWashResultDialog.ShowDialog();
                this.m_AcidWashSearchList = Business.Gateway.ReportSearchGateway.GetAcidWashSearchList(DateTime.Today.AddMonths(-3));
                this.NotifyPropertyChanged("AcidWashSearchList");
            }
        }
    }
}
