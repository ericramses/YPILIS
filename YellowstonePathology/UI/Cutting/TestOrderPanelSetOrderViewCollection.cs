using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace YellowstonePathology.UI.Cutting
{
    public class TestOrderPanelSetOrderViewCollection : ObservableCollection<TestOrderPanelSetOrderView>
    {
        public void UpdateCollection()
        {
            foreach (TestOrderPanelSetOrderView item in this)
            {
                item.Update();
            }
        }

        public TestOrderPanelSetOrderViewCollection(YellowstonePathology.Business.Test.Model.TestOrderCollection_Base testOrderCollection, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {            
            foreach (YellowstonePathology.Business.Test.Model.TestOrder_Base testOrder in testOrderCollection)
            {
                YellowstonePathology.Business.Test.Model.TestOrder realTestOrder = accessionOrder.PanelSetOrderCollection.GetTestOrderByTestOrderId(testOrder.TestOrderId);
                YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = accessionOrder.PanelSetOrderCollection.GetPanelSetOrderByTestOrderId(testOrder.TestOrderId);
                TestOrderPanelSetOrderView testOrderPanelSetOrderView = new TestOrderPanelSetOrderView(panelSetOrder, realTestOrder);
                this.Add(testOrderPanelSetOrderView);
            }
        }

        public void PrintLabels(Business.Test.AccessionOrder accessionOrder, Business.Test.AliquotOrder aliquotOrder)
        {
            foreach(TestOrderPanelSetOrderView item in this)
            {
                Business.Test.Model.TestOrder testOrder = accessionOrder.PanelSetOrderCollection.GetTestOrderByTestOrderId(item.TestOrderId);
                if(testOrder.SlideOrderCollection.Count == 0)
                {                                        
                    YellowstonePathology.Business.Visitor.AddSlideOrderVisitor addSlideOrderVisitor = new Business.Visitor.AddSlideOrderVisitor(aliquotOrder, testOrder);
                    accessionOrder.TakeATrip(addSlideOrderVisitor);
                    
                    Business.HL7View.VentanaStainOrder ventanaStainOrder = new Business.HL7View.VentanaStainOrder();
                    ventanaStainOrder.HandleOrder(accessionOrder, addSlideOrderVisitor.NewSlideOrder);
                }
            }
        }
    }
}
