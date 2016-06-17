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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace YellowstonePathology.UI.Login
{
    /// <summary>
    /// Interaction logic for DeleteAccessionPage.xaml
    /// </summary>
    public partial class DeleteAccessionPage : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public delegate void CloseEventHandler(object sender, EventArgs e);
        public event CloseEventHandler Close;

        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private object m_Writer;
        private string m_PageHeaderText = "Delete Accession page";

        public DeleteAccessionPage(Business.Test.AccessionOrder accessionOrder, object writer)
        {
            this.m_AccessionOrder = accessionOrder;
            this.m_Writer = writer;

            InitializeComponent();

            DataContext = this;
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close(this, new EventArgs());
        }

        public string PageHeaderText
        {
            get { return this.m_PageHeaderText; }
        }

        public Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
        }

        private void HyperLinkDeletePanelSet_Click(object sender, RoutedEventArgs e)
        {
            Business.Test.PanelSetOrder panelSetOrder = ((Hyperlink)sender).Tag as Business.Test.PanelSetOrder;
            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete report " + panelSetOrder.ReportNo + "?", "Delete Report", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if(result == MessageBoxResult.Yes)
            {
                Business.Rules.MethodResult methodResult = AORemover.RemovePanelSet(panelSetOrder.ReportNo, this.m_AccessionOrder, this.m_Writer);
                if(methodResult.Success == false)
                {
                    MessageBox.Show(methodResult.Message);
                }
            }
        }
    }
}
