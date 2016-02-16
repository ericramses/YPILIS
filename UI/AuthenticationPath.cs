using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI
{
    public class AuthentificationPath
    {
        public delegate void AuthentificationSuccessfulEventHandler(object sender, CustomEventArgs.SystemIdentityReturnEventArgs e);
        public event AuthentificationSuccessfulEventHandler AuthentificationSuccessful;

        public delegate void AuthentificationNotSuccessfulEventHandler(object sender, EventArgs e);
        public event AuthentificationNotSuccessfulEventHandler AuthentificationNotSuccessful;
        
        YellowstonePathology.UI.Navigation.PageNavigator m_PageNavigator;

        public AuthentificationPath(YellowstonePathology.UI.Navigation.PageNavigator pageNavigator)
        {            
            this.m_PageNavigator = pageNavigator;
        }

        public void Start()
        {
            if (Business.User.SystemIdentity.DoesLoggedInUserNeedToScanId() == true)
            {
                this.ShowScanSecurityBadgePage();
            }
            else
            {
                YellowstonePathology.Business.User.SystemIdentity currentlyLoggedInuser = Business.User.SystemIdentity.Instance;
                this.AuthentificationSuccessful(this, new CustomEventArgs.SystemIdentityReturnEventArgs(currentlyLoggedInuser));
            }
        }

        private void ShowScanSecurityBadgePage()
        {
            YellowstonePathology.UI.Login.ScanSecurityBadgePage scanSecurityBadgePage = new YellowstonePathology.UI.Login.ScanSecurityBadgePage(System.Windows.Visibility.Collapsed);
            scanSecurityBadgePage.AuthentificationSuccessful += new YellowstonePathology.UI.Login.ScanSecurityBadgePage.AuthentificationSuccessfulEventHandler(ScanSecurityBadgePage_AuthentificationSuccessful);
            this.m_PageNavigator.Navigate(scanSecurityBadgePage);
        }

        private void ScanSecurityBadgePage_AuthentificationSuccessful(object sender, CustomEventArgs.SystemIdentityReturnEventArgs e)
        {
            this.AuthentificationSuccessful(this, e);            
        }
    }
}
