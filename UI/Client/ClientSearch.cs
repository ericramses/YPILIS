using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace YellowstonePathology.UI.Client
{   
    public partial class ClientSearch : Form
    {        
        public int ClientID = 0;
		private YellowstonePathology.Business.View.ClientSearchView m_ClientSearchView;

        public ClientSearch()
        {            
			this.m_ClientSearchView = new Business.View.ClientSearchView();
            InitializeComponent();
        }

        private void textBoxClientName_TextChanged(object sender, EventArgs e)
        {
            this.SetListBoxData();
        }

        private void SetListBoxData()
        {
            if (this.textBoxClientName.Text.Length > 0)
            {
				this.m_ClientSearchView = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientSearchViewByClientName(this.textBoxClientName.Text);
                this.populateClientList();
            }
        }
        
        private void GetClientList(string clientName)
        {
        }

        private void populateClientList()
        {
            this.listViewClient.Items.Clear();
			foreach (YellowstonePathology.Business.View.ClientSearchViewItem clientSearchViewItem in this.m_ClientSearchView)
			{
				ListViewItem lvi = this.listViewClient.Items.Add(clientSearchViewItem.ClientId.ToString());
				lvi.SubItems.Add(clientSearchViewItem.ClientName);


				if (!string.IsNullOrEmpty(clientSearchViewItem.Telephone))
				{
					if (clientSearchViewItem.Telephone.Length == 10)
					{                        
						lvi.SubItems.Add("(" + clientSearchViewItem.Telephone.Substring(0, 3) + ") " + clientSearchViewItem.Telephone.Substring(3, 3) + "-" + clientSearchViewItem.Telephone.Substring(6, 4));
					}
					else
					{
						lvi.SubItems.Add(clientSearchViewItem.Telephone);
					}
				}
				else
				{
					lvi.SubItems.Add("None");
				}

				if (!string.IsNullOrEmpty(clientSearchViewItem.Fax))
				{
					if (clientSearchViewItem.Fax.Length == 10)
					{
						lvi.SubItems.Add("(" + clientSearchViewItem.Fax.Substring(0, 3) + ") " + clientSearchViewItem.Fax.Substring(3, 3) + "-" + clientSearchViewItem.Fax.Substring(6, 4));
					}
					else
					{
						lvi.SubItems.Add(clientSearchViewItem.Fax);
					}
				}
				else
				{
					lvi.SubItems.Add("None");
				}
			}
        }     

        private void listViewClient_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int clientId = Convert.ToInt32(this.listViewClient.SelectedItems[0].Text);

			YellowstonePathology.Business.Client.Model.Client client = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientByClientId(clientId);
			YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();
            objectTracker.RegisterObject(client);

            ClientEntry clientEntryDialog = new ClientEntry(client, objectTracker);

            clientEntryDialog.ShowDialog();            
            this.SetListBoxData();            
        }

        public int GetSelectedClientId()
        {
            int clientId = 0;
            if (this.listViewClient.SelectedItems != null)
            {
                clientId = Convert.ToInt32(this.listViewClient.SelectedItems[0].Text);
            }
            return clientId;
        }

        public string GetSelectedClientName()
        {
            string clientName = string.Empty;
            if (this.listViewClient.SelectedItems != null)
            {
                clientName = this.listViewClient.SelectedItems[0].SubItems[1].Text;
            }
            return clientName;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (this.listViewClient.SelectedItems.Count != 0)
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                DialogResult = DialogResult.Cancel;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }        

        private void ClientSearch_Activated(object sender, EventArgs e)
        {
            this.textBoxClientName.Focus();
        }

        private void buttonEnvelope_Click(object sender, EventArgs e)
        {
            if (this.listViewClient.SelectedItems.Count != 0)
            {
                int clientId = Convert.ToInt32(this.listViewClient.SelectedItems[0].Text);
				YellowstonePathology.Business.Client.Model.Client client = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientByClientId(clientId);
				Envelope envelope = new Envelope();
                string address = client.Address;
                string name = client.ClientName;
                string city = client.City;
                string state = client.State;
                string zip = client.ZipCode;
                envelope.PrintEnvelope(name, address, city, state, zip);
            }            
        }

        private void listViewClient_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.listViewClient.Items.Count != 0)
            {
                this.ClientID = Convert.ToInt32(this.listViewClient.SelectedItems[0].Text);
            }            
        }

        private void buttonNewClient_Click(object sender, EventArgs e)
        {            
            if (this.textBoxClientName.Text.Length == 0)
            {
                MessageBox.Show("Please enter the new clients name.");
                return;
            }

            string newClientName = this.textBoxClientName.Text;
            DialogResult result = MessageBox.Show("Add new client: " + newClientName, "Add Client", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
				string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
				//int clientId = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetLargestClientId() + 1;
				YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();
				YellowstonePathology.Business.Client.Model.Client client = new YellowstonePathology.Business.Client.Model.Client(objectId, newClientName);
				//YellowstonePathology.Business.Client.Model.Client client = new YellowstonePathology.Business.Client.Model.Client(objectId, newClientName, clientId);
				objectTracker.RegisterRootInsert(client);
                ClientEntry clientEntryDialog = new ClientEntry(client, objectTracker);
                clientEntryDialog.ShowDialog();                
            }
            this.SetListBoxData();            
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
			MessageBox.Show("Please contact IT to delete a client.", "This function not implemented", MessageBoxButtons.OK, MessageBoxIcon.Information);            
        }

        private void buttonRequisitions_Click(object sender, EventArgs e)
        {
            if (this.listViewClient.SelectedItems.Count != 0)
            {
                int clientId = Convert.ToInt32(this.listViewClient.SelectedItems[0].Text);
				string clientName = this.listViewClient.SelectedItems[0].SubItems[1].Text;
				Client.RequisitionOptionsDialog requisitionOptionsDialog = new RequisitionOptionsDialog(clientId, clientName);
				requisitionOptionsDialog.ShowDialog();
            }
        }
    }
}