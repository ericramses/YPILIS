using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Xml;

namespace YellowstonePathology.YpiConnect.Proxy
{
    public class FileTransferServiceProxy 
    {        
        //public const string EndpointAddressUrl = "https://www.YellowstonePathology.com/YpiConnect/WebService/Version/3.1.0.0/FileTransferService.svc";
        public const string EndpointAddressUrl = "https://www.YellowstonePathology.com/YpiConnect/Testing/Services/FileTransferService.svc";

        BasicHttpBinding m_BasicHttpBinding;
        EndpointAddress m_EndpointAddress;

		YellowstonePathology.YpiConnect.Contract.IFileTransferService m_FileTransferServiceChannel;
		ChannelFactory<YellowstonePathology.YpiConnect.Contract.IFileTransferService> m_ChannelFactory;

		public FileTransferServiceProxy()
        {
            this.m_BasicHttpBinding = new BasicHttpBinding();
            this.m_EndpointAddress = new EndpointAddress(EndpointAddressUrl);

            this.m_BasicHttpBinding.Security.Mode = BasicHttpSecurityMode.TransportWithMessageCredential;
            this.m_BasicHttpBinding.Security.Message.ClientCredentialType = BasicHttpMessageCredentialType.UserName;
            this.m_BasicHttpBinding.MaxReceivedMessageSize = 2147483647;

            XmlDictionaryReaderQuotas readerQuotas = new XmlDictionaryReaderQuotas();
            readerQuotas.MaxArrayLength = 25 * 208000;
            readerQuotas.MaxStringContentLength = 25 * 208000;
            this.m_BasicHttpBinding.ReaderQuotas = readerQuotas;

			this.m_ChannelFactory = new ChannelFactory<Contract.IFileTransferService>(this.m_BasicHttpBinding, this.m_EndpointAddress);
            this.m_ChannelFactory.Credentials.UserName.UserName = YellowstonePathology.YpiConnect.Contract.Identity.GuestWebServiceAccount.UserName;
            this.m_ChannelFactory.Credentials.UserName.Password = YellowstonePathology.YpiConnect.Contract.Identity.GuestWebServiceAccount.Password;

            foreach (System.ServiceModel.Description.OperationDescription op in this.m_ChannelFactory.Endpoint.Contract.Operations)
            {
                var dataContractBehavior = op.Behaviors.Find<System.ServiceModel.Description.DataContractSerializerOperationBehavior>();
                if (dataContractBehavior != null)
                {
                    dataContractBehavior.MaxItemsInObjectGraph = int.MaxValue;
                }
            }

			this.m_FileTransferServiceChannel = this.m_ChannelFactory.CreateChannel();       
        }

        public bool Ping()
        {
            return this.m_FileTransferServiceChannel.Ping();
        }

        public string GetSummaryResultString(string reportNo)
        {
            return this.m_FileTransferServiceChannel.GetSummaryResultString(reportNo);
        }

		public YellowstonePathology.YpiConnect.Contract.MethodResult Download(ref YellowstonePathology.YpiConnect.Contract.RemoteFile remoteFile, YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount webServiceAccount)
        {
			return this.m_FileTransferServiceChannel.Download(ref remoteFile, webServiceAccount);
        }

		public YellowstonePathology.YpiConnect.Contract.MethodResult Upload(ref YellowstonePathology.YpiConnect.Contract.LocalFile localFile, YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount webServiceAccount)
        {
			return this.m_FileTransferServiceChannel.Upload(ref localFile, webServiceAccount);
        }

		public YellowstonePathology.YpiConnect.Contract.MethodResult GetRemoteFileList(ref YellowstonePathology.YpiConnect.Contract.RemoteFileList remoteFileList, YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount webServiceAccount)
		{
			return this.m_FileTransferServiceChannel.GetRemoteFileList(ref remoteFileList, webServiceAccount);
		}
    }
}
