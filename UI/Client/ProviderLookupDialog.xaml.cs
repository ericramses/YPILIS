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

			YellowstonePathology.Business.Domain.Physician physician = new Business.Domain.Physician(objectId, "New Physician", "New Physician");
            objectTracker.RegisterRootInsert(physician);
			ProviderEntry providerEntry = new ProviderEntry(physician, objectTracker);
			providerEntry.ShowDialog();
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
			if (this.ListBoxProviders.SelectedItem != null)
			{                
				YellowstonePathology.Business.Domain.Physician physician = (YellowstonePathology.Business.Domain.Physician)this.ListBoxProviders.SelectedItem;
				YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();
                objectTracker.RegisterObject(physician);

				ProviderEntry providerEntry = new ProviderEntry(physician, objectTracker);
				providerEntry.ShowDialog();
			}
		}

		private void ButtonOK_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

        private void DoClientSearch(string clientName)
        {            
			this.m_ClientCollection = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientsByClientName(clientName);
            NotifyPropertyChanged("ClientCollection");
            this.ListBoxProviders.SelectedIndex = -1;
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
			this.ListBoxProviders.SelectedIndex = -1;
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
            if (this.ListBoxClients.SelectedItem != null)
            {
				YellowstonePathology.Business.Client.Model.Client client = (YellowstonePathology.Business.Client.Model.Client)this.ListBoxClients.SelectedItem;
				YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();
                objectTracker.RegisterObject(client);
				ClientEntry clientEntry = new ClientEntry(client, objectTracker);
				clientEntry.ShowDialog();
            }
        }		
	}
}
