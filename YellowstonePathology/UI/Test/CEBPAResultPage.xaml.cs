﻿using System;
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
	/// Interaction logic for CEBPAResultPage.xaml
	/// </summary>
	public partial class CEBPAResultPage : ResultControl, INotifyPropertyChanged 
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void NextEventHandler(object sender, EventArgs e);
		public event NextEventHandler Next;

		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private string m_PageHeaderText;

		private YellowstonePathology.Business.Test.CEBPA.PanelSetOrderCEBPA m_PanelSetOrder;
		private string m_OrderedOnDescription;

		public CEBPAResultPage(YellowstonePathology.Business.Test.CEBPA.PanelSetOrderCEBPA panelSetOrderCEBPA,
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity) : base(panelSetOrderCEBPA, accessionOrder)
		{
			this.m_PanelSetOrder = panelSetOrderCEBPA;
			this.m_AccessionOrder = accessionOrder;
			this.m_SystemIdentity = systemIdentity;

			this.m_PageHeaderText = "CEBPA Mutation Analysis Result For: " + this.m_AccessionOrder.PatientDisplayName;

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

		public YellowstonePathology.Business.Test.CEBPA.PanelSetOrderCEBPA PanelSetOrder
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

		private void HyperLinkNotDetected_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Test.CEBPA.CEBPANotDetectedResult result = new Business.Test.CEBPA.CEBPANotDetectedResult();
			result.SetResults(this.m_PanelSetOrder);
			this.NotifyPropertyChanged("PanelSetOrder");
		}

		private void HyperLinkDetected_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("This result is not yet implemented.", "Not implemented yet.");			
		}

		private void HyperLinkShowDocument_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Test.CEBPA.CEBPAWordDocument report = new Business.Test.CEBPA.CEBPAWordDocument(this.m_AccessionOrder, this.m_PanelSetOrder, Business.Document.ReportSaveModeEnum.Draft);
			report.Render();
			YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWord(report.SaveFileName);
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
