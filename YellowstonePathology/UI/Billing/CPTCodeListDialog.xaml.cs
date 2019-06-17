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
using System.ComponentModel;

namespace YellowstonePathology.UI.Billing
{
    /// <summary>
    /// Interaction logic for CPTCodeListDialog.xaml
    /// </summary>
    public partial class CPTCodeListDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public CPTCodeListDialog()
        {
            InitializeComponent();

            DataContext = this;
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public YellowstonePathology.Business.Billing.Model.CptCodeCollection CptCodeCollection
        {
            get { return YellowstonePathology.Store.AppDataStore.Instance.CPTCodeCollection; }
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ListBoxCPTCodes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.ListBoxCPTCodes.SelectedItem != null)
            {
                YellowstonePathology.Business.Billing.Model.CptCode cptCode = (YellowstonePathology.Business.Billing.Model.CptCode)this.ListBoxCPTCodes.SelectedItem;
                CPTCodeEditDialog dlg = new CPTCodeEditDialog(cptCode);
                bool? dialogResult = dlg.ShowDialog();
                if (dialogResult.HasValue && dialogResult.Value == true)
                {
                    YellowstonePathology.Store.AppDataStore.Instance.CPTCodeCollection.Load();
                    this.NotifyPropertyChanged("CptCodeCollection");
                }
            }
        }

        private void ButtonNewCode_Click(object sender, RoutedEventArgs e)
        {
            CPTCodeEditDialog dlg = new CPTCodeEditDialog(null);
            bool? dialogResult = dlg.ShowDialog();
            if (dialogResult.HasValue && dialogResult.Value == true)
            {
                YellowstonePathology.Store.AppDataStore.Instance.CPTCodeCollection.Load();
                this.NotifyPropertyChanged("CptCodeCollection");
            }
        }
    }
}
