using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;

namespace YellowstonePathology.UI.Billing
{
    /// <summary>
    /// Interaction logic for AutomatedBillingDialog.xaml
    /// </summary>
    public partial class AutomatedBillingDialog : Window
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private AutomationList m_AutomationList;
        private DateTime m_PostDate;

        public AutomatedBillingDialog()
        {
            this.m_PostDate = DateTime.Today;
            this.m_AutomationList = AutomationList.GetList(this.m_PostDate);
            InitializeComponent();
            this.DataContext = this;
        }

        public AutomationList AutomationList
        {
            get { return this.m_AutomationList; }
        }

        private void ButtonGetList_Click(object sender, RoutedEventArgs e)
        {
            this.m_AutomationList = AutomationList.GetList(this.m_PostDate);
            this.NotifyPropertyChanged("AutomationList");
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

        private void ButtonRun_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
