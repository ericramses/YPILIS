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
	public partial class ALKForNSCLCByFISHResultPage : UserControl, INotifyPropertyChanged, Business.Interface.IPersistPageChanges, IResultPage
	{
		public event PropertyChangedEventHandler PropertyChanged;

        public event YellowstonePathology.UI.CustomEventArgs.EventHandlerDefinitions.CancelTestEventHandler CancelTest;   

		public delegate void NextEventHandler(object sender, EventArgs e);
		public event NextEventHandler Next;

		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;
        private string m_PageHeaderText;

        private YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHResultCollection m_ResultCollection;
        private YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTestOrder m_PanelSetOrder;
        private string m_OrderedOnDescription;

        public ALKForNSCLCByFISHResultPage(YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTestOrder alkForNSCLCByFISHTestOrder,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.Persistence.ObjectTracker objectTracker,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity)
		{
            this.m_PanelSetOrder = alkForNSCLCByFISHTestOrder;
			this.m_AccessionOrder = accessionOrder;			
			this.m_SystemIdentity = systemIdentity;
			this.m_ObjectTracker = objectTracker;

            this.m_ResultCollection = new Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHResultCollection();
            this.m_PageHeaderText = "ALK Results For: " + this.m_AccessionOrder.PatientDisplayName;

			YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderByOrderTarget(this.m_PanelSetOrder.OrderedOnId);
            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetAliquotOrder(this.m_PanelSetOrder.OrderedOnId);
            this.m_OrderedOnDescription = specimenOrder.Description;
            if(aliquotOrder != null) this.m_OrderedOnDescription += ": " + aliquotOrder.Label;

			InitializeComponent();

			DataContext = this;				
		}

        public string OrderedOnDescription
        {
            get { return this.m_OrderedOnDescription; }
        }

        public YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTestOrder PanelSetOrder
        {
            get { return this.m_PanelSetOrder; }
        }

        public YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHResultCollection ResultCollection
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

        private void HyperLinkClearResult_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_PanelSetOrder.Accepted == false)
            {
                YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHResult result = new Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHResult();
                result.Clear(this.m_PanelSetOrder);
            }
            else
            {
                MessageBox.Show("You cannot set the result because the test has been accepted.");
            }
        }           

        private void HyperLinkSetResults_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_PanelSetOrder.Accepted == false)
            {
                if (this.ComboBoxResult.SelectedItem != null)
                {
                    YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHResult result = (YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHResult)this.ComboBoxResult.SelectedItem;
                    result.SetResult(this.m_PanelSetOrder);
                }
            }
            else
            {
                MessageBox.Show("You cannot set the result because the test has been accepted.");
            }
        }    

		private void HyperLinkShowDocument_Click(object sender, RoutedEventArgs e)
		{
            this.Save();
            YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHWordDocument report = new Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHWordDocument();
            report.Render(this.m_AccessionOrder.MasterAccessionNo, this.m_PanelSetOrder.ReportNo, Business.Document.ReportSaveModeEnum.Draft);

			YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_PanelSetOrder.ReportNo);
            string fileName = YellowstonePathology.Business.Document.CaseDocument.GetDraftDocumentFilePath(orderIdParser);
            YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWordViewer(fileName);
		}

        private void HyperLinkFinalizeResults_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_PanelSetOrder.Final == false)
            {
                this.m_PanelSetOrder.Finalize(this.m_SystemIdentity.User);
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
				this.m_PanelSetOrder.Accept(this.m_SystemIdentity.User);
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

        private void HyperLinkCancelInsufficient_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_PanelSetOrder.Accepted == false)
            {
                this.CancelTest(this, new CustomEventArgs.CancelTestEventArgs(this.m_PanelSetOrder, this.m_AccessionOrder, "Insufficient tissue to perform test.", this, this.m_ObjectTracker));
            }
            else
            {
                MessageBox.Show("This test cannot be canceled because it has been accepted.");
            }
        }         

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {       
            if (this.Next != null) this.Next(this, new EventArgs());
        }

        private void ComboBoxResult_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }                        
	}
}
