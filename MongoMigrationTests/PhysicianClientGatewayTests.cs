using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MongoMigrationTests
{
    [TestClass]
    public class PhysicianClientGatewayTests
    {
        [TestMethod]
        public void GetClientGroupCollectionTest()
        {
            YellowstonePathology.Business.Client.ClientGroupCollection clientGroupCollection = YellowstonePathology.Business.Gateway.PhysicianClientGatewayMongo.GetClientGroupCollection();
            Assert.IsTrue(clientGroupCollection.Count > 0);
        }

        [TestMethod]
        public void GetClientByClientIdTest()
        {
            YellowstonePathology.Business.Client.Model.Client client = YellowstonePathology.Business.Gateway.PhysicianClientGatewayMongo.GetClientByClientId(558);
            Assert.AreEqual(client.ClientId, 558);
        }

        [TestMethod]
        public void GetAllClientsTest()
        {
            YellowstonePathology.Business.Client.Model.ClientCollection clientCollection = YellowstonePathology.Business.Gateway.PhysicianClientGatewayMongo.GetAllClients();
            Assert.IsTrue(clientCollection.Count > 0);
        }

		[TestMethod]
		public void GetPhysicianByPhysicianIdTest()
		{
			YellowstonePathology.Business.Domain.Physician physician = YellowstonePathology.Business.Gateway.PhysicianClientGatewayMongo.GetPhysicianByPhysicianId(46);
			Assert.AreEqual(physician.PhysicianId, 46);
		}
		
		[TestMethod]
		public void GetPhysicianByNpiTest()
		{
			YellowstonePathology.Business.Domain.Physician physician = YellowstonePathology.Business.Gateway.PhysicianClientGatewayMongo.GetPhysicianByNpi("1972544617");
			Assert.AreEqual(physician.Npi, "1972544617");
		}

		[TestMethod]
		public void GetPhysicianClientTest()
		{
			YellowstonePathology.Business.Domain.PhysicianClient physicianClient = YellowstonePathology.Business.Gateway.PhysicianClientGatewayMongo.GetPhysicianClient(310, 251);
			Assert.AreEqual(physicianClient.PhysicianId, 310);
			Assert.AreEqual(physicianClient.ClientId, 251);
		}

		[TestMethod]
		public void GetClientsByClientNameTest()
		{
			YellowstonePathology.Business.Client.Model.ClientCollection clientCollection = YellowstonePathology.Business.Gateway.PhysicianClientGatewayMongo.GetClientsByClientName("yellow");
			Assert.IsTrue(clientCollection.Count >= 13);
		}

        [TestMethod]
        public void GetPhysiciansByClientIdTest()
        {
            YellowstonePathology.Business.Domain.PhysicianCollection physicianCollection = YellowstonePathology.Business.Gateway.PhysicianClientGatewayMongo.GetPhysiciansByClientId(558);
            Assert.IsTrue(physicianCollection.Count > 0);
        }

        [TestMethod]
        public void GetClientsByPhysicianIdIdTest()
        {
            YellowstonePathology.Business.Domain.ClientCollection clientCollection = YellowstonePathology.Business.Gateway.PhysicianClientGatewayMongo.GetClientsByPhysicianId(9);
            Assert.IsTrue(clientCollection.Count > 0);
        }

		[TestMethod]
		public void GetClientSearchViewByClientNameTest()
		{
			YellowstonePathology.Business.View.ClientSearchView result = YellowstonePathology.Business.Gateway.PhysicianClientGatewayMongo.GetClientSearchViewByClientName("yellow");
			Assert.IsTrue(result.Count > 0);
		}

		[TestMethod]
		public void GetPhysiciansByNameLastNameOnlyTest()
		{
			YellowstonePathology.Business.Domain.PhysicianCollection result = YellowstonePathology.Business.Gateway.PhysicianClientGatewayMongo.GetPhysiciansByName(string.Empty, "brow");
			Assert.IsTrue(result.Count > 0);
		}

		[TestMethod]
		public void GetPhysiciansByNameBothNamesTest()
		{
			YellowstonePathology.Business.Domain.PhysicianCollection result = YellowstonePathology.Business.Gateway.PhysicianClientGatewayMongo.GetPhysiciansByName("m", "brow");
			Assert.IsTrue(result.Count > 0);
		}

		[TestMethod]
		public void GetClientPhysicianViewByClientIdTest()
		{
			YellowstonePathology.Business.View.ClientPhysicianView result = YellowstonePathology.Business.Gateway.PhysicianClientGatewayMongo.GetClientPhysicianViewByClientId(280);
			Assert.IsTrue(result.ClientId == 280);
			Assert.IsTrue(result.Physicians.Count > 0);
		}

        [TestMethod]
		public void GetPhysicianClientViewTest()
        {
            YellowstonePathology.Business.View.PhysicianClientView result = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianClientView("9");
            Assert.IsTrue(result.PhysicianId == 9);
            Assert.IsTrue(result.Clients.Count == 4);            
        }

		[TestMethod]
		public void GetPhysicianClientNameCollectionTest()
		{
			YellowstonePathology.Business.Client.Model.PhysicianClientNameCollection result = YellowstonePathology.Business.Gateway.PhysicianClientGatewayMongo.GetPhysicianClientNameCollection("y", "brow");
			Assert.IsTrue(result.Count == 1);
		}

		[TestMethod]
		public void GetPhysicianClientNameCollection2Test()
		{
			YellowstonePathology.Business.Client.Model.PhysicianClientNameCollection result = YellowstonePathology.Business.Gateway.PhysicianClientGatewayMongo.GetPhysicianClientNameCollection("5650");
			Assert.IsTrue(result.Count == 6);
		}

		[TestMethod]
		public void GetPhysicianNameViewCollectionByPhysicianLastNameTest()
		{
			YellowstonePathology.Business.Client.Model.PhysicianNameViewCollection result = YellowstonePathology.Business.Gateway.PhysicianClientGatewayMongo.GetPhysicianNameViewCollectionByPhysicianLastName("S");
			Assert.IsTrue(result.Count > 0);
		}

		[TestMethod]
		public void GetPhysicianClientListByPhysicianLastNameTest()
		{
			YellowstonePathology.Business.Client.PhysicianClientCollection result = YellowstonePathology.Business.Gateway.PhysicianClientGatewayMongo.GetPhysicianClientListByPhysicianLastName("Bro");
			Assert.IsTrue(result.Count == 40);
		}

		[TestMethod]
		public void GetPhysicianClientListByClientPhysicianLastNameTest()
		{
			YellowstonePathology.Business.Client.PhysicianClientCollection result = YellowstonePathology.Business.Gateway.PhysicianClientGatewayMongo.GetPhysicianClientListByClientPhysicianLastName("y","Bro");
			Assert.IsTrue(result.Count == 1);
		}

		[TestMethod]
		public void GetPhysicianClientListByClientIdTest()
		{
			YellowstonePathology.Business.Client.PhysicianClientCollection result = YellowstonePathology.Business.Gateway.PhysicianClientGatewayMongo.GetPhysicianClientListByClientId(280);
			Assert.IsTrue(result.Count == 9);
		}

		[TestMethod]
		public void GetClientLocationViewByClientNameTest()
		{
			YellowstonePathology.Business.View.ClientLocationViewCollection result = YellowstonePathology.Business.Gateway.PhysicianClientGatewayMongo.GetClientLocationViewByClientName("St");
			Assert.IsTrue(result.Count == 33);
		}

		[TestMethod]
		public void GetPhysicianClientDistributionsTest()
		{
			List<YellowstonePathology.Business.Client.Model.PhysicianClientDistributionView> result = YellowstonePathology.Business.Gateway.PhysicianClientGatewayMongo.GetPhysicianClientDistributions(370);
			Assert.IsTrue(result.Count == 2);
		}

		[TestMethod]
		public void GetPhysicianClientDistributionByClientIdTest()
		{
			YellowstonePathology.Business.Client.PhysicianClientDistributionCollection result = YellowstonePathology.Business.Gateway.PhysicianClientGatewayMongo.GetPhysicianClientDistributionByClientId(280);
			Assert.IsTrue(result.Count == 9);
		}

		[TestMethod]
		public void GetPhysicianClientDistributionByClientPhysicianLastNameTest()
		{
			YellowstonePathology.Business.Client.PhysicianClientDistributionCollection result = YellowstonePathology.Business.Gateway.PhysicianClientGatewayMongo.GetPhysicianClientDistributionByClientPhysicianLastName("y", "Bro");
			Assert.IsTrue(result.Count == 1);
		}

		[TestMethod]
		public void GetPhysicianClientDistributionByPhysicianFirstLastNameTest()
		{
			YellowstonePathology.Business.Client.PhysicianClientDistributionCollection result = YellowstonePathology.Business.Gateway.PhysicianClientGatewayMongo.GetPhysicianClientDistributionByPhysicianFirstLastName("d", "SC");
			Assert.IsTrue(result.Count == 10);
		}

		[TestMethod]
		public void GetPhysicianClientDistributionByPhysicianLastNameTest()
		{
			YellowstonePathology.Business.Client.PhysicianClientDistributionCollection result = YellowstonePathology.Business.Gateway.PhysicianClientGatewayMongo.GetPhysicianClientDistributionByPhysicianLastName("Sc");
			Assert.IsTrue(result.Count == 94);
		}
	}
}
