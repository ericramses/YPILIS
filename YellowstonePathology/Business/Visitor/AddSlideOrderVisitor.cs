using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Visitor
{
    public class AddSlideOrderVisitor : AccessionTreeVisitor
    {
        private YellowstonePathology.Business.Test.AliquotOrder m_AliquotOrder;
        private YellowstonePathology.Business.Test.Model.TestOrder m_TestOrder;
        private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        private Business.Slide.Model.SlideOrder m_NewSlideOrder;      

        public AddSlideOrderVisitor(YellowstonePathology.Business.Test.AliquotOrder aliquotOrder, YellowstonePathology.Business.Test.Model.TestOrder testOrder)
            : base(true, false)
        {
            this.m_AliquotOrder = aliquotOrder;
            this.m_TestOrder = testOrder;            
            this.m_SystemIdentity = YellowstonePathology.Business.User.SystemIdentity.Instance;
        }

        public Business.Slide.Model.SlideOrder NewSlideOrder
        {
            get { return this.m_NewSlideOrder; }
        }

        public override void Visit(Test.AccessionOrder accessionOrder)
        {
            YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = accessionOrder.PanelSetOrderCollection.GetPanelSetOrderByTestOrderId(this.m_TestOrder.TestOrderId);

            YellowstonePathology.Business.Facility.Model.Facility accessioningFacility = Business.Facility.Model.FacilityCollection.Instance.GetByFacilityId(accessionOrder.AccessioningFacilityId);

            Slide.Model.Slide slide = Slide.Model.SlideFactory.Get(this.m_TestOrder.TestId);

            int nextSlideNumber = this.m_AliquotOrder.SlideOrderCollection.Count() + 1; 
            string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();            
            string slideOrderId = YellowstonePathology.Business.OrderIdParser.GetNextSlideOrderId(this.m_AliquotOrder.SlideOrderCollection, this.m_AliquotOrder.AliquotOrderId);            

            YellowstonePathology.Business.Slide.Model.SlideOrder slideOrder = new Business.Slide.Model.SlideOrder(objectId, slideOrderId, this.m_AliquotOrder, this.m_TestOrder, this.m_SystemIdentity, nextSlideNumber);
            slideOrder.TestOrder = this.m_TestOrder;
            slideOrder.ReportNo = panelSetOrder.ReportNo;
            slideOrder.TestOrderId = this.m_TestOrder.TestOrderId;
            slideOrder.TestId = this.m_TestOrder.TestId;
            slideOrder.TestName = this.m_TestOrder.TestName;
            slideOrder.TestAbbreviation = this.m_TestOrder.TestAbbreviation;
            slideOrder.PatientLastName = accessionOrder.PLastName;
            slideOrder.PatientFirstName = accessionOrder.PFirstName;
            slideOrder.OrderedBy = string.IsNullOrEmpty(this.m_TestOrder.OrderedBy) ? "NONE" : this.m_TestOrder.OrderedBy;
            slideOrder.AccessioningFacility = accessioningFacility.LocationAbbreviation;
            slideOrder.LabelType = slide.LabelType.ToString();
            slideOrder.UseWetProtocol = this.m_TestOrder.UseWetProtocol;
            slideOrder.PerformedByHand = this.m_TestOrder.PerformedByHand;

            this.m_NewSlideOrder = slideOrder;
            this.m_TestOrder.SlideOrderCollection.Add(slideOrder);
            this.m_AliquotOrder.SlideOrderCollection.Add(slideOrder);
            
            //this.HandleKappaLambda(slideOrder, accessionOrder);
        }

        /*
        private void HandleKappaLambda(YellowstonePathology.Business.Slide.Model.SlideOrder slideOrder, Business.Test.AccessionOrder accessionOrder)
        {
            Business.Test.PanelOrder panelOrder = accessionOrder.PanelSetOrderCollection.GetPanelOrderByTestOrderId(this.m_TestOrder.TestOrderId);

            Business.Test.Model.Test kappa = YellowstonePathology.Business.Test.Model.TestCollectionInstance.GetClone("360"); // KappaByISH();
            Business.Test.Model.Test lambda = YellowstonePathology.Business.Test.Model.TestCollectionInstance.GetClone("361"); // LambdaByISH();
            Business.Test.Model.Test u6 = YellowstonePathology.Business.Test.Model.TestCollectionInstance.GetClone("383"); // U6();

            if (slideOrder.TestId == kappa.TestId || slideOrder.TestId == lambda.TestId)
            {
                bool u6Exists = this.m_AliquotOrder.SlideOrderCollection.TestExists(u6.TestId);
                if (u6Exists == false)
                {
                    //add a testorder and a slide order
                    YellowstonePathology.Business.Visitor.OrderTestVisitor orderTestVisitor = new Business.Visitor.OrderTestVisitor(panelOrder.ReportNo, u6, null, null, false, this.m_AliquotOrder, false, false, accessionOrder.TaskOrderCollection);
                    accessionOrder.TakeATrip(orderTestVisitor);

                    YellowstonePathology.Business.Visitor.AddSlideOrderVisitor addSlideOrderVisitor = new Business.Visitor.AddSlideOrderVisitor(this.m_AliquotOrder, orderTestVisitor.TestOrder);
                    accessionOrder.TakeATrip(addSlideOrderVisitor);
                }
            }
        }
        */
    }
}
