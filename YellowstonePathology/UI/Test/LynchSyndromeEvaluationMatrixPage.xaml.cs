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
using System.ComponentModel;
using System.Xml.Linq;

namespace YellowstonePathology.UI.Test
{	
	public partial class LynchSyndromeEvaluationMatrixPage : ResultControl, INotifyPropertyChanged 
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void NextEventHandler(object sender, EventArgs e);
		public event NextEventHandler Next;

        public delegate void BackEventHandler(object sender, EventArgs e);
        public event BackEventHandler Back;		

		private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Test.LynchSyndrome.LSERuleCollection m_LSEResultCollection;
		private YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation m_PanelSetOrderLynchSyndromeEvaluation;
        private Business.Test.LynchSyndrome.LSERule m_LSERule;
        private Business.Test.LynchSyndrome.LSERule m_SelectedLSERule;

        public LynchSyndromeEvaluationMatrixPage(YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation panelSetOrderLynchSyndromeEvaluation,
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity) 
            : base(panelSetOrderLynchSyndromeEvaluation, accessionOrder)
		{
			this.m_PanelSetOrderLynchSyndromeEvaluation = panelSetOrderLynchSyndromeEvaluation;
			this.m_AccessionOrder = accessionOrder;
			this.m_SystemIdentity = systemIdentity;

            this.m_LSERule = YellowstonePathology.Business.Test.LynchSyndrome.LSERule.GetResult(this.m_AccessionOrder, this.m_PanelSetOrderLynchSyndromeEvaluation);
            this.FillRuleCollection(this.m_PanelSetOrderLynchSyndromeEvaluation.LynchSyndromeEvaluationType);
            
            InitializeComponent();
			DataContext = this;

            this.m_ControlsNotDisabledOnFinal.Add(this.ButtonBack);
            this.m_ControlsNotDisabledOnFinal.Add(this.ButtonNext);
            this.m_ControlsNotDisabledOnFinal.Add(this.ListViewResults);
        }

        public Business.Test.LynchSyndrome.LSERuleCollection LSERuleCollection
        {
            get { return this.m_LSEResultCollection; }
        }

        public Business.Test.LynchSyndrome.LSERule LSERule
        {
            get { return this.m_LSERule; }
        }

        public YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation PanelSetOrder
		{
			get { return this.m_PanelSetOrderLynchSyndromeEvaluation; }
		}

        public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
		{
			get { return this.m_AccessionOrder; }
		}

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}		
		
		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{            
            if (this.Next != null) this.Next(this, new EventArgs());            
		}

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            if (this.Back != null) this.Back(this, new EventArgs());
        }

        private void ListViewResults_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ListViewResults.SelectedItem != null)
            {
                this.m_SelectedLSERule = (Business.Test.LynchSyndrome.LSERule)this.ListViewResults.SelectedItem;
                this.m_SelectedLSERule.SetResultsV2(this.m_PanelSetOrderLynchSyndromeEvaluation);
                this.NotifyPropertyChanged("SelectedLSEResult");
            }
        }

        private void FillRuleCollection(string lseType)
        {
            //if(lseType == YellowstonePathology.Business.Test.LynchSyndrome.LSEType.NOTSET)
            //{
                this.m_LSEResultCollection = YellowstonePathology.Business.Test.LynchSyndrome.LSERuleCollection.GetAll();
            /*}
            else if(lseType == YellowstonePathology.Business.Test.LynchSyndrome.LSEType.COLON)
            {
                this.m_LSEResultCollection = YellowstonePathology.Business.Test.LynchSyndrome.LSERuleCollection.GetColonResults();
            }
            else if (lseType == YellowstonePathology.Business.Test.LynchSyndrome.LSEType.GENERAL)
            {
                this.m_LSEResultCollection = YellowstonePathology.Business.Test.LynchSyndrome.LSERuleCollection.GetProstateResults();
            }
            else if (lseType == YellowstonePathology.Business.Test.LynchSyndrome.LSEType.GYN)
            {
                this.m_LSEResultCollection = YellowstonePathology.Business.Test.LynchSyndrome.LSERuleCollection.GetGYNResults();
            }*/

            this.m_LSEResultCollection.SetIHCMatch(this.m_LSERule);
        }
    }
}
