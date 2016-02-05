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
using System.Windows.Shapes;
using System.IO;

namespace YellowstonePathology.UI.ReportOrder
{
	/// <summary>
	/// Interaction logic for PanelSetOrderDetails.xaml
	/// </summary>
	public partial class ReportOrderDetailPage : Window
	{
        private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.User.SystemUserCollection m_UserCollection;
		private YellowstonePathology.Business.Facility.Model.FacilityCollection m_FacilityCollection;
        private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        private string m_ReportDocumentPath;        

        public ReportOrderDetailPage(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, string reportNo, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
		{			
            this.m_AccessionOrder = accessionOrder;
            this.m_PanelSetOrder = accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
            this.m_SystemIdentity = systemIdentity;
			this.m_UserCollection = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection;
            this.m_FacilityCollection = Business.Facility.Model.FacilityCollection.GetAllFacilities();            

			YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_PanelSetOrder.ReportNo);
			this.m_ReportDocumentPath = YellowstonePathology.Business.Document.CaseDocument.GetCaseFileNamePDF(orderIdParser);

			InitializeComponent();

			DataContext = this;         
            this.Closing += new System.ComponentModel.CancelEventHandler(ReportOrderDetailPage_Closing);
		}        

        private void ReportOrderDetailPage_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
			this.Save(false);
        }

		private void Save(bool releaseLock)
		{
            YellowstonePathology.Business.Persistence.ObjectGateway.Instance.SubmitChanges(this.m_AccessionOrder, releaseLock);			
		}

        public string ReportDocumentPath
        {
            get { return this.m_ReportDocumentPath; }
            set { this.m_ReportDocumentPath = value; }
        }

        public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
        }

		public YellowstonePathology.Business.Test.PanelSetOrder PanelSetOrder
		{
			get { return this.m_PanelSetOrder; }
		}

		public YellowstonePathology.Business.User.SystemUserCollection UserCollection
		{
			get { return this.m_UserCollection; }
		}

		public YellowstonePathology.Business.Facility.Model.FacilityCollection FacilityCollection
		{
			get { return this.m_FacilityCollection; }
		}

		private void ButtonOK_Click(object sender, RoutedEventArgs e)
		{
            
			this.Close();
		}

        private void ButtonCopyReportDocumentPath_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Clipboard.SetData(DataFormats.Text, this.TextBoxReportDocumentPath.Text);
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

		private void HyperLinkShowDocument_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Interface.ICaseDocument caseDocument = YellowstonePathology.Business.Document.DocumentFactory.GetDocument(this.m_PanelSetOrder.PanelSetId);
			caseDocument.Render(this.m_PanelSetOrder.MasterAccessionNo, this.m_PanelSetOrder.ReportNo, Business.Document.ReportSaveModeEnum.Draft);

			YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_PanelSetOrder.ReportNo);
			string fileName = YellowstonePathology.Business.Document.CaseDocument.GetDraftDocumentFilePath(orderIdParser);
			YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWordViewer(fileName);
		}        

		private void HyperLinkFinalize_Click(object sender, RoutedEventArgs e)
        {
            if (this.IsOKToFinal() == true)
            {
				YellowstonePathology.Business.Rules.MethodResult result = this.m_PanelSetOrder.IsOkToFinalize();
				if (result.Success == true)
                {                    
					this.m_PanelSetOrder.Finalize(this.m_SystemIdentity.User);
                }
				else
				{
					MessageBox.Show(result.Message);
				}
			}
        }

        private bool IsOKToFinal()
        {
            bool result = true;
            YellowstonePathology.Business.PanelSet.Model.PanelSetCollection panelSetCollection = YellowstonePathology.Business.PanelSet.Model.PanelSetCollection.GetAll();
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet = panelSetCollection.GetPanelSet(this.m_PanelSetOrder.PanelSetId);

            if (panelSet != null && panelSet.ResultDocumentSource == Business.PanelSet.Model.ResultDocumentSourceEnum.YPIDatabase)
            {            
                result = false;
                MessageBox.Show("This report cannot be finalized here.");            
            }

            if (this.HasCaseBeenPublished() == false && panelSet.ResultDocumentSource == Business.PanelSet.Model.ResultDocumentSourceEnum.PublishedDocument)
            {
                result = false;
                MessageBox.Show("This report cannot be finalized until it has been published.");
            }            

            return result;
        }

		private void HyperLinkPublish_Click(object sender, RoutedEventArgs e)
		{
            if (this.DoesXPSDocumentExist() == true)
            {
                YellowstonePathology.Business.Interface.ICaseDocument caseDocument = YellowstonePathology.Business.Document.DocumentFactory.GetDocument(this.m_PanelSetOrder.PanelSetId);
                caseDocument.Render(this.m_PanelSetOrder.MasterAccessionNo, this.m_PanelSetOrder.ReportNo, YellowstonePathology.Business.Document.ReportSaveModeEnum.Normal);
                caseDocument.Publish();
                MessageBox.Show("The case was successfully published.");
            }
            else
            {
                MessageBox.Show("Cannot publish this case until the XPS document is present.");
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

		private void HyperLinkUnfinalize_Click(object sender, RoutedEventArgs e)
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

        private bool DoesXPSDocumentExist()
        {
            bool result = true;
			YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_PanelSetOrder.ReportNo);
            string xpsFileName = YellowstonePathology.Business.Document.CaseDocument.GetCaseFileNameXPS(orderIdParser);
            if (System.IO.File.Exists(xpsFileName) == false)
            {                
                result = false;
            }
            return result;
        }

        private bool HasCaseBeenPublished()
        {
            bool result = true;
			YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(this.m_PanelSetOrder.ReportNo);
            string tifFileName = YellowstonePathology.Business.Document.CaseDocument.GetCaseFileNameTif(orderIdParser);
            if (System.IO.File.Exists(tifFileName) == false)
            {
                result = false;
            }
            return result;
        }
	}
}
