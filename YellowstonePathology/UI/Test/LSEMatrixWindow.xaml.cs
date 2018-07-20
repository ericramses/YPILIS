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

namespace YellowstonePathology.UI.Test
{
    /// <summary>
    /// Interaction logic for LSEMatrixWindow.xaml
    /// </summary>
    public partial class LSEMatrixWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Business.Test.LynchSyndrome.LSEResultCollection m_LSEResultCollection;
        private Business.Test.LynchSyndrome.LSEResult m_LSEResult;
        private Business.Test.LynchSyndrome.LSEResult m_SelectedLSEResult;

        public LSEMatrixWindow()
        {
            this.m_LSEResultCollection = YellowstonePathology.Business.Test.LynchSyndrome.LSEResultCollection.GetAll();
            this.m_LSEResult = new Business.Test.LynchSyndrome.LSEResult();
            this.m_LSEResult.Indication = "LSECOLON";
            this.m_LSEResult.MLH1Result = Business.Test.LynchSyndrome.LSEResultEnum.Intact;
            this.m_LSEResult.MSH2Result = Business.Test.LynchSyndrome.LSEResultEnum.Intact;
            this.m_LSEResult.MSH6Result = Business.Test.LynchSyndrome.LSEResultEnum.Intact;
            this.m_LSEResult.PMS2Result = Business.Test.LynchSyndrome.LSEResultEnum.Intact;

            this.m_LSEResult.BrafResult = Business.Test.LynchSyndrome.LSEResultEnum.NotSet;
            this.m_LSEResult.MethResult = Business.Test.LynchSyndrome.LSEResultEnum.NotSet;

            InitializeComponent();
            this.DataContext = this;
        }

        public Business.Test.LynchSyndrome.LSEResultCollection LSEResultCollection
        {
            get { return this.m_LSEResultCollection; }
        }

        public Business.Test.LynchSyndrome.LSEResult LSEResult
        {
            get { return this.m_LSEResult; }
        }

        public Business.Test.LynchSyndrome.LSEResult SelectedLSEResult
        {
            get { return this.m_SelectedLSEResult; }
        }        

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void ListViewResults_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(this.ListViewResults.SelectedItem != null)
            {
                this.m_SelectedLSEResult = (Business.Test.LynchSyndrome.LSEResult)this.ListViewResults.SelectedItem;
                this.m_SelectedLSEResult.SetResultsV2();
                this.NotifyPropertyChanged("SelectedLSEResult");
            }
        }

        private void ButtonMatch_Click(object sender, RoutedEventArgs e)
        {
            this.m_LSEResultCollection.ClearMatched();
            if (this.m_LSEResultCollection.HasIHCMatch(this.m_LSEResult))
            {
                this.m_LSEResultCollection.SetIHCMatch(this.m_LSEResult);
            }
            this.m_LSEResultCollection = this.m_LSEResultCollection.OrderByMatched();
            this.NotifyPropertyChanged("LSEResultCollection");
        }
    }
}
