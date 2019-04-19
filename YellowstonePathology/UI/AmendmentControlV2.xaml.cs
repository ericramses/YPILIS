using System;
using System.Collections.Generic;
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

namespace YellowstonePathology.UI
{
	public partial class AmendmentControlV2 : System.Windows.Controls.UserControl, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;        
		
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;
        private YellowstonePathology.Business.Amendment.Model.AmendmentCollection m_AmendmentCollection;

        public AmendmentControlV2(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder)
		{
			this.m_AccessionOrder = accessionOrder;
            this.m_PanelSetOrder = panelSetOrder;
            this.m_AmendmentCollection = new Business.Amendment.Model.AmendmentCollection();
            if (this.m_AccessionOrder != null) this.m_AmendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(this.m_PanelSetOrder.ReportNo);

            InitializeComponent();
			this.DataContext = this;

            if (this.m_AccessionOrder != null)
            {
                if (this.m_AccessionOrder.AccessionLock.IsLockAquiredByMe == false)
                {
                    this.ListViewAmendment.ContextMenu.IsEnabled = false;
                }
            }
		}        

        public YellowstonePathology.Business.User.SystemUser CurrentUser
		{
			get { return YellowstonePathology.Business.User.SystemIdentity.Instance.User; }
		}

		public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
		{
			get { return this.m_AccessionOrder; }
			private set
			{
				this.m_AccessionOrder = value;
				this.NotifyPropertyChanged("AccessionOrder");
			}
		}

        public YellowstonePathology.Business.Amendment.Model.AmendmentCollection AmendmentCollection
        {
            get { return this.m_AmendmentCollection; }
        }

        public void ContextMenuAddAmendment_Click(object sender, RoutedEventArgs args)
        {
            if (this.m_AccessionOrder != null)
            {
                this.m_AccessionOrder.AddAmendment(this.m_PanelSetOrder.ReportNo);
                this.m_AmendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(this.m_PanelSetOrder.ReportNo);
                this.NotifyPropertyChanged("AmendmentCollection");
            }
        }

        public void ContextMenuEditAmendment_Click(object sender, RoutedEventArgs args)
        {
			if (this.ListViewAmendment.SelectedItem != null)
			{
                YellowstonePathology.Business.Amendment.Model.Amendment amendment = (YellowstonePathology.Business.Amendment.Model.Amendment)this.ListViewAmendment.SelectedItem;                    
				YellowstonePathology.UI.AmendmentV2 amendmentV2 = new AmendmentV2(amendment, this.m_AccessionOrder, this.m_PanelSetOrder);
				amendmentV2.ShowDialog();
            }
        }

        public void ContextMenuDeleteAmendment_Click(object sedner, RoutedEventArgs args)
        {
			if (this.ListViewAmendment.SelectedItem != null)
			{
				MessageBoxResult result = MessageBox.Show("Delete the selected item?", "Delete.", MessageBoxButton.OKCancel);
				if (result == MessageBoxResult.OK)
				{
                    this.m_AccessionOrder.DeleteAmendment(((YellowstonePathology.Business.Amendment.Model.Amendment)this.ListViewAmendment.SelectedItem).AmendmentId);
                    this.m_AmendmentCollection = this.m_AccessionOrder.AmendmentCollection.GetAmendmentsForReport(this.m_PanelSetOrder.ReportNo);
                    this.NotifyPropertyChanged("AmendmentCollection");
                }
            }
        }

        public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

        private void ListViewAmendment_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            ListView listView = sender as ListView;
            GridView gView = listView.View as GridView;

            var workingWidth = listView.ActualWidth - SystemParameters.VerticalScrollBarWidth; // take into account vertical scrollbar
            var col1 = 0.12;
            var col2 = 0.76;
            var col3 = 0.12;

            gView.Columns[0].Width = workingWidth * col1;
            gView.Columns[1].Width = workingWidth * col2;
            gView.Columns[2].Width = workingWidth * col3;
        }
    }
}