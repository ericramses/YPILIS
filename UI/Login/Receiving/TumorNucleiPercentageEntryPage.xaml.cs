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
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace YellowstonePathology.UI.Login.Receiving
{	
	public partial class TumorNucleiPercentageEntryPage : UserControl, INotifyPropertyChanged, Shared.Interface.IPersistPageChanges
	{
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void NextEventHandler(object sender, EventArgs e);
		public event NextEventHandler Next;

		public delegate void BackEventHandler(object sender, EventArgs e);
		public event BackEventHandler Back;		
				
		private YellowstonePathology.Business.Interface.ISolidTumorTesting m_SolidTumorTesting;
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;

        public TumorNucleiPercentageEntryPage(YellowstonePathology.Business.Interface.ISolidTumorTesting solidTumorTesting, YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
            YellowstonePathology.Business.Persistence.ObjectTracker objectTracker)
		{
            this.m_SolidTumorTesting = solidTumorTesting;
            this.m_AccessionOrder = accessionOrder;
            this.m_ObjectTracker = objectTracker;

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

		public YellowstonePathology.Business.Interface.ISolidTumorTesting SolidTumorTesting
		{
			get { return this.m_SolidTumorTesting; }
		}		

		public bool OkToSaveOnNavigation(Type pageNavigatingTo)
		{
			return true;
		}

		public bool OkToSaveOnClose()
		{
			return true;
		}

		public void Save()
		{
            this.m_ObjectTracker.SubmitChanges(this.m_AccessionOrder);
		}

		public void UpdateBindingSources()
		{

		}       

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
            this.m_AccessionOrder.PanelSetOrderCollection.UpdateTumorNucleiPercentage(this.m_SolidTumorTesting);
			if (this.Next != null) this.Next(this, new EventArgs());
		}

		private void ButtonBack_Click(object sender, RoutedEventArgs e)
		{
			if (this.Back != null) this.Back(this, new EventArgs());
		}		
	}
}
