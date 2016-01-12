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
	public partial class EGFRToALKReflexPage : UserControl, YellowstonePathology.Business.Interface.IPersistPageChanges, INotifyPropertyChanged
	{
		public delegate void PropertyChangedNotificationHandler(String info);
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void FinishEventHandler(object sender, EventArgs e);
		public event FinishEventHandler Finish;

        public delegate void BackEventHandler(object sender, EventArgs e);
        public event BackEventHandler Back;        

        public delegate void OrderALKROS1AndPDL1EventHandler(object sender, EventArgs e);
        public event OrderALKROS1AndPDL1EventHandler OrderALKROS1AndPDL1;

		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;
		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;

        private YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisTestOrder m_EGFRToALKReflexAnalysisTestOrder;
        private YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTestOrder m_ALKForNSCLCByFISHTestOrder;
        private YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTestOrder m_EGFRMutationAnalysisTestOrder;
        private YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisResult m_EGFRToALKReflexAnalysisResult;
        private string m_PageHeaderText;
        private string m_OrderedOnDescription;
        private System.Windows.Visibility m_BackButtonVisibility;

        public EGFRToALKReflexPage(YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisTestOrder testOrder,
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.Persistence.ObjectTracker objectTracker,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity,
            System.Windows.Visibility backButtonVisibility)
		{
			this.m_AccessionOrder = accessionOrder;
			this.m_ObjectTracker = objectTracker;			
			this.m_SystemIdentity = systemIdentity;
            this.m_BackButtonVisibility = backButtonVisibility;

			this.m_EGFRToALKReflexAnalysisTestOrder = testOrder;
            this.m_EGFRToALKReflexAnalysisTestOrder.SetStatus(this.m_AccessionOrder.PanelSetOrderCollection);


            this.m_EGFRMutationAnalysisTestOrder = (YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(60);
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(131) == true)
            {
                this.m_ALKForNSCLCByFISHTestOrder = (YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(131);
                
            }
            else if(this.m_AccessionOrder.PanelSetOrderCollection.Exists(68) == true)
            {
                this.m_ALKForNSCLCByFISHTestOrder = new YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTestOrder();
            }

            this.m_PageHeaderText = this.m_EGFRToALKReflexAnalysisTestOrder.PanelSetName + " for: " + this.m_AccessionOrder.PatientDisplayName;

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderByOrderTarget(this.m_EGFRToALKReflexAnalysisTestOrder.OrderedOnId);
            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetAliquotOrder(this.m_EGFRToALKReflexAnalysisTestOrder.OrderedOnId);
            this.m_OrderedOnDescription = specimenOrder.Description;
            if (aliquotOrder != null) this.m_OrderedOnDescription += ": " + aliquotOrder.Label;

            this.m_EGFRToALKReflexAnalysisResult = new Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisResult(this.m_AccessionOrder, this.m_EGFRToALKReflexAnalysisTestOrder);

			InitializeComponent();

			this.DataContext = this;
		}

        public YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisResult EGFRToALKReflexAnalysisResult
        {
            get { return this.m_EGFRToALKReflexAnalysisResult; }
        }

        public System.Windows.Visibility BackButtonVisibility
        {
            get { return this.m_BackButtonVisibility; }
        }

        public string OrderedOnDescription
        {
            get { return this.m_OrderedOnDescription; }
        }

        public YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisTestOrder EGFRToALKReflexAnalysisTestOrder
		{
			get { return this.m_EGFRToALKReflexAnalysisTestOrder; }
		}

        public YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTestOrder ALKForNSCLCByFISHTestOrder
        {
            get { return this.m_ALKForNSCLCByFISHTestOrder; }
        }

        public YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTestOrder EGFRMutationAnalysisTestOrder
        {
            get { return this.m_EGFRMutationAnalysisTestOrder; }
        }

		public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
		{
			get { return this.m_AccessionOrder; }
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

        private void HyperLinkOrderALKROS1AndPDL1_Click(object sender, RoutedEventArgs e)
        {
            this.OrderALKROS1AndPDL1(this, new EventArgs());
        }  

		private void HyperLinkSetResults_Click(object sender, RoutedEventArgs e)
		{
            this.m_EGFRToALKReflexAnalysisTestOrder.SetResults(this.m_AccessionOrder);
		}

		private void HyperLinkShowDocument_Click(object sender, RoutedEventArgs e)
		{
			this.Save();
            YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisWordDocument report = new Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisWordDocument();
			report.Render(this.m_AccessionOrder.MasterAccessionNo, this.m_EGFRToALKReflexAnalysisTestOrder.ReportNo, Business.Document.ReportSaveModeEnum.Draft);

			YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_EGFRToALKReflexAnalysisTestOrder.ReportNo);
			string fileName = YellowstonePathology.Business.Document.CaseDocument.GetDraftDocumentFilePath(orderIdParser);
			YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWordViewer(fileName);
		}

		private void HyperLinkFinalizeResults_Click(object sender, RoutedEventArgs e)
		{
            if (string.IsNullOrEmpty(this.m_EGFRToALKReflexAnalysisTestOrder.TumorNucleiPercentage) == true)
            {
                MessageBox.Show("The results cannot be finalized because the Tumor Nuclei Percentage has no value.");
            }
            else
            {
                if (this.m_EGFRToALKReflexAnalysisTestOrder.Final == false)
                {
                    this.m_EGFRToALKReflexAnalysisTestOrder.Finalize(this.m_SystemIdentity.User);
                }
            }			
		}

		private void HyperLinkUnfinalResults_Click(object sender, RoutedEventArgs e)
		{
			if (this.m_EGFRToALKReflexAnalysisTestOrder.Final == true)
			{
				this.m_EGFRToALKReflexAnalysisTestOrder.Unfinalize();				
			}
		}

		private void HyperLinkAcceptResults_Click(object sender, RoutedEventArgs e)
		{			
			YellowstonePathology.Business.Rules.MethodResult result = this.m_EGFRToALKReflexAnalysisTestOrder.IsOkToAccept();
			if (result.Success == true)
			{
				this.m_EGFRToALKReflexAnalysisTestOrder.Accept(this.m_SystemIdentity.User);
			}
			else
			{
				MessageBox.Show(result.Message);
			}		
		}

		private void HyperLinkUnacceptResults_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Rules.MethodResult result = this.m_EGFRToALKReflexAnalysisTestOrder.IsOkToUnaccept();
			if (result.Success == true)
			{
				this.m_EGFRToALKReflexAnalysisTestOrder.Unaccept();
			}
			else
			{
				MessageBox.Show(result.Message);
			}
		}

        private void ButtonFinish_Click(object sender, RoutedEventArgs e)
        {
            if (this.Finish != null) this.Finish(this, new EventArgs());   
        }        

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            if (this.Back != null) this.Back(this, new EventArgs());   
        }

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

        private void CheckBoxQNSForALK_Checked(object sender, RoutedEventArgs e)
        {
            this.m_EGFRToALKReflexAnalysisResult = new Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisResult(this.m_AccessionOrder, this.m_EGFRToALKReflexAnalysisTestOrder);
            this.NotifyPropertyChanged(string.Empty);
        }

        private void CheckBoxQNSForALK_Unchecked(object sender, RoutedEventArgs e)
        {
            this.m_EGFRToALKReflexAnalysisResult = new Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisResult(this.m_AccessionOrder, this.m_EGFRToALKReflexAnalysisTestOrder);
            this.NotifyPropertyChanged(string.Empty);
        }

        private void CheckBoxQNSForROS1_Checked(object sender, RoutedEventArgs e)
        {
            this.m_EGFRToALKReflexAnalysisResult = new Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisResult(this.m_AccessionOrder, this.m_EGFRToALKReflexAnalysisTestOrder);
            this.NotifyPropertyChanged(string.Empty);
        }

        private void CheckBoxQNSForROS1_Unchecked(object sender, RoutedEventArgs e)
        {
            this.m_EGFRToALKReflexAnalysisResult = new Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisResult(this.m_AccessionOrder, this.m_EGFRToALKReflexAnalysisTestOrder);
            this.NotifyPropertyChanged(string.Empty);
        }                    

        private void CheckBoxQNSForPDL1_Checked(object sender, RoutedEventArgs e)
        {
            this.m_EGFRToALKReflexAnalysisResult = new Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisResult(this.m_AccessionOrder, this.m_EGFRToALKReflexAnalysisTestOrder);
            this.NotifyPropertyChanged(string.Empty);
        }

        private void CheckBoxQNSForPDL1_Unchecked(object sender, RoutedEventArgs e)
        {
            this.m_EGFRToALKReflexAnalysisResult = new Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisResult(this.m_AccessionOrder, this.m_EGFRToALKReflexAnalysisTestOrder);
            this.NotifyPropertyChanged(string.Empty);
        }                    
	}
}
