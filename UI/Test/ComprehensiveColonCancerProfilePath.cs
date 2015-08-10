using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
	public class ComprehensiveColonCancerProfilePath : ResultPath
	{
        public delegate void BackEventHandler(object sender, EventArgs e);
        public event BackEventHandler Back;

		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.Test.ComprehensiveColonCancerProfile.ComprehensiveColonCancerProfile m_ComprehensiveColonCancerProfile;
		private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;

        System.Windows.Visibility m_BackButtonVisibility;

        public ComprehensiveColonCancerProfilePath(string reportNo,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.Persistence.ObjectTracker objectTracker,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
            System.Windows.Visibility backButtonVisibility) 
            : base(pageNavigator)
        {
            this.m_AccessionOrder = accessionOrder;
			this.m_ComprehensiveColonCancerProfile = (YellowstonePathology.Business.Test.ComprehensiveColonCancerProfile.ComprehensiveColonCancerProfile)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(reportNo);
			this.m_ObjectTracker = objectTracker;
            this.m_BackButtonVisibility = backButtonVisibility;

			this.Authenticated += new AuthenticatedEventHandler(ResultPath_Authenticated);
		}

		private void ResultPath_Authenticated(object sender, EventArgs e)
		{
			this.ShowResultPage();
		}

        private void ShowResultPage()
        {
			ComprehensiveColonCancerProfilePage comprehensiveColonCancerProfilePage = new ComprehensiveColonCancerProfilePage(this.m_ComprehensiveColonCancerProfile, this.m_AccessionOrder, this.m_ObjectTracker, this.m_SystemIdentity, this.m_BackButtonVisibility);
			comprehensiveColonCancerProfilePage.Next += new ComprehensiveColonCancerProfilePage.NextEventHandler(ComprehensiveColonCancerProfilePage_Next);
            comprehensiveColonCancerProfilePage.Back += new ComprehensiveColonCancerProfilePage.BackEventHandler(ComprehensiveColonCancerProfilePage_Back);
			this.m_PageNavigator.Navigate(comprehensiveColonCancerProfilePage);
        }

        private void ComprehensiveColonCancerProfilePage_Back(object sender, EventArgs e)
        {
            if (this.Back != null) this.Back(this, new EventArgs());
        }

		private void ComprehensiveColonCancerProfilePage_Next(object sender, EventArgs e)
		{
			this.Finished();
		}		
	}
}
