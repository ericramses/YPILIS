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
        private Business.Test.AccessionOrder m_AccessionOrder;
        private Business.PreviousResultsCollection m_PreviousResultsCollection;
        private string m_TableName;

        public PreviousResultDialog(Business.Test.PanelSetOrder panelSetOrder, Business.Test.AccessionOrder accessionOrder)
        {            
            this.m_PanelSetOrder = panelSetOrder;
            this.m_AccessionOrder = accessionOrder;

            this.m_TableName = Business.Persistence.PersistenceHelper.GetTableName(panelSetOrder.GetType());

            Business.Gateway.PreviousResultGateway previousResultGateway = new Business.Gateway.PreviousResultGateway();
            this.m_PreviousResultsCollection = previousResultGateway.GetPreviousResultsByTestFinal(this.m_PanelSetOrder.PanelSetId, DateTime.Today.AddDays(-90), DateTime.Today, this.m_TableName);            

            InitializeComponent();
            DataContext = this;

            this.Left = (SystemParameters.PrimaryScreenWidth / 2);
            this.Top = (SystemParameters.PrimaryScreenHeight / 2) - (this.Height / 2);

        }

        public Business.Test.PanelSetOrder PanelSetOrder
        {
            get { return this.m_PanelSetOrder; }
        }    

        public YellowstonePathology.Business.PreviousResultsCollection PreviousResultsCollection
        {
            get { return this.m_PreviousResultsCollection; }
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
                    Business.Rules.MethodResult methodResult = pso.IsOkToSetPreviousResults(this.m_PanelSetOrder, this.m_AccessionOrder);
                    if (methodResult.Success == false)
                    {
                        string message = methodResult.Message + Environment.NewLine + "  Do you want to set these results?";
                        MessageBoxResult messageBoxResult = MessageBox.Show(message, "Questionable Results", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                        if (messageBoxResult == MessageBoxResult.Yes)
                        {
                            pso.SetPreviousResults(this.m_PanelSetOrder);
                        }
                    }
                    else
                    {
                        pso.SetPreviousResults(this.m_PanelSetOrder);
                    }
                }
            }
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_PanelSetOrder.Final == false && this.m_PanelSetOrder.Accepted == false)
            {
                this.m_PanelSetOrder.ClearPreviousResults();
            }
        }
    }
}
