using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace YellowstonePathology.UI.Client
{
    public partial class PhysicianEntry : Form
    {
        private YellowstonePathology.Business.Domain.Physician m_Physician;
        private YellowstonePathology.Business.Domain.ClientCollection m_ClientCollection;
		private YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;

        private ArrayList m_ControlList;
        private Binding m_HPVStandingOrderBinding;
        private Binding m_HPV1618StandingOrderBinding;

        private YellowstonePathology.Business.Client.Model.StandingOrderCollection m_HPV1618StandingOrderCollection;
        private YellowstonePathology.Business.Client.Model.StandingOrderCollection m_HPVStandingOrderCollection;

        public PhysicianEntry(YellowstonePathology.Business.Domain.Physician physician, YellowstonePathology.Business.Persistence.ObjectTracker objectTracker)
        {
            this.m_Physician = physician;
			this.m_ObjectTracker = objectTracker;
			this.m_ClientCollection = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientsByProviderId(this.m_Physician.ObjectId);
            this.m_ControlList = new ArrayList();

            this.m_HPV1618StandingOrderCollection = YellowstonePathology.Business.Client.Model.StandingOrderCollection.GetHPV1618StandingOrders();
            this.m_HPVStandingOrderCollection = YellowstonePathology.Business.Client.Model.StandingOrderCollection.GetHPVStandingOrders();
            
            InitializeComponent();            
        }        

        private void Physician_Load(object sender, EventArgs e)
        {            
            this.PopulateClientList();
            this.Text = "Physician - " + this.m_Physician.FirstName + " " + this.m_Physician.LastName;            
            this.SetFormBinding();
            this.SetControlList();
            this.SetHPVStandingOrderComboBox();
            this.SetHPV1618StandingOrderComboBox();
			this.LockFormFields(this.m_ControlList);
        }

        private void SetHPVStandingOrderComboBox()
        {            
            this.comboBoxHPVStandingOrder.DataSource = this.m_HPVStandingOrderCollection;
            this.comboBoxHPVStandingOrder.DisplayMember = "Description";
            this.comboBoxHPVStandingOrder.ValueMember = "StandingOrderCode";            

            this.comboBoxHPVStandingOrder.DataBindings.Clear();
            Binding comboBoxHPVStandingOrderBinding = new Binding("SelectedValue", this.m_Physician, "HPVStandingOrderCode");
            this.comboBoxHPVStandingOrder.DataBindings.Add(comboBoxHPVStandingOrderBinding);    
        }

        private void SetHPV1618StandingOrderComboBox()
        {
            this.comboBoxHPV1618StandingOrder.DataSource = this.m_HPV1618StandingOrderCollection;
            this.comboBoxHPV1618StandingOrder.DisplayMember = "Description";
            this.comboBoxHPV1618StandingOrder.ValueMember = "StandingOrderCode";                        

            this.comboBoxHPV1618StandingOrder.DataBindings.Clear();
            Binding comboBoxHPV1618StandingOrderBinding = new Binding("SelectedValue", this.m_Physician, "HPV1618StandingOrderCode");
            this.comboBoxHPV1618StandingOrder.DataBindings.Add(comboBoxHPV1618StandingOrderBinding);              
        }        

        private void SetControlList()
        {
            this.m_ControlList.Add(this.textBoxFirstName);
            this.m_ControlList.Add(this.textBoxLastName);
			this.m_ControlList.Add(this.textBoxInitial);
			this.m_ControlList.Add(this.textBoxDisplayName);
			this.m_ControlList.Add(this.textBoxNPI);
			this.m_ControlList.Add(this.checkBoxActive);
            this.m_ControlList.Add(this.checkBoxKRASBRAFStandingOrder);            

            this.m_ControlList.Add(this.buttonAdd);            
            this.m_ControlList.Add(this.buttonHomeBase);
            this.m_ControlList.Add(this.buttonRemove);
            this.m_ControlList.Add(this.comboBoxHPVStandingOrder);

            this.m_ControlList.Add(this.textBoxMDFirstName);
            this.m_ControlList.Add(this.textBoxMDLastName);

            this.m_ControlList.Add(this.comboBoxHPVStandingOrder);
            this.m_ControlList.Add(this.comboBoxHPV1618StandingOrder);

            this.m_ControlList.Add(this.TextBoxNotificationEmail);
            this.m_ControlList.Add(this.checkBoxSendNotifications);
        }

        private void SetFormBinding()
        {            
            this.labelPhysicianId.DataBindings.Clear();
            Binding physicianIdBinding = new Binding("Text", this.m_Physician, "PhysicianId");
            this.labelPhysicianId.DataBindings.Add(physicianIdBinding);

            this.textBoxFirstName.DataBindings.Clear();
            Binding firstNameBinding = new Binding("Text", this.m_Physician, "FirstName");
            this.textBoxFirstName.DataBindings.Add(firstNameBinding);

            this.textBoxLastName.DataBindings.Clear();
            Binding lastNameBinding = new Binding("Text", this.m_Physician, "LastName");
            this.textBoxLastName.DataBindings.Add(lastNameBinding);

			this.textBoxInitial.DataBindings.Clear();
			Binding initialBinding = new Binding("Text", this.m_Physician, "MiddleInitial");
			this.textBoxInitial.DataBindings.Add(initialBinding);

			this.textBoxDisplayName.DataBindings.Clear();
			Binding displayNameBinding = new Binding("Text", this.m_Physician, "DisplayName");
			this.textBoxDisplayName.DataBindings.Add(displayNameBinding);

			this.textBoxNPI.DataBindings.Clear();
			Binding NpiBinding = new Binding("Text", this.m_Physician, "Npi");
			this.textBoxNPI.DataBindings.Add(NpiBinding);            

            this.checkBoxActive.DataBindings.Clear();
            Binding activeBinding = new Binding("Checked", this.m_Physician, "Active");
            this.checkBoxActive.DataBindings.Add(activeBinding);

            this.checkBoxKRASBRAFStandingOrder.DataBindings.Clear();
            Binding KRASBRAFBinding = new Binding("Checked", this.m_Physician, "KRASBRAFStandingOrder");
            this.checkBoxKRASBRAFStandingOrder.DataBindings.Add(KRASBRAFBinding);            

            this.textBoxMDFirstName.DataBindings.Clear();
            Binding mdfirstNameBinding = new Binding("Text", this.m_Physician, "MDFirstName");
            this.textBoxMDFirstName.DataBindings.Add(mdfirstNameBinding);

            this.textBoxMDLastName.DataBindings.Clear();
            Binding mdlastNameBinding = new Binding("Text", this.m_Physician, "MDLastName");
            this.textBoxMDLastName.DataBindings.Add(mdlastNameBinding);

            this.TextBoxNotificationEmail.DataBindings.Clear();
            Binding notificationEmail = new Binding("Text", this.m_Physician, "PublishNotificationEmailAddress");
            this.TextBoxNotificationEmail.DataBindings.Add(notificationEmail);

            this.checkBoxSendNotifications.DataBindings.Clear();
            Binding sendNotificationsBinding = new Binding("Checked", this.m_Physician, "SendPublishNotifications");
            this.checkBoxSendNotifications.DataBindings.Add(sendNotificationsBinding);
        }

        private void PopulateClientList()
        {
            this.listViewClientList.Items.Clear();            
            foreach (YellowstonePathology.Business.Client.Model.Client client in this.m_ClientCollection)
            {
                ListViewItem lvi = this.listViewClientList.Items.Add(client.ClientId.ToString());
				lvi.SubItems.Add(client.ClientId.ToString());
				lvi.SubItems.Add(client.ClientName);
                if (this.m_Physician.HomeBaseClientId == client.ClientId)
                {
                    lvi.SubItems.Add("\u2713");
                }
                else
                {
                    lvi.SubItems.Add(string.Empty);
                }
			}

            if (this.m_ClientCollection.Count == 0)
            {
                this.listViewClientList.Items.Add("No Items Found");
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {            
            ClientSearch clientSearch = new ClientSearch();
            DialogResult result = clientSearch.ShowDialog();
            if (result == DialogResult.OK)
            {
                long clientId = clientSearch.ClientID;
                if (this.IsClientInClient(clientId) == false)
                {
					this.Save();

					string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
					YellowstonePathology.Business.Domain.PhysicianClient physicianClient = new Business.Domain.PhysicianClient(objectId, objectId, this.m_Physician.PhysicianId, this.m_Physician.ObjectId, clientSearch.ClientID);
					YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();
					objectTracker.RegisterRootInsert(physicianClient);
					objectTracker.SubmitChanges(physicianClient);

					this.m_ClientCollection = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientsByProviderId(this.m_Physician.ObjectId);
					this.PopulateClientList();
					
					
					this.PopulateClientList();
                    MessageBox.Show("The selected physician was added.");
                }
                else
                {
                    MessageBox.Show("Client already exist.");
                }
            }
        }

        private bool IsClientInClient(long clientId)
        {
            foreach (ListViewItem lvi in this.listViewClientList.Items)
            {
                if (lvi.SubItems.Count == 1) return false;
                int currentID = 0;
                if (Int32.TryParse(lvi.SubItems[1].Text, out currentID) == true)
                {
                    if (currentID == clientId)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Remove this client?", "Remove Client.", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                if (this.listViewClientList.SelectedItems.Count != 0)
                {
                    int clientId = 0;
                    if (Int32.TryParse(this.listViewClientList.SelectedItems[0].Text, out clientId) == true)
                    {
						YellowstonePathology.Business.Domain.PhysicianClient physicianClient = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianClient(this.m_Physician.ObjectId, clientId);
						YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();
						objectTracker.RegisterRootDelete(physicianClient);
						objectTracker.SubmitChanges(physicianClient);

						this.m_ClientCollection = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientsByProviderId(this.m_Physician.ObjectId);
						this.PopulateClientList();                
					
					
					}
                    MessageBox.Show("The client has been removed.");
                }                
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
			this.Save();
            this.DialogResult = DialogResult.OK;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }        

        private void buttonHomeBase_Click(object sender, EventArgs e)
        {
			if (this.listViewClientList.SelectedItems.Count != 0)
			{
				int clientID = Convert.ToInt32(this.listViewClientList.SelectedItems[0].SubItems[1].Text);
				this.m_Physician.HomeBaseClientId = clientID;
				this.PopulateClientList();
			}
        }

        private void buttonUnlock_Click(object sender, EventArgs e)
        {            
			this.UnlockFormFields(this.m_ControlList);
		}

        private void comboBoxHpvStandingOrderInstructions_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(this.comboBoxHPVStandingOrder.SelectedValue.ToString());
            //this.textBoxHPVStandingOrder.Text = this.comboBoxHPVStandingOrder.SelectedIndex.ToString();
            //this.m_HPVStandingOrderBinding.WriteValue();
        }

		public void UnlockFormFields(ArrayList controlList)
		{
			foreach (Control ctrl in controlList)
			{
				ctrl.Enabled = true;
			}
		}

		public void LockFormFields(ArrayList controlList)
		{
			foreach (Control ctrl in controlList)
			{
				ctrl.Enabled = false;
			}
		}

		private void Save()
		{
			this.m_ObjectTracker.SubmitChanges(this.m_Physician);
		}                
	}
}