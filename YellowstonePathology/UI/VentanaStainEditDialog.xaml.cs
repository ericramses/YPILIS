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

        private bool m_AddingVentanaBenchMark;
        private int m_BarcodeNumber;

        private Business.Surgical.VentanaBenchMark m_VentanaBenchMark;

        public VentanaStainEditDialog(int barcodeNumber)
        {
            this.m_BarcodeNumber = barcodeNumber;
            this.m_VentanaBenchMark = Business.Persistence.DocumentGateway.Instance.PullVentanaBenchMark(barcodeNumber, this);
            this.m_AddingVentanaBenchMark = false;
            InitializeComponent();
            DataContext = this;
        }
        public VentanaStainEditDialog()
        {
            this.m_VentanaBenchMark = new Business.Surgical.VentanaBenchMark();
            this.m_AddingVentanaBenchMark = true;
            InitializeComponent();
            DataContext = this;
        }

        public Business.Surgical.VentanaBenchMark VentanaBenchMark
        {
            get { return this.m_VentanaBenchMark; }
            set { this.m_VentanaBenchMark = value; }
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = this.CanSave();
            if (methodResult.Success == true)
            {
                if (this.m_AddingVentanaBenchMark == true)
                {
                    YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(this.m_VentanaBenchMark, this);
                }
                else
                {
                    if(this.m_VentanaBenchMark.BarcodeNumber != this.m_BarcodeNumber)
                    {
                        // since the barcode is the key, changing it requires an insert of the new and then a delete of the old
                        YellowstonePathology.Business.Persistence.DocumentGateway.Instance.InsertDocument(this.m_VentanaBenchMark, this);
                        this.m_VentanaBenchMark.BarcodeNumber = this.m_BarcodeNumber;
                        YellowstonePathology.Business.Persistence.DocumentGateway.Instance.DeleteDocument(this.m_VentanaBenchMark, this);
                    }
                    Business.Persistence.DocumentGateway.Instance.Push(this);
                }

                this.Accept(this, new EventArgs());
                Close();
            }
            else
            {
                MessageBox.Show(methodResult.Message);
            }
        }

        private YellowstonePathology.Business.Rules.MethodResult CanSave()
        {
            YellowstonePathology.Business.Rules.MethodResult result = new Business.Rules.MethodResult();
            if (this.m_VentanaBenchMark.BarcodeNumber == 0)
            {
                result.Success = false;
                result.Message = "The VentanaId must be a number greater than 0.";
            }
            return result;
        }
    }
}
