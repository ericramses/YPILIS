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
	/// Interaction logic for CysticFibrosisResultPage.xaml
	/// </summary>
	public partial class CysticFibrosisResultPage : UserControl, INotifyPropertyChanged, Business.Interface.IPersistPageChanges
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void NextEventHandler(object sender, EventArgs e);
		public event NextEventHandler Next;

		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.UI.Navigation.PageNavigator m_PageNavigator;
		private YellowstonePathology.Business.Test.CysticFibrosis.CysticFibrosisTestOrder m_PanelSetOrder;
		private YellowstonePathology.Business.Test.CysticFibrosis.CysticFibrosisResultCollection m_ResultCollection;
		private YellowstonePathology.Business.Test.CysticFibrosis.CysticFibrosisEthnicGroupCollection m_EthnicGroupCollection;
		private YellowstonePathology.Business.Test.CysticFibrosis.CysticFibrosisGeneNames m_CysticFibrosisGeneNames;
		private YellowstonePathology.Business.Test.CysticFibrosis.CysticFibrosisTemplateCollection m_CysticFibrosisTemplateCollection;
		private string m_PageHeaderText;

		public CysticFibrosisResultPage(YellowstonePathology.Business.Test.CysticFibrosis.CysticFibrosisTestOrder panelSetOrder,
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity,
			YellowstonePathology.UI.Navigation.PageNavigator pageNavigator)
		{
			this.m_PanelSetOrder = panelSetOrder;
			this.m_AccessionOrder = accessionOrder;
			this.m_SystemIdentity = systemIdentity;
			this.m_PageNavigator = pageNavigator;

			this.m_PageHeaderText = "Cystic Fibrosis Results For: " + this.m_AccessionOrder.PatientDisplayName;
			this.m_ResultCollection = YellowstonePathology.Business.Test.CysticFibrosis.CysticFibrosisResultCollection.GetAllResults();
            this.m_EthnicGroupCollection = new YellowstonePathology.Business.Test.CysticFibrosis.CysticFibrosisEthnicGroupCollection();
            this.m_CysticFibrosisGeneNames = new YellowstonePathology.Business.Test.CysticFibrosis.CysticFibrosisGeneNames();
            this.m_CysticFibrosisTemplateCollection = new YellowstonePathology.Business.Test.CysticFibrosis.CysticFibrosisTemplateCollection();

			InitializeComponent();

			DataContext = this;

            Loaded += CysticFibrosisResultPage_Loaded;
            Unloaded += CysticFibrosisResultPage_Unloaded;                      

		}

        private void CysticFibrosisResultPage_Loaded(object sender, RoutedEventArgs e)
        {
             
        }

        private void CysticFibrosisResultPage_Unloaded(object sender, RoutedEventArgs e)
        {
             
        }

        public YellowstonePathology.Business.Test.CysticFibrosis.CysticFibrosisTestOrder PanelSetOrder
		{
			get { return this.m_PanelSetOrder; }
		}

		public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
		{
			get { return this.m_AccessionOrder; }
		}

		public YellowstonePathology.Business.Test.CysticFibrosis.CysticFibrosisResultCollection ResultCollection
		{
			get { return this.m_ResultCollection; }
		}

		public YellowstonePathology.Business.Test.CysticFibrosis.CysticFibrosisEthnicGroupCollection EthnicGroupCollection
		{
			get { return this.m_EthnicGroupCollection; }
		}

		public YellowstonePathology.Business.Test.CysticFibrosis.CysticFibrosisGeneNames CysticFibrosisGeneNames
		{
			get { return this.m_CysticFibrosisGeneNames; }
		}

		public YellowstonePathology.Business.Test.CysticFibrosis.CysticFibrosisTemplateCollection CysticFibrosisTemplateCollection
		{
			get { return this.m_CysticFibrosisTemplateCollection; }
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
            YellowstonePathology.Business.Persistence.ObjectGatway.Instance.SubmitChanges(this.m_AccessionOrder, false);
        }

        public void UpdateBindingSources()
		{

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
				this.m_PanelSetOrder.Finalize(this.m_SystemIdentity.User);
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

		private void HyperLinkAcceptResults_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Rules.MethodResult methodResult = this.m_PanelSetOrder.IsOkToAccept();
			if (methodResult.Success == true)
			{
                this.m_PanelSetOrder.Accept(this.m_SystemIdentity.User);
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

		private void HyperLinkShowDocument_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Test.CysticFibrosis.CysticFibrosisTemplate cysticFibrosisTemplate  = (YellowstonePathology.Business.Test.CysticFibrosis.CysticFibrosisTemplate)this.ComboBoxTemplate.SelectedItem;
			if (cysticFibrosisTemplate.TemplateId > 0)
			{
				this.Save(false);
				YellowstonePathology.Business.Test.CysticFibrosis.CysticFibrosisWordDocument report = new YellowstonePathology.Business.Test.CysticFibrosis.CysticFibrosisWordDocument();
				report.Render(this.m_PanelSetOrder.MasterAccessionNo, this.m_PanelSetOrder.ReportNo, Business.Document.ReportSaveModeEnum.Draft);

				YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_PanelSetOrder.ReportNo);
				string fileName = YellowstonePathology.Business.Document.CaseDocument.GetDraftDocumentFilePath(orderIdParser);
				YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWordViewer(fileName);
			}
			else
			{
				MessageBox.Show("A Report Template must be selected before you may view the report.");
			}
		}

		private void HyperLinkSetResults_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Rules.MethodResult methodResult = this.m_PanelSetOrder.IsOkToSetResults();
			if (methodResult.Success == true)
			{
                YellowstonePathology.Business.Test.CysticFibrosis.CysticFibrosisEthnicGroup cysticFibrosisEthnicGroup = new YellowstonePathology.Business.Test.CysticFibrosis.CysticFibrosisEthnicGroupUnknown();
				if (this.ComboBoxEthnicGroup.SelectedItem != null)
				{
					cysticFibrosisEthnicGroup = (YellowstonePathology.Business.Test.CysticFibrosis.CysticFibrosisEthnicGroup)this.ComboBoxEthnicGroup.SelectedItem;

				}
				YellowstonePathology.Business.Test.CysticFibrosis.CysticFibrosisResult cysticFibrosisResult = (YellowstonePathology.Business.Test.CysticFibrosis.CysticFibrosisResult)this.m_ResultCollection.GetResult(this.m_PanelSetOrder);
				cysticFibrosisResult.SetResults(this.m_PanelSetOrder, cysticFibrosisEthnicGroup);				
			}
			else
			{
				MessageBox.Show(methodResult.Message);
			}
		}

		private void ComboBoxResult_SelectionChanged(object sender, RoutedEventArgs e)
		{
			if (this.ComboBoxResult.SelectedItem != null)
			{
				YellowstonePathology.Business.Test.TestResult testResult = (YellowstonePathology.Business.Test.TestResult)this.ComboBoxResult.SelectedItem;

				this.m_PanelSetOrder.ResultCode = testResult.ResultCode;
			}
		}
	}
}
