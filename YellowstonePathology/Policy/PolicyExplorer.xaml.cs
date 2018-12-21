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
using System.ComponentModel;

namespace YellowstonePathology.Policy
{
    /// <summary>
    /// Interaction logic for PolicyExplorer.xaml
    /// </summary>
    public partial class PolicyExplorer : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
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

        private void ContextMenuAddFolder_Click(object sender, RoutedEventArgs e)
        {
            if(this.TreeViewDirectories.SelectedItem != null)
            {
                Directory parentDirectory = (Directory)this.TreeViewDirectories.SelectedItem;
                Directory newDirectory = new Directory();
                newDirectory.IsNew = true;
                newDirectory.ParentId = parentDirectory.DirectoryId;
                newDirectory.DirectoryName = "New Directory";
                this.m_DirectoryCollection.AddNewDirectory(newDirectory);
            }
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
