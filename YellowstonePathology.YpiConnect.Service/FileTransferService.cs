using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace YellowstonePathology.YpiConnect.Service
{
	public class FileTransferService : YellowstonePathology.YpiConnect.Contract.IFileTransferService
    {
        public bool Ping()
        {
            return true;
        }

        public string GetSummaryResultString(string reportNo)
        {
            throw new Exception("This is done.");
			//YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetAccessionOrderByReportNo(reportNo);
            //return accessionOrder.ToResultString(reportNo);
        }

		public YellowstonePathology.YpiConnect.Contract.MethodResult Download(ref YellowstonePathology.YpiConnect.Contract.RemoteFile remoteFile, YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount webServiceAccount)
        {
            if (remoteFile.UseReportNoToFindFile == true)
            {
				YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(remoteFile.ReportNo);
                switch (remoteFile.CaseDocumentType)
                {
                    case Contract.CaseDocumentTypeEnum.TIF:
						remoteFile.FullPath = YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser) + remoteFile.ReportNo + ".tif";
                        break;
                    case Contract.CaseDocumentTypeEnum.XPS:
						remoteFile.FullPath = YellowstonePathology.Document.CaseDocumentPath.GetPath(orderIdParser) + remoteFile.ReportNo + ".xps";
                        break;
                }
            }

            YellowstonePathology.YpiConnect.Contract.MethodResult methodResult = remoteFile.Load();
            if (methodResult.Success == false)
            {
                string sendTo = "support@ypii.com";
                string messageText = "Error while attempting file download, file does not exist: " + remoteFile.FileName;
                YellowstonePathology.YpiConnect.Contract.Message message = new Contract.Message(sendTo, webServiceAccount);
                message.ClientText = messageText;
                MessageService messageService = new MessageService();
                messageService.Send(message);
            }
            return methodResult;
        }

		public YellowstonePathology.YpiConnect.Contract.MethodResult Upload(ref YellowstonePathology.YpiConnect.Contract.LocalFile localFile, YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount webServiceAccount)
        {
			YellowstonePathology.YpiConnect.Contract.RemoteFile remoteFile = new Contract.RemoteFile(localFile, webServiceAccount);
			YellowstonePathology.YpiConnect.Contract.MethodResult methodResult = remoteFile.Save();            
			return methodResult;
        }

		public YellowstonePathology.YpiConnect.Contract.MethodResult GetRemoteFileList(ref YellowstonePathology.YpiConnect.Contract.RemoteFileList remoteFileList, YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount webServiceAccount)
		{
			YellowstonePathology.YpiConnect.Contract.MethodResult methodResult = remoteFileList.Load(webServiceAccount.RemoteFileUploadDirectory);
			return methodResult;
		}                
    }
}
