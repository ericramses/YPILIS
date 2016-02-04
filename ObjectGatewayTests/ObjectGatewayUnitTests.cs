using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ObjectGatewayTests
{
    [TestClass]
    public class ObjectGatewayUnitTests
    {
        private void SetLockToNull(string masterAccessionNo)
        {
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Persistence.ObjectGatway.Instance.GetByMasterAccessionNo(masterAccessionNo, true);
            accessionOrder.LockAquiredByHostName = null;
            accessionOrder.LockAquiredById = null;
            accessionOrder.LockAquiredByUserName = null;
            accessionOrder.TimeLockAquired = null;
            YellowstonePathology.Business.Persistence.ObjectGatway.Instance.SubmitChanges(accessionOrder, true);
        }

        private void SetLockToTestValue(string masterAccessionNo)
        {
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Persistence.ObjectGatway.Instance.GetByMasterAccessionNo(masterAccessionNo, true);
            accessionOrder.LockAquiredByHostName = "ImaginaryComputer";
            accessionOrder.LockAquiredById = 5000;
            accessionOrder.LockAquiredByUserName = "Bubba";
            accessionOrder.TimeLockAquired = DateTime.Now.AddDays(-1);
            YellowstonePathology.Business.Persistence.ObjectGatway.Instance.SubmitChanges(accessionOrder, false);
        }

        [TestMethod]
        public void LockIsAcquiredWhenUnlocked()
        {
            SetLockToNull("14-1234");
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Persistence.ObjectGatway.Instance.GetByMasterAccessionNo("14-1234", true);
            Assert.IsTrue(accessionOrder.LockedAquired == true);
            SetLockToNull("14-1234");
        }

        [TestMethod]
        public void LockIsNotAcquiredWhenLockedByOtherUser()
        {
            SetLockToTestValue("14-1234");
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Persistence.ObjectGatway.Instance.GetByMasterAccessionNo("14-1234", true);
            Assert.IsTrue(accessionOrder.LockedAquired == false);
            SetLockToNull("14-1234");
        }

        [TestMethod]
        public void LockIsReleasedOnSave()
        {
            SetLockToNull("14-1234");
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Persistence.ObjectGatway.Instance.GetByMasterAccessionNo("14-1234", true);
            Assert.IsTrue(accessionOrder.LockedAquired == true);
            YellowstonePathology.Business.Persistence.ObjectGatway.Instance.SubmitChanges(accessionOrder, true);
            Assert.IsTrue(accessionOrder.LockedAquired == false);
        }

        [TestMethod]
        public void LockIsRetainedOnSave()
        {
            SetLockToNull("14-1234");
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Persistence.ObjectGatway.Instance.GetByMasterAccessionNo("14-1234", true);
            Assert.IsTrue(accessionOrder.LockedAquired == true);
            YellowstonePathology.Business.Persistence.ObjectGatway.Instance.SubmitChanges(accessionOrder, false);
            Assert.IsTrue(accessionOrder.LockedAquired == true);
            SetLockToNull("14-1234");
        }

        [TestMethod]
        public void LockForOtherUserRemainsOnSave()
        {
            SetLockToTestValue("14-1234");
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Persistence.ObjectGatway.Instance.GetByMasterAccessionNo("14-1234", true);
            Assert.IsTrue(accessionOrder.LockedAquired == false);
            YellowstonePathology.Business.Persistence.ObjectGatway.Instance.SubmitChanges(accessionOrder, true);
            accessionOrder = YellowstonePathology.Business.Persistence.ObjectGatway.Instance.GetByMasterAccessionNo("14-1234", true);
            Assert.IsTrue(accessionOrder.LockedAquired == false);

            YellowstonePathology.Business.Persistence.ObjectGatway.Instance.SubmitChanges(accessionOrder, true);
            Assert.IsTrue(accessionOrder.LockAquiredByHostName == "ImaginaryComputer");
            SetLockToNull("14-1234");
        }
    }
}
