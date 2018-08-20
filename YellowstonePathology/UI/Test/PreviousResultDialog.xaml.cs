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

namespace YellowstonePathology.UI.Test
{    
    public partial class PreviousResultDialog : Window
    {
        private Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisTestOrder m_BRAFMutationAnalysisTestOrder;
        private Business.Search.ReportSearchList m_ReportSearchList;

        public PreviousResultDialog(Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisTestOrder brafMutationAnalysisTestOrder)
        {
            this.m_BRAFMutationAnalysisTestOrder = brafMutationAnalysisTestOrder;
            this.m_ReportSearchList = YellowstonePathology.Business.Gateway.ReportSearchGateway.GetReportSearchListByTestFinal(brafMutationAnalysisTestOrder.PanelSetId, DateTime.Today.AddDays(-90), DateTime.Today);            

            InitializeComponent();
            DataContext = this;
        }        

        public YellowstonePathology.Business.Search.ReportSearchList ReportSearchList
        {
            get { return this.m_ReportSearchList; }
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonSetResults_Click(object sender, RoutedEventArgs e)
        {
            if(this.ListViewPreviousResults.SelectedItem != null)
            {
                if(this.m_BRAFMutationAnalysisTestOrder.Final == false && this.m_BRAFMutationAnalysisTestOrder.Accepted == false)
                {
                    Business.Search.ReportSearchItem reportSearchItem = (Business.Search.ReportSearchItem)this.ListViewPreviousResults.SelectedItem;
                    Business.Test.AccessionOrder ao = Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(reportSearchItem.MasterAccessionNo, this);
                    Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisTestOrder pso = (Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisTestOrder)ao.PanelSetOrderCollection.GetPanelSetOrder(reportSearchItem.ReportNo);
                    this.m_BRAFMutationAnalysisTestOrder.Result = pso.Result;
                    this.m_BRAFMutationAnalysisTestOrder.ResultCode = pso.ResultCode;
                    this.m_BRAFMutationAnalysisTestOrder.Interpretation = pso.Interpretation;
                    this.m_BRAFMutationAnalysisTestOrder.Method = pso.Method;
                    this.m_BRAFMutationAnalysisTestOrder.ReportDisclaimer = pso.ReportDisclaimer;
                    this.m_BRAFMutationAnalysisTestOrder.ReportReferences = pso.ReportReferences;
                }
            }
        }
    }
}
