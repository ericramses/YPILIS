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

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for TestPage.xaml
    /// </summary>
    public partial class TestPage : Window
    {        
		public TestPage()
        {            
            InitializeComponent();            
        }

        private void ButtonGo_Click(object sender, RoutedEventArgs e)
        {            
            System.Windows.Window window = App.Current.MainWindow;
            var parentArea = new System.Drawing.Rectangle((int)window.Left, (int)window.Top, (int)window.Width, (int)window.Height);
            System.Windows.Forms.Screen screen = System.Windows.Forms.Screen.FromRectangle(parentArea);
            
            MessageBox.Show("Hello");
        }
    }
}
