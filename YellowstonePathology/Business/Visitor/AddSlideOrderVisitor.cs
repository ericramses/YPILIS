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

            YellowstonePathology.Business.Slide.Model.SlideOrder slideOrder = new Business.Slide.Model.SlideOrder();                        
            slideOrder.ObjectId = objectId;
            slideOrder.SlideOrderId = slideOrderId;
            slideOrder.AliquotOrderId = this.m_AliquotOrder.AliquotOrderId;            
            slideOrder.Label = YellowstonePathology.Business.Slide.Model.SlideOrder.GetSlideLabel(nextSlideNumber, this.m_AliquotOrder.Label, this.m_AliquotOrder.AliquotType);            
            slideOrder.TestOrder = this.m_TestOrder;
            slideOrder.ReportNo = panelSetOrder.ReportNo;
            slideOrder.TestOrderId = this.m_TestOrder.TestOrderId;
            slideOrder.TestId = this.m_TestOrder.TestId;
            slideOrder.TestName = this.m_TestOrder.TestName;
            slideOrder.TestAbbreviation = this.m_TestOrder.TestAbbreviation;
            slideOrder.PatientLastName = accessionOrder.PLastName;
            slideOrder.PatientFirstName = accessionOrder.PFirstName;
            slideOrder.Description = "Histology Slide";
            slideOrder.AliquotType = "Slide";
            slideOrder.OrderedById = this.m_SystemIdentity.User.UserId;
            slideOrder.OrderDate = DateTime.Now;
            slideOrder.OrderedBy = string.IsNullOrEmpty(this.m_TestOrder.OrderedBy) ? "NONE" : this.m_TestOrder.OrderedBy;
            slideOrder.OrderedFrom = System.Environment.MachineName;
            slideOrder.Status = Business.Slide.Model.SlideStatusEnum.Created.ToString();
            slideOrder.AccessioningFacility = accessioningFacility.LocationAbbreviation;
            slideOrder.LabelType = slide.LabelType.ToString();
            slideOrder.UseWetProtocol = this.m_TestOrder.UseWetProtocol;
            slideOrder.PerformedByHand = this.m_TestOrder.PerformedByHand;

            this.m_NewSlideOrder = slideOrder;
            this.m_TestOrder.SlideOrderCollection.Add(slideOrder);
            this.m_AliquotOrder.SlideOrderCollection.Add(slideOrder);
        }
    }
}
