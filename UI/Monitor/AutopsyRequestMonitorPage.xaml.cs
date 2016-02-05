using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.ComponentModel;


namespace YellowstonePathology.UI.Monitor
{
	public partial class AutopsyRequestMonitorPage : UserControl, INotifyPropertyChanged, IMonitorPage, YellowstonePathology.Business.Interface.IPersistPageChanges
    {
		public event PropertyChangedEventHandler PropertyChanged;		
		
		public AutopsyRequestMonitorPage()
		{			
			InitializeComponent();
			this.DataContext = this;         
		}       
		
		public void Refresh()
        {
    		
            this.LoadData();
        }				
		
		private void LoadData()
        {            
			this.NotifyPropertyChanged(string.Empty);
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
    }
}