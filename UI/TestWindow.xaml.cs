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
using StackExchange.Redis;

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {

        ConnectionMultiplexer m_Redis;
        public TestWindow()
        {
            this.m_Redis = ConnectionMultiplexer.Connect("10.1.2.25");
            InitializeComponent();
            this.DataContext = this;
        }        

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            IDatabase db = this.m_Redis.GetDatabase();
            string lastName = "Mouse";
            db.StringSet("LastName", lastName);

            string result = db.StringGet("LastName");
            MessageBox.Show(result);
        }
    }
}
