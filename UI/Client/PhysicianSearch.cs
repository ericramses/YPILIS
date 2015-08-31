using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace YellowstonePathology.UI.Client
{
    public partial class PhysicianSearch : Form
    {
        private YellowstonePathology.Business.Domain.Physician m_SelectedPhysician;
        private YellowstonePathology.Business.Client.Model.PhysicianNameViewCollection m_PhysicianNameViewCollection;

        public PhysicianSearch()
        {
            InitializeComponent();
        }

        public YellowstonePathology.Business.Domain.Physician SelectedPhysician
        {
            get { return this.m_SelectedPhysician; }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void textBoxPhysicianName_TextChanged(object sender, EventArgs e)
        {
            this.GetPhysicianList();
        }

        private void GetPhysicianList()
        {
            if (this.textBoxPhysicianName.Text.Length > 0)
            {
				this.m_PhysicianNameViewCollection = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianNameViewCollectionByPhysicianLastNameV2(this.textBoxPhysicianName.Text);
                this.PopulatePhysicianList();
            }
        }

        private void PopulatePhysicianList()
        {
            this.listViewPhysician.Items.Clear();

			foreach (YellowstonePathology.Business.Client.Model.PhysicianNameView physicianNameView in this.m_PhysicianNameViewCollection)
            {
                ListViewItem lvi = this.listViewPhysician.Items.Add(physicianNameView.PhysicianId.ToString());
                lvi.SubItems.Add(physicianNameView.LastName);
                lvi.SubItems.Add(physicianNameView.FirstName);
                lvi.SubItems.Add(this.GetPhoneNumber(physicianNameView.HomeBasePhone));
                lvi.SubItems.Add(this.GetPhoneNumber(physicianNameView.HomeBaseFax));
            }
        }

        public string GetPhoneNumber(string number)
        {
            string result = null;
            if (string.IsNullOrEmpty(number) == false)
            {
				if (number != "0")
				{
					if (number.Length < 10)
					{
						result = number;
					}
					else
					{
						string parsedNumber = "(" + number.Substring(0, 3) + ")" + number.Substring(3, 3) + "-" + number.Substring(6, 4);
						result = parsedNumber;
					}
				}
            }
            return result;
        }        

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void listViewPhysician_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int physicianId = Convert.ToInt32(this.listViewPhysician.SelectedItems[0].Text);
			this.m_SelectedPhysician = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianByPhysicianId(physicianId);
			YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();
			objectTracker.RegisterObject(this.m_SelectedPhysician);
            PhysicianEntry physician = new PhysicianEntry(this.m_SelectedPhysician, objectTracker);
            physician.ShowDialog();
			this.GetPhysicianList();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
			MessageBox.Show("Please contact IT to delete a Physician.", "This function not implemented", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {            
			if (this.textBoxPhysicianName.Text.Length == 0)
			{
				MessageBox.Show("Please enter the new physicians last name.");
				return;
			}
			string newPhysicianLastName = this.textBoxPhysicianName.Text;
			DialogResult result = MessageBox.Show("Add new Physician: " + newPhysicianLastName, "Add Physician", MessageBoxButtons.OKCancel);
			if (result == DialogResult.OK)
			{
				string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
				int physicianId = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetLargestPhysicianId() + 1;
				YellowstonePathology.Business.Domain.Physician physician = new Business.Domain.Physician(objectId, objectId, physicianId, newPhysicianLastName, string.Empty);

				YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();
				objectTracker.RegisterRootInsert(physician);

				Client.PhysicianEntry physicianEntry = new PhysicianEntry(physician, objectTracker);
				physicianEntry.ShowDialog();
			}
			this.GetPhysicianList();            
        }

        private void listViewPhysician_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int physicianId = Convert.ToInt32(this.listViewPhysician.SelectedItems[0].Text);
            //this.m_SelectedPhysician = this.m_PhysicianClientGateway.GetPhysicianByPhysicianId(physicianId);
        } 
    }
}