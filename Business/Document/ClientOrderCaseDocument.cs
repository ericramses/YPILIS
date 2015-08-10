using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Document
{
	public class ClientOrderCaseDocument : CaseDocument
	{
		private string m_ClientOrderId;

        public ClientOrderCaseDocument(string clientOrderId)
		{
            this.m_ClientOrderId = clientOrderId;
			FullFileName = null;
			FileName = "Client Order Data Sheet.xps";
			FilePath = null;
			CaseDocumentType = "ClientOrderDataSheet";
		}

		public override void Show(System.Windows.Controls.ContentControl contentControl)
		{
            YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder = YellowstonePathology.Business.Gateway.ClientOrderGateway.GetClientOrderByClientOrderId(this.m_ClientOrderId);
            XElement clientOrderDocument = clientOrder.ToXML(true);
            YellowstonePathology.Document.Result.Data.ClientOrderDataSheetData data = new YellowstonePathology.Document.Result.Data.ClientOrderDataSheetData(clientOrderDocument);
            YellowstonePathology.Document.Result.Xps.ClientOrderDataSheet clientOrderDataSheet = new YellowstonePathology.Document.Result.Xps.ClientOrderDataSheet(data);

            System.Windows.Controls.DocumentViewer documentViewer = new System.Windows.Controls.DocumentViewer();
            documentViewer.Loaded += new System.Windows.RoutedEventHandler(DocumentViewer_Loaded);
            documentViewer.Document = clientOrderDataSheet.FixedDocument;
            contentControl.Content = documentViewer;            
		}

		private void DocumentViewer_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			System.Windows.Controls.DocumentViewer documentViewer = (System.Windows.Controls.DocumentViewer)sender;
			documentViewer.FitToWidth();
		}
	}
}
