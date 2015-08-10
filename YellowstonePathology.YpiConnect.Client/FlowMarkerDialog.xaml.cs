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
using System.Windows.Shapes;

namespace YellowstonePathology.YpiConnect.Client
{   
    public partial class FlowMarkerDialog : Window
    {
		private YellowstonePathology.YpiConnect.Contract.Flow.MarkerCollection m_MarkerCollection;
		private YellowstonePathology.YpiConnect.Contract.Flow.FlowAccession m_FlowAccession;
		private List<YellowstonePathology.YpiConnect.Contract.Flow.Marker> m_SelectedMarkers;

		public FlowMarkerDialog(YellowstonePathology.YpiConnect.Contract.Flow.FlowAccession flowAccession)
        {
			this.m_FlowAccession = flowAccession;
			this.GetMarkerCollection();
            InitializeComponent();

			this.DataContext = this;
        }

		public YellowstonePathology.YpiConnect.Contract.Flow.MarkerCollection MarkerCollection
		{
			get { return this.m_MarkerCollection; }
		}

		public List<YellowstonePathology.YpiConnect.Contract.Flow.Marker> SelectedMarkers
		{
			get { return this.m_SelectedMarkers; }
		}

		private void GetMarkerCollection()
		{
			YellowstonePathology.YpiConnect.Proxy.FlowSignoutServiceProxy proxy = new Proxy.FlowSignoutServiceProxy();
			this.m_MarkerCollection = proxy.GetMarkers(this.m_FlowAccession.PanelSetOrderCollection[0].ReportNo);
		}

		private void ListViewFlowMarkers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			this.m_SelectedMarkers = new List<Contract.Flow.Marker>();
			if (ListViewFlowMarkers.SelectedItem != null)
			{
				this.m_SelectedMarkers.Add((YellowstonePathology.YpiConnect.Contract.Flow.Marker)ListViewFlowMarkers.SelectedItem);
			}
			this.DialogResult = true;
			Close();
		}

		private void ButtonAddMarker_Click(object sender, RoutedEventArgs e)
		{
			this.m_SelectedMarkers = new List<Contract.Flow.Marker>();
			foreach (YellowstonePathology.YpiConnect.Contract.Flow.Marker marker in this.ListViewFlowMarkers.SelectedItems)
			{
				this.m_SelectedMarkers.Add(marker);
			}
			this.DialogResult = true;
			Close();
		}

		private void ButtonCancel_Click(object sender, RoutedEventArgs e)
		{
			this.DialogResult = false;
			Close();
		}
    }
}
