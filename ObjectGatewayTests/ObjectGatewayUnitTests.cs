using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ObjectGatewayTests
{
    [TestClass]
    public class ObjectGatewayUnitTests
    {
        [TestMethod]
        public void SetupAOs()
        {
            YellowstonePathology.UI.AOBuilder aoBuilder1 = new YellowstonePathology.UI.AOBuilder();
            YellowstonePathology.Business.Test.AccessionOrder aoNew1 = aoBuilder1.Build();

            YellowstonePathology.UI.AOBuilder aoBuilder2 = new YellowstonePathology.UI.AOBuilder();
            YellowstonePathology.Business.Test.AccessionOrder aoNew2 = aoBuilder2.Build();
        }

        [TestMethod]
        public void AOPersistenceTest1()
        {
            YellowstonePathology.Business.Persistence.DocumentCollection documents = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Documents;

            object writer1 = "Writer1";
            object writer2 = 444;

            string masterAccessionNo1 = "16-11138";
            string masterAccessionNo2 = "16-11139";

            YellowstonePathology.Business.Test.AccessionOrder aoPulled11 = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(masterAccessionNo1, writer1);
            Assert.AreEqual(true, documents.Exists(masterAccessionNo1, writer1));
            aoPulled11.SvhAccount = "1";            

            YellowstonePathology.Business.Test.AccessionOrder aoPulled12 = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(masterAccessionNo1, writer2);
            Assert.AreEqual(true, documents.Exists(masterAccessionNo1, writer2));
            Assert.AreEqual("1", aoPulled12.SvhAccount);

            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(writer2);
            Assert.AreEqual(false, documents.Exists(masterAccessionNo2, writer2));
            Assert.AreEqual(true, documents.Exists(masterAccessionNo1, writer1));

            YellowstonePathology.Business.Test.AccessionOrder aoPulled21 = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(masterAccessionNo2, writer1);
            Assert.AreEqual(false, documents.Exists(masterAccessionNo1, writer1));
            Assert.AreEqual(true, documents.Exists(masterAccessionNo2, writer1));

            aoPulled11 = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(masterAccessionNo1, writer1);
            Assert.AreEqual(true, documents.Exists(masterAccessionNo1, writer1));
            Assert.AreEqual("1", aoPulled11.SvhAccount);

            aoPulled11.LockAquired = true;
            aoPulled11.LockAquiredByHostName = "HELLO";
            aoPulled11.LockAquiredById = 9999;
            aoPulled11.LockAquiredByUserName = "MOUSE";
            aoPulled11.TimeLockAquired = DateTime.Now;

            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(writer1);
            Assert.AreEqual(false, documents.Exists(masterAccessionNo1, writer1));

            aoPulled11 = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(masterAccessionNo1, writer1);
            Assert.AreEqual(false, aoPulled11.IsLockAquiredByMe);
            Assert.AreEqual("HELLO", aoPulled11.LockAquiredByHostName);


        }        
    }
}
