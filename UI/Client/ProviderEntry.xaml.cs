using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace YellowstonePathology.UI.Client
{
    public partial class ProviderEntry : Window
    {        
        private YellowstonePathology.Business.Domain.Physician m_Physician;
        private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;
		private YellowstonePathology.Business.Client.Model.StandingOrderCollection m_HpvStandingOrders;
		private YellowstonePathology.Business.View.PhysicianClientView m_PhysicianClientView;

        public ProviderEntry(YellowstonePathology.Business.Domain.Physician physician, YellowstonePathology.Business.Persistence.ObjectTracker objectTracker)
        {                        
            this.m_Physician = physician;
            this.m_ObjectTracker = objectTracker;

			this.m_PhysicianClientView = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianClientView(this.m_Physician.PhysicianId);
			this.m_HpvStandingOrders = new YellowstonePathology.Business.Client.Model.StandingOrderCollection();
			//this.m_HpvStandingOrders.SetDefaults();
            
            InitializeComponent();

            this.DataContext = this;
        }

        public YellowstonePathology.Business.Domain.Physician Physician
        {
            get { return this.m_Physician; }            
        }

		public ObservableCollection<YellowstonePathology.Business.Client.Model.StandingOrder> HpvStandingOrders
		{
			get { return this.m_HpvStandingOrders; }
		}

		public ObservableCollection<YellowstonePathology.Business.Client.Model.Client> Clients
		{
			get { return this.m_PhysicianClientView.Clients; }
		}

		private void ButtonOK_Click(object sender, RoutedEventArgs e)
		{
            this.m_ObjectTracker.SubmitChanges(this.m_Physician);
			Close();
		}

		private void ButtonCancel_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}		
		
	}
}
