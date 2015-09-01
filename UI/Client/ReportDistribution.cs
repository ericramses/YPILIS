using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace YellowstonePathology.UI.Client
{
    public partial class ReportDistribution : Form
    {
        string m_PhysicianClientID;

		private YellowstonePathology.Business.Client.Model.PhysicianClientNameCollection m_PhysicianClientNameCollection;
		private List<YellowstonePathology.Business.Client.Model.PhysicianClientDistributionView> m_PhysicianClientDistributionViewList;

        public ReportDistribution(string physicianClientID)
        {
            this.m_PhysicianClientID = physicianClientID;
            InitializeComponent();
        }

        private void ReportDistribution_Load(object sender, EventArgs e)
        {
			this.m_PhysicianClientNameCollection = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianClientNameCollectionV2(this.m_PhysicianClientID);
			this.m_PhysicianClientDistributionViewList = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianClientDistributionsV2(this.m_PhysicianClientID);

            this.Text = this.GetFormTitle();            

            this.PopulateDistributionList();
            this.PopulatePhysicianClientList();
        }

        private string GetFormTitle()
        {
			foreach (YellowstonePathology.Business.Client.Model.PhysicianClientDistributionView physicianClientDistributionView in this.m_PhysicianClientDistributionViewList)
			{
				if (this.m_PhysicianClientID == physicianClientDistributionView.PhysicianClientDistribution.PhysicianClientID)
				{
					return "Report Distribution (" + physicianClientDistributionView.PhysicianName + " - " + physicianClientDistributionView.ClientName + ")";
				}
			}
			return "Report Distribution ";
		}

        private void PopulateDistributionList()
        {
			this.listViewDistribution.Items.Clear();
			foreach (YellowstonePathology.Business.Client.Model.PhysicianClientDistributionView physicianClientDistributionView in this.m_PhysicianClientDistributionViewList)
			{
				ListViewItem lvi = this.listViewDistribution.Items.Add(physicianClientDistributionView.PhysicianClientDistribution.PhysicianClientDistributionID.ToString());
				lvi.SubItems.Add(physicianClientDistributionView.PhysicianName);
				lvi.SubItems.Add(physicianClientDistributionView.ClientName);
			}
		}

        private void PopulatePhysicianClientList()
        {
			this.listViewPhysicianClient.Items.Clear();
			foreach (YellowstonePathology.Business.Client.Model.PhysicianClientName physicianClientName in this.m_PhysicianClientNameCollection)
			{
				ListViewItem lvi = this.listViewPhysicianClient.Items.Add(physicianClientName.PhysicianClientId.ToString());
				lvi.SubItems.Add(physicianClientName.PhysicianName);
				lvi.SubItems.Add(physicianClientName.ClientName);
			}
		}

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }        

        private void buttonAddOther_Click(object sender, EventArgs e)
        {            
			PhysicianClientSearch physicianClient = new PhysicianClientSearch();
			DialogResult result = physicianClient.ShowDialog();
			if (result == DialogResult.OK)
			{
				string distributionID = physicianClient.PhysicianClientID;

				string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
                YellowstonePathology.Business.Client.Model.PhysicianClientDistribution physicianClientDistribution = new Business.Client.Model.PhysicianClientDistribution(objectId, this.m_PhysicianClientID, distributionID);

				YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();
				objectTracker.RegisterRootInsert(physicianClientDistribution);
				objectTracker.SubmitChanges(physicianClientDistribution);

				this.m_PhysicianClientDistributionViewList = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianClientDistributionsV2(this.m_PhysicianClientID);
				this.PopulateDistributionList();
			}
		}

        private void buttonAdd_Click(object sender, EventArgs e)
        {
			if (this.listViewPhysicianClient.SelectedItems.Count != 0)
			{
				string physicianClientID = this.listViewPhysicianClient.SelectedItems[0].Text;
				foreach( YellowstonePathology.Business.Client.Model.PhysicianClientDistributionView physicianClientDistributionView in this.m_PhysicianClientDistributionViewList)
				{
					if (physicianClientDistributionView.PhysicianClientDistribution.DistributionID == physicianClientID)
					{
						MessageBox.Show("The item has already been added.");
						return;
					}
				}

				string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
                YellowstonePathology.Business.Client.Model.PhysicianClientDistribution physicianClientDistribution = new Business.Client.Model.PhysicianClientDistribution(objectId, this.m_PhysicianClientID, physicianClientID);

				YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();
				objectTracker.RegisterRootInsert(physicianClientDistribution);
				objectTracker.SubmitChanges(physicianClientDistribution);

				this.m_PhysicianClientDistributionViewList = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianClientDistributionsV2(this.m_PhysicianClientID);
				this.PopulateDistributionList();
			}
		}

        private void buttonRemove_Click(object sender, EventArgs e)
        {
			if (this.listViewDistribution.SelectedItems.Count != 0)
			{
				DialogResult result = MessageBox.Show("Remove the selected item?", "Remove", MessageBoxButtons.OKCancel);
				if (result == DialogResult.OK)
				{
					int physicianClientDistributionID = 0;
					bool converted = Int32.TryParse(this.listViewDistribution.SelectedItems[0].Text, out physicianClientDistributionID);
					if(converted == true)
					{
					foreach (YellowstonePathology.Business.Client.Model.PhysicianClientDistributionView physicianClientDistributionView in this.m_PhysicianClientDistributionViewList)
					{
						if (physicianClientDistributionView.PhysicianClientDistribution.PhysicianClientDistributionID == physicianClientDistributionID)
						{
							YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();
							objectTracker.RegisterRootDelete(physicianClientDistributionView.PhysicianClientDistribution);
							objectTracker.SubmitChanges(physicianClientDistributionView.PhysicianClientDistribution);

							this.m_PhysicianClientDistributionViewList = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianClientDistributionsV2(this.m_PhysicianClientID);
							this.PopulateDistributionList();
							break;
						}
					}
					}
				}
			}
		}        
    }
}