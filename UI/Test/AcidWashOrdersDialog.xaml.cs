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

namespace YellowstonePathology.UI.Test
{
    /// <summary>
    /// Interaction logic for AcidWashOrdersDialog.xaml
    /// </summary>
    public partial class AcidWashOrdersDialog : Window
    {
        private YellowstonePathology.Business.Test.SearchEngine m_SearchEngine;

        public AcidWashOrdersDialog()
        {
            this.m_SearchEngine = new Business.Test.SearchEngine();
            this.m_SearchEngine.SetFillByPanelSetId(15);
            this.m_SearchEngine.FillSearchList();

            InitializeComponent();

            DataContext = this;
        }

        public YellowstonePathology.Business.Search.ReportSearchList ReportSearchList
        {
            get { return this.m_SearchEngine.ReportSearchList; }
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
