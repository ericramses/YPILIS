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
	public partial class HPV1618SolidTumorResultPage : ResultControl, INotifyPropertyChanged 
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void NextEventHandler(object sender, EventArgs e);
		public event NextEventHandler Next;

		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private string m_PageHeaderText;

		private YellowstonePathology.Business.Test.HPV1618SolidTumor.HPV1618SolidTumorTestOrder m_HPV1618SolidTumorTestOrder;
		private YellowstonePathology.UI.Navigation.PageNavigator m_PageNavigator;
        private List<string> m_IndicationList;

		public HPV1618SolidTumorResultPage(YellowstonePathology.Business.Test.HPV1618SolidTumor.HPV1618SolidTumorTestOrder hpv1618SolidTumorTestOrder,
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity,
			YellowstonePathology.UI.Navigation.PageNavigator pageNavigator) : base(hpv1618SolidTumorTestOrder, accessionOrder)
		{
			this.m_HPV1618SolidTumorTestOrder = hpv1618SolidTumorTestOrder;
			this.m_AccessionOrder = accessionOrder;
			this.m_SystemIdentity = systemIdentity;
			this.m_PageNavigator = pageNavigator;

            this.m_IndicationList = YellowstonePathology.Business.Test.HPV1618SolidTumor.HPV1618SolidTumorIndication.GetIndicationList();
			this.m_PageHeaderText = "HPV 16 18/45 Solid Tumor Results For: " + this.m_AccessionOrder.PatientDisplayName;
			
			InitializeComponent();

			DataContext = this;

            this.m_ControlsNotDisabledOnFinal.Add(this.ButtonNext);
            this.m_ControlsNotDisabledOnFinal.Add(this.TextBlockShowDocument);
            this.m_ControlsNotDisabledOnFinal.Add(this.TextBlockUnfinalResults);
        }

        public List<string> IndicationList
        {
            get { return this.m_IndicationList; }
        }

		public YellowstonePathology.Business.Test.HPV1618SolidTumor.HPV1618SolidTumorTestOrder PanelSetOrder
		{
			get { return this.m_HPV1618SolidTumorTestOrder; }
		}

		public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
		{
			get { return this.m_AccessionOrder; }
		}

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}
		
		public string HyperlinkFinalizeText
		{
			get
			{
				string result = "Finalize";
				if (this.m_HPV1618SolidTumorTestOrder.Final == true) result = "Unfinalize";
				return result;
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
			YellowstonePathology.Business.Rules.MethodResult methodResult = this.m_HPV1618SolidTumorTestOrder.IsOkToFinalize();
			if (methodResult.Success == true)
			{
				YellowstonePathology.Business.Test.HPV1618SolidTumor.HPV1618SolidTumorResultCollection resultCollection = YellowstonePathology.Business.Test.HPV1618SolidTumor.HPV1618SolidTumorResultCollection.GetAllResults();
				YellowstonePathology.Business.Test.HPV1618SolidTumor.HPV1618SolidTumorResult hpv1618Result = resultCollection.GetResult(this.m_HPV1618SolidTumorTestOrder.ResultCode);
				hpv1618Result.FinalizeResults(this.m_HPV1618SolidTumorTestOrder, this.m_SystemIdentity, this.m_AccessionOrder);

                YellowstonePathology.Business.ReportDistribution.Model.MultiTestDistributionHandler multiTestDistributionHandler = YellowstonePathology.Business.ReportDistribution.Model.MultiTestDistributionHandlerFactory.GetHandler(this.m_AccessionOrder);
                multiTestDistributionHandler.Set();

                if (this.m_AccessionOrder.PanelSetOrderCollection.WomensHealthProfileExists() == true)
                {
                    this.m_AccessionOrder.PanelSetOrderCollection.GetWomensHealthProfile().SetExptectedFinalTime(this.m_AccessionOrder);
                }
			}
			else
			{
				MessageBox.Show(methodResult.Message);
			}
		}

		private void HyperLinkUnfinalResults_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Rules.MethodResult methodResult = this.m_HPV1618SolidTumorTestOrder.IsOkToUnfinalize();
			if (methodResult.Success == true)
			{
                this.m_HPV1618SolidTumorTestOrder.Unfinalize();
			}
			else
			{
				MessageBox.Show(methodResult.Message);
			}
		}

		private void HyperLinkUnacceptResults_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Rules.MethodResult methodResult = this.m_HPV1618SolidTumorTestOrder.IsOkToUnaccept();
			if (methodResult.Success == true)
			{
                this.m_HPV1618SolidTumorTestOrder.Unaccept();
			}
			else
			{
				MessageBox.Show(methodResult.Message);
			}
		}

		private void HyperLinkAcceptResults_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Rules.MethodResult methodResult = YellowstonePathology.Business.Test.HPV1618SolidTumor.HPV1618SolidTumorResult.IsOkToAccept(this.m_HPV1618SolidTumorTestOrder);
			if (methodResult.Success == true)
			{
                this.m_HPV1618SolidTumorTestOrder.Accept();
			}
			else
			{
				MessageBox.Show(methodResult.Message);
			}         
		}

		private void HyperLinkShowDocument_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Test.HPV1618SolidTumor.HPV1618SolidTumorWordDocument report = new Business.Test.HPV1618SolidTumor.HPV1618SolidTumorWordDocument(this.m_AccessionOrder, this.m_HPV1618SolidTumorTestOrder, Business.Document.ReportSaveModeEnum.Draft);
			report.Render();

			YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_HPV1618SolidTumorTestOrder.ReportNo);
			string fileName = YellowstonePathology.Business.Document.CaseDocument.GetDraftDocumentFilePath(orderIdParser);
			YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWordViewer(fileName);
		}

        private void HyperLinkBothNegative_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = YellowstonePathology.Business.Test.HPV1618SolidTumor.HPV1618SolidTumorResult.IsOkToSetResult(this.m_HPV1618SolidTumorTestOrder);
            if (methodResult.Success == true)
            {
                if (this.m_HPV1618SolidTumorTestOrder.Indication == Business.Test.HPV1618SolidTumor.HPV1618SolidTumorIndication.SquamousCellCarcinomaAnalRegion)
                {
                    YellowstonePathology.Business.Test.HPV1618SolidTumor.HPV1618SolidTumorAnalRegionResult result = new Business.Test.HPV1618SolidTumor.HPV1618SolidTumorAnalRegionResult();
                    result.SetResult("Negative", "Negative");
                    result.SetResult(this.m_HPV1618SolidTumorTestOrder);
                }
                else if (this.m_HPV1618SolidTumorTestOrder.Indication == Business.Test.HPV1618SolidTumor.HPV1618SolidTumorIndication.SquamousCellCarcinomaHeadAndNeck)
                {
                    YellowstonePathology.Business.Test.HPV1618SolidTumor.HPV1618BothNegativeResult result = new Business.Test.HPV1618SolidTumor.HPV1618BothNegativeResult();
                    result.SetResult(this.m_HPV1618SolidTumorTestOrder);
                }                
			}
			else
			{
				MessageBox.Show(methodResult.Message);
			}
		}

        private void HyperLink16Positive18Negative_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = YellowstonePathology.Business.Test.HPV1618SolidTumor.HPV1618SolidTumorResult.IsOkToSetResult(this.m_HPV1618SolidTumorTestOrder);
            if (methodResult.Success == true)
            {
                if (this.m_HPV1618SolidTumorTestOrder.Indication == Business.Test.HPV1618SolidTumor.HPV1618SolidTumorIndication.SquamousCellCarcinomaAnalRegion)
                {
                    YellowstonePathology.Business.Test.HPV1618SolidTumor.HPV1618SolidTumorAnalRegionResult result = new Business.Test.HPV1618SolidTumor.HPV1618SolidTumorAnalRegionResult();
                    result.SetResult("Positive", "Negative");
                    result.SetResult(this.m_HPV1618SolidTumorTestOrder);
                }
                else if (this.m_HPV1618SolidTumorTestOrder.Indication == Business.Test.HPV1618SolidTumor.HPV1618SolidTumorIndication.SquamousCellCarcinomaHeadAndNeck)
                {
                    YellowstonePathology.Business.Test.HPV1618SolidTumor.HPV16PositiveHPV18NegativeResult result = new Business.Test.HPV1618SolidTumor.HPV16PositiveHPV18NegativeResult();
                    result.SetResult(this.m_HPV1618SolidTumorTestOrder);
                }                
			}
			else
			{
				MessageBox.Show(methodResult.Message);
			}
		}

        private void HyperLink16Negative18Positive_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = YellowstonePathology.Business.Test.HPV1618SolidTumor.HPV1618SolidTumorResult.IsOkToSetResult(this.m_HPV1618SolidTumorTestOrder);
            if (methodResult.Success == true)
            {
                if (this.m_HPV1618SolidTumorTestOrder.Indication == Business.Test.HPV1618SolidTumor.HPV1618SolidTumorIndication.SquamousCellCarcinomaAnalRegion)
                {
                    YellowstonePathology.Business.Test.HPV1618SolidTumor.HPV1618SolidTumorAnalRegionResult result = new Business.Test.HPV1618SolidTumor.HPV1618SolidTumorAnalRegionResult();
                    result.SetResult("Negative", "Positive");
                    result.SetResult(this.m_HPV1618SolidTumorTestOrder);
                }
                else if (this.m_HPV1618SolidTumorTestOrder.Indication == Business.Test.HPV1618SolidTumor.HPV1618SolidTumorIndication.SquamousCellCarcinomaHeadAndNeck)
                {
                    YellowstonePathology.Business.Test.HPV1618SolidTumor.HPV16NegativeHPV18PositiveResult result = new Business.Test.HPV1618SolidTumor.HPV16NegativeHPV18PositiveResult();
                    result.SetResult(this.m_HPV1618SolidTumorTestOrder);
                }                
			}
			else
			{
				MessageBox.Show(methodResult.Message);
			}
        }

        private void HyperLinkBothPositive_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = YellowstonePathology.Business.Test.HPV1618SolidTumor.HPV1618SolidTumorResult.IsOkToSetResult(this.m_HPV1618SolidTumorTestOrder);
            if (methodResult.Success == true)
            {
                if(this.m_HPV1618SolidTumorTestOrder.Indication == Business.Test.HPV1618SolidTumor.HPV1618SolidTumorIndication.SquamousCellCarcinomaAnalRegion)
                {
                    YellowstonePathology.Business.Test.HPV1618SolidTumor.HPV1618SolidTumorAnalRegionResult result = new Business.Test.HPV1618SolidTumor.HPV1618SolidTumorAnalRegionResult();
                    result.SetResult("Positive", "Positive");
                    result.SetResult(this.m_HPV1618SolidTumorTestOrder);
                }
                else if(this.m_HPV1618SolidTumorTestOrder.Indication == Business.Test.HPV1618SolidTumor.HPV1618SolidTumorIndication.SquamousCellCarcinomaHeadAndNeck)
                {
                    YellowstonePathology.Business.Test.HPV1618SolidTumor.HPV1618BothPositiveResult result = new Business.Test.HPV1618SolidTumor.HPV1618BothPositiveResult();
                    result.SetResult(this.m_HPV1618SolidTumorTestOrder);
                }				
			}
			else
			{
				MessageBox.Show(methodResult.Message);
			}
        }

        private void HyperLinkIndeterminate_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = YellowstonePathology.Business.Test.HPV1618SolidTumor.HPV1618SolidTumorResult.IsOkToSetResult(this.m_HPV1618SolidTumorTestOrder);
            if (methodResult.Success == true)
            {
				YellowstonePathology.Business.Test.HPV1618SolidTumor.HPV1618IndeterminateResult result = new Business.Test.HPV1618SolidTumor.HPV1618IndeterminateResult();
				result.SetResult(this.m_HPV1618SolidTumorTestOrder);
			}
			else
			{
				MessageBox.Show(methodResult.Message);
			}
		}

        private void HyperLinkProvider_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Not Implemented");
            YellowstonePathology.UI.Login.FinalizeAccession.ProviderDistributionPath providerDistributionPath = new Login.FinalizeAccession.ProviderDistributionPath(this.m_HPV1618SolidTumorTestOrder.ReportNo, this.m_AccessionOrder, System.Windows.Visibility.Visible, System.Windows.Visibility.Collapsed, System.Windows.Visibility.Visible);
            providerDistributionPath.Back += new Login.FinalizeAccession.ProviderDistributionPath.BackEventHandler(ProviderDistributionPath_Back);
            providerDistributionPath.Next += new Login.FinalizeAccession.ProviderDistributionPath.NextEventHandler(ProviderDistributionPath_Next);
            providerDistributionPath.Start();
        }

        private void ProviderDistributionPath_Next(object sender, EventArgs e)
        {
            this.m_PageNavigator.Navigate(this);
        }

        private void ProviderDistributionPath_Back(object sender, EventArgs e)
        {
            this.m_PageNavigator.Navigate(this);
        }        
	}
}
