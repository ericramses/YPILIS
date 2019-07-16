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
	public partial class PendingTestMonitorPage : UserControl, INotifyPropertyChanged, IMonitorPage
	{
		public event PropertyChangedEventHandler PropertyChanged;

        private YellowstonePathology.Business.Monitor.Model.PendingTestCollection m_CriticalTestCollection;
        private YellowstonePathology.Business.Monitor.Model.PendingTestCollection m_NormalTestCollection;

        public PendingTestMonitorPage()
		{         
            InitializeComponent();
            this.DataContext = this;            
		}

        private void LoadData()
        {
            YellowstonePathology.Business.Monitor.Model.PendingTestCollection pendingTestCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetPendingTestCollection();
            pendingTestCollection.SetState();
            this.m_CriticalTestCollection = pendingTestCollection.GetCriticalTestsForMonitorPriority(YellowstonePathology.Business.PanelSet.Model.PanelSet.MonitorPriorityCritical);
            this.m_NormalTestCollection = pendingTestCollection.GetCriticalTestsForMonitorPriority(YellowstonePathology.Business.PanelSet.Model.PanelSet.MonitorPriorityNormal);
            this.NotifyPropertyChanged("");
        }

        public void Refresh()
        {
            this.LoadData();
        }

        public YellowstonePathology.Business.Monitor.Model.PendingTestCollection CriticalTestCollection
        {
            get { return this.m_CriticalTestCollection; }
        }

        public YellowstonePathology.Business.Monitor.Model.PendingTestCollection NormalTestCollection
        {
            get { return this.m_NormalTestCollection; }
        }

        public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}        	        
		
        private void MenuItemDelay_Click(object sender, RoutedEventArgs e)
        {
            MenuItem item = sender as MenuItem;
            ListView view = ((ContextMenu)item.Parent).PlacementTarget as ListView;
            if (view.SelectedItem != null)
            {
                YellowstonePathology.Business.Monitor.Model.PendingTest pendingTest = (YellowstonePathology.Business.Monitor.Model.PendingTest)view.SelectedItem;
                PendingTestDelayDialog pendingTestDelayDialog = new PendingTestDelayDialog(pendingTest.ReportNo);
                pendingTestDelayDialog.ShowDialog();

                this.Refresh();
            }
        }

        private void MenuItemFinalize_Click(object sender, RoutedEventArgs e)
        {
            
        }		     
	}
}
