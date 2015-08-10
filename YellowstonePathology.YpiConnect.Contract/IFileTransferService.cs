using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ServiceModel;

namespace YellowstonePathology.YpiConnect.Contract
{
    [ServiceContract]    
    public interface IFileTransferService
    {
        [OperationContract]
        bool Ping();

        [OperationContract]
        string GetSummaryResultString(string reportNo);        

        [OperationContract]
		YellowstonePathology.YpiConnect.Contract.MethodResult Download(ref RemoteFile remoteFile, Identity.WebServiceAccount webServiceAccount);        

        [OperationContract]
		YellowstonePathology.YpiConnect.Contract.MethodResult Upload(ref LocalFile localFile, Identity.WebServiceAccount webServiceAccount);
        
		[OperationContract]
		YellowstonePathology.YpiConnect.Contract.MethodResult GetRemoteFileList(ref YellowstonePathology.YpiConnect.Contract.RemoteFileList remoteFileList, Identity.WebServiceAccount webServiceAccount);        
	}
}
