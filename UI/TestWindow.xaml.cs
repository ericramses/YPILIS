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
    /// Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        private DateTime m_WorkDate;
        private string m_Test;

        public TestWindow()
        {                        
            InitializeComponent();
            this.DataContext = this;
        }

        public DateTime WorkDate
        {
            get { return this.m_WorkDate; }
            set { this.m_WorkDate = value; }
        }

        public string Test
        {
            get { return this.m_Test; }
            set { this.m_Test = value; }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
			YellowstonePathology.Business.Persistence.ObjectSqlBuilder objectSqlBuilder = new YellowstonePathology.Business.Persistence.ObjectSqlBuilder(typeof(YellowstonePathology.Business.Test.AccessionOrder), "14-123");            
        }
    }
}
