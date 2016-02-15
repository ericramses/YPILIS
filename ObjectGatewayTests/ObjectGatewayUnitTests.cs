using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ObjectGatewayTests
{
    [TestClass]
    public class ObjectGatewayUnitTests
    {        
        [TestMethod]
        public void LockIsAcquiredWhenUnlocked()
        {
            /*
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder("16-3228", this);
            accessionOrder.PAddress1 = "Hello 3228";

            accessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder("16-3737", this);
            accessionOrder.LockAquired = true;
            accessionOrder.LockAquiredByHostName = "Mouse";
            accessionOrder.LockAquiredById = 99999;
            accessionOrder.LockAquiredByUserName = "Mickey";            

            accessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder("16-3228", this);
            Assert.AreEqual(accessionOrder.PAddress1, "Hello 3228");

            accessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder("16-3737", this);
            Assert.IsFalse(accessionOrder.IsLockAquiredByMe());

            accessionOrder.PAddress1 = "Will it save when I don't have the lock.";
            accessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder("16-3228", this);
            accessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder("16-3737", this);

            Assert.IsFalse(accessionOrder.IsLockAquiredByMe());
            Assert.AreEqual(accessionOrder.PAddress1, "Will it save when I don't have the lock.");

            accessionOrder.PAddress1 = "Push save without lock";
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(accessionOrder, this);
            accessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder("16-3737", this);

            Assert.IsFalse(accessionOrder.IsLockAquiredByMe());
            Assert.AreEqual(accessionOrder.PAddress1, "Push save without lock");
            */

            //YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder("16-3737", this);
            //YellowstonePathology.Business.Persistence.DocumentGateway.Instance.ReleaseLock(accessionOrder, this);
            //Assert.AreEqual(accessionOrder.LockAquired, false);

            //accessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.GetAccessionOrderByMasterAccessionNo("16-3737");
            //Assert.AreEqual(accessionOrder.LockAquired, false);
        }        
    }
}
