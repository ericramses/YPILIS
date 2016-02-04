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

        /* this one and the next assume the Micky Mouse accession is in the db and the ids are as written
        [TestMethod]
        public void InsertTestIntoAccessionOrder()
        {
            YellowstonePathology.Business.User.SystemIdentity systemIdentity = new YellowstonePathology.Business.User.SystemIdentity(YellowstonePathology.Business.User.SystemIdentityTypeEnum.CurrentlyLoggedIn);
            YellowstonePathology.Business.Test.Model.CongoRed test = new YellowstonePathology.Business.Test.Model.CongoRed();
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Persistence.ObjectGatway.Instance.GetByMasterAccessionNo("16-2823", true);
            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = accessionOrder.SpecimenOrderCollection.GetAliquotOrder("16-2823.1A");
            YellowstonePathology.Business.Visitor.OrderTestVisitor orderTestVisitor = new YellowstonePathology.Business.Visitor.OrderTestVisitor(accessionOrder.PanelSetOrderCollection.GetSurgical().ReportNo, test, test.OrderComment, null, false, aliquotOrder, false, false, accessionOrder.TaskOrderCollection, systemIdentity);
            accessionOrder.TakeATrip(orderTestVisitor);

            if ((aliquotOrder.AliquotType == "Block" ||
                aliquotOrder.AliquotType == "FrozenBlock" ||
                aliquotOrder.AliquotType == "CellBlock"))
            {
                YellowstonePathology.Business.Visitor.AddSlideOrderVisitor addSlideOrderVisitor = new YellowstonePathology.Business.Visitor.AddSlideOrderVisitor(aliquotOrder, orderTestVisitor.TestOrder, systemIdentity);
                accessionOrder.TakeATrip(addSlideOrderVisitor);
            }

            YellowstonePathology.Business.Persistence.SubmissionResult result = YellowstonePathology.Business.Persistence.ObjectGatway.Instance.SubmitChanges(accessionOrder, true);
            Assert.IsTrue(result.HasInsertCommands || result.HasInsertLastCommands);
        }

        [TestMethod]
        public void DeleteTestFromAccessionOrder()
        {
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Persistence.ObjectGatway.Instance.GetByMasterAccessionNo("16-2823", true);
            YellowstonePathology.Business.Slide.Model.SlideOrder slideOrder = accessionOrder.SpecimenOrderCollection.GetSlideOrder("16-2823.1A2");
            YellowstonePathology.Business.Visitor.RemoveSlideOrderVisitor removeSlideOrderVisitor = new YellowstonePathology.Business.Visitor.RemoveSlideOrderVisitor(slideOrder);
            accessionOrder.TakeATrip(removeSlideOrderVisitor);

            YellowstonePathology.Business.Test.Model.TestOrder testOrder = accessionOrder.PanelSetOrderCollection.GetTestOrderByTestOrderId("56b39f72bceac639e8aed3db.TO1");
            YellowstonePathology.Business.Visitor.RemoveTestOrderVisitor removeTestOrderVisitor = new YellowstonePathology.Business.Visitor.RemoveTestOrderVisitor(testOrder.TestOrderId);
            accessionOrder.TakeATrip(removeTestOrderVisitor);

            YellowstonePathology.Business.Persistence.SubmissionResult result = YellowstonePathology.Business.Persistence.ObjectGatway.Instance.SubmitChanges(accessionOrder, true);
            Assert.IsTrue(result.HasDeleteCommands || result.HasDeleteFirstCommands);
        }*/

        [TestMethod]
        public void UpdateAccessionOrder()
        {
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Persistence.ObjectGatway.Instance.GetByMasterAccessionNo("16-2823", true);
            accessionOrder.PAddress1 = "123 No Street";

            YellowstonePathology.Business.Persistence.SubmissionResult result = YellowstonePathology.Business.Persistence.ObjectGatway.Instance.SubmitChanges(accessionOrder, true);
            Assert.IsTrue(result.HasUpdateCommands);
            accessionOrder.PAddress1 = null;

            result = YellowstonePathology.Business.Persistence.ObjectGatway.Instance.SubmitChanges(accessionOrder, true);
            Assert.IsTrue(result.HasUpdateCommands);
        }
    }
}
