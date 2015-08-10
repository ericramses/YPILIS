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

namespace YellowstonePathology.YpiConnect.Client.OrderEntry
{
    /// <summary>
    /// Interaction logic for ProviderLookupDialog.xaml
    /// </summary>
    public partial class ProviderLookupDialog : Window
    {
        YellowstonePathology.YpiConnect.Proxy.ClientOrderServiceProxy m_ClientOrderServiceProxy;
        YellowstonePathology.YpiConnect.Contract.Domain.PhysicianCollection m_PhysicianCollection;
        PhysicianCollectionView m_PhysicianCollectionView;
        YellowstonePathology.Domain.ClientOrder.Model.ClientOrder m_ClientOrder;

        public ProviderLookupDialog(YellowstonePathology.Domain.ClientOrder.Model.ClientOrder clientOrder, YellowstonePathology.YpiConnect.Proxy.ClientOrderServiceProxy clientOrderServiceProxy)
        {
            this.m_ClientOrder = clientOrder;
            this.m_ClientOrderServiceProxy = clientOrderServiceProxy;
           
            InitializeComponent();

            this.m_PhysicianCollection = this.m_ClientOrderServiceProxy.GetPhysiciansByClientId(558);
            this.m_PhysicianCollectionView = new PhysicianCollectionView(this.m_PhysicianCollection);
            
            this.DataContext = this;
            this.Loaded += new RoutedEventHandler(ProviderLookupDialog_Loaded);
        }

        private void ProviderLookupDialog_Loaded(object sender, RoutedEventArgs e)
        {
            this.TextBoxPhysicianName.Focus();
        }

        public PhysicianCollectionView PhysicianCollectionView
        {
            get { return this.m_PhysicianCollectionView; }
        }

        private void ButtonSelect_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            if (this.ListViewPhysicians.SelectedItems.Count != 0)
            {
                YellowstonePathology.YpiConnect.Contract.Domain.Physician physician = (YellowstonePathology.YpiConnect.Contract.Domain.Physician)this.ListViewPhysicians.SelectedItem;
                this.m_ClientOrder.ProviderId = physician.PhysicianID.ToString();
                this.m_ClientOrder.ProviderName = physician.FullName;
            }
            this.Close();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }        

        private void TextBoxPhysicianName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(TextBoxPhysicianName.Text) == false)
            {
                this.m_PhysicianCollectionView.FilterByLastName(TextBoxPhysicianName.Text);
            }
        }        
    }
}
