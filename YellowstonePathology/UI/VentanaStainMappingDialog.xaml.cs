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

        private Business.SpecialStain.VentanaBenchMarkCollection m_VentanaBenchMarkCollection;
        public VentanaStainMappingDialog()
        {
            this.m_VentanaBenchMarkCollection = Business.Gateway.SlideAccessionGateway.GetVentanaBenchMarks();
            InitializeComponent();
            DataContext = this;
        }

        public Business.SpecialStain.VentanaBenchMarkCollection VentanaBenchMarkCollection
        {
            get { return this.m_VentanaBenchMarkCollection; }
            set { this.m_VentanaBenchMarkCollection = value; }
        }

        private void ListViewStains_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(this.ListViewStains.SelectedItem != null)
            {
                Business.SpecialStain.VentanaBenchMark ventanaBenchMark = (Business.SpecialStain.VentanaBenchMark)this.ListViewStains.SelectedItem;
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

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
