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
using System.ComponentModel;

namespace YellowstonePathology.UI.Client
{
    public partial class ProviderEntry : Window, INotifyPropertyChanged
    {
		public event PropertyChangedEventHandler PropertyChanged;
		
		private YellowstonePathology.Business.Domain.Physician m_Physician;
        private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;
		private YellowstonePathology.Business.Client.Model.StandingOrderCollection m_HpvStandingOrders;
		private YellowstonePathology.Business.View.PhysicianClientView m_PhysicianClientView;
		private YellowstonePathology.Business.Client.Model.ClientCollection m_ClientCollection;

        public ProviderEntry(YellowstonePathology.Business.Domain.Physician physician, YellowstonePathology.Business.Persistence.ObjectTracker objectTracker)
        {                        
            this.m_Physician = physician;
            this.m_ObjectTracker = objectTracker;

			this.m_PhysicianClientView = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianClientView(this.m_Physician.PhysicianId);
			this.m_HpvStandingOrders = new YellowstonePathology.Business.Client.Model.StandingOrderCollection();
			this.m_ClientCollection = new YellowstonePathology.Business.Client.Model.ClientCollection();
            
            InitializeComponent();

            this.DataContext = this;
        }

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
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

		public YellowstonePathology.Business.Client.Model.ClientCollection ClientCollection
		{
			get { return this.m_ClientCollection; }
		}

		private void ButtonOK_Click(object sender, RoutedEventArgs e)
		{
            this.m_ObjectTracker.SubmitChanges(this.m_Physician);
			Close();
		}		

		private void ButtonAddToClient_Click(object sender, RoutedEventArgs e)
		{
			if (this.ListBoxClientSelection.SelectedItem != null)
			{
				YellowstonePathology.Business.Client.Model.Client client = (YellowstonePathology.Business.Client.Model.Client)this.ListBoxClientSelection.SelectedItem;
				if (this.m_PhysicianClientView.ClientExists(client.ClientId) == false)
				{
					string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
					YellowstonePathology.Business.Domain.PhysicianClient physicianClient = new Business.Domain.PhysicianClient(objectId, this.m_PhysicianClientView.PhysicianId, client.ClientId);
					YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();
					objectTracker.RegisterRootInsert(physicianClient);
					objectTracker.SubmitChanges(physicianClient);
					this.m_PhysicianClientView.Clients.Add(client);
				}
			}
		}

		private void ButtonRemoveFromClient_Click(object sender, RoutedEventArgs e)
		{
			if(this.ListBoxClients.SelectedItem != null)
			{
				YellowstonePathology.Business.Client.Model.Client client = (YellowstonePathology.Business.Client.Model.Client)this.ListBoxClients.SelectedItem;
				YellowstonePathology.Business.Domain.PhysicianClient physicianClient = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianClient(this.m_PhysicianClientView.PhysicianId, client.ClientId);
				YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();
				objectTracker.RegisterRootDelete(physicianClient);
				objectTracker.SubmitChanges(physicianClient);
				this.m_PhysicianClientView.Clients.Remove(client);
			}
		}

		private void TextBoxClientName_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (this.TextBoxClientName.Text.Length > 0)
			{
				this.m_ClientCollection = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientsByClientName(this.TextBoxClientName.Text);
				NotifyPropertyChanged("ClientCollection");
			}
		}		
		
	}
}
