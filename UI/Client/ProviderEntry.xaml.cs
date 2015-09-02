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
		private YellowstonePathology.Business.Client.Model.StandingOrderCollection m_HPV1618StandingOrderCollection;
		private YellowstonePathology.Business.View.PhysicianClientView m_PhysicianClientView;
		private YellowstonePathology.Business.Client.Model.ClientCollection m_ClientCollection;
		private List<YellowstonePathology.Business.Client.Model.PhysicianClientDistributionView> m_PhysicianClientDistributionViewList;
		string m_PhysicianClientId;

        public ProviderEntry(YellowstonePathology.Business.Domain.Physician physician, YellowstonePathology.Business.Persistence.ObjectTracker objectTracker)
        {                        
            this.m_Physician = physician;
            this.m_ObjectTracker = objectTracker;

			this.m_PhysicianClientView = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianClientView(this.m_Physician.ObjectId);
			this.m_HpvStandingOrders = YellowstonePathology.Business.Client.Model.StandingOrderCollection.GetHPVStandingOrders();
			this.m_HPV1618StandingOrderCollection = YellowstonePathology.Business.Client.Model.StandingOrderCollection.GetHPV1618StandingOrders();
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

		public YellowstonePathology.Business.Client.Model.StandingOrderCollection HpvStandingOrders
		{
			get { return this.m_HpvStandingOrders; }
		}

		public YellowstonePathology.Business.Client.Model.StandingOrderCollection HPV1618StandingOrderCollection
		{
			get { return this.m_HPV1618StandingOrderCollection; }
		}


		public ObservableCollection<YellowstonePathology.Business.Client.Model.Client> Clients
		{
			get { return this.m_PhysicianClientView.Clients; }
		}

		public YellowstonePathology.Business.Client.Model.ClientCollection ClientCollection
		{
			get { return this.m_ClientCollection; }
		}

		public List<YellowstonePathology.Business.Client.Model.PhysicianClientDistributionView> PhysicianClientDistributionViewList
		{
			get { return this.m_PhysicianClientDistributionViewList; }
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
					this.m_ObjectTracker.SubmitChanges(this.m_Physician);

					string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
					YellowstonePathology.Business.Domain.PhysicianClient physicianClient = new Business.Domain.PhysicianClient(objectId, objectId, this.m_PhysicianClientView.PhysicianId, this.m_PhysicianClientView.ObjectId, client.ClientId);
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
				YellowstonePathology.Business.Domain.PhysicianClient physicianClient = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianClient(this.m_PhysicianClientView.ObjectId, client.ClientId);
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

		private void ButtonAddToDistribution_Click(object sender, RoutedEventArgs e)
		{
			if (this.ListBoxNewDistributionSelection.SelectedItem != null)
			{
				YellowstonePathology.Business.Client.Model.Client client = (YellowstonePathology.Business.Client.Model.Client)this.ListBoxNewDistributionSelection.SelectedItem;
				if (this.ListBoxClientMembership.SelectedItems.Count != 0)
				{
					YellowstonePathology.Business.Domain.PhysicianClient physicianClient = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianClient(this.m_Physician.ObjectId, client.ClientId);
					string physicianClientId = physicianClient.PhysicianClientId;
					foreach (YellowstonePathology.Business.Client.Model.PhysicianClientDistributionView physicianClientDistributionView in this.m_PhysicianClientDistributionViewList)
					{
						if (physicianClientDistributionView.PhysicianClientDistribution.DistributionID == physicianClientId)
						{
							MessageBox.Show("The item has already been added.");
							return;
						}
					}

					string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
					YellowstonePathology.Business.Client.Model.PhysicianClientDistribution physicianClientDistribution = new Business.Client.Model.PhysicianClientDistribution(objectId, this.m_PhysicianClientId, physicianClientId);

					YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();
					objectTracker.RegisterRootInsert(physicianClientDistribution);
					objectTracker.SubmitChanges(physicianClientDistribution);

					this.m_PhysicianClientDistributionViewList = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianClientDistributionsV2(this.m_PhysicianClientId);
					this.NotifyPropertyChanged("PhysicianClientDistributionViewList");
				}
			}
		}

		private void ButtonRemoveFromDistribution_Click(object sender, RoutedEventArgs e)
		{
			if (this.ListBoxDistributionSelection.SelectedItem != null)
			{
				MessageBoxResult result = MessageBox.Show("Remove the selected item?", "Remove", MessageBoxButton.OKCancel);
				if (result == MessageBoxResult.OK)
				{
					YellowstonePathology.Business.Client.Model.PhysicianClientDistributionView physicianClientDistributionView = (YellowstonePathology.Business.Client.Model.PhysicianClientDistributionView)this.ListBoxDistributionSelection.SelectedItem;
					YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();
					objectTracker.RegisterRootDelete(physicianClientDistributionView.PhysicianClientDistribution);
					objectTracker.SubmitChanges(physicianClientDistributionView.PhysicianClientDistribution);

					this.m_PhysicianClientDistributionViewList = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianClientDistributionsV2(this.m_PhysicianClientId);
					this.NotifyPropertyChanged("PhysicianClientDistributionViewList");
				}
			}
		}

		private void ListBoxClientMembership_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if(this.ListBoxClientMembership.SelectedItem != null)
			{
				YellowstonePathology.Business.Client.Model.Client client = (YellowstonePathology.Business.Client.Model.Client)this.ListBoxClientMembership.SelectedItem;
				YellowstonePathology.Business.Domain.PhysicianClient physicianClient = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianClient(this.m_Physician.ObjectId, client.ClientId);
				this.m_PhysicianClientId = physicianClient.PhysicianClientId;
				this.m_PhysicianClientDistributionViewList = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianClientDistributionsV2(this.m_PhysicianClientId);
				this.NotifyPropertyChanged("PhysicianClientDistributionViewList");
			}
		}
	}
}
