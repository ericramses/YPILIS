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
        private Business.Test.PanelSetOrder m_PanelSetOrder;
        private Business.Interface.IPreviousResult m_PreviousResult;
        private Business.Search.ReportSearchList m_ReportSearchList;

        public PreviousResultDialog(Business.Interface.IPreviousResult previousResult)
        {
            this.m_PanelSetOrder = (Business.Test.PanelSetOrder)previousResult;
            this.m_PreviousResult = previousResult;

            this.m_ReportSearchList = YellowstonePathology.Business.Gateway.ReportSearchGateway.GetReportSearchListByTestFinal(this.m_PanelSetOrder.PanelSetId, DateTime.Today.AddDays(-90), DateTime.Today);            

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
                if(this.m_PanelSetOrder.Final == false && this.m_PanelSetOrder.Accepted == false)
                {
                    Business.Search.ReportSearchItem reportSearchItem = (Business.Search.ReportSearchItem)this.ListViewPreviousResults.SelectedItem;
                    Business.Test.AccessionOrder ao = Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(reportSearchItem.MasterAccessionNo, this);
                    Business.Test.PanelSetOrder pso = ao.PanelSetOrderCollection.GetPanelSetOrder(reportSearchItem.ReportNo);
                    Business.Interface.IPreviousResult previousResult = (Business.Interface.IPreviousResult)ao.PanelSetOrderCollection.GetPanelSetOrder(reportSearchItem.ReportNo);
                    previousResult.SetPreviousResult(this.m_PanelSetOrder);
                }
            }
        }
    }
}
