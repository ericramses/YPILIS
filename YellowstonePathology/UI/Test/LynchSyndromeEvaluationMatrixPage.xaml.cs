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
        private YellowstonePathology.Business.Test.LynchSyndrome.LSEResultCollection m_LSEResultCollection;
		private YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation m_PanelSetOrderLynchSyndromeEvaluation;                

		public LynchSyndromeEvaluationMatrixPage(YellowstonePathology.Business.Test.LynchSyndrome.PanelSetOrderLynchSyndromeEvaluation panelSetOrderLynchSyndromeEvaluation,
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.User.SystemIdentity systemIdentity) 
            : base(panelSetOrderLynchSyndromeEvaluation, accessionOrder)
		{
			this.m_PanelSetOrderLynchSyndromeEvaluation = panelSetOrderLynchSyndromeEvaluation;
			this.m_AccessionOrder = accessionOrder;
			this.m_SystemIdentity = systemIdentity;

            this.m_LSEResultCollection = YellowstonePathology.Business.Test.LynchSyndrome.LSEResultCollection.GetAll();
            InitializeComponent();
			DataContext = this;

            this.m_ControlsNotDisabledOnFinal.Add(this.ButtonBack);
            this.m_ControlsNotDisabledOnFinal.Add(this.ButtonNext);
            this.m_ControlsNotDisabledOnFinal.Add(this.ListViewResults);
        }        
        
        public Business.Test.LynchSyndrome.LSEResultCollection LSEResultCollection
        {
            get { return this.m_LSEResultCollection; }
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
		
    }
}
