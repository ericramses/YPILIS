using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Windows;
using System.IO;

namespace YellowstonePathology.YpiConnect.Service
{
	public class FlowSignoutService : YellowstonePathology.YpiConnect.Contract.Flow.IFlowSignoutService
    {
        public bool Ping()
        {
            return true;
        }

		public YellowstonePathology.YpiConnect.Contract.Flow.FlowAccessionCollection GetFlowAccessionCollection(string masterAccessionNo)
		{
			FlowAccessionGateway gateway = new FlowAccessionGateway();
			return gateway.GetFlowAccession(masterAccessionNo);
		}

		public YellowstonePathology.YpiConnect.Contract.Flow.FlowCommentCollection GetFlowComments()
		{
			FlowAccessionGateway gateway = new FlowAccessionGateway();
			return gateway.GetFlowComments();
		}

		public YellowstonePathology.YpiConnect.Contract.Flow.MarkerCollection GetMarkers(string reportNo)
		{
			FlowAccessionGateway gateway = new FlowAccessionGateway();
			return gateway.GetMarkers(reportNo);
		}

		public YellowstonePathology.Business.Rules.MethodResult SubmitChanges(YellowstonePathology.YpiConnect.Contract.Flow.FlowAccessionSubmitter flowAccessionSubmitter)
		{
			Business.Rules.MethodResult methodResult = new Business.Rules.MethodResult();
			try
			{
				FlowAccessionGateway gateway = new FlowAccessionGateway();
				flowAccessionSubmitter.Submit(gateway);
				methodResult.Success = true;
				methodResult.Message = "Submit Changes completed successfully";
			}
			catch (Exception e)
			{
				methodResult.Success = false;
				methodResult.Message = e.Message;
			}
			return methodResult;
		}

		public XElement GetLeukemiaLymphomaReportDocument(string masterAccessionNo)
		{
			FlowAccessionGateway gateway = new FlowAccessionGateway();
            XElement reportDocument = gateway.GetReportDocument(masterAccessionNo);
            return reportDocument;
		}
	}
}
