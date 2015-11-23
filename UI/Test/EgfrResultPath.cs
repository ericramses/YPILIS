﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
    public class EGFRResultPath : ResultPath
    {
		EGFRResultPage m_EGFRResultPage;
        YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTestOrder m_EGFRMutationAnalysisTestOrder;
		private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;

        public EGFRResultPath(string reportNo,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.Persistence.ObjectTracker objectTracker,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator)
            : base(pageNavigator)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_EGFRMutationAnalysisTestOrder = (YellowstonePathology.Business.Test.EGFRMutationAnalysis.EGFRMutationAnalysisTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
			this.m_ObjectTracker = objectTracker;
		}

        protected override void ShowResultPage()
        {
            this.m_EGFRResultPage = new EGFRResultPage(this.m_EGFRMutationAnalysisTestOrder, this.m_AccessionOrder, this.m_ObjectTracker, this.m_SystemIdentity);
            this.m_EGFRResultPage.Next += new EGFRResultPage.NextEventHandler(EgfrResultPage_Next);
            this.m_PageNavigator.Navigate(this.m_EGFRResultPage);
        }

        private void EgfrResultPage_Next(object sender, EventArgs e)
        {
            if (this.ShowReflexTestPage() == false)
            {
				this.Finished();
			}
        }        

        private bool ShowReflexTestPage()
        {
            bool result = false;
            YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisTest panelSet = new Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisTest();
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSet.PanelSetId, this.m_EGFRMutationAnalysisTestOrder.OrderedOnId, true) == true)
            {
                YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisTestOrder testOrder = (YellowstonePathology.Business.Test.EGFRToALKReflexAnalysis.EGFRToALKReflexAnalysisTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSet.PanelSetId, this.m_EGFRMutationAnalysisTestOrder.OrderedOnId, true);
				result = true;
				Test.EGFRToALKReflexPath egfrToALKReflexPath = new Test.EGFRToALKReflexPath(testOrder.ReportNo, this.m_AccessionOrder, this.m_ObjectTracker, this.m_PageNavigator, System.Windows.Visibility.Visible);
                egfrToALKReflexPath.Finish += new Test.EGFRToALKReflexPath.FinishEventHandler(EGFRToALKReflexPath_Finish);
                egfrToALKReflexPath.Back += new EGFRToALKReflexPath.BackEventHandler(EGFRToALKReflexPath_Back);
                egfrToALKReflexPath.Start(this.m_SystemIdentity);
			}
            return result;
        }

        private void EGFRToALKReflexPath_Back(object sender, EventArgs e)
        {
            this.ShowResultPage();
        }

		private void EGFRToALKReflexPath_Finish(object sender, EventArgs e)
        {
            base.Finished();
		}

		private void ReportOrderPath_Finish(object sender, EventArgs e)
		{
			this.ShowReflexTestPage();
		}
	}
}
