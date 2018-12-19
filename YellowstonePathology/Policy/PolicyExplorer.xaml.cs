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

namespace YellowstonePathology.Policy
{
    /// <summary>
    /// Interaction logic for PolicyExplorer.xaml
    /// </summary>
    public partial class PolicyExplorer : Window
    {
        private DirectoryCollection m_DirectoryCollection;

        public PolicyExplorer()
        {
            this.m_DirectoryCollection = DirectoryCollection.GetRoot();
            InitializeComponent();
            this.DataContext = this;
        }

        public DirectoryCollection DirectoryCollection
        {
            get { return this.m_DirectoryCollection; }
        }
    }
}
