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
	/// Interaction logic for BCRABLByFishResultPage.xaml
	/// </summary>
	public partial class BCRABLByFishResultPage : ResultControl, INotifyPropertyChanged 
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void NextEventHandler(object sender, EventArgs e);
		public event NextEventHandler Next;

        public delegate void CPTCodeEventHandler(object sender, EventArgs e);
        public event CPTCodeEventHandler CPTCode;

        private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.Test.BCRABLByFish.BCRABLByFishTestOrder m_PanelSetOrder;
		private YellowstonePathology.Business.Test.BCRABLByFish.BCRABLByFishResultCollection m_ResultCollection;

		private string m_PageHeaderText;
		private string m_OrderedOnDescription;

		public BCRABLByFishResultPage(YellowstonePathology.Business.Test.BCRABLByFish.BCRABLByFishTestOrder testOrder,
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity) : base(testOrder, accessionOrder)
		{
            this.m_PanelSetOrder = testOrder;
			this.m_AccessionOrder = accessionOrder;
			this.m_SystemIdentity = systemIdentity;

			this.m_PageHeaderText = "BCR/ABL Fish Analysis Result For: " + this.m_AccessionOrder.PatientDisplayName;

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(this.m_PanelSetOrder.OrderedOn, this.m_PanelSetOrder.OrderedOnId);
			this.m_OrderedOnDescription = specimenOrder.Description;
            this.m_ResultCollection = new YellowstonePathology.Business.Test.BCRABLByFish.BCRABLByFishResultCollection();

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

		public YellowstonePathology.Business.Test.BCRABLByFish.BCRABLByFishTestOrder PanelSetOrder
		{
            get { return this.m_PanelSetOrder; }
		}

		public YellowstonePathology.Business.Test.BCRABLByFish.BCRABLByFishResultCollection ResultCollection
		{
			get { return this.m_ResultCollection; }
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
			YellowstonePathology.Business.Test.BCRABLByFish.BCRABLByFishWordDocument report = new YellowstonePathology.Business.Test.BCRABLByFish.BCRABLByFishWordDocument(this.m_AccessionOrder, this.m_PanelSetOrder, Business.Document.ReportSaveModeEnum.Draft);
            report.Render();
			YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWord(report.SaveFileName);
		}

        private void HyperLinkFinalizeResults_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Audit.Model.AuditResult auditResult = this.m_PanelSetOrder.IsOkToFinalize(this.m_AccessionOrder);
            if (auditResult.Status == Business.Audit.Model.AuditStatusEnum.OK)
            {
                YellowstonePathology.Business.Test.FinalizeTestResult finalizeTestResult = this.m_PanelSetOrder.Finish(this.m_AccessionOrder);
                this.HandleFinalizeTestResult(finalizeTestResult);
                if (this.m_PanelSetOrder.Accepted == false)
                {
                    this.m_PanelSetOrder.Accept();
                }
            }
            else
            {
                MessageBox.Show(auditResult.Message);
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
            YellowstonePathology.Business.Audit.Model.AuditResult result = this.m_PanelSetOrder.IsOkToAccept(this.m_AccessionOrder);
            if (result.Status == Business.Audit.Model.AuditStatusEnum.OK)
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

        private void HyperLinkCPTCodes_Click(object sender, RoutedEventArgs e)
        {
            this.CPTCode(this, new EventArgs());
        }

        private void HyperLinkProbeComment_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Helper.FISHProbeComment fishProbeComment = new Business.Helper.FISHProbeComment(this.m_PanelSetOrder.PanelSetOrderCPTCodeCollection);
            if (fishProbeComment.Success == true)
            {
                this.m_PanelSetOrder.ProbeComment = fishProbeComment.Comment;
            }
            else
            {
                MessageBox.Show(fishProbeComment.Message);
            }
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
			if (this.Next != null) this.Next(this, new EventArgs());
		}

        private void HyperLinkPreviousResults_Click(object sender, RoutedEventArgs e)
        {
            UI.Test.PreviousResultDialog dlg = new UI.Test.PreviousResultDialog(this.m_PanelSetOrder, this.m_AccessionOrder);
            dlg.ShowDialog();
        }
    }
}
