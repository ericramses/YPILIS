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

namespace YellowstonePathology.UI.Test
{
    /// <summary>
    /// Interaction logic for UnreachableResultPage.xaml
    /// </summary>
    public partial class UnreachableResultPage : UserControl
    {
        public delegate void NextEventHandler(object sender, EventArgs e);
        public event NextEventHandler Next;

        private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;

        private string m_PageHeaderText;

        public UnreachableResultPage(YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder)
        {
            this.m_PanelSetOrder = panelSetOrder;

            this.m_PageHeaderText = "Result not available for: " + accessionOrder.PatientDisplayName;

            InitializeComponent();

            DataContext = this;
        }

        public string PageHeaderText
        {
            get { return this.m_PageHeaderText; }
        }

        public string MessageLine1
        {
            get { return "The"; }
        }

        public string MessageLine2
        {
            get { return this.m_PanelSetOrder.PanelSetName; }
        }

        public string MessageLine3
        {
            get { return "result is not available from here."; }
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            if (this.Next != null) this.Next(this, new EventArgs());
        }
    }
}
