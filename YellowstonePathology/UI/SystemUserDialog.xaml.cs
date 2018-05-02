using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for SystemUserDialog.xaml
    /// </summary>
    public partial class SystemUserDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private YellowstonePathology.Business.User.SystemUserCollection m_SystemUserCollection;
        private YellowstonePathology.Business.User.SystemUser m_SystemUser;

        public SystemUserDialog()
        {
            this.m_SystemUserCollection = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection;
            InitializeComponent();
            this.DataContext = this;
            this.Closed += SystemUserDialog_Closed;
        }

        private void SystemUserDialog_Closed(object sender, EventArgs e)
        {
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(this);
            YellowstonePathology.Business.User.SystemUserCollectionInstance.Refresh();
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public YellowstonePathology.Business.User.SystemUserCollection SystemUserCollection
        {
            get { return this.m_SystemUserCollection; }
            set
            {
                this.m_SystemUserCollection = value;
                NotifyPropertyChanged("SystemUserCollection");
            }
        }

        public YellowstonePathology.Business.User.SystemUser SystemUser
        {
            get { return this.m_SystemUser; }
            set
            {
                this.m_SystemUser = value;
                NotifyPropertyChanged("SystemUser");
            }
        }

        private bool CanSave()
        {
            bool result = true;
            if(this.m_SystemUser != null)
            {
                if (string.IsNullOrEmpty(this.m_SystemUser.UserName) == true) result = false;
            }
            return result;
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            if (this.CanSave() == true)
            {
                Close();
            }
            else
            {
                MessageBox.Show("The User must have a User Name.");
            }
        }

        private void ButtonNew_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Push(this);

            YellowstonePathology.Business.User.SystemUser user = this.m_SystemUserCollection.OrderByDescending(item => item.UserId).First();
            this.m_SystemUser = new Business.User.SystemUser();
            this.m_SystemUser.UserId = user.UserId + 1;
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(this.m_SystemUser, this);
            this.m_SystemUserCollection.Add(this.m_SystemUser);
            this.NotifyPropertyChanged(string.Empty);
            this.ComboBoxUser.SelectedIndex = this.m_SystemUserCollection.Count - 1;
        }

        private void ComboBoxUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ComboBoxUser.SelectedItem != null)
            {
                this.SystemUser = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullSystemUser(((YellowstonePathology.Business.User.SystemUser)this.ComboBoxUser.SelectedItem).UserId, this);
            }
        }
    }
}
