using System;
using System.Windows;
using System.ComponentModel;

namespace YellowstonePathology.UI.Test
{
    /// <summary>
    /// Interaction logic for HER2AmplificationSummaryResultPage.xaml
    /// </summary>
    public partial class HER2AmplificationSummaryResultPage : ResultControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public delegate void NextEventHandler(object sender, EventArgs e);
        public event NextEventHandler Next;

        private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;
        private YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTestOrder m_HER2AmplificationByISHTestOrder;
        private string m_IHCScore;
        private string m_PageHeaderText;
        private string m_OrderedOnDescription;
        private string m_SummaryMessage;


        public HER2AmplificationSummaryResultPage(YellowstonePathology.Business.Test.PanelSetOrder testOrder,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.Business.User.SystemIdentity systemIdentity) : base(testOrder, accessionOrder)
        {
            this.m_PanelSetOrder = testOrder;
            this.m_AccessionOrder = accessionOrder;
            this.m_SystemIdentity = systemIdentity;

            this.m_PageHeaderText = this.m_PanelSetOrder.PanelSetName + " Results For: " + this.m_AccessionOrder.PatientDisplayName;

            YellowstonePathology.Business.Interface.IOrderTarget orderTarget = this.m_AccessionOrder.SpecimenOrderCollection.GetOrderTarget(this.m_PanelSetOrder.OrderedOnId);
            this.m_OrderedOnDescription = orderTarget.GetDescription();
            YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTest ishTest = new Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTest();
            YellowstonePathology.Business.Test.Her2AmplificationByIHC.Her2AmplificationByIHCTest ihcTest = new Business.Test.Her2AmplificationByIHC.Her2AmplificationByIHCTest();
            bool hasISH = false;
            if(this.m_AccessionOrder.PanelSetOrderCollection.Exists(ishTest.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true) == true)
            {
                this.m_HER2AmplificationByISHTestOrder = (Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(ishTest.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true);
                hasISH = true;
            }
            if(this.m_AccessionOrder.PanelSetOrderCollection.Exists(ihcTest.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true) == true)
            {
                Business.Test.Her2AmplificationByIHC.PanelSetOrderHer2AmplificationByIHC ihcTestOrder = (Business.Test.Her2AmplificationByIHC.PanelSetOrderHer2AmplificationByIHC)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(ihcTest.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true);
                if (ihcTestOrder.Accepted == true && hasISH == true)
                {
                    this.m_IHCScore = ihcTestOrder.Score;
                    this.m_SummaryMessage = "The HER2 By D-ISH Amplification, report number " + this.m_HER2AmplificationByISHTestOrder.ReportNo + ", may now be accepted and finalized.";
                }
                else if(hasISH == true)
                {
                    this.m_SummaryMessage = "The HER2 By D-ISH Amplification must be accepted before this case can be finalized.";
                }
            }

            if(string.IsNullOrEmpty(this.m_SummaryMessage) == true)
            {
                this.m_SummaryMessage = "There is no HER2 By D-ISH Amplification.  Please cancel this test.";
            }

            InitializeComponent();

            DataContext = this;


            this.m_ControlsNotDisabledOnFinal.Add(this.ButtonNext);
            this.m_ControlsNotDisabledOnFinal.Add(this.TextBlockUnfinalResults);
        }

        public YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTestOrder HER2AmplificationByISHTestOrder
        {
            get { return this.m_HER2AmplificationByISHTestOrder; }
        }

        public string IHCScore
        {
            get { return this.m_IHCScore; }
        }

        public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
        }

        public string OrderedOnDescription
        {
            get { return this.m_OrderedOnDescription; }
        }

        public string SummaryMessage
        {
            get { return this.m_SummaryMessage; }
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public string PageHeaderText
        {
            get { return this.m_PageHeaderText; }
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            this.Next(this, new EventArgs());
        }

        private void HyperLinkFinalize_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = this.m_PanelSetOrder.IsOkToFinalize();
            if (methodResult.Success == true)
            {
                this.m_PanelSetOrder.Finish(this.m_AccessionOrder);
            }
            else
            {
                MessageBox.Show(methodResult.Message);
            }
        }

        private void HyperLinkUnfinalResults_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = this.m_PanelSetOrder.IsOkToUnfinalize();
            if (methodResult.Success == true)
            {
                this.m_PanelSetOrder.Unfinalize();
            }
            else
            {
                MessageBox.Show(methodResult.Message);
            }
        }

        private void HyperLinkShowDocument_Click(object sender, RoutedEventArgs e)
        {
                YellowstonePathology.Business.Test.HER2AmplificationSummary.HER2AmplificationSummaryWordDocument report = new YellowstonePathology.Business.Test.HER2AmplificationSummary.HER2AmplificationSummaryWordDocument(this.m_AccessionOrder, this.m_PanelSetOrder, Business.Document.ReportSaveModeEnum.Draft);
                report.Render();
                YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWordViewer(report.SaveFileName);
        }

        private void HyperLinkAcceptResults_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = this.m_PanelSetOrder.IsOkToAccept();
            if (methodResult.Success == true)
            {
                this.m_PanelSetOrder.Accept();
            }
            else
            {
                MessageBox.Show(methodResult.Message);
            }
        }

        private void HyperLinkUnacceptResults_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = this.m_PanelSetOrder.IsOkToUnaccept();
            if (methodResult.Success == true)
            {
                this.m_PanelSetOrder.Unaccept();
            }
            else
            {
                MessageBox.Show(methodResult.Message);
            }
        }

        private void HyperLinkSetResults_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}

