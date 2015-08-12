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

namespace YellowstonePathology.UI.Test
{
	/// <summary>
	/// Interaction logic for NeogenomicsResultPage.xaml
	/// </summary>
	public partial class NeogenomicsResultPage : UserControl, YellowstonePathology.Business.Interface.IPersistPageChanges, INotifyPropertyChanged
	{        
		public delegate void PropertyChangedNotificationHandler(String info);
		public event PropertyChangedEventHandler PropertyChanged;

        public delegate void ShowResultEventHandler(object sender, YellowstonePathology.UI.CustomEventArgs.NeogenomicsResultReturnEventArgs e);
        public event ShowResultEventHandler ShowResult;

        public delegate void NextEventHandler(object sender, YellowstonePathology.UI.CustomEventArgs.NeogenomicsResultReturnEventArgs e);
		public event NextEventHandler Next;

		YellowstonePathology.Business.NeogenomicsResultCollection m_NeogenomicsResultCollection;

        public NeogenomicsResultPage()
		{
            this.m_NeogenomicsResultCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetNeogenomicsResultCollection();
			InitializeComponent();
			this.DataContext = this;
		}

		public YellowstonePathology.Business.NeogenomicsResultCollection NeogenomicsResultCollection
        {
            get { return this.m_NeogenomicsResultCollection; }
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
			
		}

		public void UpdateBindingSources()
		{

		}				              
        
		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

        private void ListBoxNeogenomicsResult_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ListBoxNeogenomicsResults.SelectedItem != null)
            {
				YellowstonePathology.Business.NeogenomicsResult neogenomicsResult = (YellowstonePathology.Business.NeogenomicsResult)this.ListBoxNeogenomicsResults.SelectedItem;
                this.ShowResult(this, new CustomEventArgs.NeogenomicsResultReturnEventArgs(neogenomicsResult));
            }
        }

        private void ButtonNext_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListBoxNeogenomicsResults.SelectedItem != null)
            {
				YellowstonePathology.Business.NeogenomicsResult neogenomicsResult = (YellowstonePathology.Business.NeogenomicsResult)this.ListBoxNeogenomicsResults.SelectedItem;
                this.Next(this, new CustomEventArgs.NeogenomicsResultReturnEventArgs(neogenomicsResult));
            }
        }        
	}
}
