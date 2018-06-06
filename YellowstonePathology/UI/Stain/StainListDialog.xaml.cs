using System;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;

namespace YellowstonePathology.UI.Stain
{
    /// <summary>
    /// Interaction logic for StainListDialog.xaml
    /// </summary>
    public partial class StainListDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //public YellowstonePathology.Business.Stain.Model.StainCollection m_StainCollection;

        public StainListDialog()
        {
            InitializeComponent();
            DataContext = this;
        }

        public Business.Stain.Model.StainCollection StainCollection
        {
            get { return Business.Stain.Model.StainCollection.Instance; }
        }

        private void ListViewStains_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.ListViewStains.SelectedItem != null)
            {
                Business.Stain.Model.Stain stain = (Business.Stain.Model.Stain)this.ListViewStains.SelectedItem;
                StainEditDialog dialog = new StainEditDialog(stain);
                dialog.Accept += StainEditDialog_Accept;
                dialog.ShowDialog();
            }
        }

        private void StainEditDialog_Accept(object sender, EventArgs e)
        {
            Business.Stain.Model.StainCollection.Reload();
            this.NotifyPropertyChanged("StainCollection");
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            StainEditDialog dialog = new StainEditDialog();
            dialog.Accept += StainEditDialog_Accept;
            dialog.ShowDialog();
        }

        private void MenuItemDeleteStain_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListViewStains.SelectedItems.Count != 0)
            {
                foreach (YellowstonePathology.Business.Stain.Model.Stain stain in this.ListViewStains.SelectedItems)
                {
                    YellowstonePathology.Business.Stain.Model.StainCollection.Instance.DeleteStain(stain);
                }

                Business.Stain.Model.StainCollection.Reload();
                this.NotifyPropertyChanged("StainCollection");
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
