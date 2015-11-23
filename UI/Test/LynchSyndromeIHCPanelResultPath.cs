﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
	public class LynchSyndromeIHCPanelResultPath : ResultPath
	{
		LynchSyndromeIHCPanelResultPage m_LynchSyndromeIHCPanelResultPage;
        YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeIHC m_PanelSetOrderLynchSyndromeIHC;
		private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;

        public LynchSyndromeIHCPanelResultPath(string reportNo,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.Persistence.ObjectTracker objectTracker,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator)
            : base(pageNavigator)
        {
            this.m_AccessionOrder = accessionOrder;
			this.m_PanelSetOrderLynchSyndromeIHC = (YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeIHC)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
			this.m_ObjectTracker = objectTracker;
		}

        protected override void ShowResultPage()
        {
            this.m_LynchSyndromeIHCPanelResultPage = new LynchSyndromeIHCPanelResultPage(this.m_PanelSetOrderLynchSyndromeIHC, this.m_AccessionOrder, this.m_ObjectTracker, this.m_SystemIdentity);
			this.m_LynchSyndromeIHCPanelResultPage.Next += new LynchSyndromeIHCPanelResultPage.NextEventHandler(LynchSyndromeIHCPanelResultPage_Next);
            this.m_PageNavigator.Navigate(this.m_LynchSyndromeIHCPanelResultPage);
        }

		private void LynchSyndromeIHCPanelResultPage_Next(object sender, EventArgs e)
        {
			YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeEvaluationTest panelSet = new YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeEvaluationTest();
            if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(panelSet.PanelSetId) == true)
            {
                this.StartLynchSyndromeEvaluationResultPath();
            }
            else
            {
                this.Finished();
            }            
		}

		private void StartLynchSyndromeEvaluationResultPath()
		{
			YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeEvaluationTest panelSet = new YellowstonePathology.Business.Test.LynchSyndrome.LynchSyndromeEvaluationTest();
            string reportNo = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(panelSet.PanelSetId, this.m_PanelSetOrderLynchSyndromeIHC.OrderedOnId, true).ReportNo;
			Test.LynchSyndromeEvaluationResultPath path = new LynchSyndromeEvaluationResultPath(reportNo, this.m_AccessionOrder, this.m_ObjectTracker, this.m_PageNavigator, System.Windows.Visibility.Visible);
            path.Back += new LynchSyndromeEvaluationResultPath.BackEventHandler(LynchSyndromeEvaluationResultPath_Back);
			path.Finish += new FinishEventHandler(LynchSyndromeEvaluationResultPath_Finish);
			path.Start(this.m_SystemIdentity);				
		}

        private void LynchSyndromeEvaluationResultPath_Back(object sender, EventArgs e)
        {
            this.ShowResultPage();
        }

		private void LynchSyndromeEvaluationResultPath_Finish(object sender, EventArgs e)
		{
			this.Finished();
		}
	}
}
