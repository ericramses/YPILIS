using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.YpiConnect.Contract.Flow
{
	[ServiceContract]
	public interface IFlowSignoutService
	{
		[OperationContract]
		bool Ping();

		[OperationContract]
		FlowAccessionCollection GetFlowAccessionCollection(string masterAccessionNo);

		[OperationContract]
		FlowCommentCollection GetFlowComments();

		[OperationContract]
		MarkerCollection GetMarkers(string reportNo);

		[OperationContract]
		YellowstonePathology.Business.Rules.MethodResult SubmitChanges(FlowAccessionSubmitter flowAccessionSubmitter);

		[OperationContract]
		XElement GetLeukemiaLymphomaReportDocument(string masterAccessionNo);
	}
}
