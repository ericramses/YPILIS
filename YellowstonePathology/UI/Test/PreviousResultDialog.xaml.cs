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
        private Business.Interface.ICommonResult m_CommonResult;
        private Business.Search.ReportSearchList m_ReportSearchList;

        public PreviousResultDialog(Business.Test.PanelSetOrder panelSetOrder, Business.Interface.ICommonResult commonResult)
        {
            this.m_PanelSetOrder = panelSetOrder;
            this.m_CommonResult = commonResult;

            this.m_ReportSearchList = YellowstonePathology.Business.Gateway.ReportSearchGateway.GetReportSearchListByTestFinal(panelSetOrder.PanelSetId, DateTime.Today.AddDays(-90), DateTime.Today);            

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
                    Business.Interface.ICommonResult commonResult = (Business.Interface.ICommonResult)ao.PanelSetOrderCollection.GetPanelSetOrder(reportSearchItem.ReportNo);

                    this.m_CommonResult.Result = commonResult.Result;
                    this.m_PanelSetOrder.ResultCode = pso.ResultCode;
                    this.m_CommonResult.Interpretation = commonResult.Interpretation;
                    this.m_CommonResult.Method = commonResult.Method;
                    this.m_CommonResult.ReportDisclaimer = commonResult.ReportDisclaimer;
                    this.m_PanelSetOrder.ReportReferences = pso.ReportReferences;
                }
            }
        }
    }
}
