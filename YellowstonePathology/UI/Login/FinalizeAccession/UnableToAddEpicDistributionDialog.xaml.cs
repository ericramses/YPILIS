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

namespace YellowstonePathology.UI.Login.FinalizeAccession
{
    /// <summary>
    /// Interaction logic for UnableToAddEpicDistributionDialog.xaml
    /// </summary>
    public partial class UnableToAddEpicDistributionDialog : Window
    {
        private YellowstonePathology.Business.Client.Model.PhysicianClientDistributionListItem m_PhysicianClientDistributionListItem;

        public UnableToAddEpicDistributionDialog(YellowstonePathology.Business.Client.Model.PhysicianClientDistributionListItem physicianClientDistributionListItem)
        {
            this.m_PhysicianClientDistributionListItem = physicianClientDistributionListItem;

            InitializeComponent();
        }

        private void ButtonFax_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.m_PhysicianClientDistributionListItem.FaxNumber) == false)
            {
                this.m_PhysicianClientDistributionListItem.DistributionType = YellowstonePathology.Business.ReportDistribution.Model.DistributionType.FAX;
                this.Close();
            }
            else
            {
                MessageBox.Show("Unable to send a Fax as the Fax number is not available.");
            }
        }

        private void ButtonWeb_Click(object sender, RoutedEventArgs e)
        {
            this.m_PhysicianClientDistributionListItem.DistributionType = YellowstonePathology.Business.ReportDistribution.Model.DistributionType.WEBSERVICE;
            this.Close();
        }

        private void ButtonDoNotAdd_Click(object sender, RoutedEventArgs e)
        {
            this.m_PhysicianClientDistributionListItem.DistributionType = YellowstonePathology.Business.ReportDistribution.Model.DistributionType.DONOTDISTRIBUTE;
            this.Close();
        }
    }
}
