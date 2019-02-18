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
using System.Xml.Linq;

namespace YellowstonePathology.UI.Test
{	
	public partial class ROS1ResultPage : ResultControl, INotifyPropertyChanged 
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void NextEventHandler(object sender, EventArgs e);
		public event NextEventHandler Next;

        public delegate void CPTCodeEventHandler(object sender, EventArgs e);
        public event CPTCodeEventHandler CPTCode;

        private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private string m_PageHeaderText;

        private YellowstonePathology.Business.Test.ROS1ByFISH.ROS1ByFISHTestOrder m_PanelSetOrder;
        private string m_OrderedOnDescription;

        public ROS1ResultPage(YellowstonePathology.Business.Test.ROS1ByFISH.ROS1ByFISHTestOrder ros1ByFISHTestOrder,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity) : base(ros1ByFISHTestOrder, accessionOrder)
		{
            this.m_PanelSetOrder = ros1ByFISHTestOrder;
			this.m_AccessionOrder = accessionOrder;			
			this.m_SystemIdentity = systemIdentity;

            this.m_PageHeaderText = "ROS1 Results For: " + this.m_AccessionOrder.PatientDisplayName;

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderByOrderTarget(this.m_PanelSetOrder.OrderedOnId);
            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetAliquotOrder(this.m_PanelSetOrder.OrderedOnId);
            this.m_OrderedOnDescription = specimenOrder.Description;
            if(aliquotOrder != null) this.m_OrderedOnDescription += ": " + aliquotOrder.Label;

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

        public YellowstonePathology.Business.Test.ROS1ByFISH.ROS1ByFISHTestOrder PanelSetOrder
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
            YellowstonePathology.Business.Test.ROS1ByFISH.ROS1ByFISHWordDocument report = new Business.Test.ROS1ByFISH.ROS1ByFISHWordDocument(this.m_AccessionOrder, this.m_PanelSetOrder, Business.Document.ReportSaveModeEnum.Draft);
            report.Render();
            YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWordViewer(report.SaveFileName);         
		}

        private void HyperLinkFinalizeResults_Click(object sender, RoutedEventArgs e)
        {
            bool okToFinal = false;
            YellowstonePathology.Business.Audit.Model.AuditResult auditResult = this.m_PanelSetOrder.IsOkToFinalize(this.m_AccessionOrder);
            if (auditResult.Status == Business.Audit.Model.AuditStatusEnum.OK)
            {
                okToFinal = true;
            }
            else if (auditResult.Status == Business.Audit.Model.AuditStatusEnum.Warning)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show(auditResult.Message, "Results do not match the finaled summary results",
                    MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    okToFinal = true;
                }
            }
            else
            {
                MessageBox.Show(auditResult.Message);
            }

            if (okToFinal == true)
            {
                YellowstonePathology.Business.Test.FinalizeTestResult finalizeTestResult = this.m_PanelSetOrder.Finish(this.m_AccessionOrder);
                this.HandleFinalizeTestResult(finalizeTestResult);
                if (this.m_PanelSetOrder.Accepted == false)
                {
                    this.m_PanelSetOrder.Accept();
                }
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
                MessageBox.Show("This case cannot be unfinaled because it is not final.");
            }             
		}

		private void HyperLinkAcceptResults_Click(object sender, RoutedEventArgs e)
		{
            YellowstonePathology.Business.Audit.Model.AuditResult result = this.m_PanelSetOrder.IsOkToAccept(this.m_AccessionOrder);
            if (result.Status == Business.Audit.Model.AuditStatusEnum.OK)
            {
                this.m_PanelSetOrder.Accept();
            }
            else if (result.Status == Business.Audit.Model.AuditStatusEnum.Warning)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show(result.Message, "Results do not match the finaled summary results",
                    MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    this.m_PanelSetOrder.Accept();
                }
            }
            else
            {
                MessageBox.Show(result.Message);
            }
        }

        private void HyperLinkUnacceptResults_Click(object sender, RoutedEventArgs e)
		{            			
			if (this.m_PanelSetOrder.Accepted == true)
			{
				this.m_PanelSetOrder.Unaccept();
			}
			else
			{
				MessageBox.Show("This case cannot be unaccepted because it is not accepted.");
			}            
		}

        private void HyperLinkCloseWindow_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
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
