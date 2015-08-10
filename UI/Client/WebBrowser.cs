using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace YellowstonePathology.UI.Client
{   
    public partial class WebBrowser : Form
    {               
        public WebBrowser()
        {            			
            InitializeComponent();
            this.webBrowser1.Navigate(@"http://www.google.com");
        }        
    }
}