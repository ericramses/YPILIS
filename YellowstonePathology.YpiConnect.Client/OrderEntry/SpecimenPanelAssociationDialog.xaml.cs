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
    public partial class SpecimenPanelAssociationDialog : Window, INotifyPropertyChanged
    {
        public delegate void PropertyChangedNotificationHandler(String info);
        public event PropertyChangedEventHandler PropertyChanged;

        YellowstonePathology.Domain.ClientOrder.Model.ClientOrder m_ClientOrder;
        YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity m_ApplicationIdentity;        

        public SpecimenPanelAssociationDialog(YellowstonePathology.Domain.ClientOrder.Model.ClientOrder clientOrder)
        {            
            this.m_ClientOrder = clientOrder;
            this.m_ApplicationIdentity = YellowstonePathology.YpiConnect.Contract.Identity.ApplicationIdentity.Instance;

            InitializeComponent();

            this.DataContext = this;
        }

        public YellowstonePathology.Domain.ClientOrder.Model.ClientOrder ClientOrder
        {
            get { return this.m_ClientOrder; }
        }

        protected void SelectCurrentListBoxItem(object sender, KeyboardFocusChangedEventArgs e)
        {
            ListBoxItem item = (ListBoxItem)sender;
            item.IsSelected = true;
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

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ListBoxSpecimen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }     
    }
}
