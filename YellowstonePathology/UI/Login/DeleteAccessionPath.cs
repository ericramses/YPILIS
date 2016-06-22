using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.UI.Login
{
    public class DeleteAccessionPath
    {
        public delegate void CloseOpenTabsEventHandler(object sender, EventArgs e);
        public event CloseOpenTabsEventHandler CloseOpenTabs;

        private YellowstonePathology.UI.Login.Receiving.LoginPageWindow m_LoginPageWindow;
        private DeleteAccessionLookupPage m_DeleteAccessionLookupPage;
        private int m_OpenTabCount;

        public DeleteAccessionPath(int openTabCount)
        {
            this.m_LoginPageWindow = new Receiving.LoginPageWindow();
            this.m_DeleteAccessionLookupPage = new DeleteAccessionLookupPage();
            this.m_DeleteAccessionLookupPage.Next += DeleteAccessionLookupPage_Next;
            this.m_DeleteAccessionLookupPage.Back += DeleteAccessionLookupPage_Back;

            this.m_OpenTabCount = openTabCount;
        }

        public void Start()
        {
            this.ShowDeleteAccessionLookupPage();
            this.m_LoginPageWindow.Height = 500;
            this.m_LoginPageWindow.Width = 500;
            this.m_LoginPageWindow.ShowDialog();
        }

        private void ShowDeleteAccessionLookupPage()
        {
            this.m_DeleteAccessionLookupPage.DoSearch();
            this.m_LoginPageWindow.PageNavigator.Navigate(this.m_DeleteAccessionLookupPage);
        }

        private void DeleteAccessionLookupPage_Next(object sender, CustomEventArgs.MasterAccessionNoReturnEventArgs e)
        {
            if (this.AccessionIsAvailableForDeletion(e.MasterAccessionNo) == true)
            {
                Business.Test.AccessionOrder accessionOrder = Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(e.MasterAccessionNo, this);
                if (accessionOrder.AccessionLock.IsLockAquiredByMe == true)
                {
                    this.ShowDeleteAccessionPage(accessionOrder);
                }
                else
                {
                    System.Windows.MessageBox.Show("Unable to delete as the case is locked.", "Case is locked");
                }
            }
        }

        private bool AccessionIsAvailableForDeletion(string masterAccessionNo)
        {
            bool result = false;
            if (this.m_OpenTabCount > 0)
            {
                System.Windows.MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("All open tabs will be closed and you work saved.  Do you wish to continue", "Open tabs", System.Windows.MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question, System.Windows.MessageBoxResult.No);
                if (messageBoxResult == System.Windows.MessageBoxResult.Yes)
                {
                    this.CloseOpenTabs(this, new EventArgs());
                    result = true;
                }
            }
            return result;
        }

        private void DeleteAccessionLookupPage_Back(object sender, EventArgs e)
        {
            this.m_LoginPageWindow.Close();
        }

        private void ShowDeleteAccessionPage(Business.Test.AccessionOrder accessionOrder)
        {
            DeleteAccessionPage deleteAccessionPage = new DeleteAccessionPage(accessionOrder, this);
            deleteAccessionPage.Back += DeleteAccessionPage_Back;
            deleteAccessionPage.Close += DeleteAccessionPage_Close;
            this.m_LoginPageWindow.PageNavigator.Navigate(deleteAccessionPage);
        }

        private void DeleteAccessionPage_Back(object sender, EventArgs e)
        {
            this.ShowDeleteAccessionLookupPage();
        }

        private void DeleteAccessionPage_Close(object sender, EventArgs e)
        {
            this.m_LoginPageWindow.Close();
        }
    }
}
