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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Markup;

namespace YellowstonePathology.YpiConnect.Client
{   
    public partial class BrowserWindow : Window
    {        
        public BrowserWindow(string fileName)
        {
            Uri uri = new Uri("file://" + fileName);
            InitializeComponent();            
            this.WebBrowserControl.Navigate(uri); 
        }        
    }
}
