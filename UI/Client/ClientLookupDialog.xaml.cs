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
using System.ComponentModel;

namespace YellowstonePathology.UI.Client
{
	/// <summary>
	/// Interaction logic for ClientSearchDialog.xaml
	/// </summary>
	public partial class ClientLookupDialog : Window, INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private YellowstonePathology.Business.Client.Model.ClientCollection m_ClientCollection;

		public ClientLookupDialog()
		{
			this.m_ClientCollection = new Business.Client.Model.ClientCollection();
			InitializeComponent();
			DataContext = this;
		}

		public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}        

		public YellowstonePathology.Business.Client.Model.ClientCollection ClientCollection
		{
			get { return this.m_ClientCollection; }
		}

		private void TextBoxClientName_KeyUp(object sender, KeyEventArgs e)
		{
			if (!string.IsNullOrEmpty(this.TextBoxClientName.Text))
			{
				this.m_ClientCollection = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientsByClientName(this.TextBoxClientName.Text);
				NotifyPropertyChanged("ClientCollection");
			}
		}

		private void ListBoxClients_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			if (this.ListBoxClients.SelectedItem != null)
			{
				YellowstonePathology.Business.Client.Model.Client client = (YellowstonePathology.Business.Client.Model.Client)this.ListBoxClients.SelectedItem;
				YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();
				objectTracker.RegisterObject(client);
				ClientEntryV2 clientEntry = new ClientEntryV2(client, objectTracker);
				clientEntry.ShowDialog();
			}
		}

		private void ButtonNew_Click(object sender, RoutedEventArgs e)
		{
			if (this.TextBoxClientName.Text.Length == 0)
			{
				MessageBox.Show("Please enter the new clients name.");
				return;
			}

			string newClientName = this.TextBoxClientName.Text;
			MessageBoxResult result = MessageBox.Show("Add new client: " + newClientName, "Add Client", MessageBoxButton.OKCancel);
			if (result == MessageBoxResult.OK)
			{
				string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
				int clientId = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetLargestClientId() + 1;
				YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();
				YellowstonePathology.Business.Client.Model.Client client = new YellowstonePathology.Business.Client.Model.Client(objectId, newClientName, clientId);
				objectTracker.RegisterRootInsert(client);
				client.ClientName = newClientName;
				client.ObjectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
				ClientEntryV2 clientEntryDialog = new ClientEntryV2(client, objectTracker);
				clientEntryDialog.ShowDialog();
			}

			this.m_ClientCollection = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientsByClientName(this.TextBoxClientName.Text);
			NotifyPropertyChanged("ClientCollection");
		}

		private void ButtonEnvelope_Click(object sender, RoutedEventArgs e)
		{
			if (this.ListBoxClients.SelectedItems.Count != 0)
			{
				YellowstonePathology.Business.Client.Model.Client client = (YellowstonePathology.Business.Client.Model.Client)this.ListBoxClients.SelectedItem;
				Envelope envelope = new Envelope();
				string address = client.Address;
				string name = client.ClientName;
				string city = client.City;
				string state = client.State;
				string zip = client.ZipCode;
				envelope.PrintEnvelope(name, address, city, state, zip);
			}
		}

		private void ButtonRequisitions_Click(object sender, RoutedEventArgs e)
		{
			if (this.ListBoxClients.SelectedItems.Count != 0)
			{
				YellowstonePathology.Business.Client.Model.Client client = (YellowstonePathology.Business.Client.Model.Client)this.ListBoxClients.SelectedItem;
				Client.RequisitionOptionsDialog requisitionOptionsDialog = new RequisitionOptionsDialog(client.ClientId, client.ClientName);
				requisitionOptionsDialog.ShowDialog();
			}
		}

		private void ButtonOK_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
