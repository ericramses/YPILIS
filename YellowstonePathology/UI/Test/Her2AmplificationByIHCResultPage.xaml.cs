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
	/// Interaction logic for Her2AmplificationByIHCResultPage.xaml
	/// </summary>
	public partial class Her2AmplificationByIHCResultPage : ResultControl, INotifyPropertyChanged 
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void NextEventHandler(object sender, EventArgs e);
		public event NextEventHandler Next;

        public delegate void OrderHER2RecountEventHandler(object sender, EventArgs e);
        public event OrderHER2RecountEventHandler OrderHER2Recount;

        private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private string m_PageHeaderText;

		private YellowstonePathology.Business.Test.Her2AmplificationByIHC.PanelSetOrderHer2AmplificationByIHC m_PanelSetOrder;
		private string m_OrderedOnDescription;

		public Her2AmplificationByIHCResultPage(YellowstonePathology.Business.Test.Her2AmplificationByIHC.PanelSetOrderHer2AmplificationByIHC panelSetOrderHer2AmplificationByIHC,
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity) : base(panelSetOrderHer2AmplificationByIHC, accessionOrder)
		{
			this.m_PanelSetOrder = panelSetOrderHer2AmplificationByIHC;
			this.m_AccessionOrder = accessionOrder;
			this.m_SystemIdentity = systemIdentity;

			this.m_PageHeaderText = "Her2 Amplification By IHC Result For: " + this.m_AccessionOrder.PatientDisplayName;

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.m_PanelSetOrder.OrderedOn, this.m_PanelSetOrder.OrderedOnId);
			this.m_OrderedOnDescription = specimenOrder.Description;

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

		public YellowstonePathology.Business.Test.Her2AmplificationByIHC.PanelSetOrderHer2AmplificationByIHC PanelSetOrder
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

		private void HyperLinkShowDocument_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Test.Her2AmplificationByIHC.Her2AmplificationByIHCWordDocument report = new Business.Test.Her2AmplificationByIHC.Her2AmplificationByIHCWordDocument(this.m_AccessionOrder, this.m_PanelSetOrder, Business.Document.ReportSaveModeEnum.Draft);
			report.Render();

			YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_PanelSetOrder.ReportNo);
			string fileName = YellowstonePathology.Business.Document.CaseDocument.GetDraftDocumentFilePath(orderIdParser);
			YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWordViewer(fileName);
		}

		private void HyperLinkFinalizeResults_Click(object sender, RoutedEventArgs e)
		{
			if (this.m_PanelSetOrder.Final == false)
			{
                YellowstonePathology.Business.Test.FinalizeTestResult finalizeTestResult = this.m_PanelSetOrder.Finish(this.m_AccessionOrder);
                this.HandleFinalizeTestResult(finalizeTestResult);
            }
            else
			{
				MessageBox.Show("This case cannot be finalized because it is already final.");
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

		private void HyperLinkAcceptResults_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Rules.MethodResult result = this.m_PanelSetOrder.IsOkToAccept();
            if (result.Success == true)
            {
                this.m_PanelSetOrder.Accept();
                YellowstonePathology.Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTest ishTest = new Business.Test.HER2AmplificationByISH.HER2AmplificationByISHTest();
                if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(ishTest.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true) == true)
                {
                    Business.Test.HER2AmplificationByISH.HER2AmplificationResultCollection her2ResultCollection = new Business.Test.HER2AmplificationByISH.HER2AmplificationResultCollection(this.m_AccessionOrder.PanelSetOrderCollection);
                    Business.Test.HER2AmplificationByISH.HER2AmplificationResult her2Result = her2ResultCollection.FindMatch();
                    if (her2Result.IsRecountNeeded() == true)
                    {
                        Business.Test.HER2AmplificationRecount.HER2AmplificationRecountTest her2AmplificationRecountTest = new Business.Test.HER2AmplificationRecount.HER2AmplificationRecountTest();
                        if(this.m_AccessionOrder.PanelSetOrderCollection.Exists(her2AmplificationRecountTest.PanelSetId, this.m_PanelSetOrder.OrderedOnId, true) == false)
                        {
                            MessageBoxResult messageBoxResult = MessageBox.Show("A HER2 Recount is required.  If one is not ordered this test will not be accepted.  Do you want to order it now?", "Recount required", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
                            if(messageBoxResult == MessageBoxResult.Yes)
                            {
                                this.OrderHER2Recount(this, new EventArgs());
                            }
                            else
                            {
                                this.m_PanelSetOrder.Unaccept();
                            }
                        }
                    }
                }
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

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
			if (this.Next != null) this.Next(this, new EventArgs());
		}
	}
}
