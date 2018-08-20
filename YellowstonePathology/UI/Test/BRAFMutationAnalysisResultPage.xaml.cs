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
using System.ComponentModel;

namespace YellowstonePathology.UI.Test
{
    /// <summary>
    /// Interaction logic for BRAFMutationAnalysisResultPage.xaml
    /// </summary>
    public partial class BRAFMutationAnalysisResultPage : ResultControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public delegate void NextEventHandler(object sender, EventArgs e);
        public event NextEventHandler Next;

        private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.UI.Navigation.PageNavigator m_PageNavigator;
        private string m_PageHeaderText;
        private string m_OrderedOnDescription;
        private YellowstonePathology.Business.Test.IndicationCollection m_IndicationCollection;
        private YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisResultCollection m_ResultCollection;
        private YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisTestOrder m_PanelSetOrder;
        private Window m_ParentWindow;

        public BRAFMutationAnalysisResultPage(YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisTestOrder panelSetOrderBraf,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.Business.User.SystemIdentity systemIdentity,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator) : base(panelSetOrderBraf, accessionOrder)
		{
            this.m_PanelSetOrder = panelSetOrderBraf;
            this.m_AccessionOrder = accessionOrder;
            this.m_SystemIdentity = systemIdentity;
            this.m_PageNavigator = pageNavigator;

            this.m_PageHeaderText = "BRAF Mutation Analysis Results For: " + this.m_AccessionOrder.PatientDisplayName + " (" + panelSetOrderBraf.ReportNo + ")";
            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetByAliquotOrderId(this.m_PanelSetOrder.OrderedOnId);
            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetAliquotOrder(this.m_PanelSetOrder.OrderedOnId);
            this.m_OrderedOnDescription = specimenOrder.Description + ": " + aliquotOrder.Label;

            this.m_IndicationCollection = YellowstonePathology.Business.Test.IndicationCollection.GetAll();
            this.m_ResultCollection = YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisResultCollection.GetUniqueResultChoices();

            InitializeComponent();

            this.m_ParentWindow = Window.GetWindow(this);
            DataContext = this;

            this.m_ControlsNotDisabledOnFinal.Add(this.ButtonNext);
            this.m_ControlsNotDisabledOnFinal.Add(this.TextBlockShowDocument);
            this.m_ControlsNotDisabledOnFinal.Add(this.TextBlockUnfinalResults);

            this.m_ControlsNotDisabledOnFinal.Add(this.TextBlockPreviousResults);
        }

        public YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisResultCollection ResultCollection
        {
            get { return this.m_ResultCollection; }
        }

        public YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisTestOrder PanelSetOrder
        {
            get { return this.m_PanelSetOrder; }
        }

        public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
        }

        public YellowstonePathology.Business.Test.IndicationCollection IndicationCollection
        {
            get { return this.m_IndicationCollection; }
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

        public string OrderedOnDescription
        {
            get { return this.m_OrderedOnDescription; }
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            this.Next(this, new EventArgs());
        }

        private void HyperLinkSetResults_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisResult.IsOkToSetResult(this.m_PanelSetOrder);
            if (methodResult.Success == true)
            {
                YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisResultCollection resultCollection = YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisResultCollection.GetAll();
                YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisResult result = resultCollection.GetResult(this.m_PanelSetOrder.ResultCode, this.m_PanelSetOrder.Indication);
                result.SetResults(this.m_PanelSetOrder);
            }
            else
            {
                MessageBox.Show(methodResult.Message);
            }
        }

        private void HyperLinkFinalize_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisResult.IsOkToFinal(this.m_AccessionOrder, this.m_PanelSetOrder);
            if (methodResult.Success == true)
            {
                YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisResultCollection resultCollection = YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisResultCollection.GetAll();
                YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisResult result = resultCollection.GetResult(this.m_PanelSetOrder.ResultCode, this.m_PanelSetOrder.Indication);
                result.FinalizeResults(this.m_PanelSetOrder, this.m_AccessionOrder);

                /*YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexTest krasStandardReflexTest = new YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexTest();
                if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(krasStandardReflexTest.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true) == true)
                {
                    YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexTestOrder krasStandardReflexTestOrder = (YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(krasStandardReflexTest.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true);
                    krasStandardReflexTestOrder.UpdateFromChildren(this.m_AccessionOrder, this.m_PanelSetOrder);
                }*/
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
                YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisResultCollection resultCollection = YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisResultCollection.GetAll();
                YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisResult result = resultCollection.GetResult(this.m_PanelSetOrder.ResultCode, this.m_PanelSetOrder.Indication);
                result.UnFinalizeResults(this.m_PanelSetOrder);
            }
            else
            {
                MessageBox.Show(methodResult.Message);
            }
        }

        private void HyperLinkAcceptResults_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisResult.IsOkToAccept(this.m_PanelSetOrder);
            if (methodResult.Success == true)
            {
                //YellowstonePathology.Business.Test.PanelOrder panelOrder = this.m_PanelSetOrder.PanelOrderCollection.GetUnacceptedPanelOrder();
                YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisResultCollection resultCollection = YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisResultCollection.GetAll();
                YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisResult result = resultCollection.GetResult(this.m_PanelSetOrder.ResultCode, this.m_PanelSetOrder.Indication);
                //result.AcceptResults(this.m_PanelSetOrder, panelOrder, this.m_SystemIdentity);
                result.AcceptResults(this.m_PanelSetOrder, this.m_SystemIdentity);
            }
            else
            {
                MessageBox.Show(methodResult.Message);
            }
        }

        private void HyperLinkUnacceptResults_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisResult.IsOkToUnaccept(this.m_PanelSetOrder);
            if (methodResult.Success == true)
            {
                //YellowstonePathology.Business.Test.PanelOrder panelOrder = this.m_PanelSetOrder.PanelOrderCollection.GetLastAcceptedPanelOrder();
                YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisResultCollection resultCollection = YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisResultCollection.GetAll();
                YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisResult result = resultCollection.GetResult(this.m_PanelSetOrder.ResultCode, this.m_PanelSetOrder.Indication);
                result.UnacceptResults(this.m_PanelSetOrder);
            }
            else
            {
                MessageBox.Show(methodResult.Message);
            }
        }

        private void HyperLinkShowDocument_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_PanelSetOrder.PanelOrderCollection.GetUnacceptedPanelCount() == 0)
            {
                YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisWordDocument report = new YellowstonePathology.Business.Test.BRAFMutationAnalysis.BRAFMutationAnalysisWordDocument(this.m_AccessionOrder, this.m_PanelSetOrder, Business.Document.ReportSaveModeEnum.Draft);
                report.Render();

                YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_PanelSetOrder.ReportNo);
                string fileName = YellowstonePathology.Business.Document.CaseDocument.GetDraftDocumentFilePath(orderIdParser);
                YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWordViewer(fileName);
            }
            else
            {
                MessageBox.Show("The results must be accepted before the document can be viewed.", "Accept then view");
            }
        }

        private void ComboBoxResult_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ComboBoxResult.SelectedItem != null)
            {
                YellowstonePathology.Business.Test.TestResult testResult = (YellowstonePathology.Business.Test.TestResult)this.ComboBoxResult.SelectedItem;
                this.m_PanelSetOrder.ResultCode = testResult.ResultCode;
            }
        }

        private void HyperLinkPreviousResults_Click(object sender, RoutedEventArgs e)
        {
            BRAFMutationPreviousResultsDialog dlg = new Test.BRAFMutationPreviousResultsDialog(this.m_PanelSetOrder);
            dlg.ShowDialog();
        }
    }
}
