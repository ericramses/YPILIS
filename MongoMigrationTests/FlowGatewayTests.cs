using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MongoMigrationTests
{
	[TestClass]
	public class FlowGatewayTests
	{
		/*[TestMethod]
		public void GetByLeukemiaNotFinal()
		{
			YellowstonePathology.Business.Flow.FlowLogList flowLogList = YellowstonePathology.Business.Gateway.FlowGatewayMongo.GetByLeukemiaNotFinal();
			Assert.IsTrue(flowLogList.Count > 0);
		}*/

		[TestMethod]
		public void GetByTestType()
		{
			YellowstonePathology.Business.Flow.FlowLogList flowLogList = YellowstonePathology.Business.Gateway.FlowGatewayMongo.GetByTestType(21);
			Assert.IsTrue(flowLogList.Count > 0);
		}

		[TestMethod]
		public void GetFlowLogListByReportNo()
		{
			YellowstonePathology.Business.Flow.FlowLogList flowLogList = YellowstonePathology.Business.Gateway.FlowGatewayMongo.GetFlowLogListByReportNo("14-36.F1");
			Assert.IsTrue(flowLogList.Count == 1);
		}

		[TestMethod]
		public void GetByAccessionMonth()
		{
			DateTime date = DateTime.Parse("1/1/2014");
			YellowstonePathology.Business.Flow.FlowLogList flowLogList = YellowstonePathology.Business.Gateway.FlowGatewayMongo.GetByAccessionMonth(date);
			Assert.IsTrue(flowLogList.Count == 34);
		}

		[TestMethod]
		public void GetByPatientNameLastOnly()
		{
			YellowstonePathology.Business.Flow.FlowLogList flowLogList = YellowstonePathology.Business.Gateway.FlowGatewayMongo.GetByPatientName("po");
			Assert.IsTrue(flowLogList.Count > 0);
		}

		[TestMethod]
		public void GetByPatientNameLastAndFirst()
		{
			YellowstonePathology.Business.Flow.FlowLogList flowLogList = YellowstonePathology.Business.Gateway.FlowGatewayMongo.GetByPatientName("POO, ran");
			Assert.IsTrue(flowLogList.Count > 0);
		}

		/*[TestMethod]
		public void GetByPathologistId()
		{
			YellowstonePathology.Business.Flow.FlowLogList flowLogList = YellowstonePathology.Business.Gateway.FlowGatewayMongo.GetByPathologistId(5088);
			Assert.IsTrue(flowLogList.Count > 0);
		}*/

		[TestMethod]
		public void GetFlowMarkerCollectionByReportNo()
		{
			YellowstonePathology.Business.Flow.FlowMarkerCollection flowMarkerCollection = YellowstonePathology.Business.Gateway.FlowGatewayMongo.GetFlowMarkerCollectionByReportNo("14-469.F1");
			Assert.IsTrue(flowMarkerCollection.Count > 0);
		}

		[TestMethod]
		public void GetFlowMarkerCollectionByPanelId()
		{
			YellowstonePathology.Business.Flow.FlowMarkerCollection flowMarkerCollection = YellowstonePathology.Business.Gateway.FlowGatewayMongo.GetFlowMarkerCollectionByPanelId("20-20469.F1", 2);
			Assert.IsTrue(flowMarkerCollection.Count == 30);
		}
	}
}
