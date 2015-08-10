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
	/// Interaction logic for NeogenomicsResultTextPage.xaml
	/// </summary>
	public partial class NeogenomicsResultTextPage : UserControl, YellowstonePathology.Shared.Interface.IPersistPageChanges, INotifyPropertyChanged
	{
		public delegate void PropertyChangedNotificationHandler(String info);
		public event PropertyChangedEventHandler PropertyChanged;

		public delegate void FinishEventHandler(object sender, EventArgs e);
		public event FinishEventHandler Finish;

        public delegate void BackEventHandler(object sender, EventArgs e);
        public event BackEventHandler Back;

        string m_ResultText;

        public NeogenomicsResultTextPage()
		{            
			InitializeComponent();
			this.DataContext = this;
		}

		public void ShowResultText(YellowstonePathology.Business.NeogenomicsResult neogenomicsResult)
        {
            this.m_ResultText = neogenomicsResult.ToString();
            this.NotifyPropertyChanged("ResultText");
        }

        public void Clear()
        {
            this.m_ResultText = string.Empty;
            this.NotifyPropertyChanged("ResultText");
        }

        public string ResultText
        {
            get { return this.m_ResultText; }
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

        private void ButtonFinish_Click(object sender, RoutedEventArgs e)
        {
            if (this.Finish != null) this.Finish(this, new EventArgs());   
        }        

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            if (this.Back != null) this.Back(this, new EventArgs());   
        }

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}        
	}
}
