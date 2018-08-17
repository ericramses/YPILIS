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
    /// <summary>
    /// Interaction logic for BRAFMutationPreviousResultsDialog.xaml
    /// </summary>
    public partial class BRAFMutationPreviousResultsDialog : Window
    {
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisTestOrder m_BRAFMutationAnalysisTestOrder;
        private List<YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisTestOrder> m_PanelSetOrderList;
        public BRAFMutationPreviousResultsDialog(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisTestOrder brafMutationAnalysisTestOrder)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_BRAFMutationAnalysisTestOrder = brafMutationAnalysisTestOrder;
            this.m_PanelSetOrderList = new List<Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisTestOrder>();
            this.FillPanelSetOrderList();
            InitializeComponent();
            DataContext = this;
        }

        public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
        }

        public List<YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisTestOrder> PanelSetOrderList
        {
            get { return this.m_PanelSetOrderList; }
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void FillPanelSetOrderList()
        {
            List<string> masterAccessionNos = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetPreviousBRAFMutationMasterAccessionNos(this.m_AccessionOrder.PatientId);
            foreach(string masterAccessionNo in masterAccessionNos)
            {
                YellowstonePathology.Business.Test.AccessionOrder ao = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(masterAccessionNo, this);
                YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisTestOrder testOrder = ao.PanelSetOrderCollection.GetPanelSetOrder(this.m_BRAFMutationAnalysisTestOrder.PanelSetId) as Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisTestOrder;
                if(testOrder.ReportNo != this.m_BRAFMutationAnalysisTestOrder.ReportNo) this.m_PanelSetOrderList.Add(testOrder);
            }
        }
    }
}
