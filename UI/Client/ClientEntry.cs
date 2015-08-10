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
    public partial class ClientEntry : Form
    {                
        const string NoPermissionMessage = "You do not have permissions to perform this action.";

        YellowstonePathology.Business.Client.Model.Client m_Client;        
        YellowstonePathology.Business.Persistence.ObjectTracker m_ObjectTracker;

        YellowstonePathology.Business.Domain.PhysicianCollection m_PhysicianCollection;

        ArrayList m_ControlList;
        Binding LongDistanceBinding;

        public ClientEntry(YellowstonePathology.Business.Client.Model.Client client, YellowstonePathology.Business.Persistence.ObjectTracker objectTracker)
        {
            this.m_Client = client;
            this.m_ObjectTracker = objectTracker;

            this.m_ControlList = new ArrayList();

			this.m_PhysicianCollection = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysiciansByClientId(this.m_Client.ClientId);            

            InitializeComponent();
        }

        private void Client_Load(object sender, EventArgs e)
        {
            this.SetFormBinding();
            this.PopulatePhysicianList();
            this.Text = "Client - " + this.m_Client.ClientName;
            this.SetControlList();
            LockFormFields(this.m_ControlList);
        }

        private void SetFormBinding()
        {
            this.labelClientID.DataBindings.Clear();
            Binding ClientIdBinding = new Binding("Text", this.m_Client, "ClientId");
            this.labelClientID.DataBindings.Add(ClientIdBinding);
            
            this.textBoxName.DataBindings.Clear();
            Binding NameBinding = new Binding("Text", this.m_Client, "ClientName");
            this.textBoxName.DataBindings.Add(NameBinding);

            this.textBoxAddress.DataBindings.Clear();
            Binding AddressBinding = new Binding("Text", this.m_Client, "Address");
            this.textBoxAddress.DataBindings.Add(AddressBinding);
            
            this.textBoxCity.DataBindings.Clear();
            Binding CityBinding = new Binding("Text", this.m_Client, "City");
            this.textBoxCity.DataBindings.Add(CityBinding);

            this.textBoxState.DataBindings.Clear();
            Binding StateBinding = new Binding("Text", this.m_Client, "State");
            this.textBoxState.DataBindings.Add(StateBinding);

            this.textBoxZip.DataBindings.Clear();
            Binding ZipBinding = new Binding("Text", this.m_Client, "ZipCode");
            this.textBoxZip.DataBindings.Add(ZipBinding);

            this.textBoxTelephone.DataBindings.Clear();
            Binding PhoneBinding = new Binding("Text", this.m_Client, "Telephone", true, DataSourceUpdateMode.OnValidation);
            PhoneBinding.Format += new ConvertEventHandler(PhoneBinding_Format);
            PhoneBinding.Parse += new ConvertEventHandler(PhoneBinding_Parse);
            this.textBoxTelephone.DataBindings.Add(PhoneBinding);

            this.textBoxFax.DataBindings.Clear();
            Binding FaxBinding = new Binding("Text", this.m_Client, "Fax", true, DataSourceUpdateMode.OnValidation);            
            FaxBinding.Format += new ConvertEventHandler(FaxBinding_Format);
            FaxBinding.Parse += new ConvertEventHandler(FaxBinding_Parse);
            this.textBoxFax.DataBindings.Add(FaxBinding);

            this.checkBoxInactive.DataBindings.Clear();
            Binding InactiveBinding = new Binding("Checked", this.m_Client, "Inactive");
            this.checkBoxInactive.DataBindings.Add(InactiveBinding);            

            this.textBoxAbbreviation.DataBindings.Clear();
            Binding AbbreviationBinding = new Binding("Text", this.m_Client, "Abbreviation");
            this.textBoxAbbreviation.DataBindings.Add(AbbreviationBinding);

            this.comboBoxDistributionMethod.DataBindings.Clear();
            Binding DistributionTypeBinding = new Binding("Text", this.m_Client, "DistributionType");
            this.comboBoxDistributionMethod.DataBindings.Add(DistributionTypeBinding);            

            this.checkBoxLongDistance.DataBindings.Clear();
            LongDistanceBinding = new Binding("Checked", this.m_Client, "LongDistance");
            this.checkBoxLongDistance.DataBindings.Add(LongDistanceBinding);

            this.textBoxContactName.DataBindings.Clear();
            Binding ContactNameBinding = new Binding("Text", this.m_Client, "ContactName");
            this.textBoxContactName.DataBindings.Add(ContactNameBinding);

            this.comboBoxFacilityType.DataBindings.Clear();
            Binding FacilityTypeBinding = new Binding("Text", this.m_Client, "FacilityType");
            this.comboBoxFacilityType.DataBindings.Add(FacilityTypeBinding);            

            this.checkBoxShowPhysiciansOnRequisition.DataBindings.Clear();
            Binding ShowPhysiciansOnRequisitionBinding = new Binding("Checked", this.m_Client, "ShowPhysiciansOnRequisition");
            this.checkBoxShowPhysiciansOnRequisition.DataBindings.Add(ShowPhysiciansOnRequisitionBinding);                
        }

        private void FaxBinding_Parse(object sender, ConvertEventArgs e)
        {
            e.Value = ParsePhoneNumber(e.Value.ToString());
        }

        private void FaxBinding_Format(object sender, ConvertEventArgs e)
        {
            if (e.Value != null)
            {
                e.Value = FormatPhoneNumber(e.Value.ToString());
            }
        }

        private void PhoneBinding_Parse(object sender, ConvertEventArgs e)
        {
            e.Value = ParsePhoneNumber(e.Value.ToString());
        }

        private void PhoneBinding_Format(object sender, ConvertEventArgs e)
        {
            if (e.Value != null)
            {
                e.Value = FormatPhoneNumber(e.Value.ToString());
            }
        }

        private void SetControlList()
        {
            this.m_ControlList.Add(this.textBoxAddress);
            this.m_ControlList.Add(this.textBoxCity);
            this.m_ControlList.Add(this.textBoxFax);
            this.m_ControlList.Add(this.textBoxName);
            this.m_ControlList.Add(this.labelClientID);
            this.m_ControlList.Add(this.textBoxState);
            this.m_ControlList.Add(this.textBoxTelephone);
            this.m_ControlList.Add(this.textBoxZip);
            this.m_ControlList.Add(this.checkBoxLongDistance);
            this.m_ControlList.Add(this.comboBoxDistributionMethod);            
            this.m_ControlList.Add(this.checkBoxInactive);            

            this.m_ControlList.Add(this.buttonAdd);            
            this.m_ControlList.Add(this.buttonRemove);                        
            this.m_ControlList.Add(this.textBoxAbbreviation);
            this.m_ControlList.Add(this.textBoxContactName);

            this.m_ControlList.Add(this.comboBoxFacilityType);            
            this.m_ControlList.Add(this.checkBoxShowPhysiciansOnRequisition);            
        }
        
        private void PopulatePhysicianList()
        {        
            this.listViewPhysicianList.Items.Clear();
            foreach(YellowstonePathology.Business.Domain.Physician physician in this.m_PhysicianCollection)
            {
                ListViewItem lvi = this.listViewPhysicianList.Items.Add(physician.PhysicianId.ToString());                
                string physicianFirstName = physician.FirstName;
                string physicianLastName = physician.LastName;
                lvi.SubItems.Add(physicianLastName + ", " + physicianFirstName);
            }
            if (this.m_PhysicianCollection.Count == 0)
            {
                this.listViewPhysicianList.Items.Add("No Items Found");
            }         
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {            
            PhysicianSearch physicianSearch = new PhysicianSearch();
            DialogResult result = physicianSearch.ShowDialog();
            
            if (result == DialogResult.OK)
            {                
                if(physicianSearch.SelectedPhysician != null)
                {
                    if (this.IsPhysicianInClient(physicianSearch.SelectedPhysician.PhysicianId) == false)
                    {
						string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
						YellowstonePathology.Business.Domain.PhysicianClient physicianClient = new Business.Domain.PhysicianClient(objectId, physicianSearch.SelectedPhysician.PhysicianId, this.m_Client.ClientId);
						YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();
                        objectTracker.RegisterRootInsert(physicianClient);
                        objectTracker.SubmitChanges(physicianClient);

						this.m_PhysicianCollection = this.m_PhysicianCollection = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysiciansByClientId(this.m_Client.ClientId);
                        this.PopulatePhysicianList();

                        MessageBox.Show("The selected physician was added.");
                    }
                    else
                    {
                        MessageBox.Show("Physician already exist.");
                    }
                }
            }            
        }

        private bool IsPhysicianInClient(long physicianId)
        {
            foreach (ListViewItem lvi in this.listViewPhysicianList.Items)
            {                
                if (lvi.SubItems.Count == 1) return false;
                int currentID = 0;
                if (Int32.TryParse(lvi.SubItems[1].Text, out currentID) == true)
                {                    
                    if (currentID == physicianId)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.m_ObjectTracker.SubmitChanges(this.m_Client);
            this.DialogResult = DialogResult.OK;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            if (this.listViewPhysicianList.SelectedItems.Count == 0)
            {
                return;
            }
            DialogResult result = MessageBox.Show("Remove selected physician?", "Remove", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                int physicianId = Convert.ToInt32(this.listViewPhysicianList.SelectedItems[0].Text);
				YellowstonePathology.Business.Domain.PhysicianClient physicianClient = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysicianClient(physicianId, this.m_Client.ClientId);
				YellowstonePathology.Business.Persistence.ObjectTracker objectTracker = new YellowstonePathology.Business.Persistence.ObjectTracker();
                objectTracker.RegisterRootDelete(physicianClient);
                objectTracker.SubmitChanges(physicianClient);

				this.m_PhysicianCollection = this.m_PhysicianCollection = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetPhysiciansByClientId(this.m_Client.ClientId);
                this.PopulatePhysicianList();                
            }
        }        
               
        private void buttonUnlock_Click(object sender, EventArgs e)
        {                        
            UnlockFormFields(this.m_ControlList);            
        }

        private void textBoxFax_Validating(object sender, CancelEventArgs e)
        {                                                
            string phoneNumber = textBoxFax.Text;
            if (this.IsFaxNumberLongDistance(phoneNumber) == true)
            {
                this.checkBoxLongDistance.Checked = true;
                //LongDistanceBinding.WriteValue();
            }
            else
            {
                this.checkBoxLongDistance.Checked = false;
                //LongDistanceBinding.WriteValue();
            }              
        }

        public bool IsFaxNumberLongDistance(string phoneNumber)
        {
            bool result = false;

            if(string.IsNullOrEmpty(phoneNumber) == false)
            {
                string areaCode = phoneNumber.Substring(1, 3);
                string prefix = phoneNumber.Substring(5, 3);

                if (areaCode != "406")
                {
                    result = true;
                }
                else
                {
                    result = YellowstonePathology.Business.Client.Model.LocalPhonePrefix.IsLongDistance(prefix);
                }
            }

            return result;
        }

        public static void UnlockFormFields(ArrayList controlList)
        {
            foreach (Control ctrl in controlList)
            {
                ctrl.Enabled = true;
            }
        }

        public static void LockFormFields(ArrayList controlList)
        {
            foreach (Control ctrl in controlList)
            {
                ctrl.Enabled = false;
            }
        }

        public static string FormatPhoneNumber(string value)
        {
            string phone = value;
            if (phone.Length == 10)
            {
                string formattedPhone = "(" + phone.Substring(0, 3) + ")";
                formattedPhone += phone.Substring(3, 3) + "-" + phone.Substring(6, 4);
                return formattedPhone;
            }
            return value;
        }

        public static string ParsePhoneNumber(string value)
        {
            string formattedPhone = value;
            string unFormattedPhone = formattedPhone.Replace("(", "");
            unFormattedPhone = unFormattedPhone.Replace(")", "");
            unFormattedPhone = unFormattedPhone.Replace("-", "");
            return unFormattedPhone;
        }        
    }
}