﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace YellowstonePathology.UI.Test
{
    public class FNAResultPath : ResultPath
    {        
        private FNAResultPage m_ResultPage;
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;
		private YellowstonePathology.Business.Test.FNAAdequacyAssessment.FNAAdequacyAssessmentTestOrder m_FNAAdequacyAssessmentResult;

        public FNAResultPath(string reportNo, YellowstonePathology.Business.Test.AccessionOrder accessionOrder,            
            YellowstonePathology.Business.Persistence.ObjectTracker objectTracker,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator)
            : base(pageNavigator)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_ObjectTracker = objectTracker;            
			this.m_FNAAdequacyAssessmentResult = (YellowstonePathology.Business.Test.FNAAdequacyAssessment.FNAAdequacyAssessmentTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
		}

        protected override void ShowResultPage()
		{
			this.m_ResultPage = new FNAResultPage(this.m_FNAAdequacyAssessmentResult, this.m_AccessionOrder, this.m_ObjectTracker, this.m_SystemIdentity);            
            this.m_ResultPage.Next += new FNAResultPage.NextEventHandler(ResultPage_Next);
            this.m_PageNavigator.Navigate(this.m_ResultPage);
		}

        private void ResultPage_Next(object sender, EventArgs e)
        {
            base.Finished();
        }        
    }
}
