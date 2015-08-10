namespace YellowstonePathology.UI.Client
{
    partial class PhysicianEntry
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonUnlock = new System.Windows.Forms.Button();
            this.tabPageMembership = new System.Windows.Forms.TabPage();
            this.buttonHomeBase = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.listViewClientList = new System.Windows.Forms.ListView();
            this.columnHeaderPhysicianClientID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderClientID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderClientName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderHomeBase = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPageGeneral = new System.Windows.Forms.TabPage();
            this.comboBoxHPVStandingOrder = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.comboBoxHPV1618StandingOrder = new System.Windows.Forms.ComboBox();
            this.textBoxMDLastName = new System.Windows.Forms.TextBox();
            this.textBoxMDFirstName = new System.Windows.Forms.TextBox();
            this.textBoxNPI = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxDisplayName = new System.Windows.Forms.TextBox();
            this.textBoxInitial = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBoxKRASBRAFStandingOrder = new System.Windows.Forms.CheckBox();
            this.textBoxLastName = new System.Windows.Forms.TextBox();
            this.textBoxFirstName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxActive = new System.Windows.Forms.CheckBox();
            this.labelPhysicianId = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelPhysicianIDLabel = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.TextBoxNotificationEmail = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.checkBoxSendNotifications = new System.Windows.Forms.CheckBox();
            this.tabPageMembership.SuspendLayout();
            this.tabPageGeneral.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(737, 356);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 502;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(816, 356);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 503;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonUnlock
            // 
            this.buttonUnlock.Location = new System.Drawing.Point(12, 356);
            this.buttonUnlock.Name = "buttonUnlock";
            this.buttonUnlock.Size = new System.Drawing.Size(75, 23);
            this.buttonUnlock.TabIndex = 501;
            this.buttonUnlock.Text = "Unlock";
            this.buttonUnlock.UseVisualStyleBackColor = true;
            this.buttonUnlock.Click += new System.EventHandler(this.buttonUnlock_Click);
            // 
            // tabPageMembership
            // 
            this.tabPageMembership.Controls.Add(this.buttonHomeBase);
            this.tabPageMembership.Controls.Add(this.buttonRemove);
            this.tabPageMembership.Controls.Add(this.buttonAdd);
            this.tabPageMembership.Controls.Add(this.listViewClientList);
            this.tabPageMembership.Location = new System.Drawing.Point(4, 22);
            this.tabPageMembership.Name = "tabPageMembership";
            this.tabPageMembership.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageMembership.Size = new System.Drawing.Size(875, 258);
            this.tabPageMembership.TabIndex = 1;
            this.tabPageMembership.Text = "Membership";
            this.tabPageMembership.UseVisualStyleBackColor = true;
            // 
            // buttonHomeBase
            // 
            this.buttonHomeBase.Enabled = false;
            this.buttonHomeBase.Location = new System.Drawing.Point(432, 145);
            this.buttonHomeBase.Name = "buttonHomeBase";
            this.buttonHomeBase.Size = new System.Drawing.Size(75, 23);
            this.buttonHomeBase.TabIndex = 204;
            this.buttonHomeBase.Text = "Home Base";
            this.buttonHomeBase.UseVisualStyleBackColor = true;
            this.buttonHomeBase.Click += new System.EventHandler(this.buttonHomeBase_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.Enabled = false;
            this.buttonRemove.Location = new System.Drawing.Point(432, 48);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(75, 23);
            this.buttonRemove.TabIndex = 203;
            this.buttonRemove.Text = "Remove";
            this.buttonRemove.UseVisualStyleBackColor = true;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // buttonAdd
            // 
            this.buttonAdd.Enabled = false;
            this.buttonAdd.Location = new System.Drawing.Point(432, 19);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(75, 23);
            this.buttonAdd.TabIndex = 202;
            this.buttonAdd.Text = "Add";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // listViewClientList
            // 
            this.listViewClientList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderPhysicianClientID,
            this.columnHeaderClientID,
            this.columnHeaderClientName,
            this.columnHeaderHomeBase});
            this.listViewClientList.FullRowSelect = true;
            this.listViewClientList.HideSelection = false;
            this.listViewClientList.Location = new System.Drawing.Point(22, 19);
            this.listViewClientList.Name = "listViewClientList";
            this.listViewClientList.Size = new System.Drawing.Size(393, 178);
            this.listViewClientList.TabIndex = 201;
            this.listViewClientList.UseCompatibleStateImageBehavior = false;
            this.listViewClientList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderPhysicianClientID
            // 
            this.columnHeaderPhysicianClientID.Text = "ID";
            this.columnHeaderPhysicianClientID.Width = 0;
            // 
            // columnHeaderClientID
            // 
            this.columnHeaderClientID.Text = "ID";
            this.columnHeaderClientID.Width = 61;
            // 
            // columnHeaderClientName
            // 
            this.columnHeaderClientName.Text = "Client Name";
            this.columnHeaderClientName.Width = 232;
            // 
            // columnHeaderHomeBase
            // 
            this.columnHeaderHomeBase.Text = "Home Base";
            this.columnHeaderHomeBase.Width = 77;
            // 
            // tabPageGeneral
            // 
            this.tabPageGeneral.Controls.Add(this.checkBoxSendNotifications);
            this.tabPageGeneral.Controls.Add(this.label8);
            this.tabPageGeneral.Controls.Add(this.TextBoxNotificationEmail);
            this.tabPageGeneral.Controls.Add(this.comboBoxHPVStandingOrder);
            this.tabPageGeneral.Controls.Add(this.label7);
            this.tabPageGeneral.Controls.Add(this.comboBoxHPV1618StandingOrder);
            this.tabPageGeneral.Controls.Add(this.textBoxMDLastName);
            this.tabPageGeneral.Controls.Add(this.textBoxMDFirstName);
            this.tabPageGeneral.Controls.Add(this.textBoxNPI);
            this.tabPageGeneral.Controls.Add(this.label6);
            this.tabPageGeneral.Controls.Add(this.textBoxDisplayName);
            this.tabPageGeneral.Controls.Add(this.textBoxInitial);
            this.tabPageGeneral.Controls.Add(this.label5);
            this.tabPageGeneral.Controls.Add(this.label4);
            this.tabPageGeneral.Controls.Add(this.checkBoxKRASBRAFStandingOrder);
            this.tabPageGeneral.Controls.Add(this.textBoxLastName);
            this.tabPageGeneral.Controls.Add(this.textBoxFirstName);
            this.tabPageGeneral.Controls.Add(this.label1);
            this.tabPageGeneral.Controls.Add(this.checkBoxActive);
            this.tabPageGeneral.Controls.Add(this.labelPhysicianId);
            this.tabPageGeneral.Controls.Add(this.label3);
            this.tabPageGeneral.Controls.Add(this.label2);
            this.tabPageGeneral.Controls.Add(this.labelPhysicianIDLabel);
            this.tabPageGeneral.Location = new System.Drawing.Point(4, 22);
            this.tabPageGeneral.Name = "tabPageGeneral";
            this.tabPageGeneral.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGeneral.Size = new System.Drawing.Size(875, 312);
            this.tabPageGeneral.TabIndex = 0;
            this.tabPageGeneral.Text = "General";
            this.tabPageGeneral.UseVisualStyleBackColor = true;
            // 
            // comboBoxHPVStandingOrder
            // 
            this.comboBoxHPVStandingOrder.Enabled = false;
            this.comboBoxHPVStandingOrder.FormattingEnabled = true;
            this.comboBoxHPVStandingOrder.Location = new System.Drawing.Point(29, 231);
            this.comboBoxHPVStandingOrder.Name = "comboBoxHPVStandingOrder";
            this.comboBoxHPVStandingOrder.Size = new System.Drawing.Size(814, 21);
            this.comboBoxHPVStandingOrder.TabIndex = 115;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(26, 261);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(135, 13);
            this.label7.TabIndex = 113;
            this.label7.Text = "HPV 16/18 Standing Order";
            // 
            // comboBoxHPV1618StandingOrder
            // 
            this.comboBoxHPV1618StandingOrder.Enabled = false;
            this.comboBoxHPV1618StandingOrder.FormattingEnabled = true;
            this.comboBoxHPV1618StandingOrder.Location = new System.Drawing.Point(29, 277);
            this.comboBoxHPV1618StandingOrder.Name = "comboBoxHPV1618StandingOrder";
            this.comboBoxHPV1618StandingOrder.Size = new System.Drawing.Size(814, 21);
            this.comboBoxHPV1618StandingOrder.TabIndex = 114;
            // 
            // textBoxMDLastName
            // 
            this.textBoxMDLastName.Enabled = false;
            this.textBoxMDLastName.Location = new System.Drawing.Point(418, 63);
            this.textBoxMDLastName.Name = "textBoxMDLastName";
            this.textBoxMDLastName.Size = new System.Drawing.Size(305, 20);
            this.textBoxMDLastName.TabIndex = 104;
            this.textBoxMDLastName.Tag = "";
            this.textBoxMDLastName.Text = "MD Last Name";
            // 
            // textBoxMDFirstName
            // 
            this.textBoxMDFirstName.Enabled = false;
            this.textBoxMDFirstName.Location = new System.Drawing.Point(418, 37);
            this.textBoxMDFirstName.Name = "textBoxMDFirstName";
            this.textBoxMDFirstName.Size = new System.Drawing.Size(305, 20);
            this.textBoxMDFirstName.TabIndex = 102;
            this.textBoxMDFirstName.Tag = "";
            this.textBoxMDFirstName.Text = "MD First Name";
            // 
            // textBoxNPI
            // 
            this.textBoxNPI.Enabled = false;
            this.textBoxNPI.Location = new System.Drawing.Point(107, 138);
            this.textBoxNPI.Name = "textBoxNPI";
            this.textBoxNPI.Size = new System.Drawing.Size(305, 20);
            this.textBoxNPI.TabIndex = 107;
            this.textBoxNPI.Tag = "";
            this.textBoxNPI.Text = "NPI";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(73, 141);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(28, 13);
            this.label6.TabIndex = 112;
            this.label6.Text = "NPI:";
            // 
            // textBoxDisplayName
            // 
            this.textBoxDisplayName.Enabled = false;
            this.textBoxDisplayName.Location = new System.Drawing.Point(107, 112);
            this.textBoxDisplayName.Name = "textBoxDisplayName";
            this.textBoxDisplayName.Size = new System.Drawing.Size(305, 20);
            this.textBoxDisplayName.TabIndex = 106;
            this.textBoxDisplayName.Tag = "";
            this.textBoxDisplayName.Text = "Display Name";
            // 
            // textBoxInitial
            // 
            this.textBoxInitial.Enabled = false;
            this.textBoxInitial.Location = new System.Drawing.Point(107, 87);
            this.textBoxInitial.Name = "textBoxInitial";
            this.textBoxInitial.Size = new System.Drawing.Size(305, 20);
            this.textBoxInitial.TabIndex = 105;
            this.textBoxInitial.Tag = "";
            this.textBoxInitial.Text = "Initial";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 13);
            this.label5.TabIndex = 109;
            this.label5.Text = "Display Name:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(67, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(34, 13);
            this.label4.TabIndex = 108;
            this.label4.Text = "Initial:";
            // 
            // checkBoxKRASBRAFStandingOrder
            // 
            this.checkBoxKRASBRAFStandingOrder.AutoSize = true;
            this.checkBoxKRASBRAFStandingOrder.Enabled = false;
            this.checkBoxKRASBRAFStandingOrder.Location = new System.Drawing.Point(687, 112);
            this.checkBoxKRASBRAFStandingOrder.Name = "checkBoxKRASBRAFStandingOrder";
            this.checkBoxKRASBRAFStandingOrder.Size = new System.Drawing.Size(162, 17);
            this.checkBoxKRASBRAFStandingOrder.TabIndex = 110;
            this.checkBoxKRASBRAFStandingOrder.Text = "KRAS/BRAF Standing Order";
            this.checkBoxKRASBRAFStandingOrder.UseVisualStyleBackColor = true;
            // 
            // textBoxLastName
            // 
            this.textBoxLastName.Enabled = false;
            this.textBoxLastName.Location = new System.Drawing.Point(107, 62);
            this.textBoxLastName.Name = "textBoxLastName";
            this.textBoxLastName.Size = new System.Drawing.Size(305, 20);
            this.textBoxLastName.TabIndex = 103;
            this.textBoxLastName.Tag = "";
            this.textBoxLastName.Text = "Last Name";
            // 
            // textBoxFirstName
            // 
            this.textBoxFirstName.Enabled = false;
            this.textBoxFirstName.Location = new System.Drawing.Point(107, 37);
            this.textBoxFirstName.Name = "textBoxFirstName";
            this.textBoxFirstName.Size = new System.Drawing.Size(305, 20);
            this.textBoxFirstName.TabIndex = 101;
            this.textBoxFirstName.Tag = "";
            this.textBoxFirstName.Text = "First Name";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 212);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 13);
            this.label1.TabIndex = 105;
            this.label1.Text = "HPV Standing Order";
            // 
            // checkBoxActive
            // 
            this.checkBoxActive.AutoSize = true;
            this.checkBoxActive.Enabled = false;
            this.checkBoxActive.Location = new System.Drawing.Point(687, 91);
            this.checkBoxActive.Name = "checkBoxActive";
            this.checkBoxActive.Size = new System.Drawing.Size(56, 17);
            this.checkBoxActive.TabIndex = 109;
            this.checkBoxActive.Text = "Active";
            this.checkBoxActive.UseVisualStyleBackColor = true;
            // 
            // labelPhysicianId
            // 
            this.labelPhysicianId.AutoSize = true;
            this.labelPhysicianId.Location = new System.Drawing.Point(107, 16);
            this.labelPhysicianId.Name = "labelPhysicianId";
            this.labelPhysicianId.Size = new System.Drawing.Size(66, 13);
            this.labelPhysicianId.TabIndex = 100;
            this.labelPhysicianId.Text = "Physician ID";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(40, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Last Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "First Name:";
            // 
            // labelPhysicianIDLabel
            // 
            this.labelPhysicianIDLabel.AutoSize = true;
            this.labelPhysicianIDLabel.Location = new System.Drawing.Point(32, 16);
            this.labelPhysicianIDLabel.Name = "labelPhysicianIDLabel";
            this.labelPhysicianIDLabel.Size = new System.Drawing.Size(69, 13);
            this.labelPhysicianIDLabel.TabIndex = 1;
            this.labelPhysicianIDLabel.Text = "Physician ID:";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPageGeneral);
            this.tabControl.Controls.Add(this.tabPageMembership);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(883, 338);
            this.tabControl.TabIndex = 300;
            // 
            // TextBoxNotificationEmail
            // 
            this.TextBoxNotificationEmail.Enabled = false;
            this.TextBoxNotificationEmail.Location = new System.Drawing.Point(107, 164);
            this.TextBoxNotificationEmail.Name = "TextBoxNotificationEmail";
            this.TextBoxNotificationEmail.Size = new System.Drawing.Size(305, 20);
            this.TextBoxNotificationEmail.TabIndex = 116;
            this.TextBoxNotificationEmail.Tag = "";
            this.TextBoxNotificationEmail.Text = "Notification Email";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 167);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(91, 13);
            this.label8.TabIndex = 117;
            this.label8.Text = "Notification Email:";
            // 
            // checkBoxSendNotifications
            // 
            this.checkBoxSendNotifications.AutoSize = true;
            this.checkBoxSendNotifications.Enabled = false;
            this.checkBoxSendNotifications.Location = new System.Drawing.Point(687, 135);
            this.checkBoxSendNotifications.Name = "checkBoxSendNotifications";
            this.checkBoxSendNotifications.Size = new System.Drawing.Size(112, 17);
            this.checkBoxSendNotifications.TabIndex = 118;
            this.checkBoxSendNotifications.Text = "Send Notifications";
            this.checkBoxSendNotifications.UseVisualStyleBackColor = true;
            // 
            // PhysicianEntry
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(907, 391);
            this.Controls.Add(this.buttonUnlock);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.tabControl);
            this.Name = "PhysicianEntry";
            this.ShowInTaskbar = false;
            this.Text = "Physician";
            this.Load += new System.EventHandler(this.Physician_Load);
            this.tabPageMembership.ResumeLayout(false);
            this.tabPageGeneral.ResumeLayout(false);
            this.tabPageGeneral.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonUnlock;
		private System.Windows.Forms.TabPage tabPageMembership;
		private System.Windows.Forms.Button buttonHomeBase;
		private System.Windows.Forms.Button buttonRemove;
		private System.Windows.Forms.Button buttonAdd;
		private System.Windows.Forms.ListView listViewClientList;
		private System.Windows.Forms.ColumnHeader columnHeaderPhysicianClientID;
		private System.Windows.Forms.ColumnHeader columnHeaderClientID;
		private System.Windows.Forms.ColumnHeader columnHeaderClientName;
		private System.Windows.Forms.ColumnHeader columnHeaderHomeBase;
		private System.Windows.Forms.TabPage tabPageGeneral;
		private System.Windows.Forms.TextBox textBoxDisplayName;
		private System.Windows.Forms.TextBox textBoxInitial;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBoxKRASBRAFStandingOrder;
		private System.Windows.Forms.TextBox textBoxLastName;
		private System.Windows.Forms.TextBox textBoxFirstName;
        private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox checkBoxActive;
		private System.Windows.Forms.Label labelPhysicianId;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label labelPhysicianIDLabel;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TextBox textBoxNPI;
		private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxMDLastName;
        private System.Windows.Forms.TextBox textBoxMDFirstName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBoxHPV1618StandingOrder;
        private System.Windows.Forms.ComboBox comboBoxHPVStandingOrder;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox TextBoxNotificationEmail;
        private System.Windows.Forms.CheckBox checkBoxSendNotifications;
    }
}