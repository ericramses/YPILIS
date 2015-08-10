using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using System.Xml;
using System.Printing;
using System.IO;

namespace YellowstonePathology.YpiConnect.Client
{
    public class CaseDocument
    {
        private XpsDocument m_XpsDocument;
        private string m_ReportNo;
        private YellowstonePathology.YpiConnect.Contract.CaseDocumentTypeEnum m_CaseDocumentType;
        private string m_FilePath;
        private YellowstonePathology.YpiConnect.Proxy.FileTransferServiceProxy m_FileTransferServiceProxy;
        private bool m_IsDownloaded;

        public CaseDocument(string reportNo, YellowstonePathology.YpiConnect.Contract.CaseDocumentTypeEnum caseDocumentType)
        {
            this.m_ReportNo = reportNo;
            this.m_CaseDocumentType = caseDocumentType;
			this.m_FileTransferServiceProxy = new YpiConnect.Proxy.FileTransferServiceProxy();
            this.m_IsDownloaded = false;           
		}        

        public YellowstonePathology.YpiConnect.Contract.MethodResult Download()
        {            
            YellowstonePathology.YpiConnect.Contract.RemoteFile remoteFile = new Contract.RemoteFile(this.m_ReportNo, this.m_CaseDocumentType);
			YellowstonePathology.YpiConnect.Contract.MethodResult methodResult = this.m_FileTransferServiceProxy.Download(ref remoteFile, YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount);

            if (methodResult.Success == true)
            {
                this.m_XpsDocument = XpsDocumentHelper.FromMemoryStream(remoteFile.MemoryStream);
                this.m_IsDownloaded = true;
            }            

            return methodResult;
        }

        public XpsDocument XpsDocument
        {
            get { return this.m_XpsDocument; }
        }

        public YellowstonePathology.YpiConnect.Contract.MethodResult Print()
        {
            YellowstonePathology.YpiConnect.Contract.MethodResult methodResult = new Contract.MethodResult();
            YellowstonePathology.YpiConnect.Contract.MethodResult downloadResult = null;

            if (this.m_IsDownloaded == true)
            {
                downloadResult = new Contract.MethodResult();
                downloadResult.Success = true;                
            }
            else
            {
                downloadResult = this.Download();
            }

            if (downloadResult.Success == true)
            {
                LocalPrintServer ps = new LocalPrintServer();
                PrintQueue pq = ps.DefaultPrintQueue;
                XpsDocumentWriter xpsdw = PrintQueue.CreateXpsDocumentWriter(pq);
                xpsdw.Write(this.m_XpsDocument.GetFixedDocumentSequence().DocumentPaginator);
                methodResult.Success = true;
            }
            else
            {
                methodResult.Success = false;
                methodResult.Message = downloadResult.Message;
            }

            return methodResult;
        }

		public YellowstonePathology.YpiConnect.Contract.MethodResult Save()
        {
			YellowstonePathology.YpiConnect.Contract.MethodResult result = new Contract.MethodResult();
			result.Success = false;
			if (this.m_IsDownloaded == false)
			{
				YellowstonePathology.YpiConnect.Contract.RemoteFile remoteFile = new Contract.RemoteFile(this.m_ReportNo, this.m_CaseDocumentType);
				YellowstonePathology.YpiConnect.Contract.MethodResult methodResult = this.m_FileTransferServiceProxy.Download(ref remoteFile, YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount);

				if (methodResult.Success == true)
				{
					YellowstonePathology.YpiConnect.Contract.LocalFile localFile = new Contract.LocalFile(remoteFile, YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount);
					localFile.Save();
					result.Success = methodResult.Success;
				}
			}
			return result;
        }
	}
}
