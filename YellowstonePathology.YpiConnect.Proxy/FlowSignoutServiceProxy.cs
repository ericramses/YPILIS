using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.YpiConnect.Proxy
{
	public class FlowSignoutServiceProxy
	{
        //public const string EndpointAddressUrl = "https://10.1.2.187/YpiConnect/WebService/Version/Internal/FlowSignoutService.svc";
        public const string EndpointAddressUrl = "https://www.YellowstonePathology.com/YpiConnect/WebService/Version/3.0.1.0/FlowSignoutService.svc";
        //public const string EndpointAddressUrl = "https://www.YellowstonePathology.com/YpiConnect/Testing/Services/FlowSignoutService.svc";

        BasicHttpBinding m_BasicHttpBinding;
        EndpointAddress m_EndpointAddress;   

        YellowstonePathology.YpiConnect.Contract.Flow.IFlowSignoutService m_FlowSignoutChannel;
        ChannelFactory<YellowstonePathology.YpiConnect.Contract.Flow.IFlowSignoutService> m_ChannelFactory;

		public FlowSignoutServiceProxy()
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

            this.m_ChannelFactory = new ChannelFactory<Contract.Flow.IFlowSignoutService>(this.m_BasicHttpBinding, this.m_EndpointAddress);
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

			this.m_FlowSignoutChannel = this.m_ChannelFactory.CreateChannel();            
        }

        public bool Ping()
        {
			return this.m_FlowSignoutChannel.Ping();
        }

		public YellowstonePathology.YpiConnect.Contract.Flow.FlowAccessionCollection GetFlowAccessionCollection(string masterAccessionNo)
        {
			YellowstonePathology.YpiConnect.Contract.Flow.FlowAccessionCollection flowAccessionCollection = this.m_FlowSignoutChannel.GetFlowAccessionCollection(masterAccessionNo);
			return flowAccessionCollection;
        }

		public YellowstonePathology.YpiConnect.Contract.Flow.FlowCommentCollection GetFlowComments()
		{
			YellowstonePathology.YpiConnect.Contract.Flow.FlowCommentCollection flowCommentCollection = this.m_FlowSignoutChannel.GetFlowComments();
			return flowCommentCollection;
		}

		public YellowstonePathology.YpiConnect.Contract.Flow.MarkerCollection GetMarkers(string reportNo)
		{
			YellowstonePathology.YpiConnect.Contract.Flow.MarkerCollection markerCollection = this.m_FlowSignoutChannel.GetMarkers(reportNo);
			return markerCollection;
		}

		public YellowstonePathology.Business.Rules.MethodResult SubmitChanges(YellowstonePathology.YpiConnect.Contract.Flow.FlowAccessionSubmitter flowAccessionSubmitter)
		{
			YellowstonePathology.Business.Rules.MethodResult methodResult = this.m_FlowSignoutChannel.SubmitChanges(flowAccessionSubmitter);
			return methodResult;
		}

		public XElement GetLeukemiaLymphomaReportDocument(string masterAccessionNo)
        {
            return this.m_FlowSignoutChannel.GetLeukemiaLymphomaReportDocument(masterAccessionNo);
        }        
	}
}
