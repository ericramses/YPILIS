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

namespace YellowstonePathology.UI.Login.ReceiveSpecimen
{
	/// <summary>
	/// Interaction logic for DocumentMatchingWizardPage.xaml
	/// </summary>
	public partial class DocumentMatchingWizardPage : PageFunction<PageFunctionResult>
	{
		private YellowstonePathology.Business.Document.CaseDocumentCollection m_CaseDocumentCollection;
		//private YellowstonePathology.Business.Client.Model.Client m_Client;
		//private YellowstonePathology.Business.ClientOrder.Model.ClientOrderMediaCollection m_ClientOrderMediaCollection;
		private YellowstonePathology.Business.ClientOrder.Model.ClientOrderMedia m_ClientOrderMedia;
		private string m_ClientOrderId;
        private string m_StepText = "Step #4 - Match/Enter Requisition";

		public DocumentMatchingWizardPage(YellowstonePathology.Business.Document.CaseDocumentCollection caseDocumentCollection, YellowstonePathology.Business.ClientOrder.Model.ClientOrderMedia clientOrderMedia, string clientOrderId)
		{
			this.m_CaseDocumentCollection = caseDocumentCollection;
			//this.m_Client = client;
			//this.m_ClientOrderMediaCollection = clientOrderMediaCollection;
			this.m_ClientOrderMedia = clientOrderMedia;
			this.m_ClientOrderId = clientOrderId;

			InitializeComponent();

			Loaded += new RoutedEventHandler(DocumentMatchingWizardPage_Loaded);
		}

		void DocumentMatchingWizardPage_Loaded(object sender, RoutedEventArgs e)
		{
			DocumentScanPage documentScanPage = new DocumentScanPage(this.m_StepText);
			documentScanPage.KeepAlive = true;
			documentScanPage.Return += new ReturnEventHandler<PageFunctionResult>(DocumentScanPage_Return);
			this.NavigationService.Navigate(documentScanPage);
		}

		private void ReceiveCaseDocument(string documentId)
		{
			bool found = false;
			foreach (YellowstonePathology.Business.Document.CaseDocument caseDocument in this.m_CaseDocumentCollection)
			{
				if (caseDocument.DocumentId == documentId)
				{
					caseDocument.Received = true;
					found = true;
					break;
				}
			}
			if (!found)
			{
				YellowstonePathology.Business.Document.CaseDocument caseDocument = new Business.Document.CaseDocument();
				caseDocument.DocumentId = documentId;
				caseDocument.ClientOrderId = this.m_ClientOrderId;
				caseDocument.Received = true;
				this.m_CaseDocumentCollection.Add(caseDocument);
			}
		}

		private void DocumentScanPage_Return(object sender, ReturnEventArgs<PageFunctionResult> e)
		{
			if (e.Result.PageNavigationDirectionEnum == YellowstonePathology.UI.Navigation.PageNavigationDirectionEnum.Back)
			{
				PageFunctionResult result = new PageFunctionResult(false, YellowstonePathology.UI.Navigation.PageNavigationDirectionEnum.Back, null);
				OnReturn(new ReturnEventArgs<PageFunctionResult>(result));
			}
			else
			{
				if (e.Result.Success == true)
				{
					this.ReceiveCaseDocument(e.Result.Data.ToString());
					PageFunctionResult result = new PageFunctionResult(true, YellowstonePathology.UI.Navigation.PageNavigationDirectionEnum.Next, null);
					OnReturn(new ReturnEventArgs<PageFunctionResult>(result));
				}
			}
		}
	}
}
