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

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for VentanaStainEditDialog.xaml
    /// </summary>
    public partial class VentanaStainEditDialog : Window
    {
        public delegate void AcceptEventHandler(object sender, EventArgs e);
        public event AcceptEventHandler Accept;

        private Business.SpecialStain.VentanaBenchMark m_VentanaBenchMark;
        public VentanaStainEditDialog(int barcodeNumber)
        {
            this.m_VentanaBenchMark = Business.Persistence.DocumentGateway.Instance.PullVentanaBenchMark(barcodeNumber, this);
            InitializeComponent();
            DataContext = this;
        }

        public Business.SpecialStain.VentanaBenchMark VentanaBenchMark
        {
            get { return this.m_VentanaBenchMark; }
            set { this.m_VentanaBenchMark = value; }
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            Business.Persistence.DocumentGateway.Instance.Push(this);
            this.Accept(this, new EventArgs());
            Close();
        }
    }
}
