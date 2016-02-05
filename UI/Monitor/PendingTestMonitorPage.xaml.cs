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

namespace YellowstonePathology.UI.Monitor
{
	public partial class PendingTestMonitorPage : UserControl, INotifyPropertyChanged, YellowstonePathology.Business.Interface.IPersistPageChanges, IMonitorPage
	{
		public event PropertyChangedEventHandler PropertyChanged;

        private YellowstonePathology.Business.Monitor.Model.PendingTestCollection m_PendingTestCollection;

        public PendingTestMonitorPage()
		{         
            InitializeComponent();
            this.DataContext = this;            
		}

        private void LoadData()
        {
            YellowstonePathology.Business.Monitor.Model.PendingTestCollection pendingTestCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetPendingTestCollection();
            pendingTestCollection.SetState();
            pendingTestCollection = pendingTestCollection.SortByDifference();
            this.m_PendingTestCollection = pendingTestCollection;
            this.NotifyPropertyChanged("");
        }

        public void Refresh()
        {
            this.LoadData();
        }

        public YellowstonePathology.Business.Monitor.Model.PendingTestCollection PendingTestCollection
        {
            get { return this.m_PendingTestCollection; }
        }

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}        	        

		public bool OkToSaveOnNavigation(Type pageNavigatingTo)
		{
			return true;
		}

		public bool OkToSaveOnClose()
		{
			return true;
		}

		public void Save(bool releaseLock)
		{            
            
		}

		public void UpdateBindingSources()
		{

		}

        private void MenuItemDelay_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Please tell Sid that you see this message.  Thank you. Sid.");
            /*
            if (this.ListViewPendingTests.SelectedItem != null)
            {
                YellowstonePathology.Business.Monitor.Model.PendingTest pendingTest = (YellowstonePathology.Business.Monitor.Model.PendingTest)this.ListViewPendingTests.SelectedItem;
				YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetAccessionOrderByReportNo(pendingTest.ReportNo);
                

                YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = accessionOrder.PanelSetOrderCollection.GetPanelSetOrder(pendingTest.ReportNo);
                YellowstonePathology.Business.User.SystemIdentity systemIdentity = new YellowstonePathology.Business.User.SystemIdentity(Business.User.SystemIdentityTypeEnum.CurrentlyLoggedIn);
                PendingTestDelayDialog pendingTestDelayDialog = new PendingTestDelayDialog(panelSetOrder, systemIdentity);
                pendingTestDelayDialog.ShowDialog();
                
                this.Refresh();
            }
            */
        }

        private void MenuItemFinalize_Click(object sender, RoutedEventArgs e)
        {
            
        }		     
	}
}
