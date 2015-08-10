using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MongoMigrationTests
{
	[TestClass]
	public class AccessionOrderGatewayTest
	{
		[TestMethod]
		public void GetAccessionOrderByMasterAccessionNoTest()
		{
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Gateway.AccessionOrderGatewayMongo.GetAccessionOrderByMasterAccessionNo("14-191");
			Assert.AreEqual(accessionOrder.MasterAccessionNo, "14-191");
		}

		[TestMethod]
		public void GetAccessionOrderByReportNoTest()
		{
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Gateway.AccessionOrderGatewayMongo.GetAccessionOrderByReportNo("14-24.S");
			YellowstonePathology.Business.Test.PanelSetOrder surgicalTestOrder = accessionOrder.PanelSetOrderCollection.GetPanelSetOrder("14-24.S");
			Assert.AreEqual(surgicalTestOrder.PanelSetId, 13);
			YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = accessionOrder.PanelSetOrderCollection.GetPanelSetOrder("14-24.F1");
			Assert.AreEqual(panelSetOrder.PanelSetId, 66);
		}

		[TestMethod]
		public void GetAccessionOrderViewTest()
		{
			YellowstonePathology.Business.Test.AccessionOrderView accessionOrderView = YellowstonePathology.Business.Gateway.AccessionOrderGatewayMongo.GetAccessionOrderView("14-191");
			Assert.AreEqual(accessionOrderView.MasterAccessionNo, "14-191");
		}

		[TestMethod]
		public void GetHostCollectionTest()
		{
			YellowstonePathology.Business.Facility.Model.HostCollection result = YellowstonePathology.Business.Gateway.AccessionOrderGatewayMongo.GetHostCollection();
			Assert.IsTrue(result.Count > 0);
		}

		[TestMethod]
		public void GetTypingShortcutCollectionByUserTest()
		{
			YellowstonePathology.Business.Typing.TypingShortcutCollection result = YellowstonePathology.Business.Gateway.AccessionOrderGatewayMongo.GetTypingShortcutCollectionByUser(5013);
			Assert.IsTrue(result.Count > 0);
		}

		[TestMethod]
		public void GetNeogenomicsResultCollectionTest()
		{
			YellowstonePathology.Business.NeogenomicsResultCollection result = YellowstonePathology.Business.Gateway.AccessionOrderGatewayMongo.GetNeogenomicsResultCollection();
			Assert.IsTrue(result.Count > 0);
		}

		[TestMethod]
		public void GetCaseToScheduleTest()
		{
			YellowstonePathology.Business.Test.PanelSetOrderView panelSetOrderView = YellowstonePathology.Business.Gateway.AccessionOrderGatewayMongo.GetCaseToSchedule("14-218.S");
			Assert.AreEqual(panelSetOrderView.ReportNo, "14-218.S");
		}

		/*[TestMethod]
		public void GetCasesToScheduleTest()
		{
			List<YellowstonePathology.Business.Test.PanelSetOrderView> panelSetOrderViews = YellowstonePathology.Business.Gateway.AccessionOrderGatewayMongo.GetCasesToSchedule();
			Assert.AreEqual(panelSetOrderViews.Count, 1);
		}

		[TestMethod]
		public void GetNextCasesToPublishTest()
		{
			List<YellowstonePathology.Business.Test.PanelSetOrderView> panelSetOrderViews = YellowstonePathology.Business.Gateway.AccessionOrderGatewayMongo.GetNextCasesToPublish();
			Assert.AreEqual(panelSetOrderViews.Count, 2);
		}*/

	}
}
