using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace YellowstonePathology.UI.Client
{
    public partial class PhysicianClientSearch : Form
    {
        public int PhysicianClientID = 0;
        public int ClientID = 0;
        public int PhysicianID = 0;
        public string ClientName = "";
        public string PhysicianName = "";
        public string FirstName = "";
        public string LastName = "";

		private YellowstonePathology.Business.Client.Model.PhysicianClientNameCollection m_PhysicianClientNameCollection;

        public PhysicianClientSearch()
        {
            this.m_PhysicianClientNameCollection = new Business.Client.Model.PhysicianClientNameCollection();
            InitializeComponent();
        }

        private void PhysicianClient_Load(object sender, EventArgs e)
        {
        }

        public void PopulatePhysicianClientList()
        {
			this.listViewPhysicianClient.Items.Clear();
			foreach (YellowstonePathology.Business.Client.Model.PhysicianClientName physicianClientName in this.m_PhysicianClientNameCollection)
			{
				ListViewItem lvi = this.listViewPhysicianClient.Items.Add(physicianClientName.PhysicianClientId.ToString());
				lvi.SubItems.Add(physicianClientName.ClientId.ToString());
				lvi.SubItems.Add(physicianClientName.PhysicianId.ToString());
				lvi.SubItems.Add(physicianClientName.ClientName);                
				lvi.SubItems.Add(physicianClientName.FirstName);
				lvi.SubItems.Add(physicianClientName.LastName);
			}
        }

        private void textBoxPhysicianName_TextChanged(object sender, EventArgs e)
        {
            this.SearchClientPhysician();
        }

        private void SearchClientPhysician()
        {
			if (string.IsNullOrEmpty(this.textBoxPhysicianName.Text) == false)
			{
				string[] spaceSplit = this.textBoxPhysicianName.Text.Split(' ');
				if (spaceSplit.Length > 1)
				{
					string clientName = spaceSplit[0];
					string physicianName = spaceSplit[1];
					if (physicianName.Length > 0)
					{
						this.m_PhysicianClientNameCollection = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianClientNameCollection(clientName, physicianName);
						this.PopulatePhysicianClientList();
					}
				}
			}
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {            
            if (this.listViewPhysicianClient.SelectedItems.Count != 0)
            {                
                this.PhysicianClientID = Convert.ToInt32(this.listViewPhysicianClient.SelectedItems[0].Text);
                this.ClientID = Convert.ToInt32(this.listViewPhysicianClient.SelectedItems[0].SubItems[1].Text);
                this.PhysicianID = Convert.ToInt32(this.listViewPhysicianClient.SelectedItems[0].SubItems[2].Text);                
                this.ClientName = this.listViewPhysicianClient.SelectedItems[0].SubItems[3].Text;
                this.FirstName = this.listViewPhysicianClient.SelectedItems[0].SubItems[4].Text;
                this.LastName = this.listViewPhysicianClient.SelectedItems[0].SubItems[5].Text;
                this.PhysicianName = this.listViewPhysicianClient.SelectedItems[0].SubItems[4].Text + ' ' +
                    this.listViewPhysicianClient.SelectedItems[0].SubItems[5].Text;
            }
            this.DialogResult = DialogResult.OK;            
        }

        private void buttonDistribution_Click(object sender, EventArgs e)
        {
            if (this.listViewPhysicianClient.SelectedItems.Count != 0)
            {
                int physicianClientID = Convert.ToInt32(this.listViewPhysicianClient.SelectedItems[0].Text);
				ReportDistribution reportDistribution = new ReportDistribution(physicianClientID);
                reportDistribution.ShowDialog();
            }
        }        
    }
}