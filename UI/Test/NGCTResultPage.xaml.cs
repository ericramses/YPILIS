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
	/// Interaction logic for NGCTResultPage.xaml
	/// </summary>
	public partial class NGCTResultPage : UserControl, INotifyPropertyChanged, Business.Interface.IPersistPageChanges
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void NextEventHandler(object sender, EventArgs e);
		public event NextEventHandler Next;

		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;
		private YellowstonePathology.Business.Test.NGCT.NGCTTestOrder m_PanelSetOrder;
		private YellowstonePathology.Business.Test.NGCT.NGCTResultCollection m_NGResultCollection;
		private YellowstonePathology.Business.Test.NGCT.NGCTResultCollection m_CTResultCollection;
		private string m_PageHeaderText;


		public NGCTResultPage(YellowstonePathology.Business.Test.NGCT.NGCTTestOrder testOrder,
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.Persistence.ObjectTracker objectTracker,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity)
		{
			this.m_PanelSetOrder = testOrder;
			this.m_AccessionOrder = accessionOrder;
			this.m_SystemIdentity = systemIdentity;
			this.m_ObjectTracker = objectTracker;

			this.m_PageHeaderText = "NG CT Results For: " + this.m_AccessionOrder.PatientDisplayName;
			this.m_NGResultCollection = YellowstonePathology.Business.Test.NGCT.NGCTResultCollection.GetNGResultCollection();
			this.m_CTResultCollection = YellowstonePathology.Business.Test.NGCT.NGCTResultCollection.GetCTResultCollection();

			InitializeComponent();

			DataContext = this;

		}

		public YellowstonePathology.Business.Test.NGCT.NGCTTestOrder PanelSetOrder
		{
			get { return this.m_PanelSetOrder; }
		}

		public YellowstonePathology.Business.Test.NGCT.NGCTResultCollection NGResultCollection
		{
			get { return this.m_NGResultCollection; }
		}

		public YellowstonePathology.Business.Test.NGCT.NGCTResultCollection CTResultCollection
		{
			get { return this.m_CTResultCollection; }
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

		public bool OkToSaveOnNavigation(Type pageNavigatingTo)
		{
			return true;
		}

		public bool OkToSaveOnClose()
		{
			return true;
		}

		public void Save()
		{
			this.m_ObjectTracker.SubmitChanges(this.m_AccessionOrder);
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
                YellowstonePathology.Business.ReportDistribution.Model.MultiTestDistributionHandler multiTestDistributionHandler = YellowstonePathology.Business.ReportDistribution.Model.MultiTestDistributionHandlerFactory.GetHandler(this.m_AccessionOrder);
                multiTestDistributionHandler.Set();                

				this.m_PanelSetOrder.Finalize(this.m_SystemIdentity.User);

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
				YellowstonePathology.Business.Test.NGCT.NGCTResult ngctResult = new YellowstonePathology.Business.Test.NGCT.NGCTResult();
				ngctResult.AcceptResults(this.m_PanelSetOrder, this.m_SystemIdentity.User);
				this.NotifyPropertyChanged("");
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
			YellowstonePathology.Business.Rules.MethodResult methodResult = this.m_PanelSetOrder.IsOkToSetResults();
			if (methodResult.Success == true)
			{
				YellowstonePathology.Business.Test.NGCT.NGCTResult ngctResult = new YellowstonePathology.Business.Test.NGCT.NGCTResult();
				ngctResult.SetResults(this.m_PanelSetOrder);
			}
			else
			{
				MessageBox.Show(methodResult.Message);
			}
		}

		private void HyperLinkShowDocument_Click(object sender, RoutedEventArgs e)
		{
			this.Save();
			YellowstonePathology.Business.Test.NGCT.NGCTWordDocument report = new YellowstonePathology.Business.Test.NGCT.NGCTWordDocument();
			report.Render(this.m_PanelSetOrder.MasterAccessionNo, this.m_PanelSetOrder.ReportNo, Business.Document.ReportSaveModeEnum.Draft);

			YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_PanelSetOrder.ReportNo);
			string fileName = YellowstonePathology.Business.Document.CaseDocument.GetDraftDocumentFilePath(orderIdParser);
			YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWordViewer(fileName);
		}

		private void HyperOrderRetest_Click(object sender, RoutedEventArgs e)
		{
			this.m_PanelSetOrder.OrderRetest(this.m_SystemIdentity.User);
		}

		private void HyperLinkAcceptPanel_Click(object sender, RoutedEventArgs e)
		{
			Hyperlink hyperlink = (Hyperlink)sender;
			YellowstonePathology.Business.Test.PanelOrder panelOrder = (YellowstonePathology.Business.Test.PanelOrder)hyperlink.Tag;
			panelOrder.AcceptResults(this.m_SystemIdentity.User);
			panelOrder.NotifyPropertyChanged("AcceptedBy");
		}

		private void HyperLinkUnacceptPanel_Click(object sender, RoutedEventArgs e)
		{
			Hyperlink hyperlink = (Hyperlink)sender;
			YellowstonePathology.Business.Test.PanelOrder panelOrder = (YellowstonePathology.Business.Test.PanelOrder)hyperlink.Tag;
			panelOrder.UnacceptResults();
			panelOrder.NotifyPropertyChanged("AcceptedBy");
		}

		private void HyperLinkDeletePanel_Click(object sender, RoutedEventArgs e)
		{
			Hyperlink hyperlink = (Hyperlink)sender;
			YellowstonePathology.Business.Test.PanelOrder panelOrder = (YellowstonePathology.Business.Test.PanelOrder)hyperlink.Tag;
			if (panelOrder.PanelId == 6)
			{
				this.m_PanelSetOrder.PanelOrderCollection.Remove(panelOrder);
			}
			else
			{
				MessageBox.Show("The initial panel may not be deleted.");
			}
		}

		private void ComboBoxNGResult_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (this.ComboBoxNGResult.SelectedItem != null)
			{
				YellowstonePathology.Business.Test.TestResult testResult = (YellowstonePathology.Business.Test.TestResult)this.ComboBoxNGResult.SelectedItem;
				this.m_PanelSetOrder.NGResultCode = testResult.ResultCode;
			}
		}

		private void ComboBoxCTResult_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (this.ComboBoxCTResult.SelectedItem != null)
			{
				YellowstonePathology.Business.Test.TestResult testResult = (YellowstonePathology.Business.Test.TestResult)this.ComboBoxCTResult.SelectedItem;
				this.m_PanelSetOrder.CTResultCode = testResult.ResultCode;
			}
		}
	}
}
