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

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for DailyDOHDistributionDialog.xaml
    /// </summary>
    public partial class DailyDOHDistributionDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistributionCollection m_TestOrderReportDistributionCollection;
        private DateTime m_DateAdded;
        string m_DistributionType;
        private List<string> m_DistributionTypes;

        public DailyDOHDistributionDialog()
        {
            this.m_DistributionTypes = new List<string>();
            this.m_DistributionTypes.Add("MTDOH");
            this.m_DistributionTypes.Add("WYDOH");
            this.m_DateAdded = DateTime.Today;
            this.m_TestOrderReportDistributionCollection = new YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistributionCollection();

            InitializeComponent();

            this.DataContext = this;
            this.ComboBoxDistributionType.SelectedIndex = 0;
        }

        public DateTime DateAdded
        {
            get { return this.m_DateAdded; }
            set
            {
                if (this.m_DateAdded != value)
                {
                    this.m_DateAdded = value;
                    this.GetDistributions();
                    this.NotifyPropertyChanged("DateAdded");
                }
            }
        }

        public string DistributionType
        {
            get { return this.m_DistributionType; }
            set { this.m_DistributionType = value; }
        }

        public List<string> DistributionTypes
        {
            get { return this.m_DistributionTypes; }
        }

        public YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistributionCollection TestOrderReportDistributionCollection
        {
            get { return this.m_TestOrderReportDistributionCollection; }
        }

        private void GetDistributions()
        {
            this.m_TestOrderReportDistributionCollection = YellowstonePathology.Business.Gateway.ReportDistributionGateway.GetReportDistributionCollectionByDateTumorRegistry(this.m_DateAdded, this.m_DateAdded.AddHours(24), this.m_DistributionType);
            this.NotifyPropertyChanged("TestOrderReportDistributionCollection");
        }

        private void ComboBoxDistributionType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ComboBoxDistributionType.SelectedItem != null)
            {
                this.m_DistributionType = (string)this.ComboBoxDistributionType.SelectedItem;
                this.GetDistributions();
            }
        }

        private void ButtonDateBack_Click(object sender, RoutedEventArgs args)
        {
            this.DateAdded = this.m_DateAdded.AddDays(-1);
        }

        private void ButtonDateForward_Click(object sender, RoutedEventArgs args)
        {
            this.DateAdded = this.m_DateAdded.AddDays(1);
        }

        private void ButtonDistribute_Click(object sender, RoutedEventArgs args)
        {
            foreach (YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistribution testOrderReportDistribution in this.ListViewDistributions.SelectedItems)
            {
                if (testOrderReportDistribution.Distributed == false)
                {
                    testOrderReportDistribution.ScheduleForDistribution(DateTime.Now);
                }
            }
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
    }
}
