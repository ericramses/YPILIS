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

        private Business.Test.LynchSyndrome.LSERuleCollection m_LSERuleCollection;
        private Business.Test.LynchSyndrome.LSERule m_LSERule;
        private Business.Test.LynchSyndrome.LSERule m_SelectedLSERule;
        private Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation m_PanelSetOrderLynchSyndromeEvaluation;
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;

        public LSEMatrixWindow(YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation panelSetOrderLynchSyndromeEvaluation,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_PanelSetOrderLynchSyndromeEvaluation = panelSetOrderLynchSyndromeEvaluation;
            this.m_AccessionOrder = accessionOrder;
            this.m_LSERuleCollection = YellowstonePathology.Business.Test.LynchSyndrome.LSERuleCollection.GetAll();
            this.m_LSERule = YellowstonePathology.Business.Test.LynchSyndrome.LSERule.GetResult(this.m_AccessionOrder, this.m_PanelSetOrderLynchSyndromeEvaluation);
            this.m_LSERuleCollection.SetMatch(this.m_AccessionOrder, this.m_PanelSetOrderLynchSyndromeEvaluation, this.m_LSERule);

            InitializeComponent();
            this.DataContext = this;
        }

        public Business.Test.LynchSyndrome.LSERuleCollection LSERuleCollection
        {
            get { return this.m_LSERuleCollection; }
        }

        public Business.Test.LynchSyndrome.LSERule LSERule
        {
            get { return this.m_LSERule; }
        }

        public Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation PanelSetOrderLynchSyndromeEvaluation
        {
            get { return this.m_PanelSetOrderLynchSyndromeEvaluation; }
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
                this.m_SelectedLSERule = (Business.Test.LynchSyndrome.LSERule)this.ListViewResults.SelectedItem;
                this.m_SelectedLSERule.SetResultsV2(this.m_PanelSetOrderLynchSyndromeEvaluation);
                this.NotifyPropertyChanged("SelectedLSEResult");
            }
        }

        /*private void ButtonMatch_Click(object sender, RoutedEventArgs e)
        {
            this.m_LSERuleCollection.ClearMatched();
            if (this.m_LSERuleCollection.HasIHCMatch(this.m_LSERule))
            {
                this.m_LSERuleCollection.SetIHCMatch(this.m_LSERule);
            }
            this.m_LSERuleCollection = this.m_LSERuleCollection.OrderByMatched();
            this.NotifyPropertyChanged("LSEResultCollection");
        }*/
    }
}
