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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace YellowstonePathology.UI.Login.FinalizeAccession
{
    /// <summary>
    /// Interaction logic for HL7CopyPage.xaml
    /// </summary>
    public partial class HL7CopyPage : UserControl
    {
        public delegate void DoNotDistributeEventHandler(object sender, EventArgs e);
        public event DoNotDistributeEventHandler DoNotDistribute;

        public delegate void DistributeEventHandler(object sender, CustomEventArgs.PhysicianClientDistributionReturnEventArgs e);
        public event DistributeEventHandler Distribute;

        private YellowstonePathology.Business.Client.Model.PhysicianClientDistributionListItem m_PhysicianClientDistributionListItem;
        private string m_PageHeaderText = "Copy To Distribution Page";

        public HL7CopyPage(YellowstonePathology.Business.Client.Model.PhysicianClientDistributionListItem physicianClientDistributionListItem)
        {
            this.m_PhysicianClientDistributionListItem = physicianClientDistributionListItem;
            InitializeComponent();

            DataContext = this;
        }

        public string PageHeaderText
        {
            get { return this.m_PageHeaderText; }
        }

        private void ButtonFax_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.m_PhysicianClientDistributionListItem.FaxNumber) == false)
            {
                this.m_PhysicianClientDistributionListItem.DistributionType = YellowstonePathology.Business.ReportDistribution.Model.DistributionType.FAX;
                this.Distribute(this, new CustomEventArgs.PhysicianClientDistributionReturnEventArgs(this.m_PhysicianClientDistributionListItem));
            }
            else
            {
                MessageBox.Show("Unable to send a Fax as the Fax number is not available.");
            }
        }

        private void ButtonWeb_Click(object sender, RoutedEventArgs e)
        {
            this.m_PhysicianClientDistributionListItem.DistributionType = YellowstonePathology.Business.ReportDistribution.Model.DistributionType.WEBSERVICE;
            this.Distribute(this, new CustomEventArgs.PhysicianClientDistributionReturnEventArgs(this.m_PhysicianClientDistributionListItem));
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            this.DoNotDistribute(this, new EventArgs());
        }
    }
}
