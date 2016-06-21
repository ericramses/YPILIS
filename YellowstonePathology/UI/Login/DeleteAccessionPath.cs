using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.UI.Login
{
    public class DeleteAccessionPath
    {
        private YellowstonePathology.UI.Login.Receiving.LoginPageWindow m_LoginPageWindow;

        public DeleteAccessionPath()
        {
            this.m_LoginPageWindow = new Receiving.LoginPageWindow();
        }

        public void Start()
        {
            this.ShowDeleteAccessionOrderLookupPage();
            this.m_LoginPageWindow.Height = 500;
            this.m_LoginPageWindow.Width = 500;
            this.m_LoginPageWindow.ShowDialog();
        }

        private void ShowDeleteAccessionOrderLookupPage()
        {
            DeleteAccessionLookupPage deleteAccessionLookupPage = new DeleteAccessionLookupPage();
            deleteAccessionLookupPage.Next += DeleteAccessionLookupPage_Next;
            deleteAccessionLookupPage.Back += DeleteAccessionLookupPage_Back;
            this.m_LoginPageWindow.PageNavigator.Navigate(deleteAccessionLookupPage);
        }

        private void DeleteAccessionLookupPage_Next(object sender, CustomEventArgs.MasterAccessionNoReturnEventArgs e)
        {
            if (this.AccessionIsAvailableForDeletion(e.MasterAccessionNo) == true)
            {
                Business.Test.AccessionOrder accessionOrder = Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(e.MasterAccessionNo, this);
                if (accessionOrder.AccessionLock.IsLockAquiredByMe == true)
                {
                    this.ShowDeleteAccessionOrderLookupPage(accessionOrder);
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Unable to delete as the case is locked.", "Case is locked");
            }
        }

        private bool AccessionIsAvailableForDeletion(string masterAccessionNo)
        {
            bool result = true;
            Business.Test.AccessionLockCollection accessionLockCollection = new Business.Test.AccessionLockCollection();
            foreach(Business.Test.AccessionLock accessionLock in accessionLockCollection)
            {
                if(accessionLock.MasterAccessionNo == masterAccessionNo)
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        private void DeleteAccessionLookupPage_Back(object sender, EventArgs e)
        {
            this.m_LoginPageWindow.Close();
        }

        private void ShowDeleteAccessionOrderLookupPage(Business.Test.AccessionOrder accessionOrder)
        {
            DeleteAccessionPage deleteAccessionPage = new DeleteAccessionPage(accessionOrder, this);
            deleteAccessionPage.Close += DeleteAccessionPage_Close;
            this.m_LoginPageWindow.PageNavigator.Navigate(deleteAccessionPage);
        }

        private void DeleteAccessionPage_Close(object sender, EventArgs e)
        {
            this.m_LoginPageWindow.Close();
        }
    }
}
