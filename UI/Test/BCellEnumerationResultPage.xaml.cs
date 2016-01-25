/*
 * Created by SharpDevelop.
 * User: william.copland
 * Date: 1/6/2016
 * Time: 9:12 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.ComponentModel;

namespace YellowstonePathology.UI.Test
{
	/// <summary>
	/// Interaction logic for BCellEnumerationResultPage.xaml
	/// </summary>
	public partial class BCellEnumerationResultPage : UserControl, INotifyPropertyChanged, Business.Interface.IPersistPageChanges
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void NextEventHandler(object sender, EventArgs e);
		public event NextEventHandler Next;

		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.Test.BCellEnumeration.BCellEnumerationTestOrder m_PanelSetOrder;
		private string m_PageHeaderText;

		public BCellEnumerationResultPage(YellowstonePathology.Business.Test.BCellEnumeration.BCellEnumerationTestOrder testOrder,
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity)
		{
			this.m_PanelSetOrder = testOrder;
			this.m_AccessionOrder = accessionOrder;
			this.m_SystemIdentity = systemIdentity;

			this.m_PageHeaderText = "B-Cell Enumeration Results For: " + this.m_AccessionOrder.PatientDisplayName;
			InitializeComponent();

			DataContext = this;

            Loaded += BCellEnumerationResultPage_Loaded;
            Unloaded += BCellEnumerationResultPage_Unloaded;
		}

        private void BCellEnumerationResultPage_Loaded(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Persistence.ObjectTrackerV2.Instance.RegisterObject(this.m_AccessionOrder, this);
        }

        private void BCellEnumerationResultPage_Unloaded(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Persistence.ObjectTrackerV2.Instance.CleanUp(this);
        }

        public YellowstonePathology.Business.Test.BCellEnumeration.BCellEnumerationTestOrder PanelSetOrder
		{
			get { return this.m_PanelSetOrder; }
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
            YellowstonePathology.Business.Persistence.ObjectTrackerV2.Instance.SubmitChanges(this.m_AccessionOrder, this);
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

		private void HyperLinkShowDocument_Click(object sender, RoutedEventArgs e)
		{
			this.Save();
			//if (string.IsNullOrEmpty(this.m_PanelSetOrder.ResultCode) == false)
			//{
				YellowstonePathology.Business.Test.BCellEnumeration.BCellEnumerationWordDocument report = new YellowstonePathology.Business.Test.BCellEnumeration.BCellEnumerationWordDocument();
				report.Render(this.m_PanelSetOrder.MasterAccessionNo, this.m_PanelSetOrder.ReportNo, Business.Document.ReportSaveModeEnum.Draft);

				YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_PanelSetOrder.ReportNo);
				string fileName = YellowstonePathology.Business.Document.CaseDocument.GetDraftDocumentFilePath(orderIdParser);
				YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWordViewer(fileName);
			//}
			//else
			//{
			//	MessageBox.Show("The result must be set before the report can be viewed.");
			//}
		}
	}
}