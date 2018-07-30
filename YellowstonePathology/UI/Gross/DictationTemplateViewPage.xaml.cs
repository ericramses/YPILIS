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

namespace YellowstonePathology.UI.Gross
{
    /// <summary>
    /// Interaction logic for DictationTemplateViewPage.xaml
    /// </summary>
    public partial class DictationTemplateViewPage : Window
    {
        public DictationTemplateViewPage()
        {
            InitializeComponent();
            DataContext = this;
        }

        public DictationTemplateCollection DictationTemplates
        {
            get { return DictationTemplateCollection.Instance; }
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
