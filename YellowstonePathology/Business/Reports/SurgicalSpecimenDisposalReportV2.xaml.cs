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

namespace YellowstonePathology.Business.Reports
{
    /// <summary>
    /// Interaction logic for SurgicalSpecimenDisposalReportV2.xaml
    /// </summary>
    public partial class SurgicalSpecimenDisposalReportV2 : FixedDocument
    {
        private DisposalReportData m_DisposalReportData;

        public SurgicalSpecimenDisposalReportV2(DateTime disposalDate)
        {
            this.m_DisposalReportData = YellowstonePathology.Business.Gateway.SlideDisposalReportGateway.GetSpecimenDisposalReport_1(disposalDate);

            InitializeComponent();

            this.DataContext = this;
        }

        public DisposalReportData DisposalReportData
        {
            get { return this.m_DisposalReportData; }
        }
    }
}
