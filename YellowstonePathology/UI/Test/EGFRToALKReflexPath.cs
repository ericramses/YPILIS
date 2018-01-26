﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
    public class EGFRToALKReflexPath : ResultPath
    {
        public delegate void BackEventHandler(object sender, EventArgs e);
        public event BackEventHandler Back;

		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;

        private YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisTestOrder m_EGFRToALKReflexAnalysisTestOrder;
        private EGFRToALKReflexPage m_EGFRToALKReflexPage;
        private System.Windows.Visibility m_BackButtonVisibility;

        public EGFRToALKReflexPath(string reportNo, YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
            System.Windows.Window window,
            System.Windows.Visibility backButtonVisibility) : base(pageNavigator, window)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_BackButtonVisibility = backButtonVisibility;
            
            this.m_EGFRToALKReflexAnalysisTestOrder = (YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
        }

        protected override void ShowResultPage()
        {
            this.m_EGFRToALKReflexPage = new EGFRToALKReflexPage(this.m_EGFRToALKReflexAnalysisTestOrder, this.m_AccessionOrder, this.m_SystemIdentity, this.m_BackButtonVisibility);
            this.m_EGFRToALKReflexPage.OrderALK += new EGFRToALKReflexPage.OrderALKEventHandler(EGFRToALKReflexPage_OrderALK);
            this.m_EGFRToALKReflexPage.OrderROS1 += new EGFRToALKReflexPage.OrderROS1EventHandler(EGFRToALKReflexPage_OrderROS1);            
            this.m_EGFRToALKReflexPage.OrderPDL122C3 += new EGFRToALKReflexPage.OrderPDL122C3EventHandler(EGFRToALKReflexPage_OrderPDL122C3);
            this.m_EGFRToALKReflexPage.Finish +=new EGFRToALKReflexPage.FinishEventHandler(EGFRToALKReflexPage_Finish);
            this.m_EGFRToALKReflexPage.Back += new EGFRToALKReflexPage.BackEventHandler(EGFRToALKReflexPage_Back);
            this.m_PageNavigator.Navigate(this.m_EGFRToALKReflexPage);
        }

        private void EGFRToALKReflexPage_OrderPDL122C3(object sender, EventArgs e)
        {
            YellowstonePathology.Business.Test.PDL122C3.PDL122C3Test pdl122C3Test = new Business.Test.PDL122C3.PDL122C3Test();
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget = this.m_AccessionOrder.SpecimenOrderCollection.GetOrderTarget(this.m_EGFRToALKReflexAnalysisTestOrder.OrderedOnId);
            YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo = new YellowstonePathology.Business.Test.TestOrderInfo(pdl122C3Test, orderTarget, false);
            YellowstonePathology.Business.Visitor.OrderTestOrderVisitor orderVisitor = new Business.Visitor.OrderTestOrderVisitor(testOrderInfo);
            this.m_AccessionOrder.TakeATrip(orderVisitor);
            orderVisitor.PanelSetOrder.Distribute = false;

            YellowstonePathology.Business.Task.Model.TaskOrder taskOrder = this.m_AccessionOrder.CreateTask(testOrderInfo);
            this.m_AccessionOrder.TaskOrderCollection.Add(taskOrder);

            this.m_AccessionOrder.PanelSetOrderCollection.UpdateTumorNucleiPercentage(this.m_EGFRToALKReflexAnalysisTestOrder);
        }        

        private void EGFRToALKReflexPage_OrderALK(object sender, EventArgs e)
        {
            YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTest alkForNSCLCByFISHTest = new YellowstonePathology.Business.Test.ALKForNSCLCByFISH.ALKForNSCLCByFISHTest();
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget = this.m_AccessionOrder.SpecimenOrderCollection.GetOrderTarget(this.m_EGFRToALKReflexAnalysisTestOrder.OrderedOnId);
            YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo = new YellowstonePathology.Business.Test.TestOrderInfo(alkForNSCLCByFISHTest, orderTarget, false);
            YellowstonePathology.Business.Visitor.OrderTestOrderVisitor orderVisitor = new Business.Visitor.OrderTestOrderVisitor(testOrderInfo);
            this.m_AccessionOrder.TakeATrip(orderVisitor);
            orderVisitor.PanelSetOrder.Distribute = false;

            YellowstonePathology.Business.Task.Model.TaskOrder taskOrder = this.m_AccessionOrder.CreateTask(testOrderInfo);
            this.m_AccessionOrder.TaskOrderCollection.Add(taskOrder);            

            this.m_AccessionOrder.PanelSetOrderCollection.UpdateTumorNucleiPercentage(this.m_EGFRToALKReflexAnalysisTestOrder);
        }

        private void EGFRToALKReflexPage_OrderROS1(object sender, EventArgs e)
        {
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget = this.m_AccessionOrder.SpecimenOrderCollection.GetOrderTarget(this.m_EGFRToALKReflexAnalysisTestOrder.OrderedOnId);
            YellowstonePathology.Business.Test.ROS1ByFISH.ROS1ByFISHTest ros1ByFISHTest = new Business.Test.ROS1ByFISH.ROS1ByFISHTest();
            YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo = new YellowstonePathology.Business.Test.TestOrderInfo(ros1ByFISHTest, orderTarget, false);
            YellowstonePathology.Business.Visitor.OrderTestOrderVisitor orderVisitor = new Business.Visitor.OrderTestOrderVisitor(testOrderInfo);
            this.m_AccessionOrder.TakeATrip(orderVisitor);
            orderVisitor.PanelSetOrder.Distribute = false;

            YellowstonePathology.Business.Task.Model.TaskOrder taskOrder = this.m_AccessionOrder.CreateTask(testOrderInfo);
            this.m_AccessionOrder.TaskOrderCollection.Add(taskOrder);

            this.m_AccessionOrder.PanelSetOrderCollection.UpdateTumorNucleiPercentage(this.m_EGFRToALKReflexAnalysisTestOrder);
        }

        private void EGFRToALKReflexPage_Back(object sender, EventArgs e)
        {
            if( this.Back != null) this.Back(this, new EventArgs());
        }

		private void EGFRToALKReflexPage_Finish(object sender, EventArgs e)
		{            
            this.Finished();
		}       

        private void ReportOrderPath_Finish(object sender, EventArgs e)
        {
			this.m_EGFRToALKReflexAnalysisTestOrder.SetStatus(this.m_AccessionOrder.PanelSetOrderCollection);
            this.m_PageNavigator.Navigate(this.m_EGFRToALKReflexPage);
        }		

        private void XpsDocumentViewerPage_Next(object sender, EventArgs e)
        {
            this.m_PageNavigator.Navigate(this.m_EGFRToALKReflexPage);
        }
	}
}
