using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;

namespace YellowstonePathology.YpiConnect.Client.OrderEntry
{
    /// <summary>
    /// Interaction logic for OwnershipDialog.xaml
    /// </summary>
    public partial class OwnershipDialog : Window, INotifyPropertyChanged
    {
        public delegate void PropertyChangedNotificationHandler(String info);
        public event PropertyChangedEventHandler PropertyChanged;

        YellowstonePathology.Domain.ClientOrder.Model.ClientOrder m_ClientOrder;

        public OwnershipDialog(YellowstonePathology.Domain.ClientOrder.Model.ClientOrder clientOrder)
        {
            this.m_ClientOrder = clientOrder;

            InitializeComponent();

            this.DataContext = this;
        }

        public YellowstonePathology.Domain.ClientOrder.Model.ClientOrder ClientOrder
        {
            get { return this.m_ClientOrder; }
        }

        public YellowstonePathology.YpiConnect.Contract.Identity.WebServiceAccount CurrentUser
        {
            get { return YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount; }
        }

        private void ButtonTakeOwnership_Click(object sender, RoutedEventArgs e)
        {
			if (YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount.DisplayName != this.m_ClientOrder.OrderedBy)
            {
                this.m_ClientOrder.OrderedBy = YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance.WebServiceAccount.DisplayName;
            }
            this.Close();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        protected void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }     
    }
}
