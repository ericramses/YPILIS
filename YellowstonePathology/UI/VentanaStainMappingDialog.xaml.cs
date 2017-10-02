using System;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for VentanaStainMappingDialog.xaml
    /// </summary>
    public partial class VentanaStainMappingDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Business.Surgical.VentanaBenchMarkCollection m_VentanaBenchMarkCollection;
        public VentanaStainMappingDialog()
        {
            this.m_VentanaBenchMarkCollection = Business.Gateway.SlideAccessionGateway.GetVentanaBenchMarks();
            InitializeComponent();
            DataContext = this;
        }

        public Business.Surgical.VentanaBenchMarkCollection VentanaBenchMarkCollection
        {
            get { return this.m_VentanaBenchMarkCollection; }
            set { this.m_VentanaBenchMarkCollection = value; }
        }

        private void ListViewStains_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(this.ListViewStains.SelectedItem != null)
            {
                Business.Surgical.VentanaBenchMark ventanaBenchMark = (Business.Surgical.VentanaBenchMark)this.ListViewStains.SelectedItem;
                VentanaStainEditDialog dialog = new UI.VentanaStainEditDialog(ventanaBenchMark.BarcodeNumber);
                dialog.Accept += VentanaStainEditDialog_Accept;
                dialog.ShowDialog();
            }
        }

        private void VentanaStainEditDialog_Accept(object sender, EventArgs e)
        {
            this.m_VentanaBenchMarkCollection = Business.Gateway.SlideAccessionGateway.GetVentanaBenchMarks();
            this.NotifyPropertyChanged("VentanaBenchMarkCollection");
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            //VentanaStainAddDialog dialog = new UI.VentanaStainAddDialog();
            //dialog.Accept += VentanaStainAddDialog_Accept;
            //dialog.ShowDialog();
        }

        private void VentanaStainAddDialog_Accept(object sender, EventArgs e)
        {
            this.m_VentanaBenchMarkCollection = Business.Gateway.SlideAccessionGateway.GetVentanaBenchMarks();
            this.NotifyPropertyChanged("VentanaBenchMarkCollection");
        }

        private void MenuItemDeleteStain_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewStains.SelectedItems.Count != 0)
            {
                foreach (YellowstonePathology.Business.Surgical.VentanaBenchMark ventanaBenchMark in this.ListViewStains.SelectedItems)
                {
                    YellowstonePathology.Business.Persistence.DocumentGateway.Instance.DeleteDocument(ventanaBenchMark, this);
                }

                this.m_VentanaBenchMarkCollection = Business.Gateway.SlideAccessionGateway.GetVentanaBenchMarks();
                this.NotifyPropertyChanged("VentanaBenchMarkCollection");
            }
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
