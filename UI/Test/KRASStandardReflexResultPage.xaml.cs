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

namespace YellowstonePathology.UI.Test
{
	/// <summary>
	/// Interaction logic for KRASBRAFReflexResultPage.xaml
	/// </summary>
	public partial class KRASStandardReflexResultPage : UserControl, INotifyPropertyChanged 
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void NextEventHandler(object sender, EventArgs e);
		public event NextEventHandler Next;
		public delegate void BackEventHandler(object sender, EventArgs e);
		public event BackEventHandler Back;
		public delegate void OrderBRAFEventHandler(object sender, EventArgs e);
		public event OrderBRAFEventHandler OrderBRAF;

		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.UI.Navigation.PageNavigator m_PageNavigator;
		private string m_PageHeaderText;
		private string m_OrderedOnDescription;

        private YellowstonePathology.Business.Test.IndicationCollection m_IndicationCollection;
		private YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexResult m_KRASStandardReflexResult;
        private YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexTestOrder m_KRASStandardReflexTestOrder;
		private System.Windows.Visibility m_BackButtonVisibility;

        public KRASStandardReflexResultPage(YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexTestOrder krasStandardReflexTestOrder,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity,
			YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
			System.Windows.Visibility backButtonVisibility)
		{
            this.m_KRASStandardReflexTestOrder = krasStandardReflexTestOrder;
			this.m_AccessionOrder = accessionOrder;
			this.m_SystemIdentity = systemIdentity;
			this.m_PageNavigator = pageNavigator;
			this.m_BackButtonVisibility = backButtonVisibility;

            this.m_IndicationCollection = YellowstonePathology.Business.Test.IndicationCollection.GetAll();
            this.m_KRASStandardReflexResult = YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexResultFactory.GetResult(this.m_KRASStandardReflexTestOrder.ReportNo, this.m_AccessionOrder);

            this.m_PageHeaderText = "KRAS Standard Reflex Results For: " + this.m_AccessionOrder.PatientDisplayName + " (" + krasStandardReflexTestOrder.ReportNo + ")";
			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetByAliquotOrderId(this.m_KRASStandardReflexTestOrder.OrderedOnId);
            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetAliquotOrder(this.m_KRASStandardReflexTestOrder.OrderedOnId);
            this.m_OrderedOnDescription = specimenOrder.Description + ": " + aliquotOrder.Label;

			InitializeComponent();

			DataContext = this;

            Loaded += KRASStandardReflexResultPage_Loaded;
            Unloaded += KRASStandardReflexResultPage_Unloaded;                      
		}

        private void KRASStandardReflexResultPage_Loaded(object sender, RoutedEventArgs e)
        {
             
        }

        private void KRASStandardReflexResultPage_Unloaded(object sender, RoutedEventArgs e)
        {
             
        }

        public YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexTestOrder PanelSetOrder
        {
            get { return this.m_KRASStandardReflexResult.KRASStandardReflexTestOrder; }
        }

		public YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexResult KRASStandardReflexResult
		{
			get { return this.m_KRASStandardReflexResult; }
		}

        public YellowstonePathology.Business.Test.IndicationCollection IndicationCollection
        {
            get { return this.m_IndicationCollection; }
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

		public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}

		public string OrderedOnDescription
		{
			get { return this.m_OrderedOnDescription; }
		}

		public System.Windows.Visibility BackButtonVisibility
		{
			get { return this.m_BackButtonVisibility; }
		}

		public bool OkToSaveOnNavigation(Type pageNavigatingTo)
		{
			return true;
		}

		public bool OkToSaveOnClose()
		{
			return true;
		}

		public void Save(bool releaseLock)
		{
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.SubmitChanges(this.m_AccessionOrder, false);
        }

        public void UpdateBindingSources()
		{

		}

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
			this.Next(this, new EventArgs());
		}

		private void ButtonBack_Click(object sender, RoutedEventArgs e)
		{
			if (this.Back != null) this.Back(this, new EventArgs());
		}

		private void HyperLinkFinalize_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Rules.MethodResult methodResult = YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexResult.IsOkToFinal(this.m_KRASStandardReflexResult);
			if (methodResult.Success == true)
			{
				this.m_KRASStandardReflexResult.FinalizeResults(this.m_KRASStandardReflexResult.KRASStandardReflexTestOrder, this.m_SystemIdentity);
			}
			else
			{
				MessageBox.Show(methodResult.Message);
			}
		}

		private void HyperLinkUnfinalResults_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Rules.MethodResult methodResult = YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexResult.IsOkToUnFinalize(this.m_KRASStandardReflexResult);
			if (methodResult.Success == true)
			{
				this.m_KRASStandardReflexResult.UnFinalizeResults(this.m_KRASStandardReflexResult.KRASStandardReflexTestOrder);
			}
			else
			{
				MessageBox.Show(methodResult.Message);
			}
		}

		private void HyperLinkShowDocument_Click(object sender, RoutedEventArgs e)
		{
			this.Save(false);
			YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexWordDocument report = new YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexWordDocument();
			report.Render(this.m_KRASStandardReflexResult.KRASStandardReflexTestOrder.MasterAccessionNo, this.m_KRASStandardReflexResult.KRASStandardReflexTestOrder.ReportNo, Business.Document.ReportSaveModeEnum.Draft);

			YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_KRASStandardReflexResult.KRASStandardReflexTestOrder.ReportNo);
			string fileName = YellowstonePathology.Business.Document.CaseDocument.GetDraftDocumentFilePath(orderIdParser);
			YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWordViewer(fileName);
		}		

		private void ProviderDetailPage_Return(object sender, UI.Navigation.PageNavigationReturnEventArgs e)
		{
			this.m_PageNavigator.Navigate(this);
		}

		private void HyperLinkOrderBRAFV600EK_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Rules.MethodResult methodResult = YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexResult.IsOkToOrderBRAF(this.m_KRASStandardReflexResult);
			if (methodResult.Success == true)
			{
				this.OrderBRAF(this, new EventArgs());
			}
			else
			{
				MessageBox.Show(methodResult.Message);
			}
		}

        private void HyperLinkSetResults_Click(object sender, RoutedEventArgs e)
        {            
            YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexResult krasStandardReflexResult = YellowstonePathology.Business.Test.KRASStandardReflex.KRASStandardReflexResultFactory.GetResult(this.m_KRASStandardReflexTestOrder.ReportNo, this.m_AccessionOrder);
            krasStandardReflexResult.SetResults();
        }

		private void HyperLinkAcceptResults_Click(object sender, RoutedEventArgs e)
		{			
			YellowstonePathology.Business.Rules.MethodResult result = this.m_KRASStandardReflexResult.KRASStandardReflexTestOrder.IsOkToAccept();
			if (result.Success == true)
			{
				this.m_KRASStandardReflexResult.KRASStandardReflexTestOrder.Accept(this.m_SystemIdentity.User);
			}
			else
			{
				MessageBox.Show(result.Message);
			}			
		}

		private void HyperLinkUnacceptResults_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Rules.MethodResult result = this.m_KRASStandardReflexResult.KRASStandardReflexTestOrder.IsOkToUnaccept();
			if (result.Success == true)
			{
				this.m_KRASStandardReflexResult.KRASStandardReflexTestOrder.Unaccept();
			}
			else
			{
				MessageBox.Show(result.Message);
			}
		}
	}
}
