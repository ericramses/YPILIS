using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ObjectGatewayTests
{
    [TestClass]
    public class ObjectGatewayUnitTests
    {        
        [TestMethod]
        public void AOPersistenceTest1()
        {
            YellowstonePathology.Business.Persistence.DocumentCollection documents = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Documents;

            object writer1 = "Writer1";
            object writer2 = 444;

            YellowstonePathology.UI.AOBuilder aoBuilder1 = new YellowstonePathology.UI.AOBuilder();
            YellowstonePathology.Business.Test.AccessionOrder aoNew1 = aoBuilder1.Build();

            YellowstonePathology.Business.Test.AccessionOrder aoPulled11 = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(aoNew1.MasterAccessionNo, writer1);
            Assert.AreEqual(true, documents.Exists(aoPulled11.MasterAccessionNo, writer1));

            aoPulled11.SvhAccount = "1";            

            YellowstonePathology.Business.Test.AccessionOrder aoPulled12 = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(aoNew1.MasterAccessionNo, writer2);
            Assert.AreEqual(true, documents.Exists(aoPulled12.MasterAccessionNo, writer2));
            Assert.AreEqual("1", aoPulled12.SvhAccount);
            Assert.AreEqual(aoPulled11.MasterAccessionNo, aoPulled12.MasterAccessionNo);

            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(writer2);
            Assert.AreEqual(false, documents.Exists(aoPulled12.MasterAccessionNo, writer2));
            Assert.AreEqual(true, documents.Exists(aoPulled11.MasterAccessionNo, writer1));

            YellowstonePathology.UI.AOBuilder aoBuilder2 = new YellowstonePathology.UI.AOBuilder();
            YellowstonePathology.Business.Test.AccessionOrder aoNew2 = aoBuilder2.Build();

            YellowstonePathology.Business.Test.AccessionOrder aoPulled21 = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(aoNew2.MasterAccessionNo, writer1);
            Assert.AreEqual(false, documents.Exists(aoPulled11.MasterAccessionNo, writer1));
            Assert.AreEqual(true, documents.Exists(aoPulled21.MasterAccessionNo, writer1));

            aoPulled11 = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(aoNew1.MasterAccessionNo, writer1);
            Assert.AreEqual(true, documents.Exists(aoPulled11.MasterAccessionNo, writer1));
            Assert.AreEqual("1", aoPulled11.SvhAccount);
        }        
    }
}
