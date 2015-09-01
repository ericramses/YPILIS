using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Visitor
{
    public class OrderTestOrderVisitor : AccessionTreeVisitor
    {
        protected YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;
        protected YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        protected YellowstonePathology.Business.PanelSet.Model.PanelSet m_PanelSet;
        protected YellowstonePathology.Business.Interface.IOrderTarget m_OrderTarget;        
        protected YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        protected string m_ReportNo;
        protected bool m_OrderTargetIsKnow;
		protected YellowstonePathology.Business.Test.TestOrderInfo m_TestOrderInfo;

        public OrderTestOrderVisitor(YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
            : base(true, true)
        {
            this.m_OrderTargetIsKnow = testOrderInfo.OrderTargetIsKnown;
            this.m_PanelSet = testOrderInfo.PanelSet;
            this.m_OrderTarget = testOrderInfo.OrderTarget;
            this.m_SystemIdentity = systemIdentity;            
			this.m_TestOrderInfo = testOrderInfo;
        }        

        public YellowstonePathology.Business.Test.PanelSetOrder PanelSetOrder
        {
            get { return this.m_PanelSetOrder; }
        }

        public override void Visit(Test.AccessionOrder accessionOrder)
        {
            this.m_AccessionOrder = accessionOrder;
            this.HandleAddAliquotOnOrder();
            this.HandleClientAccessioned();
            this.HandlePanelSetOrder();                                    
            this.HandlePanelOrders();
            this.HandlDistribution();
            this.HandlReflexTestingPlan();                       
        }

        private void HandleAddAliquotOnOrder()
        {
            if (this.m_PanelSet.AddAliquotOnOrder == true)
            {

            }
        }

        private void HandleClientAccessioned()
        {
            if (this.m_PanelSet.IsClientAccessioned == true)
            {
                this.m_AccessionOrder.ClientAccessioned = true;
                foreach (YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in this.m_AccessionOrder.SpecimenOrderCollection)
                {
                    specimenOrder.ClientAccessioned = true;
                    foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in specimenOrder.AliquotOrderCollection)
                    {
                        aliquotOrder.ClientAccessioned = true;
                    }
                }
            }
        }

        private void HandlePanelSetOrder()
        {
            this.m_ReportNo = this.m_AccessionOrder.GetNextReportNo(this.m_PanelSet);
            string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();

            bool distribute = this.m_TestOrderInfo.Distribute;
            if (this.m_PanelSet.NeverDistribute == true)
            {
                distribute = false;
            }

            this.m_PanelSetOrder = null;
            if (this.m_PanelSet.HasNoOrderTarget == true)
            {
                this.m_PanelSetOrder = YellowstonePathology.Business.Test.PanelSetOrderFactory.CreatePanelSetOrder(this.m_AccessionOrder.MasterAccessionNo, this.m_ReportNo, objectId, this.m_PanelSet, distribute, this.m_SystemIdentity);
            }
            else
            {
                if (this.m_OrderTargetIsKnow == false)
                {
                    this.m_OrderTarget = this.m_AccessionOrder.SpecimenOrderCollection.GetOrderTarget(this.m_PanelSet.OrderTargetTypeCollectionRestrictions);
                }
                this.m_PanelSetOrder = YellowstonePathology.Business.Test.PanelSetOrderFactory.CreatePanelSetOrder(this.m_AccessionOrder.MasterAccessionNo, this.m_ReportNo, objectId, this.m_PanelSet, this.m_OrderTarget, distribute, this.m_SystemIdentity);
            }            

            this.m_AccessionOrder.PanelSetOrderCollection.Add(this.m_PanelSetOrder);
            this.m_AccessionOrder.UpdateCaseAssignment(this.m_PanelSetOrder);
			this.m_TestOrderInfo.PanelSetOrder = this.m_PanelSetOrder;
        }

        public virtual void HandlePanelOrders()
        {
            foreach (YellowstonePathology.Business.Panel.Model.Panel panel in this.m_PanelSet.PanelCollection)
            {
                string panelOrderId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
                YellowstonePathology.Business.Test.PanelOrder panelOrder = YellowstonePathology.Business.Test.PanelOrderFactory.GetPanelOrder(this.m_ReportNo, panelOrderId, panelOrderId, panel, this.m_SystemIdentity.User.UserId);
                this.m_PanelSetOrder.PanelOrderCollection.Add(panelOrder);

                if (panel.AcknowledgeOnOrder == true)
                {
                    panelOrder.Acknowledged = true;
                    panelOrder.AcknowledgedById = this.m_SystemIdentity.User.UserId;
                    panelOrder.AcknowledgedDate = DateTime.Today;
                    panelOrder.AcknowledgedTime = DateTime.Now;
                }

                this.HandleTestOrders(panel, panelOrder);
            }
        }

        public virtual void HandleTestOrders(YellowstonePathology.Business.Panel.Model.Panel panel, YellowstonePathology.Business.Test.PanelOrder panelOrder)
        {
            if (this.m_OrderTarget is YellowstonePathology.Business.Test.AliquotOrder)
            {
                YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = (YellowstonePathology.Business.Test.AliquotOrder)this.m_OrderTarget;
                foreach (YellowstonePathology.Business.Test.Model.Test test in panel.TestCollection)
                {
                    string testOrderObjectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
                    YellowstonePathology.Business.Test.Model.TestOrder testOrder = panelOrder.TestOrderCollection.Add(panelOrder.PanelOrderId, testOrderObjectId, aliquotOrder.AliquotOrderId, test, test.OrderComment);                    

                    aliquotOrder.TestOrderCollection.Add(testOrder);
                    aliquotOrder.SetLabelPrefix(testOrder, true);
                }
            }
            else
            {
                foreach (YellowstonePathology.Business.Test.Model.Test test in panel.TestCollection)
                {
                    string testOrderObjectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
                    YellowstonePathology.Business.Test.Model.TestOrder testOrder = panelOrder.TestOrderCollection.Add(panelOrder.PanelOrderId, testOrderObjectId, null, test, test.OrderComment);
                    panelOrder.TestOrderCollection.Add(testOrder);
                }
            }
        }

        public virtual void HandlDistribution()
        {
            if (this.m_AccessionOrder.ClientId != 0 && this.m_AccessionOrder.PhysicianId != 0)
            {
                if (this.m_PanelSet.NeverDistribute == false)
                {
                    if (this.m_PanelSetOrder.Distribute == true)
                    {
                        YellowstonePathology.Business.Client.PhysicianClientDistributionCollection physicianClientDistributionCollection = YellowstonePathology.Business.Gateway.ReportDistributionGateway.GetPhysicianClientDistributionCollection(this.m_AccessionOrder.PhysicianId, this.m_AccessionOrder.ClientId);
                        physicianClientDistributionCollection.SetDistribution(this.m_PanelSetOrder, this.m_AccessionOrder);
                    }
                }
            }
        }

        public virtual void HandlReflexTestingPlan()
        {
            if (this.m_PanelSetOrder is YellowstonePathology.Business.Test.ReflexTesting.ReflexTestingPlan)
            {
                YellowstonePathology.Business.Test.ReflexTesting.ReflexTestingPlan reflexTestingPlan = (YellowstonePathology.Business.Test.ReflexTesting.ReflexTestingPlan)this.m_PanelSetOrder;
                reflexTestingPlan.OrderInitialTests(this.m_AccessionOrder, this.m_OrderTarget, this.m_SystemIdentity);
            }
        }

        public override void Visit(YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder surgicalTestOrder)
        {
			foreach (YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in this.m_AccessionOrder.SpecimenOrderCollection)
            {
                if (this.m_PanelSet.OrderTargetTypeCollectionExclusions.Exists(specimenOrder) == false)
                {
                    if (surgicalTestOrder.SurgicalSpecimenCollection.Exists(specimenOrder.SpecimenOrderId) == false)
                    {
                        YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen = surgicalTestOrder.SurgicalSpecimenCollection.Add(this.m_ReportNo);
                        surgicalSpecimen.FromSpecimenOrder(specimenOrder);
                    }
                }
            }
        }
    }
}
