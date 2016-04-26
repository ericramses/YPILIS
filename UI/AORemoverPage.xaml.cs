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

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for AORemoverPage.xaml
    /// </summary>
    public partial class AORemoverPage : UserControl
    {
        private string m_PageHeaderText;

        public AORemoverPage()
        {
            InitializeComponent();
            this.m_PageHeaderText = "Remove Accession Order Page";
        }

        public string PageHeaderText
        {
            get { return this.m_PageHeaderText; }
        }

        private void HyperLinkRemoveAccession_Click(object sender, RoutedEventArgs e)
        {
            string masterAccessionNo = this.TextBoxMasterAccessionNo.Text;
            if (string.IsNullOrEmpty(masterAccessionNo) == false)
            {
                YellowstonePathology.Business.OrderIdParser orderIdParser = new Business.OrderIdParser(masterAccessionNo);
                if(orderIdParser.IsValidMasterAccessionNo == true)
                {
                    AORemover aoRemover = new AORemover();
                    YellowstonePathology.Business.Rules.MethodResult methodResult = aoRemover.Remove(masterAccessionNo);
                    if(methodResult.Success == false)
                    {
                        MessageBox.Show(methodResult.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Enter a valid Master Accession Number");
                }
            }
            else
            {
                MessageBox.Show("Enter a Master Accession Number");
            }
        }

        private void ButtonFinished_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }
    }
}
