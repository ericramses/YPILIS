using System;
using System.Collections.Generic;
using System.ComponentModel;
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
	/// <summary>
	/// Interaction logic for ProviderLookupDialog.xaml
	/// </summary>
	public partial class ProviderLookupDialog : Window, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private YellowstonePathology.Business.Domain.PhysicianCollection m_PhysicianCollection;
		private YellowstonePathology.Business.Client.Model.ClientCollection m_ClientCollection;

		public ProviderLookupDialog()
		{
			InitializeComponent();
			DataContext = this;
		}

		public YellowstonePathology.Business.Domain.PhysicianCollection ProviderCollection
		{
			get { return this.m_PhysicianCollection; }
			private set
			{
				this.m_PhysicianCollection = value;
				NotifyPropertyChanged("ProviderCollection");
			}
		}

		public YellowstonePathology.Business.Client.Model.ClientCollection ClientCollection
        {
            get { return this.m_ClientCollection; }
        }

		private void ButtonNewProvider_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();
			string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
			int physicianId = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetLargestPhysicianId() + 1;
			YellowstonePathology.Business.Domain.Physician physician = new Business.Domain.Physician(objectId, physicianId, "New Physician", "New Physician");
			objectTracker.RegisterRootInsert(physician);

			ProviderEntry providerEntry = new ProviderEntry(physician, objectTracker);
			providerEntry.ShowDialog();
		}

		private void ButtonNewClient_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();
			string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
			int clientId = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetLargestClientId() + 1;
			YellowstonePathology.Business.Client.Model.Client client = new YellowstonePathology.Business.Client.Model.Client(objectId, "New Client", clientId);
			objectTracker.RegisterRootInsert(client);

			ClientEntryV2 clientEntry = new ClientEntryV2(client, objectTracker);
			clientEntry.ShowDialog();
		}

		private void ButtonEnvelope_Click(object sender, RoutedEventArgs e)
		{
			if (this.ListViewClients.SelectedItems.Count != 0)
			{
				YellowstonePathology.Business.Client.Model.Client client = (YellowstonePathology.Business.Client.Model.Client)this.ListViewClients.SelectedItem;
				Envelope envelope = new Envelope();
				string address = client.Address;
				string name = client.ClientName;
				string city = client.City;
				string state = client.State;
				string zip = client.ZipCode;
				envelope.PrintEnvelope(name, address, city, state, zip);
			}
		}

		private void ButtonRequisition_Click(object sender, RoutedEventArgs e)
		{
			if (this.ListViewClients.SelectedItems.Count != 0)
			{
				YellowstonePathology.Business.Client.Model.Client client = (YellowstonePathology.Business.Client.Model.Client)this.ListViewClients.SelectedItem;
				Client.RequisitionOptionsDialog requisitionOptionsDialog = new RequisitionOptionsDialog(client.ClientId, client.ClientName);
				requisitionOptionsDialog.ShowDialog();
			}
		}

		private void ButtonOK_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void TextBoxProviderName_KeyUp(object sender, KeyEventArgs e)
		{
			if (!string.IsNullOrEmpty(this.TextBoxProviderName.Text))
			{
				this.DoProviderSearch();
			}
		}

        private void TextBoxClientName_KeyUp(object sender, KeyEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.TextBoxClientName.Text))
            {
                this.DoClientSearch(this.TextBoxClientName.Text);
            }
        }

		private void ListBoxProviders_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (this.ListViewProviders.SelectedItem != null)
			{
				YellowstonePathology.Business.Domain.Physician physician = (YellowstonePathology.Business.Domain.Physician)this.ListViewProviders.SelectedItem;
				YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();
                objectTracker.RegisterObject(physician);

				ProviderEntry providerEntry = new ProviderEntry(physician, objectTracker);
				providerEntry.ShowDialog();
			}
		}

        private void DoClientSearch(string clientName)
        {            
			this.m_ClientCollection = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientsByClientName(clientName);
            NotifyPropertyChanged("ClientCollection");
			this.ListViewProviders.SelectedIndex = -1;
        }

		private void DoProviderSearch()
		{
			string firstName = string.Empty;
			string lastName = string.Empty;
			string[] commaSplit = this.TextBoxProviderName.Text.Split(',');
			lastName = commaSplit[0].Trim();
			if (commaSplit.Length > 1)
			{
				firstName = commaSplit[1].Trim();
			}

			this.m_PhysicianCollection = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysiciansByName(firstName, lastName);
			NotifyPropertyChanged("ProviderCollection");
			this.ListViewProviders.SelectedIndex = -1;
		}

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}        

        private void ListBoxClients_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
			if (this.ListViewClients.SelectedItem != null)
            {
				YellowstonePathology.Business.Client.Model.Client client = (YellowstonePathology.Business.Client.Model.Client)this.ListViewClients.SelectedItem;
				YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();
                objectTracker.RegisterObject(client);
				ClientEntryV2 clientEntry = new ClientEntryV2(client, objectTracker);
				clientEntry.ShowDialog();
            }
        }
	}
}
