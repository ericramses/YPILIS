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
		private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;

		public SurgicalBlocks()
		{
			this.m_SurgicalBillingItemCollection = new YellowstonePathology.Business.Surgical.SurgicalBillingItemCollection();
			this.m_ObjectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();
			this.m_ObjectTracker.RegisterObject(this.m_SurgicalBillingItemCollection);
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
			this.m_ObjectTracker.SubmitChanges(this.m_SurgicalBillingItemCollection);
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
				this.m_ObjectTracker.SubmitChanges(this.m_SurgicalBillingItemCollection);

				this.m_Date = this.BlockDatePicker.SelectedDate.Value;

				this.m_SurgicalBillingItemCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetSurgicalBillingItemCollectionByDate(m_Date);
				this.m_ObjectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();
				this.m_ObjectTracker.RegisterObject(this.m_SurgicalBillingItemCollection);
				this.NotifyPropertyChanged("SurgicalBillingItemCollection");
				//for (int idx = m_SurgicalBillingItemCollection.Count - 1; idx > -1; idx--)
				//{
				//	m_SurgicalBillingItemCollection.RemoveAt(idx);
				//}

				//YellowstonePathology.Business.Surgical.SurgicalBillingItemCollection collection = this.m_AccessionOrderGateway.GetSurgicalBillingItemCollectionByDate(m_Date);
				//if (collection != null)
				//{
				//	foreach (YellowstonePathology.Business.Surgical.SurgicalBillingItem item in collection) this.m_SurgicalBillingItemCollection.Add(item);
				//}
				//this.m_SurgicalBillingItemCollection.Clear();
			}
		}

	}
}
