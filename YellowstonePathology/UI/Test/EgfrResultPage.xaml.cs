using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Xml.Linq;

namespace YellowstonePathology.UI.Test
{	
	public partial class EGFRResultPage : ResultControl, INotifyPropertyChanged 
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void NextEventHandler(object sender, EventArgs e);
		public event NextEventHandler Next;

		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private string m_PageHeaderText;

        private YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTestOrder m_PanelSetOrder;
        private string m_OrderedOnDescription;

        public EGFRResultPage(YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTestOrder egfrMutationAnalysisTestOrder,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.Business.User.SystemIdentity systemIdentity) : base(egfrMutationAnalysisTestOrder, accessionOrder)
        {
            this.m_PanelSetOrder = egfrMutationAnalysisTestOrder;
            this.m_AccessionOrder = accessionOrder;
            this.m_SystemIdentity = systemIdentity;

            this.m_PageHeaderText = "EGFR Results For: " + this.m_AccessionOrder.PatientDisplayName;

            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderByOrderTarget(this.m_PanelSetOrder.OrderedOnId);
            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetAliquotOrder(this.m_PanelSetOrder.OrderedOnId);
            this.m_OrderedOnDescription = specimenOrder.Description;
            if (aliquotOrder != null) this.m_OrderedOnDescription += ": " + aliquotOrder.Label;

            InitializeComponent();

            DataContext = this;

            this.m_ControlsNotDisabledOnFinal.Add(this.ButtonNext);
            this.m_ControlsNotDisabledOnFinal.Add(this.TextBlockShowDocument);
            this.m_ControlsNotDisabledOnFinal.Add(this.TextBlockUnfinalResults);
        }

        public string OrderedOnDescription
        {
            get { return this.m_OrderedOnDescription; }
        }

        public YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTestOrder PanelSetOrder
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

        private void HyperLinkPositiveL858R_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisDetectedResult result = new Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisDetectedResult();
            result.SetResult(this.m_PanelSetOrder, "L858R");
        }

        private void HyperLinkPositiveExon19_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisDetectedResult result = new Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisDetectedResult();
            result.SetResult(this.m_PanelSetOrder, "exon 19 insertion/deletion");
        }

        private void HyperLinkPositiveE709X_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisDetectedResult result = new Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisDetectedResult();
            result.SetResult(this.m_PanelSetOrder, "E709X");
        }

        private void HyperLinkPositiveG719X_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisDetectedResult result = new Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisDetectedResult();
            result.SetResult(this.m_PanelSetOrder, "G719X");
        }

        private void HyperLinkPositiveS768I_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisDetectedResult result = new Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisDetectedResult();
            result.SetResult(this.m_PanelSetOrder, "S768I");
        }

        private void HyperLinkPositiveL861Q_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisDetectedResult result = new Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisDetectedResult();
            result.SetResult(this.m_PanelSetOrder, "L861Q");
        }

        private void HyperLinkResistantT790M_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisResistanceResult result = new Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisResistanceResult();
            result.SetResult(this.m_PanelSetOrder, "T790M");
        }

        private void HyperLinkResistantExon20_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisResistanceResult result = new Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisResistanceResult();
            result.SetResult(this.m_PanelSetOrder, "Resistant Exon 20");
        }

        private void HyperLinkUninterpretable_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisUninterpretableResult result = new Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisUninterpretableResult();
            result.SetResult(this.m_PanelSetOrder, null);
        }        

        private void HyperLinkNotDetected_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisNotDetectedResult result = new Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisNotDetectedResult();
            result.SetResult(this.m_PanelSetOrder, null);
        }

		private void HyperLinkShowDocument_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisWordDocument report = new Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisWordDocument(this.m_AccessionOrder, this.m_PanelSetOrder, Business.Document.ReportSaveModeEnum.Draft);
            report.Render();

			YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_PanelSetOrder.ReportNo);
            string fileName = YellowstonePathology.Business.Document.CaseDocument.GetDraftDocumentFilePath(orderIdParser);
            YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWordViewer(fileName);
		}

        private void HyperLinkFinalizeResults_Click(object sender, RoutedEventArgs e)
        {
			YellowstonePathology.Business.Rules.MethodResult result = this.m_PanelSetOrder.IsOkToFinalize();
            if (result.Success == true)
            {
                YellowstonePathology.Business.Test.FinalizeTestResult finalizeTestResult = this.m_PanelSetOrder.Finish(this.m_AccessionOrder);
                this.HandleFinalizeTestResult(finalizeTestResult);
            }
            else
            {
				MessageBox.Show(result.Message);
            }            
        }

		private void HyperLinkUnfinalResults_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Rules.MethodResult result = this.m_PanelSetOrder.IsOkToUnfinalize();
			if (result.Success == true)
            {
                this.m_PanelSetOrder.Unfinalize();
            }
            else
            {
				MessageBox.Show(result.Message);
            } 
		}

		private void HyperLinkAcceptResults_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Rules.MethodResult result = this.m_PanelSetOrder.IsOkToAccept();
			if (result.Success == true)
			{
				this.m_PanelSetOrder.Accept();
			}
			else
			{
				MessageBox.Show(result.Message);
			}
		}

		private void HyperLinkUnacceptResults_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Rules.MethodResult result = this.m_PanelSetOrder.IsOkToUnaccept();
			if (result.Success == true)
			{
				this.m_PanelSetOrder.Unaccept();
			}
			else
			{
				MessageBox.Show(result.Message);
			}
		}

        private void HyperLinkCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {       
            if (this.Next != null) this.Next(this, new EventArgs());
        }        
	}
}
