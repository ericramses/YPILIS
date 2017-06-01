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
    /// Interaction logic for RetrospectiveReviews.xaml
    /// </summary>
    public partial class RetrospectiveReviews : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private YellowstonePathology.Business.Search.ReportSearchList m_ReportSearchList;
        private DateTime m_WorkDate;

        public RetrospectiveReviews()
        {            
            if (DateTime.Today.DayOfWeek == DayOfWeek.Monday)
            {
                this.m_WorkDate = this.m_WorkDate.AddDays(-3);
            }
            else
            {
                this.m_WorkDate = DateTime.Today.AddDays(-1);
            }

            this.m_ReportSearchList = YellowstonePathology.Business.Gateway.ReportSearchGateway.GetRetrospectiveReviews(this.m_WorkDate);
            this.NotifyPropertyChanged("ReportSearchList");

            InitializeComponent();

            this.DataContext = this;            
        }

        public DateTime WorkDate
        {
            get { return this.m_WorkDate; }
            set { this.m_WorkDate = value; }
        }

        private void AddRandomTest(DateTime workDate)
        {
            Business.Search.ReportSearchList list = YellowstonePathology.Business.Gateway.ReportSearchGateway.GetPossibleRetrospectiveReviews(workDate);
            
            int count = list.Count;
            double tenPercentOfCount = Math.Round((count * .1), 0);

            Random rnd = new Random();
            int i = 0;
            while (true)
            {
                int nextRnd = rnd.Next(0, count - 1);
                string nextMasterAccessionNo = list[nextRnd].MasterAccessionNo;

                Business.Test.AccessionOrder ao = Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(nextMasterAccessionNo, this);                
                if(ao.PanelSetOrderCollection.HasPanelSetBeenOrdered(262) == false)
                {
                    YellowstonePathology.Business.Test.RetrospectiveReview.RetrospectiveReviewTest retrospectiveReviewTest = new YellowstonePathology.Business.Test.RetrospectiveReview.RetrospectiveReviewTest();
                    YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo = new Business.Test.TestOrderInfo(retrospectiveReviewTest, null, false);
                    YellowstonePathology.Business.Visitor.OrderTestOrderVisitor orderTestOrderVisitor = new Business.Visitor.OrderTestOrderVisitor(testOrderInfo);
                    ao.TakeATrip(orderTestOrderVisitor);
                }

                i += 1;
                if (i == tenPercentOfCount)
                {
                    Business.Persistence.DocumentGateway.Instance.Push(this);
                    break;
                }
            }
        }

        private void ContextMenuDeleteReview_Click(object sender, RoutedEventArgs e)
        {
            if(this.ListViewReviews.SelectedItems.Count != 0)
            {
                foreach (Business.Search.ReportSearchItem item in this.ListViewReviews.SelectedItems)
                {
                    Business.Test.AccessionOrder ao = Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(item.MasterAccessionNo, this);
                    Business.Test.PanelSetOrder pso = ao.PanelSetOrderCollection.GetPanelSetOrder(item.ReportNo);
                    ao.PanelSetOrderCollection.Remove(pso);
                    Business.Persistence.DocumentGateway.Instance.Push(this);
                }

                this.m_ReportSearchList = YellowstonePathology.Business.Gateway.ReportSearchGateway.GetRetrospectiveReviews(this.m_WorkDate);
                this.NotifyPropertyChanged("ReportSearchList");
            }            
        }

        public YellowstonePathology.Business.Search.ReportSearchList ReportSearchList
        {
            get { return this.m_ReportSearchList; }
        }                       

        private void ButtonAddRandom_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_ReportSearchList.Count == 0)
            {
                this.AddRandomTest(this.m_WorkDate);
                this.m_ReportSearchList = YellowstonePathology.Business.Gateway.ReportSearchGateway.GetRetrospectiveReviews(this.m_WorkDate);
                this.NotifyPropertyChanged("ReportSearchList");
            }
            else
            {
                MessageBox.Show("Unable to add random test because some already exist.");
            }
        }

        private void ButtonAccessionOrderBack_Click(object sender, RoutedEventArgs e)
        {
            this.m_WorkDate = this.m_WorkDate.AddDays(-1);
            this.m_ReportSearchList = YellowstonePathology.Business.Gateway.ReportSearchGateway.GetRetrospectiveReviews(this.m_WorkDate);
            this.NotifyPropertyChanged(string.Empty);
        }

        private void ButtonAccessionOrderForward_Click(object sender, RoutedEventArgs e)
        {
            this.m_WorkDate = this.m_WorkDate.AddDays(1);
            this.m_ReportSearchList = YellowstonePathology.Business.Gateway.ReportSearchGateway.GetRetrospectiveReviews(this.m_WorkDate);
            this.NotifyPropertyChanged(string.Empty);
        }

        private void ButtonAccessionOrderRefresh_Click(object sender, RoutedEventArgs e)
        {
            this.m_ReportSearchList = YellowstonePathology.Business.Gateway.ReportSearchGateway.GetRetrospectiveReviews(this.m_WorkDate);
            this.NotifyPropertyChanged(string.Empty);
        }

        private void ButtonPrintList_Click(object sender, RoutedEventArgs e)
        {
            UI.Login.CaseListReport caseListReport = new UI.Login.CaseListReport(this.m_ReportSearchList);
            YellowstonePathology.UI.XpsDocumentViewer xpsDocumentViewer = new XpsDocumentViewer();
            xpsDocumentViewer.LoadDocument(caseListReport.FixedDocument);
            xpsDocumentViewer.ShowDialog();
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
