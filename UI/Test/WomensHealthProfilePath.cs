using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Login
{
    public class WomensHealthProfilePath
    {
        public delegate void FinishedEventHandler(object sender, EventArgs e);
        public event FinishedEventHandler Finished;
        public delegate void BackEventHandler(object sender, EventArgs e);
        public event BackEventHandler Back;

        private YellowstonePathology.UI.Test.WomensHealthProfilePage m_WomensHealthProfilePage;        
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;
		private YellowstonePathology.Business.ClientOrder.Model.ClientOrder m_ClientOrder;
        private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        private YellowstonePathology.UI.Navigation.PageNavigator m_PageNavigator;
        private System.Windows.Visibility m_BackButtonVisibility;

        public WomensHealthProfilePath(YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.Business.Persistence.ObjectTracker objectTracker,
			YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder,
            YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
            YellowstonePathology.Business.User.SystemIdentity systemIdentity,
            System.Windows.Visibility backButtonVisibility)
        {            
            this.m_SystemIdentity = systemIdentity;            
            this.m_AccessionOrder = accessionOrder;
            this.m_ObjectTracker = objectTracker;
			this.m_ClientOrder = clientOrder;
            this.m_PageNavigator = pageNavigator;
            this.m_BackButtonVisibility = backButtonVisibility;

			this.m_WomensHealthProfilePage = new Test.WomensHealthProfilePage(this.m_AccessionOrder, this.m_ObjectTracker, this.m_ClientOrder, this.m_SystemIdentity, this.m_BackButtonVisibility);
			this.m_WomensHealthProfilePage.Finished += new Test.WomensHealthProfilePage.FinishedEventHandler(WomensHealthProfilePage_Finished);
            this.m_WomensHealthProfilePage.Back += new Test.WomensHealthProfilePage.BackEventHandler(WomensHealthProfilePage_Back);
        }

        private void WomensHealthProfilePage_Finished(object sender, EventArgs e)
        {
            if (this.Finished != null) this.Finished(this, new EventArgs());
        }

        private void WomensHealthProfilePage_Back(object sender, EventArgs e)
        {
            if (this.Back != null) this.Back(this, new EventArgs());
        }

        public void Start()
        {
			this.m_PageNavigator.Navigate(this.m_WomensHealthProfilePage);            
        }
    }
}
