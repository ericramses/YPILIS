using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace YellowstonePathology.YpiConnect.Client
{
    public class PageNavigator
    {        
        private ContentControl m_ContentControl;
        private UserControl m_CurrentPage;        

        public PageNavigator(ContentControl contentControl)
        {
            this.m_ContentControl = contentControl;
        }

        public UserControl CurrentPage
        {
            get { return this.m_CurrentPage; }
        }

        public void Navigate(UserControl page)
        {
			YellowstonePathology.YpiConnect.Client.NavigatingEventArgs eventArgs = new NavigatingEventArgs(this.m_CurrentPage, page);

			YellowstonePathology.Shared.Interface.IPersistPageChanges navigatingFromPage = (YellowstonePathology.Shared.Interface.IPersistPageChanges)this.m_CurrentPage;
			YellowstonePathology.Shared.Interface.IPersistPageChanges navigatingToPage = (YellowstonePathology.Shared.Interface.IPersistPageChanges)page;

			if (navigatingFromPage != null)
            {
				if (navigatingFromPage.OkToSaveOnNavigation(navigatingToPage.GetType()) == true)
				{
					navigatingFromPage.Save();
				}
            }

            this.m_CurrentPage = page;
            this.m_ContentControl.Content = page;            
        }        

        public void CloseDocumentWindow()
        {

        }

        public void Close()
        {
			YellowstonePathology.Shared.Interface.IPersistPageChanges page = (YellowstonePathology.Shared.Interface.IPersistPageChanges)this.m_CurrentPage;
            if (page.OkToSaveOnClose() == true)
            {
                page.Save();
            }
        }
    }
}
