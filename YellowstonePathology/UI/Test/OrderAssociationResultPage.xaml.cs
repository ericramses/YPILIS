using System;
using System.Windows;
using System.ComponentModel;

namespace YellowstonePathology.UI.Test
{
    /// <summary>
    /// Interaction logic for OrderAssociationResultPage.xaml
    /// </summary>
    public partial class OrderAssociationResultPage : ResultControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public delegate void NextEventHandler(object sender, EventArgs e);
        public event NextEventHandler Next;

        private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Test.OrderAssociation.OrderAssociationTestOrder m_PanelSetOrder;

        private string m_PageHeaderText;
        public OrderAssociationResultPage(YellowstonePathology.Business.Test.OrderAssociation.OrderAssociationTestOrder panelsetOrder,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.Business.User.SystemIdentity systemIdentity) : base(panelsetOrder, accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_SystemIdentity = systemIdentity;

            this.m_PanelSetOrder = panelsetOrder;
            this.m_PageHeaderText = this.m_PanelSetOrder.PanelSetName + " Results For: " + this.m_AccessionOrder.PatientDisplayName;

            InitializeComponent();

            DataContext = this;

            this.m_ControlsNotDisabledOnFinal.Add(this.ButtonClose);
            this.m_ControlsNotDisabledOnFinal.Add(this.TextBlockClose);
            this.m_ControlsNotDisabledOnFinal.Add(this.TextBlockUnfinalResults);
        }

        public YellowstonePathology.Business.Test.OrderAssociation.OrderAssociationTestOrder PanelSetOrder
        {
            get { return this.m_PanelSetOrder; }
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

        private void HyperLinkFinalizeResults_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_PanelSetOrder.Final == false)
            {
                this.m_PanelSetOrder.Finish(this.m_AccessionOrder);
            }
            else
            {
                MessageBox.Show("This case cannot be finalized because it is already final.");
            }
        }

        private void HyperLinkCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            if (this.Next != null) this.Next(this, new EventArgs());
        }

        /*private void HyperLinkShowDocument_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Test.TestCancelled.TestCancelledWordDocument report = new Business.Test.TestCancelled.TestCancelledWordDocument(this.m_AccessionOrder, this.m_ReportOrderTestCancelled, Business.Document.ReportSaveModeEnum.Draft);
            report.Render();

            YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_ReportOrderTestCancelled.ReportNo);
            string fileName = YellowstonePathology.Business.Document.CaseDocument.GetDraftDocumentFilePath(orderIdParser);
            YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWord(fileName);
        }*/

        private void HyperLinkAcceptResults_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_PanelSetOrder.Accepted == false)
            {
                this.m_PanelSetOrder.Accept();
            }
            else
            {
                MessageBox.Show("This case cannot be accepted because it is already accepted.");
            }
        }

        private void HyperLinkUnfinalResults_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_PanelSetOrder.Final == true)
            {
                this.m_PanelSetOrder.Unfinalize();
            }
            else
            {
                MessageBox.Show("This case cannot be unfinalized because it is not final.");
            }
        }

        private void HyperLinkUnacceptResults_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_PanelSetOrder.Final == false)
            {
                if (this.m_PanelSetOrder.Accepted == true)
                {
                    this.m_PanelSetOrder.Unaccept();
                }
                else
                {
                    MessageBox.Show("This case cannot be unaccepted because it is not accepted.");
                }
            }
            else
            {
                MessageBox.Show("This case cannot be unaccepted because it is final.");
            }
        }
    }
}
