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
using System.Windows.Shapes;
using System.ComponentModel;

namespace YellowstonePathology.UI.Test
{
	/// <summary>
	/// Interaction logic for SurgicalBlocks.xaml
	/// </summary>
	public partial class SurgicalBlocks : Window, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		private YellowstonePathology.Business.Surgical.SurgicalBillingItemCollection m_SurgicalBillingItemCollection;
		private DateTime m_Date;		

		public SurgicalBlocks()
		{
			this.m_SurgicalBillingItemCollection = new YellowstonePathology.Business.Surgical.SurgicalBillingItemCollection();
			m_Date = DateTime.Today;
			DataContext = this;

			InitializeComponent();
		}

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		public DateTime Date
		{
			get { return this.m_Date; }
			set { this.m_Date = value; }
		}

		public YellowstonePathology.Business.Surgical.SurgicalBillingItemCollection SurgicalBillingItemCollection
		{
			get { return this.m_SurgicalBillingItemCollection; }
		}

		private void ButtonOK_Click(object sender, RoutedEventArgs e)
		{
            YellowstonePathology.Business.Persistence.ObjectGateway.Instance.SubmitChanges(this.m_SurgicalBillingItemCollection, false);			
			Close();
		}

		private void ButtonCancel_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
		{
			if (this.BlockDatePicker.SelectedDate.HasValue)
			{
                YellowstonePathology.Business.Persistence.ObjectGateway.Instance.SubmitChanges(this.m_SurgicalBillingItemCollection, false);

                this.m_Date = this.BlockDatePicker.SelectedDate.Value;

				this.m_SurgicalBillingItemCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetSurgicalBillingItemCollectionByDate(m_Date);				
				this.NotifyPropertyChanged("SurgicalBillingItemCollection");
			}
		}
	}
}
